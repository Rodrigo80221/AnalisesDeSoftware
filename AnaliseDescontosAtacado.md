# Épico: Descontos para Atacado
Abril/2021

## Problema a ser resolvido

Relato do cliente:
*"No sistema Telecon não tem o pack virtual? então você vai criando vários packs não é isso,  no sistema softcom eu tenho o gestão de promoções e aqui tem os grupos , então você criava o grupo do feijão, no grupo do feijão todo o feijão a partir de 5 unidades dá um desconto de tantos por cento, então para você não criar um grupo, pra você não ir colocando de 1 por 1, você criava um grupo e dizia “Feijão” aí esses códigos de barras que estiverem nesse grupo a partir de 5 unidades vai ter 5% de desconto. A diferença desse para o Telecon é que se eu for pegar um de cada feijão e conseguir os 5 ele vai dar desconto no 5 , só que na verdade é para dar o desconto apenas em 1 , só vai dar o desconto se eu levar 5 de 1 código de barras"*

Atualmente o cliente Novo Preço possui muitos produtos no formato de atacado. Ex:
Se levar a partir de 6 produtos ganhe 10% de desconto

Para utilizar esse processo no nosso sistema o cliente foi instruído a adicionar um pack virtual com essa regra no modelo
`Pague x por cento a menos a partir de x unidades (atacado)`

Mas no nosso sistema se adicionar mais de um produto neste pack ele agrupa todos os produtos para a contagem do desconto. Ex:
Se criar a regra
`Leve a partir de 6 produtos e ganhe 10% de desconto`
e adicionar no pack 
`COCA COLA ZERO PET 2 LT UND`
`COCA COLA ZERO LATA 350ML UND`

![](https://github.com/Rodrigo80221/MARKDOWN/blob/main/Imagens/FrmPackVirtual.png?raw=true)

Se o cliente vender 1 `COCA COLA ZERO PET 2 LT UND` e 5 `COCA COLA ZERO LATA 350ML UND` ganhará 10% de desconto nos 2 itens. Isso causou prejuízo para o cliente inicialmente. Então ele teve que fazer um pack para cada produto e já tem mais de 3 mil packs cadastrados

Iremos criar uma nova tela específica para Descontos de Atacado. Esta nova tela deverá permitir o cliente cadastrar uma regra e adicionar vários produtos que terão o desconto de acordo com essa regra, mas cada produto será independente e não terá ligação com os outros produtos adicionados.
Ex:
Se criar a regra
`Leve a partir de 6 produtos e ganhe 10% de desconto`
e adicionar no pack 
`COCA COLA ZERO PET 2 LT UND`
`COCA COLA ZERO LATA 350ML UND`

O cliente terá que comprar 6 itens do produto `COCA COLA ZERO PET 2 LT UND` para receber o desconto de 10% e receberá apenas neste produto. Ou terá que comprar 6 itens do produto `COCA COLA ZERO LATA 350ML UND` para receber o desconto de 10% apenas neste produto.

## Pontos chave da história

1. Criar uma forma ágil de configurar uma % de desconto para os produtos de acordo com uma regra. O cliente quer poder configurar o desconto para um conjunto de muitos produtos, podendo inclusive adicionar um grupo inteiro.
1. Facilitar a impressão de etiquetas. Atualmente o cliente tem que mudar o modelo de etiqueta para cada tipo de produto. Um modelo para preço normal outro para o preço de atacado. No processo atual o cliente abre a tela de preços alterados e imprime as etiquetas, estas etiquetas devem ser impressas com layout de Atacado para os produtos com desconto percentual ou layout de varejo para os produtos sem preço de atacado.
1. Alterar o cupom para mostrar os descontos de atacado assim como no seu concorrente principal.
1. Após a implementação validar com a Gabriela. Ela está acompanhando as dificuldades do cliente.

## Impactos

1. Na etapa 1 iremos programar de forma a termos o menor impacto possível. A nova tela irá especializar a tela de Pack Virtual buscando não impactar seu funcionamento.
1. Os arredondamentos para a Nova funcionalidade devem funcionar da mesma maneira que no modelo de pack virtual abaixo.
`Pague x por cento a menos a partir de x unidades (atacado)`
1. Não podemos adicionar um modelo de desconto para um produto que já possui pack virtual. Devemos dar a opção para o usuário excluir o produto da outra regra anterior. 
1. Teremos um impacto relativamente alto na tela de impressão de etiquetas, fundamental na rotina do cliente.
1. Teremos impacto na impressão de cupom, com as novas informações de desconto para atacado.

# Tarefas

## Tarefa 1: Criar branch no git
Criar novo branch feature/EpicoDescontosParaAtacado


 ## Tarefa 2: Corrigir layout na tela Pack Virtual

1. Corrigir Label
    Ao dar um novo e selecionar o modelo 
    `Pague x porcento a menos a partir de x unidades (atacado)`

    Alterar o texto do label da regra substituindo
    `% de desconto nestes outros produtos` 
    por 
    `% de desconto por unidade nestes produtos`

    o _"outros"_ deve ser usado apenas nos packs que tem 2 grids
1. Corrigir tamanho do combo
    Aumentar o combo de modelo, pois está cortando algumas descrições no pack virtual
1. Corrigir o tamanho da fonte do label lblMensagemPrecoDiferente para 8 pts


 ## Tarefa 3: Alterar posição do frame de Limite da Regra do Pack Virtual

1. Alterar a posição do frame gbxLimitePack, colocar ele para a direita assim como o frame de arredondamentos, tentar colocar ele acima do frame para não sobrepor e para caber os dois.
Colocar o text e o label de limite ao lado direito do checkbox.
O label `Limitar a quantidade de` pode ser excluído
O label `produtos desse Pack Virtual por venda.` por `Limite deste Pack Virtual por Venda`


## Tarefa 4: Possibilitar maximizar a tela Pack Virtual
Objetivo: Poder maximizar a tela para aumentar a grade de produtos, o cliente irá inserir mais de mil produtos nesta grade e atualmente ela é pequena.
1. Habilitar o botão de maximizar a tela
1. Ao maximizar a tela ancorar o label lblMensagemPrecoDiferente e os botões de imprimir etiqueta, cancelar e salvar na parte inferior do formulário. Ancorar da mesma forma os objetos da aba de pesquisa.
1. Ajustar os dois grids de produtos de forma que fiquem ancorados na parte inferior do formulário.

## Tarefa 5: Colocar um contador de registros para o grid de produtos
1. Colocar um label contador de registros para o grid dgvGrupo1. Ao lado do label lblMensagemPrecoDiferente com o mesmo padrão de funcionamento do label lblTotalRegistros. Controlá lo nos diversos estados da tela. 


## Tarefa 6: Criar módulo para Gerenciar o recurso Descontos para Atacado

1. Criar verifica banco para inserir módulo. Se o cliente não utiliza PDV o módulo não deve ser ativado. 
Utilizar:
`fVerificarSeUsaPDV()`
`sInserirModulo`
`sInserirModuloSistemasRS`
Executar update em operadores_modulos para ativar o módulo
Descrição das variáveis:
`sGrupo = "PRODUTOS"`
`sDescrição = "Descontos para Atacado`
`sPalavraChave = FrmDescontosAtacado`
1. Tratar em mdlGestao.sCarregaModulos. A tela deverá ficar habilitada no gestão e no sistema S confome está a tela do pack virtual no cliente
    * Chamar na tela clássica no menu de PRODUTOS > Descontos para Atacado.  _Se possível posicionar abaixo do Pack Virtual_
    * Tratar no recurso de software TI do sistema S para habilitar para quem está com o pack virtual habilitado.
1. Tratar no frmModulos.cmdRestaurar_Click conforme padrão do procedimento

## Tarefa 7: Criar formulário C# FrmDescontosParaAtacado

Objetivo: A nova tela deverá herdar a tela pack virtual, a ideía é reaproveitar o código que já tem na tela do pack, lojas, grupo de clientes, formas de pagamento, configurações de arredondamento entre outros. E criar uma personalização da tela mas sem alterar a tela de pack virtual. 
1. Criar diretório e formulário no gestão c#. `DescontosAtacado > FrmDescontosParaAtacado`
1. Adicionar todas as diretivas que estão no form FrmPackVirtual (using) 
1. Adicionar a diretiva `using System.ComponentModel.Design.Serialization;`
1. Adicionar o código abaixo em todos os eventos do form FrmPackVirtual (load, close,keypress) 
`if (this.DesignMode) { return; }`
1. Adicionar no FrmDescontosAtacado a Herança para o form FrmPackVirtual assim como fazermos para utilizar o formulário de pesquisa FrmPesquisa
1. Fechar o Visual Studio ir no diretorio do projeto, excluir o diretório bin e obj. Abrir novamente o visual studio, dar um clear na solution e um rebuild. Nesse momento o Design do pack já deve estar funcionando no novo form
1. Corrigir eventuais erros que possam aparecer. 
1. Se alterarmos a tela do pack, podemos ter que repetir alguns dos passos acima. 

## Tarefa 8: Personalizar a tela Descontos para Atacado
1. Ao abrir o formulário FrmDescontosParaAtacado realizar as alterações abaixo editando a tela do Pack Virtual
1. Mudar o caption do form para “Descontos para atacado”
1. Mudar o label “Cód. Pack” para “Cód. Desconto” e “Descrição do Pack” para “Descrição do Desconto” (nas 2 abas)
1. Mudar o label “Modelo do Pack” para “Modelo de Desconto” (nas 2 abas)
1. Deixar invisível os check boxes Encerrados, Em andamento e Próximos. Deixar sempre marcado o checkbox “Em andamento”
1. Deixar invisível o botão “Prorrogar”
1. Na aba de cadastro deixar todos os campos e labels referentes a encarte invisíveis 
1. Na aba Cadastro deixar a data início e a data fim invisíveis, setar a data fim com a data '06/06/2079' data limite do campo smalldatetime

## Tarefa 9: Carregar o modelo de Desconto no combo sobrescrevendo o método do pack virtual

Passar para public virtual o método no FrmPackVitual conforme abaixo
`public virtual void CarregarComboModeloPack`
No FrmDescontosParaAtacado criar o método abaixo para sobrescrever ele
`public override void CarregarComboModeloPack`
Nesse método carregar o combo de modelos apenas com a opção 
`A partir de X unidades ganhe X % de desconto`

Utilizar esta estrutura virtual/override para realizar as alterações necessárias nas próximas tarefas.

## Tarefa 10: Carregar o layout de acordo com o modelo de desconto 
No combo do Pack Virtual ao selecionarmos um modelo os objetos são reposicionados, vamos realizar algumas alterações no ReposicionarObjetos para poder sobrescrever o método

1. Criar o método `public virtual ReposicionarObjetos` no FrmPackVirtual, colocar nele todo o código que tem após o switch do método  cboModeloPack_SelectedIndexChanged
1. Chamar o método `ReposicionarObjetos` no cboModeloPack_SelectedIndexChanged, no local do código retirado.
1. No FrmDescontosParaAtacado criar o método `override ReposicionarObjetos`para sobrescrever o ReposicionarObjetos colocando apenas o tratamento para o novo modelo no switch, carregar os objetos da mesma forma que no pack `Pague x porcento a menos a partir de x unidades (atacado)`

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
“Exemplo: Compre a partir de 12 unidades de 1 (um) dos produtos cadastrados e receba 10% de desconto em cada unidade (apenas neste item). ”`

## Tarefa 13: Salvar Modelo de desconto. 
1. Alterar o FrmPackVirtual.btnSalvar_Click. Criar o método ValidarModelosDePacks, e colocar nele todas as validações de packs que estão neste procedimento. No FrmDescontosParaAtacado criar um método para sobreescrever esse método criado, Nele colocar apenas as validações necessárias para o novo modelo que são as mesmas usadas no pack `"Pague x porcento a menos a partir de x unidades (atacado)"` e para os campos de arredondamento. 
1. Sobrescrever o método `private Pack.PackVirtual RetornarPackVirtual()` Retirar o código referente ao tischer, referente ao chkLimitarQntPack e referente ao txtCodEncarte. Setar o packVirtual.ModeloPack = 13
1. Verificar se com estes passos já está salvando, corrigir enventuais problemas restantes para obter o resultado abaixo.
##### Resultado:
* após gravar um modelo de desconto, devemos ter o cabeçalho na tabela Pack Virtual com o modelo 13.
* os campos `TipoAjusteValor` e `AjusteUltimaCasaDecimal` salvos corretamente
* os produtos salvos na tabela PackVirtualGrupo1 
* `PackVirtualFormasPgto`, `PackVirtualGrupoClientes` e `PackVirtualLojas` refletindo as configurações selecionadas

## Tarefa 14: Atualizar Grade de pesquisa removendo colunas
1. Sobrescrever o método FrmPackVirtual.FormatarDataGridViewPackFiltro, deixar oculto ou com Width = 0 as colunas `DtFinal` e `QuantidadeLimite`
1. Sobrescrever o método FrmPackVirtual.CarregarDgvGrupo1 tirando os 2 últimos ifs, pois os elses não serão necessários, somente o código referente ao modelo `"Pague x porcento a menos a partir de x unidades (atacado)"`

## Tarefa 15: Tratar inserção de produtos que já fazem parte de outro modelo de desconto ou pack vitual

1. Nos métodos `VerificarProdutoPackVirtualExistentePassado` e `txtCodProdGrupo1_KeyDown` possuem uma mensagem que impede a inserção de um produto caso ele já faça parte de outra promoção.

``` C
Msg.Criticar("O produto código " + dgvGrupo1[i, (int)ColunasDgvGrupo.Codigo] +
" não pode ser " + status + ", pois já encontra-se vinculado ao Pack código " +
```                                         
Criar em FrmPackVirtual o procedimento `CriticarProdutoJaUtilizado` passando o objeto packvirtual por parâmetro. Colocar neste método a mensagem acima. Utilizar este método no lugar das mensagens.

No FrmDescontosAtacado Sobrescrever o método `CriticarProdutoJaUtilizado`. Montar uma mensagem como na descrita abaixo e tratar para mostrar os botões sim/não com foco default no não. 
Caso o cliente selecione sim, tratar para excluir o produto do outro pack e adicionar neste. 

```O produto XXXXX código XXXX já encontra-se vinculado ao modelo de desconto abaixo.

Código XXX
Descrição: XXXXXXXXXXX
Modelo: "Pack Virtual: (NomeDoPack) ou Desconto Para Atacado: (NomeDoModelo)"
Dt. Inicio (Caso pack virtual)
Dt. Fim (Caso pack virtual)

Deseja excluir este produto do outro Pack "(Virtual/Desconto Para Atacado)" e inserir neste modelo?

Atenção: Esta ação não poderá ser desfeita!
```

Tratar na mensagem acima para mostrar o texto pack virtual ou Desconto para atacado. Se for 13 é deconto para atacado.
Tratar a exclusão do produto caso o usuário concorde.

## Tarefa 16: Impedir adicionar produtos associados
Objetivo: Neste primeiro momento a nova funcionalidade não terá tratamento para produtos associados, então teremos que avisar o usuário e dar a ele outra alternativa.
1. no método `txtCodProdGrupo1_KeyDown` existe uma funcionalidade para automatizar a inserção de produtos associados. Não iremos adicionar produtos associados, retirar essa funcinalidade com o código igual ou similar o da __Tarefa 15__ 
1. Criar um tratamento sobrescrevendo a mensagem `Este produto possui produtos associados, deseja inserí-los também?` para verificar se o produto adicionado possui produtos associados cadastrados.
Caso haja teremos que informar o usuário com a mensagem abaixo. 

```
Atenção! O produto XXXXX possui produtos associados.
Os produtos associados a este item não serão inseridos automaticamente.

Neste modelo de Desconto Para Atacado não é possível adicionar os produtos
associados a este item, pois neste modelo cada código de barras é vendido separadamente.

Se desejar crie uma promoção Pack Virtual com o modelo 
"Pague x porcento a menos a partir de x unidades (atacado)" e adicione
este conjunto de produtos associados.
```
1. Quando o usuário cancelar o produto não é adicionado.

## Tarefa 17: Buscar Produto por grupo e inserir em massa 
Objetivo: Um dos requisitos da tela é uma forma de adicionar produtos em massa, e também poder selecionar por grupos.

1. Adicionar um botão acima do btnAddGrupo1 e do txtDesProdGrupo1 alinhado a direita
No texto dele colocar `Buscar Produtos por Grupo`. Se Necessário diminuir um pouco o grid.
1. Ao clicar neste botão iremos iniciar uma busca em 2 etapas, primeiro iremos abrir uma busca para selecionar o grupo, depois uma busca para selecionar os produtos.
1. Copiar o formulário de busca `FrmAnaliseMensaVendas>BuscaGrupo` para o diretório `DescontosAtacado`
1. Copiar o formulário de busca `FrmCadCompradoresExcecoesProdutos>BuscaProdutos` para o diretório `DescontosAtacado`
1. Implementar para mostrar os grupos como na tela abaixo
![](https://github.com/Rodrigo80221/MARKDOWN/blob/main/Imagens/FrmAnaliseMensaVendas_BuscaGrupo.png?raw=true)
1. Após implementar para selecionar os produtos do grupo selecionado e inserir em massa
![](https://github.com/Rodrigo80221/MARKDOWN/blob/main/Imagens/FrmCadCompradoresExcecoesProdutos_BuscaProdutos.png?raw=true)
1. Ao selecionar um grupo pai deverá carregar os produtos de todos os subgrupos
1. Ao inserir em massa deve adicionar os produtos no grid, o tratamento de associados deve permanacer bem como outros relevantes do adicionar. 


## Tarefa 18: Teste de integração
1. Testar a funcionalidade por completo incluindo a funcionalidade no PDV VB e no PDV Torus (Ver teste com o Fabrício). Corrigir eventuais falhas.
1. Testar com diversos tipos de produtos, associados, inativos, que já possem outro desconto para atacado, que já possuem outro pack virtual em vigência e fora de vigência
1. Testar o item anterior com outros packs em outras lojas
1. Testar o Desconto Para Atacado por grupos de clientes e formas de pagamento. 
1. Testar as configurações de arredondamento

## Tarefa 19: Implementar Descontos Para Atacado no PDV VB6
Após gravarmos um modelo de desconto, devemos ter o cabeçalho na tabela Pack Virtual com o ModeloPack = 13 e os produtos salvos na tabela PackVirtualGrupo1.

Esse modelo pack deve aplicar a regra como no exemplo abaixo para cada código de barras do cupom. 
Ex:
Se criar a regra
`Leve a partir de 6 produtos e ganhe 10% de desconto`
e adicionar no pack 
`COCA COLA ZERO PET 2 LT UND`
`COCA COLA ZERO LATA 350ML UND`

O cliente terá que comprar 6 itens do produto `COCA COLA ZERO PET 2 LT UND` para receber o desconto de 10% e receberá apenas neste produto. Ou terá que comprar 6 itens do produto `COCA COLA ZERO LATA 350ML UND` para receber o desconto de 10% apenas neste produto.

A tabela PackVirtualGrupo1 poderá ter vários produtos mas cada produto será independente e não terá ligação com os outros produtos adicionados.

Neste momento não teremos produtos associados, todos os produtos funcionarão da mesma forma. 

Devemos aplicar um teste de stress com muitos dados, o banco do cliente novo preço já possui a regra de quantidade `Leve a partir de 6 produtos e ganhe 10% de desconto` com mais de 1400 produtos

As regras de arredondamento deverão permanecer da mesma maneira que no pack virtual modelo `Pague x por cento a menos a partir de x unidades (atacado)`. O campos da tabela `PackVirtual.TipoAjusteValor` e `PackVirtual.AjusteUltimaCasaDecimal` estão sendo salvos da mesma forma.

As configurações de desconto por forma de pagamento, grupo de clientes e lojas estão implementadas e devem funcionar no mesmo formato que os outros modelos de Pack Virtual

### Detalhes para o PDV Torus

O cliente comentou que o seu concorrente Bonanza possui algumas informações no cupom que iremos implementar da mesma forma. 
1. Os produtos que recebem desconto ganham uma observação abaixo do item com o preço fora da promoção.
1. No final do cupom tem o totalizador Desconto Atacado.

