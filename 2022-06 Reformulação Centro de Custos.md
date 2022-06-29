# Épico: Reformulação Centro de Custos Por Loja

## Problema a ser resolvido

- Possibilitar centros de custos diferentes por loja
- Possibilitar configurar um centro de custos de outra loja no plano de contas
- Agilizar e melhorar o preenchimento dos centros de custos no contas a pagar
- Criar o Cadastro de Seções vs Centro de Custos

## Pontos chave da história

- Alterar a modelagem do banco de dados e as telas envolvidas

## Impactos

- O impacto será em todo o centro de custos atual, mas até o momento este recurso não foi implantado em nenhum cliente.
- Cadastro de Centro de Custos
- Plano de Contas
- Lançamentos Financeiros
- Relatório de Análise do Resultado da Loja

## Resultado Final

- Alteração no Cadastro de Centro de Custos
- Criação da aba "Centro de Custos" no Cadastro de Plano de Contas possibilitando configurar centros de custos diferentes por loja, ou entre lojas.
- Alteração da aba "Centro de Custos" nos Lançamentos Financeiros
- Criação da tela Relação Seções vs Centro de Custos
- Alteração do Relatório de Análise do Resultado da Loja
---


# Modelagem Atual do Centro de Custos

![image](https://user-images.githubusercontent.com/80394522/174605425-2bd7b260-d9c2-4a87-a259-ca2543f67291.png)
> Imagem Antiga Estrutura Centro de Custos

# Tarefa 1: Modificação do banco de dados para se adequar ao Centro de Custos por loja

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


# Tarefa 2: Remover campo porcentagem padrão no centro de custos

![image](https://user-images.githubusercontent.com/80394522/174673749-689b26fb-30b0-4a4b-8f93-57fd1e132870.png)
> Imagem print formulário "Cadastro de Centros de Custo"

1. Remover o campo `% Padrão`
1. Remover o botão e a funcionalidade `Atualizar Plano de Contas`

Obs: Devido a termos um centro de custos por loja e também podermos adicionar um centro de custos de outra loja impossibilitou criar uma forma simplista para popular o plano de contas.
Teremos um novo recurso no plano de contas para importar e exportar configurações de determinada conta.

Obs2: O recurso de atualizar os lançamentos financeiros será migrado para a tela do plano de contas. O código pode ser mantido para utilizar na outra tela.


# Tarefa 3: Testar Cadastro do Centro de Custos

1. Ao excluir um centro de custo
	- Caso Possua registros na tabela CentroCustosSecoes com porcentagem > 0
		- Bloquear e exibir mensagem dizendo que o centro de custo está configurado no cadastro de secões
	- Caso Possua registros na tabela CentroCustoPlanoContas com porcentagem > 0
		- Bloquear e exibir mensagem dizendo que o centro de custo está configurado no Plano de Contas
	- Caso Possua registros na tabela Lançamentos Financeiros Centro Custo com porcentagem > 0
		- Bloquear e exibir mensagem dizendo que o centro de custo está configurado nos Centros de Custos do Contas a Pagar (Lançamentos Financeiros)

1. Realizar testes na tabela CentroCustoLojas utilizando a tela "Cadastro de Centro de Custos"
    - Ao add um novo centro de custos deverá popular a CentroCustoLojas com todas as lojas
    - Ao remover um centro de custos deverá remover da CentroCustoLojas 

![image](https://user-images.githubusercontent.com/80394522/174691133-ce0041e8-6d73-4bff-82bd-433574046278.png)
>> Imagem Excel "Cadastro de Centros de Custo"

# Tarefa 4: Criar nova aba "Centro de Custos" na tela do plano de contas (Parte 1)

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

# Tarefa 5: Criar nova aba "Centro de Custos" na tela do plano de contas (Parte 2)

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

# Tarefa 6: Criar nova aba "Centro de Custos" na tela do plano de contas (Parte 3)

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

# Tarefa 7: Alteração da grade no contas a pagar (Parte 1)

![image](https://user-images.githubusercontent.com/80394522/175953440-8ec2a2ab-9ce1-48c0-866d-c65fa3792d28.png)

> Imagem Excel "Grade No Contas a Pagar (Parte 1)"

1. Diminuir a coluna de descrição do centro de custos, deixar semelhante a imagem, assim fica mais rápido a visualização da conta vs valores quando temos muitos centros de custos cadastrados

1. Alterar o label "Recarregar Percentuais dos Centros de Custos" para "Ratear Valor Automaticamente"
    - Caso não tenha nenhum valor no lançamento financeiro mostrar mensagem alertando e colocar o foco no campo de valor.

1. Alterar regra da aba Centro de Custos
    - Se o plano de contas não tiver nenhum centro de custos cadastrado, ocultar a aba de centro de custos

1. Alterar carregamento atual do grid
    - Carregar o grid de acordo com os centros de custos da loja configurada no plano de contas
    - Carregar apenas os centros de custos que tem valor (0 - 100%) cadastrado no plano de contas.
   
1. Inserir e Implementar o label "Valor" `lblValorTotalCentroCusto`
    - Caso 1 > o usuário digitou primeiro o valor no lançamento financeiro 
        - o `lblValorTotalCentroCusto` deverá receber o valor do lançamento financeiro.

    - Caso 2 > o usuário digitou primeiro o valor na grade de centro de custos
        - o `lblValorTotalCentroCusto` deverá receber o total da coluna valor da grade.
        - o campo valor do lançamento financeiro também deverá receber esse valor.

1. Alterar funcionamento da linha de totais
    - A linha de total deverá ser a soma das porcentagens ou valores 
    - A linha de totais deverá estar bloqueada para edição

1. Adequar as regras atuais sugestão de valor vs porcentagem

    - Caso 1 > o usuário digitou primeiro o valor no lançamento financeiro 
        - esse valor será utilizado para calcular a porcentagem de referência.

    - Caso 2 > o usuário digitou primeiro o valor na grade de centro de custos
        - o valor do `lblValorTotalCentroCusto` ou total do grid deverá utilizado para calcular a porcentagem de referência.

1. Validação do Gravar
    - Permitir gravar somente se a linha de total do percentual estiver em 0% ou 100%

1. Remover label de alerta de valores diferentes (label vermelho) e alterar a lógica.  
    - Caso a linha total não esteja batendo 100% deixar a célula em vermelho
    - Caso o usuário tenha inserido o valor do lançamento financeiro e a linha de total esteja diferente desse valor deixar a célula em vermelho.

1. Criar um label link abaixo do `Ratear Valor Automaticamente`
    - "Zerar Valores": Ao clicar nesse recurso limpar todas as linhas da coluna Valor

1. Verificar tabindex nos componentes da tela
1. Verificar Ampulheta do mouse nos recursos criados        


# Tarefa 8: Alteração da grade no contas a pagar (Parte 2)

![image](https://user-images.githubusercontent.com/80394522/175953922-c219075f-8826-4b18-adeb-5a5bc5c21535.png)

> Imagem Excel "Grade No Contas a Pagar (Parte 2)"

1. Criar uma nova aba de Centros de Custos na tela de lançamentos financeiros
    - Caso a conta tenha centro de custos configurados e a conta tenha PlanoContas.PermiteCentroCustoLojas = true 
        - Exibir a nova aba
        - Ocultar a aba antiga
    - Tratar para não carregar sempre o código das 2 abas, ou carregar uma ou outra
    - Carregar o grid com os centros de custos e lojas preenchidas no plano de contas
    - Carregar as colunas de totais

1. Criar a linha de totais
    - A linha de totais deve ser a soma dos valores das colunas e não deve ser editável    
    - Criar a mesma regra para coloração de vermelho da outra aba
    - Para unir as colunas da grade na linha de totais utilizar o formata grid e o recurso utilizado no relatório do pack virtual para fazer merge nas colunas

1. Criar o label de valor utilizando a mesma regra anterior (Caso 1 e Caso 2)

1. Criar os mesmos botões da outra aba do centro de custos para resetar os valores ou limpar a coluna de valor do grid

1. Criar a regra para a coluna de total
    - A coluna de porcentagem total deverá ser a soma das colunas da esquerda

1. Colunas `Total %` e `Total R$`
    - Deverão ter a cor de fundo em amarelo padrão do sistema
    - Deverão estar bloqueadas para edição

1. Criar o frame para definir se irá preecher a grade com valor monetário ou percentual

1. Verificar tabindex nos componentes da tela
1. Verificar Ampulheta do mouse nos recursos criados  

    ```
        Sugestão para programação: 
        - Podemos criar uma classe ou struct com 2 propriedades (valor e porcentagem).
        - Carregamos o grid e na tag de cada célula colocamos o objeto criado com a respectiva porcentagem e valor
        - Sempre manipulamos o valor e a porcentagem juntos 
        - Se usuário alternar na configuração de valor ou percentual exibe a propriedade equivalente do objeto
    ```

# Tarefa 9: Criação da tela para relacionar as seções e os centros de custos

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

# Tarefa 10:  Criar filtros de busca no financeiro para buscar lançamentos sem centro de custos

1. Criar checkbox `Centro Custos Pendente`
    - Tratar tab index
    - Colocar tooltip "Exibe apenas os lançamentos fincanceiros que não estão com centro de custos preencido. (Possuem 0% na porcentagem rateada)
    - Devará ser exibido apenas no contas a pagar
    - Deverá ser exibido apenas se o cliente tem o módulo do centro de custos habilitado

1. Implementar filtro
    - Ao marcar a configuração filtrar os registros listados.
        - Mostrar apenas registros que TENHAM o plano de contas com percentuais cadastrados
        - Mostrar registros em que o sum(LancametosFinanceirosCentroCusto.Percentual) <> 100

![image](https://user-images.githubusercontent.com/80394522/176172086-ea792746-bbfd-4f6d-b848-3dcf3fd47e5f.png)
> Imagem Protótipo "Lançamentos financeiros + combo c\check do centro de custos"




# Tarefa 11: Alterar query's do Rrelatório de Resultado da Loja

1. Alterar a consulta do procedimento abaixo

	``` csharp
	public static List<TotalizadorDespesasPorSecao> Consultar(IBanco banco,
									  string dataInicioPeriodo,
									  string dataFimPeriodo)
	```

	- No momento não tenho as tabelas criadas e nem dados na tabela, teremos que validar. Mas seguindo a modelagem será algo semelhante a consulta abaixo

	``` sql

	SELECT CCS.CodSecao , CCS.CodLojaCentroCusto, SUM( (Tabela.Valor * CCS.Percentual) / 100 ) FROM CentroCustoSecoes CCS
	INNER JOIN

			(
					SELECT sum((LF.Valor * LFCC.Percentual) / 100) [Valor] FROM LancamentosFinanceirosCentrosCusto LFCC 
					where  LFCC.CodLojaCentroCusto = CCS.CodLojaCentroCusto 
					INNER JOIN LancamentosFinanceiros LF ON LF.CodLancamentoFinanceiro = LFCC.CodLancamentoFinanceiro
					WHERE 1 = 1
					AND CONVERT(DATETIME,DATEDIFF(DAY, 0,LF.DataHoraPagamento)) >= Convert(SmallDateTime, '20220102', 126)
				AND CONVERT(DATETIME,DATEDIFF(DAY, 0,LF.DataHoraPagamento)) <= Convert(SmallDateTime, '20220207', 126)
			) AS Tabela
	```
	
1. Alterar a consulta do procedimento abaixo no join com a tabela `LancamentosFinanceirosCentrosCustos`


	``` csharp
		public static List<VisaoTatica> Consultar(IBanco banco, string dataInicioPeriodo, string dataFimPeriodo, 
							      string lojas, decimal margemLiquidaInicial, decimal margemLiquidaFinal, 
							      string descricaoProduto, int codFornecedor, string codGrupo, 
							      int codEncarte, int codLista, int ordenacao, int tipoCmv, 
							      int tipoDescricaoProduto, int tipoCodProduto, 
							      bool considerarTransferenciaCompras)

	```



1. Teremos que utilizar o LancamentosFinanceirosCentrosCusto.CodLojaCentroCusto em vez do LancamentosFinanceiros.CodLoja
    - Teremos que analisar melhor essa consulta com o banco de dados criado e dados nas tabelas.
