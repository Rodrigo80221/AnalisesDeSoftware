Épico:
Problema a ser resolvido
Pontos chave da história
Impactos
Resultado Final


# Épico: Campo Origem nos Preços Alterados
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

## Solução

1- Criar o campo origem na tela de preços alterados, assim podemos filtrar pela origem que gerou a alteração de preço.
2- Sabendo a origem podemos bloquear a remoção de preços alterados originado por promoções

## Tarefa 1: Criar verifica banco

Resumo: Criar tabela OrigemPrecosAlterados no banco de dados pelo verifica banco com os dados abaixo inseridos. 

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

## Tarefa 2: Criar enum público eOrigemPrecosAlterados

Resumo: Criação de código para controle. Tarefa apenas de programação.

1. Criar enum no mdlGestao junto com as outras variáveis globais
	Criar igual a tabela OrigemPrecosAlterados na formatação CamelCase
	ex:
	Promoções = 1
	CadastroDeProdutos = 2


## Tarefa 3: Criar combobox Origem:

Resumo: Inserir mais um fitro na tela de preços alterados. Filtro origem, mantendo a mesma funcionalidade dos outros combos.

obs: Colocar a tela na resolução 1024 para os ajustes

1. Criar o combobox "Origem:"
	- Popular o combobox com os dados da tabela OrigemPrecosAlterados + opção "Todos"
	- programar para filtrar de acordo com o combobox assim como nos outros combos da tela
	- Após programado verificar com a análise para definirmos o melhor local para inserir o combo
	- Acertar tab index

# Tarefa 4: Pintar células de Promoção

Resumo: Ao listar os resgistros nos preços alterados o usuário deverá rapidamente identificar os registros de promoção que estarão com a coloração verde.
A coloração deverá ficar no menor valor, que é o da promoção. Deverá também ser atualizado o tooltip informando que o destaque é devido a uma promoção.

1. Pintar a célula de verde (utilizar o mesmo verde do cadastro de produtos)
	- se o registro é de promoção é o preço alterado > preço anterior pintar a célula preço alterado
	- se o registro é de promoção é o preço anterior > preço alterado pintar a célula preço anterior
	ou seja, colorir o preço menor... 

1. caso a cor da célula seja verde concatenar ao tooltip existente "- Registro de Promoção"


## Tarefa 5: Excluir procedimentos que não são mais utilizados e estão incorretos

Resumo: Remoção de códigos obsoletos

1. Exluir o procedimento frmCadEncartes.sAtualizaPromocoesRelacionadas
1. Exluir o procedimento  frmCadProdutos.fInserePromocao


## Tarefa 6: Alterar o procedimento frmCadFornecedores.sGravaPrecoTabela

Resumo: Ao alterar o valor de um produto pelo preço de tabela no cadastro de fornecedores deverá realizar um registro com a origem correta nos preços alterados
Recurso utilizado pelos clientes Grandini, Santana e Barbosa e Hoff

1. Alterar o insert para inserir a origem
	PrecosAlterados.CodOrigem = Cad. Fornecedores (utilizar eOrigemPrecosAlterados)

1. Testar a funcionalidade

## Tarefa 7: Alterar o procedimento frmCadGrupo.cmdReajuste_Click

Resumo: Ao alterar o valor de venda do produto utilizado o recurso de reagustar pelo cadastro de grupos deverá gerar registros em preços alterados respeitando a origem correta

1. Alterar o insert para inserir a origem
	PrecosAlterados.CodOrigem = Cad. Grupos (utilizar eOrigemPrecosAlterados)

1. Testar a funcionalidade


## Tarefa 8: Alterar o procedimento frmCadProdutos.sGravarPrecosAlteradosLojas

Resumo: Ao cadastrar um novo produto com valor de venda pelo cadastro de produto o registro em preços alterados deverá respeitar a origem correta

1. Alterar o insert para inserir a origem
	PrecosAlterados.CodOrigem = Cadastro de Produtos (utilizar eOrigemPrecosAlterados)

1. Testar a funcionalidade


## Tarefa 9: Alterar o procedimento frmCadProdutos.fAlterarPrecoVenda

Resumo: Ao alterar o preço de venda de um produto pelo cadastro de produtos deverá respeitar a origem correta
Ao alterar o preço de venda de um produto pelo controle de entradas deverá respeitar a origem correta

1. Criar um parâmetro do tipo eOrigemPrecosAlterados
	- Alterar as duas chamadas (cad. produtos e Controle de Entradas) para passar o parâmetro
	- Alterar o insert para inserir a origem de acordo com o parâmetro (utilizar eOrigemPrecosAlterados)
	
1. Testar a funcionalidade no cadastro de produtos e controle de entradas


## Tarefa 10: Alterar o procedimento frmCadProdutos.sEnviarRegistroImediata

Resumo: Ao enviar imediata para o sistema KW deverá respeitar a origem do cadastro de produtos 
A equipe de testes não poderá testar esse recurso, acho que não temos mais clientes KW, validar...

1. Alterar o insert para inserir a origem
	PrecosAlterados.CodOrigem = Cad. de Produtos (utilizar eOrigemPrecosAlterados)

1. Testar a funcionalidade (forçar para entrar no procedimento pelo imadiate do vb para testar)




## Tarefa 11: Alterar o procedimento frmListaAssociados.fAlteraCadProdutosFornecedor

Resumo: No cadastro de fornecedores ao alterar o preço de tabela de um produto que tem produtos associados, estes deverão tbm respeitar a origem no registro de preços alterados.

1. Alterar o insert para inserir a origem

		Se o form frmCadFornecedor estiver carregado (Funcoes.fFormCarregado(VB.Forms, "frmCadFornecedores") = True) PrecosAlterados.CodOrigem = Cad. Fonecedor (utilizar eOrigemPrecosAlterados)
		Caso contrário PrecosAlterados.CodOrigem = Cad. Produtos (utilizar eOrigemPrecosAlterados)

1. Testar a funcionalidade para ver se irá funcionar o código sugerido


## Tarefa 12: Alterar o procedimento frmListaAssociados.sAlteraAssociadosControleEntrada

Resumo: Ao alterar o valor de venda no controle de entradas que tem produtos associados, estes deverão tbm respeitar a origem no registro de preços alterados.

1. Alterar o insert para inserir a origem
PrecosAlterados.CodOrigem = Controle de Entradas (utilizar eOrigemPrecosAlterados)

1. Testar a funcionalidade


## Tarefa 13: Alterar os procedimentos sVerificaInicioPromocao e sVerificaFimPromocao no mdiPrincipal

Resumo: Ao criar promoções deverá respeitar a origem na tela preços alterados.

1. Alterar o insert dos 2 procedimentos para inserir a origemPrecosAlterados.CodOrigem = Promoção (utilizar eOrigemPrecosAlterados)

1. Testar a funcionalidade criando promoções



## Tarefa 14: Ajustar operador das Fórumas promoções + Correção de Bug

obs: Tarefa com maior complexidade do épico

Resumo:
Alguns produtos possuem fórmula no valor de venda, então ao alterar o valor de venda de um filho o pai é atualizado automaticamente. Nesta atualização é gerado um registro de preço alterado.
Atualmente este registro fica no operador Telecon. A alteração deverá deixar o operador que realizou a alteração de preço 

Atualmente possuimos um bug: 
Atualmente ao criarmos uma promoção para um produto deverá ou não atualizar o preço dos pais, dependendo da configuração `Considerar Promoções` que está nas fórumas dos pais no cadastro de produto.
Atualmente está com diferença entre as lojas, se eu crio promoção para um produto filho de fórmula para a loja que estou logado funciona, se crio para outras lojas não cria a alteração de preço dos pais

1. Alterar a procedure SP_VINCULA_PRECO_VENDA_ITENS_FORMULA
	- colocar mais 1 parametro CodOperador int
	- no insert em PRECOS_ALTERADOS (online) inserir com CD_OPERADOR_ALTERACAO = CodOperador e CD_OPERADOR_CARGA = CodOperador
	- no insert em PRECOS_ALTERADOS (offline) inserir com CD_OPERADOR_ALTERACAO = CodOperador

1. Alterar os inserts da procedure SP_VINCULA_PRECO_VENDA_ITENS_FORMULA para colocar a origem: Fórmulas produtos
	- Retirar o código `EXEC SP_VINCULA_PRECO_VENDA_ITENS_FORMULA @CodProduto , @CodLoja` da trigger [TG_UPD_ProdutoLojas]
	- Passar o código para a trigger [TG_INS_PRECOS_ALTERADOS] após o update em produtoLojas
	- Criar uma variável para receber o valor de DBO.FN_ObterValorProdutoLoja(@CodProduto, @CodLoja) para passar para o update e para a procedure não chamando essa função 2 vezes

1. Testes
	- testar em ambiente nomonoloja online
	- testar ambiente nomonoloja offline
	- testar ambiente multiloja online
	- testar ambiente multiloja offline
	- Criar promoções para produtos com fórmula, deverá atualizar o preço dos pais corretamente em todas as lojas, de acordo com a configuração `Considerar Promoções`


##  Tarefa 15: Testar arquivo morto na seção referente aos preços alterados

1. Testar o envio de registros dos preços alterados para o arquivo morto. 
O arquivo morto está sendo compatibilizado novamente pelo Potter, não podemos deixar essa melhoria quebrar novamente o recurso.


## Tarefa 16: Tratar o procedimento frmPrecosAlterados.cmdRemover_Click (bloquear exclusão)

Resumo: Essa é a principal tarefa do Épico. Todas alterações foram criadas por esse motivo.
Não podemos dar nenhuma possibilidade para que o usuário consiga excluir um registro de preços alterados que tenha origem de promoções, sendo na criação da promoção ou no encerramento.
Lembrando que o funcionando pode variar dependendo da configuração online e offline. 

1. Adicionar mais uma coluna oculta no grid de preços alterados.
	- Criar a coluna origem e inserir o código da origem
	- Carregar a nova coluna junto com as outras 

1. Tratar o procedimento frmPrecosAlterados.cmdRemover_Click
	- No procedimento testar em cada registro que será excluído se o código de origem é igual ao Enum de Promoções, caso seja mostrar a mensagem abaixo.
	```
		Foram selecionados registros de promoção que não podem ser excluídos!
		Para remover essas alterações de preço utilize a tela de Promoções e encerre a promoção desejada.
	```
	- Neste caso não remover a alteração de preço.


1. Fazer testes validando que não é possível remover um registro de promoção de nenhuma forma.



## Tarefa 17: Tratar o procedimento frmPrecosAlterados.cmdRemover_Click (2 pcs ao mesmo tempo)

Resumo: 
Obs: O botão de remover da tela de preços alterados só permite excluir registros que ainda não foram enviado carga (offline). Mas é possível exluir se utilizarmos 2 telas ao mesmo tempo.

Como gerar o bug de vulnerabilidade:
Se abrirmos o controle de entradas em 2 computadores, listar nos 2, enviar carga em 1 e no outro remover os registros, é possível
remover registros que já tiveram a carga enviada. Isso nunca pode acontecer!

Tarefa: ao remover um registro de preço alterado acessar ele no banco e testar novamente com um select se o registro já não teve a carga enviada



## Tarefa 18: Tratar o procedimento frmPrecosAlterados.cmdRemover_Click (atu cad produtos)

Resumo: Atualmente na tela de preços alterados ao enviar carga se o cadastro de produtos estiver aberto ele já será atualizado com o novo preço, não acontecendo de ficar com a tela aberta em um registro desatualizado.
O mesmo não corre no botão remover.

1. Ao remover atualizar o cadastro de produtos utilizando o código abaixo
	```vb
	If Funcoes.fFormCarregado(Forms, "frmCadProdutos") Then frmCadProdutos.cadProdutos.sRenovar
	```



## Tarefa 19: Criar Atalho na tela de Promoções

Resumo: Adicionar um atalho na tela de promoções para abrir a tela de preços alterados exibindo apenas os registros com origem: Promoções.

1. Inserir um botão "Preços Alterados" na tela de promoções antes do botão "Prorrogar"
	- Aumentar o width do formulário para no máximo da resolução 1024 x 768, já aumentar um pouco o Height 
	- Ajustar o tab index dos botões após ter inserido o novo botão
	- Ajustar os outros objetos na tela para se adequarem a nova resolução (aumentar o width de cada um)

1. Ao clicar no botão "Preços Alterados" abrir a tela de preços alterados 
	- Abrir posicionando o combo na origem: Promoções
	- Utilizar o código semelhante ao do procedimento `frmControleEntradas3.cmdPrecosAlterados_Click`



## Tarefa 20: Testar relatório Mapa Alteração de Preço Custo

Resumo: Este relatório utiliza a tabela de preços alterados e pode ter algum impacto.

1. Verificar se o relatório está listando os registros e mostrando corretamente os novos preços alterados.
















verificar problema dos registros que aparecem parcelados 





está gerando preços alterados sempre, para produtos em promoção 
não está excluindo preços alterados não enviados 
"frmCadGrupo", Err, "cmdReajuste_Click"



# Avaliar
## Alterar o procedimento frmCadFornecedores.sGravaPrecoTabela
Alterar o insert para inserir a origem
PrecosAlterados.CodOrigem = 6- Cad. Fornecedores (utilizar eOrigemPrecosAlterados)

Testar a funcionalidade

Provavelmente temos um bug aqui, não está avaliando se o produto tem promoção ativa, neste caso iria tirar o produto da promoção


# Avaliar
## Alterar o procedimento frmCadGrupo.cmdReajuste_Click
Alterar o insert para inserir a origem
PrecosAlterados.CodOrigem = 7- Cad. Grupos (utilizar eOrigemPrecosAlterados)

Testar a funcionalidade

Provavelmente temos um bug aqui, não está avaliando se o produto tem promoção ativa, neste caso iria tirar o produto da promoção



# Tarefa 1: Corrigir problema do usuário das Promoções, acaba mudando o usuário
verificar o forçar das promoções

Verifiquei os registros de preços alterados originados das promoções mas está tudo ok com relação ao operador da carga
não encontrei nenhum erro

olhei a questão de forçar das promoções e está ok
vi que esse forçar está sendo feito em 2 lugares , no login automático, ao alterar o preço de qualquer produto, e nesse atualizar das promoções

a única coisa que vi que poderia ter acontecido, é que algúem gerou uma promoção com a versão mais antiga e ficou null no operador da promoção
dai se está null ao criar os preço alterado é colocado o usuário logado



