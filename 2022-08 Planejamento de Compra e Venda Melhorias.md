# Tela inicial
- Alterado o componente de data
- O combo de loja deverá permitir selecionar 1 ou mais lojas
- Inserido botão de Configurações
- Adicionada coluna de pedidos de compra
- Adicionado botão fechar no rodapé do formulário
- Ao alterar o valor de compra devemos atualizar a porcentagem
- melhorar a questão da digitação da diferença, atualizar ou ao pressionar enter ou a retirar o foco do campo, ao alterarmos da erro, ou somente corrigir o erro

![image](https://user-images.githubusercontent.com/80394522/187738884-8567eacf-2acb-4238-9182-eed09a8db42a.png)

# Botão Configurações
- Migrada a configuração das transferências
- Criada configuração de execeção para os grupos
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
- Corrigida a formatação nos campos
- Adicionados os subgrupos no relatório
- Adicionado + níveis de subgrupos
- Adicionado níveis de coloração nas linhas
- Combo de Lojas terá o filtro "Todos" os componentes da tela ficarão somente leitura
- Botão de imprimir foi movido para o rodapé do formulário
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

---
---
---































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



