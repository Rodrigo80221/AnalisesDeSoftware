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



## Criar tabela VendedoresFornecedor -------------- OK
(será que precisa o código do fornecedor??)
- Criar tabela VendedoresFornecedor (CodVendedor (PK Identity), CodFornecedor, Nome, telefone, e-mail, MixPorVendedor(bit))
## Popular tabela VendedoresFornecedor -------------- OK
(neste caso serão somente os produtos que tem pedido então?)
- Popular a nova tabela criada com dados históricos da tabela Pedidos (distinct de Pedidos.codFornecedor e Pedidos.Vendedor) O e-mail e fone deve ser do último pedido do vendedor
## Criar campo PedidoCompras.CodVendedor
- Criar campo CodVendedor (Null, Foregin key VendedoresFornecedor) na tabela PedidoCompras (OBS: pode ser null pois o usuário pode não informar vendedor caso desejar)
- Atualizar PedidoCompras.CodVendedor buscando os dados populados na tabela "VendedoresFornecedor"
- Remover da tabela PedidoCompra os antigos campos Vendedor, Fone, E-mail.


## Criar tabela VendedoresProdutos
- Criar tabela VendedoresProdutos (codVendedor, CodProduto)
## Popular tabela VendedoresProdutos
- Popular nova tabela com dados dos últimos 3 pedidos do vendedor
## Criar tela para manipular os Vendedores dos fornecedores
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

## Tarefa: Criar verifica banco para criar tabela

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
REFERENCES [dbo].[Fornecedores] ([codigo])
GO
ALTER TABLE [dbo].[FornecedorVendedores] ADD CONSTRAINT chk_NomeVendedor CHECK (LEN(Nome) > 1)
```

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