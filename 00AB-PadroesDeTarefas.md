
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
>frmLogin.lblInicializando.Caption = ""
>Utilizar o Funcoes.fExisteObjeto 

>Utilizar o Funcoes.fExisteObjeto e excluir objeto caso já tenha sido criado


Cuidar time out da conexão, retornar time out no trata erro
Fechar recordset no trata erro
Reabilitar triggers e constraints no trata erro
 Funcoes.fAtivarSingleUser goConexao, True




## Tarefa X: Criar novo form

Criar form XXXX
No novo form implementar as configurações padrões da Telecon
1. Fechar com esc
1. Enter como tab
1. Ícone na janela
1. Abrir com vbmodal
1. Não deve ter os botões de maximizar
1. O Fechar/Sair cancela a operação
1. Não deve permitir resize na tela
1. Deve abrir centralizado. 
1. Deve estar correto quanto ao tab index ao finalizar a tela
1. F4 abre a busca 


