# Modelagem Atual do Centro de Custos

![image](https://user-images.githubusercontent.com/80394522/174605425-2bd7b260-d9c2-4a87-a259-ca2543f67291.png)
> Imagem Antiga Estrutura Centro de Custos

## Tarefa: Modificação do banco de dados para se adequar ao Centro de Custos por loja

![image](https://user-images.githubusercontent.com/80394522/175949307-b7331086-31b7-4704-aa03-65a8c2925e5c.png)

> Imagem Nova Estrutura Centro de Custos

obs: Podemos realizar as alterações desejadas pois ainda não temos nenhum cliente utilizando o centro de custos

1. Criar verifica banco para realizar as alterações apontadas em vermelho na imagem
    - Dropar e recriar as tabelas, PK's e FK's com os novos campos
    - Criar chaves PK indicadas
    - Criar relacionamento FK indicados
    - Criar triggers
    - Alterar classes Telecode
    - Considerar também os detalhes descritos nos itens abaixo

1. Alterar a tabela CentrosCusto excluindo o campo informado PercentualPadrao
    - Alterar classe Telecode

1. Criar tabela CentroCustoLojas com chave PK composta
    - Criar pk para CentrosCusto. Add delete e update cascade. Caso seja excluído ou alterado um registro na tabela CentrosCusto deverá ser propagada para as outras tabelas
    - Realizar um update na tabela CentroCustoLojas populando a tabela com todas lojas e todos centros de custos no banco atual

1. Criar a trigger TG_InserirCentrosCustoLojas na tabela CentrosCusto
    obs: servirá para que ao inserir um novo centro de custos na tabela CentrosCusto crie automáticamente o centro de custo para todas as lojas na tabela CentroCustoLojas

1. Criar trigger TG_InserirCentrosDeCusto na tabela de Lojas 
    - Ao inserirmos um registro na tabela Lojas deverá ser preencida a tabela CentrosCustoLojas com a nova loja e todos os centros de custos

1. Recriar a tabela CentroCustoSecoes 
    - Terá chave PK composta de 3 elementos 
    - Terá Fk Composta com 2 elementos (CodLojaCentrosCusto, CodCentroCusto) para CentroCustoLojas
    - Alterar classe Telecode

1. Recriar a tabela CentroCustoPlanoContas 
    - Terá chave PK composta de 3 elementos 
    - Terá Fk Composta com 2 elementos (CodLojaCentrosCusto, CodCentroCusto) para CentroCustoLojas
    - Alterar classe Telecode

1. Recriar a tabela LancamentosFinanceirosCentroCusto
    - Terá PK composta de 3 elementos 
    - Terá FK Composta com 2 elementos (CodLojaCentrosCusto, CodCentroCusto) para CentroCustoLojas
    - Alterar classe Telecode

1. Alterar a tabela PlanoContas adicionando o campo bit PermiteCentroCustoLojas    
    - Alterar classe do Telecode


# Remover campo porcentagem padrão no centro de custos

![image](https://user-images.githubusercontent.com/80394522/174673749-689b26fb-30b0-4a4b-8f93-57fd1e132870.png)
> Imagem print formulário "Cadastro de Centros de Custo"

1. Remover o campo `% Padrão`
1. Remover o botão e a funcionalidade `Atualizar Plano de Contas`

Obs: Devido a termos um centro de custos por loja e também podermos adicionar um centro de custos de outra loja impossibilitou criar uma forma simplista para popular o plano de contas.
Teremos um novo recurso no plano de contas para importar e exportar configurações de determinada conta.

Obs2: O recurso de atualizar os lançamentos financeiros será migrado para a tela do plano de contas. O código pode ser mantido para utilizar na outra tela.


# Testar Cadastro do Centro de Custos

1. Realizar testes na tabela CentroCustoLojas utilizando a tela "Cadastro de Centro de Custos"
    - Ao add um novo centro de custos deverá popular a CentroCustoLojas com todas as lojas
    - Ao remover um centro de custos deverá remover da CentroCustoLojas 

![image](https://user-images.githubusercontent.com/80394522/174691133-ce0041e8-6d73-4bff-82bd-433574046278.png)
>> Imagem Excel "Cadastro de Centros de Custo"

# Criar nova aba "Centro de Custos" na tela do plano de contas (Parte 1)

![image](https://user-images.githubusercontent.com/80394522/175951327-b98b5ae1-4899-4154-8596-1d51e31d40b5.png)

> Imagem Excel "Nova aba no Plano de Contas (parte 1)"

1. No plano de contas criar uma nova aba chamada de "Centros de Custos" e migrar os recursos atuais relacionados para essa aba.
    - Criar o checkbox chkPermiteAddCentrosDeOutrasLojas
    - Carregar um grid com os "Centros de Custos vs Lojas" com as células editáveis
    - Carregar o grid de acordo com os valores contidos na tabela CentroCustoPlanoContas
    - Limitar em 100 a digitação em cada célula
    - Carregar uma linha de totais que não pode ser editável
    - A célula da linha de totais deverá ficar em vermelho caso seja <> 100
    - Possibilitar informar "0" na digitação da célula
    - Caso chkPermiteAddCentrosDeOutrasLojas esteja DESMARCADO ao salvar obrigar que todas as lojas estejam com o total em 100%
    - Salvar o grid na tabela CentroCustoPlanoContas (salvar apenas os registros que possuírem valor de 0 a 100) conforme demostrado na imagem

1. Criar label link `Replicar percentais da matris para outras lojas` 
    - Deixar ele visível apenas se o `chkPermiteAddCentrosDeOutrasLojas` estiver visível (será implementado mais adiante)
    - Ao clicar no botão copiar as porcentagens da matriz para as outras lojas.


1. Colocar um label na tela abaixo do grid com a mensagem abaixo
    ```
    Os centros de custos sem percentual informado ficarão ocultos nos lançamentos financeiros.
    Para que sejam exibidos digite 0 (zero) na célula da grade.
    ```   

1. Verificar tabindex nos componentes da tela
1. Verificar Ampulheta do mouse nos recursos criados        

[Link para a planilha com os protótipos](https://docs.google.com/spreadsheets/d/1Yn1sK54hgP0BfxfBuJbDr1zxpfpZKRfue5dw_2ZIQAE/edit?usp=sharing)    

# Criar nova aba "Centro de Custos" na tela do plano de contas (Parte 2)

![image](https://user-images.githubusercontent.com/80394522/175956971-2d970cd5-cfd6-480d-b222-6bdd949aeea0.png)

> Imagem Excel "Nova aba no Plano de Contas (parte 2)"

1. Implementar o checkbox `chkPermiteAddCentrosDeOutrasLojas`
    - Deixar ele visível apenas para clientes multilojas
    - Salvar e carregar o checkbox na tabela PlanoContas (PermiteCentroCustoLojas)

1. Implementar label "Porcentagem Total Distribuída"    
    - Deixar visível quando o checkbox `chkPermiteAddCentrosDeOutrasLojas` for marcado
    - O label acima deverá conter a soma da linha de totais
    - Se for 100% deixar o label em verde, caso contrário em vermelho
    - Não permitir salvar caso a porcentagem distribuída não esteja em 100% e o checkbox esteja marcado
    - Não terá mais a lógica anterior que deixava a linha de total em verde ou vermelha, deixar ela em amarelo

1. Implementar o botão `?`.
    - Deverá ficar visível quando o checkbox `chkPermiteAddCentrosDeOutrasLojas` estiver visível.
    - Ao clicar nele exibir a mensagem abaixo. Colocar o botão ao lado do checkbox

    ```    
    Opção: Permite adicionar Centro de ustos de outras lojas

    Habilitando esta opção podemos distribuir a despesa com os centros de custo da(s) outra(s) loja(s). Está opção será utilizada nas despesas que são compartilhadas entre lojas.

    ```

1. Verificar tabindex nos componentes da tela
1. Verificar Ampulheta do mouse nos recursos criados    

# Criar nova aba "Centro de Custos" na tela do plano de contas (Parte 3)

![image](https://user-images.githubusercontent.com/80394522/175957075-690cb89e-dd10-4185-a5f3-a93e6768c2a1.png)

> Imagem Excel "Nova aba no Plano de Contas (parte 2)"

1. Recriar o recurso anterior que estava na tela "Cadastro de Centro de Custos" ("Atualizar os Lançamentos Financeiros")
    - Criar botão `Atualizar Todos Lançamentos Financeiros`
    - Deixar visível apenas para usuário Telecon
    - Adptar o recurso antigo para inserir também as lojas do plano de contas nos lançamentos financeiros
    - Não esquecer de avalidar o `chkPermiteAddCentrosDeOutrasLojas`

1. Criar o botão `Atualizar Lançamentos Financeiros Desta Conta`
    - Deixar visível apenas para usuário Telecon
    - Utilizar mesma lógica do botão acima mas apenas para a conta atual

1. Implementar o botão `Importar Percetuais de Outra Conta`
    - Exibir formulário de busca para selecionar a conta
    - Exibir somente contas que tenham registros na tabela CentroCustoPlanoContas
    - Importar também o campo PlanoContas.PermiteCentroCustoLojas 
    - Importar as informações da conta selecionada  

1. Implementar o botão `Exportar Percetuais para outras contas`
    - Exibir formulário de busca para seleção em massa
    - Exibir somente as as contas de pagamento. ver `select * from Configuracoes where Descricao like '%EstruturaContas%'`
    - Ver possiblidade de colocar a coluna "Config. Centro Custos" com "Possui Config." ou "Não possui config." para o usuário saber que essa conta ja está configurada. Se não der discutir solução com a análise.
    - Exportar as configurações para todas as contas selecionadas
    - Exportar também o campo PlanoContas.PermiteCentroCustoLojas
    - Após a exportação caso o botão `Atualizar Todos Lançamentos Financeiros` esteja visível perguntar também se deseja atualizar os lançamentos financeiros dos planos de contas selecionados. Atualizar somente os planos de contas exportados.
 
1. Verificar tabindex nos componentes da tela
1. Verificar Ampulheta do mouse nos recursos criados    

# Alteração da grade no contas a pagar (Parte 1)

![image](https://user-images.githubusercontent.com/80394522/175953440-8ec2a2ab-9ce1-48c0-866d-c65fa3792d28.png)

> Imagem Excel "Grade No Contas a Pagar (Parte 1)"

1. Diminuir a coluna de descrição do centro de custos, deixar semelhante a imagem, assim fica mais rápido a visualização da conta vs valores

1. Alterar o label "Recarregar Percentuais dos Centros de Custos" para "Ratear Valor Automaticamente"
    - Caso não tenha nenhum valor no lançamento financeiro mostrar mensagem alertando e colocar o foco no campo de valor.

1. Alterar carregamento atual do grid
    - Carregar o grid de acordo com os centros de custos da loja configurada no plano de contas
    - Carregar apenas os centros de custos que tem valor (0 - 100%) cadastrado no plano de contas.

1. Alterar funcionamento da linha de totais
    - A linha de total deverá ser a soma das porcentagens ou valores 
    - A linha de totais deverá estar bloqueada para edição
     
1. Inserir e Implementar o label "Valor" `lblValorTotalCentroCusto`
    - Caso 1 > o usuário digitou primeiro o valor no lançamento financeiro 
        - o `lblValorTotalCentroCusto` deverá receber o valor do lançamento financeiro.

    - Caso 2 > o usuário digitou primeiro o valor na grade de centro de custos
        - o `lblValorTotalCentroCusto` deverá receber o total da coluna valor da grade.
        - o campo valor do lançamento financeiro também deverá receber esse valor.

1. Adequar as regras atuais sugestão de valor vs porcentagem

    - Caso 1 > o usuário digitou primeiro o valor no lançamento financeiro 
        - esse valor será utilizado para calcular a porcentagem de referência.

    - Caso 2 > o usuário digitou primeiro o valor na grade de centro de custos
        - o valor do `lblValorTotalCentroCusto` ou total do grid deverá utilizado para calcular a porcentagem de referência.

1. Validação do Gravar
    - Permitir gravar somente se a linha de total do percentual estiver em 100%

1. Remover label de alerta de valores diferentes (label vermelho) e alterar a lógica.  
    - Caso a linha total não esteja batendo 100% deixar a célula em vermelho
    - Caso o usuário tenha inserido o valor do lançamento financeiro e a linha de total esteja diferente desse valor deixar a célula em vermelho.

1. Criar um label link abaixo do `Ratear Valor Automaticamente`
    - "Zerar Valores": Ao clicar nesse recurso limpar todas as linhas da coluna Valor

1. Verificar tabindex nos componentes da tela
1. Verificar Ampulheta do mouse nos recursos criados        


# Alteração da grade no contas a pagar (Parte 2)

![image](https://user-images.githubusercontent.com/80394522/175953922-c219075f-8826-4b18-adeb-5a5bc5c21535.png)

> Imagem Excel "Grade No Contas a Pagar (Parte 2)"

1. Criar uma nova aba de Centros de Custos na tela de lançamentos financeiros
    - Caso PlanoContas.PermiteCentroCustoLojas esteja marcado mostrar a nova aba, do contrário deixar visível a antiga
    - Tratar para não carregar sempre o código das 2 abas, ou carregar uma ou outra
    - Carregar o grid com os centros de custos e lojas preenchidas no plano de contas
    - Carregar as colunas de totais

1. Criar a linha de totais
    - A linha de totais deve ser a soma dos valores das colunas e não deve ser editável    
    - Criar a mesma regra para coloração de vermelho da outra aba
    - Para unir as colunas da grade na linha de totais utilizar o formata grid e o recurso utilizado no relatório do pack virtual para fazer merge nas colunas

1. Criar o label de valor utilizando a mesma regra anterior (Caso 1 e Caso 2)

1. Criar os mesmos botões da outra aba do centro de custos para resetar os valores ou limpar a coluna de valor do grid

1. Criar a regra de editar os percentuais ou o valor do grid
    - A diferença é que ao editar o valor iremos distribuir a porcentagem também nas colunas proporcionalmente como o cadastrado no plano de contas

1. Criar o frame para definir se irá preecher a grade com valor monetário ou percentual

1. Verificar tabindex nos componentes da tela
1. Verificar Ampulheta do mouse nos recursos criados    

# Criação da tela para relacionar as seções e os centros de custos

![image](https://user-images.githubusercontent.com/80394522/175954074-a8bfc8ed-ee17-469d-8629-f56455605434.png)

> Imagem Excel "Relação Seções vs Centro de Custos"

1. criado o um módulo para o formulário `FrmRelacionarCustosESecoes`    
    - ele deverá vir habilitado caso o usuário esteja com o centro de custos e as seções habilitadas
    - criar no mesmo padrão que foi criado o `FrmCadCentroCusto`

1. Criar o formulário FrmRelacionarCustosESecoes    
    - Fechar com esc
    - Enter como tab
    - Ícone do sistema S na janela
    - Deve permitir resize na tela, o grid deve estar ancorado para ajudar o usuário a visualizar a coluna de totais caso tenha muitos centros de custos configurados     
    - Em baixo colocar o botão Fechar/Salvar
    - Deve abrir centralizado. 

1. Criar combo de loja

1. Carregar Grid
    - Carregar os centros de custos nas linhas
    - Carregar as seções nas colunas
    - Carregar de acordo com a tabela CentroCustoCessoes
    - Carregar de acordo com o combo de loja 
    - Ao carregar deixar em vermelho as seções que ainda não tiveram percentual configurado

1. Implementar o botão de salvar, para salvar as informações na tabela CentroCustoCessoes    

1. Criar o botão `Replicar Configuração Para Todas as Lojas`    
    - Exibir mensagem avisando com a ação não poderá ser desfeita (padrão não)
    - Dar update em CentroCustoCessoes
    - Exibir mensagem de ok

1. Verificar tabindex nos componentes da tela
1. Verificar Ampulheta do mouse nos recursos criados    


# Criar algum campo na conta para dizer que essa conta tem centro de custos ou obrigar o preenchimento

# Criar filtros de busca no financeiro para buscar lançamentos sem centro de custos ?


# criar logs nos botões de atualizar o centros de custos nos lançamentos financeiros

# alterar query relatório de resultado da loja

# vamos fazer algo para o  usuário ser obrigado a verificar o centro de custos?
