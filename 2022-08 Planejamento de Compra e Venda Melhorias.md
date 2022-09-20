# Tela inicial
- Alterado o componente de data -- OK --
- O combo de loja deverá permitir selecionar 1 ou mais lojas -- OK --
- Inserido botão de Configurações -- OK --
- Adicionada coluna de pedidos de compra
- Adicionado botão fechar no rodapé do formulário -- OK --
- Ao alterar o valor de compra devemos atualizar a porcentagem -- OK --
- melhorar a questão da digitação da diferença, atualizar ou ao pressionar enter ou a retirar o foco do campo, ao alterarmos da erro, ou somente corrigir o erro -- OK --
- deixar os campos de planejamento somente para visualizar em amarelo -- OK --
- fixar totalizador na ordenação -- OK --
- resolver problemas de arredondamento, pois sempre fica em vermelho -- OK --
- bater com o relatório de diferença de compra e venda
- range de tolerância para o alerta de diferença nos labels -- OK --
- verificar bug , no mês de setembro está mostrando 30 dias mesmo tendo 31 dias no banco -- OK --
- Verificar módulos, deixar em amarelo caso não seja possível editar -- OK --

![image](https://user-images.githubusercontent.com/80394522/187738884-8567eacf-2acb-4238-9182-eed09a8db42a.png)

# Botão Configurações
- Migrada a configuração das transferências -- OK --
- Criada configuração de execeção para os grupos -- OK --
- Adiconar configuração ocultar grupos/ subgrupos sem movimentação

![image](https://user-images.githubusercontent.com/80394522/187539513-c88d61ae-9704-4109-9355-cb1c1eec1a16.png)

# Botão Criar Planejamento - Etapa 1
- Etapa para selecionar a origem do planejamento
- Se selecionarmos um planejamento anterior e ele tiver no formato antigo, devemos avisar o usuário que ficará no formato antigo

![image](https://user-images.githubusercontent.com/80394522/188020774-5fb9c8b9-a905-439e-805b-4664f85bb0ed.png)

# Botão Criar Planejamento - Etapa 2
- Etapa para selecionar as lojas
- Caso já exista um planejamento criado para a loja ela deverá vir desmarcada com a linha desabilitada (cor cinza claro) impossibilitando marcar novamente

![image](https://user-images.githubusercontent.com/80394522/188029517-0efa48b3-2520-472c-9ac1-bc6d0d0c04bd.png)

# Botão Criar Planejamento - Etapa 3
- Etapa para definir os valores de venda para o planejamento
- É possível editar o valor de crescimento percentual

![image](https://user-images.githubusercontent.com/80394522/188029551-0896d4cf-6181-4d24-a803-7cd23da72cd5.png)

# Botão Criar Planejamento - Etapa 4
- Etapa para definir os valores de compra para o planejamento
- Opção para não importar as informação de compra podendo definir um percentual padrão

![image](https://user-images.githubusercontent.com/80394522/188020925-e98b26ef-9bee-4946-b776-7a6d5a334694.png)


# Tela inicial - Planejamento Criado
- Corrigida a formatação nos campos -- OK --
- Adicionados os subgrupos no relatório
- Adicionado + níveis de subgrupos
- Adicionado níveis de coloração nas linhas
- Combo de Lojas terá o filtro "Todos" os componentes da tela ficarão somente leitura
- Botão de imprimir foi movido para o rodapé do formulário -- OK --
- Ao alterarmos a porcentagem de venda ou compa de um subgrupo, deve atualizar o percentual do pai 
- Devemos adicionar o valor de pedidos de compra na grade e somarmos na coluna diferença
- Melhorar a ordenação, o ideal é ordenarmos pelo código do grupo
- Ao alterar a porcentagem de um subgrupo atualizar os pais 
- o total deve somar somente a porcentagem dos pais 

![image](https://user-images.githubusercontent.com/80394522/188015285-60bc97f7-a681-4307-9264-4a2dc50808ac.png)


# Tela vendas dia
- ajustar layout
- carregar combo com base nos grupos da tabela
- alterar a query das vendasdia real para agrupar por grupo `public static List<PlanejamentoCompraVendaDia> ConsultarPorPlanejamentoGrupo`

![image](https://user-images.githubusercontent.com/80394522/191285379-f59d0f1e-541f-44d5-8e21-72b2a2198c45.png)

---
---
---






## Tarefa 1: Correção Bug 30 dias
1. Correção de bug , no mês de setembro está mostrando 30 dias mesmo tendo 31 dias no banco 


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
1. Inserir combo de loja com opção de uma ou mais lojas
    - ver exemplo no relatório pack virtual
    - ainda não programar as querys, somente layout
1. Adicionar e implementar botão de fechar
1. Tratar tab index
> As alterações na grade não serão realizadas nesta tarefa

![image](https://user-images.githubusercontent.com/80394522/191282474-536fc8da-447a-406d-890a-a9b43e07038c.png)


# Tarefa 1: Ajustar layout no FrmPlanejamentoCompraVendaDia
> Utilizar resolução 1024x768
1. Alterar o componente de data
    - inserir componente padrão de outros relatórios
    - ajustar o código para utilizar o novo componente
1. Inserir combo de loja com opção de uma ou mais lojas
    - ver exemplo no relatório pack virtual
    - ainda não programar as querys, somente layout
1. Adicionar e implementar botão de fechar
1. Tratar tab index
> As alterações na grade não serão realizadas nesta tarefa

![image](https://user-images.githubusercontent.com/80394522/191282474-536fc8da-447a-406d-890a-a9b43e07038c.png)



# Tarefa 1: Criar formulário FrmPlanejamentoCompraVendaInserir (Parte 1)
1. Criar formulário FrmPlanejamentoCompraVendaInserir
    - Implementar o layout conforme a imagem anexa
    - Deverá ter um componete tabControl assim como no sped
        - Quando estiver compilado subir o componente escondendo as abas
    - O caption do formulário será "Planejamento de Compra e Venda - Criar Planejamento"
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

# Tarefa 1: Criar formulário FrmPlanejamentoCompraVendaInserir (Parte 2)
> Implementar aba 1 "Defina a origem do Planejamento"
1. Adicionar Radio Buttons
    - Ao selecionar cada um exibir o campo de data relacionado
1. Ao clicar em avançar mudar para a próxima aba
1. Tratar tab index

# Tarefa 1: Criar formulário FrmPlanejamentoCompraVendaInserir (Parte 3)
> Implementar o avançar da aba 2


# Tarefa 1: Criar formulário FrmPlanejamentoCompraVendaInserir (Parte 3)




















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



