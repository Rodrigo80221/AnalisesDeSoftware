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

## Tarefa 4: Substituir o fileListBox por um ListBox no FrmImpressao

1. No frame fraArquivo trocar o caption Arquivo por Etiquetas

1. Substituir o fileListBox filArquivos por um listview lvwArquivos manter a mesma aparência, utilizar as configurações padrões da telecon. Ao carregar, carregar a descrição da etiqueta e o código oculto para controle.

1. Tratar no FrmImpressao.Form_Load para listar os arquivos de acordo com as informações do banco de dados. No final do Form_Load tratar para buscar a última etiqueta utilizada salva no registro do windows, neste caso tratar para caso não haja nenhuma etiqueta no banco de dados.
A etiqueta que está sendo utilizada deve ficar com a fonte em vermelho e em negrito para ajudar o usuário a identificar qual a etiqueta que está selecionada. 

1. Quando o usuário der um click em uma linha do listview deverá aparecer o tooltip
"Dê duplo clique para selecionar a etiqueta"

1. Manter o mesmo tamanho do list e o tabindex = 87

## Tarefa 5: Excluir etiquetas

1. Substituir o cmdDeletar do FrmImpressao from um cmdSalvarComo, manter o tabindex. Remover o código de exclusão. Esse botão será implementado mais adiante.

1. Colocar um ícone com um "x" do lado direito do list. Se o usuário clicar no item mostrar a mensagem abaixo, retirar do list e excluir do banco.

```
A etiqueta será excluída. A ação não poderá ser desfeita. Deseja continuar? (sim/não) O foco inicial deve ficar em cima do botão não.
```

## Tarefa 6: Salvar, Retirar o txtArquivo e carregar etiqueta

1. Remover o componete txtArquivo do FrmImpressao

1. Retirar código do `Form_resize`

1. Excluir procedimento `filArquivos_Click`

1. Alterar o `cmdSalvar_Click` para não utilizar mais o txtArquivo nem os arquivos texto, Criar a função `fMontarEtiqueta` que chame a função `fSalvarPropriedadeEtiqueta` para cada objeto da tela e retorne uma string com toda a formatação da etiqueta assim como era no arquivo. Utilizaremos essa string novamente em outra tarefa.
    * Salvar a etiqueta no banco de dados.

1. Adequar o `sCarregarConfigEtiquetas` para carregar o arquivo do banco de dados. 

1. Ao dar um duplo clique na listview carregar a etiqueta e deixar o label em vermelho, configurar para não selecionar a linha, para não ficar azul a linha podendo assim ver o vermelho do label. Caso não dê após o duplo clique tirar o foco do componente.

## Tarefa 7: Tratar o fechamento da tela para avisar que a etiqueta foi modificada. 

1. Criar a função fValidarEtiquetaAlterada e chama-la no form_unload

1. Nesta função Chamar o `fMontarEtiqueta` para a etiqueta que está selecionada e compará-la com ela mesmo no banco de dados, se for difente mostrar a mensagem abaixo
    ```
    A etiqueta XXXXX foi alterada, deseja fechar a tela e perder as alterações na etiqueta?
    (sim/não) padrão no não
    ```
1. Caso sim fechar a tela, caso não voltar para a tela da central de impressão

## Tarefa 8: Criar o botão salvar como...

1. No local onde tínhamos o botão Deletar, implementar o botão `cmdSalvarComo` com o Text `S&alvar como...`

1. Implementar o botão, para quando o usuário clicar nele chamar a tela de input box com o texto abaixo, adicionar ele no list view e salvar o novo arquivo no banco de dados.

    `Informe uma nova descrição para a etiqueta:`
1. validar uma descrição válida até 50 caracté
1. Colocar o Tooltip = "Salve a etiqueta atual com um novo nome"


## Tarefa 9: Criar o botão Novo (validar etiquetas)

1. Substituir o botão `cmdCarregar` pelo `cmdNovo` com caption `&Novo` manter o mesmo tabindex

1. Tooltiptext = `Crie uma nova etiqueta a partir de modelos pré definidos.`

1.  Ao clicar no botão exibir um input box com o texto abaixo
    `Insira um nome para a nova etiqueta:`
1. validar uma descrição válida até 50 caractéres

1. Criar um form frmAbrirEtiquetas colocar um list com uma imagem e abaixo listar algumas etiquetas padrões em um listview, algo semelhante com a imagem abaixo
    ![](https://github.com/Rodrigo80221/MARKDOWN/blob/main/Imagens/SelecaoEtiqueta.png?raw=true)

    * No novo form manter as configurações padrões da Telecon, fechar com esc, enter como tab, ícone na janela, abrir com vbmodal, não deve ter os botões de minimizar e maximizar, o fechar cancela a operação, não deve permitir resize na tela, deve abrir centralizado. 
    * Abaixo colocar 2 botões (&Abrir Etiqueta e &Cancelar), ajustar o tabindex
    * Na imagem colocar uma foto da imagem para cada arquivo
    [Link para as etiquetas](https://github.com/Rodrigo80221/MARKDOWN/tree/main/Download/Etiquetas)
    * Criar um procedimento para cada uma destas etiquetas retornando uma string
    * Abaixo o link para as etiquetas, testar com a argox da telecon, se necessário ajustar os objetos. Testar com valores até 999,00 para ver se não corta
    * após abrir uma etiqueta de modelo salvar no banco de dados com o nome definido pelo usuário
    * Não coloquei as imagens das etiquetas porque ainda podem sofrer alterações.


## Tarefa 10: Teste de integração
1. Ajustar algum código antigo que possa ter ficado para trás
1. Testar as opções criadas e a integração com o banco de dados, novo, salvar como, deletar e salvar. Criar e selecionar diversas etiquetas e imprimir verificando o comportamento da tela. 
1. Ajustar possíves erros. 
 



criar classe de packs virtuais 
colocar uma propriedade para saber se é pack ou modelo de desconto
nas telas listas de acordo com essa propriedade

alterar nomenclaturas

criar desconto para atacado para os associados 

colocar associação da etiqueta na tela de desconto para atacado

Colocar pack virtual ou desconto para atacado no cadastro de produtos 

Arrumar help dos packs mais novos para ajudar o usuário
Verificar a coluna Pack nos grids e o pack nas mensagens
Sigan > frmBusca > coluna grid  (Pack/Atacado)
Importa cupom fiscal > coluna grid
FrmImpressão > Coluna grid (na etiqueta e no cartaz) no cartaz está cortando a descrição
Encarte > 2 mensagens 


  



## --------------------------------------------

### Leve x pague y > Leve 3 pague 2

```
Exemplo de promoção:
Compre 12 unidades do refrigerante XXX e pague apenas 10 unidades.
```
### Pague 1 centavo ou mais em outro produto > Leve 3 e ganhe outro produto diferente

```
Exemplo de promoção:
Compre 2 unidades da Pizza XXX e pague 1 centavo ou mais em um bombom da marca YYY.
Observação: O valor de 1 centavo pode ser trocado por outro valor.
```
### Pague menos a partir de x unidades (atacado) > A partir de 6 pague menos R$

```
Exemplo de promoção:
Compre mais de 100 latas de Refri Lata da marca XXX e pague 1 real em cada unidade.
```
### Pague menos por unidade > Leve 6 pague menos em cada unid.

```
Exemplo de promoção:
Compre 12 caixas de leite da marca XXX e pague R$ 1,00 cada caixa.
Observação: A 13ª caixa de leite custará o preço original.
```

### Pague 1 centavo ou mais no próximo produto > Leve 3 e ganhe 1

```
Exemplo de promoção:
Compre 5 biscoitos da marca XXX e pague 1 centavo ou mais no próximo.
```
### Leve x e Receba desconto percentual > Leve 3 e ganhe desconto em outro produto diferente R$

```
Exemplo de promoção:
Compre 2 unidades da Pizza XXX e ganhe 10% de desconto na compra de um refrigerante da marca HHH.

Exemplo para dar desconto no mesmo produto.
Exemplo de promoção: Compre o produto XXX e receba 10% de desconto
Passos: 
1)Insira 0,1 na quantidade e informe o desconto percentual.
2)Insira o mesmo produto nas duas grades.
```
### Leve x e Receba desconto por unidade > Leve 3 e ganhe desconto em outro produto diferente R$

```
Exemplo de promoção:
Compre 5 unidades da Pizza XXX e ganhe R$ 0,10 de desconto em cada.
Observação: A 6ª unidade  não entra na promoção, apenas quando fechar 10 unidades.
```
### Pague x porcento a menos a partir de x unidades (atacado) > A partir de 6 pague menos %

```
Exemplo de promoção:
Compre mais de 100 unidades de Refri Lata da marca XXX e receba 10% de desconto em cada unidade.
```
### Leve RS x e receba desconto por unidade no produto x >  Nas compras acima de RS50,00 ganhe desconto nestes produtos R$

```
Exemplo de promoção:
Compre R$ 50,00 em produtos de fora deste pack e ganhe R$ 2,00 de desconto no produto x.
```
### Leve RS x e receba desconto percentual no produto x > Nas compras acima de RS50,00 ganhe desconto nestes produtos %

```
Exemplo de promoção:
Compre R$ 50,00 em produtos de fora deste pack e ganhe 10 % de desconto no produto x.
```
### Pague x por cento a menos por unidade > Leve 6 pague menos em cada unid. (%)
















