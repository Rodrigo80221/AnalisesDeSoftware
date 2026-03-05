# Otimização de Performance na Inserção de Vendas

## Task 1: [Serviço Telecon] Atualização Assíncrona da DataUltimaVenda via Thread (C#) e Remoção da FN_RetornarDataUltimaVenda do Fluxo de Venda

### Contexto
Atualmente o sistema utiliza a função `FN_RetornarDataUltimaVenda` durante o fluxo de venda para calcular a última data de venda dos produtos. Essa função executa consultas no histórico de vendas, gerando impactos negativos. Como essa lógica não precisa ocorrer em tempo real, ela será removida do fluxo transacional. A atualização passará a ser realizada por uma rotina assíncrona no Serviço Telecon.

• Impactos gerados pela função atual
     - Leituras pesadas nas tabelas de vendas
     - Aumento do tempo de gravação do cupom
     - Contenção de locks durante a venda
     - Impacto direto no desempenho dos PDVs

### Objetivo
Remover completamente o cálculo de `DataUltimaVenda` do fluxo de venda e transferir essa responsabilidade para uma thread dedicada dentro do Serviço Telecon, que atualizará os dados periodicamente.

### Locais onde a função é chamada
A função `FN_RetornarDataUltimaVenda` foi identificada nos seguintes objetos do banco de dados:

• Stored Procedure
     - SP_ALTERAR_ESTOQUE (Tipo: SQL_STORED_PROCEDURE)

• Triggers
     - TG_NF_SAIDAS_PRODUTOS_DtUltimaVenda (Tipo: SQL_TRIGGER)
     - TG_UPD_NF_SAIDAS_DtUltimaVenda (Tipo: SQL_TRIGGER)

### Ação Necessária
Remover todas as chamadas da função `FN_RetornarDataUltimaVenda` dos objetos identificados. Após essa alteração, nenhuma venda deverá executar essa função.

• Objetos identificados para alteração
     - SP_ALTERAR_ESTOQUE
     - TG_NF_SAIDAS_PRODUTOS_DtUltimaVenda
     - TG_UPD_NF_SAIDAS_DtUltimaVenda

### Nova Arquitetura
O cálculo da `DataUltimaVenda` passará a ocorrer fora do fluxo de venda, sendo executado por uma Thread dentro do Serviço Telecon. Essa thread executará periodicamente um script SQL otimizado que atualiza os produtos com base nas movimentações recentes.

### Passos de Implementação

**Passo 1 — Remover chamadas da função**
Remover das procedures e triggers os trechos semelhantes a:
```sql
SELECT dbo.FN_RetornarDataUltimaVenda(...)
-- ou
SET @DataUltimaVenda = dbo.FN_RetornarDataUltimaVenda(...)
```

**Passo 2 — Criar Thread no Serviço Telecon**
Criar uma nova Thread que será iniciada no `OnStart` do Serviço Telecon. Essa Thread será responsável por executar o cálculo da `DataUltimaVenda` periodicamente.

**Passo 3 — Criar loop de execução**
A Thread deve possuir um loop `while` que executa o script SQL no banco, aguarda 5 minutos e repete o processo.
```csharp
Thread.Sleep(300000); // (300.000 ms = 5 minutos)
```

**Passo 4 — Tratamento de erros**
Implementar bloco `try/catch` dentro do loop. A exceção deve ser registrada no log do Serviço Telecon, mas a Thread não deve ser finalizada.

• Possíveis erros a serem tratados
     - Timeout
     - Falha de conexão
     - Erro de execução SQL

**Passo 5 — Controle de parada**
No método `OnStop()` do Serviço Telecon, alterar a variável de controle:
```csharp
_servicoRodando = false;
```

### Código C#
```csharp
private Thread _threadUltimaVenda;
private bool _servicoRodando = true;

public void IniciarThreadDataUltimaVenda()
{
    _threadUltimaVenda = new Thread(LoopAtualizacaoUltimaVenda);

    _threadUltimaVenda.IsBackground = true;
    _threadUltimaVenda.Name = "ThreadAtualizacaoUltimaVenda";

    _threadUltimaVenda.Start();
}

private void LoopAtualizacaoUltimaVenda()
{
    while (_servicoRodando)
    {
        try
        {
            using (SqlConnection conexao = new SqlConnection("SUA_CONNECTION_STRING"))
            {
                // Executar script SQL abaixo
            }
        }
        catch (Exception ex)
        {
            // Logar erro no Serviço Telecon
        }

        Thread.Sleep(300000);
    }
}
```

### Script SQL
(Mesmo script já definido para atualização das últimas vendas).

### Observação Rodrigo Goulart
Também deve ser garantida a atualização da `DataUltimaVenda` na tabela Produtos, mantendo consistência.

• Tabelas para manter consistência
     - ProdutoLojas
     - Produtos

Consulta atual:
```sql
SELECT DT_ULTIMA_VENDA
FROM produtos
```

---

## Task 2: [Serviço Telecon] Isolamento de Leitura nas Consultas (Inclusão de NOLOCK)

### Contexto
O Serviço Telecon executa diversas rotinas em background que realizam leituras e agregações sobre o histórico de vendas. Essas consultas podem manter locks compartilhados (`LCK_M_S`) que colidem com locks exclusivos (`LCK_M_X`) gerados pelos PDVs durante a gravação de cupons.

• Tabelas afetadas pelos locks
     - Vendas
     - VendasProdutos

• Problemas gerados
     - Lentidão no PDV
     - Filas de gravação de vendas
     - Contenção nas tabelas transacionais

### Objetivo
Garantir que as rotinas de leitura executadas pelo Serviço Telecon não bloqueiem as operações transacionais dos PDVs.

• Tabelas que receberão o comando WITH (NOLOCK)
     - Vendas
     - VendasProdutos

### Rotinas Identificadas

**1. ChamarIntegracaoScannTech**
Consultas extensivas nas tabelas.

• Tabelas consultadas
     - Vendas
     - VendasProdutos
     - Tabelas financeiras auxiliares

```sql
FROM Vendas V WITH (NOLOCK)
INNER JOIN VendasProdutos VP WITH (NOLOCK)
    ON VP.CodVenda = V.Codigo
```

**2. ChamarIntegracaoPdvPos**
Validação de vendas vindas da API.
```sql
FROM Vendas V WITH (NOLOCK)
```

**3. MonitorDeTransacaoETaxasDoTEF**
Consulta grande envolvendo múltiplas tabelas.

• Tabelas envolvidas
     - Vendas
     - VendasFormas
     - VendasFormasTEF

```sql
FROM Vendas V WITH (NOLOCK)
INNER JOIN VendasFormas VF WITH (NOLOCK)
    ON ...
INNER JOIN VendasFormasTEF VFT WITH (NOLOCK)
    ON ...
```

**4. ChamarIntegracaoCresceVendas**
Consultas de agregação.
```sql
SELECT IsNull(Count(*),0) AS NroVendas
FROM Vendas WITH (NOLOCK)

SELECT IsNull(Sum(Total + DescontoAcrecimo),0) AS TotalVendas
FROM Vendas WITH (NOLOCK)
```

---

## Task 3: [Torus PDV] Ajustar geração de ID para utilizar SEQUENCE

### Contexto
Atualmente os IDs são gerados usando `SELECT MAX(Codigo) + 1`, o que gera varredura completa de tabela e gargalos de I/O.

• Tabelas afetadas
     - Vendas
     - HistoricosPDV

### Objetivo
Substituir a lógica por `SEQUENCE` do SQL Server.

### Passos

**Passo 1**
Ler as parametrizações.

• Parâmetros a serem lidos
     - NAO_USAR_SQ_VENDAS (2865)
     - NAO_USAR_SQ_HISTORICOSPDV (2945)

**Passo 2 — Vendas**
Se permitido:
```sql
SELECT NEXT VALUE FOR dbo.Seq_Vendas
```
Caso contrário:
```sql
SELECT MAX(Codigo) + 1 FROM Vendas
```

**Passo 3 — HistoricosPDV**
```sql
SELECT NEXT VALUE FOR dbo.Seq_HistoricosPDV
```
Ou fallback:
```sql
SELECT MAX(Codigo) + 1 FROM HistoricosPDV
```

**Fallback de segurança**
Se ocorrer erro de PK, executar fallback (`SELECT MAX(Codigo) + 1`) e tentar novamente.

---

## Task 4: Refatorar Trigger TG_EXPORTACAO_TELEVO

### Problema
A trigger usa cursor (`DECLARE NOVO_DRIVE CURSOR`). Isso gera processamento linha a linha (Row-By-Row).

### Ação
Converter para Set-Based.

### Passos

**Passo 1**
Remover cursor e bloco WHILE.

**Passo 2**
Criar `INSERT` via `SELECT`:
```sql
INSERT INTO DriveFilaExportacoes (...)
SELECT ...
FROM inserted I
LEFT JOIN deleted D
ON I.Codigo = D.Codigo
```

---

## Task 5: Remover Cursores da Baixa de Fórmulas (SP_ESTOQUE_FORMULA)

### Problema
A procedure utiliza `DECLARE rscursor CURSOR LOCAL` para dar baixa em ingredientes. Isso degrada a performance do cupom.

### Ação
Eliminar cursor e usar processamento em conjunto.

### Passos

**Passo 1**
Reescrever a procedure.

**Passo 2**
Substituir a lógica atual.

• Abordagens para substituição da lógica
     - CTEs
     - Updates massivos

Exemplo:
```sql
UPDATE ProdutoEstoqueLoja
SET ...
FROM ...
```