## Criar verifica banco

criar tabela OrigemPrecosAlterados e coluna CodOrigem na tabela preços alterados 

Criar tabela OrigemPrecosAlterados
Utilizar Funcoes.fExisteObjeto

 CREATE TABLE [dbo].[OrigemPrecosAlterados](
	[Codigo] [Tinyint] NOT NULL,
	[Descricao] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_OrigemPrecosAlterados] PRIMARY KEY CLUSTERED 
(
	[Codigo] ASC
))

Inserir nesta tabela os registros abaixo

1- Promoções
2- Cadastro de Produtos
3- Controle de Entradas
4- Fórmulas dos Produtos
5- Cadastro de Fornecedores
6- Cadastro de Grupos
7- Sem Registro de Origem

Criar campo CodOrigem, tratar utilizando fExisteCampo
ALTER TABLE PRECOS_ALTERADOS ADD CodOrigem Tinyint null default 7

criar também no arquivo morto, tratar utilizando fExisteCampo
ALTER TABLE PRECOS_ALTERADOS_ARQ_MORTO ADD CodOrigem Tinyint null default 7

Dar um update nos registros antigos colocando todos no código 7 tanto na tabela preços alterados quanto na arquivo morto

## Criar enum público eOrigemPrecosAlterados
Criar enum no mdlGestao junto com as outras variáveis globais
Criar igual a tabela OrigemPrecosAlterados na formatação CamelCase
ex:
Indefinido = 1
CadastroDeProdutos = 2
...

## Colocar coluna origem na tela preços alterados 
Colocar a tela na resolução 1024 para os ajustes

Retirar o "Selecionar" diminuir essa coluna do checkbox 
Ajustar colunas de datas para a melhor largura
Diminuir um pouco as colunas de preço

Programar para atualizar a tela com as informações da nova coluna

## Criar combobox Origem:
Colocar a tela na resolução 1024 para os ajustes
Criar o combobox "Origem:"
Popular o combobox com os dados da tabela OrigemPrecosAlterados
programar para filtrar de acordo com o combobox assim como nos outros combos da tela
Após programado verificar com a análise o melhor local para inserir o combo

## Excluir procedimento frmCadEncartes.sAtualizaPromocoesRelacionadas
O procedimento não está mais sendo utilizado e possui erros na sua estrutura


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


## Alterar o procedimento frmCadProdutos.sGravarPrecosAlteradosLojas

Alterar o insert para inserir a origem
PrecosAlterados.CodOrigem = 2- Cadastro de Produtos (utilizar eOrigemPrecosAlterados)

Testar a funcinalidade


## Alterar o procedimento frmCadProdutos.fAlterarPrecoVenda

Criar um parâmetro do tipo eOrigemPrecosAlterados
Alterar as duas chamadas (cad. produtos e Controle de Entradas) para passar o parâmetro
Alterar o insert para inserir a origem de acordo com o parâmetro
PrecosAlterados.CodOrigem = 2- Cadastro de Produtos 

Testar a funcinalidade no cadastro de produtos e controle de entradas


## Alterar o procedimento frmCadProdutos.sEnviarRegistroImediata

Alterar o insert para inserir a origem
PrecosAlterados.CodOrigem = 2- Cad. de Produtos (utilizar eOrigemPrecosAlterados)

Testar a funcinalidade (forçar para entrar no procedimento pelo imadiate do vb para testar)




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




## Testar arquivo morto na seção referente aos preços alterados



## Tratar o procedimento frmPrecosAlterados.cmdRemover_Click
não possibilitar excluir produtos de origem da promoção, mostrar a mensagem abaixo

O registro de preço alterado do produto 9999 XXXX tem a origem "Promoções" e não poderá ser removido.

Fazer testes validando que não é possível remover um produto de promoção 


## Tratar o procedimento frmPrecosAlterados.cmdRemover_Click
ao remover um produto filho de fórmula avisar


## Tratar o procedimento frmPrecosAlterados.cmdRemover_Click
Resolver o bug de vulnerabilidade abaixo
Se abrirmos o controle de entradas em 2 computadores, listar nos 2, enviar carga em 1 e no outro remover os registros, é possível
remover registros que já tiveram a carga enviada. Isso nunca pode acontecer

Tarefa: ao excluir um registro ir no banco e testar novamente com um select se o registro já não teve a carga enviada




verificar problema dos registros que aparecem parcelados 

testar relatorio mapa alteração preço custo





está gerando preços alterados sempre, para produtos em promoção 
não está excluindo preços alterados não enviados 
"frmCadGrupo", Err, "cmdReajuste_Click"