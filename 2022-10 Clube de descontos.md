URL FOTO

![image](https://user-images.githubusercontent.com/80394522/193161284-e8939ab9-24e9-428c-a537-60b6382e3e73.png)



Na tabela configurações do gestão criar a configuração ClubeDescontos
![image](https://user-images.githubusercontent.com/80394522/193161493-57496476-852f-469c-99a0-fcdb8a05f222.png)


Na tela de configuração dos pdvs criar a configuração clube de descontos

![image](https://user-images.githubusercontent.com/80394522/193161705-05bbb8b1-ee1a-4c0f-85ad-d25c24e7e9ad.png)


no cadastro do cliente criar a configuração clube de descontos

![image](https://user-images.githubusercontent.com/80394522/193161779-2987a522-c6fb-4a49-9d0c-985907948197.png)


No menu da tela clássica criar o menu clube de descontos 


Criar campos para os dados do qqcusta 
![image](https://user-images.githubusercontent.com/80394522/193162499-1309f2e0-e327-467b-9206-0b5dae18b387.png)


criar formulário clube de descontos

![image](https://user-images.githubusercontent.com/80394522/193162155-b495598a-fd91-462c-9432-e054d5a8070e.png)
![image](https://user-images.githubusercontent.com/80394522/193162370-3944c5a4-762d-4ee3-abd6-0618db63b517.png)

ao salvar o pack enviar os dados da campanha para a api do qqcusta


ao realizar uma venda enviar os dados da venda para o qqcusta
- talvez enviar já o xml 

tratar o novo modelo de packvirtual no pdv

vamos ter que de alguma forma gravar as vendas por cpf para ver os limites
demora para subir os atus 


------

Modulos 
    - criar módulo club de descontos
    - criar menu tela clássica

URL Foto Produto
    - criar cadastro de imagens 
    - criar tabela no verifica banco 
    - criar classe telecode 
    - exibir apenas se o módulo estiver habilitado
    - link no cadastro de produtos 
    - no qqcusta tem uma telinha de gestão de imagens 
    - ser mais parecido com uma tela de pesquisa
    - com filtros , descrição , grupos de produtos .. 

Configurações PDV 
    - criar configuração clube de descontos (não sei se precisa)
    - criar configuração qq custa
    - exibir configurações apenas se o módulo estiver habilitado
    - criar novos campos no banco NovoPDV.mdb
    - adicionar exportação dessas campos

Pack Virtual 
    - Exibir pack preço 2 se o novo módulo estiver habilitado
    - Alterar nomenclatura do PackPreço 2 
    - Criar nova coluna "Limite" na tabela preço 2 
    - Alterar classe telecode    
    - Adicionar recursos para manipular a coluna "Limite"
    - Ao salvar pack acessar API e salvar campanha (qual imagem enviamos?? é obrigatória a imagem??)
        - Salvar no banco de dados apenas se consegui enviar a camanha para o qq custa 
    (podemos aqui já resolver o problema de muitos produto na tela pack virtual)
    - resolver problema mutios produtos
    
PDV
    - Criar nova regra de pack preço 2 no PDV     
    - tela de chamar o clube , tem uma oficial já 

Gerenciador 
    - ler o arquivo .atu da venda 
    - acessar API e salvar dados da venda 
    - acessar API e salvar XML da venda 
    - ver problema do atu do delfino , resolver o problema do atus não 

API (Possibilidade 1)
    - Na hora da venda consultar a API e ver se o cpf está no clube 
        - Abrir thread no C# após inserir o cpf na nota 
    

API (Possibilidade 2)
    - ao cadastrar no app atualiar o gestão do cliente + comandos para os pdvs 
        - criar API no cliente 
    - recurso para forçar uma busca da API no momento da venda , após atualizar o gestão e o banco do pdv 


Clientes
    - criar configuração clube de descontos , talvez só um label , pq o cliente nã poder marcar 
    - exibir configurações apenas se o módulo estiver habilitado
    - criar nova configuração no NovoPDV.mdb
    - exportar configuração
    - ao salvar enviar comando para o commandos pdv 


Fazer tratamento limite de vendas por cpf no pdv 
    - Criar nova tabela no banco de dados 
    - No final da venda largar uma thread para tentar enviar para o servidor, ou fazer por arquivo          
         - inserir também nos comandos pdvs 
         - criar tabela totalizadora no pdv.mdb 
         - atualizar tabela na exportação 
         - limpar tabela 
- no controle de campanhas promocionais tem um controle pelo saldo 
- temos uma lista de produtos que podem ter limites por cpf 
- ao digitar o cpf buscar uma lista online
- novo atu somente para produtos que tem limite 
- avisar o operador de caixa que o produto não tera desconto pois está no limite 


