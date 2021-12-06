Termo de abertura
https://docs.google.com/document/d/1nLzFj06A8gLK8v7oTavBS4TrJgzdbN0u/edit

atualizações do compras
https://brandolffvitor.wixsite.com/compras/pagina-inicial




seria essa nossa ideia, ver se é possível, com novo visual

protótipo s

* Alteração Tela do S
    * Layout Tela S
        * Popup Lateral por setor
        * Alarm das novidades

* Operador Setores
    * Criar tela para o usuário parametrizar 
    * Operadores novidades por setor

bordas arredondadas c#
https://www.codeproject.com/Articles/15964/ExControls-Version-1-0


How to use CefSharp (chromium embedded framework c#) in a Winforms application
https://www.youtube.com/watch?v=fOzBVy-sDbM

#Passo a Passo

* Criar projeto windows form com framework 4.6.1
* Instalar pacote Nuget CefCharp winforms
* No gerenciador de configurações da solution passei o projeto para x86
* Alteração do csProj add `<CefSharpAnyCpuSupport>true</CefSharpAnyCpuSupport>`
* Alteração do app.config add
``` js
<runtime>
    <assemblyBinding xmlns="urn:schemas-microsoft-com:asm.v1">
        <probing privatePath="x86"/>
    </assemblyBinding>
</runtime>
```
* Criei a classe de comunicação com construturor de 2 parâmetros
* Adcionei o código no form principal
* adicionei uma pag index.html
* coloquei para sempre copiar a página para o debug




código para habilitar o download

``` c#

            cwbSite = new ChromiumWebBrowser("http://grupotelecon.com.br/");
            //cwbTelaDoS = new ChromiumWebBrowser("https://drive.google.com/drive/folders/1P_hFANgvAZO1TliU9Henl1G8AVyYSCzT?usp=sharing", null);

            cwbTelaDoS = new ChromiumWebBrowser(paginaDoS);

            cwbTelaDoS.Margin = Padding.Empty;

            DownloadHandler downloadHandler = new DownloadHandler();
            cwbTelaDoS.DownloadHandler = downloadHandler;



            

```
Learn To Build An SVG Animation With CSS
https://www.youtube.com/watch?v=gWai7fYp9PY

figma
rodrigo80221@gmail.com
Rodrigo949






notificações
``` sql

select * from Push  
select * from PushTipo 
select * from PushStatus 
select * from PushOperador 


select * from Push 

insert into push values ('Teste', 'Descricao', 0, 'Destino', 1, null , getdate()+100 )
insert into PushOperador values(5,1, 1, 1)

```


testes framework

atualizar um pc utilizando o OptionalFeatures
ver se o gestão comercial vai abrir , fazer alguns processos nele 
se funcionar
    criar uma função para verificar o framework do pc 
    tentar automatizar a atualização 
vericar se a tabela LogAtualizacaoFramework existe
    salvar nela o pc que não está atualizado ou teve erro
    se atualizou ou possui marcar esse pc como atualizado    


    criar um arquivo com o .svg separado
    https://www.kadunew.com/blog/html/svg-sprite-como-criar-um-arquivo-de-icones-usando-svg


    

procedimento para habilitar o push
    ``` vb
    Sub sExecutaPush()
    ```


problema da thread
    https://stackoverflow.com/questions/5519294/getting-webbrowser-control-to-work-in-console-application




# Não esquecer

Avaliar a necessidade de atualização do framework do pdv

Paula, 17:03
agora, sobre os cartazes poderíamos elaborar novo layout com marketing e ai deixar como padrão no sistema s, se desse


 # Espaço para questões


    
Como será a estrutura e o acesso a movidesk?

Como está a universidade Telecon?
não está mais em funcionamento

Mostrar idéias do Profit, Universidade, Vídeos, Alertas, Atualização

A Telecon já tem outro sistema para incluir o cadastro ou fazer um projeto novo?
    * Já não temos algo que os clientes acessam?
    * Como pensam em fazer a atualização automática?

Os clientes novos aderem bem a Tela do S? 
    * Ou quando eles descobrem a tela clássica eles migram
    * Existema clientes novos usando a tela clássica 

O que faria um cliente como o índio usar a Tela do S?
    * Se já sabemos o setor do usuário não era melhor mostrar os seus atalhos direto já?
        * Poderíamos deixar acizentado outros setores..
        * Lançar uma versão antes para já ir mapeando as telas que o usuário utiliza
        * Personalização??

Como está funcionando a caixa de entrada? Está sendo utilizada?
não está funcionando



utilizar a estrutura do comand help para inserir link nos bancos dos clientes
criar pagina web para cadastrar os links

criar tela inicial para relacionar usuários vs módulos

criar etapas iniciais para atualização do framework nos clientes

como altera os texts da tela do s??

temos que cofigurar o youtube para não mostrar mensagens com propagandas de outros softwares d gestão comercial

será que a cor do fundo cinza com letras brancas está boa para visualização?

os setores abaixo precisam serem programados no sistema R ?
JuridicoSistemaS
MarketingSistemaS,
RretaguardaSistemaS

 ## Reunião 26/10/2021

### Usabilidade 

 Eu gostaria de ter uma meta de conseguirmos abandonar a tela classica (seria sucesso para o projeto)
 - Se continuarem usando ela, nada disso aparecerá para alguns clientes

O que faria um cliente como o índio usar a Tela do S?

Idéias
- Menu na esquerda com a árvore de recursos do gestão
- Colocar na esquerda os ícones mais utilizados 

- Menu na direita para abrir os alertas, youtube, busca de artigos, universidade telecon, suporte, 

Existe um motivo para alterar o nome dos texts somente na tela dos processos?

### Projeto

- Criar tela inicial para relacionar usuários vs setores que deseja acompanhar as novidades

- Automatizar a atualização de framework


``` sql

update Setores set DtAtuLandingPage = getdate() , LinkLandingPage = 'https://www.dicio.com.br/pesquisa.php?q=compras' where codigo < 100 and Nome = 'Compras'
update Setores set DtAtuLandingPage = getdate() , LinkLandingPage = 'https://www.dicio.com.br/pesquisa.php?q=estoque' where codigo < 100 and Nome = 'Estoques'
update Setores set DtAtuLandingPage = getdate() , LinkLandingPage = 'https://www.dicio.com.br/pesquisa.php?q=retaguarda' where codigo < 100 and Nome = 'T.I / CPD / Preços'
update Setores set DtAtuLandingPage = getdate() , LinkLandingPage = 'https://www.dicio.com.br/pesquisa.php?q=loja' where codigo < 100 and Nome = 'Loja / Setores'
update Setores set DtAtuLandingPage = getdate() , LinkLandingPage = 'https://www.dicio.com.br/pesquisa.php?q=caixa' where codigo < 100 and Nome = 'Frente de Caixa'
update Setores set DtAtuLandingPage = getdate() , LinkLandingPage = 'https://www.dicio.com.br/pesquisa.php?q=acougue' where codigo < 100 and Nome = 'Açougue'
update Setores set DtAtuLandingPage = getdate() , LinkLandingPage = 'https://www.dicio.com.br/pesquisa.php?q=padaria' where codigo < 100 and Nome = 'Padaria / Confeitaria'
update Setores set DtAtuLandingPage = getdate() , LinkLandingPage = 'https://www.dicio.com.br/pesquisa.php?q=fiabreria' where codigo < 100 and Nome = 'Fiambreria / Rotisseria'
update Setores set DtAtuLandingPage = getdate() , LinkLandingPage = 'https://www.dicio.com.br/pesquisa.php?q=hortifruti' where codigo < 100 and Nome = 'Hortifruti'
update Setores set DtAtuLandingPage = getdate() , LinkLandingPage = 'https://www.dicio.com.br/pesquisa.php?q=financeiro' where codigo < 100 and Nome = 'Financeiro'
update Setores set DtAtuLandingPage = getdate() , LinkLandingPage = 'https://www.dicio.com.br/pesquisa.php?q=contábil' where codigo < 100 and Nome = 'Contábil / Fiscal'
update Setores set DtAtuLandingPage = getdate() , LinkLandingPage = 'https://www.dicio.com.br/pesquisa.php?q=seguranca' where codigo < 100 and Nome = 'Segurança'
update Setores set DtAtuLandingPage = getdate() , LinkLandingPage = 'https://www.dicio.com.br/pesquisa.php?q=rh' where codigo < 100 and Nome = 'R.H.'
update Setores set DtAtuLandingPage = getdate() , LinkLandingPage = 'https://www.dicio.com.br/pesquisa.php?q=indicadores' where codigo < 100 and Nome = 'Indicadores'
update Setores set DtAtuLandingPage = getdate() , LinkLandingPage = 'https://www.dicio.com.br/pesquisa.php?q=pdv' where codigo < 100 and Nome = 'Telecon PDV'

```











# Reformulação Sistema S: Controle de alertas por operador

1- Criar as tabelas abaixo pelo verifica banco


2- Utilizar Funcoes.fExisteObjeto 

``` sql
CREATE TABLE [dbo].[OperadorLandingPagesLog](
	[Codigo] [bigint] IDENTITY(1,1) NOT NULL,
	[CodOperador] [int] NOT NULL,
	[CodSetor] [int] NOT NULL,
	[DtVisualizacao] [datetime] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Codigo] ASC
))

GO 

CREATE TABLE [dbo].[OperadorLandingPages](
	[CodOperador] [int] NOT NULL,
	[CodSetor] [int] NOT NULL,
	[DtVisualizacao] [datetime] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[CodOperador] ASC,[CodSetor] ASC
))

```


3- Criar as classes `OperadorLandingPagesLog` e `OperadorLandingPages` pelo Telecode 
obs: Namespace `Telecon.GestaoComercial.Biblioteca.Pessoas`


4- Inserir um botão do lado esquerdo do formulário com o caption "Atualizar LP"
Ao clicar no botão inserir o código abaixo

``` sql
update Setores set DtAtuLandingPage = getdate() , LinkLandingPage = 'https://www.dicio.com.br/pesquisa.php?q=compras' where codigo < 100 and Nome = 'Compras'
update Setores set DtAtuLandingPage = getdate() , LinkLandingPage = 'https://www.dicio.com.br/pesquisa.php?q=estoque' where codigo < 100 and Nome = 'Estoques'
update Setores set DtAtuLandingPage = getdate() , LinkLandingPage = 'https://www.dicio.com.br/pesquisa.php?q=retaguarda' where codigo < 100 and Nome = 'T.I / CPD / Preços'
update Setores set DtAtuLandingPage = getdate() , LinkLandingPage = 'https://www.dicio.com.br/pesquisa.php?q=loja' where codigo < 100 and Nome = 'Loja / Setores'
update Setores set DtAtuLandingPage = getdate() , LinkLandingPage = 'https://www.dicio.com.br/pesquisa.php?q=caixa' where codigo < 100 and Nome = 'Frente de Caixa'
update Setores set DtAtuLandingPage = getdate() , LinkLandingPage = 'https://www.dicio.com.br/pesquisa.php?q=acougue' where codigo < 100 and Nome = 'Açougue'
update Setores set DtAtuLandingPage = getdate() , LinkLandingPage = 'https://www.dicio.com.br/pesquisa.php?q=padaria' where codigo < 100 and Nome = 'Padaria / Confeitaria'
update Setores set DtAtuLandingPage = getdate() , LinkLandingPage = 'https://www.dicio.com.br/pesquisa.php?q=fiabreria' where codigo < 100 and Nome = 'Fiambreria / Rotisseria'
update Setores set DtAtuLandingPage = getdate() , LinkLandingPage = 'https://www.dicio.com.br/pesquisa.php?q=hortifruti' where codigo < 100 and Nome = 'Hortifruti'
update Setores set DtAtuLandingPage = getdate() , LinkLandingPage = 'https://www.dicio.com.br/pesquisa.php?q=financeiro' where codigo < 100 and Nome = 'Financeiro'
update Setores set DtAtuLandingPage = getdate() , LinkLandingPage = 'https://www.dicio.com.br/pesquisa.php?q=contábil' where codigo < 100 and Nome = 'Contábil / Fiscal'
update Setores set DtAtuLandingPage = getdate() , LinkLandingPage = 'https://www.dicio.com.br/pesquisa.php?q=seguranca' where codigo < 100 and Nome = 'Segurança'
update Setores set DtAtuLandingPage = getdate() , LinkLandingPage = 'https://www.dicio.com.br/pesquisa.php?q=rh' where codigo < 100 and Nome = 'R.H.'
update Setores set DtAtuLandingPage = getdate() , LinkLandingPage = 'https://www.dicio.com.br/pesquisa.php?q=indicadores' where codigo < 100 and Nome = 'Indicadores'
update Setores set DtAtuLandingPage = getdate() , LinkLandingPage = 'https://www.dicio.com.br/pesquisa.php?q=pdv' where codigo < 100 and Nome = 'Telecon PDV'

```
após isso dar um load no browser para atualizar a tela 
`cwbTelaDoS.Load(BuscarPaginaDoS());`


5- Criar função para verificar se o setor teve uma atualização nova para o usuário

a- Criar no FrmPrincipal a função `VerificarAlertaNoSetor (enum Setor)` que retorne um boolean

b- Ir no banco e carregar as variáveis abaixo com o retorno do select

var DtAtuLandingPage =  SELECT [DtAtuLandingPage] FROM [Setores] WHERE [Codigo] = `Setor` 

var DtVisualizacao = SELECT TOP 1 [DtVisualizacao] FROM [OperadorLandingPages] WHERE [CodOperador] = X AND [CodSetor] = `Setor` ORDER BY [DtVisualizacao] DESC

c- A função deverá retornar conforme a lógica abaixo

``` csharp
if (DtAtuLandingPage > DtVisualizacao) 
    return true 
else return false
```
obs: tratar caso algum dos 2 selects retornar null
if DtAtuLandingPage = null and DtVisualizacao = null then return false
else if DtAtuLandingPage = null then return false
else if DtVisualizacao = null then return true


6- Programar para a tela web verificar se temos um alerta para algum setor

a- criar a função `carregarAlertas` na classe `ComunicacaoChromium` que chame a função `VerificarAlertaNoSetor (enum Setor)` do FrmPrincipal

b- criar as chamadas para essa função no arquivo `SistemaS.JS`
 
obs: Realizar uma estrutura semelhante a já criada para o `carregarResponsaveis` mas em vez de alterar a propriedade `textContent`no arquivo `SistemaS.JS` alterar os atributos abaixo da mesma forma que alteramos os atributos na função js `AjustarTextoNoBox`

``` 
    stroke: green !important;
    stroke-width: 4;
```


7- Ao clicar no ícone atualizar a data de visualização da Landing Page desse usuário para esse setor 

a- Criar no formulário a função `AtualizarOperadorLandingPage (enumSetores setor)`
b- Realizar um insert na tabela [OperadorLandingPagesLog]
c- Realizar um insert na tabela [OperadorLandingPages]

Criar novos setores na tela setores
criar um campo para imagem na tela RecursosSoftware
Criar um campo para dizer a linguagem (vb ou c#)
Fazer a tela de processos buscar a imagem da tabela RecursosSoftware
Inserir na tabela RecursosSoftware todos os recursos que ainda não temos

Criar uma classe ou lista que reflita o menu




