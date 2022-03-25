Épico:
Problema a ser resolvido
Pontos chave da história
Impactos
Resultado Final


# Épico: Campo Origem na tela Preços Alterados
Data de início da análise: 22/03/2022
## Problema a ser resolvido

Bug: O usuário pode remover registros de promoções na tela de preços alterados, fazendo com que o preço de nunca entre ou saia da promoção

Bug: O usuário consegue remover um registro de preço alterados mesmo que já tenha sido enviado carga, deixando registros listados na tela por muito tempo

Melhoria: Possibilitar que os usuários selecionem primeiramente alterações de preços de promoções para enviar na primeira hora do dia

## Impactos

1- Promoções
2- Cadastro de Produtos
3- Controle de Entradas
4- Fórmulas dos Produtos
5- Cadastro de Grupos
6- Cadastro de Fornecedores

## Pré Requisitos

Nenhum
## Solução

1- Criar o campo origem na tela de preços alterados, assim podemos filtrar pela origem que gerou a alteração de preço.
2- Sabendo a origem podemos bloquear a remoção de preços alterados originado por promoções

## Tarefa 1: Criar verifica banco

1. criar tabela OrigemPrecosAlterados e coluna CodOrigem na tabela preços alterados 
	- Criar tabela OrigemPrecosAlterados
	- Utilizar Funcoes.fExisteObjeto

	``` SQL

	CREATE TABLE [dbo].[OrigemPrecosAlterados](
		[Codigo] [Tinyint] NOT NULL,
		[Descricao] [nvarchar](50) NOT NULL,
	CONSTRAINT [PK_OrigemPrecosAlterados] PRIMARY KEY CLUSTERED 
	(
		[Codigo] ASC
	))

	```

 1. Inserir nesta tabela os registros abaixo

	1- Promoções
	2- Cadastro de Produtos
	3- Controle de Entradas
	4- Fórmulas dos Produtos
	5- Cadastro de Grupos
	6- Cadastro de Fornecedores


1. Criar campo CodOrigem, tratar utilizando fExisteCampo
	ALTER TABLE PRECOS_ALTERADOS ADD CodOrigem Tinyint null

	criar também no arquivo morto, tratar utilizando fExisteCampo
	ALTER TABLE PRECOS_ALTERADOS_ARQ_MORTO ADD CodOrigem Tinyint null

## Tarefa 1: Criar enum público eOrigemPrecosAlterados
1. Criar enum no mdlGestao junto com as outras variáveis globais
	Criar igual a tabela OrigemPrecosAlterados na formatação CamelCase
	ex:
	Promoções = 1
	CadastroDeProdutos = 2
...


						## Colocar coluna origem na tela preços alterados 
						obs: Colocar a tela na resolução 1024 para os ajustes

						1. Redimencionar colunas
							- Retirar o caption "Selecionar" da primeira coluna do grid e diminuir essa coluna do checkbox 
							- Ajustar colunas de datas para a menor largura possível
							- Diminuir um pouco as colunas de preço
							Após programado validar com a análise a melhor largura para as colunas

						Programar para atualizar a tela com as informações da nova coluna

## Tarefa 1: Criar combobox Origem:
obs: Colocar a tela na resolução 1024 para os ajustes

1. Criar o combobox "Origem:"
	- Popular o combobox com os dados da tabela OrigemPrecosAlterados + opção "Todos"
	- programar para filtrar de acordo com o combobox assim como nos outros combos da tela
	- Após programado verificar com a análise para definirmos o melhor local para inserir o combo


# Tarefa 1: Pintar células de Promoção

1. Pintar a célula de verde (utilizar o mesmo verde do cadastro de produtos)
	- se o registro é de promoção é o preço alterado > preço anterior pintar a célula preço alterado
	- se o registro é de promoção é o preço anterior > preço alterado pintar a célula preço anterior
	ou seja, colorir o preço menor... 

1. caso a cor da célula seja verde concatenar ao tooltip existente "- Registro de Promoção"


## Tarefa 1: Excluir procedimentos que não são mais utilizados e estão incorretos
1. Exluir o procedimento frmCadEncartes.sAtualizaPromocoesRelacionadas
1. Exluir o procedimento  frmCadProdutos.fInserePromocao


## Tarefa 1: Alterar o procedimento frmCadFornecedores.sGravaPrecoTabela

1. Alterar o insert para inserir a origem
	PrecosAlterados.CodOrigem = Cad. Fornecedores (utilizar eOrigemPrecosAlterados)

1. Testar a funcinalidade

## Tarefa 1: Alterar o procedimento frmCadGrupo.cmdReajuste_Click
1. Alterar o insert para inserir a origem
	PrecosAlterados.CodOrigem = Cad. Grupos (utilizar eOrigemPrecosAlterados)

1. Testar a funcinalidade


## Tarefa 1: Alterar o procedimento frmCadProdutos.sGravarPrecosAlteradosLojas

1. Alterar o insert para inserir a origem
	PrecosAlterados.CodOrigem = Cadastro de Produtos (utilizar eOrigemPrecosAlterados)

1. Testar a funcinalidade


## Tarefa 1: Alterar o procedimento frmCadProdutos.fAlterarPrecoVenda

1. Criar um parâmetro do tipo eOrigemPrecosAlterados
	- Alterar as duas chamadas (cad. produtos e Controle de Entradas) para passar o parâmetro
	- Alterar o insert para inserir a origem de acordo com o parâmetro (utilizar eOrigemPrecosAlterados)
	
1. Testar a funcinalidade no cadastro de produtos e controle de entradas


## Tarefa 1: Alterar o procedimento frmCadProdutos.sEnviarRegistroImediata

1. Alterar o insert para inserir a origem
	PrecosAlterados.CodOrigem = Cad. de Produtos (utilizar eOrigemPrecosAlterados)

1. Testar a funcinalidade (forçar para entrar no procedimento pelo imadiate do vb para testar)

1. A equipe de testes não poderá testar esse recurso, acho que não temos mais clientes KW, validar...












## Alterar o procedimento frmListaAssociados.fAlteraCadProdutosFornecedor

Alterar o insert para inserir a origem
PrecosAlterados.CodOrigem = 2- Cad. de Produtos (utilizar eOrigemPrecosAlterados)

Testar a funcinalidade



## Alterar o procedimento frmListaAssociados.sAlteraAssociadosControleEntrada

Alterar o insert para inserir a origem
PrecosAlterados.CodOrigem = 3- Controle de Entradas (utilizar eOrigemPrecosAlterados)

Testar a funcinalidade


## Alterar os procedimentos sVerificaInicioPromocao e sVerificaFimPromocao no mdiPrincipal

Alterar o insert dos 2 procedimentos para inserir a origem
PrecosAlterados.CodOrigem = 5- Promoção (utilizar eOrigemPrecosAlterados)

Testar a funcinalidade criando promoções


# Corrigir problema do usuário das Promoções, acaba mudando o usuário
verificar o forçar das promoções

Verifiquei os registros de preços alterados originados das promoções mas está tudo ok com relação ao operador da carga
não encontrei nenhum erro

olhei a questão de forçar das promoções e está ok
vi que esse forçar está sendo feito em 2 lugares , no login automático, ao alterar o preço de qualquer produto, e nesse atualizar das promoções

a única coisa que vi que poderia ter acontecido, é que algúem gerou uma promoção com a versão mais antiga e ficou null no operador da promoção
dai se está null ao criar os preço alterado é colocado o usuário logado



# Ver filtro de encartes do serginho do copetti

 # Ajustar usuário das Fórumas promoções

alterar a procedure SP_VINCULA_PRECO_VENDA_ITENS_FORMULA
colocar mais 1 parametro CodOperador int

no insert em PRECOS_ALTERADOS (online) inserir com CD_OPERADOR_ALTERACAO = CodOperador e CD_OPERADOR_CARGA = CodOperador

no insert em PRECOS_ALTERADOS (offline) inserir com CD_OPERADOR_ALTERACAO = CodOperador

Alterar os inserts para colocarem origem de Fórmulas produtos

Retirar o código `EXEC SP_VINCULA_PRECO_VENDA_ITENS_FORMULA @CodProduto , @CodLoja`
da trigger [TG_UPD_ProdutoLojas]

Passar o código para a trigger [TG_INS_PRECOS_ALTERADOS] após o update em produtoLojas
Criar uma variável para receber o valor de DBO.FN_ObterValorProdutoLoja(@CodProduto, @CodLoja) para passar para o update e para a procedure não chamando essa função 2 vezes

testar nomonoloja online
testar nomonoloja offline

testar multiloja online
testar multiloja offline


## Testar arquivo morto na seção referente aos preços alterados



## Tratar o procedimento frmPrecosAlterados.cmdRemover_Click
não possibilitar excluir produtos de origem da promoção, mostrar a mensagem abaixo

O registro de preço alterado do produto 9999 XXXX tem a origem "Promoções" e não poderá ser removido.

Fazer testes validando que não é possível remover um produto de promoção 



## Tratar o procedimento frmPrecosAlterados.cmdRemover_Click
Resolver o bug de vulnerabilidade abaixo
Se abrirmos o controle de entradas em 2 computadores, listar nos 2, enviar carga em 1 e no outro remover os registros, é possível
remover registros que já tiveram a carga enviada. Isso nunca pode acontecer

Tarefa: ao excluir um registro ir no banco e testar novamente com um select se o registro já não teve a carga enviada



## Tratar o procedimento frmPrecosAlterados.cmdRemover_Click
atualizar o cadastro de produtos



verificar problema dos registros que aparecem parcelados 

testar relatorio mapa alteração preço custo





está gerando preços alterados sempre, para produtos em promoção 
não está excluindo preços alterados não enviados 
"frmCadGrupo", Err, "cmdReajuste_Click"





# Avaliar
## Alterar o procedimento frmCadFornecedores.sGravaPrecoTabela
Alterar o insert para inserir a origem
PrecosAlterados.CodOrigem = 6- Cad. Fornecedores (utilizar eOrigemPrecosAlterados)

Testar a funcinalidade

Provavelmente temos um bug aqui, não está avaliando se o produto tem promoção ativa, neste caso iria tirar o produto da promoção


# Avaliar
## Alterar o procedimento frmCadGrupo.cmdReajuste_Click
Alterar o insert para inserir a origem
PrecosAlterados.CodOrigem = 7- Cad. Grupos (utilizar eOrigemPrecosAlterados)

Testar a funcinalidade

Provavelmente temos um bug aqui, não está avaliando se o produto tem promoção ativa, neste caso iria tirar o produto da promoção