# Épico: Integração Conciliação de Cartões

## Problema a ser resolvido
Como Gestor de supermercado, desejo visualizar em meu financeiro o resultado real da minha loja. O volume de vendas em cartões é cada vez maior (hoje em torno de 60% das vendas) e as despesas dessa venda precisam estar corretamente lançadas no financeiro do sistema. As taxas cobradas pelas administradoras devem estar descontada da venda líquida de cartões no meu fluxo de caixa e as despesas (Aluguel POS, Antecipação, outras taxas)  devem ser consideradas no DRE. Como já possuo contrato com uma empresa de conciliação de cartões, desejo que as informação sejam importadas para o meu ERP de forma automática.

Como diretor comercial da Telecon, desejo que o Sistema S possua uma API genérica para a integração com conciliadoras diversas. Caso um cliente optar por contratar uma conciliadora diferente das que temos integração, devemos apenas fornecer a documentação e credenciais de acesso e deixar que eles façam o trabalho.

## Pontos chave da história
Analisar os requisitos do card: https://trello.com/c/zvr3y7v3

## Impactos
- Telecon PDV
- Torus PDV
- Cadastro Administradoras Cartão
- SPED (registro 1600)
- Saldo de Caixa
- Fluxo de Caixa
- DRE
- Contas a Pagar
- Contas a Receber

## Resultado Final
- Cliente visualizar no seu fluxo de caixa as vendas de cartões já descontada da taxa e com os vencimentos corretos conciliados pela empresa conciliadora.
- Cliente visualizar no seu "Contas a Pagar", o valor das demais despesas oriundas das vendas em cartão.
- Cliente visualizar tela de pendências com:
     - Vendas Não Conciliadas (Que estão no ERP e não vieram na Conciliação)
     - Vendas Não Localizadas (Que estão na Concliação e não estão no Sistema)


Cassio Rocha 7 horas atrás
Alguns questionamentos:
- Como tratar valores parcelados???
- Mostrar no nosso fluxo de caixa
- Permitir filtrar por status (conciliado/não conciliado)

-----

Resumo após conversa reunião com a C3

![image](https://user-images.githubusercontent.com/80394522/193141047-2e944fa4-d91d-4103-938d-65256a21d971.png)

A idéia conversada na imagem acima seria:
1 - Venda : a venda é realizada no pdv 

2 - Gravar transação : gravar mais dados da venda, gravar o NSU, para podermos fazer a chave primária NSU+Data+Valor , devemos aproveitar já e gravar a bin da bandeira, pois talvez vamos precisar desta bim para as promoções scanntech, devemos gravar tudo em tabela separada assim como era o programa atigo da telecon para integração 
no fluxo de caixa em vez de mostrarmos os dados das vendas iremos mostrar os dados que estão nesta tabela , nesta tabela teremos também a taxa da administradora que já esta cadastrada no nosso cadastro das administradoras de cartão 

3 - Ajustar transação (quitar, mudar data pagamento/ previsão , mudar taxa)
aqui iremos disponibilizar uma API para a empresa de conciliação, ela fara o trabalho de atualizar os status , editar alguma alteração na data previsata de pagamento e alterar a taxa da administradora
     - podemos já ajustar essa taxa se estiver configurado para alterar automáticamente , podemos até ter uma vigências destas taxas caso necessário, a vigência será a nossa própria tabela , e o top será a coluna do nosso cadastro 
     
     


![image](https://user-images.githubusercontent.com/80394522/193141083-97e44ab0-514f-444b-8645-fd8b88a6428e.png)

aqui falamos também sobre os atalhos no fluxo de caixa para verificar o analitico das vendas por cartões , neste caso apontaria para o site da c3





---

Cassio Rocha 5 de abr às 16:42

Case inicial será com a conciliadora chamada C3, a qual a Telecon está formalizando uma parceria comercial.

---

Cassio Rocha 5 de abr às 16:41

Precisamos que o PDV extraia da Clisistef o maior número possível de informações sobre a venda: Ex: NSU, BIN, Autorização, Etc...
Armazenar em uma tabela separada no banco de dados para não carregar desnecessariamente a tabela de Vendas.
Incluir na nova tabela: CodVenda, CodAdm, BIN, NSU, NroAutorizacao, DataPrevistaPgto, DataPagamento, ValorBruto, Taxa, ValorLiquido, DataConciliacao, CodUsuarioConciliacao
No fluxo de caixa apresentar: "Valor Líquido" na venda em cartão.
API de Concliação deve atualizar os valores de Taxa, Valor Liquido, DataPagamento e DataConciliação.
API deve permitir receber outras despesas de cartões. Ex: Aluguel, etc... Gravar no financeiro Contas a Pagar.



---

# Card Controle de Cartões Débito/Crédito

Sistema deve permitir a quitação manual, dos lançamentos (mudança de statuts)

Demanda Copetti:
1) Hoje eles utilizam os relatórios de "Vendas por Cartão Crédito", porém encontram erros de lançamento das operadoras (quando cartão POS) e não conseguem corrigir, pois essa informação não tem uma interface de cadastro no Gestão, apenas relatórios.

2) Deseja poder dar "baixa" importando arquivo da conciliadora de cartões.

![image](https://user-images.githubusercontent.com/80394522/177851086-037180d4-e4cc-46ee-8225-78aa186045e2.png)

![image](https://user-images.githubusercontent.com/80394522/177851136-73298134-7737-4892-a0b5-ec645db770da.png)
---

Daniel Dorneles da Silva 3 de dez de 2018 às 06:50

Problema: Financeiro do supermercado precisa gerenciar melhor a venda em cartões, ter mais opções de filtros, totais e de baixar os cartões já recebidos.

---

Daniel Dorneles da Silva 3 de dez de 2018 às 07:39

Ver antigo Gerenciador TEF

---

Cassio Rocha 10 de dez de 2018 às 09:34

@danieldornelesdasilva e @robsonpaines
Estão surgindo demandas (ex: Tischler) de enxergar no financeiro o fluxo de caixa correto a receber dos cartões. Assim como a liquidação (pagamento) entrar também no saldo das contas.
Peço a contribuição de vocês para alinhar essas demandas com o DIS desse card.

---

Cassio Rocha 27 de dez de 2018 às 16:36

@brunoquoos @robsonpaines @tiagolickoski
O Tischler não aprovou a integração com do arquivo com a conciliadora, porém precisamos tocar a nova tela para melhor gerenciamento dos cartões.

O Lickoski está fazendo uma análise para a nova integração KW e alguns pontos interagem com esse DIS que o Bruno está prestes a fazer.

Eu criei um novo documento com uma pré-análise do que eu imagino ser a solução. https://docs.google.com/document/d/1AZHUUkn9UC6cpDMqgd9Sfn11heVH1654gWQu9FCgz5Q/edit?usp=sharing

@robsonpaines peço que nos ajude na análise, principalmente no que diz respeito a exportação dessas informações para o controle financeiro e posterior envio à contabilidade.

Assim que chegarmos a um consenso liberamos o bruno para tocar a nova tela em C#.

---

[Ficha do Dis feito pelo Bruno[(https://docs.google.com/document/d/14R3w_Iglj4HwmJlqd-BvZtiZgYCSNd29q4wIy-XiGdo/edit)

