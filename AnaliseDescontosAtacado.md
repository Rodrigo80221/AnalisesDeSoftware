# Épico: Descontos para Atacado
Abril/2021

## Problema a ser resolvido

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

## Pontos chave da história

1. Criar uma forma ágil de configurar uma % de desconto para os produtos. O cliente quer poder configurar o desconto para um conjunto de produtos, inclusive adicionar um grupo inteiro.
1. Facilitar a impressão de etiquetas. Atualmente o cliente tem que mudar o modelo de etiqueta para cada tipo de produto, um modelo para preço normal outro para o preço de atacado. No processo atual o cliente abre a tela de preços alterados e imprime as etiquetas, estas etiquetas devem ser impressas com layout de Atacado para os produtos com desconto percentual ou layout de varejo para os produtos sem preço de atacado.
1. Alterar o cupom para mostrar os descontos de atacado assim como no seu concorrente principal.
1. Após a implementação validar com a Gabriela. Ela está acompanhando as dificuldades do cliente.

## Impacto

1. Na etapa 1 iremos programar de forma para termos o menor impacto possível. A nova tela irá especializar a tela de Pack Virtual e poderemos ter impacto nesta funcionalidade.
1. Os arredondamentos para a Nova funcionalidade devem funcionar da mesma maneira que no modelo de pack abaixo.
`Pague x porcento a menos a partir de x unidades (atacado)`
1. Não podemos adicionar um pack e um desconto de atacado para o mesmo produto
1. Ao adicionar um produto em um cadastro do Descontos de Atacado e esse produto fizer parte de um pack ou de uma outra regra de Descontos de Atacado deve mostrar uma mensagem se o usuário deseja excluir do outro grupo e adicionar neste. 
1. Iremos alterar a central de impressão, é algo crítico no processo do cliente, não deve desconfigurar a etiqueta ao abrir esta tela em outros momentos. 

# Tarefas






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


 ## Tarefa 3: Alterar posição do frame de Limite da Regra do Pack Virtual

1. Alterar a posição do frame gbxLimitePack, colocar ele para a direita assim como o frame de arredondamentos, tentar colocar ele acima do frame da direita para não ficar por cima e para caber os dois.


## Tarefa 4: Possibilitar maximizar a tela Pack Virtual
Objetivo: Poder maximizar a tela para aumentar a grade de produtos, o cliente irá inserir mais de mil produtos nesta grade e atualmente ela é pequena.
1. Abilitar o botão de maximizar a tela
1. Ao maximizar a tela ancorar o label lblMensagemPrecoDiferente e os botões de imprimir etiqueta, cancelar e salvar na parte inferior do formulário.
1. Ajustar os dois grid de produto de forma que fiquem ancorados na parte inferior do formulário.

## Tarefa 5: Colocar um contador de registros para o grid de produtos
1. Colocar um label contador de registros para o grid dgvGrupo1. Ao lado do label lblMensagemPrecoDiferente com o mesmo padrão de funcionamento do label lblTotalRegistros. Controlá lo nos diversos estados da tela. 


## Tarefa 6: Criar módulo para Gerenciar o recurso Descontos para Atacado

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

## Tarefa 7: Criar formulário C# FrmDescontosParaAtacado

Objetivo: A nova tela deverá herdar a tela pack virtual, a ideía é reaproveitar o código que já tem na tela do pack, encartes, lojas, grupo de clientes, formas de pagamento, configurações de arredondamento entre outros. E criar uma personalização da tela mas sem alterar a tela de pack virtual. 
1. Criar diretório e formulário no gestão c#.
1. Adicionar todas as diretivas que estão no form FrmPackVirtual (using) 
1. Adicionar a diretiva `using System.ComponentModel.Design.Serialization;`
1. Adicionar o código abaixo em todos os eventos do form FrmPackVirtual (load, close,keypress) 
`if (this.DesignMode) { return; }`
1. Adicionar no FrmDescontosAtacado a Herança para o form FrmPackVirtual assim como fazermos para utilizar o formulário de pesquisa FrmPesquisa
1. Fechar o Visual Studio ir no diretorio do projeto, excluir a pasta bin e obj. Abrir novamente o visual studio, dar um clear na solution e um rebuild. Nesse momento o Design do pack já deve estar funcionando no Desconto Atacado
1. Corrigir eventuais erros que podem aparecer. 
1. Se alterarmos a tela do pack, podemos ter que repetir alguns dos passos acima. 

## Tarefa 8 Personalizar a tela Descontos para Atacado
1. Ao abrir o formulário FrmDescontosParaAtacado realizar as alterações abaixo editando a tela do Pack Virtual
1. Mudar o caption do form para “Descontos para atacado”
1. Mudar o label “Cód. Pack” para “Cód. Desconto” e “Descrição do Pack” para “Descrição do Desconto” (nas 2 abas)
1. Mudar o label “Modelo do Pack” para “Modelo de Desconto” (nas 2 abas)
1. Deixar invisível os check boxes Encerrados, Em andamento e Próximos. Deixar sempre marcado o checkbox “Em andamento”
1. Deixar invisível o botão “Prorrogar”
1. Na aba de cadastro deixar todos os campos e labels referentes a encarte invisíveis 
1. Na aba Cadastro deixar a data início e a data fim invisíveis, setar a data fim com a data '06/06/2079' data limite do campo smalldatetime

## Tarefa 9: Carregar o modelo de Desconto no combo

Passar para public virtual o método no FrmPackVitual conforme abaixo
`public virtual void CarregarComboModeloPack`
No FrmDescontosParaAtacado criar o método abaixo para sobrescrever ele
`public override void CarregarComboModeloPack`
Nesse método carregar o combo de modelos apenas com a opção 
`A partir de X unidades ganhe X % de desconto`
Utilizar override para modificar as próximas alterações necessárias.

## Tarefa 10: Carregar o layout de acordo com o modelo de desconto 
No combo do Pack Virtual ao selecionarmos um modelo os objetos são posicionados, vamos realizar algumas alterações no ReposicionarObjetos para poder sobrescrever o método

1. Criar o método `public virtual ReposicionarObjetos` no FrmPackVirtual, colocar nele todo o código que tem após o switch do método  cboModeloPack_SelectedIndexChanged
1. Chamar o método `ReposicionarObjetos` no cboModeloPack_SelectedIndexChanged, no local do código retirado.
1. No FrmDescontosParaAtacado criar o método `override ReposicionarObjetos`para sobrescrever o ReposicionarObjetos colocando apenas um tratamento para o novo modelo no switch, carregar os objetos da mesma forma que no pack `Pague x porcento a menos a partir de x unidades (atacado)`

## Tarefa 11: Adicionar produto no grid de produtos
1. Criar o método `btnAddGrupo1_Click` em FrmDescontosParaAtacado para sobrescrever o método `FrmPackVirtual.btnAddGrupo1_Click`
Copiar código o método  `FrmPackVirtual.btnAddGrupo1_Click` retirando a variável `valorDiferente` e a pergunta `"O produto informado tem valor diferente dos demais produtos da lista.`
1. Criar o método `CarregarDgvGrupo1` em FrmDescontosParaAtacado para sobrescrever o método `FrmPackVirtual.CarregarDgvGrupo1`. Copiar o mesmo código mas trocar `"Pague x porcento a menos a partir de x unidades (atacado)"`
por `A partir de X unidades ganhe X % de desconto`

## Tarefa 12: Alterar mensagens para não falar em pack virtual
1. Ao salvar alterar as mensagens abaixo
`Msg.Informar("Pack Virtual cadastrado com sucesso!");`
`Msg.Informar("Pack Virtual alterado com sucesso!");`
para 
`Msg.Informar("Registro cadastrado com sucesso!");`
`Msg.Informar("Registro alterado com sucesso!");`
1. Sobrescrever o método FrmPackVirtual.ptbFormaPagamento_Click substituindo a mensagem. Trocar `Para formar um Pack Virtual por forma de pagamento` por `Para formar um Desconto Para Atacado por forma de pagamento`
1. Sobrescrever o `FrmPackVirtual.ptbQuestao_Click`, colocar um if apenas para o nosso modelo com o texto 
`Modelo utilizado para preços de atacado. Comprando a partir de x unidades de determinado código de barras o valor do item terá desconto conforme o cofigurado.
“Exemplo: Compre mais de 11 unidades de 1 (um) dos produtos cadastrados e receba 10% de desconto em cada unidade (apenas neste item). ”`

## Tarefa 13: Salvar Modelo de desconto. 
1. Alterar o FrmPackVirtual.btnSalvar_Click. Criar um procedimento para validar os campos, e colocar todas as validações que estão antes do salvar nesse procedimento. No FrmDescontosParaAtacado criar um método para sobreescrever esse método criado, Nele colocar apenas as validações necessárias que são as mesmas usadas no pack `"Pague x porcento a menos a partir de x unidades (atacado)"` e para os campos de arredondamento. 
1. Sobrescrever o método `private Pack.PackVirtual RetornarPackVirtual()` Retirar o código referente ao tischer, referente ao chkLimitarQntPack e referente ao txtCodEncarte. Setar o packVirtual.ModeloPack = 13
1. Verificar se com estes passos já está salvando, corrigir enventuais problemas restantes para obter o resultado abaixo.
##### Resultado:
* após gravar um modelo de desconto, devemos ter o cabeçalho na tabela Pack Virtual com o modelo 13.
* os campos `TipoAjusteValor` e `AjusteUltimaCasaDecimal` salvos corretamente
* os produtos salvos na tabela PackVirtualGrupo1 
* `PackVirtualFormasPgto`, `PackVirtualGrupoClientes` e `PackVirtualLojas` refletindo as configurações selecionadas

## Tarefa 14: Atualizar Grade de pesquisa
1. Sobrescrever o método FrmPackVirtual.FormatarDataGridViewPackFiltro, deixar oculto ou com Width = 0 as colunas `DtFinal` e `QuantidadeLimite`
1. Sobrescrever o método FrmPackVirtual.CarregarDgvGrupo1 tirando os 2 últimos ifs, pois os elses não serão necessários, somente o código referente ao modelo `"Pague x porcento a menos a partir de x unidades (atacado)"`

## Tarefa 15: Tratar inserção de produtos que já fazem parte de outro modelo de desconto ou pack vitual

1. Nos métodos `VerificarProdutoPackVirtualExistentePassado` e `txtCodProdGrupo1_KeyDown` possuem uma mensagem que impede a inserção de um produto caso ele já faça parte de outra promoção.

``` C
Msg.Criticar("O produto código " + dgvGrupo1[i, (int)ColunasDgvGrupo.Codigo] +
" não pode ser " + status + ", pois já encontra-se vinculado ao Pack código " +
```                                         
Criar em FrmPackVirtual o procedimento CriticarProdutoJaUtilizado passando o objeto packvirtual por parâmetro e com essa mensagem e substituir no lugar destas mensagens.

No FrmDescontosAtacado Sobrescrever o método CriticarProdutoJaUtilizado. Montar uma mensagem como na descrita abaixo e tratar para mostrar os botões sim/não com foco default no não. 
Caso o cliente selecione sim, tratar para excluir o produto do outro pack e adicionar neste. 

```O produto código XXXX já encontra-se vinculado ao modelo de desconto abaixo.

Código XXX
Descrição: XXXXXXXXXXX
Modelo: Pack Virtual: (NomeDoPack) ou Desconto Para Atacado: (NomeDoModelo)
Dt. Inicio (Caso pack virtual)
Dt. Fim (Caso pack virtual)

Deseja excluir este produto do outro Pack Virtual/Desconto Para Atacado e inserir neste modelo?

Atenção: Esta ação não poderá ser desfeita!
```

Tratar a exclusão do produto caso o usuário concorde.

## Tarefa 16: Impedir adicionar produtos associados








