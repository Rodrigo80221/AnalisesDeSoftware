# Tela inicial
- Alterado o componente de data -- OK --
- O combo de loja deverá permitir selecionar 1 ou mais lojas -- OK --
- Inserido botão de Configurações -- OK --
- Adicionada coluna de pedidos de compra -- OK --
- Adicionado botão fechar no rodapé do formulário -- OK --
- Ao alterar o valor de compra devemos atualizar a porcentagem -- OK --
- melhorar a questão da digitação da diferença, atualizar ou ao pressionar enter ou a retirar o foco do campo, ao alterarmos da erro, ou somente corrigir o erro -- OK --
- deixar os campos de planejamento somente para visualizar em amarelo -- OK --
- fixar totalizador na ordenação -- OK --
- resolver problemas de arredondamento, pois sempre fica em vermelho -- OK --
- bater com o relatório de diferença de compra e venda -- OK --
- range de tolerância para o alerta de diferença nos labels -- OK --
- verificar bug , no mês de setembro está mostrando 30 dias mesmo tendo 31 dias no banco -- OK --
- Verificar módulos, deixar em amarelo caso não seja possível editar -- OK --

![image](https://user-images.githubusercontent.com/80394522/187738884-8567eacf-2acb-4238-9182-eed09a8db42a.png)

# Botão Configurações
- Migrada a configuração das transferências -- OK --
- Criada configuração de execeção para os grupos -- OK --

![image](https://user-images.githubusercontent.com/80394522/187539513-c88d61ae-9704-4109-9355-cb1c1eec1a16.png)

# Botão Criar Planejamento - Etapa 1
- Etapa para selecionar a origem do planejamento  -- OK --
- Se selecionarmos um planejamento anterior e ele tiver no formato antigo, devemos avisar o usuário que ficará no formato antigo  -- OK --

![image](https://user-images.githubusercontent.com/80394522/188020774-5fb9c8b9-a905-439e-805b-4664f85bb0ed.png)

# Botão Criar Planejamento - Etapa 2
- Etapa para selecionar as lojas  -- OK --
- Caso já exista um planejamento criado para a loja ela deverá vir desmarcada com a linha desabilitada (cor cinza claro) impossibilitando marcar novamente -- OK --

![image](https://user-images.githubusercontent.com/80394522/188029517-0efa48b3-2520-472c-9ac1-bc6d0d0c04bd.png)

# Botão Criar Planejamento - Etapa 3
- Etapa para definir os valores de venda para o planejamento -- OK --
- É possível editar o valor de crescimento percentual -- OK --

![image](https://user-images.githubusercontent.com/80394522/188029551-0896d4cf-6181-4d24-a803-7cd23da72cd5.png)

# Botão Criar Planejamento - Etapa 4
- Etapa para definir os valores de compra para o planejamento -- OK --
- Opção para não importar as informação de compra podendo definir um percentual padrão -- OK --

![image](https://user-images.githubusercontent.com/80394522/188020925-e98b26ef-9bee-4946-b776-7a6d5a334694.png)


# Tela inicial - Planejamento Criado
- Corrigida a formatação nos campos -- OK --
- Adicionados os subgrupos no relatório  -- OK --
- Adicionado + níveis de subgrupos  -- OK --
- Adicionado níveis de coloração nas linhas -- OK --
- Combo de Lojas terá o filtro "Todos" os componentes da tela ficarão somente leitura -- OK --
- Botão de imprimir foi movido para o rodapé do formulário -- OK --
- Ao alterarmos a porcentagem de venda ou compa de um subgrupo, deve atualizar o percentual do pai  -- OK --
- Devemos adicionar o valor de pedidos de compra na grade e somarmos na coluna diferença -- OK --
- Remover a ordenação para mantermos a estrutura dos grupos pais e filhos  -- OK --
- o total deve somar somente a porcentagem dos pais  -- OK --

![image](https://user-images.githubusercontent.com/80394522/188015285-60bc97f7-a681-4307-9264-4a2dc50808ac.png)


# Tela vendas dia
- ajustar layout  -- OK --
- carregar combo com base nos grupos da tabela  -- OK --
- alterar a query das vendasdia real para agrupar por grupo `public static List<PlanejamentoCompraVendaDia> ConsultarPorPlanejamentoGrupo`

![image](https://user-images.githubusercontent.com/80394522/191285379-f59d0f1e-541f-44d5-8e21-72b2a2198c45.png)

---
---
---








## Tarefa 1: Ajustes nas formatações dos campos
1. Ajustar formatação dos totalizadores Venda Planejada e Compra Planejada para "#0.00" no formulário FrmPlanejamentoCompraVenda" (ao digitar formatar o campo)
1. Ajustar formatação dos totalizadores Venda Planejada e Compra Planejada para "#0.00" no formulário FrmPlanejamentoCompraVendaDia" (ao digitar formatar o campo)
1. Ajustar campos da grade do formulário "FrmPlanejamentoCompraVenda", todos os campos moeda deverão receber a formatação "#0.00"
1. Ajustar campos da grade do formulário "FrmPlanejamentoCompraVendaDia", todos os campos moeda deverão receber a formatação "#0.00"
1. Não esquecer de ajustar também as linhas de totalizadores das grades


## Tarefa 1: Melhorias de usabilidade no campo Diferença Planejada
> Realizar as alterações abaixo no FrmPlanejamentoCompraVenda

1. Ao clicar no campo selecionar todo o texto
1. Permitir inserir um número fracionado ex "50,60". Alterar os locais que consomem esse campo para não gerar erros
1. Ao limparmos o campo e aguardarmos uns segundos gera o erro abaixo, corrigir o erro 
    ```
    ---------------------------
    Atenção
    ---------------------------
    A cadeia de caracteres de entrada não estava em um formato correto.
    ---------------------------
    OK   
    ---------------------------
    ```

1. Atualmente ao alterarmos a porcentagem é calculada a compra planejada, realizar a mesma programação deste campo na compra planejada.
    - Ao alterar o campo "Compra Planejada" calcular a porcentagem com no máximo 2 casas decimais
    - Ao clicar no campo "Compra Planejada" selecionar todo o campo
    - Ao alterar após alguns segundos realizar o cálculo assim como no campo de diferença
    - ao digitar formatar o campo "#0.00"

## Tarefa 1: Remover reordenação das colunas
1. No FrmPlanejamentoCompraVenda remover reordenação ao clicar no cabeçalho da coluna 
1. No FrmPlanejamentoCompraVendaDia remover reordenação ao clicar no cabeçalho da coluna 


## Tarefa 1: Criar tolerância para os alertas em vermelho
1. Criar verifica banco, inserir na tabela de configurações a configuração PlanejamentoCompraVendaTolerancia com o valor 5
1. Consumir essa configuração nos formulários "FrmPlanejamentoCompraVenda" e "FrmPlanejamentoCompraVendaDia"
    - Deixar os alertas em vermelho apenas se for > que a porcentagem de tolerância 


# Tarefa 1: Ajustar configurações de módulos

![image](https://user-images.githubusercontent.com/80394522/191273621-abc01015-9dca-47a9-af38-761571574657.png)

1. Alterar o módulo `Planejamenco de Compra e Venda` para ter as opções "Incluir/ Alterar/ Excluir"
1. Excluir o módulo `Planejamenco de Compra e Venda (edição)` 
1. Se o módulo de edição estiver habilitado marcar as opções "Incluir/ Alterar/ Excluir" para o operador
1. Inserir um botão de Configurações apenas para já realizar essa programação
1. Alterar a tela para implementar o "Incluir/ Alterar/ Excluir"
    - Não esquecer de retirar o código referente ao módulo excluído
    - Desabilitar os botões
    - Programar os campos com fundo em amarelo 
    - verificar a utilização da propriedadde `_permissaoParaEdicao`
> Tratar conforme a imagem abaixo.

![image](https://user-images.githubusercontent.com/80394522/191276484-5d577a73-f0b3-4cdc-b423-7c7ec59cce7a.png)


# Tarefa 1: Implementar Botão Configurações
1. Criar formulário FrmPlanejamentoCompraVendaConfig
    - Implementar o layout conforme a imagem anexa
    - O caption do formulário será "Planejamento de Compra e Venda - Configurações"
1. Implentar padrões Telecon Abaixo
- Fechar com esc
- Enter como tab
- Ícone na janela
- Abrir como modal
- Não deve ter os botão de maximizar
- Não deve permitir resize na tela
- Deve abrir centralizado. 
- Deve estar correto quanto ao tab index ao finalizar a tela
- F4 no campo de código abre a busca de grupos
- Implementar tratamentos da ampulheta do mouse no click dos botões de lupa e gravar
1. Migrar a configuração das transferências para esse formulário
    - Retirar dos formulários "FrmPlanejamentoCompraVenda" e "FrmPlanejamentoCompraVendaDia"
    - Ajustar programação
1. Implementar grade de grupos, os ícones serão os mesmos da tela do pack virtual 
    - Ao salvar, salvar os grupos que estão na grade em uma configuração na tabela de configurações concatenadas. ex. "17,17.04"

![image](https://user-images.githubusercontent.com/80394522/187539513-c88d61ae-9704-4109-9355-cb1c1eec1a16.png)


# Tarefa 1: Ajustar layout no FrmPlanejamentoCompraVenda
> Utilizar resolução 1024x768
1. Alterar o componente de data
    - inserir componente padrão de outros relatórios
    - ajustar o código para utilizar o novo componente
1. Adicionar e implementar botão de fechar
1. Tratar tab index
> As alterações na grade e como de loja não serão realizadas nesta tarefa

![image](https://user-images.githubusercontent.com/80394522/191282474-536fc8da-447a-406d-890a-a9b43e07038c.png)

# Tarefa 1: Validar banco Gestão Relatórios

1. Ao abrir o formulário "FrmPlanejamentoCompraVenda" realizar um teste para ver se existe o banco de dados do gestão relatórios, caso não exista exibir uma mensagem assim como no formulário FrmResultadoDRE

1. Colocar no rodapé (embaixo a esquerda) dos formulários "FrmPlanejamentoCompraVenda" e "FrmPlanejamentoCompraVendaDia" um label exibindo a data de início e fim da base de dados disponível no gestão relatórios
    - Fazer no mesmo formato que foi feito no formulário FrmResultadoDRE

![image](https://user-images.githubusercontent.com/80394522/191598775-5d405564-b9b8-4799-9a74-c08aa672d544.png)

# Tarefa 1: Alterar a origem dos dados de compra e venda atual (gestão relatórios)

1. Criar um conexão com o banco Gestão Relatórios assim como no FrmResultadoDRE

1. Alterar o procedimento `FrmPlanejamentoCompraVendaDia.ConsultarPorPlanejamentoGrupo`, passando para ele a conexão com o gestão relatórios. 
    - Adaptar a consulta para buscar nas tabelas VendasDia, NotasSaidas, NotasEntradas
    - Podemos usar como base as consultas da classe `Telecon.GestaoComercial.Biblioteca.Relatorios.ResultadoLoja.VisaoGeral`

1. Criar um conexão com o banco Gestão Relatórios assim como no FrmResultadoDRE

1. Alterar o procedimento `FrmPlanejamentoCompraVenda.ConsultarPorPlanejamento`, passando para ele a conexão com o gestão relatórios. 
    - Adaptar a consulta para buscar nas tabelas VendasDia, NotasSaidas, NotasEntradas
    - Podemos usar como base as consultas da classe `Telecon.GestaoComercial.Biblioteca.Relatorios.ResultadoLoja.VisaoGeral`    

1. Validar determinado período para que esteja retornando os mesmo dados gestão vs gestão relatórios.   
    - Utilizar transferências e notas de estorno
    - O Victor tem um banco com algumas notas de estorno


# Tarefa 1: Incluir nas compras a coluna de pedidos

1. Adicionar tabela PedidoCompra e PedidoCompraProdutos no banco gestão relatórios
    - Utilizar os campos das consultas abaixo
    - Chamar o procedimento `mdlGestao.CriarEstruturaDeViewsDoGestaoRelatorios` no verifica banco assim como foi feito no procedimento `mdlAtualizaBanco3.fRecriarTabelasGestaoRelatorios`
    - Criar view no procedimento `mdlGestao.CriarEstruturaDeViewsDoGestaoRelatorios`
    - Adicionar store procedure `SP_RELATORIOS_BI_DIMENSOES` no procedimento `frmConfigGestaoRelatorios.fCriarEstruturaGestaoRelatorios`
    - Incluir chamada no procedimento `frmConfigGestaoRelatorios.fCarregarDadosGestaoRelatorios`

    ``` sql
    SELECT Codigo, DataPedido, CodLoja FROM [dbo].[PedidoCompra] 

    SELECT CodPedidoCompra, CodProduto, QtdProduto  FROM [dbo].[PedidoCompraProdutos] 
    ```
    - Testar funcionamento e Validar tarefa com o Samir

1. Adicionar  na classe PlanejamentoCompraVendaGrupo a propriedade PedidoRealRS
    - Alterar o get da propriedade CompraDiferencaRS = (CompraRealRS + PedidoRealRS) - CompraPrevistaAcum
    - Alterar o get da propriedade CompraDiferencaPerc = CompraDiferencaRS / (CompraRealRS + PedidoRealRS) * 100
    - Adicionar no procedimento `ObterPlanejamentoCompraVendaGrupoTotal`

1. Alterar o procedimento `FrmPlanejamentoCompraVenda.ConsultarPorPlanejamento` para retornar os pedidos em aberto por grupo

1. Criar coluna na grade do `FrmPlanejamentoCompraVenda` conforme o template, tratar para carregar a coluna 
    - Realizar testes para ver se a funcionalidade irá funcionar corretamente

1. Alterar a impressão para imprimir a nova coluna `FrmPlanejamentoCompraVenda.btnImprimir_Click`

1. Adicionar  na classe PlanejamentoCompraVendaDiaTotal a propriedade PedidoRealRS
    - Alterar o get da propriedade CompraDiferencaRS = (CompraRealRS + PedidoRealRS) - CompraPrevistaAcum

1. Alterar o procedimento `FrmPlanejamentoCompraVendaDia.ConsultarPorPlanejamentoGrupo`, para retornar os pedidos em aberto por grupo e dia

1. Criar coluna na grade do `FrmPlanejamentoCompraVendaDia` conforme o template, tratar para carregar a coluna 
    - Realizar testes para ver se a funcionalidade irá funcionar corretamente

1. Criar pedidos de compra e verifica o funcionamento, realizar os ajustes necessários

1. Alterar a impressão para imprimir a nova coluna `FrmPlanejamentoCompraVendaDia.btnImprimir_Click`



# Tarefa 1: Ajustar layout no FrmPlanejamentoCompraVendaDia
> Utilizar resolução 1024x768
1. Alterar o componente de data
    - inserir componente padrão de outros relatórios
    - ajustar o código para utilizar o novo componente
1. Adicionar e implementar botão de fechar
1. Tratar tab index
> As alterações na grade e combo de loja não serão realizadas nesta tarefa

![image](https://user-images.githubusercontent.com/80394522/191285379-f59d0f1e-541f-44d5-8e21-72b2a2198c45.png)


# Tarefa 1: Implementar combo de lojas
> Programar no FrmPlanejamentoCompraVenda e FrmPlanejamentoCompraEVendaDia

1. Inserir combo de loja com opção de uma ou mais lojas nos 2 formulários
    - possui exemplo no relatório pack virtual
    - ajustar todos os lugares que utilizam esse combo para quando estiver selecionada apenas 1 loja funcionar da mesma forma

1. Ao selecionar 2 ou mais lojas mostrar a tela somente como consulta
    - Bloquear os campos de edição da grade
    - Bloquear os texts para não permitir edição 
    - Caso tenha selecionado 1 ou mais lojas ao clicar nos campos acima mostrar uma mensagem `Para editar selecione apenas 1 loja!`
    - Alterar FrmPlanejamentoCompraVenda.ListarDados() para carregar o objeto `_planejamento` com os dados de todas as lojas
    - Alterar FrmPlanejamentoCompraVenda.ListarDados() para carregar o objeto `_grupos` com os dados de todas as lojas
    - Alterar FrmPlanejamentoCompraVendaDia.ListarDados() para carregar o objeto `_planejamento` com os dados de todas as lojas
    - Alterar FrmPlanejamentoCompraVendaDia.ListarDados() para carregar o objeto `_grupos` com os dados de todas as lojas   
    - Ajustar FrmPlanejamentoCompraVendaDia quanto a questão do combo de grupos "Todos" ou apenas 1
    - Ajustar formulários para ficarem compatíveis com as novas alterações 
    - Verificar a impressão

``` C sharp

    // alteração para o procedimento PlanejamentoCompraVendaGrupo.ConsultarPorPlanejamento

                sql += " DECLARE @ValorVendaPlanejada AS FLOAT = (SELECT SUM(PCV.ValorVendaPlanejada) FROM PlanejamentoCompraVenda PCV WHERE PCV.Ano = 2022 and PCV.mes = 9 and PCV.codloja in (1, 2, 3)) \n";

                sql += " INSERT INTO @RESULTADO (CodPlanejamento, CodGrupo, PercentualVendaPlanejada, PercentualMargemPlanejada, VendaRealRS, CompraRealRS) \n";
                sql += " SELECT 1, CodGrupo, (T.VendaPlanejada * 100) / @ValorVendaPlanejada PercentualVendaPlanejada , (T.CompraPlanejada * 100) / T.VendaPlanejada PercentualMargemPlanejada ,0 ,0 from \n";
                sql += " ( \n";
                sql += " SELECT CodGrupo, SUM((PercentualVendaPlanejada* PCV.ValorVendaPlanejada) / 100) VendaPlanejada, SUM(((PercentualMargemPlanejada * ((PercentualVendaPlanejada * PCV.ValorVendaPlanejada) / 100)) / 100)) CompraPlanejada \n";
                sql += " FROM PLANEJAMENTOCOMPRAVENDAGRUPOS PCVG \n";
                sql += " INNER JOIN PlanejamentoCompraVenda PCV ON PCVG.CodPlanejamento = PCV.CodPlanejamento AND PCV.Ano = 2022 and PCV.mes = 9 and PCV.codloja in (1, 2, 3) \n";
                sql += " GROUP BY CodGrupo \n";
                sql += " ) as T \n";
```

``` C sharp
// ideia da consulta para carregar o _planejamento
            var sql = "select 14 CodPlanejamento , 1 CodLoja , 2022 Ano, 9 Mes, sum(ValorVendaPlanejada) ValorVendaPlanejada, sum(ValorCompraPlanejada) ValorCompraPlanejada  " +
                "from PlanejamentoCompraVenda where Ano = 2022 and mes = 9 and codloja in (1,2,3)";
```                





![image](https://user-images.githubusercontent.com/80394522/192026543-54989ebe-3585-4c85-9e6c-cbea9ece174f.png)

![image](https://user-images.githubusercontent.com/80394522/192360830-4b704a24-28e7-4cd0-b6b0-cc3405cd39c3.png)

![image](https://user-images.githubusercontent.com/80394522/192031544-caab729a-b90b-46d7-87ea-a5109d2d6c4e.png)

![image](https://user-images.githubusercontent.com/80394522/192055262-585256b6-09ec-481c-a79a-96f6c26157ae.png)

![image](https://user-images.githubusercontent.com/80394522/192057000-0bf04695-a586-460d-a722-0832087f8454.png)


# Tarefa 1: Criar formulário FrmPlanejamentoCompraVendaInserir (Parte 1)

![image](https://user-images.githubusercontent.com/80394522/192026543-54989ebe-3585-4c85-9e6c-cbea9ece174f.png)

1. Criar formulário FrmPlanejamentoCompraVendaInserir
    - Implementar o layout conforme a imagem anexa
    - Deverá ter um componete tabControl assim como no sped
        - Quando estiver compilado subir o componente escondendo as abas
    - O caption do formulário será "Planejamento de Compra e Venda - Criar Planejamento"
    - O formulário deverá ser chamado pelo botão "Criar planejamento" apenas quando estiver no modo criar
1. Implentar padrões Telecon Abaixo
    - Fechar com esc
    - Enter como tab
    - Ícone na janela
    - Abrir como modal
    - Não deve ter os botão de maximizar
    - Não deve permitir resize na tela
    - Deve abrir centralizado. 
    - Deve estar correto quanto ao tab index ao finalizar a tela
    - Implementar tratamentos da ampulheta do mouse no click dos botões avançar e retornar
1. A selecionar uma opção exibir o campo de data da direita relacionada a opção    
1. Na opção importar planejamento anterior trazer posicionado no mês anterior
1. Na opção (Mensal) trazer posicionado no mesmo mês mas no ano passado
1. Na opção (Período) trazer posicionado nos últimos 3 meses
1. Ao abrir a tela abrir como padrão a opção (Mensal)
1. Tratar tab index
# Tarefa 1: Criar formulário FrmPlanejamentoCompraVendaInserir (Parte 1)

![image](https://user-images.githubusercontent.com/80394522/192026543-54989ebe-3585-4c85-9e6c-cbea9ece174f.png)

1. Se o usuário selecionou a opção "Criar novo planejamento"
    - alterar o caption do botão avançar para "Inserir Planejamento" 
        - ao clicar em "Inserir Planejamento" fechar o formulário e criar o planejamento da mesma forma que anteriormente
        - alterar para adicionar os subgrupos no relatório, tanto na tabela PlanejamentoCompraVendaGrupo quanto na PlanejamentoCompraVendaDia
    - Fazer tratamento para respeitar as configurações de grupos (não importar os grupos configurados nas exceções)

# Tarefa 1: Criar formulário FrmPlanejamentoCompraVendaInserir (Parte 2)

![image](https://user-images.githubusercontent.com/80394522/192026543-54989ebe-3585-4c85-9e6c-cbea9ece174f.png)

1. Se o usuário selecionou a opção "Importar planejamento anterior" , alterar o caption do botão avançar para "Inserir Planejamento"
    - Ao clicar em "Importar planejamento anterior" fechar o formulário e criar o planejamento da mesma forma que o anterior mas importando de acordo com a data definida
    - Se o usuário selecionar um planejamento feito versão antiga (não possui subgrupos) alertar que o planejamento foi feito em uma versão antiga que não possui o planejamentos de compra e venda nos subgrupos
    - Verificar se está importando corretamente meses com número diferentes de dias
1. Nas demais opções ao clicar em avançar mudar para a próxima aba


# Tarefa 1: Criar formulário FrmPlanejamentoCompraVendaInserir (Parte 3)
> Implementar aba 2 "Origem dos dados de compra"
1. Ao selecionar a opção percentual padrão exibir o text de percentual com o default 75
    - o text pode ter uma máscara ou ter o "%" fora do campo...
1. Ao selecionar a opção custo das mercadorias exibir os radio buttons , deixar o custo gerencial como padrão
1. Trazer como padrão a opção custo das mercadorias vendidas + custo gerencial
1. Ao clicar em avançar mudar para a próxima aba
1. Tratar tab index

![image](https://user-images.githubusercontent.com/80394522/192360830-4b704a24-28e7-4cd0-b6b0-cc3405cd39c3.png)


# Tarefa 1: Criar formulário FrmPlanejamentoCompraVendaInserir (Parte 4)

![image](https://user-images.githubusercontent.com/80394522/192031544-caab729a-b90b-46d7-87ea-a5109d2d6c4e.png)

> Implementar a aba 3 "Defina para quais lojas o planejamento será criado"
1. Criar tela conforme template
    - Carregar grade conforme a tabela de lojas
    - Criar opção marcar e desmarcar todas 
1. Verificar se já existe um planejamento criado para alguma das lojas, se existir desabilitar a loja no grid e deixar desmarcado o checkbox     
1. Ao clicar em avançar mudar para a próxima aba
    - Se o usuário não selecionar nenhuma loja desabilitar o botão de avançar
1. Ao clicar em retornar voltar para a aba anterior
1. Tratar tab index


# Tarefa 1: Criar formulário FrmPlanejamentoCompraVendaInserir (Parte 5)
> Implementar o avançar da aba 3 "Defina para quais lojas o planejamento será criado"

1. Criar um objeto PlanejamentoCompraVenda, PlanejamentoCompraVendaGrupo e PlanejamentoCompraVendaDia para cada loja com todos os dados necessários de acordo com a consulta anexa
    - Fazer tudo em tread e deixar um gif de carregando na grade de lojas, assim como nos outos relatórios do c#

> Abaixo o link da consulta sql para inserir com base em dados anteriores

``` HTML
https://github.com/Rodrigo80221/AnalisesDeSoftware/blob/main/SQL/Consulta%20principal%20Planejamento%20de%20Compra%20e%20Venda%20Melhorias.md
```

# Tarefa 1: Criar formulário FrmPlanejamentoCompraVendaInserir (Parte 6)
> Implementar a aba 4 "Definição dos planejamento das vendas"

1. Listar os objetos PlanejamentoCompraVenda na grade (campo de venda)
1. Criar uma coluna editável para ser digitado um percentual
1. Na coluna venda planejada adicionar o percentual e exibir na coluna (não atualizar ainda nos objetos, só no final para evitar erro caso o usuário retorne nas abas seguintes)
1. Ao clicar em avançar mudar para a próxima aba
1. Ao clicar em retornar voltar para a aba anterior
1. Tratar tab index

![image](https://user-images.githubusercontent.com/80394522/192055262-585256b6-09ec-481c-a79a-96f6c26157ae.png)


# Tarefa 1: Criar formulário FrmPlanejamentoCompraVendaInserir (Parte 7)

![image](https://user-images.githubusercontent.com/80394522/192057000-0bf04695-a586-460d-a722-0832087f8454.png)

> Implementar a aba 5 "Definição dos planejamento das compras"
1. Listar os objetos PlanejamentoCompraVenda na grade (campo de compra)
1. Criar uma coluna editável para ser alterado o percentual de diferença, ao alterar o percentual, alterar o valor da compra planejada (CompraPlanejada = (%Diferença * VendaPlanejada) / 100)
1. Na coluna compra planejada adicionar o percentual de crescimento, exibir a porcentagem de compra calculada com a venda planejada da aba anterior (compraPlanejada * 100) / (vendaPlanejada * %DiferençaCompraEVenda)
1. Ao clicar em Inserir Planejamento    
    - Atualizar o valor de venda planejada e compra planejada no objeto PlanejamentoCompraVenda de todas as lojas
    - inserir os dados no banco de dados de acordo com o link abaixo
    - fazer em thread e exibir o gif de carregamento
    - atualizar a tela PlanejamentoCompraVenda 
1. Tratar tab index

# Tarefa 1: Alterar lógica do combro de grupos no formulário FrmPlanejamentoCompraVendaDia
1. Listar todos grupos e subgrupos no combo
    - Ao abrir a tela através de um link de subgrupo no FrmPlanejamentoCompraVendaGrupo corrigir para não dar erro    

# Tarefa 1: Alterar lógica de grupos > subgrupos do formulário FrmPlanejamentoCompraVendaGrupo (parte 1)
1. Alterar a lógicas dos totalizadores para somar apenas os registros relacionados aos grupos pais 
    - Atualmente está somando todas as linhas 

# Tarefa 1: Alterar lógica de grupos > subgrupos do formulário FrmPlanejamentoCompraVendaGrupo (parte 2)
> Realizar tratamentos nas edições das colunas 

1. Ao alterar a porcentagem de venda de um subgrupo, atualizar o valor do grupo pai com a soma de todos os filhos
    - Verificar que o valor do grupo pai também salvou automaticamente

1. Ao alterar o valor de venda planejada do subgrupo, atualizar o valor do grupo pai com a soma de todos os filhos
    - Verificar que o valor do grupo pai também salvou automaticamente

1. Ao alterar a diferença de compra de um subgrupo, atualizar o valor do grupo pai com a soma de todos os filhos





## Tarefa 1: Adicionar recurso de retrair e expandir linhas 
1. Adicionar mais 2 colunas a esquerda, uma para o sinal "+/-" outra para colocar o codigo do grupo
    - Ao carregar a grande, quando for um grupo pai (ver pelo código) colocar o sinal de "+" na nova coluna 
        - Após carregar toda a grade dar um hide em todos os filhos 
    - Ao clicar mudar o sinal para "-" e dar um show nas linhas dos grupos filhos 


## Tarefa 1: Adicionar níveis de coloração nas linhas dos grupos pais no formulário FrmPlanejamentoCompraVendaGrupo
1. Os grupos pais deverão ter coloração mais escura de acordo com o layout abaixo
    - Utiliza a coluna código do grupo 
    - Utilizar o mesmo padrão dos outros relatórios c# (utilizar classe cores do genérico)

![image](https://user-images.githubusercontent.com/80394522/191282474-536fc8da-447a-406d-890a-a9b43e07038c.png)

## Tarefa 1: Teste botão "Repetir percentuais para todos os grupos" no FrmPlanejamentoCompraVendaDia
1. Verificar se se manteve a funcionalidade após as alterações
    - Testar com os subgrupos

## Tarefa 1: Testes, testar Bug 30 dias
1. Foi relatado pelo comercial que temos bugs ao importar planejamentos de meses com menos dias para meses com 31 dias 
    - Realizar testes e realizar os ajustes necessários

## Tarefa 1: Testes verificar os dados de venda importada    
    - Bater os dados de venda importada (Inserir Planejamento) com o relatório de diferença de compra e venda (os clientes atualmente buscam os valores desse relatório)




![image](https://user-images.githubusercontent.com/80394522/192057000-0bf04695-a586-460d-a722-0832087f8454.png)


![image](https://user-images.githubusercontent.com/80394522/186961299-74196da4-2ed6-4566-8d1b-f1e3d3745ab1.png)


![image](https://user-images.githubusercontent.com/80394522/186961870-8b5d185f-fec4-4c6c-9abe-50ec22c4f753.png)



**Índio:**
- Permitir visualizar planejamento de todas as lojas (Adicionar filtro "Todas" no relatório, + opção de escolher lojas alternadas) 
- Na tela principal, permitir criar/editar planejamento de todas as lojas. (ver print/planilha anexo)

**Copetti/Mago:**
- Mais opções de importação de planejamento: Ex: Pegar também do ano anterior ou média da venda real (Diferença de compra e venda) OBS: Atualmente com bug em meses com 30/31dias.
Cliente Copetti(Lottar): Sugestão Jacinto Mago: Fazer uma média de quanto venda cada dia da semana em meses anteriores e sugerir automaticamente.

**Fábio diz:** Buscar venda do ano anterior mesmo período, permitir informar % de crescimento, ou o valor total (calcular o outro). 
Está com bug na diarização da Compra. Os percentuais não estão sendo replicados corretamente no campo "Compra P. Acm" quando a diarização está personalizada. 
- Adicionar sinal de "+" ao lado de cada grupo, permitir abrir os grupos em "Subgrupos". (compra e venda)

**David Indio diz: ** Criar coluna "Pedido Real." antes da coluna "Compra Real." Somar apenas pedidos que estão em Aberto. (porque depois que chegam viram NF de Compra).

**Outros:**
- Formatar casa decimal valores com ponto no milhar 

- Permitir excluir/incluir grupos
 
- Ao importar planejamento anterior excluir grupos zerados (Ex: Taxas, Geral, etc..)

**OBS:** Ver demais cards relacionados a melhoria dessa tela. Falar com Fábio consultor.


![image](https://user-images.githubusercontent.com/80394522/186961411-a3d5bdb7-53f4-4c6b-bd9f-0fe1bad047f1.png)

![image](https://user-images.githubusercontent.com/80394522/186961476-47d964a7-3589-4234-8f7c-929fa0c6143d.png)

quando tem muitos compradores

cmopram de mais 

formatar valores

hoje eles abrem o relatório de diferença de compra e venda 

com 5 - diariazação
mostrar coluna com valores do ano anterior ou mes anterior
com um período de um mês ou mais meses dai faz o valor do mês ou uma média se for mais de um mês , não poderia ser um valor menor que 1 mês 



ao importar ,teria que puxar pelos dias da semana e não pelo dia do mês 


geralmente na ultima semana do mes é para o mes seguinte 

podemos ter mais colunas, mas teríamos que modificar a tela , ou 2 telas ou 2 abas


as margens de compra os grupos também não são iguais, podemos buscar da realidade tambem 

se alterar a compra alterar a porcentagem com casas decimais 

percentual de crescimento 

AO selecionar várias lojas deixar somente leitura

configuração para ignorar alguns grupos 

mostrar pedidos de compra ta em aberto 

visualizar o estoque seria uma boa 



