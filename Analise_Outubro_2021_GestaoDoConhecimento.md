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