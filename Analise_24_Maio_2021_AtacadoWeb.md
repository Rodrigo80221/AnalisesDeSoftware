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
    ``` Csharp
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

## Busca no combo

1. O usuário poderá digitar no combo para buscar o pack

![](https://github.com/Rodrigo80221/AnalisesDeSoftware/blob/main/Imagens/AtacadoWEB/COMBO4jpg.jpg?raw=true)

---

## Criar a opção `Novo` no combo

1. Caso o usuário não encontre um pack desejado deverá escolher a opção `Novo`

    ![](https://github.com/Rodrigo80221/AnalisesDeSoftware/blob/main/Imagens/AtacadoWEB/COMBO3.jpg?raw=true)


---

## Opção `Novo` no combo ou clique na coluna `Preço Venda`

> Neste momento o usuário já buscou pela descrição ou clicou no `Preço Venda`

> Possívelmente o usuário deseja apenas alterar o `Preço Venda`

1. Será exibida a tela de busca
1. A tela deverá vir carregada com os dados atuais do produto

    ![](https://github.com/Rodrigo80221/AnalisesDeSoftware/blob/main/Imagens/AtacadoWEB/Selecionar2.jpg?raw=true)

1. Caso o usuário altere o campo `Valor Atacado` o campo `Desconto %` deverá ser calculado

    > aqui teremos que tentar usar o arredondamento para tirar a quebra do percentual

1. Caso o usuário altere o campo `Desconto %` o campo `Valor Atacado` deverá ser recalculado de acordo com a regra de arredondamento

1. A cada alteração o grid deverá buscar os respectivos packs    

    ![](https://github.com/Rodrigo80221/AnalisesDeSoftware/blob/main/Imagens/AtacadoWEB/Selecionar1.jpg?raw=true)

1. Se colocar o mouse em uma linha mostrar um tooltip com o Valor de Atacado

1. Se o usuário der duplo clique em uma linha do grid ou selecionar um modelo volta para o controle de entradas

> O packs buscados sempre terão que estar na mesma regra de arredondamento da tela de controle de entradas, senão o resultado final ficará errado.
---

## Botão `Adicionar Novo`

> O usuário não encontrou um modelo 

1. Irá clicar no botão `Adicionar Novo`

    ![](https://github.com/Rodrigo80221/AnalisesDeSoftware/blob/main/Imagens/AtacadoWEB/Selecionar3.jpg?raw=true)

1. Será exibido um inputbox sugerindo a descrição

    ![](https://github.com/Rodrigo80221/AnalisesDeSoftware/blob/main/Imagens/AtacadoWEB/Selecionar4.jpg?raw=true)

1. Após confirmado já voltará para a tela do controle de entradas.
1. O pack já será criado no banco com as regras, descrição e configurações de arredondamento

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

1. Neste momento iremos colocar o produto no respectivo pack
---

## Produtos associados

> Adicionar os produtos associados no pack?

![](https://github.com/Rodrigo80221/AnalisesDeSoftware/blob/main/Imagens/AtacadoWEB/ProdutosAssociados.jpg?raw=true)

---

## Produto com promoção + pack

> Tratar para no atacado nunca aplicar desconto em produtos com promoção?

Item que está na promoção não deverá sofrer alteração percentual no preço de venda
 * Tratar na exportação para o Te levo
 * Tratar no PDV 




































