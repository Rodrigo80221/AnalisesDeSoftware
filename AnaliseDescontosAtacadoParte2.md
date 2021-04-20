## Tarefa 1: Criar tabela no verifica banco
1. Criar verifica banco para inserir a tabela abaixo

``` sql
CREATE TABLE Etiquetas
(
    Codigo int Identity Primary Key,
    Descricao varchar(50) not null,
    Arquivo nvarchar(max) not null
)            
```

## Tarefa 2: Criar verifica banco para migrar etiquetas para o sql server
**Objetivo:** Atualmente salvamos as etiquetas da central de impressão na pasta `C:\Telecon_Sistemas\Gestao\Etiquetas`, e isso tem gerados alguns problemas de perda de arquivos, problemas de acesso nas pastas e substituição dos arquivos nas atualizações do sistema. Iremos a partir deste momento salvar as etiquetas no banco de dados. 

Na atualização do sistema iremos migrar as etiquetas para o banco de dados.
1. Criar atualiza banco para fazer backup das etiquetas. Programar conforme a lógica abaixo
* caso na pasta das etiquetas que está no bd rede, exemplo `\\Servidor\Telecon_Sistemas\Gestao\Etiquetas` possua arquivos de etiquetas, fazer backup delas, voltar um diretório e criar a pasta BkpEtiquetas exemplo `\\Servidor\Telecon_Sistemas\Gestao\BkpEtiquetas`
Salvar essas etiquetas no banco do gestão comercial na tabela Etiquetas. O arquivo deve ser salvo literalmente, uma linha abaixo da outra.
    
obs: não salvar o .etq na descrição do arquivo.

2. Excluir os arquivos .etq do diretório `Etiquetas`

## Tarefa 4: Substituir o fileListBox por um ListBox no FrmImpressao

1. No frame fraArquivo trocar o caption Arquivo por Etiquetas

1. Substituir o fileListBox filArquivos por um listview lvwArquivos manter a mesma aparência, utilizar as configurações padrões da telecon. Ao carregar, carregar a descrição da etiqueta e o código oculto para controle.

1. Tratar no FrmImpressao.Form_Load para listar os arquivos de acordo com as informações do banco de dados. No final do Form_Load tratar para buscar a última etiqueta utilizada.
A etiqueta que está sendo utilizada deve ficar com a fonte em vermelho e em negrito para ajudar o usuário a identificar qual a etiqueta que está selecionada.

1. Quando o usuário der um click em uma linha do listview deverá aparecer o tooltip
"Dê duplo clique para selecionar a etiqueta"

1. Manter o mesmo tamanho do list e o tabindex = 87

## Tarefa 5: Excluir etiquetas

1. Excluir o cmdDeletar do FrmImpressao.

1. Colocar um ícone com um "x" do lado direito do list. Se o usuário clicar no item mostrar a mensagem abaixo, retirar do list e excluir do banco.

1. O layout da etiqueta será excluído. A ação não poderá ser desfeita. Deseja continuar? O foco deve ficar em cima do botão não.

## Tarefa 6: Salvar, Retirar o txtArquivo e carregar etiqueta

1. Remover o componete txtArquivo do FrmImpressao

1. Retirar código do `Form_resize`

1. Excluir procedimento `filArquivos_Click`

1. Alterar o `cmdSalvar_Click` para não utilizar mais o txtArquivo nem os arquivos texto, Criar a função `fMontarEtiqueta` que chame a função `fSalvarPropriedadeEtiqueta` para cada objeto da tela e retorne uma string. Utilizaremos essa string novamente em outra tarefa.

1. Adequar o `sCarregarConfigEtiquetas` para carregar o arquivo do banco de dados. 

1. Ao dar um duplo clique na listview carregar a etiqueta e deixar o label em vermelho, configurar para não selecionar a linha, para não ficar azul a linha podendo assim ver o vermelho do label.

## Tarefa 7: Tratar o fechamento da tela para avisar que a etiqueta foi modificada. 

1. Chamar o `fMontarEtiqueta` para a etiqueta que está selecionada e compará-la com ela mesmo no banco de dados, se for difente mostrar a mensagem abaixo
    ```
    A etiqueta XXXXX foi modificada, deseja fechar a tela e perder as modificações?
    (sim/não) padrão no não
    ```

1. Criar uma função para esse procedimento.

## Tarefa 8: Criar o botão salvar como...

1. Aonde tínhamos o botão Deletar, criar o botão `cmdSalvarComo` com o Text `S&alvar como...`

2. Implementar o botão, para quando o usuário clicar nele chamar a tela de input box da telecon com o texto abaixo, adicionar ele no list view e salvar o novo arquivo no banco de dados.

    `Insira um nome para a nova etiqueta:`

3. colocar o tabindex = 91

4. Colocar o Tooltip = "Crie uma nova etiqueta com o layout atual mostrado na tela"


## Tarefa 9: Criar o botão Novo (validar etiquetas)

1. Substituir o botão `cmdCarregar` pelo `cmdNovo` com caption `&Novo` manter o mesmo tabindex

1.  Ao clicar no botão exibir um input box da telecon com o texto abaixo, validar uma descrição válida
    `Insira um nome para a nova etiqueta:`

1. Criar um form frmAbrirEtiquetas colocar um list com uma imagem e abaixo listar algumas etiquetas padrões, algo semelhante com a imagem abaixo
    * No novo form manter as configurações padrões da Telecon, fechar com esc, enter como tab, ícone na janela, abrir com vbmodal, não deve ter os botões de minimizar e maximizar, o fechar cancela a operação, não deve permitir resize na tela, deve abrir centralizado. 
    * Abaixo colocar 2 botões (&Abrir e &Cancelar)
    * Na imagem colocar um print da imagem para cada arquivo
    * Listar nesse formato
    ```
        Etiqueta Básica
        Etiqueta Grande
        Etiqueta com referência a unidade básica 
        Etiqueta Atacado
        Etiqueta Atacado com referencia a unidade básica
    ```
    * Criar um procedimento para cada etiqueta retornando uma string
    * Abaixo o link para as etiquetas, testar com a argox da telecon, se necessário ajustar os objetos 
    * após abrir salvar no banco de dados com o nome definido pelo usuário



  
1. 



