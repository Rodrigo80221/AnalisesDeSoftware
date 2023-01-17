
# GESTÃO

## Tarefa 1: Criar módulo

Criar modulo Vale Presente

## Tarefa 2: Criar verifica banco para inserir a nova tecla de comando

1. Insere a tecla no menu principal do pdv

``` vb
insert into Teclas_Comandos 
select (select max(codigo) + 1 from Teclas_Comandos), 0 , 'VALE PRESENTE',90,56,''
```

2. Insere a tecla no menu da tela do contravale , estamos populando igual ao da sangria 

``` sql
declare @Codigo as bigint = (select max(codigo) + 1 from Teclas_Comandos)
insert into Teclas_Comandos 
select  @Codigo + Row_Number() Over (Order By Codigo) As RowNum, 9 , funcao,ASCII,Enum,Tecla from Teclas_Comandos where status = 6
```

3. Apos será necessário configurar manualmente uma tecla livre para o recurso

## Tarefa 3: Criar mais uma aba no formulário do cadastro de teclas, será o status 9

1. Alterar o frmCadTeclas
    - Criar mais uma aba no formulário do cadastro de teclas, será o status 9


# PDV


## Tarefa 4: Adicionar teclas no enum 

1. Alterar o procedimento clsVenda > Declarations

``` vb
Enum Teclas    
    tValePresente = 56
End Enum
```

## Tarefa 5: Exibir o novo menu na tela de venda do PDV

1. Tratar para exibir o novo menu na tela de venda do PDV

``` vb
FrmPrincipal.fFormKeyDown

  Case loVenda.iTeclas(tValePresente)
      loVenda.btStatus = 9
      frmNaoVinculados.Show vbModal
```       

2. Antes de chamar a nova tela deverá exibir a tela de senha do supervisor e gerar um log para ser visualizado nos relatórios de log de supervidor na tela de saldo de caixa.

## Tarefa 6: Tratar Load da tela de não vinculados

1.  Tratar load da tela de não vinculados frmNaoVinculados.Form_Load

``` vb
    ElseIf frmPrincipal.loVenda.btStatus = 9 Then
        lblTitulo.Caption = "VALE PRESENTE"
```         

## Tarefa 7: Tratar Load da tela de não vinculados

1. Tratar fluxos da tela de não vinculados frmNaoVinculados.Form_KeyDown

    - Tratar para caso o usuário clique ESC e já tenha feito o pagamento em cartão. Não deverá permitir, ele deverá finalizar com a tecla T
    - Testar fluxos com outras teclas, nenhuma deve permitir fugir do fluxo

## Tarefa 8: Tratar para emitir o Vale Presente

1. Alterar procedimento clsVenda.fEmiteSuprimento, ou criar outro semelhante só para o vale presente
    - Emitir comprovante com texto correto 

``` vb
sGerarCodigoBarrasContraVale
loECF.fNaoVinculado gtIF.sContraVale, "CONTRA - VALE", Format(cContraVale, "#,##0.00"), Left(.TextMatrix(btLinha, 2), 2), True, .TextMatrix(btLinha, 1), False, lsCodigosContraVale
```                                

 ## Tarefa 9: Chamar o TEF na forma de pagamento cartões do não vinculados  

1. Programar algo mais ou menos como no exemplo abaixo no final do procedimento frmNaoVinculados.grdFormas_FimDaEdicao

2. Tratar fluxos alternativos 
    - tratar cancelar tef 
    - tratar erros tef para zerar o grid e voltar para a tela 
    - tratar para no final da transação bloquear a grade 
    - tratar para imprimir o cupom

3. após a transação tef a linha será bloqueada e grifada em vermelho
    - Deve ter um if também para caso a linha esteja bloqueada, não deverá passar pelo tef mais de uma vez (no finalizar)

``` vb
    If grdFormas.Row = 1 Then ' utilizar fRetornarLinhaFormaPagamentoTEF()

        If IsNumeric(grdFormas.Text) Then

            If grdFormas.Text > 0 Then
            
                Dim bRetorno As Boolean
                Dim cValorValePresente As Currency

                cValorValePresente = grdFormas.Text

                bRetorno = frmPrincipal.loVenda.goDedicado.fEfetuaTransacao("000000", Format(cValorValePresente, "#,##0.00"))
                
            End If
            
        End If

    End If
```



## Tarefa 10: Tratar para passar o cartão e para emitir o não vinculado

2. Ao confirmar o vale presente imprimir o comprovante do tef
3. Ao cofirmar imprimir o contra vale junto com o código de barras (parte na próxima tarefa)
    - Provavelmente será necessário alterar o projeto impressora no c# para formatar o vale presente
4. Antes de finalizar mostrar uma mensagem de validação assim como na sangria e suprimento, deseja emitir um vale presente? 

> Foi criado para teste o codigo abaixo no procedimento frmNaoVinculados.Form_KeyDown

``` vb

'Criar para teste > ClsECF Public lsCodigosContraVale As String 

                ElseIf .btStatus = 9 Then

                    .sGerarCodigoBarrasContraVale

                    .loECF.lsCodigosContraVale = .lsCodigosContraVale

                    .fEmiteNaoVinculado 0, "VALE PRESENTE", Format(lblValorTotal.Caption, "#,##0.00"), 1, True

                    Dim sCodigoContraVale As String
                    If Len(.lsCodigosContraVale) = 12 Then
                        sCodigoContraVale = Mid(.lsCodigosContraVale, 5, 8)
                    Else
                        sCodigoContraVale = .lsCodigosContraVale
                    End If

                    .sInsereContraValePdvMdb sCodigoContraVale, Format(lblValorTotal.Caption, "#,##0.00")
 ``` 
 
 ## Tarefa 11:  Emitir o contra vale com cód barras

1. Alterar em clsECF.fNaoVinculado

``` vb
145     ElseIf sDescricao = "VALE PRESENTE" Then
146         gtIF.iRetorno = goNaoVinculadoImpressora.EmitirContraVale(6, CDbl(sValor), gtIF.sOperador, sCodigoBarrasContraVale, "Troco em contra-vale", False, bEmitirBarras)
``` 

 ## Tarefa 12: Emitir o .atu 

1. Alterar em frmNaoVinculados.Form_KeyDown

> após emitir o não vinculado com sucesso enviar o atu, fazer algo semelhante ao código abaixo

``` vb
.sGeraArqItensNaoVinculado 0, 5, Format(lblValorTotal.Caption, "#,##0.00"), True
``` 

# GESTÃO

## Tarefa 13: Criar nova coluna na tela Saldo de caixa 

1. Criar outra coluna no procedimento frmSaldoCaixa.sFormataGrid
2. Popular nova coluna na tela Saldo de caixa 
3. Preencher coluna no frmSaldoCaixa.cmdVerificar_Click















