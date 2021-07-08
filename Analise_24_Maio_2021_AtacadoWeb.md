# Alterações Controle de entradas

## Maximizar

1. Maximizar a tela do controle de entradas para podermos visualizar mais colunas
![](https://github.com/Rodrigo80221/AnalisesDeSoftware/blob/main/Imagens/AtacadoWEB/Maximizar.jpg?raw=true)

---

## Correção

1. Arrumar colunas "Lojas" e "Nome da Loja"

![](https://github.com/Rodrigo80221/AnalisesDeSoftware/blob/main/Imagens/AtacadoWEB/ColunaLojaNomeDaLoja.jpg?raw=true)    


---
## Melhoria

1. Alterar os produtos com promoção para não colorir a linha toda, apenas a coluna `Valor Atual`
 * Colocar uma legenda
 * Não colorir os produtos com pack, eles terão o combo
    ![](https://github.com/Rodrigo80221/AnalisesDeSoftware/blob/main/Imagens/AtacadoWEB/ColoririPromocao.jpg?raw=true)

--

## Configuração

1. Criar a configuração abaixo no controle de entradas na opção `Configurações`
    - [x] Exibir Preços de Atacado
1. Ao marcar a opção exibir o um frame com o caption `Configurações padrão para novos modelos`    
    * Neste frame criar configuração de arredondamento igual a tela Desconto Atacado 
    * Neste frame Criar configuração de lojas igual a tela Desconto Atacado 

    ![](https://github.com/Rodrigo80221/AnalisesDeSoftware/blob/main/Imagens/AtacadoWEB/Configurar4.jpg?raw=true)
---

## Listar Packs

1. Verificar se o produto possui pack de atacado
    * Se possui inserir uma linha abaixo do produto. Adicionar o combo que liste a descrição de todos os packs
    ``` sql
    select Descricao from PackVirtual where modelopack = 13
    ```
    * Posicionar o pack do produto
    * As colunas `Valor Atual` (do Atacado) e `Preço de Venda` (do Atacado) receberão o preço de venda do produto com o desconto % do seu pack + arredondamento

    ![](https://github.com/Rodrigo80221/AnalisesDeSoftware/blob/main/Imagens/AtacadoWEB/COMBO1.jpg?raw=true)

---

## Alterar Preço de Venda do Produto

1. Ao alterar a coluna `Preço de Venda` (do Produto) deverá alterar também a coluna `Preço de Venda` (do Atacado) aplicando o percentual + arred.
    * A Margem e Markup são sempre calculadas em relação aos preços em qualquer alteração ocorrida

    ![](https://github.com/Rodrigo80221/AnalisesDeSoftware/blob/main/Imagens/AtacadoWEB/COMBO12.jpg?raw=true)

> Verificar como vou ter o histórico da coluna `Valor Atual` (do atacado)    
---    

## Alterar Pack

1. O usuário poder alterar o pack no combo, caso altere, a `Preço de Venda` (do atacado) deverá ser atualizada com o desconto % + arred.

![](https://github.com/Rodrigo80221/AnalisesDeSoftware/blob/main/Imagens/AtacadoWEB/COMBO2.jpg?raw=true)


---

## Criar a opção `Novo` no combo

1. Caso o usuário não encontre um pack desejado deverá escolher a opção `Novo`

    ![](https://github.com/Rodrigo80221/AnalisesDeSoftware/blob/main/Imagens/AtacadoWEB/COMBO3.jpg?raw=true)


---

## Opção `Novo` no combo ou clique na coluna `Preço Venda`

1. Será exibida a tela de busca
1. A tela deverá vir carregada com os dados atuais do produto

    ![](https://github.com/Rodrigo80221/AnalisesDeSoftware/blob/main/Imagens/AtacadoWEB/Selecionar2.jpg?raw=true)

1. Caso o usuário altere o campo `Valor Atacado` o campo `Desconto %` deverá ser calculado

    > aqui teremos que tentar usar o arredondamento para tirar a quebra do percentual

1. Caso o usuário altere o campo `Desconto %` o campo `Valor Atacado` deverá ser recalculado de acordo com a regra de arredondamento configurado nas configurações

1. A cada alteração o grid deverá buscar os respectivos packs de acordo com a quantidade e porcentagem    

1. O grid deve exibir a coluna `Preço Atacado` que deverá ser calculada com o valor atual + porcentagem do pack + arredondamento do pack. O arredondamento do pack pode ser visto na tela Desconto Atacado.

    ![](https://github.com/Rodrigo80221/AnalisesDeSoftware/blob/main/Imagens/AtacadoWEB/Selecionar1.jpg?raw=true)

1. Se o usuário der duplo clique em uma linha do grid ou selecionar um modelo volta para o controle de entradas


---

## Botão `Adicionar Novo`

> O usuário não encontrou um modelo 

1. Irá clicar no botão `Adicionar Novo`

    ![](https://github.com/Rodrigo80221/AnalisesDeSoftware/blob/main/Imagens/AtacadoWEB/Selecionar3.jpg?raw=true)

1. Será exibido um inputbox sugerindo a descrição

    ![](https://github.com/Rodrigo80221/AnalisesDeSoftware/blob/main/Imagens/AtacadoWEB/Selecionar4.jpg?raw=true)

1. Após confirmado já voltará para a tela do controle de entradas.
1. O pack já será criado no banco com as regras, descrição e configurações de arredondamento;
    * A regra de arredondamento e as lojas estão nas configurações da tela.

> Validar porcentagem entre 0 e 99%
---

## Criar menu para adicionar pack

1. Ao clicar com o botão direito será exibido um menu

    ![](https://github.com/Rodrigo80221/AnalisesDeSoftware/blob/main/Imagens/AtacadoWEB/ListarPacks5.jpg?raw=true)

1. A selecionar `Adicionar Desconto Atacado` exibir a tela de Busca
    * O campo `A partir de (Qtd.)` deverá vir preenchido com a quantidade da embalagem desse fornecedor
    * O campo `Valor Atacado` deverá vir em branco
1. No grid serão listados os packs que se encaixam nas regras digitadas, o usuário pode selecionar um modelo.

    ![](https://github.com/Rodrigo80221/AnalisesDeSoftware/blob/main/Imagens/AtacadoWEB/Selecionar1.jpg?raw=true)

1. Após selecionado um pack voltará para a tela do controle de entradas.

---

## Criar menu para remover pack do produto

1. Ao clicar com o botão direito será exibido um menu para remover o pack do produto

    ![](https://github.com/Rodrigo80221/AnalisesDeSoftware/blob/main/Imagens/AtacadoWEB/ListarPacks6.jpg?raw=true)

---
    
## Botão `Aplicar Valores`    

1. Neste momento iremos colocar o produto no respectivo pack não importando as lojas selecionadas.
---

## Produtos associados

1. No final iremos mostrar a tela de produtos associados para verificar se o cliente deseja inserir a promoção nos outros produtos.
    * Criar um novo form para os packs
    * Caption do form (Produtos Associados - Adicionar Desconto para Atacado)

![](https://github.com/Rodrigo80221/AnalisesDeSoftware/blob/main/Imagens/AtacadoWEB/ProdutosAssociados.jpg?raw=true)

---

## Criar verifica banco no te levo para não precisar mais de script manual


---

## Produto com promoção + pack

> **Espetado:
Programação ok
vb6 - Gestão Promoções - fValidarProdutoComPack
vb6 - Gestão Duplicar encartes
vb6 - PDV - Alteração no mdlValidarRegras - no select de busca dos produtos.
c# - Pack Virtual - ValidarProdutosComPromocao() + ValidarPackComPromocao()**

> O item que está na promoção não deverá sofrer mais uma alteração percentual no preço de venda

1. Tratar no pdv para caso o produto esteja em promoção, não participar dos packs 

1. Tratar nas triggers ou exportação para o Te Levo

1. Alertar caso cadastre um pack para um produto com promoção vigente
    * Alertar também no prorrogar

1. Alertar caso cadastre um promoção para um produto com pack vigente ou com desconto para atacado
    * Alertar também no prorrogar
    * Alertar também no prorrogar encarte    

---
## Criar trigger para atualizar a tabela DriveFilaExportação ao inserir ou alterar um desconto para atacado

> **Espetado:
Programação ok**

``` sql

INSERT INTO [dbo].[DriveTiposExportacao]
           ([CodTipo]
           ,[Descricao])
     VALUES
           (6,'Desconto para Atacado')


ALTER TRIGGER [dbo].[TG_EXPORTACAO_TELEVO_PACKVIRTUALGRUPO1] ON [dbo].[PACKVIRTUALGRUPO1]
FOR INSERT, UPDATE
AS
SET NOCOUNT ON;


DECLARE @CD_PRODUTO AS FLOAT;
DECLARE @IS_UPDATE_INSERT AS BIT;


SET @IS_UPDATE_INSERT = 0;

IF (
        (
            EXISTS (
                SELECT *
                FROM INSERTED
                )
            AND EXISTS (
                SELECT *
                FROM DELETED
                )
            )
        OR (
            EXISTS (
                SELECT *
                FROM INSERTED
                )
            AND NOT EXISTS (
                SELECT *
                FROM DELETED
                )
            )
        )
BEGIN
    SET @IS_UPDATE_INSERT = (1);
END

IF (@IS_UPDATE_INSERT = 1)
BEGIN
    DECLARE NOVO_DRIVE CURSOR FOR

    SELECT DISTINCT I.CodProduto AS CD_PRODUTO
    FROM Inserted I
	INNER JOIN PackVirtual PV ON I.CodPack = PV.Codigo AND PV.ModeloPack = 13
	INNER JOIN PackVirtualLojas PVL ON PV.Codigo = PVL.CodPack 
	INNER JOIN Lojas L ON PVL.CodLoja = L.Codigo AND L.SincronizaDrive = 1
    INNER JOIN LISTA_PRODUTOS LP ON L.CodListaDrive = LP.CD_LISTA AND LP.CD_PRODUTO = I.CodProduto
	WHERE I.CodProduto NOT IN (SELECT D.CodProduto FROM deleted D)
   
    OPEN NOVO_DRIVE;

    FETCH NEXT FROM NOVO_DRIVE INTO @CD_PRODUTO

    WHILE @@FETCH_STATUS = 0
    BEGIN

	DELETE FROM [DriveFilaExportacoes] WHERE Detalhe = CONVERT(NVARCHAR, cast(@CD_PRODUTO AS BIGINT)) AND CodTipoExportacao = 6

    INSERT INTO [dbo].[DriveFilaExportacoes] (
        [CodTipoExportacao]
        , [Detalhe]
        , [DataHoraRegistro]
        , [DataHoraUltimoEnvio]
        , [MensagemErro]
        , [Exportado]
        )
    VALUES (
			6
			, CONVERT(NVARCHAR, cast(@CD_PRODUTO AS BIGINT))
			, getdate()
			, NULL
			, ''
			, 0
        )

        FETCH NEXT FROM NOVO_DRIVE INTO @CD_PRODUTO;
    END

    CLOSE NOVO_DRIVE;

    DEALLOCATE NOVO_DRIVE;
END



```

---
## Criar View para consultado do Te levo, retornando os dados de atacado
> **Espetado:
Programação OK - Criado atualiza banco fCriarObjetosAtacadoWeb()**

``` sql

Create Function fn_ArredondarBC(@Valor Money, @UltimaCasa Money) returns money as
begin
    If (@UltimaCasa = Null Or @UltimaCasa = -1 Or @UltimaCasa = 0) 
        return  Round(@Valor, 1)
    Else
		begin
			set @UltimaCasa = @UltimaCasa / 100
			return  Round(@Valor, 1) + @UltimaCasa
		end
return 0;
End 


Create Function fn_ArredondarAB(@Valor Money, @UltimaCasa Money) returns money as
begin
    If (@UltimaCasa = Null Or @UltimaCasa = -1) 
        return Round(@Valor - (0.049), 1)
    Else
	begin
        set @UltimaCasa = @UltimaCasa / 100
        return Round(@Valor - ((0.049) + @UltimaCasa), 1) + @UltimaCasa
    End
return 0;
End 


Create Function fn_ArredondarAC(@Valor Money, @UltimaCasa Money) returns money as
begin
    If (@UltimaCasa = Null Or @UltimaCasa = -1) 
       return  Round(@Valor + (0.049), 1)
    Else
	begin
        set @UltimaCasa = @UltimaCasa / 100
        return Round(@Valor + (0.049) - @UltimaCasa, 1) + @UltimaCasa
    End 
return 0;
End 


create FUNCTION FN_ArredondarDescontoAtacado (@Valor Money , @AjusteUltimaCasaDecimal Int,  @TipoAjusteValor varchar(2)) returns money as
Begin

    If (@AjusteUltimaCasaDecimal = '') 
      Set @AjusteUltimaCasaDecimal = -1    
	
		if (@TipoAjusteValor = 'BC') 
		begin
			If (Not @TipoAjusteValor = '') 
			begin
				If ( RIGHT(@Valor,1)  > 4) 
					return [dbo].[fn_ArredondarAC] (@Valor, cast(@AjusteUltimaCasaDecimal as money))
				Else
					return [dbo].[fn_ArredondarAB] (@Valor, cast(@AjusteUltimaCasaDecimal as money))					
				
			end
		    else
				return [dbo].[fn_ArredondarBC] (@Valor, cast(@AjusteUltimaCasaDecimal as money))
		end
			
		If (@TipoAjusteValor = 'AC') 
			return [dbo].[fn_ArredondarAC] (@Valor, cast(@AjusteUltimaCasaDecimal as money))

		If (@TipoAjusteValor = 'AB') 
			return [dbo].[fn_ArredondarAB] (@Valor, cast(@AjusteUltimaCasaDecimal as money))

		if (@TipoAjusteValor = '') 
			return @Valor
     	 
return 0;
End;



Create View VW_ProdutosAtacado
as
select 
PVG.CodProduto [CodigoProduto],
PV.QtdRegra [QtdEmbalagem],
[GESTAO].[dbo].FN_ArredondarDescontoAtacado(PL.ValorNoPdv - (PL.ValorNoPdv * PV.VlrRegra/100),PV.AjusteUltimaCasaDecimal,PV.TipoAjusteValor) [ValorProduto], 
PVL.CodLoja 
from PackVirtual PV
inner join PackVirtualLojas PVL on PV.Codigo = PVL.CodPack
inner join PackVirtualGrupo1 PVG on pv.Codigo = pvg.CodPack  
inner join ProdutoLojas PL on PL.codProduto = PVG.CodProduto and PVL.CodLoja = PL.codLoja
where 
ModeloPack in (13) and DtFinal > getdate()
and PVG.CodProduto not in 
(select CD_PRODUTO from PROMOCAO PM where codLoja = PVL.CodLoja and GETDATE() > PM.DT_INICIAL AND GETDATE() < ISNULL(PM.DT_FINAL,GETDATE()+1) and Config = '1' )

``` 
















O LABEL DE ALTERAÇÃO DE PREÇO EM VERMELHO DEVE FICAR VERMELHO ATÉ QUANDO???


REGRA PARA PINTAR DE AMARELO (QUAL CÉLULA EU DEVERIA PINTAR?)

``` vb
if bApurarSt then

    If IIf(ReducaoBaseStEfetivo = 0, ValorVenda, (ValorVenda * ReducaoBaseStEfetivo) / 100) > BaseValorStRetido And BaseValorStRetido <> 0 Then
    ' PINTA DE AMARELO
    end if 

end if 
```

REGRA PARA PINTAR DE VERMELHO CLARO 

``` vb
5   If Abs(btMarkUp - btMarkUpIdeal) > txtDiferencaMarkup.Text Then
6       If btMarkUpIdeal > 0 Then
7          ' PINTA DE VERMELHO
8       End If
9   End If

5   If Abs(btMargem - btMargemIdeal) > txtDiferencaMargem.Text Then
6       If btMargemIdeal > 0 Then
7          ' PINTA DE VERMELHO
8       End If
9   End If
```

REGRA PARA PINTAR DE VERDE 
CASO O PRODUTO ESTEJA EM UMA PROMOÇÃO VIGENTE OU TENHA UM PACK VIRTUAL QUE NÃO SEJA DE ATACADO (MODELOPACK <> 13)


REGRA PARA PINTAR DE CINZA
LINHA DO FORNECEDOR
MARGEM, MARKUP E VALOR DE VENDA




comentar tratamento para coloração do st em amarelo
comentar tratamento para coloração da divergencia de margem em vermelho

colocar coloração de seleção de linha em azul

acertar tratamento da promoção, colocar fonte do valor de venda em verde com tooltips detalhando

confirmar cor vermelha ao enviar carga (que vai virar azul)

colocar cor em azul e talvez em negrito

margem e markup cor em vermelho, com tooltip avisando

diferimento e st | são distorção de impostos que reduzem a margem do produto | colocar a margem em vermelho com tootips de alertas
criar uma estrutura para poder criar novos alertas 











btCodBarras
btCodigoProduto
btDescricaoProduto
btPicStatusPrecosAlterados
btCustoExibido
btCustoGerencialAnterior
btMarkUp
btMargem
btValorVenda
btValorOriginal
btQtdProduto
btCustoMedio
btCfop
btLojaDesc





                        If .RowHeight(CLng(iLinhaProdAnterior)) <> 0 Then
						
                            'liNumeroProdutos = liNumeroProdutos + 1
							
                            liSomaQtdProdutos = liSomaQtdProdutos - .TextMatrix(iLinhaProdAnterior, btQtdProduto)
							
							
							
							
							
                            liSomaQtdProdutos = liSomaQtdProdutos + format(grsGeral("QT_Produto"), "#0.00")
                            lcSomaMarkup = lcSomaMarkup - CCur(.TextMatrix(iLinhaProdAnterior, btQtdProduto) * .TextMatrix(iLinhaProdAnterior, btMarkUp))
                            lcSomaMarkup = lcSomaMarkup + CCur(format(grsGeral("QT_Produto"), "#0.00") * format(fMarkUp(grsGeral("CD_Produto"), IIf(bAlteraCusto, cCustoAtual, grsGeral("CustoGerencialAnterior")), grsGeral("ValorVenda")), "#0.00"))
                            lcSomaMargem = lcSomaMargem - CCur(.TextMatrix(iLinhaProdAnterior, btQtdProduto) * .TextMatrix(iLinhaProdAnterior, btMargem))
                            lcSomaMargem = lcSomaMargem + CCur(format(grsGeral("QT_Produto"), "#0.00") * format(fMargem(IIf(bAlteraCusto, cCustoAtual, grsGeral("CustoGerencialAnterior")), grsGeral("ValorVenda"), grsGeral("Aliquota"), grsGeral("PisEntrada"), grsGeral("CofinsEntrada"), grsGeral("PisSaida"), grsGeral("CofinsSaida"), cIcms_compra, grsGeral("ValorFcpEntrada"), grsGeral("AliquotaFcpVenda"), grsGeral("BaseValorStRetido"), grsGeral("AliquotaIcmsStRetido"), grsGeral("BaseValorStRetido"), grsGeral("AliquotaFcpStRetido"), grsGeral("ReducaoBaseStEfetivo"), bApurarSt), "#0.00"))
                        End If
