# Épico: Reformulação do DRE
Data de início da análise: 23/05/2022

## Problema a ser resolvido
Diferenças nas informações das vendas e custo das mercadorias vendidas entre o DRE Gerencial e o ABC de Mercadorias

## Impactos
Não terá impacto pois será uma tela nova somente para exibição de dados

## Pré Requisitos
Será necessário ter o ambiente do Gestao_Relatorios funcionando.
Necessita o Finish da feature do ABC 2.0

## Solução


## Tarefa 1: Criar feature no git e ambiente

git flow feature start NovoRelatorioDRE

Criar ambiente GestaoRelatorios

Tela Classica > Sistema > Congigurações Gestão Relatórios

------------------------------------------------------------------------------------------------------

## Tarefa 2: Criar novo formulário
Criar no C# o formulário FrmRelDRE no caminho  GestaoComercial > Formularios > Financeiro

Criar layout conforme protótipo

No novo form implementar as configurações padrões da Telecon
1. Criar formulário do tamanho 1024x768
1. Fechar com esc
1. Enter deve funcionar como tab
1. Ícone do sistema S na janela
1. O botão Sair fecha a tela
1. Não deve permitir o resize da tela com o mouse
1. Ao maximizar deveremos apenas ancorar os botões e o grid no botton, assim iremos apenas aumentar o grid para baixo.
1. Deve abrir centralizado. 
1. Implementar tratamento da ampulheta do mouse no início e fim do click do botões consultar
1. Formatar o grid. 
1. Adicionar o combo + checkbox de lojas com a opção de marcar e desmarcar todos
1. Adicionar o combo CMV com as Oções "Custo Médio" e "Custo Gerencial"

![image](https://user-images.githubusercontent.com/80394522/171756235-e8adbe33-4845-4b6f-85f4-95090ad372c3.png)


------------------------------------------------------------------------------------------------------


## Tarefa: Criar módulo para Gerenciar o recurso Relatório DRE Gerencial

1. Criar verifica banco para inserir o código abaixo
sInserirModulo 572, "FINANCEIRO", "Relatório DRE Gerencial", eStatusModulo.mOCULTO, 127, 10, "FrmRelDRE", True, True, True, True, 5

2. Validar se apareceu no módulo de indicadores e também no novo menu do lado esquerdo do novo sistema S
Se não ficar ativo para todo usuários executar update em operadores_modulos para ativar o módulo


3. Chamar na tela clássica no menu de Financeiro > Relatório DRE Gerencial
Adicionar abaixo do DRE Gerencial, logo será substituído.

------------------------------------------------------------------------------------------------------

## Tarefa: Implementar o botão Consultar (Parte 1)

obs: Utilizar como base o Relatório Analise de Venda Conjunta e Relatório Pack Virtual

1. No clique do botão consultar (assim como no relatório Analise de venda conjunta)
- Limpar a grade
- Criar e Popular uma classe filtros contendo os filtros da tela
- Criar o procedimento `Processar` e chamar ele por uma thread passando a classe filtro por parâmetro
- Estartar a thread


1. Criar diretório "DREGerencial" no caminho Telecon.GestaoComercial.Biblioteca.Relatorios


1. Criar a classe DREGerencialLinhaRelatorio com as propriedades abaixo

``` c sharp
public string CodEstrutura { get; set; }
public double CodConta { get; set; }
public string Descricao { get; set; }
public decimal Valor { get; set; }
public decimal PorcentagemReceita { get; set; }
public decimal PorcentagemDespesa { get; set; }
public decimal Previsao { get; set; }
```

2. No diretório "DREGerencial" Criar a classe `DREGerencialRelatorio`
- Na classe `DREGerencialRelatorio` criar o procedimento `ConsultarRelatorioDRE` que retorne um list de `DREGerencialLinhaRelatorio` e receba a classe de filtros por parâmetro.
- Na classe `DREGerencialRelatorio` criar o procedimento `void MontarEstruturaDRE`
- Na classe `DREGerencialRelatorio` criar o procedimento `void CarregarResultadoBruto`
- Na classe `DREGerencialRelatorio` criar o procedimento `void CarregarDespesas` 

------------------------------------------------------------------------------------------------------

## Tarefa: Implementar procedimento `ConsultarRelatorioDRE`

1. Criar a variável list ` <List>DREGerencialLinhaRelatorio listaDRE`

1. Chamar o procedimento `MontarEstruturaDRE` passando a `listaDRE` por parâmetro de referência.

1. Implementar o procedimento `MontarEstruturaDRE`
- Na lista `listaDRE` Adicionar manualmente os itens abaixo  

 ```
    CodEstrutura = 3
    Descricao = RESULTADO GERENCIAL

    CodEstrutura = 3.1
    Descricao = RESULTADO BRUTO

    CodEstrutura = 3.1.1
    Descricao = Vendas

    CodEstrutura = 3.1.1.01
    Descricao = Vendas NFCe

    CodEstrutura = 3.1.1.02
    Descricao = Vendas NFe

    CodEstrutura = 3.1.2
    Descricao = (-) Custo das Mercadorias Vendidas ***

    CodEstrutura = 3.2
    Descricao = (-) DESPESAS GERENCIAIS OPERACIONAIS 

    * os outros campos recebem zero
```
*** A descrição o custo pode variar de acordo com o combro (Custo Médio / Custo Gerencial). Deverá ficar no formato abaixo 
"(-) Custo Médio das Mercadorias Vendidas"
"(-) Custo Gerencial das Mercadorias Vendidas"

1. Inserir na `listaDRE` todo o plano de contas a partir do "3.2" que está na configuração `Conta100PorCentoPagar`
- Criar a variável `var contaDREDespesas = config.Conta100PorCentoPagar.CodEstrutural;`
- Adicionar na `listaDRE` todo o restante do plano de contas assim como foi feito no procedimento `PlanoConta.ConsultarAPartirEstrutura(banco, contaDRE);` mas na variável `contaDREDespesas`

1. Chamar o procedimento `RetornarLinhasResultadoBruto`  passando a `listaDRE` por parâmetro de referência.

1. Chamar o procedimento `RetornarLinhasDespesasGerenciaisOperacionais` passando a `listaDRE` por parâmetro de referência.

1. Chamar o procedimento `RetornarLinhasDespesasEReceitasNaoOperacionais` passando a `listaDRE` por parâmetro de referência.
------------------------------------------------------------------------------------------------------

## Tarefa: Implementar procedimento `CarregarResultadoBruto`


1. Realizar uma consulta em banco conforme os passos abaixo e atualizar na `listaDRE` que veio por parâmetro as informações de Venda e custo.

Requisitos para a consulta:
- Buscar as vendas no banco GestaoRelatorios, para a variável IBanco utilizar `Utilitarios.ObterConexaoRelatorios();`. 
- Consultar na tabela `VendasDia` utilizando os filtros (where) do mesmo formato que foi utilizado no procedimento `Telecon.GestaoComercial.Biblioteca.Relatorios.ResultadoLoja.VisaoGeral.Consultar`
- Valor vendas pdv = sum(vendasDia) where TipoVenda = 'NFCe'
- Valor vendas nfe = sum(vendasDia) where TipoVenda =  'NFe'
- Diferenciar Custo Médio ou Custo Gerencial dependendo do combo (filtro)

2. Percorrer os dados da consulta atualizando a propriedade `Valor` da `listaDRE`

------------------------------------------------------------------------------------------------------

## Tarefa: Implementar procedimento `CarregarListaDespesas`


obs 1: Iremos utilizar o procedimento Telecon.GestaoComercial.Biblioteca.Financeiro.ConsultarDRE para
utilizar como base.

obs 2: Iremos utilizar o procedimento Telecon.GestaoComercial.Biblioteca.Financeiro.ConsultarLancamentos para
utilizar como base.

1. Criar a variável com a configuração da contaDRE `var contaDRE = config.ContaDRE.CodEstrutural;`

- Realizar consulta na tabela LancamentosFinanceirosPagamento semelhante a consulta abaixo utilizando os filtros corretos (where)
- Realizar consulta na tabela LancamentosFinanceirosRecebimentos semelhante a consulta abaixo utilizando os filtros corretos (where)
- Para as 2 consultas usar o filtro abaixo
Filtro 1:  CodEstrutural LIKE `var contaDRE + %` (ficará algo semelhante a consulta logo abaixo)
Filtro 2: `Cancelado = " + banco.ObterVerdadeiroFalso(false)`
Filtro 3: Utilizar Filtros de data nesse modelo `sb.AppendLine(" AND " + new CalculosRelatoriosSQL().SoData("LP.DataHoraPagamento") + " >= " + banco.ObterData(Convert.ToDateTime(dataInicio)));`


``` SQL
    SELECT TAB.CodEstrutural, TAB.Descricao, SUM(TAB.VALOR) FROM
    (

        select PC.CodEstrutural, PC.Descricao, LFR.* from [dbo].[LancamentosFinanceirosReceber][LFR]
        inner join [dbo].[PlanoContas][PC] ON LFR.CodContaReceber = PC.CodConta 
        WHERE CodEstrutural LIKE '3.%' AND YEAR(LFR.DataCompetencia) = 2022 AND MONTH(LFR.DataCompetencia) = 2

    ) AS TAB
    GROUP BY TAB.CodEstrutural, TAB.Descricao




    SELECT TAB.CodEstrutural, TAB.Descricao, SUM(TAB.VALOR) FROM
    (

        select PC.CodEstrutural, PC.Descricao, LFR.* from [dbo].[LancamentosFinanceirosPagar][LFR]
        inner join [dbo].[PlanoContas][PC] ON LFR.CodContaPagar = PC.CodConta 
        WHERE CodEstrutural LIKE '3.%' AND YEAR(LFR.DataCompetencia) = 2022 AND MONTH(LFR.DataCompetencia) = 2

    ) AS TAB
    GROUP BY TAB.CodEstrutural, TAB.Descricao
    order by TAB.CodEstrutural 
```


1. Realizar as consultas abaixo assim como no procedimento `ConsultarLancamentos` citado no início da tarefa 
- BUSCAR LANÇAMENTOS FINANCEIROS REFERENTES A ESTORNO NAS NOTAS DE SAÍDA DO TIPO DE OPERAÇÃO Estorno de NF-e (DÉBITO) e (CRÉDITO)
- BUSCAR LANÇAMENTOS FINANCEIROS REFERENTES A ESTORNO NAS NOTAS DE ENTRADA DO TIPO DE OPERAÇÃO Estorno de NF-e (DÉBITO) e (CRÉDITO) 
obs 1: Em vez da tabela Gestao.LancamentosFinanceiros iremos utilizar as tabelas GestaoRelatorios.LancamentosFinanceirosReceber e GestaoRelatorios.LancamentosFinanceirosPagar
`PUTS`obs 2: Não precisaremos dos filtros de codestrutura??, pois já está certo na tabela Gestão
--- SERÁ QUE NÃO PRECISAREMOS PARA FAZER JOIN COM O TIPO DE OPERAÇÃO ???


- Preenche a variável de contas com os valores
- Adicionar nas contas os valores de Juros, taxas, descontos e multas
- Atualizar previsto vs realizado
- Fazer if para exluir contas sem saldo caso selecionado pelo usuário





 Teremos que realizar diversas alterações. Alterar o retorno do procedimento ConsultarDRE de  `List<RelDRE>` para `<List>DREGerencialLinhaRelatorio`

- retirar os parâmetros `EnumTipoData tipoData| EnumTipo tipo | numAnalise analise | bool naoVisualizarAjuste`

- Retirar o if `if (naoVisualizarAjuste)`
- Retirar o if `if (tipo == EnumTipo.Sintetico)`
- Comentar todos os trechos de código que utilizam o if `if (analise == EnumAnalise.Horizontal)`

1. Iremos copiar o procedimento Telecon.GestaoComercial.Biblioteca.Financeiro.ConsultarLancamentos apenas para
utilizar como base. Teremos que realizar diversas alterações. Alterar o retorno do procedimento ConsultarDRE de  `List<RelDRE>` para `<List>DREGerencialLinhaRelatorio`
- Comentar todos os trechos de código que utilizam o if `if (analise == EnumAnalise.Horizontal)`
- 


obs: Esta será a tarefa mais complexa
obs: Neste momento consultar a análise para realizar os passos abaixo
- Corrigir todos os nomes de campos para buscarmos do banco Gestão Relatórios
- Duplicar o select principal devido a termos 2 tabelas 




## Tarefa: Implementar procedimento `RetornarLinhasDespesasEReceitasNaoOperacionais`
------------------------------------------------------------------------------------------------------

1. Atualizar o item "RESULTADO GERENCIAL" com a soma da lista `listaDRE.Valor`
------------------------------------------------------------------------------------------------------

## Tarefa: No procedimento Processar chamar o ConsultarRelatorioDRE e a partir do retorno dele carregar o grid

Colocar em vermelho quando o resultado é negativo





## Tarefa: Melhorar dados de cabeçalho contendo todoas as inforações de filtros



## Tarefa: Criar mensagem padrão caso não possua a estrutura do Gestão Relatórios Ativa

------------------------------------------------------------------------------------------------------

## Tarefa: Trabalhar na questão das contas selecionadas para carregar as despesas na tabela Gestão Relatórios





-
-
-
-
-
-
-
-
-
-
-
-
-
-
-
-
-
-
-

-
-
-
-
-
-







------------------------------------------------------------------------------------------------------

## Tarefa: Programar o contador de registros

## Tarefa: Implementar gif de carregando



## Tarefa: Implementar botão de imprimir
1. Pergutar se a impressão será analítica ou sintética
2. No final da impressão colocar observação do tipo de análise é "Data de Competência: Análise do Resultado do Exercício"    

## Tarefa: Implementar botão de exportar

## Tarefa: Criar botão Question
   - Colocar um mensagebox explicando a origem dos dados

## Tarefa: Últimos ajustes

## Tarefa: Ajustar Tab Index

## Tarefa: - Relatório ABC 2.0 
    - Alterar os nomes se for custo gerencial ou custo médio



------------------------------------------------------------------------------------------------------

![image](https://user-images.githubusercontent.com/80394522/170387581-be5e6dff-f95a-4924-9a1b-dbf9045d7a5e.png)

------------------------------------------------------------------------------------------------------

![image](https://user-images.githubusercontent.com/80394522/169874103-36b84d01-40a6-45cb-8f83-7e6889e19e7d.png)

------------------------------------------------------------------------------------------------------

![image](https://user-images.githubusercontent.com/80394522/170390128-27d82208-c37c-4a5c-bca2-10d6a237aa38.png)

------------------------------------------------------------------------------------------------------

![image](https://user-images.githubusercontent.com/80394522/169874258-60505df8-d7ff-4215-bed7-4dd563ea6868.png)


![image](https://user-images.githubusercontent.com/80394522/169874352-e65b0abc-2fd4-4fdb-bfb3-88cc754f9290.png)


![image](https://user-images.githubusercontent.com/80394522/169874442-68c0a11b-99ac-40c3-bea0-613ada85b679.png)


------------------------------------------------------------------------------------------------------
Pré Requisito: Necessita do Finish da branch feature/ABC_2_0_Relatorio

------------------------------------------------------------------------------------------------------

Criar a configuração abaixo na tabela de configurações

![image](https://user-images.githubusercontent.com/80394522/170153508-ccaa511f-a0a3-4fea-bc6e-6ff2ba77223b.png)

------------------------------------------------------------------------------------------------------

Criar no formulário acima o mesmo label de período que na tela do abc, fazer o tratamento também para não selecionar uma data posterior
Utilizar o mesmo código e não duplicar, se necessário mudar o código de lugar deixando ele público

![image](https://user-images.githubusercontent.com/80394522/169921621-b7ae6b6b-8481-4ec4-88d1-d6704c556e2f.png)

------------------------------------------------------------------------------------------------------

Ao clicar em visualizar caso a configuração criada esteja marcada instanciar o objeto GestaoComercial.Formularios.Indicadores.ResultadoDaLoja.CarregarVisaoGeral para obtermos os dados do relatório abc de mercadorias
Utilizar os parâmetros de data da tela
Utilizar o cmv da configuração (médio ou gerencial)
utilizar as transferências configuradas
os outros parametros são em branco , zero ou nulos

![image](https://user-images.githubusercontent.com/80394522/169874541-7b3ee2ba-4ccf-4a83-b854-88cac08f6387.png)


------------------------------------------------------------------------------------------------------

Aqui ver para quando tiver a configuração pegar do objeto instanciado , ver configuração da estrutura
