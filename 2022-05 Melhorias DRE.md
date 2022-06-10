# Épico: Reformulação do DRE
Data de início da análise: 23/05/2022

## Problema a ser resolvido
Diferenças nas informações das vendas e custo das mercadorias vendidas entre o DRE Gerencial e o ABC de Mercadorias.

## Impactos
Não terá impacto pois será uma tela nova somente para exibição de dados.

## Pré Requisitos
Será necessário ter o ambiente do GestaoRelatorios funcionando.
Necessita o Finish da feature do ABC 2.0

## Solução

Foi realizada análise para uma nova tela conforme o protótipo abaixo. Essa novas tela deverá atender as especificações abaixo:
- Será criada uma tela para melhor visualização dos dados. A nova tela deverá atender os padrões de qualidade da Telecon Sistemas.
- Consultar no banco GestaoRelatorios (data warehouse criado pelo Samir: Tela Classica > Sistema > Configurações Gestão Relatórios)
- Mostrar os dados de venda e custo das mercadorias não mais do financeiro e sim diretamente das vendas.
- Mostrar os dados de despesas de acordo com as informações que já tínhamos no DRE Gerencial. (Irá mudar alguns nomes de contas apenas.)
- Nas informações de despesas devemos manter regras antigas como NF de entrada e saída de estorno + taxas, juros, descontos e multas nos registros do financeiro.

![image](https://user-images.githubusercontent.com/80394522/171756235-e8adbe33-4845-4b6f-85f4-95090ad372c3.png)

Abaixo uma imagem com as principais mudanças em relação ao DRE Gerencial
![Alterações DRE Gerencial](https://user-images.githubusercontent.com/80394522/173059149-3893c07d-e6eb-4fc6-8175-7b1be9d4f308.png)

------------------------------------------------------------------------------------------------------

## Tarefa 1: Criar feature no git e ajustar ambiente

1. git flow feature start NovoRelatorioDRE
2. Criar ambiente GestaoRelatorios
- Tela Classica > Sistema > Configurações Gestão Relatórios

## Tarefa 2: Criar novo formulário

1. Criar no C# o formulário FrmRelDRE no caminho  GestaoComercial > Formularios > Financeiro
2. Criar layout conforme protótipo no final da tarefa.
    - Adicionar o combo + checkbox de lojas com a opção de marcar e desmarcar todos
    - Adicionar o combo CMV com as Oções "Custo Médio" e "Custo Gerencial"
3. No novo form implementar as configurações padrões da Telecon
    - Criar formulário do tamanho 1024x768
    - Fechar com esc
    - Enter deve funcionar como tab
    - Ícone do sistema S na janela
    - O botão Sair fecha a tela
    - Não deve permitir o resize da tela com o mouse
    - Ao maximizar deveremos apenas ancorar os botões e o grid no botton, assim iremos apenas aumentar o grid para baixo.
    - Deve abrir centralizado. 


![image](https://user-images.githubusercontent.com/80394522/171756235-e8adbe33-4845-4b6f-85f4-95090ad372c3.png)

## Tarefa3: Carregamento da tela (Form Load) 
 
1. Criar label com a data de início e fim dos dados disponibilizados no data warehouse assim como no relatório ABC 2.0 
- Não está no protótipo. Adicionar abaixo dos campos no groupbox de filtros.
- Seguir exemplo abaixo
- Já fazer uma validação para caso não exista o banco GestaoRelatorios. Neste caso mostrar uma mensagem e desatiblitar o botão consultar.

![image](https://user-images.githubusercontent.com/80394522/172939691-b576749e-36a9-4977-99b9-c83ffdda88f2.png)

2. No início do botão consultar dar a mensagem abaixo e impedir que o filtro de data esteja fora do intervalo de dados do data warehouse.
```
    "Os parâmetros de data estão fora do período disponível para a consulta.
     Revise os filtros!"
```
- colocar o foco no campo de data inicial

DRG (Demonstrativo de Resultado Gerencial)
## Tarefa 4: Criar módulo para Gerenciar o recurso Relatório DRE Gerencial

1. Criar verifica banco para inserir o código abaixo
sInserirModulo 572, "FINANCEIRO", "Relatório DRE Gerencial", eStatusModulo.mOCULTO, 127, 10, "FrmRelDRE", True, True, True, True, 5

2. Validar se apareceu no módulo de indicadores e também no novo menu do lado esquerdo do novo sistema S
Se não ficar ativo para todo usuários executar update em operadores_modulos para ativar o módulo

3. Chamar na tela clássica no menu de Financeiro > Relatório DRE Gerencial
Adicionar abaixo do DRE Gerencial, logo será substituído.

4. Adicionar no procedimento do botão `Padronizar recursos de software` 
- Esse botão fica no setor Retaguarda
- Ao padronizarmos a nova tela deverá ficar no setor financeiro.

## Tarefa 4: Implementar o botão Consultar

obs: Utilizar como base o Relatório Analise de Venda Conjunta e Relatório Pack Virtual

1. No clique do botão consultar (assim como no relatório Analise de venda conjunta)
- Alterar o ponteiro do mouse para aguardando
- Limpar a grade
- Criar e Popular uma classe filtros contendo os filtros da tela.
- Criar o procedimento `processar` e chamar ele por uma thread passando a classe filtro por parâmetro
- Estartar a thread

1. Criar diretório "DREGerencial" no caminho Telecon.GestaoComercial.Biblioteca.Relatorios

1. Criar a classe DREGerencialLinhaRelatorio com as propriedades abaixo

``` c sharp

    public enum OrigemRegistro
    {
            InseridoManualmente = 0,
            PlanoDeContas,
            TabelaVendas,
    }

    public string CodEstrutura { get; set; }
    public double CodConta { get; set; }
    public string Descricao { get; set; }
    public decimal Valor { get; set; }
    public decimal PorcentagemReceita { get; set; }
    public decimal PorcentagemDespesa { get; set; }
    public OrigemRegistro TipoDeRegistro { get; set; }

```
2. No diretório "DREGerencial" Criar a classe `DREGerencialRelatorio`
- Na classe `DREGerencialRelatorio` criar o procedimento `ConsultarRelatorioDRE` que retorne um list de `DREGerencialLinhaRelatorio` e receba a classe de filtros por parâmetro. Se não der podemos transferir a classe de filtros para o diretório DREGerencial
- Na classe `DREGerencialRelatorio` criar o procedimento `void MontarEstruturaDRE` passando a classe de filtros
- Na classe `DREGerencialRelatorio` criar o procedimento `void CarregarResultadoBruto` passando a classe de filtros
- Na classe `DREGerencialRelatorio` criar o procedimento `void CarregarDespesas`  passando a classe de filtros

3. No procedimento `processar` 
- Chamar o procedimento `ConsultarRelatorioDRE` passando a classe de filtros
- Com o retorno do `ConsultarRelatorioDRE` iremos carregar o grid, deixar apenas um comentário pois iremos fer esta parte mais na frente.
- Após consultar colocar o foco na grade

## Tarefa 5: Implementar procedimento "ConsultarRelatorioDRE" e "MontarEstruturaDRE"

1. Criar a variável list `<List>DREGerencialLinhaRelatorio listaDRE`

2. No `ConsultarRelatorioDRE` chamar o procedimento `MontarEstruturaDRE` passando a `listaDRE` e por parâmetro de referência.

3. Implementar o procedimento `MontarEstruturaDRE`
- Na lista `listaDRE` Adicionar manualmente os itens abaixo  

 ```
    CodEstrutura = 3
    Descricao = RESULTADO GERENCIAL
    TipoDeRegistro = InseridoManualmente

    CodEstrutura = 3.1
    Descricao = RESULTADO BRUTO
    TipoDeRegistro = InseridoManualmente

    CodEstrutura = 3.1.1
    Descricao = Vendas
    TipoDeRegistro = InseridoManualmente

    CodEstrutura = 3.1.1.01
    Descricao = Vendas NFCe
    TipoDeRegistro = TabelaVendas

    CodEstrutura = 3.1.1.02
    Descricao = Vendas NFe
    TipoDeRegistro = TabelaVendas

    CodEstrutura = 3.1.2
    Descricao = (-) Custo das Mercadorias Vendidas ***
    TipoDeRegistro = TabelaVendas

    * os outros campos recebem zero
```
*** A descrição o custo pode variar de acordo com o combro (Custo Médio / Custo Gerencial). Deverá ficar no formato abaixo 
"(-) Custo Médio das Mercadorias Vendidas"
"(-) Custo Gerencial das Mercadorias Vendidas"

4. Inserir na `listaDRE` todo o plano de contas a partir do "3.2" que está na configuração `Conta100PorCentoPagar`
- Criar a variável `var contaDREDespesas = config.Conta100PorCentoPagar.CodEstrutural;`
- Adicionar na `listaDRE` todo o restante do plano de contas assim como foi feito no procedimento `PlanoConta.ConsultarAPartirEstrutura(banco, contaDRE);` 
- Ao adicionar os registros na listas setar a propriedade `TipoDeRegistro = PlanoDeContas`

5. Chamar o procedimento `CarregarListaDespesas` passando a `listaDRE` por parâmetro de referência + a variável de filtros.

6. Chamar o procedimento `RetornarLinhasDespesasGerenciaisOperacionais` passando a `listaDRE` por parâmetro de referência + a variável de filtros.

7. Retorar a função com a variável `listaDRE`


## Tarefa 6: Implementar procedimento "processar" no FrmRelatorioPack

obs: Realizar o procedimento de forma semelhante ao processar do FrmRelatorioPack

1. Deixar visível o gif de aguarde durante a consulta, com o grid invisivel e o group box de viltros desabilitado
2. Realizar tratamento caso a lista venha vazia (se utilizar o check box para remover contas zeradas a listaa poderá vir vazia)
3. Formatar o grid
4. Atualizar a linha de totais
5. Carregar o Grid
    - Colocar em vermelho quando a coluna valor for negativa (retirando o sinal de - (menos))
    - Por enquanto fazer só essa parte, mais a frente ajustaremos melhor o grid

## Tarefa 7: Implementar procedimento "CarregarResultadoBruto"

1. Realizar uma consulta no banco GestaoRelatorios conforme os passos abaixo e atualizar a propriedade `Valor` na `listaDRE` que veio por parâmetro com as informações de Venda e custo.
Requisitos para a consulta:
- Buscar as vendas no banco GestaoRelatorios, para a variável IBanco utilizar `Utilitarios.ObterConexaoRelatorios();`. 
- Consultar na tabela `VendasDia` utilizando os filtros (where) do mesmo formato que foi utilizado no procedimento `Telecon.GestaoComercial.Biblioteca.Relatorios.ResultadoLoja.VisaoGeral.Consultar`
- Valor vendas pdv = sum(vendasDia) where TipoVenda = 'NFCe'
- Valor vendas nfe = sum(vendasDia) where TipoVenda =  'NFe'
- Diferenciar Custo Médio ou Custo Gerencial dependendo do combo (filtro)

2. Atualizar a propriedade `Valor` da `listaDRE` no item correspondente

## Tarefa 8: Implementar procedimento "CarregarListaDespesas"  (Parte 1)

obs 1: Iremos utilizar o procedimento Telecon.GestaoComercial.Biblioteca.Financeiro.RelDRE.ConsultarDRE para
utilizar como base.

obs 2: Iremos utilizar o procedimento Telecon.GestaoComercial.Biblioteca.Financeiro.RelDRE.ConsultarLancamentos para
utilizar como base.

1. Criar a variável com a configuração da contaDRE `var contaDRE = config.Conta100PorCentoPagar.CodEstrutural;`

2. Realizar consulta na tabela LancamentosFinanceirosPagamento semelhante a consulta abaixo utilizando os filtros corretos (where)
- Para a consulta usar o filtro abaixo
Filtro 1:  CodEstrutural LIKE `var contaDRE + %` (ficará algo semelhante a consulta logo abaixo)
Filtro 2: `Cancelado = " + banco.ObterVerdadeiroFalso(false)`
Filtro 3: Utilizar Filtros de data nesse modelo `sb.AppendLine(" AND " + new CalculosRelatoriosSQL().SoData("LP.DataHoraPagamento") + " >= " + banco.ObterData(Convert.ToDateTime(dataInicio)));`

``` SQL
    SELECT TAB.CodEstrutural, TAB.Descricao, SUM(TAB.VALOR) FROM
    (

        select PC.CodEstrutural, PC.Descricao, LFR.* from [dbo].[LancamentosFinanceirosReceber][LFR]
        inner join [dbo].[PlanoContas][PC] ON LFR.CodContaReceber = PC.CodConta 
        WHERE CodEstrutural LIKE '3.%' AND YEAR(LFR.DataCompetencia) = 2022 AND MONTH(LFR.DataCompetencia) = 2

    ) AS TAB
    GROUP BY TAB.CodEstrutural, TAB.Descricao
```
3. Percorrer os dados da consulta acima atualizando a propriedade `Valor` da `listaDRE` (quando débito deverá ser negativo)
obs: seria interessante criar um procedimento para isso que pudesse ser utilizado também nas outras consultas

4. Realizar consulta na tabela LancamentosFinanceirosRecebimentos semelhante a consulta abaixo utilizando os filtros corretos (where)
- Para a consultas usar o filtro abaixo
Filtro 1:  CodEstrutural LIKE `var contaDRE + %` (ficará algo semelhante a consulta logo abaixo)
Filtro 2: `Cancelado = " + banco.ObterVerdadeiroFalso(false)`
Filtro 3: Utilizar Filtros de data nesse modelo `sb.AppendLine(" AND " + new CalculosRelatoriosSQL().SoData("LP.DataHoraPagamento") + " >= " + banco.ObterData(Convert.ToDateTime(dataInicio)));`

``` SQL


    SELECT  TAB.CodEstrutural, TAB.Descricao, SUM(TAB.VALOR) FROM
    (

        select PC.CodEstrutural, PC.Descricao, LFR.* from [dbo].[LancamentosFinanceirosPagar][LFR]
        inner join [dbo].[PlanoContas][PC] ON LFR.CodContaPagar = PC.CodConta 
        WHERE CodEstrutural LIKE '3.%' AND YEAR(LFR.DataCompetencia) = 2022 AND MONTH(LFR.DataCompetencia) = 2

    ) AS TAB
    GROUP BY TAB.CodEstrutural, TAB.Descricao
    order by TAB.CodEstrutural 

```
5. Percorrer os dados da consulta acima atualizando a propriedade `Valor` da `listaDRE`


## Tarefa 9: Implementar procedimento "CarregarListaDespesas" (Parte 2)

Obs 1: Iremos utilizar o procedimento Telecon.GestaoComercial.Biblioteca.Financeiro.RelDRE.ConsultarLancamentos para
utilizar como base.

Obs 2: Realizar as consultas abaixo assim como no procedimento `ConsultarLancamentos` 

obs 3: Em vez da tabela Gestao.LancamentosFinanceiros iremos utilizar as tabelas GestaoRelatorios.LancamentosFinanceirosReceber e GestaoRelatorios.LancamentosFinanceirosPagar, de acordo com cada consulta (débito ou crédito)

obs 4: Não precisaremos dos filtros (where) de codestrutura, em vez disso iremos verificar se estão na tabela de recebimentos ou pagamentos

1. Criar consulta para buscar lançamentos financeiros referentes a estorno nas notas de saída do tipo de operação Estorno de NF-e (DÉBITO)
2. Criar consulta para buscar lançamentos financeiros referentes a estorno nas notas de saída do tipo de operação Estorno de NF-e (CRÉDITO)
3. Criar consulta para buscar lançamentos financeiros referentes a estorno nas notas de entrada do tipo de operação Estorno de NF-e (DÉBITO)
4. Criar consulta para buscar lançamentos financeiros referentes a estorno nas notas de entrada do tipo de operação Estorno de NF-e (CRÉDITO) que estejam nas contas de débito e crédito
5. Criar consulta para buscar lançamentos financeiros referentes a estorno nas notas de entrada do tipo de operação Estorno de NF-e (CRÉDITO) que estejam nas notas de crédito e não nas de débito

6. Percorrer os dados das consultas acima atualizando a propriedade `Valor` da `listaDRE` (quando débito deverá ser negativo)

7.. Adicionar nas contas os valores de Juros, taxas, descontos e multas semelhante a como foi feito no procedimento `ConsultarLancamentos`
- Criar consulta para buscar os valores de Juros, taxas, descontos e multas
- Percorrer os dados da consulta acima atualizando a propriedade `Valor` da `listaDRE`

- Alterar a descrição da conta `Despesas Fixas` para `(-) DESPESAS GERENCIAIS OPERACIONAIS` 


## Tarefa 10: Finalizar dados na lista


1. No `FrmRelDRE`criar procedimento para atualizar o valor das linhas de cabeçalho da lista `listaDRE`. Chamar esse procedimento antes de caregar o Grid.
- Utilizar o procedimento abaixo como base
`Telecon.GestaoComercial.Biblioteca.Financeiro.RelDRE.AtualizarContasPais(contas);`

2. Criar procedimento para atualizar os percentuais 
- Utilizar o procedimento abaixo como base
`Telecon.GestaoComercial.Biblioteca.Financeiro.RelDRE.AtualizarPercentuais`. Chamar esse procedimento após o de cima e antes de caregar o Grid.
- Temos também um arquivo de apoio
[Link Planilha](https://docs.google.com/spreadsheets/d/1cr54cDCsruG1pRD61DhnGFrpHO5xjzVNp-8DjKglcHg/edit?usp=sharing)

3. Fazer if para exluir do list as contas sem saldo caso selecionado pelo usuário, algo semelhante ao código abaixo. Chamar esse procedimento antes de caregar o Grid.

``` C sharp
            if (!visualizarContasSemSaldo)
                contas.RemoveAll(c => c.Valor.Equals(0));
```

## Tarefa 11: Implementar procedimento "processar" (Parte 2 - Carregamento do Grid)           

1. Carregar a `listaDRE` no grid
2. Ao carregar o grid caso o código estrutural possua outros derivados na lista `listaDRE` (usar lambda) colocar um `"+"` na primeira coluna
3. Nos 2 primeiros (quando codEstrutura Não possui "." ou possui 1 ponto "." ) a fonte ficará com a fonte em maiúsculo 
4. Os 3 primeiros níveis terão coloração de cinza em degradê na linha assim como no exemplo do excel.  (Tratar pelo número de "." no codStrutural)
    - Criar constantes para as cores, caso desajarem trocar fica mais fácil.
    - [Link Planilha](https://docs.google.com/spreadsheets/d/1cr54cDCsruG1pRD61DhnGFrpHO5xjzVNp-8DjKglcHg/edit?usp=sharing)
5. Ao inserir a descrição na linha terá uma tabulação no início. O número de tab é igual ao número de "." no codEstruturaal
- Após carregar o grid dar um Hide em todas as linhas que não possuam o `"+"`

## Tarefa 12: Implementar o recolher e expandir linhas

1. Ao clicar no "+' ou "-" verificar se na lista tem derivados no codestrutural e esconder ou exibir as linhas.
- No relatório do pack virtual existe um recurso semelhante


## Tarefa 13: Validação dos Dados

Essa será a tarefa mais difícil teremos que fazer uma auditoria nos dados

1. Criar notas de estorno de entrada e saída para verificarmos como fica no DRE antigo e no novo;
2. Gerar juros, multa, desconto e taxas no financeiro;
3. Bater os dados do novo DRE com o Antigo DRE, Novo ABC 2.0 e Antigo ABC de Mercadorias
4. Fazer os ajustes necessários
- Se necessário teremos que alterar a criação das tabelas no Data Warehouse GestaoRelatorios

## Tarefa 14: Criar as chamadas para outras telas do c# ou vb

1. Grid: linha de vendas ou custos das mercadorias
- Alterar o cursor para mouse hand ao colocar o mouse sob esse tipo de linha (TipoDeRegistro.TabelaVendas)
- Caso clique na linha de vendas ou custos das mercadorias abrir o Reltório do ABC 2.0 (Resultado da Loja) com mesma data, loja e combo de cmv 

2. Grid: linha de contas
- Alterar o cursor para mouse hand ao colocar o mouse sob esse tipo de linha  (TipoDeRegistro.PlanoDeContas)
- Caso clique na linha de contas abrir a tela Lançamentos Financeiros filtrando pela mesma conta e data na loja todas

## Tarefa 15: Implementar botão de imprimir
1. Pergutar se a impressão será analítica ou sintética. 
- Mostrar a mensagem "Deseja visualizar as informações referentes as contas (Analítico)?"
2. Tratar a impressão analítica/ sintética no mesmo formato que o relatório antigo.
3. No final da impressão colocar observação do tipo de análise é "Data de Competência: Análise do Resultado do Exercício"   
4. Criar o mesmo relatório do DRE antigo 
5. Melhorar dados de cabeçalho do relatório contendo todas as inforações de filtros, seguindo a formatação do exemplo abaixo mas ocupando o menor espaço possível.
![image](https://user-images.githubusercontent.com/80394522/172962951-6ebf0732-3c3b-4014-a0ef-9156dd77738f.png)

## Tarefa 16: Implementar botão de exportar
1. Criar o exportar no mesmo formato que já temos no DRE antigo

## Tarefa 17: Criar botão Question

1. Colocar um botão "?" (mesmo ícone da tela pack virtual) do lado direito do groupbox de filtros que exiba o mensagebox abaixo:
```
    O DRE Gerencial (Demonstrativo de Resultado de Exercício Gerencial) exibe as receitas, custos e despesas operacionais e não operacionais segundo a competência das movimentações financeiras.
    As informações de venda e custo das mercadorias vendidas são obtidas diretamente dos registros de vendas.
    As informações de despesas operacionais e não operacionais são obtidas dos lançamentos financeiros.
```

## Tarefa 18: Últimos ajustes
- Ajustar Tab Index
- Tratar cursor do mouse nos botões de imprimir e exportar
- Realizar testes e ajustes gerais
- Comemorar e correr para o abraço







------------------------------------------------------------------------------------------------------

![image](https://user-images.githubusercontent.com/80394522/170387581-be5e6dff-f95a-4924-9a1b-dbf9045d7a5e.png)

------------------------------------------------------------------------------------------------------

![image](https://user-images.githubusercontent.com/80394522/169874103-36b84d01-40a6-45cb-8f83-7e6889e19e7d.png)

------------------------------------------------------------------------------------------------------

![image](https://user-images.githubusercontent.com/80394522/170390128-27d82208-c37c-4a5c-bca2-10d6a237aa38.png)

------------------------------------------------------------------------------------------------------

![image](https://user-images.githubusercontent.com/80394522/169874258-60505df8-d7ff-4215-bed7-4dd563ea6868.png)


![image](https://user-images.githubusercontent.com/80394522/169874352-e65b0abc-2fd4-4fdb-bfb3-88cc754f9290.png)


![image](https://user-images.githubusercontent.com/80394522/169874442-68c0a11b-99ac-40c3-bea0-613ada85b679.png)


------------------------------------------------------------------------------------------------------
Pré Requisito: Necessita do Finish da branch feature/ABC_2_0_Relatorio

------------------------------------------------------------------------------------------------------

Criar a configuração abaixo na tabela de configurações

![image](https://user-images.githubusercontent.com/80394522/170153508-ccaa511f-a0a3-4fea-bc6e-6ff2ba77223b.png)

------------------------------------------------------------------------------------------------------

Criar no formulário acima o mesmo label de período que na tela do abc, fazer o tratamento também para não selecionar uma data posterior
Utilizar o mesmo código e não duplicar, se necessário mudar o código de lugar deixando ele público

![image](https://user-images.githubusercontent.com/80394522/169921621-b7ae6b6b-8481-4ec4-88d1-d6704c556e2f.png)

------------------------------------------------------------------------------------------------------

Ao clicar em visualizar caso a configuração criada esteja marcada instanciar o objeto GestaoComercial.Formularios.Indicadores.ResultadoDaLoja.CarregarVisaoGeral para obtermos os dados do relatório abc de mercadorias
Utilizar os parâmetros de data da tela
Utilizar o cmv da configuração (médio ou gerencial)
utilizar as transferências configuradas
os outros parametros são em branco , zero ou nulos

![image](https://user-images.githubusercontent.com/80394522/169874541-7b3ee2ba-4ccf-4a83-b854-88cac08f6387.png)


------------------------------------------------------------------------------------------------------

Aqui ver para quando tiver a configuração pegar do objeto instanciado , ver configuração da estrutura
