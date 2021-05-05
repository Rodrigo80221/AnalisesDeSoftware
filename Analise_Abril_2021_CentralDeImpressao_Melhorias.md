# Épico: Descontos para Atacado - Melhorias central de impressão
Data: 04/2021 
## Problema a ser resolvido
1. Salvar as etiquetas no banco de dados para poder finalizar o épico `Descontos para Atacado`
* Facilitar a impressão de etiquetas. Atualmente o cliente tem que mudar o modelo de etiqueta para cada tipo de produto. Um modelo para preço normal outro para o preço de atacado. No processo atual o cliente abre a tela de preços alterados e imprime as etiquetas, estas etiquetas devem ser impressas com layout de Atacado para os produtos com desconto percentual ou layout de varejo para os produtos sem preço de atacado.
1. Resolver problema da perda de configuração das etiquetas eventualmente e principalmente na atualização do sistema onde as vezes os arquivos são substituídos.
## Pontos chave da história
1. Criar uma configuração na tela Descontos Para Atacado para selecionar um layout de etiqueta quer será utilizada automaticamente na central de impressão

## Impactos
1. Os impactos são todos na central de impressão ao imprimir as etiquetas, deverá ser testado por completo todas as funcionalidades.

## Resultado Final

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
1. Criar atualiza banco para fazer backup das etiquetas. Caso na pasta das etiquetas que está no bd rede, exemplo `\\Servidor\Telecon_Sistemas\Gestao\Etiquetas` possua arquivos de etiquetas salvar essas etiquetas no banco do gestão comercial na tabela Etiquetas. O arquivo deve ser salvo literalmente, uma linha abaixo da outra.    
obs: não salvar o .etq na descrição do arquivo.

## Tarefa 3: Substituir o fileListBox por um ListBox no FrmImpressao

1. No frame fraArquivo trocar o caption Arquivo por Etiquetas

1. Substituir o fileListBox filArquivos por um listview lvwArquivos manter a mesma aparência, utilizar as configurações padrões da telecon. Ao carregar, carregar a descrição da etiqueta e o código oculto para controle.

1. Tratar no FrmImpressao.Form_Load para listar os arquivos de acordo com as informações do banco de dados. No final do Form_Load tratar para buscar a última etiqueta utilizada salva no registro do windows, neste caso tratar para caso não haja nenhuma etiqueta no banco de dados.
A etiqueta que está sendo utilizada deve ficar com a fonte em vermelho e em negrito para ajudar o usuário a identificar qual a etiqueta que está selecionada. 

1. Quando o usuário der um click em uma linha do listview deverá aparecer o tooltip
"Dê duplo clique para selecionar a etiqueta"

1. Manter o mesmo tamanho do list e o tabindex = 87

## Tarefa 4: Excluir etiquetas

1. Substituir o cmdDeletar do FrmImpressao from um cmdSalvarComo, manter o tabindex. Remover o código de exclusão. Esse botão será implementado mais adiante.

1. Colocar um ícone com um "x" do lado direito do list. Se o usuário clicar no item mostrar a mensagem abaixo, retirar do list e excluir do banco.

```
A etiqueta será excluída. A ação não poderá ser desfeita. Deseja continuar? (sim/não) O foco inicial deve ficar em cima do botão não.
```

## Tarefa 5: Salvar, Retirar o txtArquivo e carregar etiqueta

1. Remover o componete txtArquivo do FrmImpressao

1. Retirar código do `Form_resize`

1. Excluir procedimento `filArquivos_Click`

1. Alterar o `cmdSalvar_Click` para não utilizar mais o txtArquivo nem os arquivos texto, Criar a função `fMontarEtiqueta` que chame a função `fSalvarPropriedadeEtiqueta` para cada objeto da tela e retorne uma string com toda a formatação da etiqueta assim como era no arquivo. Utilizaremos essa string novamente em outra tarefa.
    * Salvar a etiqueta no banco de dados.

1. Adequar o `sCarregarConfigEtiquetas` para carregar o arquivo do banco de dados. 

1. Ao dar um duplo clique na listview carregar a etiqueta e deixar o label em vermelho, configurar para não selecionar a linha, para não ficar azul a linha podendo assim ver o vermelho do label. Caso não dê após o duplo clique tirar o foco do componente.

## Tarefa 6: Tratar o fechamento da tela para avisar que a etiqueta foi modificada. 

1. Criar a função fValidarEtiquetaAlterada e chama-la no form_unload

1. Nesta função Chamar o `fMontarEtiqueta` para a etiqueta que está selecionada e compará-la com ela mesmo no banco de dados, se for difente mostrar a mensagem abaixo
    ```
    A etiqueta XXXXX foi alterada, deseja fechar a tela e perder as alterações na etiqueta?
    (sim/não) padrão no não
    ```
1. Caso sim fechar a tela, caso não voltar para a tela da central de impressão

## Tarefa 7: Criar o botão salvar como...

1. No local onde tínhamos o botão Deletar, implementar o botão `cmdSalvarComo` com o Text `S&alvar como...`

1. Implementar o botão, para quando o usuário clicar nele chamar a tela de input box com o texto abaixo, adicionar ele no list view e salvar o novo arquivo no banco de dados.

    `Informe uma nova descrição para a etiqueta:`
1. validar uma descrição válida até 50 caracté
1. Colocar o Tooltip = "Salve a etiqueta atual com um novo nome"


## Tarefa 8: Criar o botão Novo (validar etiquetas)

1. Substituir o botão `cmdCarregar` pelo `cmdNovo` com caption `&Novo` manter o mesmo tabindex

1. Tooltiptext = `Crie uma nova etiqueta a partir de modelos pré definidos.`

1.  Ao clicar no botão exibir um input box com o texto abaixo
    `Insira um nome para a nova etiqueta:`
1. validar uma descrição válida até 50 caractéres

1. Criar um form frmAbrirEtiquetas colocar um list com uma imagem e abaixo listar algumas etiquetas padrões em um listview, algo semelhante com a imagem abaixo
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
1. Abaixo o link para as etiquetas, testar com a argox da telecon, se necessário ajustar os objetos. Testar com valores até 999,00 para ver se não corta
1. Após abrir uma etiqueta de modelo salvar no banco de dados com o nome definido pelo usuário
** Não coloquei as imagens das etiquetas porque ainda podem sofrer alterações.


## Tarefa 9: Teste de integração
1. Ajustar algum código antigo que possa ter ficado para trás
1. Testar as opções criadas e a integração com o banco de dados, novo, salvar como, deletar e salvar. Criar e selecionar diversas etiquetas e imprimir verificando o comportamento da tela. 
1. Ajustar possíves erros. 
 


















