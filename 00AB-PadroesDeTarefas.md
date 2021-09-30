
## Tarefa X: Criar branch no git
Criar novo branch feature/EpicoDescontosParaAtacado


## Tarefa X: Criar módulo para Gerenciar o recurso XXXXXXXXXXX

1. Criar verifica banco para inserir módulo. Se o cliente não utiliza PDV o módulo não deve ser verificado. 

Utilizar:
`fVerificarSeUsaPDV()`
`sInserirModulo`
`sInserirModuloSistemasRS`

Executar update em operadores_modulos para ativar o módulo

Descrição das variáveis:

`sGrupo = (PDV,PEDIDOS,FINANCEIRO,RELATÓRIOS,NOTAS,CLIENTES,FORNECEDORES,PRODUTOS)`
`sDescrição = (Nome da Tela)`
`sPalavraChave = (Nome do Form)`

1. Tratar em mdlGestao.sCarregaModulos

    1.1 Chamar na tela clássica no menu de  PDV > Ocorrências com Supervisor

1. Tratar no frmModulos.cmdRestaurar_Click conforme padrão do procedimento

## Tarefa X: Criar Verifica Banco

>Observações:
>Utilizar o Funcoes.fExisteObjeto 
>Utilizar o Funcoes.fExisteObjeto e excluir objeto caso já tenha sido criado
Cuidar time out da conexão, retornar time out no trata erro
Fechar recordset no trata erro
Reabilitar triggers e constraints no trata erro
Funcoes.fAtivarSingleUser goConexao, True


## Tarefa X: Adicionar combo

_KeyPress
KeyAscii = Funcoes.fBuscaCombo(cboFormaPgto, KeyAscii)


## Tarefa X: Criar novo form

Criar form XXXX
No novo form implementar as configurações padrões da Telecon
1. Fechar com esc
1. Enter como tab

        Propriedade do form: keyPreview = True

            Private Sub Form_KeyDown(KeyCode As Integer, Shift As Integer)
                On Error GoTo TrataErro

                Select Case KeyCode
                Case vbKeyReturn
                    If Funcoes.fControleAtivo(Me) <> "grdGrid.name" Then SendKeys "{TAB}"
                Case vbKeyEscape
                    If dtpFinal.Enabled = False Then Exit Sub
                    Unload Me
                Case vbKeyF1
                    KeyCode = 0
                End Select

                Exit Sub
            TrataErro:
                Funcoes.sLogErros App, "frmControleEntradasAtacado", Err, "Form_KeyDown", Erl, True
            End Sub

1. Ícone na janela
1. Abrir com vbmodal
1. Não deve ter os botões de maximizar
1. Não deve permitir resize na tela
        Propriedade do form: BorderStyle: 1 - Fixed Single

1. O Fechar/Sair cancela a operação, caso alterando na barra de cadastros, mostrar mensagem de aviso.

1. Deve abrir centralizado. 
        Propriedade do form: StartUpPosition = 2 - CenterScreen

1. Deve estar correto quanto ao tab index ao finalizar a tela
1. F4 abre a busca 
-- evento activeText_KeyDown
    If KeyCode = vbKeyF4 Then
        cmdBuscaGrupo_Click
    End If        
1. Implementar tratamentos da ampulheta do mouse no click dos botões de lupa, gravar, excluir, alterar e atualiza tela.




# Finalização/ Testes de Integração
1. Verificar tratamentos da ampulheta do mouse, deve ficar em espera em consultas e procedimentos demorados, deverá retornar para a seta após o procedimento. Em caso de erro deverá retornar também para o padrão no trataerro ou try catch

1. Bloquear campos de consulta/pesquisa em consultas e procedimentos demorados, deverá desbloquear após o procedimento finalizar. Em caso de erro deverá retornar também para o padrão no trataerro ou try catch

1. Procedimentos demorados no C# devem ser executados em threads



## Tarefa X: Criar formulário de busca
1. Adicionar Formulário no namespace `GestaoComercial.Formularios.`
1. Adicionar herança do formulário `FrmPesquisa`. Este formulário deverá seguir o padrão dos formulários de pesquisa da Telecon
1. Implementar o `AdicionaColunasList` , (acertar a largura)
``` c#
        base.AdicionarColunaLista("Código", 50, typeof(string));
        base.AdicionarColunaLista("Razao Social", 200, typeof(string));
        base.AdicionarColunaLista("Fantasia", 190, typeof(string));
        base.AdicionarColunaLista("CNPJ", 130, typeof(string));
```
1. Implementar critérios
=> Campo + Valor no Combo
``` c#
        base.AdicionarCriterio(typeof(int), "Codigo"; "Código");
        base.AdicionarCriterio(typeof(string), "Nome", "Razao Social");
        base.AdicionarCriterio(typeof(string), "Fantasia", "Fantasia");
        base.AdicionarCriterio(typeof(string), "CNPJ", "CNPJ");
```
1. Implementar Ordenação
``` c#
        base.AdicionarOrdenacao(typeof(int), "Codigo", "Código");
        base.AdicionarOrdenacao(typeof(string), "Nome", "Razao Social");
        base.AdicionarOrdenacao(typeof(string), "Fantasia", "Fantasia");
        base.AdicionarOrdenacao(typeof(string), "CNPJ", "CNPJ");
```
1. Implementar o `CarregaStringSql`
* utilizar a classe `Nome da Classe da tabela`
* tratar o cursor do mouse durante a pesquisa (trocar seta por ampulheta)



## Tarefa X: Recordset Desconectado

>Nesta tarefa criaremos a funcionalidade xyz, mas por motivos de performance não realizaremos mais uma consulta no banco honerando a tela, iremos utilizar um recordset desconectado

1. Nas declarações do FrmPedidoCompra criar a variável abaixo
``` vb
Private lrsConsultaFfornecedorVendedoresProdutos As ADODB.Recordset
```
1. Criar o procedimento `fCarregarRecordAtacado` para carregar os dados da tabela `FornecedorVendedoresProdrodutos` na memória do cliente (recordset lrsConsultaFfornecedorVendedoresProdutos) de acordo com as regras abaixo:
* Realizar consulta na tabela `FornecedorVendedoresProdrodutos` de acordo com o vendedor e o fornecedor
* Criar o procedimento no mesmo padrão do procedimento `frmControleEntradas3.fCarregarRecordAtacado`