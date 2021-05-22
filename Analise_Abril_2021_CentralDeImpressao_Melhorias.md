# Épico: Descontos para Atacado - Melhorias central de impressão
Data: 04/2021 

## Problema a ser resolvido

1. Salvar as etiquetas no banco de dados para poder finalizar o épico `Descontos para Atacado` na questão de relacionar a etiqueta com um pack virtual ou desconto para atacado.

1. Resolver problema da perda de configuração das etiquetas que ocorre eventualmente, principalmente na atualização do sistema onde as vezes os arquivos são substituídos.

## Pontos chave da história
1. Salvar as etiquetas no banco de dados
1. Criar uma aba na central de impressão para relacionar uma etiqueta com um pack virtual ou desconto para atacado.

## Impactos
1. Os impactos são todos na central de impressão ao imprimir as etiquetas, deverá ser testado por completo todas as funcionalidades existentes.

## Resultado Final

### Parte 1 
1. Criar tabela Etiquetas no banco sql 
1. Migrar etiquetas para o banco de dados
1. Realizar alterações na tela para se adequar a nova estrutura
1. Criar o botão novo que sugere alguns layouts pré formatados
    ![](https://raw.githubusercontent.com/Rodrigo80221/MARKDOWN/main/Imagens/SelecaoEtiqueta.png)

### Parte 2 
1. Criar a tabela EtiquetasRelacionamentos
1. Criar a aba relacionar etiquetas
    ![](https://github.com/Rodrigo80221/AnalisesDeSoftware/blob/main/Imagens/FrmImpresssao_Aba_RelacionarEtiquetas.jpg?raw=true)
1. Imprimir as etiquetas de acordo com a relação    



## Tarefa 1: Criar tabela no verifica banco
1. Criar verifica banco para inserir a tabela abaixo

``` sql
CREATE TABLE Etiquetas
(
    Codigo tinyint Identity Primary Key,
    Descricao varchar(50) not null,
    Arquivo nvarchar(max) not null
)            
```
>Observações:
>frmLogin.lblInicializando.Caption = "Criando tabela Etiquetas"
>Utilizar o Funcoes.fExisteObjeto 


## Tarefa 2: Criar verifica banco para migrar etiquetas para o sql server
**Objetivo:** Atualmente salvamos as etiquetas da central de impressão na pasta `C:\Telecon_Sistemas\Gestao\Etiquetas`, e isso tem gerados alguns problemas de perda de arquivos, problemas de acesso nas pastas e substituição dos arquivos nas atualizações do sistema. Iremos a partir deste momento salvar as etiquetas no banco de dados. 

Na atualização do sistema iremos migrar as etiquetas para o banco de dados.
1. Criar atualiza banco para fazer backup das etiquetas. 
    * Buscar caminho do servidor no bd rede, exemplo `\\Servidor\Telecon_Sistemas\Gestao\Etiquetas` 
    * Se possuir arquivos de etiquetas salvar essas etiquetas no banco do gestão comercial na tabela Etiquetas
    * O arquivo deve ser salvo literalmente, uma linha abaixo da outra.    
    * obs: não salvar o .etq na descrição do arquivo.

## Tarefa 3: Substituir o fileListBox por um ListBox no FrmImpressao

1. Para teste deixar algumas etiquetas salvas na tabela Etiquetas
1. No frame fraArquivo trocar o caption Arquivo por Etiquetas
1. Substituir o fileListBox filArquivos por um listview lvwArquivos 
    * Manter a mesma aparência, utilizar as configurações padrões da telecon. 
1. Verificar se o procedimento `Form_Resize` continuará funcionando, se necessário ajustar    
1. Manter o mesmo tamanho do list e o tabindex = 87

## Tarefa 3: Carregar etiquetas no Form_Load

1. Criar a função `fListarEtiquetas` para listar as etiquetas no listview, carregar a descrição das etiquetas
    * Buscar a última etiqueta utilizada salva no registro do windows `GetSetting("Gestao", "Impressao", "ArquivoEtq", -1)`
    * Posicionar o List na etiqueta que retornou no passo acima. A etiqueta que está sendo utilizada deve ficar no list com a fonte em negrito. 
    * Caso não retorne nada, deixar o list sem posição
    * A função deverá __retornar uma string__ com o conteúdo da etiqueta. Ou retornar em branco.    
    
1. Tratar no FrmImpressao.Form_Load para listar os arquivos de acordo com as informações do banco de dados. 
    * Substituir o código abaixo pela chamada da função `fListarEtiquetas` para carregar o list
    ``` vb
        16  If Dir(strRede & "..\Etiquetas", vbDirectory) = "" Then MkDir strRede & "..\Etiquetas"

        17  If Funcoes.fExistePasta(strRede & "..\Etiquetas") = False Then
        18      Screen.MousePointer = vbDefault
        19      MsgBox "Pasta com os arquivos de etiqueta não foi encontrada: " & App.Path & "\relatorios\cartazes\", vbInformation
        20  Else
        21      filArquivos.Path = strRede & "..\Etiquetas"

        22  End If
    ```
    * exluir o código abaixo
    ``` vb
        filArquivos.Refresh
    ```
    * exluir o código abaixo
    ``` vb
        96  filArquivos.Refresh
    ```    

    * exluir o código abaixo, ele deve estar todo contemplado na função `fListarEtiquetas`
    ``` vb
    85  If filArquivos.ListCount > 0 Then
    86      If Val(GetSetting("Gestao", "Impressao", "ArquivoEtq", -1)) <= filArquivos.ListCount _
            And Val(GetSetting("Gestao", "Impressao", "ArquivoEtq", -1)) >= 0 Then
    87          filArquivos.ListIndex = Val(GetSetting("Gestao", "Impressao", "ArquivoEtq", -1))
    88      Else
    89          filArquivos.ListIndex = 0
    90      End If
    91  End If
    ```   

1. No final do formload substituir o trecho abaixo 

    ``` vb
    93      cmdCarregar_Click
    ``` 
    
    Por

    ``` vb
    sCarregarConfigEtiquetasParametro 
    ``` 
    * enviar por parâmentro uma variável com o retorno da função `fListarEtiquetas` chamada anteriormente.

1. No procedimento sCarregarConfigEtiquetasParametro __alterar o nome do parâmetro__ `NomeEtiqueta As String` por `Etiqueta As String`. 

1. Alterar o procedimento `fLerPropriedadeEtiquetaParametro` para receber o parâmentro `Etiqueta As String` e em vez de percorrer o arquivo da etiqueta, __percorrer esse parâmetro__

1. Excluir os procedimentos abaixo
`sCarregarConfigEtiquetas()`
`cmdCarregar_Click`
`fLerPropriedadeEtiqueta`
`filArquivos_Click`

1. Alterar o procedimento `sUsaImpressoraWindows` para buscar do registro do windows e não da configuração `UltimaEtiquetaSelecionada`. O código está logo no início.




## Tarefa 3: Alterar procedimento filArquivos_DblClick

1. Alterar o procedimento `filArquivos_DblClick` para carregar a etiqueta na tela
    * Buscar a etiqueta no banco de dados
    * Chamar o sCarregarConfigEtiquetasParametro
    * Salvar a etiqueta selecionada no registro do windows.

## Tarefa 3: Alterar procedimento Form_Unload
1. Alterar o código abaixo no Form_Unload para salvar no registro do windows 
    ``` vb
    4   If filArquivos.filename <> "" Then
    5       SaveSetting "Gestao", "Impressao", "ArquivoEtq", filArquivos.ListIndex
    6   End If
    ```


## Tarefa 3: Ajustar alterações

1. Devido a alteração do filelist e retirada das funções alguns códigos terão que ser adaptados.
    * Dar ctrl + F5 e verificar demais pontos que devem ser alterados, ajustar.
    Caso haja alterações relevantes verificar com a análise.

1. Com o list carregado quando o usuário colocar o mouse em uma linha do listview colocar o tooltip
"Dê duplo clique para selecionar a etiqueta"


## Tarefa 4: Excluir etiquetas

1. Substituir o cmdDeletar do FrmImpressao por um cmdSalvarComo, manter o tabindex. Remover o código de exclusão. Esse botão será implementado mais adiante.

1. Colocar um ícone com um "x" do lado direito do list. Se o usuário clicar no item mostrar a mensagem abaixo, retirar do list e excluir do banco.

```
A etiqueta será excluída. A ação não poderá ser desfeita! Deseja continuar? (sim/não) O foco inicial deve ficar em cima do botão não.
```

## Tarefa 5: Salvar, Retirar o txtArquivo e carregar etiqueta

1. Remover o componete txtArquivo do FrmImpressao
    * Retirar código do `Form_resize`

1. Alterar o `cmdSalvar_Click` para retirar o 1º e o 2º IF

1. Criar a função `fRetornarPropriedadesEtiqueta`
    * Ela deverá retornar um string com todas configurações da etiqueta, assim como era no arquivo `.etq` 
    * Retirar o código que está no `cmdSalvar_Click` e utilizar na função `fRetornarPropriedadesEtiqueta`
    * Remover a função `fSalvarPropriedadeEtiqueta` ou altera-la para salvar na string e dar um enter

1. No procedimento `cmdSalvar_Click` chamar a função fRetornarPropriedadesEtiqueta e implementar para salvá-la no banco de dados, salvar de acordo com a etiqueta do list que está sendo alterada

1. Ainda poderá restar trechos de códigos dos componentes que alteramos na tela, ir dando ctrl + F5 e corrigir

## Tarefa 6: Tratar o fechamento da tela para avisar que a etiqueta foi modificada. 

1. Criar a função fValidarEtiquetaAlterada e chama-la no form_unload

1. Nesta função Chamar o `fRetornarPropriedadesEtiqueta` para a etiqueta que está selecionada e compará-la com ela mesmo no banco de dados, se for difente mostrar a mensagem abaixo
    ```
    A etiqueta XXXXX foi alterada, deseja fechar a tela e perder as alterações na etiqueta?
    (sim/não) padrão no não
    ```
1. Caso sim fechar a tela, caso não voltar para a tela da central de impressão


## Tarefa 7: Criar o botão salvar como...

1. No local onde tínhamos o botão Deletar, implementar o botão `cmdSalvarComo` com o Text `S&alvar como...`

1. Implementar o botão, para quando o usuário clicar nele chamar a tela de input box com o texto abaixo, adicionar ele no list view e salvar o novo arquivo no banco de dados.

    `Informe uma nova descrição para a etiqueta:`
1. validar uma descrição válida até 50 caracteres
1. Validar para não inserir uma nomenclatura repetida
1. Colocar o Tooltip no botão = "Salve a etiqueta atual com um novo nome"


## Tarefa 8: Criar o botão Novo (validar etiquetas)

1. Substituir o botão `cmdCarregar` pelo `cmdNovo` com caption `&Novo` manter o mesmo tabindex

1. Tooltiptext = `Crie uma nova etiqueta a partir de modelos pré definidos.`

1.  Ao clicar no botão exibir um input box com o texto abaixo
    `Insira um nome para a nova etiqueta:`
1. validar uma descrição válida até 50 caractéres

1. Criar um form frmImpressaoNovaEtq colocar um list com uma imagem e abaixo listar algumas etiquetas padrões em um listview, algo semelhante com a imagem abaixo
    ![](https://raw.githubusercontent.com/Rodrigo80221/MARKDOWN/main/Imagens/SelecaoEtiqueta.png)

No novo form manter as configurações padrões da Telecon
1. Fechar com esc
1. Enter como tab
1. Ícone na janela
1. Abrir com vbmodal
1. Não deve ter os botões de minimizar e maximizar
1. O fechar cancela a operação
1. Não deve permitir resize na tela
1. Deve abrir centralizado. 
1. Deve estar correto quanto ao tab index ao finalizar a tela
1. Abaixo colocar 2 botões (&Abrir Etiqueta e &Cancelar), ajustar o tabindex
1. Na imagem colocar uma foto da imagem para cada arquivo
    [Link para as etiquetas](https://github.com/Rodrigo80221/MARKDOWN/tree/main/Download/Etiquetas)
1. Criar um procedimento para cada uma destas etiquetas retornando uma string
1. Abaixo o link para as etiquetas, testar com a argox da telecon, ajustar os objetos. Testar com valores até 999,00 para ver se não corta
1. Após abrir uma etiqueta de modelo salvar no banco de dados com o nome definido pelo usuário
** Não coloquei as imagens das etiquetas porque ainda podem sofrer alterações.


## Tarefa 9: Ajustes finais e Teste de integração

1. As tarefas anteriores refletam a estrutura principal, realizar os testes abaixos e corrigir eventuais erros.

1. Testar as opções criadas e a integração com o banco de dados
    * novo
    * salvar como
    * deletar 
    * salvar
    * Criar e alternar entre as etiquetas, verificar a impressão tanto na argox quanto no windows

 



















