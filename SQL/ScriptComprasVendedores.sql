USE MASTER
RESTORE DATABASE GESTAO FROM DISK = 'C:\GESTAONOVACOMPRA2.BAK'

USE GESTAO

GO

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

GO

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

GO

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

GO

ALTER TABLE PedidoCompra ADD [CodVendedor] [int] NULL;
GO
ALTER TABLE PedidoCompra WITH NOCHECK ADD CONSTRAINT FK_PedidoCompra_FornecedorVendedores FOREIGN KEY ([CodVendedor]) REFERENCES DBO.FornecedorVendedores (CodVendedor);

GO

UPDATE PC SET PC.[CodVendedor] = FV.[CodVendedor] from PedidoCompra PC INNER JOIN FornecedorVendedores FV ON (PC.CodFornecedor = FV.CodFornecedor AND PC.Vendedor = FV.Nome)

GO

ALTER TABLE PedidoCompra DROP COLUMN Vendedor
GO
ALTER TABLE PedidoCompra DROP COLUMN VendedorFone
GO
ALTER TABLE PedidoCompra DROP COLUMN VendedorMail

GO

CREATE TABLE [dbo].[FornecedorVendedoresProdutos](
	[CodVendedor] [int] NOT NULL,
	[CodProduto] [float] NOT NULL)
GO
ALTER TABLE [dbo].[FornecedorVendedoresProdutos]  WITH NOCHECK ADD  CONSTRAINT [FK_FornecedorVendedoresProdutos_FornecedorVendedores] FOREIGN KEY([CodVendedor])  
REFERENCES [dbo].[FornecedorVendedores] ([CodVendedor]) ON DELETE CASCADE ON UPDATE CASCADE
GO
ALTER TABLE [dbo].[FornecedorVendedoresProdutos]  WITH NOCHECK ADD CONSTRAINT [FK_FornecedorVendedoresProdutos_Produtos] FOREIGN KEY([CodProduto])  
REFERENCES [dbo].[Produtos] ([codigo]) ON DELETE CASCADE ON UPDATE CASCADE


GO

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