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


## Tarefa 1: Criar feature no git

git flow feature start NovoRelatorioDRE

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
sInserirModulo 572, "FINANCEIRO", "Relatório DRE Gerencial", eStatusModulo.mHabilitado, 127, 10, "FrmRelDRE", True, True, True, True, 5

2. Validar se apareceu no módulo de indicadores e também no novo menu do lado esquerdo do novo sistema S
Se não ficar ativo para todo usuários executar update em operadores_modulos para ativar o módulo


3. Chamar na tela clássica no menu de Financeiro > Relatório DRE Gerencial
Adicionar abaixo do DRE Gerencial, logo será substituído.

------------------------------------------------------------------------------------------------------

## Tarefa: Implementar o botão Consultar (Parte 1)

obs: Utilizar como base o Relatório Analise de Venda Conjunta e Relatório Pack Virtual

1. No clique do botão consultar
- Limpar a grade
- Popular uma classe com com filtros
- Criar o procedimento `Processar` e chamar ele por uma thread passando a classe filtro por parâmetro
- Estartar a thread


1. Criar diretório "DREGerencial" no caminho Telecon.GestaoComercial.Biblioteca.Relatorios
1.1. Criar a classe DREGerencialRelatorio com as propriedades abaixo

``` c sharp
public string CodEstrutura { get; set; }
public string Descricao { get; set; }
public decimal Valor { get; set; }
public decimal PorcentagemReceita { get; set; }
public decimal PorcentagemDespesa { get; set; }
```
1.1. Criar o procedimento ConsultarRelatorioDRE que retorne um list de "DREGerencialRelatorio"

1.1. No procedimento `Processar` chamar o ConsultarRelatorioDRE e a partir do retorno dele carregar o grid

------------------------------------------------------------------------------------------------------

## Tarefa: Implementar o botão Consultar (Parte 2)

obs: Utilizar como base o Relatório Analise de Venda Conjunta e Relatório Pack Virtual

1. criar procedimento `private List<VisaoGeral> CarregarVisaoGeral()` igual ao que tem no formulário FrmResultadoLojaFiltros

1. Implementar o procedimento ConsultarRelatorioDRE
1.1. Criar o list `var visaoGeral = CarregarVisaoGeral();`
1.1. Criar a variável `VendasPDV` com o conteúdo `visaoGeral.Sum(x => x.CompraTransferencia);`

Criar a variável VendasNotasFiscais

                var qtdVendida = visao.Sum(x => x.Venda);
                var Custo = visao.Sum(x => x.CMV);



Colocar em vermelho quando o resultado é negativo


Alterar os nomes dependendo se é custo gerencial ou custo médio



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
