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
SELECT 
PVG.CodProduto [CodigoProduto],
PV.QtdRegra [QtdEmbalagem],
CASE WHEN VALOR_PROMOCAO IS NULL THEN [GESTAO].[dbo].FN_ArredondarDescontoAtacado(PL.ValorNoPdv - (PL.ValorNoPdv * PV.VlrRegra/100),PV.AjusteUltimaCasaDecimal,PV.TipoAjusteValor) ELSE VALOR_PROMOCAO END [ValorProduto], 
PVL.CodLoja
FROM PackVirtual PV
INNER JOIN PackVirtualLojas PVL on PV.Codigo = PVL.CodPack
INNER JOIN PackVirtualGrupo1 PVG on pv.Codigo = pvg.CodPack  
INNER JOIN ProdutoLojas PL on PL.codProduto = PVG.CodProduto and PVL.CodLoja = PL.codLoja
LEFT JOIN PROMOCAO P ON P.CD_PRODUTO = PVG.CodProduto AND P.CodLoja = PL.codLoja AND P.CONFIG LIKE '1%'
WHERE 
ModeloPack in (13) and DtFinal > getdate()

``` 





``` sql
--CORREÇÃO DO BUG DE NÃO TER GRUPO DE CLIENTES
	DECLARE @CodGrupoCliente as tinyint
	DECLARE @CodPack as int

	DECLARE rsCursorPack CURSOR LOCAL FOR
	select Codigo from PACKVIRTUAL WHERE modelopack = 13 and CODIGO NOT IN (SELECT CodPack FROM [PackVirtualGrupoClientes]))
	OPEN rsCursorPack 
	FETCH NEXT FROM rsCursorPack INTO @CodPack
	WHILE @@FETCH_STATUS = 0 
	BEGIN  

		DECLARE rsCursorCliente CURSOR LOCAL FOR     

		select Codigo from GRUPOS_CLIENTES
 
		OPEN rsCursorCliente 
		FETCH NEXT FROM rsCursorCliente INTO @CodGrupoCliente
		WHILE @@FETCH_STATUS = 0 
		BEGIN       
			INSERT INTO [PackVirtualGrupoClientes] (CodPack,CodGrupoCliente) VALUES (@CodPack, @CodGrupoCliente)
			FETCH NEXT FROM rsCursorCliente INTO @CodGrupoCliente
		END  

		CLOSE rsCursorCliente 
		DEALLOCATE rsCursorCliente


		FETCH NEXT FROM rsCursorPack INTO @CodPack
	END  

	CLOSE rsCursorPack 
	DEALLOCATE rsCursorPack
```     




Tarefa 19.1: Fixar configurações na tela desconto atacado

Atualmente o usuário pode definir as configurações padrões do desconto atacado no form frmControleDeEntradas. Caso ele faça isso devemos usar as mesmas configurações na tela desconto atacado e boloquear a edição

>Caso ele ative a configuração no controle de entradas
>"ControleDeEntradas_DescontoAtacado_UsaAtacado" = "1"


1. Padronizar a configuração de arredondamento e desabilitar a edição
`ControleDeEntradas_DescontoAtacado_TipoAjusteValor` ("AC","AB","BC","")
`ControleDeEntradas_DescontoAtacado_AjusteUltimaCasaDecimal`

1. Padronizar a selecção de lojas e desabilitar a edição
`ControleDeEntradas_DescontoAtacado_ConfigLojas`
    > obs: O código da loja virá separado por vírulas exemplos ("1") ou ("1,2,3,4,5")

1. Padronizar a descrição e bloquear, padronizar no formato abaixo, inserir a descrição ao alterar ou qtdRegra ou vlrRegra
`A PARTIR DE 12 GANHE 12,00% DE DESCONTO`






Innovation Center/GestaoComercial/GestaoComercial.Biblioteca/PackVirtual/PackVirtual.cs
Innovation Center/GestaoComercial/GestaoComercial.Biblioteca/PackVirtual/PackVirtualVB6.cs
Innovation Center/GestaoComercial/GestaoComercial/Formularios/PackVirtual/FrmPackVirtual.cs





















Tarefa 19.2: Validar e se preciso Adicionar código criado na hitória do atacado web

>As alterações abaixo servem para alertar caso seja criado um pack virtual ou um desconto para atacado em um produto que já possua promoção. As validações também são utilizadas na tela do controle de entradas.

>Pode ser que as funções já estejam na tela refatorada.

Arquivo
`Innovation Center/GestaoComercial/GestaoComercial/Formularios/PackVirtual/FrmPackVirtual.cs`
No final do `private void TestarCampos()` colocar o código abaixo



``` csharp
            // Foi colocada essa validação de lojas que não tinha
            if (dgvLojasCadastro.Visible)
            {
                var lojas = RetornarLojasSelecionadasDataGridView(dgvLojasCadastro);

                if (lojas.Count.Equals(0))
                {
                    throw new Exception("Selecione uma loja!");
                }
            }
             
            if (!ValidarProdutosComPromocao())
                throw new Exception("Remova os produtos com promoção ativa!");

```

Criar o procedimento abaixo


``` csharp

        private bool ValidarProdutosComPromocao()
        {
 
            var lojas = RetornarLojasSelecionadasDataGridView(dgvLojasCadastro);

            var listaLojas = "";

            if (dgvLojasCadastro.Visible)
            {
                foreach (var l in lojas)
                {

                    if (listaLojas != "")
                        listaLojas += ",";

                    listaLojas += l.Codigo.ToString();

                }
            }
            else
            {
                listaLojas = "1";
            }

            string produtosGrupo1 = "";

            for (var l = 0;l < dgvGrupo1.Rows.Count;l++)
            {
                if (produtosGrupo1 != "")
                    produtosGrupo1 += ",";

                produtosGrupo1 += dgvGrupo1[l, (int)ColunasDgvGrupo.Codigo];

            }

            string produtosGrupo2 = "";

            for (var l = 0; l < dgvGrupo2.Rows.Count; l++)
            {
                if (produtosGrupo2 != "")
                    produtosGrupo2 += ",";

                produtosGrupo2 += dgvGrupo2[l, (int)ColunasDgvGrupo.Codigo];

            }

            PackVirtualVB6 vb6 = new PackVirtualVB6();
            var banco = Telecon.GestaoComercial.Biblioteca.Utilitarios.Utilitarios.ObterConexao();
            return vb6.ValidarPackComPromocao(listaLojas, produtosGrupo1, produtosGrupo2, Convert.ToDateTime(dtpHoraInicio.Value), Convert.ToDateTime(dtpHoraFim.Value), banco);

        }


```

Alterar o procedimento `private void btnProrrogar_Click(object sender, EventArgs e)`


``` csharp

    if (!removido)
    {

```

por 


``` csharp

    var alertaPromocao = ValidarPackComPromocao(item.Codigo, listaPackVirtualGrupo1, listaPackVirtualGrupo2, dataAtual, item.DtFinal);

    if (!removido && alertaPromocao)
    {

```



Criar o procedimento abaixo


``` csharp

        private Boolean ValidarPackComPromocao(double codPack, List<Pack.PackVirtualGrupo1> listaPackVirtualGrupo1, List<Pack.PackVirtualGrupo2> listaPackVirtualGrupo2, DateTime dataInicial, DateTime dataFinal)
        {

            string listaLojas = "";
            string produtosGrupo1 = "";
            string produtosGrupo2 = "";

            var banco = Telecon.GestaoComercial.Biblioteca.Utilitarios.Utilitarios.ObterConexao();

            var dr = banco.Consultar("Select CodLoja from PackVirtualLojas Where CodPack = " + codPack.ToString());

            while (dr.Read())
                listaLojas += listaLojas == "" ? dr["CodLoja"].ToString() : "," + dr["CodLoja"].ToString();
                        
            for (var aux = 0; aux < listaPackVirtualGrupo1.Count(); aux++)
                produtosGrupo1 += produtosGrupo1 == "" ? listaPackVirtualGrupo1[aux].CodProduto.ToString() : "," + listaPackVirtualGrupo1[aux].CodProduto.ToString();
            
            for (var aux = 0; aux < listaPackVirtualGrupo2.Count(); aux++)
                produtosGrupo2 += produtosGrupo2 == "" ? listaPackVirtualGrupo2[aux].CodProduto.ToString() : "," + listaPackVirtualGrupo2[aux].CodProduto.ToString();

            banco.FecharConexao();
            banco.Dispose();

            PackVirtualVB6 vb6 = new PackVirtualVB6();
            return vb6.ValidarPackComPromocao(listaLojas, produtosGrupo1, produtosGrupo2, dataInicial, dataFinal, banco);

        }

```


Criar o procedimento abaixo em `Innovation Center/GestaoComercial/GestaoComercial.Biblioteca/PackVirtual/PackVirtualVB6.cs` 

===> Adicionar no final do arquivo

``` csharp

        public Boolean ValidarPackComPromocao(string listaLojas, string produtosGrupo1, string produtosGrupo2, DateTime dataInicial, DateTime dataFinal, string servidor)
        {
            var banco = new Sql2000(servidor, "GESTAO", "sa", "a2m8x7h5");
            banco.AbrirConexao();

            return ValidarPackComPromocao(listaLojas, produtosGrupo1, produtosGrupo2, dataInicial, dataFinal, banco);
        }


        public Boolean ValidarPackComPromocao(string listaLojas, string produtosGrupo1, string produtosGrupo2, DateTime dataInicial, DateTime dataFinal, IBanco banco)
        {


            string PromocaoDTInicio = "P.DT_INICIAL";
            string PromocaoDTFIM = "ISNULL(CONVERT(DATETIME,P.DT_FINAL),CONVERT(smalldatetime,'2079/06/06'))";
            string PackDTInicio = banco.ObterDataHora(dataInicial);
            string PackDTFIM = banco.ObterDataHora(dataFinal);


            string sql = "";

            sql = "SELECT CD_PROMOCAO, CD_PRODUTO, CODLOJA, DT_INICIAL, DT_FINAL , (SELECT DESCRICAOREDUZIDA FROM PRODUTOS WHERE CODIGO = CD_PRODUTO) [DESCRICAO] FROM PROMOCAO P" + Environment.NewLine;
            sql += "WHERE" + Environment.NewLine;
            //sql += "CONFIG = '1'" + Environment.NewLine;
            sql += "CODLOJA IN(" + listaLojas + ")" + Environment.NewLine;
            sql += "AND (" + Environment.NewLine;
            sql += "    CD_PRODUTO IN(" + produtosGrupo1 + ")" + Environment.NewLine;

            if (produtosGrupo2 != "")
                sql += "    OR CD_PRODUTO IN(" + produtosGrupo2 + ")" + Environment.NewLine;

            sql += "    )" + Environment.NewLine;
            sql += "AND" + Environment.NewLine;
            sql += "(" + Environment.NewLine;

            sql += "    ( " + PackDTInicio + " > " + PromocaoDTInicio + " and " + PackDTInicio + " < " + PromocaoDTFIM + " ) " + Environment.NewLine;
            sql += " or ( " + PackDTFIM + " > " + PromocaoDTInicio + " and " + PackDTFIM + " < " + PromocaoDTFIM + " ) " + Environment.NewLine;
            sql += " or ( " + PackDTInicio + " < " + PromocaoDTInicio + " and " + PackDTFIM + " > " + PromocaoDTFIM + " ) " + Environment.NewLine;

            sql += " ) " + Environment.NewLine;

            var dr = banco.Consultar(sql);

            var msgm = "Os produtos abaixo possuem promoção vigente neste período! Durante a promoção o pack virtual ou desconto para atacado não ficará ativo. Deseja continuar mesmo assim?" + Environment.NewLine + Environment.NewLine;

            while (dr.Read())
                if (!msgm.Contains(dr["CD_PRODUTO"].ToString()))
                    msgm += "Produto: " + dr["CD_PRODUTO"].ToString() + " - " + dr["DESCRICAO"].ToString() + Environment.NewLine;

            if (msgm.Contains("Produto:"))
                if (Msg.PerguntarPadraoNao(msgm) == DialogResult.No)
                {
                    banco.FecharConexao();
                    banco.Dispose();
                    return false;
                }

            banco.FecharConexao();
            banco.Dispose();

            return true;
        }

```












## Tarefa 20: Adicionar facilitador para o Desconto de Atacado no Cadastro de Produtos 

![](https://github.com/Rodrigo80221/AnalisesDeSoftware/blob/main/Imagens/FrmCadProdutos_FramePromocao.jpg?raw=true)

1. Se o produto possui pack virtual ou desconto atacado na loja logada , mostrar um label com a descrição do pack ou atacado no cadastro de produtos no local onde está o combobox de desconto atacado na imagem

1. Criar um checkbox "Usa Desconto Atacado" 
    * Se o "Usa Desconto Atacado" esteja configurado no controle de entradas exibir esse checkbox
    * Se o produto possuir desconto para atacado marcar essa opção
    * Se o produto possuir pack virtual nunca exibir essa pção 
    
* Carregar um combobox com os descontos atacado cadastrados ordernados por qtdRegra, vlrRegra
Carregar uma opção novo

* Se o checkbox estiver marcado exibir combobox na posição do atacado do produto
    * o usuário pode alterar o desconto atacado do produto
    * o usuário escolher a opção Novo (chamar o frmControleEntradasAtacado), no retorno recarregar o combo

* Se o usuário marcar o checkbox exibir o combobox na posição Novo

> Ao alterar um desconto atacado

* Se o produto possui promoção para uma das lojas configuradas no controle de entradas apresentar mensagem de alerta (chamar validação que já existe no controle de entradas no menu add desconto atacado)

1. Acertar o tabIndex












## Erros relacionados a promoções

> Durante o desenvolvimento da história atacado web foi encontrado alguns problemas relacionados a promoções

`hotfix/CorrecoesPromocoes`

### 1. Erro no controle de entradas 
* Ao Cadastrar uma promoção pela tela de promoções, abrir o controle de entradas (novo - do menu ajuda) e alterar o preço do produto. A promoção não ia para o PDV.
Percebi que ao abrir a tela de preços alterados não tinhamos o preço da promoção para enviar para o PDV, o registro sumia, logo a promoção não ia para o PDV
> Resultado esperado
* A promoção deve gerar um registro em preços alterados, ao alterar o preço do produto pelo controle de entradas, cadastro de produtos ou outro local, não deve gerar um novo preço alterado pois o preço que deve ser enviado para o PDV é o da promoção.

### 2. Erro no cadastro de produtos
* Considere o cenário abaixo

Produto Coca Cola

Loja | Matriz|Loja 1|Loja 2
----------|-----|-----|-----
Vlr. Venda|R$ 3,00 |R$ 4,00 |R$ 6,00 
Promoção|R$ 1,99 |R$ 1,99 |R$ 1,99  

* Logar na Matriz
* Ir no cadastro de produtos na aba `Lojas e Estoques`
* Alterar o preço do produto da Loja 2 para R$ 5,00

* Verificar a promoção da loja 2 na tela de promoções
    * Neste caso o sistema colocava no valor anterior da promoção o valor de venda da Matriz de 3,00 e ao acabar a promoção o valor de venda da loja 2 ficaria 3,00 e deveria ficar R$ 6,00

> Resultado esperado
* Ao alterar o valor de venda da loja 2 ou de qualquer loja com promoção, independente de estar logado nessa loja, ao encerrar a promoção deve manter o valor de venda anterior.