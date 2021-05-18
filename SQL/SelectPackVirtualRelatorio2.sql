DECLARE @DataInicial as SmallDateTime 
DECLARE @DataFinal as SmallDateTime 

DECLARE @Lojas TABLE (CodLojas INT)  
DECLARE @ModeloPack TABLE (CodModelos INT) 


-- INICIO CARREGANDO FILTROS -------------------------------------------------------------------------

set @DataInicial = Convert(SmallDateTime, '20210101', 126) 
set @DataFinal = Convert(SmallDateTime, '20210131', 126) 
 
-- TRATAMENTO PARA FILTRO DE MODELOS
INSERT INTO @ModeloPack VALUES (1);
INSERT INTO @ModeloPack VALUES (2);
INSERT INTO @ModeloPack VALUES (3);
INSERT INTO @ModeloPack VALUES (4);
INSERT INTO @ModeloPack VALUES (5);
INSERT INTO @ModeloPack VALUES (6);
INSERT INTO @ModeloPack VALUES (7);
INSERT INTO @ModeloPack VALUES (8);
INSERT INTO @ModeloPack VALUES (9);
INSERT INTO @ModeloPack VALUES (10);
INSERT INTO @ModeloPack VALUES (11);
INSERT INTO @ModeloPack VALUES (12);
INSERT INTO @ModeloPack VALUES (13);
INSERT INTO @ModeloPack VALUES (14);

-- TRATAMENTO PARA FILTRO DE LOJAS
INSERT INTO @LOJAS VALUES (1);
INSERT INTO @LOJAS VALUES (2);

  
--TRATAMENTO PARA DATA FINAL      
---------------------------------------------------------------------------------------------------------------------------------      
SET @DataFinal = DATEADD(d,1,@DataFinal)    
---------------------------------------------------------------------------------------------------------------------------------      
 
declare @TabRel TABLE (  
CODPACK INT,    
CodProduto FLOAT,      
Unidade NVARCHAR(1),      
BARRAS NVARCHAR(30),      
DESCRICAO NVARCHAR(160),      
QTD MONEY,      
VALORUNIT MONEY,      
TOTAL MONEY,      
CUSTOGERENCIAL MONEY,      
MARGEMRS DECIMAL(38,10),      
CMV DECIMAL(38,10),      
MARGEMPORC DECIMAL(38,10),      
MARKUP DECIMAL(38,10)      
)      
       
DECLARE @TabRelTemp TABLE (      
CODPACK INT,
CODIGOPRODUTO FLOAT,      
DESCRICAO NVARCHAR(160),      
QTD MONEY,      
VALORUNIT MONEY,      
TOTALITEM MONEY,     
CUSTOGERENCIAL MONEY,      
CMV DECIMAL(38,10),     
MARGEMRS DECIMAL(38,10),      
MARGEM DECIMAL(38,10),      
MARKUP DECIMAL(38,10)     
)      
    
     
---------------------------------------------------------------------------------------------------------------------------------      
 
-- INSERE AS VENDAS NA TABELA TEMPORÁRIA      
---------------------------------------------------------------------------------------------------------------------------------      
INSERT INTO @TabRelTemp (CODPACK, CODIGOPRODUTO, DESCRICAO, QTD, VALORUNIT, TOTALITEM, CUSTOGERENCIAL, CMV, MARGEMRS)       
SELECT      
FIM.CodPack 
, FIM.CODIGOPRODUTO      
, P.DESCRICAO     
, SUM(FIM.QTD) QTD     
, CASE WHEN SUM(FIM.QTD) > 0 THEN SUM(FIM.TotalItem) / SUM(FIM.QTD) ELSE 0 END VALORUNIT  
, SUM(FIM.TotalItem) TOTALITEM     
, CASE WHEN SUM(FIM.QTD) > 0 THEN SUM(FIM.CMV) / SUM(FIM.QTD) ELSE 0 END CUSTOGERENCIAL  
, SUM(FIM.CMV) CMV     
, SUM(FIM.MARGEMRS) MARGEMRS       
FROM      
(     
SELECT      
VENDAS.CODPACK
, VENDAS.CodigoProduto       
, VENDAS.Qtd      
, VENDAS.Qtd * VENDAS.CustoGerencial CMV     
, VENDAS.TotalItem     
, VENDAS.valorunit AS VALORUNIT     
, VENDAS.CustoGerencial       
,ROUND(( VENDAS.TotalItem - ((VENDAS.BaseCalculoPis * VENDAS.AliquotaPis/100) + (VENDAS.BaseCalculoCofins * VENDAS.AliquotaCofins/100)  
+ (VENDAS.TotalItem  *                 
(  
CASE WHEN ISNUMERIC((SELECT ALIQUOTA FROM ALIQUOTAS A WHERE A.CODIGO = VENDAS.CodAliquotaIcms)) = 1 THEN   
CAST( REPLACE((SELECT ALIQUOTA FROM ALIQUOTAS A WHERE A.CODIGO = VENDAS.CodAliquotaIcms),',','.' ) AS MONEY ) / 100   
ELSE 0 END)   
)          
+(VENDAS.TotalItem * (CASE WHEN NOT VENDAS.CodAliquotaFCP IS NULL AND NOT (SELECT ALIQUOTA FROM ALIQUOTAS A WHERE A.CODIGO = VENDAS.CodAliquotaIcms) = 'FF' 
THEN(SELECT ALIQUOTA FROM AliquotaFCP A WHERE A.CodAliquota = VENDAS.CodAliquotaFCP) 
ELSE 0 END ) / 100))) 
- (ROUND(VENDAS.CustoMargem, 2)) * (VENDAS.qtd)  
 
-((((((VENDAS.TotalItem / VENDAS.Qtd) * (CASE WHEN VENDAS.pRedBCEfet = 0 THEN 100 ELSE VENDAS.pRedBCEfet END))/100)  * VENDAS.AliquotaIcmsStRetido / 100) - (VENDAS.BaseCalculoStRetido * VENDAS.AliquotaIcmsStRetido / 100)) + ((VENDAS.TotalItem / VENDAS.Qtd * VENDAS.AliquotaFcpStRetido / 100) - (VENDAS.BaseCalculoStRetido * VENDAS.AliquotaFcpStRetido / 100))) 
, 2) as MARGEMRS 
FROM     
		(     
			select  
			VP.CodPack     
			, vp.CodigoProduto       
			, vp.Qtd      
			, CAST(STR((CAST(STR(VP.qtd * VP.valorunit, 50, 2) AS MONEY) - VP.desconto) * (1 + (CAST(VP.DsDescontoAcrescimo AS FLOAT) / CASE WHEN CAST(VP.DsTotal AS FLOAT) = 0 THEN 1 ELSE CAST(VP.DsTotal AS FLOAT)END)),50,2) AS MONEY)   as TotalItem   
			, VP.valorunit     
			, VP.BaseCalculoCofins     
			, VP.BaseCalculoPis     
			, VP.AliquotaPis     
			, VP.AliquotaCofins     
			, VP.CodAliquotaIcms      
			, VP.CodAliquotaFCP      
			, VP.desconto     
			, VP.DsDescontoAcrescimo     
			, VP.DsTotal                         
			,ROUND(ISNULL (      
			(      
			SELECT TOP 1 CUSTOGERENCIAL      
			FROM CustoHistorico CH WITH(NOLOCK)  
			WHERE CH.CodLoja = VP.DSCODLOJA AND CH.CodProduto = VP.CODIGOPRODUTO AND CH.DtCusto <= VP.DSDatahora      
			AND CH.AlteraCusto = 1 
			ORDER BY DtCusto DESC, DtInsercao DESC       
			),       
			(      
			SELECT AVG(VW.custogerencial)      
			FROM VW_ProdutoCustoLoja VW      
			WHERE VW.codproduto = VP.CODIGOPRODUTO      
			AND VW.CodLoja = VP.DSCODLOJA      
			)      
			), 2) AS CustoGerencial      
 
			, ISNULL (      
			(      
			SELECT TOP 1 CUSTOMARGEM      
			FROM CustoHistorico CH WITH(NOLOCK)  
			WHERE CH.CodLoja = VP.DSCODLOJA AND CH.CodProduto = VP.CODIGOPRODUTO AND CH.DtCusto <= VP.DSDatahora      
			AND CH.AlteraCusto = 1 
			ORDER BY DtCusto DESC, DtInsercao DESC       
			),       
			(      
			SELECT AVG(VW.CUSTOMARGEM)      
			FROM VW_ProdutoCustoLoja VW      
			WHERE VW.codproduto = VP.CODIGOPRODUTO      
			AND VW.CodLoja = VP.DSCODLOJA      
			)      
			) AS CustoMargem                         
			, CASE  
							WHEN ( 
									( 
										SELECT ISNULL(UsaComplementoSt, 0) 
										FROM Empresa E 
										INNER JOIN ComplementoStVigencia C ON (LTRIM(C.UF) = LTRIM(E.UF)) 
										WHERE VP.DSDatahora >= DataInicio 
											AND VP.DSDatahora <= DataFim 
										) = 1 
									) 
								THEN ISNULL(( 
											SELECT TOP 1 ValorBaseStRetido 
											FROM CustoHistorico CH WITH (NOLOCK) 
											WHERE CH.CodLoja = VP.DSCODLOJA 
												AND CH.CodProduto = VP.CODIGOPRODUTO 
												AND CH.DtCusto <= VP.DSDatahora 
											ORDER BY DtCusto DESC 
												, DtInsercao DESC 
											), 0) 
							ELSE 0 
							END AS BaseCalculoStRetido 
			, CASE  
							WHEN ( 
									( 
										SELECT ISNULL(UsaComplementoSt, 0) 
										FROM Empresa E 
										INNER JOIN ComplementoStVigencia C ON (LTRIM(C.UF) = LTRIM(E.UF)) 
										WHERE VP.DSDatahora >= DataInicio 
											AND VP.DSDatahora <= DataFim 
										) = 1 
									) 
								THEN ISNULL(( 
											SELECT TOP 1 AliquotaIcmsStRetido 
											FROM CustoHistorico CH WITH (NOLOCK) 
											WHERE CH.CodLoja = VP.DSCODLOJA 
												AND CH.CodProduto = VP.CODIGOPRODUTO 
												AND CH.DtCusto <= VP.DSDatahora 
											ORDER BY DtCusto DESC 
												, DtInsercao DESC 
											), 0) 
							ELSE 0 
							END AS AliquotaIcmsStRetido 
						, CASE  
							WHEN ( 
									( 
										SELECT ISNULL(UsaComplementoSt, 0) 
										FROM Empresa E 
										INNER JOIN ComplementoStVigencia C ON (LTRIM(C.UF) = LTRIM(E.UF)) 
										WHERE VP.DSDatahora >= DataInicio 
											AND VP.DSDatahora <= DataFim 
										) = 1 
									) 
								THEN ISNULL(( 
											SELECT TOP 1 AliquotaFcpStRetido 
											FROM CustoHistorico CH WITH (NOLOCK) 
											WHERE CH.CodLoja = VP.DSCODLOJA 
												AND CH.CodProduto = VP.CODIGOPRODUTO 
												AND CH.DtCusto <= VP.DSDatahora 
											ORDER BY DtCusto DESC 
												, DtInsercao DESC 
											), 0) 
							ELSE 0 
							END AS AliquotaFcpStRetido 
			, VP.pRedBCEfet  
 
			from vendasprodutos vp WITH(NOLOCK)  

			where VP.DsDataHora >= @DataInicial      
			and VP.DsDataHora < @DataFinal             
			and vp.QtdCancelada = 0      
			and VP.CODPACK IS NOT NULL
			AND VP.Qtd > 0    
		    AND VP.DsCodLoja IN (SELECT CODLOJAS FROM @LOJAS) 
			AND VP.CODPACK IN (SELECT CODIGO FROM PACKVIRTUAL WHERE ModeloPack IN (SELECT CodModelos FROM @ModeloPack) )
			--AND VP.CodPack = xxx
			--And VP.CODPACK IN (SELECT CODIGO FROM PackVirtual WHERE CODENCARTE = 1)
		) AS VENDAS     


) AS FIM     

INNER JOIN PRODUTOS P WITH(NOLOCK) ON FIM.CODIGOPRODUTO = P.CODIGO      
GROUP BY       
CODPACK, CODIGOPRODUTO , P.DESCRICAO      
 
-- CALCULA OS CAMPOS DE MARGEM E MARKUP   
UPDATE @TabRelTemp SET      
MARGEM =  case TOTALITEM when 0 then 0 else ( MargemRS / TOTALITEM ) * 100 END  
, MARKUP =  ROUND(case CMV when 0 then 100 else (CAST(ROUND(VALORUNIT, 2) AS DECIMAL(10,4))  / ( CAST(ROUND(CUSTOGERENCIAL, 2)AS DECIMAL(10,4)) ) * 100 ) - 100 END, 2)   
 

-- INSERE O TOTALIZADOR DOS PACKS NA TABELA TEMPORÁRIA

DECLARE @TOTALREGISTROS AS BIGINT      
SELECT @TOTALREGISTROS = COUNT(*) FROM @TabRelTemp      
 
IF @TOTALREGISTROS > 0      
BEGIN      
	INSERT INTO @TabRel      
	SELECT      
	TAB.CODPACK
	, null CODIGOPRODUTO      
	, null Unidade      
	, null Barras        
	, TAB.DESCRICAO      
	, TAB.QTD      
	, TAB.VALORUNIT      
	, TAB.TOTAL      
	, TAB.CUSTOGERENCIAL      
	, TAB.MARGEMRS      
	, TAB.CMV      
	, TAB.MARGEMPORC      
	, TAB.MARKUP       
	FROM      
	(      
		SELECT  
		T2.CODPACK     
		, null CODIGOPRODUTO      
		, null Unidade      
		, null Barras      
		,(select DESCRICAO from PACKVIRTUAL T3 where T3.Codigo = T2.CODPACK) DESCRICAO      
		,(select SUM(QTD) from @TabRelTemp T1 where T1.CODPACK = T2.CODPACK) QTD      
		,SUM(T2.VALORUNIT) VALORUNIT      
		,SUM(T2.TOTALITEM) TOTAL      
		,SUM(T2.CUSTOGERENCIAL) CUSTOGERENCIAL      
		,SUM(T2.MARGEMRS) MARGEMRS       
		,SUM(T2.CMV) CMV      
		,0 MARGEMPORC      
		,0 MARKUP      
		FROM @TabRelTemp T2      
		GROUP BY T2.CODPACK
	) AS TAB      
 
END      

-- CALCULA OS CAMPOS DE MARGEM E MARKUP   
UPDATE @TabRel SET      
MARGEMPORC =  case TOTAL when 0 then 0 else ( MargemRS / TOTAL ) * 100 END    
, MARKUP =  case CMV when 0 then 100 else (  TOTAL  / ( CMV ) * 100 ) - 100 END  
     
-- INSERE TUDO NA TABELA FINAL
 
INSERT INTO @TabRel      
(CODPACK, CodProduto, Unidade, BARRAS, DESCRICAO, QTD, VALORUNIT, TOTAL, CUSTOGERENCIAL, MARGEMRS, CMV, MARGEMPORC, MARKUP)     
SELECT     
CODPACK,  
CODIGOPRODUTO,      
(SELECT U.CONFIG FROM PRODUTOS P INNER JOIN UNIDADES U ON P.UNIDADE = U.CODIGO WHERE P.CODIGO = T.CODIGOPRODUTO ) AS UNIDADE       
, ISNULL((SELECT TOP 1 PB.BARRAS FROM PRODUTO_BARRAS PB WHERE PB.CD_PRODUTO = T.CODIGOPRODUTO),CODIGOPRODUTO) AS BARRAS      
, T.DESCRICAO      
, T.QTD      
, T.VALORUNIT      
, T.TOTALITEM      
, T.CUSTOGERENCIAL      
, T.MARGEMRS      
, T.CMV      
, T.MARGEM     
, T.MARKUP       
FROM  @TabRelTemp T      
   
 
select * from @TabRel 
ORDER BY CODPACK DESC 

