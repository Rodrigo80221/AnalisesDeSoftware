sql ```

DECLARE @CodProduto INT = 48021; -- Código do Produto
DECLARE @CodLoja INT = 1; -- Código da Loja

SELECT 
    c.CUSTOGERENCIAL AS CustoGerencial,
	pl.valorProduto AS ValorDeVenda,    
	m.MargemPercentual_01 AS MargemAtual,
    CASE 
        WHEN m.PercentImpostosVenda_03 >= 100 THEN 0 
        ELSE c.CUSTOMARGEM / (1 - (m.PercentImpostosVenda_03 / 100)) 
    END AS PMZ        
FROM VW_ProdutoCustoLoja c
INNER JOIN ProdutoLojas pl 
    ON c.CODPRODUTO = pl.codProduto 
   AND c.CODLOJA = pl.codLoja
INNER JOIN Produtos p 
    ON p.codigo = c.CODPRODUTO
INNER JOIN Aliquotas a 
    ON a.Codigo = p.Aliquota
INNER JOIN AliquotasPisCofins apc 
    ON apc.Codigo = p.CodAliquotaPisCofins
LEFT JOIN Lojas l 
    ON l.codigo = pl.codLoja
INNER JOIN Clientes cli 
    ON cli.codigo = l.Cliente
INNER JOIN EstadoAliquotaFCP eaf 
    ON eaf.UF = cli.estado
LEFT JOIN AliquotaFCP afcp 
    ON eaf.CodAliquotaFCP = afcp.CodAliquota 
   AND p.FundoCombateaPobreza = 1
CROSS APPLY dbo.fn_retornar_margem(
    DBO.FN_ICMS_MONEY(a.Aliquota),  
    apc.PisSaida, 
    apc.CofinsSaida, 
    (pl.ValorNoPdv * (1 - DBO.FN_ICMS_MONEY(a.Aliquota) / 100)), 
    afcp.Aliquota, 
    pl.ValorNoPdv, 
    c.CUSTOGERENCIAL, 
    c.CUSTOMARGEM
) m
WHERE c.CODPRODUTO = @CodProduto
  AND pl.CODLOJA = @CodLoja;
  
```
