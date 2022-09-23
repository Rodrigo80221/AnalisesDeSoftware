``` SQL
------------------------ AQUI DEFINO AS VARIÁVEIS 

DECLARE @CodlLoja as Integer = 1
DECLARE @ValorVenda as float = 1000000
DECLARE @ValorCompra as float = 500000

------------------------  DELETE O PLANEJAMENTO CASO JÁ EXISTA
DELETE FROM PlanejamentoCompraVenda WHERE ANO = YEAR(GETDATE()) AND MES = MONTH(GETDATE()) AND CODLOJA = @CodlLoja
----------------------------------------------------

------------------------ CALCULO VARIÁVEIS AUXILIARES
DECLARE @CodPlanejamento as Integer = (SELECT ISNULL(MAX(CodPlanejamento),0) + 1 FROM PlanejamentoCompraVenda)
DECLARE @MES AS TINYINT = MONTH(GETDATE()) 
DECLARE @ANO AS INTEGER = YEAR(GETDATE())
DECLARE @PercentualCompraPlanejada as money = (@ValorCompra * 100.00 / @ValorVenda)


------------------------ CRIO TABELAS TEMPORÁRIAS CONFORME AS ORIGINAIS 
DECLARE @PlanejamentoCompraVenda AS TABLE (
CodPlanejamento	int,
CodLoja	int,
Ano	smallint,
Mes	tinyint,
ValorVendaPlanejada	money,
ValorCompraPlanejada money)


DECLARE @PlanejamentoCompraVendaGrupos AS TABLE (
CodPlanejamento	int,
CodGrupo nvarchar(MAX),
PercentualVendaPlanejada money,
PercentualMargemPlanejada money)


DECLARE @PlanejamentoCompraVendaDias AS TABLE (
CodPlanejamento	int,
CodGrupo	nvarchar(max),
Dia	tinyint,
PercentualVendaPlanejada	money,
PercentualCompraPlanejada	money
)


------------------------ INSIRO DADOS NAS TABELAS @PlanejamentoCompraVenda / @PlanejamentoCompraVendaGrupos

INSERT INTO @PlanejamentoCompraVenda 
SELECT @CodPlanejamento,@CodlLoja,@ANO,@MES,@ValorVenda,@ValorCompra

INSERT @PlanejamentoCompraVendaGrupos
SELECT @CodPlanejamento, G.Codigo,0, @PercentualCompraPlanejada
FROM GRUPOS G -- AQUI PRECISAMOS ADICIONAR O FILTRO DE GRUPOS



------------------------ ATUALIZAR A TABELA @PlanejamentoCompraVendaGrupos COM UM CÁLCULO DO PERCENTUAL / TOTAL DE GRUPOS (APENAS OS GRUPOS PAIS)

UPDATE G SET PercentualVendaPlanejada = CONVERT(MONEY,100) / (SELECT COUNT(*) FROM @PlanejamentoCompraVendaGrupos WHERE CodGrupo NOT LIKE '%.%') FROM @PlanejamentoCompraVendaGrupos G WHERE CodGrupo NOT LIKE '%.%' 

DECLARE @Codigo NVARCHAR(MAX)
DECLARE @QTDFilhos INT
DECLARE @PercentualVendaPlanejada	money
DECLARE @PercentualVendaPlanejadaFILHOS	money

DECLARE rsCursor CURSOR LOCAL FOR 
  
SELECT CodGrupo, PercentualVendaPlanejada from @PlanejamentoCompraVendaGrupos WHERE CodPlanejamento = @CodPlanejamento
    
OPEN rsCursor 
FETCH NEXT FROM rsCursor INTO @Codigo, @PercentualVendaPlanejada
WHILE @@FETCH_STATUS = 0 
BEGIN 
     
    SET @QTDFilhos = (SELECT COUNT(CODIGO) FROM GRUPOS WHERE LEFT(CODIGO,len(@Codigo)) = @Codigo and codigo <> @Codigo)

	IF @QTDFilhos > 0
	BEGIN
		SET @PercentualVendaPlanejadaFILHOS = CONVERT(MONEY, @PercentualVendaPlanejada / @QTDFilhos)

		update @PlanejamentoCompraVendaGrupos set PercentualVendaPlanejada = @PercentualVendaPlanejadaFILHOS WHERE LEFT(CodGrupo,len(@Codigo)) = @Codigo and CodGrupo <> @Codigo		 
	END 

    FETCH NEXT FROM rsCursor INTO @Codigo, @PercentualVendaPlanejada
END 
 
CLOSE rsCursor 
DEALLOCATE rsCursor


------------------------ INSIRO DADOS NA TABELA @PlanejamentoCompraVendaDias
 
DECLARE @TAB_DIAS AS TABLE (DIA INTEGER)
DECLARE @TotalDias TINYINT = (SELECT datediff(day, GETDATE(), dateadd(month, 1, GETDATE())))
DECLARE @Dia TINYINT = 1

WHILE (@Dia <= @TotalDias)
BEGIN
	INSERT INTO @TAB_DIAS SELECT @Dia
	SET @Dia = @Dia + 1
END 


------------------------ ATUALIZO A TABELA @PlanejamentoCompraVendaDias

INSERT INTO @PlanejamentoCompraVendaDias 
SELECT @CodPlanejamento,CodGrupo, dia,CONVERT(MONEY,100.00/@TotalDias),@PercentualCompraPlanejada 
FROM @PlanejamentoCompraVendaGrupos inner join @TAB_DIAS on 1=1
order by  CodGrupo , dia

 
INSERT INTO PlanejamentoCompraVenda
SELECT * FROM @PlanejamentoCompraVenda

INSERT INTO PlanejamentoCompraVendaGrupos
SELECT * FROM @PlanejamentoCompraVendaGrupos order by CodGrupo

INSERT INTO PlanejamentoCompraVendaDias
select * from @PlanejamentoCompraVendaDias






```
