``` SQL
 /*


 CREATE FUNCTION dbo.FN_RetornarGruposFilhos(@CodGrupoParametro AS NVARCHAR(40)) returns @CodGruposRetorno TABLE ( Codigo nvarchar(40)) AS
BEGIN

  DECLARE @CodGruposFiltro NVARCHAR(MAX)
  DECLARE @TotalNiveisParametro INTEGER = LEN(@CodGrupoParametro) - LEN(REPLACE(@CodGrupoParametro, '.', ''));
  DECLARE @TotalNiveisGrupoCursor INTEGER

  DECLARE rsCursor CURSOR LOCAL FOR 
  
  SELECT Codigo from grupos where LEFT(Codigo, LEN(@CodGrupoParametro)) = @CodGrupoParametro
    
  OPEN rsCursor 
  FETCH NEXT FROM rsCursor INTO @CodGruposFiltro
  WHILE @@FETCH_STATUS = 0 
  BEGIN 

	 set @TotalNiveisGrupoCursor = LEN(@CodGruposFiltro) - LEN(REPLACE(@CodGruposFiltro, '.', ''));

	 -- IDÉIA É BUSCAR OS GRUPOS QUE TEM APENAS "." (UM PONTO A MAIS NO CÓDIGO ESTRUTURAL) -- UM NÍVEL ABAIXO --
	 IF @TotalNiveisGrupoCursor = @TotalNiveisParametro + 1
	 BEGIN

		INSERT INTO @CodGruposRetorno
		SELECT @CodGruposFiltro
	
	 END
	 
     FETCH NEXT FROM rsCursor INTO @CodGruposFiltro
  END 
 
  CLOSE rsCursor 
  DEALLOCATE rsCursor
  

  RETURN;

END 


*/


 DECLARE @LOJA INT = 1
 DECLARE @DATA_FILTRO_INICIO NVARCHAR(20) = Convert(SmallDateTime, '20220701', 126)
 DECLARE @DATA_FILTRO_FIM NVARCHAR(20) = Convert(SmallDateTime, '20220801', 126)

 ------------------------ TABELA PARA RETORNO
 DECLARE @RESULTADO TABLE( 
 Dia	Int, 
 CodGrupo Nvarchar(20), 
 VendaImportada	FLOAT, 
 CompraImportada	FLOAT, 
 VendaPlanejada	FLOAT,
 CompraPlanejada	FLOAT 
 ) 
 -------------------------------------------------------------





  ------------------------ TABELA PARA INSERIRMOS OS DADOS DE VENDAS E COMPRAS R$
 DECLARE @NF_SAIDAS_REAIS TABLE(  
 DIA DATETIME,  
 TOTAL FLOAT
 , CodGrupo Nvarchar(40) 
 )  
  
 DECLARE @VENDAS_REAIS TABLE( 
 DIA DATETIME,  
 TOTAL FLOAT 
 , CodGrupo Nvarchar(40)
 , Custo Float
 ) 
  
 DECLARE @COMPRAS_REAIS TABLE( 
 DIA DATETIME,  
 TOTAL FLOAT  
 , CodGrupo Nvarchar(40) 
 ) 
 --------------------------------------------------------------------------------------
 



------------------------ CONSULTA QUE DEVERÁ SER FEITA NO BANCO GESTÃO RELATÓRIOS SIMILAR AO PROCEDIMENTO FrmPlanejamentoCompraVendaDia.ConsultarPorPlanejamentoGrupo

-- RETIREI O DAY() / RETIREI O FILTRO DE GRUPO 
-- NO INSERT INTO @VENDAS_REAIS ADICIONEI A COLUNA DE CUSTO GERENCIAL
-- RETIREI OS AGRUPAMENTOS DBO.FN_GRUPO_PAI(P.GRUPO)
  
INSERT INTO @COMPRAS_REAIS 
select DIA, SUM(COMPRA_REAL) as COMPRA_REAL, GRUPO FROM(
SELECT N.DT_ENTRADA AS DIA
, SUM(NP.QT_PRODUTO * NP.CUSTO_GERENCIAL) AS COMPRA_REAL 
, P.Grupo
FROM NF_ENTRADAS N 
INNER JOIN NF_ENTRADAS_PRODUTOS NP ON N.CD_NOTA = NP.CD_NOTA 
INNER JOIN PRODUTOS P ON NP.CD_PRODUTO = P.CODIGO 
INNER JOIN CFOP C ON NP.CFOP = C.CD_CFOP 
INNER JOIN Tipos_Operacao TPOE ON TPOE.CODIGO = N.CODTIPOOPERACAO  
WHERE (C.COMPRASAPURACAOIMPOSTOS = 1 OR TPOE.TIPOTRANSFERENCIA <> 0) 
AND N.DT_ENTRADA >= @DATA_FILTRO_INICIO
AND N.DT_ENTRADA < @DATA_FILTRO_FIM
AND N.CODLOJA = @LOJA 
AND N.NUMERO <> 0  
AND N.CODSTATUS = 2  
AND N.SITUACAO_NF <> 1  
-- AND DBO.FN_GRUPO_PAI(P.GRUPO) = @GRUPO 
GROUP BY N.DT_ENTRADA ,P.GRUPO
UNION 
SELECT N.DT_SAIDA AS DIA
, SUM(NP.TOTAL_PRODUTO) * -1 AS COMPRA_REAL  
,P.Grupo
FROM NF_SAIDAS N  
INNER JOIN NF_SAIDAS_PRODUTOS NP ON N.CD_NOTA = NP.CD_NOTA  
INNER JOIN PRODUTOS P ON NP.CD_PRODUTO = P.CODIGO  
INNER JOIN CFOP C ON NP.CFOP = C.CD_CFOP  
INNER JOIN Tipos_Operacao TPOE ON TPOE.CODIGO = N.CODTIPOOPERACAO  
WHERE (TPOE.TIPOTRANSFERENCIA <> 0) AND N.DT_SAIDA >= @DATA_FILTRO_INICIO
AND N.DT_SAIDA < @DATA_FILTRO_FIM
AND N.CODLOJA = @LOJA  
AND N.NUMERO <> 0   
AND N.CODSTATUS = 2   
AND N.SITUACAO_NF <> 1   
-- AND DBO.FN_GRUPO_PAI(P.GRUPO) = @GRUPO 
GROUP BY DT_SAIDA  ,P.Grupo
--DEDUZIR AS DEVOLUÇÕES DE COMPRAS 
UNION 
SELECT N.DT_SAIDA AS DIA  
, SUM(NP.TOTAL_PRODUTO) * -1 AS COMPRA_REAL  
,P.Grupo
FROM NF_SAIDAS N  
INNER JOIN NF_SAIDAS_PRODUTOS NP ON N.CD_NOTA = NP.CD_NOTA  
INNER JOIN PRODUTOS P ON NP.CD_PRODUTO = P.CODIGO  
INNER JOIN CFOP C ON NP.CFOP = C.CD_CFOP  
INNER JOIN Tipos_Operacao TPOE ON TPOE.CODIGO = N.CODTIPOOPERACAO  
WHERE C.DevolucaoOuRetorno = 1 AND N.DT_SAIDA >= @DATA_FILTRO_INICIO
AND N.DT_SAIDA < @DATA_FILTRO_FIM
AND N.CODLOJA = @LOJA  
AND N.NUMERO <> 0   
AND N.CODSTATUS = 2   
AND N.SITUACAO_NF <> 1   
--AND DBO.FN_GRUPO_PAI(P.GRUPO) = @GRUPO 
GROUP BY DT_SAIDA  ,P.Grupo
)T GROUP BY DIA  , GRUPO
  
  
INSERT INTO @NF_SAIDAS_REAIS  
SELECT DIA, SUM(SAIDA_REAL) AS SAIDA_REAL, GRUPO FROM (  
SELECT N.DT_EMISSAO AS DIA  
, SUM(NP.TOTAL_PRODUTO) AS SAIDA_REAL  
,P.Grupo
FROM NF_SAIDAS N  
INNER JOIN NF_SAIDAS_PRODUTOS NP ON N.CD_NOTA = NP.CD_NOTA  
INNER JOIN PRODUTOS P ON NP.CD_PRODUTO = P.CODIGO  
INNER JOIN CFOP C ON NP.CFOP = C.CD_CFOP  
WHERE C.CFOPDeVenda = 1  
AND N.DT_EMISSAO >= @DATA_FILTRO_INICIO
AND N.DT_EMISSAO < @DATA_FILTRO_FIM
AND N.CODLOJA = @LOJA  
AND N.NUMERO <> 0   
AND N.CODSTATUS = 2   
AND N.SITUACAO_NF <> 1   
-- AND DBO.FN_GRUPO_PAI(P.GRUPO) = @GRUPO 
GROUP BY  N.DT_EMISSAO  ,P.Grupo
--DEDUZIR AS DEVOLUÇÕES DE SAIDAS 
UNION ALL 
SELECT  NE.DT_Entrada AS DIA, SUM(isnull(ROUND(NEP.VALOR, 2) * NEP.QT_PRODUTO,0)) * -1 AS VENDA_REAL 
,P.Grupo
FROM NF_ENTRADA_produtor N inner join NF_SAIDAS V ON N.ChaveAcesso = V.ChaveAcesso 
INNER JOIN NF_ENTRADAS NE ON N.CodNotaEntrada = NE.CD_NOTA 
INNER JOIN NF_ENTRADAS_PRODUTOS NEP on NE.CD_NOTA = NEP.CD_NOTA 
INNER JOIN CFOP ON NE.CFOP = CFOP.CD_CFOP 
INNER JOIN Produtos P ON NEP.CD_PRODUTO = P.codigo 
WHERE CFOP.DevolucaoOuRetorno = 1 
AND NE.CODLOJA = @LOJA 
AND NE.NUMERO <> 0 
AND NE.CODSTATUS = 2 
AND NE.SITUACAO_NF <> 1 
AND NE.DT_Entrada >= @DATA_FILTRO_INICIO
AND NE.DT_Entrada < @DATA_FILTRO_FIM
-- AND DBO.FN_GRUPO_PAI(P.GRUPO) = @GRUPO 
GROUP BY NE.DT_Entrada,P.Grupo ) AS TAB 
GROUP BY DIA , GRUPO
  
  
INSERT INTO @VENDAS_REAIS 
SELECT DIA, SUM(VENDA_REAL) AS VENDA_REAL ,Grupo, SUM(CUSTO) FROM (  
SELECT VP.DsDataHora AS DIA 
, SUM(CAST(STR((CAST(STR(VP.QTD * VP.VALORUNIT, 50, 2) AS MONEY) - VP.DESCONTO) * (1 + (CAST(VP.DSDESCONTOACRESCIMO AS FLOAT) / CASE WHEN CAST(VP.DSTOTAL AS FLOAT) = 0 THEN 1 ELSE CAST(VP.DSTOTAL AS FLOAT)END)),50,2) AS MONEY)  ) AS VENDA_REAL 
,P.Grupo
       ,    ISNULL ( 
                        ( 
                            SELECT TOP 1 CUSTOGERENCIAL
                            FROM CustoHistorico CH WITH(NOLOCK) 
                            WHERE CH.CodLoja = @LOJA AND CH.CodProduto = VP.CodigoProduto AND CH.DtCusto <= VP.DsDataHora And CH.AlteraCusto = 1
                            ORDER BY DtCusto DESC, DtInsercao DESC  
                        ),  
                        ( 
                            SELECT AVG(VW.CUSTOGERENCIAL) 
                            FROM VW_ProdutoCustoLoja VW 
                            WHERE VW.codproduto = VP.CodigoProduto 
                            AND VW.CodLoja = @LOJA 
                        ) 
            ) AS Custo
FROM VENDASPRODUTOS VP 
INNER JOIN PRODUTOS P ON VP.CODIGOPRODUTO = P.CODIGO 
WHERE VP.DsDataHora >= @DATA_FILTRO_INICIO
AND VP.DsDataHora < @DATA_FILTRO_FIM
AND VP.DSCODLOJA = @LOJA 
AND VP.QTD > 0 
-- AND DBO.FN_GRUPO_PAI(P.GRUPO) = @GRUPO 
GROUP BY VP.DsDataHora , P.GRUPO , VP.CodigoProduto
--DEDUZIR AS DEVOLUÇÕES DE VENDAS 
UNION ALL 
SELECT  NE.DT_Entrada AS DIA, SUM(isnull(ROUND(NEP.VALOR, 2) * NEP.QT_PRODUTO,0)) * -1 AS VENDA_REAL 
,P.Grupo, SUM(NEP.CUSTO_GERENCIAL) * -1 as CUSTO ----------------------------------------------------------------- AQUI ALTERAR CUSTO
FROM NF_ENTRADA_produtor N inner join VENDAS V ON N.ChaveAcesso = V.ChaveAcesso 
INNER JOIN NF_ENTRADAS NE ON N.CodNotaEntrada = NE.CD_NOTA 
INNER JOIN NF_ENTRADAS_PRODUTOS NEP on NE.CD_NOTA = NEP.CD_NOTA 
INNER JOIN CFOP ON NE.CFOP = CFOP.CD_CFOP 
INNER JOIN Produtos P ON NEP.CD_PRODUTO = P.codigo 
WHERE CFOP.DevolucaoOuRetorno = 1 
AND NE.CODLOJA = @LOJA 
AND NE.NUMERO <> 0 
AND NE.CODSTATUS = 2 
AND NE.SITUACAO_NF <> 1 
AND NE.DT_Entrada >= @DATA_FILTRO_INICIO
AND NE.DT_Entrada < @DATA_FILTRO_FIM
-- AND DBO.FN_GRUPO_PAI(P.GRUPO) = @GRUPO 
GROUP BY NE.DT_Entrada, p.Grupo ) AS TAB 
GROUP BY DIA , GRUPO

 --------------------------------------------------------------------------------------







------------------------ TRECHO ADICIONADO PARA POPULAR AS TABELAS COM TODOS OS DIAS E GRUPOS PARA PODERMOS FAZER O AVG CORRETO POR DIA DA SEMANA 


 DECLARE @NF_SAIDAS_REAIS2 TABLE(  
 DIA DATETIME,  
 TOTAL FLOAT
 , CodGrupo Nvarchar(20) 
 )  
  
 DECLARE @VENDAS_REAIS2 TABLE( 
 DIA DATETIME,  
 TOTAL FLOAT 
 , CodGrupo Nvarchar(20) 
 ) 
  
 DECLARE @COMPRAS_REAIS2 TABLE( 
 DIA DATETIME, 
 TOTAL FLOAT  
 , CodGrupo Nvarchar(20) 
 ) 


DECLARE @TAB_DIAS AS TABLE (DIA datetime)
DECLARE @TotalDias TINYINT = (SELECT datediff(day, @DATA_FILTRO_INICIO, @DATA_FILTRO_FIM))
DECLARE @Dia TINYINT = 0

WHILE (@Dia < @TotalDias)
BEGIN
	INSERT INTO @TAB_DIAS SELECT DATEADD(day,@Dia,@DATA_FILTRO_INICIO)
	SET @Dia = @Dia + 1
END 

INSERT INTO @NF_SAIDAS_REAIS2
SELECT dia, 0 , G.Codigo
FROM GRUPOS G inner join @TAB_DIAS on 1=1 ------------------------ AQUI DEVEMOS RETIRAR OS GRUPOS QUE NÃO FARÃO PARTE DO RELATÓRIO (CONFIGURAÇÕES)
order by G.Codigo , dia

INSERT INTO @VENDAS_REAIS2 
SELECT dia, 0 , G.Codigo
FROM GRUPOS G inner join @TAB_DIAS on 1=1 ------------------------ AQUI DEVEMOS RETIRAR OS GRUPOS QUE NÃO FARÃO PARTE DO RELATÓRIO (CONFIGURAÇÕES)
order by G.Codigo , dia

INSERT INTO @COMPRAS_REAIS2 
SELECT dia, 0 , G.Codigo
FROM GRUPOS G inner join @TAB_DIAS on 1=1 ------------------------ AQUI DEVEMOS RETIRAR OS GRUPOS QUE NÃO FARÃO PARTE DO RELATÓRIO (CONFIGURAÇÕES)
order by G.Codigo , dia


UPDATE TAB2 SET TAB2.TOTAL = TAB1.TOTAL 
FROM (SELECT CAST(DIA AS DATE) DIA, SUM(TOTAL) TOTAL, CODGRUPO FROM  @NF_SAIDAS_REAIS GROUP BY CAST(DIA AS DATE), CODGRUPO) TAB1 INNER JOIN @NF_SAIDAS_REAIS2 TAB2 ON CAST(TAB1.DIA AS DATE) = CAST(TAB2.DIA AS DATE) AND TAB1.CodGrupo = TAB2.CodGrupo

UPDATE TAB2 SET TAB2.TOTAL = TAB1.TOTAL 
FROM (SELECT CAST(DIA AS DATE) DIA, SUM(TOTAL) TOTAL, CODGRUPO FROM  @VENDAS_REAIS GROUP BY CAST(DIA AS DATE), CODGRUPO) TAB1 INNER JOIN @VENDAS_REAIS2 TAB2 ON CAST(TAB1.DIA AS DATE) = CAST(TAB2.DIA AS DATE) AND TAB1.CodGrupo = TAB2.CodGrupo

UPDATE TAB2 SET TAB2.TOTAL = TAB1.TOTAL 
FROM (SELECT CAST(DIA AS DATE) DIA, SUM(CUSTO) TOTAL, CODGRUPO FROM  @VENDAS_REAIS GROUP BY CAST(DIA AS DATE), CODGRUPO) TAB1 INNER JOIN @COMPRAS_REAIS2 TAB2 ON CAST(TAB1.DIA AS DATE) = CAST(TAB2.DIA AS DATE) AND TAB1.CodGrupo = TAB2.CodGrupo
  
-----------------------------------------------------------------------------








------------------------ INSERIR NA TABELA RESULTADO OS DADOS VendaImportada, CompraImportada, VendaPlanejada, CompraPlanejada

DECLARE @VENDAIMPORTADA AS FLOAT = (SELECT SUM(TOTAL) FROM @NF_SAIDAS_REAIS) + (SELECT SUM(TOTAL) FROM @VENDAS_REAIS)
DECLARE @COMPRAIMPORTADA AS FLOAT = (SELECT SUM(TOTAL) FROM @COMPRAS_REAIS)

INSERT INTO @RESULTADO (Dia, CodGrupo, VendaImportada, CompraImportada, VendaPlanejada, CompraPlanejada) 
SELECT DAY(dia), G.Codigo,@VENDAIMPORTADA,@COMPRAIMPORTADA,0,0
FROM GRUPOS G inner join @TAB_DIAS on 1=1
order by G.Codigo , dia

  
UPDATE R SET CompraPlanejada = T.TOTAL from @RESULTADO R INNER JOIN
(
	SELECT DATEPART(dw,dia) DIA , AVG(C.TOTAL) TOTAL ,  C.CodGrupo 
	FROM @COMPRAS_REAIS2 C 
	GROUP BY DATEPART(dw,c.dia) , C.CodGrupo
) AS T ON DATEPART(dw,R.Dia) = T.DIA AND R.CodGrupo = T.CodGrupo


UPDATE R SET VendaPlanejada = T.TOTAL from @RESULTADO R INNER JOIN
(
	SELECT DATEPART(dw,dia) DIA , AVG(C.TOTAL) TOTAL , C.CodGrupo 
	FROM @VENDAS_REAIS2 C 
	GROUP BY DATEPART(dw,dia) , C.CodGrupo
) AS T ON DATEPART(dw,R.Dia) = T.DIA AND R.CodGrupo = T.CodGrupo

UPDATE R SET VendaPlanejada += T.TOTAL from @RESULTADO R INNER JOIN
(
	SELECT DATEPART(dw,dia) DIA , AVG(C.TOTAL) TOTAL , C.CodGrupo 
	FROM @NF_SAIDAS_REAIS2 C 
	GROUP BY DATEPART(dw,dia) , C.CodGrupo
) AS T ON DATEPART(dw,R.Dia) = T.DIA AND R.CodGrupo = T.CodGrupo



------------------------ AQUI DEVEMOS RETIRAR AS COMPRAS DE GRUPOS QUE NÃO TIVERAM VENDAS, POIS NESTE CASO NÃO TEMOS COMO TER UMA DIFERENÇA DE COMPRA E VENDA

UPDATE @RESULTADO SET CompraPlanejada = 0 WHERE CodGrupo IN
(SELECT CodGrupo FROM @RESULTADO GROUP BY CodGrupo HAVING SUM(VendaPlanejada) = 0)





------------------------ AQUI A TABELA PRONTA PARA MOSTRAR PARA O USUÁRIO

SELECT * FROM @RESULTADO

SELECT  
 (SELECT TOP 1 VendaImportada FROM @RESULTADO)[VENDA IMPORTADA] 
, (SELECT TOP 1 CompraImportada FROM @RESULTADO)[COMPRA IMPORTADA]
, (SELECT SUM(VendaPlanejada) FROM @RESULTADO) [VENDA PLANEJADA]
, (SELECT SUM(CompraPlanejada) FROM @RESULTADO) [COMPRA PLANEJADA]





---------------------------------------------------------------------------------------------------------------

------------------------ A PARTIR DAQUI INSERIR OS DADOS NAS TABELAS 

---------------------------------------------------------------------------------------------------------------

----------------------------------------------------
DELETE FROM PlanejamentoCompraVenda WHERE ANO = YEAR(GETDATE()) AND MES = MONTH(GETDATE()) AND CODLOJA = @LOJA
----------------------------------------------------


DECLARE @TabelaGruposFilhos TABLE (Codigo nvarchar(40))


DECLARE @Acrescimo as float = 0

DECLARE @ValorTotalVendaPlanejada as float = (SELECT SUM(VendaPlanejada) FROM @RESULTADO)
DECLARE @ValorTotalCompraPlanejada as float = (SELECT SUM(CompraPlanejada) FROM @RESULTADO)

DECLARE @CodPlanejamento as Integer = (SELECT ISNULL(MAX(CodPlanejamento),0) + 1 FROM PlanejamentoCompraVenda)

------------------------ ADICIONAR O ACRESCIMO NOS TOTALIZADORES
SET @ValorTotalVendaPlanejada = @ValorTotalVendaPlanejada + (@ValorTotalVendaPlanejada * (@Acrescimo/100))
SET @ValorTotalCompraPlanejada = @ValorTotalCompraPlanejada + (@ValorTotalCompraPlanejada * (@Acrescimo/100))

------------------------ ADICIONAR OS ACRESCIMOS NO RESULTADO
UPDATE R SET R.VendaPlanejada = R.VendaPlanejada + (R.VendaPlanejada * (@Acrescimo/100)) , R.CompraPlanejada = R.CompraPlanejada + (R.CompraPlanejada * (@Acrescimo/100)) FROM  @RESULTADO R 


INSERT INTO PlanejamentoCompraVenda 
SELECT @CodPlanejamento,@Loja,YEAR(GETDATE()),MONTH(GETDATE()),@ValorTotalVendaPlanejada,@ValorTotalCompraPlanejada


INSERT INTO PlanejamentoCompraVendaGrupos
SELECT @CodPlanejamento, G.Codigo,0, 0
FROM GRUPOS G  ------------------------ AQUI DEVEMOS RETIRAR OS GRUPOS QUE NÃO FARÃO PARTE DO RELATÓRIO (CONFIGURAÇÕES) OU BUSCAR OS GRUPOS DO @RESULTADO

------------------------ CALCULO PARA CALCULAR O PERCENTUAL DE VENDA DE CADA GRUPO
UPDATE P SET PercentualVendaPlanejada = (R.VendaPlanejada * 100) /  @ValorTotalVendaPlanejada FROM PlanejamentoCompraVendaGrupos P INNER JOIN 
(SELECT CODGRUPO, SUM(CompraPlanejada) CompraPlanejada , SUM(VendaPlanejada) VendaPlanejada FROM @RESULTADO GROUP BY CODGRUPO) AS R ON P.CodGrupo = R. CodGrupo AND P.CodPlanejamento = @CodPlanejamento

------------------------ CALCULO PARA CALCULAR O PERCENTUAL DE COMPRA DE CADA GRUPO
UPDATE P SET PercentualMargemPlanejada = CASE WHEN R.VendaPlanejada > 0 THEN (R.CompraPlanejada * 100) /  R.VendaPlanejada ELSE 0 END FROM PlanejamentoCompraVendaGrupos P INNER JOIN 
(SELECT CODGRUPO, SUM(CompraPlanejada) CompraPlanejada , SUM(VendaPlanejada) VendaPlanejada FROM @RESULTADO GROUP BY CODGRUPO) R ON P.CodGrupo = R. CodGrupo  AND P.CodPlanejamento = @CodPlanejamento


------------------------ ATUALIZAR O CAMPO PercentualVendaPlanejada E PercentualMargemPlanejada NOS PAIS, POI DEVERÁ SER O TOTAL DOS FILHOS


  DECLARE @CodGrupoCursor NVARCHAR(40)
  DECLARE @TotalSubGrupos INTEGER
  DECLARE @CompraPlanejadaSubGrupos FLOAT
  DECLARE @VendaPlanejadaSubGrupos FLOAT

  DECLARE rsCursor CURSOR LOCAL FOR 
  
  SELECT CodGrupo FROM PlanejamentoCompraVendaGrupos WHERE CodPlanejamento = @CodPlanejamento
    
  OPEN rsCursor 
  FETCH NEXT FROM rsCursor INTO @CodGrupoCursor
  WHILE @@FETCH_STATUS = 0 
  BEGIN 
	
	DELETE FROM @TabelaGruposFilhos

	INSERT INTO @TabelaGruposFilhos
	SELECT * FROM DBO.FN_RetornarGruposFilhos(@CodGrupoCursor)

	SET @TotalSubGrupos = (select ISNULL(COUNT(*),0) from @TabelaGruposFilhos)

	IF @TotalSubGrupos > 0
	BEGIN

		UPDATE P1 SET P1.PercentualVendaPlanejada = P2.PercentualVendaPlanejada FROM  PlanejamentoCompraVendaGrupos P1 
		INNER JOIN 
		(
		SELECT SUM(PercentualVendaPlanejada) PercentualVendaPlanejada , @CodGrupoCursor CodGrupo
		 FROM PlanejamentoCompraVendaGrupos P3 WHERE CodGrupo in (select codigo from @TabelaGruposFilhos) and CodPlanejamento = @CodPlanejamento
		 ) AS P2 ON P1.CodGrupo = P2.CodGrupo and CodPlanejamento = @CodPlanejamento


		 SET @CompraPlanejadaSubGrupos = ISNULL((SELECT SUM(CompraPlanejada) CompraPlanejada FROM @RESULTADO WHERE CodGrupo in (select codigo from @TabelaGruposFilhos)),0)
		 SET @VendaPlanejadaSubGrupos = ISNULL((SELECT SUM(VendaPlanejada) VendaPlanejada FROM @RESULTADO WHERE CodGrupo in (select codigo from @TabelaGruposFilhos)),0)


		 IF @VendaPlanejadaSubGrupos > 0 
		 BEGIN
			-- CALCULO PARA CALCULAR O PERCENTUAL DE COMPRA DE CADA GRUPO
			UPDATE P SET PercentualMargemPlanejada = CASE WHEN P.PercentualVendaPlanejada > 0 THEN (@CompraPlanejadaSubGrupos * 100) /  @VendaPlanejadaSubGrupos ELSE 0 END FROM PlanejamentoCompraVendaGrupos P 
			WHERE P.CodGrupo = @CodGrupoCursor AND P.CodPlanejamento = @CodPlanejamento 
		 END
		 		

	 END

     FETCH NEXT FROM rsCursor INTO @CodGrupoCursor
  END 
 
  CLOSE rsCursor 
  DEALLOCATE rsCursor

  

  
------------------------ POPULAR TABELA PlanejamentoCompraVendaDias


INSERT INTO PlanejamentoCompraVendaDias 
SELECT @CodPlanejamento, R.CodGrupo, R.DIA, 0, 0
FROM @RESULTADO R 


DECLARE @CodGrupo nvarchar(40) 
DECLARE @VendaPlanejada	FLOAT
DECLARE @CompraPlanejada	FLOAT

DECLARE rsCursor CURSOR LOCAL FOR 
  
SELECT CODGRUPO, SUM(CompraPlanejada) CompraPlanejada , SUM(VendaPlanejada) VendaPlanejada FROM @RESULTADO GROUP BY CODGRUPO HAVING SUM(VendaPlanejada) > 0
    
OPEN rsCursor 
FETCH NEXT FROM rsCursor INTO @CodGrupo, @CompraPlanejada, @VendaPlanejada 
WHILE @@FETCH_STATUS = 0 
BEGIN 

	------------------------ CALCULO PARA CALCULAR O PERCENTUAL DE VENDA DE CADA GRUPO E DIA
	UPDATE P SET PercentualVendaPlanejada = (R.VendaPlanejada * 100) /  @VendaPlanejada FROM PlanejamentoCompraVendaDias P
	INNER JOIN @RESULTADO R ON P.CodGrupo = R. CodGrupo AND P.Dia = R.Dia
	WHERE R.CodGrupo = @CodGrupo

	------------------------ CALCULO PARA CALCULAR O PERCENTUAL DE COMPRA DE CADA GRUPO E DIA
	UPDATE P SET PercentualCompraPlanejada = CASE WHEN R.VendaPlanejada > 0 THEN (R.CompraPlanejada * 100) /  R.VendaPlanejada ELSE 0 END FROM PlanejamentoCompraVendaDias P
	INNER JOIN @RESULTADO R ON P.CodGrupo = R. CodGrupo AND P.Dia = R.Dia
	WHERE R.CodGrupo = @CodGrupo 
	
    FETCH NEXT FROM rsCursor INTO @CodGrupo, @CompraPlanejada, @VendaPlanejada
END 
 
CLOSE rsCursor 
DEALLOCATE rsCursor





------------------------ AQUI TEMOS QUE ATUALIZAR OS DIAS DOS GRUPOS PAIS COM AS SOMAS DOS DIAS DOS FILHOS 


  DECLARE rsCursor CURSOR LOCAL FOR 
  
  SELECT CodGrupo FROM PlanejamentoCompraVendaGrupos WHERE CodPlanejamento = @CodPlanejamento
    
  OPEN rsCursor 
  FETCH NEXT FROM rsCursor INTO @CodGrupoCursor
  WHILE @@FETCH_STATUS = 0 
  BEGIN 
	
	DELETE FROM @TabelaGruposFilhos

	INSERT INTO @TabelaGruposFilhos
	SELECT * FROM DBO.FN_RetornarGruposFilhos(@CodGrupoCursor)

	SET @TotalSubGrupos = (select ISNULL(COUNT(*),0) from @TabelaGruposFilhos)

	IF @TotalSubGrupos > 0
	BEGIN


		 SET @CompraPlanejadaSubGrupos = ISNULL((SELECT SUM(CompraPlanejada) CompraPlanejada FROM @RESULTADO WHERE CodGrupo in (select codigo from @TabelaGruposFilhos)),0)
		 SET @VendaPlanejadaSubGrupos = ISNULL((SELECT SUM(VendaPlanejada) VendaPlanejada FROM @RESULTADO WHERE CodGrupo in (select codigo from @TabelaGruposFilhos)),0)


		 IF @VendaPlanejadaSubGrupos > 0 
		 BEGIN

			UPDATE P1 SET 
			P1.PercentualVendaPlanejada = (P2.VendaPlanejada * 100) /  @VendaPlanejadaSubGrupos
			FROM  PlanejamentoCompraVendaDias P1 
			INNER JOIN 
			(
				SELECT SUM(VendaPlanejada) VendaPlanejada , SUM(CompraPlanejada) CompraPlanejada,  @CodGrupoCursor CodGrupo, DIA
				FROM @RESULTADO P3 WHERE CodGrupo in (select codigo from @TabelaGruposFilhos) GROUP BY DIA
			) AS P2 ON P1.CodGrupo = @CodGrupoCursor and CodPlanejamento = @CodPlanejamento AND P1.Dia = P2.Dia 


			UPDATE P1 SET 
			P1.PercentualCompraPlanejada = CASE WHEN P2.VendaPlanejada > 0 THEN (P2.CompraPlanejada * 100) /  P2.VendaPlanejada ELSE 0 END
			FROM  PlanejamentoCompraVendaDias P1 
			INNER JOIN 
			(
				SELECT SUM(VendaPlanejada) VendaPlanejada , SUM(CompraPlanejada) CompraPlanejada,  @CodGrupoCursor CodGrupo, DIA
				FROM @RESULTADO P3 WHERE CodGrupo in (select codigo from @TabelaGruposFilhos) GROUP BY DIA
			) AS P2 ON P1.CodGrupo = @CodGrupoCursor and CodPlanejamento = @CodPlanejamento AND P1.Dia = P2.Dia 


		 END
		 		

	 END

     FETCH NEXT FROM rsCursor INTO @CodGrupoCursor
  END 
 
  CLOSE rsCursor 
  DEALLOCATE rsCursor
  










  











``` 
