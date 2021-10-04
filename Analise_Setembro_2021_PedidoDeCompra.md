# Épico: Controle de Vendedores nas Compras
Data de início do recurso: 16/09/2021  
## Problema principal a ser resolvido
* Na tela de pedido de compras o usuário deseja informar o vendedor e receber a listagem de produtos desse vendedor.
* Clientes envolvidos: Santana e Nova Compra
## Impactos
* Pedidos de Compra
* Notas de Entrada
* Relatório Análise Mensal de Vendas
* Controle de Cargas Pedidos
* Pedido de Compra Tishler
* Controle de Entradas
## Pré Requisitos
* Tela Pedido De Compras
    * Recurso para selecionar o vendedor;
    * Opção para visualizar apenas produtos desse vendedor;
    * Recurso para desvincular produtos do vendedor;
* Tela Notas de Entradas
    * Recurso para relacionar a nota ao seu pedido de compra;
    * Recurso para selecionar o vendedor, para análisar vendas desse vendedor no relatório;
* Relatório Análise Mensal de Vendas
    * Filtro de vendas apenas desse vendedor.
* Tela Controle de Entradas
    * Fixar Preço de Venda informado no pedido    

## Solução Final Analisada

### Atualização do Banco de Dados

* Criação da tabela FornecedorVendedores
    * Migração dos vendedores da tabela PedidoCompra para a tabela FornecedorVendedores
    * Nota: Manter o email e telefone do último pedido do vendedor
* Criação da tabela NF_Entradas_Pedidos
* Alteração da tabela PedidoCompras
    * Adicionar coluna CodVendedor
    * Atualizar coluna com o código dos vendedores
    * Remover da tabela os campos antigos Vendedor, Fone e Email
* Criação da tabela VendedoresProdutos
    * Nota: Popular nova tabela com dados dos últimos 3 pedidos do vendedor
* Criação dos módulos para os recursos do sistema S    
* Criação de Função para pesquisa de Fornecedor com performance
### Compatibilizar recursos do C#

* Criar a classe Telecode FornecedorVendedores
* Criar a classe Telecode FornecedorVendedoresProdutos
* Recriar a classe Telecode PedidoCompra
* Alterar formulário de busca FrmBuscaPedidosCompra do Controle de Cargas Pedidos
    * Buscar o campo de vendedor da nova tabela FornecedorVendedores
* Alterar o formulário FrmControleCargas (Controle de Cargas Pedidos)
    * Ao selecionar o pedido buscar o vendedor da nova tabela FornecedorVendedores
    * Ao adicionar o pedido na grade buscar os dados do vendedor da tabela FornecedorVendedores
    * Ao atualizar a tela buscar os dados do vendedor da tabela FornecedorVendedores
* Alterar o formulário de busca FrmPesquisaPedidosCompra do Controle de Cargas Pedidos
    * Buscar o campo de vendedor da nova tabela FornecedorVendedores
### Criar formulário C# FrmCadFornecedorVendedores (Cadastro de Vendedores dos Fornecedores)      

* Recurso para cadastrar, visualizar e alterar dados de vendedores
* Recurso para visualizar o mix de produtos do vendedor
* Recurso para impressão do mix de produtos do vendedor
* Chamada pela tela clássica e pelo sistemas S

### Alteração do formulário FrmPedidoCompra (Pedido de Compra)

* Adicionar combo para seleção do vendedores
* Deixar os campos de Telefone e Email apenas leitura
* Criar label link para chamar o Cadastro de Vendedores
* Atualizar novos campos nos relatórios
* Criar a caixa de seleção Mix do Vendedor
    * Filtrar produtos do vendedor
* Destacar na grade em itálico produtos que estão fora do Mix do Vendedor
* Salvar Mix do Vendedor
* Desvincular produtos do Mix do Vendedor
    * Nota: Ao desvincular produtos do fornecedor, desvincular também do Vendedor

### Alteração do formulário FrmPedidoCompraProdutos (Pedido de Compra Produtos)

* Compatibilizar para utilizar os novos campos do vendedor da tabela FornecedorVendedores

### Alteração do relatório C# Análise Mensal de Vendas FrmAnaliseMensalVendas

* Criação do Filtro por vendedor

### Criação do Formulário de Busca C# FrmBuscaPedidoCompra

* Herança do FrmPesquisa utilizando a classe FornecedorVendedores
### Alteração do formulário FrmNFEntradas (Notas de Entrada)

* Inserir campos para seleção do Pedido de Compra    
* Inserir label link para os Pedidos de Compra
* Inserir campo para seleção do Vendedor
* Inserir label link para o Cad de vendedores
* Realizar alterações ao gravar a nota
    * Associar produtos da nota ao Mix do Vendedor
    * Finalizar pedido de compra
    * Alterar Data Entrega do Pedido de Compra
        
### Alteração do formulário frmControleEntradas3 (Controle de Entradas)

* Destacar Preço de Venda de produtos com preço de venda definido no Pedido de Compra
    * Nota: O Preço de Venda será bloqueado para edição
* Colocar tooltip mostranto o motivo do destaque e o código do Pedido

### Protótipos das telas

__Pedido de Compra__

![image](https://user-images.githubusercontent.com/80394522/135516183-b0d21fed-ce06-43d4-b903-19c6c09ca9a5.png)

__Pedido de Compra__

![image](https://user-images.githubusercontent.com/80394522/135516470-96e1ab04-1e97-40e4-81a2-def3c020f04b.png)

__Pedido de Compra__

![image](https://user-images.githubusercontent.com/80394522/135518761-c17b14af-f980-4e31-898f-282157b22c7f.png)

__Análise Mensal de Vendas__

![image](https://user-images.githubusercontent.com/80394522/135518929-1857dc2b-4118-4762-88a9-8558b0c3d041.png)

__Notas de Entrada__

![image](https://user-images.githubusercontent.com/80394522/135519699-f8d5731c-f528-4047-9f5f-8ac14705730c.png)

__Cadastro de Vendedores__

![image](https://user-images.githubusercontent.com/80394522/134396374-20abcab9-8f8d-4f9e-a29f-24c71913320a.png)

__Cadastro de Vendedores__

![image](https://user-images.githubusercontent.com/80394522/134260261-62b36ea5-09e3-42ba-a7b6-82e041a1cff1.png)

Obs: Será add também na grade a coluna último custo.

# Tarefas

---
### Atualização do Banco de Dados
---

 ## Tarefa 1: Criar função no atualiza banco fCriarCadastroDeVendedoresNasCompras  PARTE 1

1. Criar função fCriarCadastroDeVendedoresNasCompras
1. Inserir o código formatado no banco de dados, utilizar vbNewLine
1. Adicionar os comandos abaixo

``` SQL
CREATE TABLE [dbo].[FornecedorVendedores](
	[CodVendedor] [int] NOT NULL IDENTITY,
	[CodFornecedor] [int] NOT NULL,
	[Nome] [nvarchar](50),
	[Fone] [nvarchar](14) NOT NULL DEFAULT (''),
	[Email] [nvarchar](64) NOT NULL DEFAULT (''),
	[MixPorVendedor] [bit] NOT NULL DEFAULT (0), CONSTRAINT [PK_CodVendedor] PRIMARY KEY CLUSTERED ([CodVendedor] ASC))
GO
ALTER TABLE [dbo].[FornecedorVendedores]  WITH NOCHECK ADD  CONSTRAINT [FK_Fornecedor_Vendedores] FOREIGN KEY([CodFornecedor])  
REFERENCES [dbo].[Fornecedores] ([codigo]) ON DELETE CASCADE ON UPDATE CASCADE
GO
ALTER TABLE [dbo].[FornecedorVendedores] ADD CONSTRAINT chk_NomeVendedor CHECK (LEN(Nome) >= 1)
```

``` SQL
CREATE TABLE [dbo].[NF_Entradas_Pedidos](
	[CD_NOTA] [float] NOT NULL,
	[CodPedido] [float] NULL,
	[CodVendedor] [int] NULL, CONSTRAINT [PK_NF_Entradas_Pedidos] PRIMARY KEY CLUSTERED ([CD_NOTA] ASC))
GO
ALTER TABLE [dbo].[NF_Entradas_Pedidos]  WITH NOCHECK ADD  CONSTRAINT [FK_NF_EntradasPedidos_NFEntradas] FOREIGN KEY([CD_NOTA])  
REFERENCES [dbo].[NF_ENTRADAS] ([CD_NOTA]) ON DELETE CASCADE ON UPDATE CASCADE
GO
ALTER TABLE [dbo].[NF_Entradas_Pedidos]  WITH NOCHECK ADD  CONSTRAINT [FK_NF_EntradasPedidos_PedidoCompra] FOREIGN KEY([CodPedido])  
REFERENCES [dbo].[PedidoCompra] ([Codigo]) ON DELETE CASCADE
GO
ALTER TABLE [dbo].[NF_Entradas_Pedidos]  WITH NOCHECK ADD  CONSTRAINT [FK_NF_EntradasPedidos_FornecedorVendedores] FOREIGN KEY([CodVendedor])  
REFERENCES [dbo].[FornecedorVendedores] ([CodVendedor]) ON DELETE CASCADE
GO
ALTER TABLE [dbo].[NF_Entradas_Pedidos] ADD CONSTRAINT chk_NFEntradasPedidos_DadosNulos CHECK (CodPedido is not null or CodVendedor is not null)

```

## Tarefa 2: Continuar função fCriarCadastroDeVendedoresNasCompras PARTE 2

1. Incrementar na função fCriarCadastroDeVendedoresNasCompras
1. Inserir o código formatado no banco de dados, utilizar vbNewLine
1. Ao final verificar se atendeu os requisitos abaixo:
- Os vendedores cadastrados nos pedidos devem ser migrados para a tabela FornecedorVendedores
- O e-mail e fone devem ser do último pedido do vendedor

``` SQL
INSERT INTO [FornecedorVendedores] ([CodFornecedor],[Nome],[Fone],[Email])
SELECT 
CodFornecedor
, Vendedor
, ISNULL((SELECT TOP 1 VendedorFone from PedidoCompra PC 
	      WHERE PC.CodFornecedor = PDC.CodFornecedor AND PC.Vendedor = PDC.Vendedor AND PC.VendedorFone <> '' 
		  ORDER BY DataPedido DESC),'')
, ISNULL((SELECT TOP 1 VendedorMail from PedidoCompra PC 
          WHERE PC.CodFornecedor = PDC.CodFornecedor AND PC.Vendedor = PDC.Vendedor AND PC.VendedorMail <> '' 
		  ORDER BY DataPedido DESC),'')
FROM PedidoCompra PDC
WHERE Vendedor IS NOT NULL AND Vendedor <> ''
AND NOT EXISTS (SELECT * FROM FornecedorVendedores FV WHERE FV.CodFornecedor <> PDC.CodFornecedor AND FV.Nome <> PDC.Vendedor) 
GROUP BY CodFornecedor, Vendedor
```

## Tarefa 3: Continuar função fCriarCadastroDeVendedoresNasCompras PARTE 3

1. Incrementar na função fCriarCadastroDeVendedoresNasCompras
1. Inserir o código formatado no banco de dados, utilizar vbNewLine
1. Ao final verificar se atendeu o requisito abaixo:
- Criar campo CodVendedor na tabela PedidoCompras 
- Atualizar PedidoCompras.CodVendedor buscando os dados populados na tabela "VendedoresFornecedor"
- Remover da tabela PedidoCompra os antigos campos Vendedor, Fone, E-mail.

``` SQL
ALTER TABLE PedidoCompra ADD [CodVendedor] [int] NULL;
GO
ALTER TABLE PedidoCompra WITH NOCHECK ADD CONSTRAINT FK_PedidoCompra_FornecedorVendedores FOREIGN KEY ([CodVendedor]) REFERENCES DBO.FornecedorVendedores (CodVendedor);
```

``` SQL
UPDATE PC SET PC.[CodVendedor] = FV.[CodVendedor] from PedidoCompra PC INNER JOIN FornecedorVendedores FV ON (PC.CodFornecedor = FV.CodFornecedor AND PC.Vendedor = FV.Nome)
```

``` SQL
ALTER TABLE PedidoCompra DROP COLUMN Vendedor
GO
ALTER TABLE PedidoCompra DROP COLUMN VendedorFone
GO
ALTER TABLE PedidoCompra DROP COLUMN VendedorMail
```


## Tarefa 4: Continuar função fCriarCadastroDeVendedoresNasCompras PARTE 4

1. Incrementar na função fCriarCadastroDeVendedoresNasCompras
1. Inserir o código formatado no banco de dados, utilizar vbNewLine
1. Ao final verificar se atendeu o requisito abaixo:
- Criar tabela VendedoresProdutos (codVendedor, CodProduto)
- Popular nova tabela com dados dos últimos 3 pedidos do vendedor


``` SQL
CREATE TABLE [dbo].[FornecedorVendedoresProdutos](
	[CodVendedor] [int] NOT NULL,
	[CodProduto] [float] NOT NULL)
GO
ALTER TABLE [dbo].[FornecedorVendedoresProdutos]  WITH NOCHECK ADD  CONSTRAINT [FK_FornecedorVendedoresProdutos_FornecedorVendedores] FOREIGN KEY([CodVendedor])  
REFERENCES [dbo].[FornecedorVendedores] ([CodVendedor]) ON DELETE CASCADE ON UPDATE CASCADE
GO
ALTER TABLE [dbo].[FornecedorVendedoresProdutos]  WITH NOCHECK ADD CONSTRAINT [FK_FornecedorVendedoresProdutos_Produtos] FOREIGN KEY([CodProduto])  
REFERENCES [dbo].[Produtos] ([codigo]) ON DELETE CASCADE ON UPDATE CASCADE
```

``` SQL
DECLARE @CodVendedor INTEGER
DECLARE NOVOS CURSOR LOCAL FOR

SELECT CodVendedor FROM PedidoCompra WHERE CodVendedor IS NOT NULL group by CodVendedor  

OPEN NOVOS;  
FETCH NEXT FROM NOVOS INTO @CodVendedor;  
WHILE @@FETCH_STATUS = 0  
BEGIN  
	INSERT INTO FornecedorVendedoresProdutos
	SELECT @CodVendedor, CodProduto FROM PedidoCompraProdutos PCP WHERE PCP.CodPedidoCompra in
	(SELECT TOP 3 PC.Codigo FROM PedidoCompra PC where PC.CodVendedor = @CodVendedor group by PC.CodVendedor, PC.DataPedido, PC.Codigo ORDER BY PC.CodVendedor, DataPedido desc)

    FETCH NEXT FROM NOVOS INTO @CodVendedor;  
END  
CLOSE NOVOS;  
DEALLOCATE NOVOS; 
```

``` SQL
CREATE FUNCTION DBO.FN_RetornarPesquisaFornecedor (@Descricao AS NVARCHAR(100)) RETURNS @Resultado TABLE (CodFornecedor INT) AS 

BEGIN

INSERT INTO @Resultado
SELECT DISTINCT CODIGO FROM
	(
		SELECT Codigo FROM Fornecedores F
		WHERE F.Fantasia LIKE '%' + @Descricao + '%'
		UNION
		SELECT Codigo FROM Fornecedores F
		WHERE F.Nome LIKE '%' + @Descricao + '%'
		UNION
		SELECT Codigo FROM Fornecedores F
		WHERE F.CNPJ LIKE '%' + @Descricao + '%'
		UNION
		SELECT Codigo FROM Fornecedores F
		WHERE DBO.FN_SO_NUMERO(F.CNPJ) LIKE '%' + @Descricao + '%'
		UNION
		SELECT Codigo FROM Fornecedores F
		WHERE LTRIM(STR(F.CODIGO,50)) = @Descricao
		UNION
		SELECT CodFornecedor [CODIGO] FROM FornecedorVendedores F
		WHERE F.Nome LIKE '%' + @Descricao + '%'
	) AS T

RETURN;

END
```

---
### Compatibilizar recursos do C#
---

## Tarefa 5: Atualizar/Criar classes C# com Telecode
1. Criar a classe FornecedorVendedores pelo aplicativo do Telecode no namespace Telecon.GestaoComercial.Biblioteca.Pedidos

1. Criar a classe FornecedorVendedoresProdutos pelo aplicativo do Telecode no namespace Telecon.GestaoComercial.Biblioteca.Pedidos

1. RECRIAR classe Telecon.GestaoComercial.Biblioteca.Pedidos => PedidoCompra.Telecode pelo aplicativo do Telecode
- Resultado esperado: 
    - Deverão ser removidas da classe as colunas Vendedor, VendedorFone e VendedorEmail;
    - Deverá ser add a nova coluna CodVendedor


## Tarefa 6: Compatibilizar classe PedidoCompra do c# para utilizar os novos campos na tela Controle de Cargas Pedidos

> Resumo: Ao criar uma nova carga e selecionarmos um pedido consultamos o vendedor. Como a estrutura do banco mudou teremos que alterar os locais de consulta, que são os locais abaixo:
>- Ao buscar um pedido pelo formulário de busca
>- Ao selecionar o pedido
>- Ao inserir o pedido na grade
>- Ao selecionar a carga na tela inicial

=> Tela a ser alterada: Pedidos => Controle de Cargas Pedidos

Após dar um rebuild teremos algumas linhas que ficarão incompatíveis, corrigir os itens abaixo:

1. Compatibilizar a classe PedidoDeCompra Telecon.GestaoComercial.Biblioteca.Pedidos.PedidoCompra
* Alterar os locais abaixo para utilizar pedidoCompra.CodVendedor em vez dos campos antigos seguindo os padrões dos procedimentos
    * ConverterDataReaderPesquisaPedidoCompra
    * ConverterDataReaderResumidoPesquisa
    * ConverterDataReaderGeral

## Tarefa 7: Compatibilizar tela FrmBuscaPedidosCompra do c# para utilizar os novos campos 

1. Compatibilizar o formulário de busca de busca GestaoComercial.Formularios.Outros.FrmBuscaPedidosCompra
* Alterar o precedimento FrmBuscaPedidosVenda_CarregaStringSql. Utilizar a função FornecedorVendedores.ConsultarChave passando o item.CodVendedor

2. Verificar o funcionamento da pesquisa pelo critério, se necessário programar para funcionar

## Tarefa 8: Compatibilizar tela FrmControleCargas do c# para utilizar os novos campos

1. Compatibilizar o controle de cargas GestaoComercial.Formularios.PedidosVendas.FrmControleCargas
* Alterar o procedimento TxtCodigoPedido_KeyDown. 
    * Para preencher o  txtVendedorPedido.Text utilizar a função FornecedorVendedores.ConsultarChave passando o pedido.CodVendedor
    * Abaixo dessa linha atribuir ao txtVendedorPedido.Tag o pedido.CodVendedor
    * Adicionar no botão o tratamento para a ampulheta do mouse

* Alterar o procedimento AtualizaGridPedidosConformeObjetoPrincipal. Utilizar a função FornecedorVendedores.ConsultarChave passando o pedidoCompra.CodVendedor
* Alterar o procedimento AdicionaPedidosCompraDeCargaExistente. Utilizar a função FornecedorVendedores.ConsultarChave passando o pedido.CodVendedor
* Alterar o procedimento BtnAddPedido_Click. Atribuir ao pedidoAdd.CodVendedor o conteúdo do campo  txtVendedorPedido.Tag
    * Adicionar no botão o tratamento para a ampulheta do mouse
    * No procedimento LimpaCamposTextPedidos limpar o txtVendedorPedido.Tag


## Tarefa 9: Compatibilizar a tela FrmPesquisaPedidosCompra do c# para utilizar os novos campos

1. Compatibilizar o FrmPesquisaPedidosCompra GestaoComercial.Formularios.PedidosVendas.FrmControleCargas   
* Alterar o procedimento CarregarPagina. Utilizar a função FornecedorVendedores.ConsultarChave passando o pedido.CodVendedor
* Alterar o procedimento BtCarga_Click. Precisaremos buscar o código do vendedor no pedido. Criar na classe PedidoCompra o procedimento RetornarCodVendedor que receba o código do pedido e retorne o código do vendedor
* Adicionar no botão o tratamento para a ampulheta do mouse

## Tarefa 10: Teste de integração do controle de cargas 
* Dar um rebuild no C# e verificar se todos os locais foram contemplados pela análise, corrigir eventuais problemas, caso necessário retornar para a análise.
* Testar os recursos da tela alterados, verificar se está buscando corretamente o vendedor do pedido. Avaliar os recursos de busca, adicionar na grade e pesquisa inicial.
* Verificar se o form de busca de pedidos funcionou corretamente 
* Se necessário assistir a video aula do controle de cargas


---
### Criar formulário C# FrmCadFornecedorVendedores (Cadastro de Vendedores dos Fornecedores)      
---


## Tarefa 11: Criar tela para manipular os dados dos Vendedores

![image](https://user-images.githubusercontent.com/80394522/134396374-20abcab9-8f8d-4f9e-a29f-24c71913320a.png)

![image](https://user-images.githubusercontent.com/80394522/134260261-62b36ea5-09e3-42ba-a7b6-82e041a1cff1.png)

1. Add formulário FrmFornecedorVendedores no projeto do gestão c#
Link: [link github](https://github.com/Rodrigo80221/AnalisesDeSoftware/tree/main/Classes/FrmFornecedorVendedores)
Add no caminho: GestaoComercial.Formularios.PedidosVendas

> Alterar o nome do Formulário para FrmCadFornecedorVendedores

1. Implentar formulário c# nas regras abaixo
* Implementar os eventos da barraCadastro. Verificar exemplos em outras telas
* Para atualização e persistência dos dados utilizar as classes FornecedorVendedores, FornecedorVendedoresProdutos e FornecedorProduto
* Para pesquisar os fornecedores utilizar o formulário GestaoComercial.Formularios.Produtos.FrmBuscaFornecedor
* Os campos Cód no Fornecedor e última compra deverão vir dos produtos dos fornecedores
* Adicionar também na grade a coluna último custo. Foi decidido após a criação do protótipo. 

> No novo form implementar as configurações padrões da Telecon.
1. Esc deve ferchar a tela
1. Enter deverá funcionar como tab
1. Ícone do gestão na janela
1. Não deve ter o botão de maximizar
1. Não deve permitir resize na tela
    Propriedade do form: BorderStyle: 1 - Fixed Single
1. O Fechar/Sair cancela a operação, caso alterando na barra de cadastros, mostrar mensagem de aviso.
1. Deve abrir centralizado. 
    Propriedade do form: StartUpPosition = 2 - CenterScreen
1. Deve estar correto quanto ao tab index ao finalizar a tela
1. F4 no campo Cod. Fornecedor deve abrir a busca de fornecedor
1. Implementar tratamentos da ampulheta do mouse no click dos botões de lupa, gravar, excluir, alterar e atualiza tela.

## Tarefa 12: Adicionar opção de imprimir grade no FrmFornecedorVendedores
1. Adicionar um botão ou ícone de imprimir abaixo da grade de produtos
1. Chamar o imprime grid do genéricos
> Pode usar como modelo o formuário GestaoComercial.Formularios.Indicadores.FrmAnalVendaConjunta

## Tarefa 13: Criar chamadas para a tela de Cadastro de Vendedores
> Criar módulo para Gerenciar o recurso FrmFornecedorVendedores nos Sistema S

Resultado esperado: A tela Cadastro de Vendedores deverá ser chamada pelo menu de fornecedores da tela clássica e pelo setores de Pedidos e Fornecedores do Sistemas S e R

1. Criar verifica banco para inserir módulo. Seguir padrão de outros módulos criados

- Utilizar os procedimentos:
`sInserirModulo`
`sInserirModuloSistemasRS`
- Executar update em operadores_modulos para ativar o módulo

Descrição das variáveis que serão utilizadas:

`sGrupo = PEDIDOS,FORNECEDORES`
`sDescrição = Cadastro de Vendedores dos Fornecedores`
`sPalavraChave = FrmFornecedorVendedores`

1. Tratar em Funcoes.sCarregaModulos, conforme padrão dos procedimentos

1. Chamar na tela clássica no menu de Fornecedores > Fornecedor Vendedores

1. Tratar no frmModulos.cmdRestaurar_Click conforme padrão do procedimento

---
### Alteração do formulário FrmPedidoCompra (Pedido de Compra)    
---

## Tarefa 14: Alterar FrmPedidoCompra - Inserir combobox para o vendedor

![image](https://user-images.githubusercontent.com/80394522/135516183-b0d21fed-ce06-43d4-b903-19c6c09ca9a5.png)

1. Trocar o txtVendedor por um cboVendedor
1. Colorir o BackColor do txtFone e txtEMail de amarelo "&H00C0FFFF&" e desabilitar os campos para edição
1. No Funcoes criar o procedimento `Public void sCarregarComboVendedores` que recebe o combo e o codigo do fornecedor (double) por parâmetro. Carregar o CodVendedor no .ItemData
* Neste procedimento carregar o combo cboVendedor com os vendedores disponíveis para o fornecedor do parâmetro de acordo com a tabela FornecedorVendedores. 
* Na última posição do combo carregar um campo vazio para caso o usuário queira deixar o vendedor em branco
1. Chamar esse procedimento no início do procedimento sCarregaVendedor passando o ldbCodigoFornecedor
* Atualizar a consulta do sCarregaVendedor
* No sCarregaVendedor após a consulta posicionar o combo no vendedor utilizado no último pedido desse fornecedor na tabela PedidoCompra oriundo da consulta, caso não haja ou seja null deixar o combo na posição vazio
> utilizar Funcoes.fProcuraItemData
1. No if do sCarregaVendedor retirar o trecho abaixo
``` vb
7       txtVendedor.Text = rsVendedor("Vendedor") & ""
8       txtFone.Text = rsVendedor("VendedorFone") & ""
9       txtEMail.Text = rsVendedor("VendedorMail") & ""
``` 
1. Alterar procedimento sLimparCampos
* Colocar na última posição do combo, que deverá ser a posição em branco
`cboVendedor.ListIndex = cboVendedor.ListCount - 1`

## Tarefa 15: Alterar FrmPedidoCompra - Tratar eventos do cboVendedor
1. Programar o evento KeyPress utilizando Funcoes.fBuscaCombo seguindo o padrão dos outros combos da tela
1. Programar o envento change do cboVendedor, ao alterar o item do combo deverá preencher os campos de email e telefone de acordo com a tabela FornecedorVendedores
1. Testar se com esse tratamento ao alterar o fornecedor já estará preenchendo esses campos, ajustar caso necessário

## Tarefa 16: Alterar FrmPedidoCompra - Criar Label Link Vendedores

1. Acima do combo de Vendedores inserir um label link conforme a imagem
1. Criar em Funcoes a função `fAlterarFornecedorVendedores`, criar um parâmetro byval double dbCodVendedor.
1. Nesta função criada programar de acordo com os passos abaixo
1. Se o dbCodVendedor = 0 então chamar a tela c# com o novo habilitado para cadastrar
1. Se o dbCodVendedor <> 0 então chamar a tela c# com o alterar habilitado para atualizar um vendedor
* a função `fAlterarFornecedorVendedores` deve retornar o CodVendedor do vendedor cadastrado ou atualizado
* seguir o padrão de funcionamento utilizado nos outros label links dessa tela

1. Programar no click do link uma chamada para a função `Funcoes.fAlterarFornecedorVendedores`, após passar por essa função chamar novamente o procedimento `Funcoes.sCarregarComboVendedores`
* Após esses passos posicionar o combo de vendedores no CodVendedor que retornou da função `fAlterarFornecedorVendedores` 

* Inserir tooltip no label link: Abrir o Cadastro de Vendedores dos Fornecedores

## Tarefa 17: Alterar FrmPedidoCompra - fGravaPedido

1. Alterar o procedimento fGravaPedido, retirar do sql os campos vendedor, vendedorFone e VendedorMail e adicionar o campo CodVendedor caso haja um vendedor selecionado, do contrário inserir null
1. Testar comportamento

## Tarefa 18: Alterar FrmPedidoCompra - sGerarRelatorioPorLote
1. Alterar o procedimento sGerarRelatorioPorLote
* Nos dois from add a tabela FornecedorVendedores com left join unindo pelo CodVendedor
* Nas duas consultas em vez de buscar por `PC.Vendedor, PC.VendedorMail`
buscar por `FornecedorVendedores.Vendedor, FornecedorVendedores.VendedorMail`
1. Testar comportamento

## Tarefa 19: Alterar FrmPedidoCompra - txtNumeroPedido_LostFocus
1. Alterar o procedimento txtNumeroPedido_LostFocus
* substituir o código abaixo pelo preencimento do campo cboVendedor
``` VB
35          txtVendedor.Text = rsPedidoCompra("Vendedor") & ""
36          txtFone.Text = rsPedidoCompra("VendedorFone") & ""
37          txtEMail.Text = rsPedidoCompra("VendedorMail") & ""
```

## Tarefa 20: Alterar FrmPedidoCompra - sImprimirRelatorio
1. Alterar o rpt do relatório `Pedido Compra.rpt"` 
1. No rpt adicionar a tabela FornecedorVendedores
1. Criar relacionamento LeftJoin entre a tabela FornecedorVendedores e PedidoCompra
1. Remover os campos antigos e add os novos campos da tabela FonecedorVendedores no relatório
1. Testar comportamento

## Tarefa 21: Alterar FrmPedidoCompra - Inserir e tratar o checkbox chkFiltroVendedor

![image](https://user-images.githubusercontent.com/80394522/135516470-96e1ab04-1e97-40e4-81a2-def3c020f04b.png)

1. Inserir o check box chkFiltroVendedor na tela conforme a imagem
* se necessário aumentar minimamente o frame de filtros para colocar os labels de observações de 2 em 2 abrindo espaço para novas configurações
* Colocar tooltip: Filtra apenas os produtos do vendedor selecionado. (Após o cadastro de um pedido com um vendedor selecionado os produtos serão vinculados a esse vendedor.)

1. Alterar o procedimento cboVendedor_Change
* caso o combo cboVendedor_Change tenha um vendedor selecionado deixar o componente chkFiltroVendedor.visible = true, caso esteja em branco deixar false
* Caso tenha vendedor e o campo FornecedorVendedor.MixPorVendedor seja true marcar o  checkbox chkFiltroVendedor, caso seja false desmarcar
* verificar se ao selecionar um fornecedor está trazendo corretamente o vendedor e marcando o checkbox de acordo com o banco

1. Alterar o procedimento sAjustaMenorQue1024x768
* Ajustar o chkFiltroVendedor nas resoluções assim como o chkFiltroFornecedor

1. Alterar o procedimento lblLimpar_Click
* Adicionar chkFiltroVendedor.Value = vbUnchecked

1. Implementar o chkFiltroVendedor_Click
* Add o código abaixo
``` vb
   If Me.ActiveControl.name = "chkFiltroVendedor" Then
       Call sListaProdutos
   End If
```

## Tarefa 22: Alterar FrmPedidoCompra - Tratar o checkbox chkFiltroVendedor no sListaProdutos

1. Remover o procedimento sListaProdutosbkp, não está sendo utilizado

1. Alterar o procedimento sListaProdutos
* Adicionar if no procedimento para caso o chkFiltroVendedor = vbchecked adicionar um filtro 
`sFiltro = sFiltro & " AND P.Codigo In (Select Distinct FP.CD_Produto From FornecedorVendedoresProdutos where CodVendedor =  "item data do cboVendedor")` 
* Fazer similar ao que já acontece com o chkFiltroFornecedor

1. No final da tarefa verificar se só isso já resolve para filtrar os produtos do vendedor. Verificar se ao desmarchar o checkbox do vendedor e alterar a quantidade de um produto fora do mix do vendedor, ao marcar o checkbox do vendedor de novo o produto alterado continuará na grade. Ajustar caso necessário ou verificar com a análise.


## Tarefa 23: Alterar FrmPedidoCompra - Tratar o checkbox chkFiltroVendedor no sInserirProdutosFornecedores

1. Criar o procedimento sSalvarDadosDoVendedor.
1. Chamar o procedimento sSalvarDadosDoVendedor dentro do fGravaPedido após a chamada do sAtualizarDadosGiroEstoque
1. Implemenentar o procedimento sSalvarDadosDoVendedor seguindo as regras abaixo:
* Atualizar o campo FornecedorVendedor.MixPorVendedor =  chkFiltroVendedor.Value , de acordo com o vendedor e o fornecedor selecionado
* O código do vendedor está no item do combo e do fornecedor em uma variável local ldbCodigoFornecedor
* Caso não haja vendedor selecionado sair da sub


## Tarefa 24: Alterar FrmPedidoCompra - Inserir produtos do pedido no MIX do vendedor

> Continuar implementando o procedimento `sSalvarDadosDoVendedor`
1. Após salvar o campo FornecedorVendedor.MixPorVendedor salvar os produtos do pedido na tabela FornecedorVendedoresProdutos de acordo com as regras abaixo:

1. Dar um insert em `FornecedorVendedoresProdutos` adicionando nesta tabela todos os produtos que estão no pedido com qtd > 0, que estão na tabela `Fornecedor_Produtos` e ainda não estão associados a esse vendedor
* Por motivos de performance utilizar um insert into FornecedorVendedoresProdutos where exists in PedidoCompraProdutos com Quantidade > 0 and exists in Fornecedor_Produtos and not exists in FornecedorVendedoresProdutos.
* Se não for selecionado um vendedor no combo dar um exit sub no início do procedimento


## Tarefa 25: Alterar FrmPedidoCompra - menu desvincular 

![image](https://user-images.githubusercontent.com/80394522/135518761-c17b14af-f980-4e31-898f-282157b22c7f.png)

> Ao clicar bom botão direito na grade temos a opção de desvincular o produto do fornecedor. Implementar o mesmo funcionamento com os vendedores

1. Criar o procedimento sDesvincularProdutoFornecedorVendedores
1. Criar os 3 menus para chamar esse procedimento. Os menus variam de acordo com o produto da grade, se tem quantidade, se é bonificação ou se é Trocas

> Caminho do menu: Tools => Menu Editor (Na função design do Form)

>Fazer similar ao fornecedor
sDesvincularProdutoFornecedor
mnuDesvincularFornecedor_Click
mnuDesvincularFornecedorBonf_Click
mnuDesvincularFornecedorTrocas_Click

1. O menu deverá aparecer apenas se tivermos um vendedor selecionado no combo
1. Ao desvincular um produto do fornecedor e tivermos um vendedor selecionado, devemos também desvincular o produto ao vendedor, chamando o procedimento `sDesvincularProdutoFornecedorVendedores`


## Tarefa 26: Alterar FrmPedidoCompra - Deixar em itálico na grade produtos fora do MIX do vendedor

>Nesta tarefa criaremos a funcionalidade de deixar os itens fora do mix do vendedor em itálico na grade, mas por motivos de performance não realizaremos mais uma consulta no banco honerando a tela, iremos utilizar um recordset desconectado

1. Nas declarações do FrmPedidoCompra criar a variável abaixo
``` vb
Private lrsConsultaFfornecedorVendedoresProdutos As ADODB.Recordset
```
1. Criar o procedimento `fCarregarRecordsetVendedoresProdutos` para carregar os dados da tabela `FornecedorVendedoresProdrodutos` na memória do client (recordset lrsConsultaFfornecedorVendedoresProdutos) de acordo com as regras abaixo:
* Realizar consulta na tabela `FornecedorVendedoresProdrodutos` de acordo com o vendedor e o fornecedor passador por parâmentro
* Criar o procedimento no mesmo padrão do procedimento `frmControleEntradas3.fCarregarRecordAtacado`

1. Chamar o procedimento `fCarregarRecordsetVendedoresProdutos` nos locais abaixo:
* Ao alterar um item no combo do vendedor
* No final do procedimento `sDesvincularProdutoFornecedorVendedores` 
* No final do procedimento `sSalvarDadosDoVendedor`

1. Criar o procedimento `sDestacadarProdutosForaDoMixDoVendedor` de acordo com as regras abaixo:
* Deve receber por parâmetro
    * a linha da grade
    * código do fornecedor
* Verificar se o produto da linha está contido no recordset lrsConsultaFfornecedorVendedoresProdutos.Filter
    * Caso não esteja deixar o código do produto, o código de barras e a descrição do produto em itálico na grade
* Chamar o procedimento no final do loop do `sListaProdutos`    

## Tarefa 27: Alterar FrmPedidoCompra - Criar Filtro por vendedor 

![image](https://user-images.githubusercontent.com/80394522/135731645-aa3445cb-b024-4097-9f7b-b513d046aae0.png)

1. Alterar Caption do label conforme a imagem

1. No procedimento sPesquisaPedido substituir código abaixo
``` VB
    If txtPesqFornecedor.Text <> "" Then
        sFiltro = "(F.Fantasia Like '" & Replace(txtPesqFornecedor.Text, " ", "%") & "%' "
        sFiltro = sFiltro & "Or F.Nome Like '" & Replace(txtPesqFornecedor.Text, " ", "%") & "%' "
        sFiltro = sFiltro & "Or F.CNPJ Like '" & Replace(txtPesqFornecedor.Text, " ", "%") & "%' "
        If IsNumeric(txtPesqFornecedor.Text) Then
            sFiltro = sFiltro & "Or F.Codigo = " & Val(txtPesqFornecedor.Text)
        End If
        sFiltro = sFiltro & ")"
    End If
```
POR 
``` VB
    If txtPesqFornecedor.Text <> "" Then
        sFiltro = "F.Codigo in (SELECT CodFornecedor FROM DBO.FN_RetornarPesquisaFornecedor('" & txtPesqFornecedor.Text & "'))"
    End If
```
## Tarefa 28: Alterar FrmPedidoCompra - Adicionar Coluna Vendedor na grade de Pesquisa
1. Adicionar a coluna Vendedor conforme o padrão da tela 
2. Carregar a coluna com os dados da tabela FornecedorVendedores.Nome 


## Tarefa 29: Teste de Integração do Pedido de Compra

1. Testar a solução completa verificando se atendeu os requisítos.
1. Corrigir erros encontrados, se necessário enviar a análise

* Testar combo para seleção do vendedores
* Testar label link para chamar o Cadastro de Vendedores
* Testar os 2 relatórios (pedido monoloja e multiloja)
* Testar a caixa de seleção Mix do Vendedor
    * Verificar o filtro de produtos do vendedor
* Testar o destaque na grade em itálico produtos que estão fora do Mix do Vendedor
* Testar o salvar Mix do Vendedor
* Testar o desvincular produtos do Mix do Vendedor
    * Nota: Ao desvincular produtos do fornecedor, desvincular também do Vendedor

---
### Alteração do formulário FrmPedidoCompraProdutos (Pedido de Compra Produtos)   
---
## Tarefa 30: Alterar pedido de compra do Tishler
1. Tratar o insert do procedimento FrmPedidoCompraProdutos.fInserirPedidoCabecalho. Substituir os campos Vendedor, Email e Fone por CodVendedor.
1. Excluir variáveis que não serão mais utilizadas.
1. Verificar o básico do funcionamento na tela.

---
### Alteração do relatório C# Análise Mensal de Vendas FrmAnaliseMensalVendas  
---

## Tarefa 31: Alterar relatório Análise Mensal de Vendas

![image](https://user-images.githubusercontent.com/80394522/135518929-1857dc2b-4118-4762-88a9-8558b0c3d041.png)

 1. Inserir os campos conforme a imagem adicionando o campo Vendedor, reorganizando e alinhando os outros componentes
    * O componente Text deve ser do tipo `Genéricos.Controles.Busca` assim como o do Fonecedor
1. Altear a classe GestaoComercial.Formularios.Indicadores.FrmAnaliseMensalVendas:
* Na `partial class FrmAnaliseMensalVendas` criar a propriedade `private Telecon.Genericos.Controles.Busca busVendedor;`
* No FrmAnaliseMensalVendas implementar o procedimento `busVendedor_CarregaDados()` para implementar o busVendedor. Seguir todo o Modelo da propriedade busFornecedor
    * usar busVendedor.Colunas = 2 (Código e Nome);
1. Criar novo parâmentro no procedimento `RelAnaliseMensalVendas.Consultar` e implementar a consulta da mesma forma que ocorre no fornecedor
1. Realizar testes finais e corrigir eventuais falhas encontradas

---
### Criação do Formulário de Busca C# FrmBuscaPedidoCompra
---

## Tarefa 32: Criar formulário de busca de pedido de compra FrmBuscaPedidoCompra
1. Criar diretório `GestaoComercial.Formularios.PedidosCompras`
1. Adicionar Formulário no namespace `GestaoComercial.Formularios.PedidosCompras`
1. Adicionar herança do formulário `FrmPesquisa`. Este formulário deverá seguir o padrão dos formulários de pesquisa da Telecon
1. Implementar o `AdicionaColunasList`, (acertar as larguras da colunas)
``` c#
        base.AdicionarColunaLista("Nro. Pedido", 50, typeof(string));
        base.AdicionarColunaLista("Dt. Entrega", 50, typeof(string));
        base.AdicionarColunaLista("Fornecedor", 150, typeof(string));
        base.AdicionarColunaLista("Loja", 100, typeof(string));
        base.AdicionarColunaLista("Comprador", 100, typeof(string));
        base.AdicionarColunaLista("Data Pedido", 50, typeof(string));
```
1. Implementar critérios
=> Campo + Valor no Combo
``` c#
        base.AdicionarCriterio(typeof(int), "PedidoCompra.Codigo"; "Nro. Pedido");
        base.AdicionarCriterio(typeof(string), "PedidoCompra.DataEntrega", "Dt. Entrega");
        base.AdicionarCriterio(typeof(string), "Fornecedor.Fantasia", "Fornecedor");
        base.AdicionarCriterio(typeof(string), "Loja.Nome", "Loja");
        base.AdicionarCriterio(typeof(string), "Operador.Nome", "Comprador");
        base.AdicionarCriterio(typeof(string), "PedidoCompra.Datapedido", "Data Pedido");
```
1. Implementar Ordenação
``` c#
        base.AdicionarOrdenacao(typeof(int), "Nro. Pedido", "Nro. Pedido");
        base.AdicionarOrdenacao(typeof(string), "Dt. Entrega", "Dt. Entrega");
        base.AdicionarOrdenacao(typeof(string), "Fornecedor", "Fornecedor");
        base.AdicionarOrdenacao(typeof(string), "Loja", "Loja");
        base.AdicionarOrdenacao(typeof(string), "Comprador", "Comprador");
        base.AdicionarOrdenacao(typeof(string), "Data Pedido", "Data Pedido");
```
1. Implementar o `CarregaStringSql`
* utilizar a classe `PedidoCompra`
* tratar o cursor do mouse durante a pesquisa (trocar seta por ampulheta)

---
### Alteração do formulário FrmNFEntradas (Notas de Entrada)
---

## Tarefa 33: Criar campos no NF_Entradas para inserir o Pedido e o Vendedor

![image](https://user-images.githubusercontent.com/80394522/135519699-f8d5731c-f528-4047-9f5f-8ac14705730c.png)

1. Adicionar os campos na tela conforme a imagem
1. O campo do código do pedido só deverá aceitar números (textMask = Integer)
1. Cuidar o TabIndex
1. Inserir um ícone de ajuda ao lado da busca do pedido, ao clicar mostrar a mensagem abaixo:
```
Ao selecionar um pedido de compra na nota de entrada os seguintes passos serão executados:
- Preenchimento o campo vendedor na nota de entrada caso ele tenha sido informado no pedido;
- Encerraramento o Pedido de Compra;
- Atualização da Data de Entrega do Pedido de Compra;
- Envio do Valor de Venda do produto para o Controle de Entradas caso ele tenha sido definido no pedido.
```
## Tarefa 34: Tornar funcional os componentes de vendedor nas NF Entradas

1. Chamar o procedimento `Funcoes.sCarregarComboVendedores `após selecionar um fornecedor no `fBuscaFornecedor` acima da linha abaixo:
``` vb
71      fBuscaFornecedor = True
```
1. No procedimento `sLimpaCamposFornecedor` passar o combo do vendedor para o último List Index que deve ser o em branco
1. Programar o Label Link do Vendedor assim como na tela do Pedido de Compra chamando a função criada no Funcoes    

## Tarefa 35: Tornar funcional os componentes de Pedidos nas NF Entradas

Pedido
1. Programar o botão de busca de pedido, utilizar a tela c# criada FrmBuscaPedidoCompra
1. Programar o Keypress do código do pedido
* Ao digitar o código do pedido e der enter verificar se o pedido existe. Se existe atribuir esse código ao txt.Text. Se não existe apresentar mensagem.
    * Na busca atualizar a ampulheta do mouse
* Se ao selecionar um pedido o fornecedor da nota estiver em branco dar a mensagem `Primeiro selecione o fornecedor!`, Colocar o foco no text do fornecedor
* Se o pedido existe e possui vendedor já posicionar o combo do vendedor, tratar para caso o vendedor do pedido não esteja no combo, pode acontecer caso o cnpj do pedido seja diferente do cnpj da nota, neste caso deixar o combo em branco.
* Se clicar F4 abrir a busca de pedidos c#, implentar a busca pela lupa
* Se o cliente utiliza o módulo de autorizaçao de pedidos (821) teremos que validar se o pedido foi autorizado, e somente se ele foi autorizado poderemos adicionar ele no text. 
Testar utilizando `If oUsuarioAcesso.Habilitado(821, giCodLojaPadrao) Then`. Caso não tenha sido autorizado mostrar a mensagem `O pedido não foi autorizado! Solicite autorização no pedido de compras.`. Neste caso deixar zero no text do pedido.

1. Programar o Label Link do Pedido
* Ao clicar no link se o código do pedido for <> 0 chamar `sAbrirForm frmPedidoCompra,dbCodPedido`


## Tarefa 36: Programar o Atualiza tela para atualizar os campos de pedido e vendedor

1. No procedimento `sConsultar` add no select o código abaixo
``` 
//Campos
NEP.CodVendedor
NEP.CodPedido
// Join
LEFT JOIN NF_Entradas_Pedidos NEP ON  N.CD_NOTA = NEP.CD_NOTA
```
1. No procedimento `AtualizaTela` após a linha ` 37  fBuscaFornecedor` posicionar o combobox do vendedor na posição correta recebida do [CodVendedor]
1. Após o CodVendedor carregar o text do pedido com o [CodPedido]. Carregar o .text e o .tag

## Tarefa 37: Gravar os novos campos das NF_Entradas

>Resumo: Ao inserirmos os campos de pedido e vendedor nas notas de entradas temos que atualizar a tabela NF_Entradas_Pedidos e fechar o pedido de compra. Teremos que tratar também o alterar.

1. Primeiro passo, criar no FrmNFEntradas o procedimento `sEncerrarPedido` `Public Sub sEncerrarPedido(ByVal iCodOperador As Integer, ByVal dbCodPedido As Double, ByVal dtDataEntrega As Datem, byval iCodVendedor as integer) `
* Add nele o código abaixo
``` vb
11          sSQL = "Update PedidoCompra "
12          sSQL = sSQL & "Set Finalizado = 1 , CodOperadorFinalizacao = " & iCodOperador & "," &  "DataEntrega =" & Funcoes.fTrataData(dtDataEntrega, True)
13          sSQL = sSQL & " Where Codigo = " & dbCodPedido
14          oConexao.Execute sSQL
```
Após esse código  dar um insert na tabela NF_Entradas_Pedidos com os dados de pedido, se vier zero colocar NULL
    * Nunca poderá ser inserido pedido null e vendedor null

1. Segundo passo, criar no FrmNFEntradas o procedimento `sReabrirPedido` `Public Sub sReabrirPedido(ByVal dbCodPedido As Double) `
* Add nele o código abaixo
    ``` vb
        sSQL = "Update PedidoCompra "
        sSQL = sSQL & "Set Finalizado = 0"
        sSQL = sSQL & " Where Codigo = " & dbCodPedido
        oConexao.Execute sSQL
    ```
Após esse código  dar um delete na tabela NF_Entradas_Pedidos com os dados de pedido


1. Terceiro Passo: Criar o procedimento `sAtualizarDadosDePedidoDeCompra` e chamar ele no `cadNotas_Gravar` no final após o `sExibirAvisoFunRural`.
 Implementação do `sAtualizarDadosDePedidoDeCompra`
* caso inserindo `If cadNotas.sAcao = "Inserindo Registro" Then` 
    * se o o text do cód. pedido <> 0 chamar o `sEncerrarPedido` com dtDataEntrega recebendo a data de entrada da nota 
    * Dar o insert do vendedor em NF_Entradas_Pedido ou colocar null

* Caso Alterando `If cadNotas.sAcao = "Alterando Registro" Then`   
    * Se o .txt.Text do cód. pedido = 0 e txt.Tag <> 0 então o usuário removeu o pedido. 
        * chamar o sReabrirPedido txt.tag para reabrir o pedido

    * Se o .txt.Text <> txt.Tag e <> 0 então o usuário alterou o pedido. 
        * Reabrir o pedido anterior sReabrirPedido txt.Tab
        * Encerrar o pedido atual sEncerrarPedido txt.Text
    * Dar o update do vendedor em NF_Entradas_Pedido ou colocar null

## Tarefa 38: Gravar os novos produtos das NF_Entradas no vendedor

1. Nas NFEntradas criar o procedimento `sinserirProdutosDoVendedor`
1. O procedimento deve ser chamado no final do `sAtualizarDadosDePedidoDeCompra`. (Executa no gravar e no alterar)
1. Dar um insert em `FornecedorVendedoresProdutos` adicionando nesta tabela todos os produtos que estão na nota e ainda não estão associados a esse vendedor
* Por motivos de performance utilizar um insert into FornecedorVendedoresProdutos where exists in NF_Entradas_produtos and not exists in FornecedorVendedoresProdutos. Tratar com distinct, lembrando que o produto pode vir duas vezes na nota

## Tarefa 39: Teste de integração das NF_Entradas 
1. Testar a solução completa verificando se atendeu os requisítos.
1. Corrigir erros encontrados, se necessário enviar a análise

* Testar os campos para seleção do Pedido de Compra    
* Testar label link para os Pedidos de Compra (novo e alterando)
* Testar campo para seleção do Vendedor
* Testar label link para o Cad de vendedores (novo e alterando)
* Testar CRUD destes campos, realizar alterações nesses campos
    * Verificar se fechou o pedido e atualizou a data da entrega
* Verificar se foram inseridos os produtos da nota na tabela FornecedorVendedoresProdutos
---
### Alteração do formulário frmControleEntradas3 (Controle de Entradas)
---

## Tarefa 40: Alterar Controle de entradas - Criar recordset desconectado de pedidos

Verifique nos passos abaixo o processo esperado antes de desenvolver as tarefas

```
Fluxo do resultado esperado das tarefas referentes ao controle de entradas
- Usuário cria um pedido para o produto x
- No pedido o usuário seta um novo valor de venda para o produto x
- Usuário recebe o pedido e lança a nota de entrada
- Usuário relaciona o pedido na nota de entrada
- Sistema finaliza o pedido
- Usuário abre o controle de entradas no intervalo de datas da nota
- O produto x deverá ser listado e na coluna preço venda já deverá estar preenchido o valor inserido no pedido. Deverá ficar na cor amarela como no pedido, e não deve ser permitida a alteração do preço de venda.
```

1. Nas declarações do frmControleEntradas3 declarar o recordset abaixo junto com as declareções dos outros recorsets da tela 
``` vb
Private lrsPedidoProdutos As ADODB.Recordset
```
1. Criar o procedimento `sCarregarRecordsetPedidoProdutos`. Neste procedimento carregar o recordset `lrsPedidoProdutos` da mesma forma que foi feito na função `fCarregarRecordAtacado`
* Criar os parametros dtDataInicio e dtDataFim no procedimento e add no select abaixo. 
``` sql
SELECT NF.CD_NOTA,NFP.CodPedido,PCP.CodProduto,PCP.NovoValorVenda FROM 
NF_Entradas NF 
INNER JOIN [NF_Entradas_Pedidos] NFP ON NF.CD_NOTA = NFP.CD_NOTA
INNER JOIN [PedidoCompra] PC ON NFP.CodPedido = PC.Codigo 
INNER JOIN [PedidoCompraProdutos] PCP ON PC.Codigo = PCP.CodPedidoCompra 
WHERE DATAS
```
1. Chamar o procedimento `sCarregarRecordsetPedidoProdutos` no `sVisualizarDados` antes da a linha
``` vb
11  sCarregaDados
```
* chamar apenas se `gbChamouDoFormCentralColeta = false and gbChamadoPelosPedidos = false`
> Se a tela FrmControleEntradas3 for chamada pela tela de pedido de compra não deverá ter impacto


## Tarefa 41: Alterar Controle de entradas - Setar valor do pedido na grade e colorir

1. Nas declarações do frmControleEntradas3 declarar a constante abaixo junto com as declareções das outras colorações
``` vb
Private Const ldbCorAmareloPedidos As Double = &H80FFFF
```
> Abaixo iremos criar funções e códigos para colocar o preço de venda definido no pedido na grade , alterar a cor e colocar um tooltip

1. Criar a função `fRetornarValorDoPedido` que recebe o código nota e o código produto por parâmentro
* O código da nota será uma string e poderá ter vários códigos de notas separados por ","
* A função deverá retornar o campo `PCP.NovoValorVenda` do último pedido
* A função deverá pesquisar no `lrsPedidoProdutos.Filter` assim como na função `fObterDescricaoPack` buscando performance
* Se não encontrar nada retornar zero

> Para constar: No controle de entradas se utilizar a configuração **Ocultar Variação de Custo** ele agrupa os produtos iguais e coloca os cod notas agrupadas na coluna de notas, neste caso teremos que considerar que pode ter várias notas

1. Alterar a função `fRetornarValorDoPedido` para receber o parâmetro `ByRef dbCodPedido as double` setar essa váriavel com o código do pedido selecionado
* Se não encontrar nada atribuir zero ao dbCodPedido

1.  No procedimento `sAplicarRegraColoracoes` após o trexo de código abaixo:
``` vb
If CCur(.TextMatrix(.row, btValorVenda)) <> CCur(.TextMatrix(.row, btValorVendaAntesAlteracao)) And .CellBackColor <> &H80FFFF Then
    .Col = btValorVenda
    .CellBackColor = ldbCorCinza
    .CellForeColor = ldbCorAzul
    .negrito = True
End If
```
APÓS ESSE if inserir o código abaixo corrigindo os parâmetros necessários
``` vb
    if gbChamouDoFormCentralColeta = false and gbChamadoPelosPedidos = false then
            cValorPedido = fRetornarValorDoPedido (btCdNotasProdAgrupados,btCodigoProduto)
            If fValorPedido <> 0 Then
                .CellBackColor = &H80FFFF
                .TextMatrix(.row, btValorVenda) = format(cValorPedido, "#0.00")
                grdDados_FimDaEdicao (cValorPedido)
                .CellForeColor = ldbCorAzul
                .negrito = Trueta 
            End If
    end if 
```
1.  No procedimento `sAplicarRegraColoracoes` após o trexo de código abaixo:
``` vb
If .TextMatrix(iLinha, btValorVenda) <> .TextMatrix(iLinha, btValorVendaAntesAlteracao) And Trim(.TextMatrix(iLinha, btValorVendaAntesAlteracao)) <> "" Then
    If Funcoes.fLeConfiguracao(goConexao, "USA_PRECO_OFFLINE", "TRUE") = "FALSE" Then
        sMensagem = "Preço de venda alterado.    Valor Anterior: R$" & .TextMatrix(iLinha, btValorVendaAntesAlteracao)
    Else
        sMensagem = "Preço de venda alterado, aplique o valor e envie carga para os PDVs.    Valor Anterior: R$" & .TextMatrix(iLinha, btValorVendaAntesAlteracao)
    End If
End If
```
APÓS ESSE if inserir o código abaixo corrigindo os parâmetros necessários
``` vb
    dbCodPedido = 0
    fRetornarValorDoPedido (btCdNotasProdAgrupados,btCodigoProduto,dbCodPedido)

    If dbCodPedido <> 0 Then
        sMensagem = "Preço de venda definido no Pedido Nº " & dbCodPedido & "." & " " & sMensagem
    End If
```


## Tarefa 42: Teste de integração do Controle de Entradas
1. Testar a solução completa verificando se atendeu os requisítos.
1. Corrigir erros encontrados, se necessário enviar a análise

Testar o fluxo abaixo

1. Usuário cria um pedido para o produto x
 1.No pedido o usuário seta um novo valor de venda para o produto x
 1.Usuário recebe o pedido e lança a nota de entrada
 1.Usuário relaciona o pedido na nota de entrada
 1.Sistema finaliza o pedido
 1.Usuário abre o controle de entradas no intervalo de datas da nota
 1.O produto x deverá ser listado e na coluna preço venda já deverá estar preenchido o valor inserido no pedido. Deverá ficar na cor amarela como no pedido, e não deve ser permitida a alteração do preço de venda.
 1.O preço de vende deverá ficar bloqueado para edição
 1.Testar alterando os filtros da tela, ordenação, bonificação e Ocultar variação de custo nas configurações

