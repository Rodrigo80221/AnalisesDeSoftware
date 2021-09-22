# Pedido de Compra: Criar castro de Vendedores e Filtro de Produtos por Vendedor (Santana, Nova Compra)

Alguns fornecedores possuem mais que um vendedor atendendo os supermercados, um para cada "pasta", ou seja uma linha de produtos ou marcas. Isso causa dificuldade para o comprador que não consegue filtrar na tela do Compras apenas os produtos de determinado vendedor.

Atualmente sugerimos ao usuário (comprador) fazer filtros por Grupos ou criar lista de códigos, porém ambas não tem boa usabilidade na hora do pedido.

É comum o usuário solicitar um filtro por "Vendedor" ao fazer o pedido. 

Atualmente nossa estrutura para armazenar o vendedor é um campo livre para digitação no cadastro do pedido, isso dificulta um filtro seguro.

**Tarefas:**
**1) Criar um cadastro de Vendedores**
- Criar tabela VendedoresFornecedor (CodVendedor (PK Identity), CodFornecedor, Nome, telefone, e-mail, MixPorVendedor(bit))
- Popular a nova tabela criada com dados históricos da tabela Pedidos (distinct de Pedidos.codFornecedor e Pedidos.Vendedor) O e-mail e fone deve ser do último pedido do vendedor
- Criar campo CodVendedor (Null, Foregin key VendedoresFornecedor) na tabela PedidoCompras (OBS: pode ser null pois o usuário pode não informar vendedor caso desejar)
- Atualizar PedidoCompras.CodVendedor buscando os dados populados na tabela "VendedoresFornecedor"
- Remover da tabela PedidoCompra os antigos campos Vendedor, Fone, E-mail.
- Criar tela de Crud para o usuário cadastrar/alterar os dados da tabela VendedoresFornecedor (será chamada apenas pela tela do Pedido de Compra)
- Na tela do Pedido, criar label link "Vendedor" permitindo chamar tela de cadastro do vendedor para criar novo ao alterar um existente.
- Criar botão de procura (lupa) e popular campos na tela buscando dos novos campos nas tabelas PedidoCompras.CodVendedor e VendedoresFornecedor
- Alterar os rpts dos Pedidos de Compra para buscar os campos da tabela nova.
- Criar coluna no grid da busca do pedido e  filtro por nome do vendedor na tela de busca do pedido.

**2) Criar relação de VendedoresProdutos**
- Criar tabela VendedoresProdutos (codVendedor, CodProduto)
- Popular nova tabela com dados dos últimos 3 pedidos do vendedor
- Na tela do pedido de compras, nos filtros de produtos do pedido, criar novo filtro "Vendedor". Deve listar os vendedores disponíveis do fornecedor, e já trazer escolhido o vendedor selecionado no pedido como padrão.
- Criar checkbox para ativar/desabitar o filtro, sugerindo o vendedor do pedido (semelhante ao filtro atual de "Fornecedor"). Padrão desmarcado, salvar escolha em "VendedorFornecedores.MixPorVendedor)
- Caso vendedor selecionado e checkbox desmarcado, fazer left join no select que retorna os produtos ainda não adicionados ao pedido para indicar se o produto está no mix do Vendedor ou não, assinalando com fonte itálico nome do produto que estiver fora do mix do vendedor.
- Caso vendedor selecionado e checkbox marcado, fazer Inner join no select que retorna os produtos para retornar na pesquisa dos produtos ainda não adicionados ao pedido apenas os produtos dentro do mix do vendedor.
- Caso vendedor não selecionado não deve ser feito nenhum join de vendedores.
- Criar menu no clique direito do produto "Desvincular produto do Vendedor"
- Ao "desvincular produto do Fornecedor" deve também desvincular do mix de todos os Vendedores, caso esteja.
- Ao inserir o produto em "PedidoCompraProduto" caso um vendedor estiver selecionado e o produto fora do mix do vendedor, inserir também em VendedoresProdutos.



pernambuco 2 clientes

na nota fiscal em informações complementares
vem o vendedor
na tela de notas colocar também quem foi o vendedor daquela nota 

no relatório de vendas por período por vendedor
no pedido de compras tem um atalho para esse relatório, filtro por fornecedor e vendedor

impressão na telinha de cadastro

colocar uma chamada da telinha em fornecedores




## Criar tabela VendedoresFornecedor -------------- OK
(será que precisa o código do fornecedor??)
- Criar tabela VendedoresFornecedor (CodVendedor (PK Identity), CodFornecedor, Nome, telefone, e-mail, MixPorVendedor(bit))
## Popular tabela VendedoresFornecedor -------------- OK
(neste caso serão somente os produtos que tem pedido então?)
- Popular a nova tabela criada com dados históricos da tabela Pedidos (distinct de Pedidos.codFornecedor e Pedidos.Vendedor) O e-mail e fone deve ser do último pedido do vendedor
## Criar campo PedidoCompras.CodVendedor -------------- OK
- Criar campo CodVendedor (Null, Foregin key VendedoresFornecedor) na tabela PedidoCompras (OBS: pode ser null pois o usuário pode não informar vendedor caso desejar)
- Atualizar PedidoCompras.CodVendedor buscando os dados populados na tabela "VendedoresFornecedor"
- Remover da tabela PedidoCompra os antigos campos Vendedor, Fone, E-mail.


## Criar tabela VendedoresProdutos -------------- OK
- Criar tabela VendedoresProdutos (codVendedor, CodProduto)
## Popular tabela VendedoresProdutos -------------- OK
- Popular nova tabela com dados dos últimos 3 pedidos do vendedor
## Criar tela para manipular os Vendedores dos fornecedores -------------- OK
- Criar tela de Crud para o usuário cadastrar/alterar os dados da tabela VendedoresFornecedor (será chamada apenas pela tela do Pedido de Compra)

## Alterações na tela pedido de compra

* Label link "Vendedor"
    * Na tela do Pedido, criar label link "Vendedor" permitindo chamar tela de cadastro do vendedor para criar novo ou alterar um existente.
* Lupa para selecionar o vendedor (acho que não precisa)
    * Criar botão de procura (lupa) e popular campos na tela buscando dos novos campos nas tabelas PedidoCompras.CodVendedor e VendedoresFornecedor
* Criar filtro "Vendedor" no filtro de produtos com funcionamento semelhante ao do fornecedor
    * Na tela do pedido de compras, nos filtros de produtos do pedido, criar novo filtro "Vendedor". Deve listar os vendedores disponíveis do fornecedor, e  já trazer escolhido o vendedor selecionado no pedido como padrão.
* Criar checkbox para o vendedor, semelhante ao do fornecedor
    * Criar checkbox para ativar/desabitar o filtro, sugerindo o vendedor do pedido (semelhante ao filtro atual de "Fornecedor"). Padrão desmarcado, salvar escolha em "VendedorFornecedores.MixPorVendedor)
    * Caso vendedor selecionado e checkbox desmarcado, fazer left join no select que retorna os produtos ainda não adicionados ao pedido para indicar se o produto está no mix do Vendedor ou não, assinalando com fonte *** itálico *** nome do produto que estiver fora do mix do vendedor.
    * Caso vendedor selecionado e checkbox marcado, fazer Inner join no select que retorna os produtos para retornar na pesquisa dos produtos ainda não adicionados ao pedido apenas os produtos dentro do mix do vendedor.
    * Caso vendedor não selecionado não deve ser feito nenhum join de vendedores.
* Criar menu para desvincular produto ao vendedor - tratar tbm no menu do fornecedor
    * Criar menu no clique direito do produto "Desvincular produto do Vendedor"
    * Ao "desvincular produto do Fornecedor" deve também desvincular do mix de todos os Vendedores, caso esteja.
* Ao inserir o produto em pedido compra produto add tbm no mix do vendedor
    * Ao inserir o produto em "PedidoCompraProduto" caso um vendedor estiver selecionado e o produto fora do mix do vendedor, inserir também em VendedoresProdutos.
## Alterar RPTs
- Alterar os rpts dos Pedidos de Compra para buscar os campos da tabela nova.
## Criar coluna na busca de pedidos de compra
- Criar coluna no grid da busca do pedido e  filtro por nome do vendedor na tela de busca do pedido.








# ALTERAÇÕES NO BANCO DE DADOS  

## Tarefa 1: Criar função no atualiza banco fCriarCadastroDeVendedoresNasCompras  PARTE 1

1. Criar função fCriarCadastroDeVendedoresNasCompras
1. Criar tabela abaixo
1. Testar com Funcoes.fExisteObjeto se a tabela FornecedorVendedores já estiver no banco de dados dar um `Exit Sub`

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
ALTER TABLE [dbo].[FornecedorVendedores] ADD CONSTRAINT chk_NomeVendedor CHECK (LEN(Nome) > 1)
```



## Tarefa 2: Continuar função fCriarCadastroDeVendedoresNasCompras PARTE 2

1. Incrementar na função fCriarCadastroDeVendedoresNasCompras
1. Ao final verificar se atendeu o requisito abaixo:
- Os vendedores cadastrados nos pedidos devem ser migrados para a tabela FornecedorVendedores
- O e-mail e fone deve ser do último pedido do vendedor

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
1. Ao final verificar se atendeu o requisito abaixo:
- Criar tabela VendedoresProdutos (codVendedor, CodProduto)
- Popular nova tabela com dados dos últimos 3 pedidos do vendedor

``` SQL
CREATE TABLE [dbo].[FornecedorVendedoresProdutos](
	[CodVendedor] [int] NOT NULL,
	[CodProdut] [float] NOT NULL)
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


## Tarefa 6: Atualizar/Criar classes C# com Telecode
1. Criar a classe FornecedorVendedores pelo aplicativo do Telecode no namespace Telecon.GestaoComercial.Biblioteca.Pedidos

1. Criar a classe FornecedorVendedoresProdutos pelo aplicativo do Telecode no namespace Telecon.GestaoComercial.Biblioteca.Pedidos

1. Recriar classe Telecon.GestaoComercial.Biblioteca.Pedidos => PedidoCompra.Telecode pelo aplicativo do Telecode
- Resultado esperado: 
    - Deverão ser removidas da classe as colunas Vendedor, VendedorFone e VendedorEmail;
    - Deverá ser add a nova coluna CodVendedor


## Tarefa 7: Compatibilizar classe PedidoCompra do c# para utilizar os novos campos 

> Tela a ser alterada: Pedidos => Controle de Cargas Pedidos

> Resumo: Ao criar uma nova carga e selecionarmos um pedido, devemos consultar o vendedor na tabela >FornecedorVendedores nos locais:
>- Ao buscar um pedido pelo formulário de busca
>- Ao selecionar o pedido
>- Ao inserir o pedido na grade
>- Ao selecionar a carga na tela inicial

> Abaixo após dar um rebuild teremos algumas linhas que ficarão incompatíveis, corrigir.

1. Compatibilizar a classe PedidoDeCompra Telecon.GestaoComercial.Biblioteca.Pedidos.PedidoCompra
* Alterar os locais abaixo para utilizar pedidoCompra.CodVendedor seguindo os padrões dos procedimentos
    * ConverterDataReaderPesquisaPedidoCompra
    * ConverterDataReaderResumidoPesquisa
    * ConverterDataReaderGeral

## Tarefa 8: Compatibilizar tela FrmBuscaPedidosCompra do c# para utilizar os novos campos 

1. Compatibilizar o formulário de busca de busca GestaoComercial.Formularios.Outros.FrmBuscaPedidosCompra
* Alterar o precedimento FrmBuscaPedidosVenda_CarregaStringSql. Utilizar a função FornecedorVendedores.ConsultarChave passando o item.CodVendedor

2. Verificar o funcionamento da pesquisa pelo critério, se necessário programar para funcionar

## Tarefa 9: Compatibilizar tela FrmControleCargas do c# para utilizar os novos campos

1. Compatibilizar o controle de cargas GestaoComercial.Formularios.PedidosVendas.FrmControleCargas
* Alterar o procedimento TxtCodigoPedido_KeyDown. 
    * Para preencher o  txtVendedorPedido.Text utilizar a função FornecedorVendedores.ConsultarChave passando o pedido.CodVendedor
    * Abaixo dessa linha atribuir ao txtVendedorPedido.Tag o pedido.CodVendedor

* Alterar o procedimento AtualizaGridPedidosConformeObjetoPrincipal. Utilizar a função FornecedorVendedores.ConsultarChave passando o pedidoCompra.CodVendedor
* Alterar o procedimento AdicionaPedidosCompraDeCargaExistente. Utilizar a função FornecedorVendedores.ConsultarChave passando o pedido.CodVendedor
* Alterar o procedimento BtnAddPedido_Click. Atribuir ao pedidoAdd.CodVendedor o conteúdo do campo  txtVendedorPedido.Tag
    * No procedimento LimpaCamposTextPedidos limpar o txtVendedorPedido.Tag


## Tarefa 10: Compatibilizar a tela FrmPesquisaPedidosCompra do c# para utilizar os novos campos

1. Compatibilizar o FrmPesquisaPedidosCompra GestaoComercial.Formularios.PedidosVendas.FrmControleCargas   
* Alterar o procedimento CarregarPagina. Utilizar a função FornecedorVendedores.ConsultarChave passando o pedido.CodVendedor
* Alterar o procedimento BtCarga_Click. Precisaremos buscar o código do vendedor no pedido. Criar na classe PedidoCompra o procedimento RetornarCodVendedor que receba o código do pedido e retorne o código do vendedor


## Tarefa 11: Teste de integração do controle de cargas 
* Dar um rebuild no C# e verificar se todos os locais foram contemplados pela análise, corrigir eventuais problemas, caso necessário retornar para a análise.
* Testar os recursos da tela alterados, verificar se está buscando corretamente o vendedor do pedido. Avaliar os recursos de busca, adicionar na grade e pesquisa inicial.
* Verificar se o form de busca de pedidos funcionou corretamente 


## Tarefa 12: Criar tela para manipular os dados dos Vendedores

![image](https://user-images.githubusercontent.com/80394522/134396374-20abcab9-8f8d-4f9e-a29f-24c71913320a.png)

![image](https://user-images.githubusercontent.com/80394522/134260261-62b36ea5-09e3-42ba-a7b6-82e041a1cff1.png)


1. Add formulário FrmFornecedorVendedores no progeto do gestão c#
Link: [link github](https://github.com/Rodrigo80221/AnalisesDeSoftware/tree/main/Classes/FrmFornecedorVendedores)
Add no caminho: GestaoComercial.Formularios.PedidosVendas

1. Implentar formulário c#
* Implementar os eventos da barraCadastro. Verificar exemplos em outras telas
* Para atualização e persistência dos dados utilizar as classes FornecedorVendedores, FornecedorVendedoresProdutos e FornecedorProduto
* Para pesquisar os fornecedores utilizar o formulário GestaoComercial.Formularios.Produtos.FrmBuscaFornecedor

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


## Tarefa 12: Criar chamadas para o formulário FrmFornecedorVendedores
COMENTÁRIOESPETO