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

° Criar ou duplicar o formulário

° Alterar filtros
   - Deixar invisível o filtro da análise horizontal/ Vertical (vamos usar só no futuro) 
   - Retirar o Filtro Tipo > Analítico/ Sintético e todo código relativo a ele
   - Retirar o filtro Tipo de Análise  e todo código relativo a ele
   - Retirar o filtro Não visualizar ajuste de saldo  e todo código relativo a ele
   - Adicionar o filtro do custo gerencial / custo médio igual a do relatório ABC 2.0
   - Trocar o combo de loja colocando igual a do Relatório Pack Virtual (combo + check box) + marcar/desmarcar todas padrão todas

° Adicionar grade
    - Utilizar as mesmas funções de formatação da grade que no relatório de pack virtual
    - Colocar totalizador de registros 
    
° Outros
    - Trocar o botão Visualizar por Consultar
    - Colocar embaixo os botões Exportar, Imprimir e Sair 
    - Ajustar Tab Index
    - Alterar os nomes dependendo se é custo gerencial ou custo médio
    - Criar thread no consultar
    - Deixar do tamanho 1024x768
    - Possibilitar maximizar aumentando principalmente o grid
    - Colocar em vermelho quando o resultado é negativo
    
° Impressão    
    - Na impressão perguntar se quer o analítico ou sintético
    - No final da impressão colocar observação do tipo de análise é "Data de Competência: Análise do Resultado do Exercício"    

° Botão Question
    - Colocar um mensagebox explicando a origem dos dados

° Relatório ABC 2.0 
    - Alterar os nomes se for custo gerencial ou custo médio










## Tarefa 1: 

git flow hotfix start {nome]


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
