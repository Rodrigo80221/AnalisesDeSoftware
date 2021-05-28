# Alterações Controle de entradas

## Maximizar

1. Maximizar a tela do controle de entradas para podermos visualizar mais colunas
![](https://github.com/Rodrigo80221/AnalisesDeSoftware/blob/main/Imagens/AtacadoWEB/Maximizar.jpg?raw=true)

---

## Correção

1. Arrumar colunas "Lojas" e "Nome da Loja"

![](https://github.com/Rodrigo80221/AnalisesDeSoftware/blob/main/Imagens/AtacadoWEB/ColunaLojaNomeDaLoja.jpg?raw=true)    


---

## Configuração

1. Criar a configuração abaixo no controle de entradas
    - [x] Exibir Preços de Atacado
1. Criar configuração de arredondamento igual a tela Desconto Atacado 
![](https://github.com/Rodrigo80221/AnalisesDeSoftware/blob/main/Imagens/AtacadoWEB/ConfigurarArredondamento.jpg?raw=true)

> Terei que ter a mesma configuração de arredondamento dos packs.

> Na simulação de preço/ porcentagem terei que buscar somente packs com as mesmas questões de arredondamento, do contrário teremos problemas no resultado final

> Criar configuração de arredondamento geral?

---

## Listar Packs

1. Caso configurado no controle de entradas listar os descontos de atacado para cada produto em forma de combobox 
    * Inicialmente nas colunas `Valor Atual (do Atacado)` e `Preço de Venda (do Atacado)` devem constar o preço de venda do produto com o desconto do pack selecionado + arredondamento
    * Ao alterar a coluna `Preço de Venda (do Produto)` deverá alterar também a coluna `Preço de Venda (do Atacado)` aplicando o percentual
    * A Margem e Markup são sempre calculadas em relação aos preços em qualquer alteração ocorrida

    ![](https://github.com/Rodrigo80221/AnalisesDeSoftware/blob/main/Imagens/AtacadoWEB/COMBO1.jpg?raw=true)

---

## Alterar Pack

1. O usuário poder alterar o tipo de desconto no combo, caso altere o percentual deverá atualizar a coluna `Preço de Venda`

![](https://github.com/Rodrigo80221/AnalisesDeSoftware/blob/main/Imagens/AtacadoWEB/COMBO2.jpg?raw=true)


---

## Busca no combo

1. O usuário poderá digitar no combo para buscar o pack

![](https://github.com/Rodrigo80221/AnalisesDeSoftware/blob/main/Imagens/AtacadoWEB/COMBO4jpg.jpg?raw=true)

---

## Criar a opção `Novo` no combo

1. Caso o usuário não encontre um pack desejado deverá escolher a opção `Novo`

    ![](https://github.com/Rodrigo80221/AnalisesDeSoftware/blob/main/Imagens/AtacadoWEB/COMBO3.jpg?raw=true)


---

## Opção `Novo` no combo ou clique na coluna `Preço Venda`

> Neste caso possívelmente o cliente deseja alterar o preço de venda

1. Será exibida a tela de busca
1. A tela deverá vir carregada com os dados atuais do produto

    ![](https://github.com/Rodrigo80221/AnalisesDeSoftware/blob/main/Imagens/AtacadoWEB/Selecionar2.jpg?raw=true)

1. Caso o usuário altere os campos `Valor Atacado` ou `Desconto %` o outro deverá ser recalculado de acordo com a regra de arredondamento
    * Se houver alteração o grid deverá buscar os respectivos packs
    * Validar porcentagem entre 0 e 99%

    ![](https://github.com/Rodrigo80221/AnalisesDeSoftware/blob/main/Imagens/AtacadoWEB/Selecionar1.jpg?raw=true)

> O packs buscados sempre terão que estar na mesma regra de arredondamento da tela de controle de entradas, senão o resultado final ficará errado.
---

## Botão `Adicionar Novo`

> O cliente não encontrou um modelo 

1. O usuário irá clicar no botão `Adicionar Novo`

    ![](https://github.com/Rodrigo80221/AnalisesDeSoftware/blob/main/Imagens/AtacadoWEB/Selecionar3.jpg?raw=true)

1. Será exibido um inputbox sugerindo a descrição

    ![](https://github.com/Rodrigo80221/AnalisesDeSoftware/blob/main/Imagens/AtacadoWEB/Selecionar4.jpg?raw=true)

1. Após confirmado já voltará para a tela do controle de entradas.
1. O pack já será criado no banco com as regras, descrição e configurações de arredondamento

---

## Criar menu para adicionar pack

1. Ao clicar com o botão direito será exibido um menu

    ![](https://github.com/Rodrigo80221/AnalisesDeSoftware/blob/main/Imagens/AtacadoWEB/ListarPacks5.jpg?raw=true)

1. A selecionar `Inserir Desconto Para Atacado %` exibir a tela de definição de modelos
    * O campo `A partir de (Qtd.)` deverá vir preenchido com a quantidade da embalagem desse fornecedor
    1. O campo `Valor Atacado` deverá vir em branco
1. No grid serão listados os packs que se encaixam nas regras digitadas, o usuário pode selecionar um modelo.

    ![](https://github.com/Rodrigo80221/AnalisesDeSoftware/blob/main/Imagens/AtacadoWEB/Selecionar1.jpg?raw=true)

1. Após selecionado um pack voltará para a tela do controle de entradas.

---

## Criar menu para remover pack do produto

1. Ao clicar com o botão direito será exibido um menu para remover o pack do produto

    ![](https://github.com/Rodrigo80221/AnalisesDeSoftware/blob/main/Imagens/AtacadoWEB/ListarPacks6.jpg?raw=true)


    


    * O procedimento para colorir a promoção deverá ficar apenas em 1 célular
    * Colocar uma legenda




Tratamento para produtos associados
Adicionar os produtos associados no pack?


Item que está na promoção não deverá sofrer alteração percentual no preço de venda
 * Tratar na exportação para o Te levo
 * Tratar no PDV 


mais para frente padronizar as configurações de arredondamento


ajustar o arredondamento e o percentual para ficar número cheio 3,89 / 10%

se alterar o produto pai , altera o percentual do filho

o valor atual é diferente na linha do  pack 




remover pack 

























