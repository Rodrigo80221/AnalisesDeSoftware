# Modelagem Atual do Centro de Custos

![image](https://user-images.githubusercontent.com/80394522/174605425-2bd7b260-d9c2-4a87-a259-ca2543f67291.png)


# Nova Modelagem do Centro de Custos


![image](https://user-images.githubusercontent.com/80394522/175109696-a1b91399-2c58-4942-a7fc-5dfe7206dae9.png)


## Tarefa: Modificação do banco de dados para se adequar ao Centro de Custos por loja

obs: Podemos realizar as alterações desejadas pois ainda não temos nenhum cliente utilizando o centro de custos

1. Remover relacionamento FK entre Produtos e Seções
    - Deixar invisível o campo relativo a esse campo no cadastro de produtos no VB6
    - Manter o campo no cadastro de produtos para não termos impacto

1. Alterar a tabela CentrosCusto excluindo o campo informado
    - Alterar classe Telecode

1. Criar tabela CentroCustoLojas com chave PK composta
    - Add delete e update cascade. Caso seja excluído ou alterado um registro na tabela CentrosCusto deverá ser propagada para as outras tabelas
    - Realizar um update na tabela CentroCustoLojas populando a tabela com todas lojas e todos centros de custos no banco atual

1. Criar a trigger TG_InserirCentrosCustoLojas na tabela CentrosCusto para que ao inserir um novo centro de custos na tabela CentrosCusto crie automáticamente o centro de custo para todas as lojas na tabela CentroCustoLojas

1. Criar trigger TG_InserirCentrosDeCusto na tabela de Lojas para que ao ser inserida uma nova loja seja preenchida a tabela CentrosCustoLojas

1. Recriar a tabela CentroCustoSecoes com chave PK composta de 3 elementos + Fk Composta com 2 elementos (CodLojaCentrosCusto, CodCentroCusto) para CentroCustoLojas
    - Alterar classe Telecode

1. Recriar a tabela CentroCustoPlanoContas com chave composta de 3 elementos + Fk Composta com 2 elementos (CodLojaCentrosCusto, CodCentroCusto) para CentroCustoLojas
    - Alterar classe Telecode

1. Recriar a tabela LancamentosFinanceirosCentroCusto com chave composta de 3 elementos + Fk Composta com 2 elementos (CodLojaCentrosCusto, CodCentroCusto) para CentroCustoLojas
    - Alterar classe Telecode

1. Alterar a tabela PlanoContas adicionando o campo bit PermiteCentroCustoLojas    
    - Alterar classe do Telecode


# Remover campo porcentagem padrão no centro de custos

![image](https://user-images.githubusercontent.com/80394522/174673749-689b26fb-30b0-4a4b-8f93-57fd1e132870.png)

1. Remover o campo %Padrão
1. Remover o botão e a funcionalidade "Atualizar Plano de Contas"

Obs: Devido a termos um centro de custos por loja e também podermos adicionar um centro de custos de outra loja impossibilitou criar uma forma simplista para popular o plano de contas.
Teremos um novo recurso no plano de contas para importar e exportar configurações de determinada conta.

Obs2: O recurso de atualiza os lançamentos financeiros será migrado para a tela do plano de contas


# Alterar cadastro do centro de custos

1. Manipular (add, alterar e excluír) os dados utilizando o cadastro de centro de custos atual e verificar se irá manter equivalente as 2 tabelas quanto aos centros de custos e lojas (CentrosCusto vs CentroCustoLojas)

![image](https://user-images.githubusercontent.com/80394522/174691133-ce0041e8-6d73-4bff-82bd-433574046278.png)

# Criar nova aba "Centro de Custos" na tela do plano de contas (Parte 1)

![image](https://user-images.githubusercontent.com/80394522/174691639-a825bf55-2e33-4b02-b182-220e5116c54a.png)

1. No plano de contas criar uma nova aba chamada de "Centros de Custos" e migrar os recursos atuais relacionados para essa aba.
    - Criar o checkbox chkPermiteAddCentrosDeOutrasLojas
    - Carregar um grid com os "Centros de Custos vs Lojas"
    - Deixar editavel as celulas referentes as porcentagens
    - Carregar um linha de totais
    - Limitar em 100% a digitação em 1 célula
    - Possibilitar informar "0" na digitação da célula
    - Caso chkPermiteAddCentrosDeOutrasLojas esteja desmarcado ao salvar obrigar que todas as lojas estejam com o total em 100%
    - Salvar o grid na tabela CentroCustoPlanoContas (salvar apenas os registros que possuírem valor de 0 a 100)
1. Criar o botão `Replicar percentais da matris para outras lojas` Caso chkPermiteAddCentrosDeOutrasLojas esteja desmarcado deixar o botão visível. Ao clicar no botão copiar as porcentagens da matriz para as outras lojas.
    - Tratar para carregar o grid de acordo com a tabela CentroCustoPlanoContas
1. Salvar e carregar o checkbox na tabela PlanoContas
1. Colocar um label na tela com a mensagem abaixo
    ```
    Os centros de custos sem percentual informado ficarão ocultos nos lançamentos financeiros.
    Para que sejam exibidos digite 0 (zero) na célula da grade.
    ```
   

[Link para a planilha com os protótipos](https://docs.google.com/spreadsheets/d/1Yn1sK54hgP0BfxfBuJbDr1zxpfpZKRfue5dw_2ZIQAE/edit?usp=sharing)    

# Criar nova aba "Centro de Custos" na tela do plano de contas (Parte 2)

![image](https://user-images.githubusercontent.com/80394522/174691186-17f5f0e5-aeb5-45b8-9564-aeff0f6d1180.png)

1. Ao habilitar o checkbox chkPermiteAddCentrosDeOutrasLojas 
    - exibir o label `Porcentagem total distribuída`. O label deverá mostrar o total a soma das colunas da linha de total
    - implementar o botão `?`. Ao clicar nele exibir a mensagem abaixo. Colocar o botão ao lado do checkbox

    ```    
    Opção: Permite adicionar Centro de ustos de outras lojas

    Habilitando esta opção podemos distribuir 100% da despesa nos centros de custo da loja ou compartilhar esse custo com outra(s) loja(s). Está opção será utilizada nas despesas que são compartilhadas entre lojas.

    ```

# Criar nova aba "Centro de Custos" na tela do plano de contas (Parte 3)

1. Adaptar o recurso atual de atualizar os lançamentos financeiros. Para inserir o código da loja na tabela LancamentosFinanceirosCentroCusto. Trocar o nome do botão de "Atualizar Lançamentos".

1. Criar um botão com o mesmo recurso atual de atualizar os lançamentos financeiros mas apenas da conta posicionada. O nome do botão deverá ser "Atualizar Lançamentos Deste Plano de Contas".
    - Deixar este botão visível apenas se o botão "Atualizar Lançamentos" estiver visível. 

1. Implementar o botão `Importar Percetuais de Outra Conta`
    - Exibir formulário de busca para selecionar a conta
    - Exibir somente contas que tenham registros na tabela CentroCustoPlanoContas
    - Importar também o campo PlanoContas.PermiteCentroCustoLojas 
    - Importar as informações da conta selecionada  

1. Implementar o botão `Exportar Percetuais para outras contas`
    - Exibir formulário de busca para seleção em massa
    - Exibir todas as contas de pagamento. ver `select * from Configuracoes where Descricao like '%EstruturaContas%'`
    - Exportar as configurações para todas as contas selecionadas
    - Exportar também o campo PlanoContas.PermiteCentroCustoLojas
    - Após a exportação caso o botão "Atualizar Lançamentos" esteja visível perguntar também se deseja atualizar os lançamentos financeiros dos planos de contas selecionados. Atualizar somente os planos de contas exportados.
 

# Alteração da grade no contas a pagar (Parte 1)

![image](https://user-images.githubusercontent.com/80394522/174691224-e4bfeb32-365c-42ca-badc-1302d7125c5d.png)

1. Diminuir a coluna de descrição do centro de custos, deixar semelhante a imagem, assim fica mais rápido a visualização da conta vs valores

1. Carregar o grid de acordo com os centros de custos da loja configurada no plano de contas
    - Alterar o carregamento do grid atual. Carregar apenas os centros de custos que tem valor (0 - 100%) cadastrado no plano de contas. Essa questão servirá para facilitar os lançamentos, pois teremos poucas opções para informar 

1. Adequar as regras atuais de digitação conforme a imagem abaixo
    - Colocar o label "Valor: " semelhante a imagem. Caso o usuário digite o valor no lançamento financeiro o valor do label deverá receber o valor do lançamento financeiro.
    - Caso o usuário não digite o valor no lançamento financeiro, o label deverá receber o valor total da coluna valor no grid. E o campo valor do lançamento financeiro também deverá receber esse valor.
    - A coluna valor e porcentagem do grid deverão estar bloqueadas para digitação
    - A linha de total deverá ser a soma das porcentagens ou valores    
    - Caso o usuário tenha informado o valor do lançamento financeiro esse valor será utilizado para calcular a porcentagem de referência.
    -  Caso o usuário não tenha informado o valor do lançamento financeiro. O valor para a porcentagem de referência será o label que está com a linha de total do grid.
    - Permitir gravar somente se o percentual estiver em 100%

1. Remover label de alerta de valores diferentes (label vermelho)    
    - Caso a linha total não esteja batendo 100% deixar a célula em vermelho
    - Caso o usuário tenha inserido o valor do lançamento financeiro e a linha de total esteja diferente desse valor deixar a célula em vermelho.

1. Criar um botão ou label link 
    - "Zerar Valores": Ao clicar nesse recurso limpar todas as linhas da coluna Valor



![image](https://user-images.githubusercontent.com/80394522/175376155-0ad1ad5b-b1b8-4748-b288-0f90d2c498d6.png)


# Alteração da grade no contas a pagar (Parte 2)

![image](https://user-images.githubusercontent.com/80394522/174691257-a0e6f9c2-4abd-4a2b-bfc0-63725c6d7eef.png)

1. Criar uma nova aba de Centros de Custos na tela de lançamentos financeiros
    - Caso PlanoContas.PermiteCentroCustoLojas esteja marcado mostrar a nova aba, do contrário deixar visível a antiga
    - Tratar para não carregar sempre o código das 2 abas, ou carregar uma ou outra
    - Carregar o grid com as informações dos centros de custos e as lojas cadastradas no plano de contas
    - Carregar as colunas de totais

1. Criar o label "Porcentagem Total Distribuída"
    - O label deverá receber a soma da porcentagem de todas as células do grid 

1. Criar o label "Valor Total Distribuído"
    - C

# Criação da tela para relacionar as seções e os centros de custos

![image](https://user-images.githubusercontent.com/80394522/174691282-faa96fbf-627a-4553-8bbd-1095e9e5fbf0.png)


# Criar filtros de busca no financeiro para buscar lançamentos sem centro de custos ?
