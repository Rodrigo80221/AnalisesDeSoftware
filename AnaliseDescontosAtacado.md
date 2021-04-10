#Épico: Descontos para Atacado
Abril/2021

##Problema a ser resolvido

Relato do cliente:
*"No sistema Telecon não tem o pack virtual? então você vai criando varios packs,não é isso,  no sistema softcom eu tenho o gestão de promoções e aqui tem os grupos , então você criava, grupo do feijão, o grupo do feijão todo o feijão a partir de 5 unidades  dá um desconto de tantos porcento, então para você não criar um grupo, pra você não ir colocando de 1 por 1, você criava um grupo e dizia “Feijão” aí esses códigos de barras que estiverem nesse grupo a partir de 5 unidades vai ter 5% de desconto. A diferença desse para o Telecon é que se eu for pegar um de cada feijão e conseguir os 5 ele vai dar desconto no 5 , só que na verdade é para dar o desconto apenas em 1 , só vai dar o desconto se eu levar 5 de 1 código de barras"*

Atualmente o cliente Novo Preço possui muitos produtos no formato de atacado. Ex:
Se levar mais de 6 coca colas 2l ganhe 10% de desconto

Para utilizar esse processo no nosso sistema o cliente precisa adicionar um pack com essa regra. 

Mas no nosso sitema se adicionar mais de um produto no pack ele agrupa todos os produtos para a contagem do desconto. Ex:
Se criar a regra
`Leve 6 produtos e ganhe 10% de desconto`
e adicionar no pack 
`coca cola 2l`
`suco tang 50g`

Se o cliente vender 1 coca cola e 6 tangs ganhará 10% de desconto. Isso causou prejuízo para o cliente inicialmente. Então ele teve que fazer um pack para cada produto e já tem mais de 3 mil packs cadastrados

Inicialmente faremos uma solução similar ao que já temos mas que possa dar desconto para os produtos separadamente. 

##Pontos chave da história

1. Criar uma forma ágil de configurar uma % de desconto para os produtos. O cliente quer poder configurar o desconto para um conjunto de produtos, inclusive adicionar um grupo inteiro.
1. Facilitar a impressão de etiquetas. Atualmente o cliente tem que mudar o modelo de etiqueta para cada tipo de produto, um modelo para preço normal outro para o preço de atacado. No processo atual o cliente abre a tela de preços alterados e imprime as etiquetas, estas etiquetas devem ser impressas com layout de Atacado para os produtos com desconto percentual ou layout de varejo para os produtos sem preço de atacado.
1. Alterar o cupom para mostrar os descontos de atacado assim como no seu concorrente principal.

##Impacto

1. Na etapa 1 iremos programar de forma para termos o menor impacto possível. A nova tela irá especializar a tela de Pack Virtual e poderemos ter impacto nesta funcionalidade.
1. Os arredondamentos para a Nova funcionalidade devem funcionar da mesma maneira que no modelo de pack abaixo.
`Pague x porcento a menos a partir de x unidades (atacado)`
1. Não podemos adicionar um pack e um desconto de atacado para o mesmo produto
1. Ao adicionar um produto em um cadastro do Descontos de Atacado e esse produto fizer parte de um pack ou de uma outra regra de Descontos de Atacado deve mostrar uma mensagem se o usuário deseja excluir do outro grupo e adicionar neste. 
1. Iremos alterar a central de impressão, é algo crítico no processo do cliente, não deve desconfigurar a etiqueta ao abrir esta tela em outros momentos. 

#Tarefas






## Tarefa 1: Criar branch no git
Criar novo branch feature/EpicoDescontosParaAtacado


 ## Tarefa 2: Corrigir layout na tela Pack Virtual

1. Corrigir Label
    Ao dar um novo e selecionar o modelo 
    `Pague x porcento a menos a partir de x unidades (atacado)`

    Alterar o label da regra substituindo
    `% de desconto nestes outros produtos` 
    por 
    `% de desconto por unidade nestes produtos`

    o _"outros"_ deve ser usado apenas nos packs que tem 2 grids
1. Corrigir tamanho do combo
    Aumentar o combo de modelo, pois está cortando algumas descrições no pack virtual
1. Corrigir o tamanho da fonte do label lblMensagemPrecoDiferente para 8 pts


 ## Tarefa 3: Alterar posição dos frames de configuração do Pack Virtual
Objetivo: Foram criados alguns frames de configuração que estão ocupando espaço na tela e também poluindo. Vamos criar uma aba de configuração

1. Alterar a posição do frame gbxLimitePack
Nos dois Packs abaixo ele deve ser colocado na nova aba criada no groupBoxValores. O caption da aba deverá estar como "Configuração de Limites da Regra"
`Leve X pague Y`
`Pague menos por unidade`

1. Alterar a posição do frame gbAjustarQuebra
No Pack abaixo ele deve ser colocado na nova aba criada no groupBoxValores. O caption da aba deverá estar como "Configuração de quebra (Arredondamento)"
 `Pague x porcento a menos a partir de x unidades (atacado)`


## Tarefa 4: Possibilitar maximizar a tela Pack Virtual
Objetivo: Poder maximizar a tela para aumentar a grade de produtos, o cliente irá inserir mais de mil produtos nesta grade e atualmente ela é pequena.
1. Abilitar o botão de maximizar a tela
1. Ao maximizar a tela ancorar o label lblMensagemPrecoDiferente e os botões de imprimir etiqueta, cancelar e salvar na parte inferior do formulário.
1. Ajustar os dois grid de produto de forma que fiquem ancorados na parte inferior do formulário.

## Tarefa 5: Colocar um contador de registros para o grid de produtos
1. Colocar um label contador de registros para o grid dgvGrupo1. Ao lado do label lblMensagemPrecoDiferente com o mesmo padrão de funcionamento do label lblTotalRegistros. Controlá lo nos diversos estados da tela. 




## Tarefa 3: Criar módulo para Gerenciar o recurso Descontos para Atacado

1. Criar verifica banco para inserir módulo. Se o cliente não utiliza PDV o módulo não deve ser verificado. 

Utilizar:
`fVerificarSeUsaPDV()`
`sInserirModulo`
`sInserirModuloSistemasRS`

Executar update em operadores_modulos para ativar o módulo

Descrição das variáveis:

`sGrupo = "PRODUTOS"`
`sDescrição = "Descontos para Atacado`
`sPalavraChave = FrmDescontosAtacado`

1. Tratar em mdlGestao.sCarregaModulos

    1.1 Chamar na tela clássica no menu de PRODUTOS > Descontos para Atacado.  _Se possível posicionar abaixo do Pack Virtual_

1. Tratar no frmModulos.cmdRestaurar_Click conforme padrão do procedimento

## Tarefa 4: Criar formulário C# FrmDescontosParaAtacado

Objetivo: A nova tela deverá herdar a tela pack virtual, a ideía é reaproveitar o código que já tem na tela do pack, encartes, lojas, grupo de clientes, formas de pagamento, configurações de arredondamento entre outros. E criar uma personalização da tela mas sem alterar a tela de pack virtual. 
1. Criar diretório e formulario no gestão c#.
1. Adicionar todas as diretivas que estão no form FrmPackVirtual (using) 
1. Adicionar a diretiva `using System.ComponentModel.Design.Serialization;`
1. Adicionar o código abaixo em todos os eventos do form FrmPackVirtual (load, close,keypress) 
`if (this.DesignMode) { return; }`
1. Adicionar no FrmDescontosAtacado a Herança para o form FrmPackVirtual assim como fazermos para utilizar o formulário de pesquisa FrmPesquisa
1. Fechar o Visual Studio ir no diretorio do projeto, excluir a pasta bin e obj. Abrir novamente o visual studio, dar um clear na solution e um rebuild. Nesse momento o Design do pack já deve estar funcionando no Desconto Atacado
1. Corrigir eventuais erros que podem aparecer. 
1. Se alterarmos a tela do pack, podemos ter que repetir alguns dos passos acima. 








