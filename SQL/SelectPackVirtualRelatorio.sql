--select * from PackVirtual


--select pv.Descri/cao , pv.ModeloPack ,  from


/*
select vp.DsCodLoja, pv.ModeloPack,  pv.QtdRegra, pv.VlrRegra, count(pv.ModeloPack) [Total de Packs Vendidos]
from PackVirtual pv inner join
(
	select distinct  vp.DsCodLoja, vp.CodVenda, vp.CodPack, pv.ModeloPack, pv.QtdRegra, pv.VlrRegra 
	from VendasProdutos vp inner join PackVirtual pv on vp.CodPack = pv.Codigo
	where vp.CodPack is not null
			and vp.DsCodLoja = 1	
			and vp.DsDataHora >= '2020/01/01'
			and vp.DsDataHora <= '2021/01/01'

) vp on pv.codigo = vp.CodPack
group by vp.DsCodLoja, pv.ModeloPack, pv.QtdRegra, pv.VlrRegra
order by count(pv.ModeloPack) desc

*/




select vp.DsCodLoja,
'Compre a partir de ' + REPLACE(STR(pv.QtdRegra,15,0),' ','') + ' unidade(s) ' + 
'Ganhe ' + REPLACE(STR(pv.VlrRegra,15,0),' ','')  + '% de desconto por unidade(s).' [Modelo]
,count(pv.ModeloPack) [Total de Packs Vendidos]
from PackVirtual pv inner join
(
	select distinct  vp.DsCodLoja, vp.CodVenda, vp.CodPack, pv.ModeloPack, pv.QtdRegra, pv.VlrRegra 
	from VendasProdutos vp inner join PackVirtual pv on vp.CodPack = pv.Codigo
	where vp.CodPack is not null
			and vp.DsCodLoja = 1	
			and vp.DsDataHora >= '2020/01/01'
			and vp.DsDataHora <= '2022/01/01'
			--and Vp.CodigoProduto in (select codigo from produtos where descricao like '%coca%cola%')

) vp on pv.codigo = vp.CodPack
group by vp.DsCodLoja, pv.ModeloPack, pv.QtdRegra, pv.VlrRegra
order by count(pv.ModeloPack) desc

-- ver se é essas duas opções e o que mais
-- ver se será feito no vb ou c#



update modulos set config = 2 where codigo <> 265 and codigo <> 1036
select * from modulos where DESCRICAO like '%sistema%R%'

	select *
	from VendasProdutos vp
	where vp.CodPack is not null
			and vp.DsCodLoja = 1	
			and vp.DsDataHora >= '2020/01/01'
			and vp.DsDataHora <= '2021/01/01'
			and Vp.CodigoProduto in (select codigo from produtos where descricao like '%coca%cola%')



select * from [dbo].[AbcMercadoriasProcessandoTemp]
