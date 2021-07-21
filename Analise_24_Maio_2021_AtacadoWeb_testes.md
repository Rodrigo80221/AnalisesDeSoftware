## Produtos com Promoção + Pack/ Desconto Atacado

1. Mensagens de aviso ao cadastrar um produto com 2 tipos de promoção
    * Mensagem ao cadastrar promoção
    * Mensagem no prorrogar promoção
    * Mensagem ao cadastrar pack virtual
    * Mensagem no prorrogar pack virtual
    * Mensagem ao cadastrar desconto atacado
    * Mensagem ao prorrogar encarte

1. PDV - produto com pack ou desconto atacado cadastrado para uma determinada loja deverá ser vendido com preço de promoção

1. Atacado web - O produto com desconto atacado deverá ser vendido com preço de promoção

## Maximizar

1. Maximizar controle de entradas deverá funcionar em diversas resoluções com ou sem a configuração usa atacado

## Grupo de produtos

1. Campo para código do grupo no ecommerce

## Controle de entradas

1. Se não marcar a configuração "Exibir preços de Atacado" a tela deverá funcionar como antes, com melhorias no maximizar e nas colorações e tooltip

1. Ao ativar a configuração do controle de entradas usa preço atacado 
    * permitir cadatrar as lojas e arredondamento para os novos descontos para atacado que serão cadastrados na tela
    * Ao salvar as configurações de arredondamento e loja deverão ser refletidas em todos os descontos para atacado cadastrados
    * mostrar coluna de desconto atacado na grade
    * menu para add ou remover desconto atacado nos produtos 
    * ao alterar valor, margem ou markup do produto deverá refletir no preço do atacado
    * ao alterar valor, margem ou markup do desconto atacado deverá abrir a tela de alteração já com o valor digitado
    * duplo clique no valor, margem ou markup do desconto atacado deverá abrir a tela de alteração
    * Atalhos (A) e (DEL) para inserir ou remover atacado
    * combobbox deve possibilitar alteração do desconto de atacado
    * ao alterar o combobox deverá ser recalculado o markup, margem e valor de venda 
    * novo do combobox abre a tela para cadastrar novo desconto atacado

1. Detalhes da tela
    * Na grade o cálculo da porcentagem do desconto atacado deverá levar em consideração o arredondamento configurado
    * Ao aplicar valores deverá ser salvo o desconto atacado de acordo todas as lojas configuradas
    * Ao aplicar valores deverá ser desativado os descontos para atacado que não são mais utilizandos, visando ter o menor número possível
    * Ao listar na grade o desconto atacado deverá ser listado de acordo com as lojas configuradas
    * Se o produto estar repetidos na grade para lojas diferentes que estejam configuradas para utilizar o desconto atacado, deverá ser inserido, removido e alterado em todas as ocorrências
    * Ao filtrar um número grande de registros deverá ser exibida uma mensagem e não dar erro na tela
    * Foram feitos diversos tratamentos para a coloração de seleção do grid (azul), não deverá se perder ou se repetir

1. Tooltip + coloração do grid
    * produto em promoção
    * produto com pack
    * diferenças entre markup/ markup ideal e margem/ margem ideal de acordo com a diferença configurada 
    * produtos fora do mix 
    * custo gerencial de notas que não alteram custo
    * preço de venda alterado - neste caso agora deverá ficar alterado até o envio de carga para os pdvs

1. Impressão
    * o relatório deverá imprimir também os registros relacionados a desconto atacado
    
## Controle de entradas - Tela de cadastro/alteração do desconto para atacado

    * Na tela de alteração ao digitar um valor de atacado deverá sugerir uma porcentagem calculada de acordo com o arredondamento configurado. Buscando sempre uma porcentagem que não fique quebrada
    * Na tela de alteração ao digitar uma porcentagem o valor de atacado deverá ser calculado de acordo com o arredondamento configurado
    * Ao cadastrar um novo desconto atacado pelo atalho (A), pelo menu inserir, ou pelo novo do combo, a quantidade do produto deverá ser preencida de acordo com a embalagem do fornecedor
    * Na grade da tela de alteração ao editar a porcentagem o valor de atacado deverá ser calculado de acordo com o arredondamento configurado