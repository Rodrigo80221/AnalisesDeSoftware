
# GESTÃO

## Tarefa 1: Criar módulo

Criar modulo Vale Presente

## Tarefa 2: Criar verifica banco para inserir a nova tecla de comando

> Insere a tecla no menu principal do pdv

``` vb
insert into Teclas_Comandos 
select (select max(codigo) + 1 from Teclas_Comandos), 0 , 'VALE PRESENTE',90,56,''
```

> Insere a tecla no menu da tela do contravale , estamos populando igual ao da sangria 
declare @Codigo as bigint = (select max(codigo) + 1 from Teclas_Comandos)
insert into Teclas_Comandos 
select  @Codigo + Row_Number() Over (Order By Codigo) As RowNum, 9 , funcao,ASCII,Enum,Tecla from Teclas_Comandos where status = 6

adicionar uma tecla livre para o recurso

__________________________________________________________________________________
frmCadTeclas

Criar mais uma aba no formulário do cadastro de teclas, será o status 9
__________________________________________________________________________________




PDV
__________________________________________________________________________________


> Adicionar teclas no enum 

clsVenda - Declarations

Enum Teclas    
    tValePresente = 56
End Enum

__________________________________________________________________________________

> Tratar para exibir o novo menu

FrmPrincipal.fFormKeyDown

  Case loVenda.iTeclas(tValePresente)
      loVenda.btStatus = 9
      frmNaoVinculados.Show vbModal

> Adicionar log de supervisor 

__________________________________________________________________________________
> Tratar load da tela de não vinculados
"frmNaoVinculados", Err, "Form_Load",

    ElseIf frmPrincipal.loVenda.btStatus = 9 Then
        lblTitulo.Caption = "VALE PRESENTE"

__________________________________________________________________________________

> Tratar fluxos da tela de não vinculados

"frmNaoVinculados", Err, "Form_KeyDown",
Tratar para caso o usuário clique ESC e já tenha feito o pagamento em cartão
não deverá permitir, ele deverá finalizar com a tecla T

Testar fluxos com outras teclas, nenhuma deve permitir fugir do fluxo
__________________________________________________________________________________


> Emitir suprimento / contra-vale 


"clsVenda", Err, "fEmiteSuprimento"

Emitir comprovante com texto correto 

                                sGerarCodigoBarrasContraVale
                                loECF.fNaoVinculado gtIF.sContraVale, "CONTRA - VALE", Format(cContraVale, "#,##0.00"), Left(.TextMatrix(btLinha, 2), 2), True, .TextMatrix(btLinha, 1), False, lsCodigosContraVale

__________


____________
ClsECF
Public lsCodigosContraVale As String 
____________


>> emitir contra vale com código de barras 

"frmNaoVinculados", Err, "Form_KeyDown"

                ElseIf .btStatus = 9 Then

                    ' aqui percorrer para ver se tem alguma forma tef
                    If grdFormas.Row = 1 Then

                        If IsNumeric(grdFormas.Text) Then

                            If grdFormas.Text > 0 Then

                                Dim bRetorno As Boolean
                                Dim cValorValePresente As Currency

                                cValorValePresente = grdFormas.Text

                                bRetorno = frmPrincipal.loVenda.goDedicado.fEfetuaTransacao("000000", Format(cValorValePresente, "#,##0.00"))

                            End If

                        End If

                    End If



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
 
 > Antes de finalizar mostrar uma mensagem de validação assim como na sangria e suprimento, deseja emitir um vale presente? 
 
__________________________________________________________________________________

"frmNaoVinculados", Err, "Form_KeyDown"

> após emitir o não vinculado com sucesso enviar o atu, fazer algo semelhante ao código abaixo

.sGeraArqItensNaoVinculado 0, 5, Format(lblValorTotal.Caption, "#,##0.00"), True

__________________________________________________________________________________

> emitir contra vale com código de barras 

"clsECF", Err, "fNaoVinculado",

145     ElseIf sDescricao = "VALE PRESENTE" Then
146         gtIF.iRetorno = goNaoVinculadoImpressora.EmitirContraVale(6, CDbl(sValor), gtIF.sOperador, sCodigoBarrasContraVale, "Troco em contra-vale", False, bEmitirBarras)






__________________________________________________________________________________

> Chamar o TEF na forma de pagamento cartões do não vinculados  

aqui código para “tentar passar o cartão “
tratar cancelar tef 
tratar erros tef para zerar o grid e voltar para a tela 
tratar para no final da transação bloquear a grade 
tratar para imprimir o cupom
> aqui podemos tratar também para chamar a função do cartão só no finalizar 

"frmNaoVinculados", Err, "grdFormas_FimDaEdicao", (final do procedimento)

> aqui tratar corretamente , não pelo número da linha mas sim se a linha for tef ou não 
> após a transação tef a linha será bloqueada e grifada em vermelho
> Deve ter um if também para caso a linha esteja bloqueada, não deverá passar pelo tef mais de uma vez 


    If grdFormas.Row = 1 Then

        If IsNumeric(grdFormas.Text) Then

            If grdFormas.Text > 0 Then
            
                Dim bRetorno As Boolean
                Dim cValorValePresente As Currency

                cValorValePresente = grdFormas.Text

                bRetorno = frmPrincipal.loVenda.goDedicado.fEfetuaTransacao("000000", Format(cValorValePresente, "#,##0.00"))
                
            End If
            
        End If

    End If




__________________________________________________________________________________


GESTÃO
__________________________________________________________________________________


> Criar nova coluna na tela Saldo de caixa 

"frmSaldoCaixa", Err, "sFormataGrid"
Criar outra coluna 

> Popular nova coluna na tela Saldo de caixa 

"frmSaldoCaixa", Err, "cmdVerificar_Click"
preencher coluna 














