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

> Terei que ter a mesma configuração de arredondamento dos packs
> Na simulação de preço/ porcentagem terei que buscar somente packs com as mesmas questões de arredondamento, do contrário teremos problemas no resultado final
> Criar configuração de arredondamento geral?

---

## Listar Packs

1. Caso configurado no controle de entradas listar os descontos de atacado para cada produto em forma de combobox 
    * Inicialmente nas colunas `Valor Atual (do Atacado)` e `Preço de Venda (do Atacado)` devem constar o preço de venda do produto com o desconto do pack aplicado
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




1. Permitir editar a quantidade da regra 
1. Permitir editar a porcentagem da regra 
1. Ao alterar o preço de venda deve alterar a porcentagem, ao alterar a porcentagem deve alterar o preço de venda (margem e markup andam juntas)
1. Se estiver marcado a sugestão de arredondamento deve ser utilizada também para o pack virtual
1. Deve atualizar dinamicamente a descrição do pack
1. Ao salvar o pack deve salvar em um pack com a mesma descrição e regras ou criar outro
![](https://github.com/Rodrigo80221/AnalisesDeSoftware/blob/main/Imagens/AtacadoWEB/ListarPacks4.jpg?raw=true)




---

![](https://github.com/Rodrigo80221/AnalisesDeSoftware/blob/main/Imagens/AtacadoWEB/ListarPacks5.jpg?raw=true)
1. Adicionar menu no produto "Inserir desconto para atacado (%)"
1. Exibir input box para exibir a quantidade e porcentagem inicial

---

1. Destaque da cor da linha
    * Atualmente os packs e as promoções são destacados com verde, será mantida a mesma cor???
    * O procedimento para colorir a linha está bem ruim, pisca muito
    * Colocar uma legenda



---


1. Criar configurações para colunas visualizadas




---


Tratamento para produtos associados



Item que está na promoção não deverá sofrer alteração percentual no preço de venda
 * Tratar na exportação para o Te levo
 * Tratar no PDV 






qtd atacado




mais para frente padronizar as configurações de arredondamento


ajustar o arredondamento e o percentual para ficar número cheio 3,89 / 10%

se alterar o produto pai , altera o percentual do filho

o valor atual é diferente na linha do  pack 

tirar os custos da linha do pack , cfop , loja, tirar o simbolo também 

sugerir a quantidade a partir do fornecedor da nota, no imputbox , o índio vai utilizar 

remover pack 

























