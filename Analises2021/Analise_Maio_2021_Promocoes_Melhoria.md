## Descrição do épico

https://trello.com/c/70rNjL0h/10-an%C3%A1lise-melhorias-promo%C3%A7%C3%B5es

As promoções são uma ferramenta de gestão muito utilizadas pelos supermercados, principalmente os maiores que tem concorrência forte na cidade. No check-list em anexo temos um conjunto de cards que são importantes para melhorar essa ferramente de acordo com relatos de nossos clientes.

Pontos Importantes a serem:
- Envolve muito o processo interno da loja (envio de carga, impressão etiqueta, troca de preço na loja). Clientes como Codebal e Redesuper tem promoções/encartes todos os dias da semana.

Envolve a reputação do supermercado: Promoções são divulgadas em Encartes e mídias sociais, caso o cliente vá até a loja e não encontre o produto em promoção gera muita reclamação.



https://trello.com/c/wKKIT7UK/3430-ticket2020030410000805-agendar-mais-de-uma-promo%C3%A7%C3%A3o-para-o-mesmo-produto

## Ticket#2020030410000805 — Agendar mais de uma promoção para o mesmo produto

Cliente: Farroupilha (também solicitado por Codebal)

Descrição do problema:
Quando gera duas promoções do mesmo produto com datas diferentes, o valor da primeira promoção assume o valor de venda.
Exemplo: Promoção - 30
Valor de venda - 5.00
Valor da promoção - 6.00
Data - 12/03/2020 até 13/03/2020
Produto - 3030

Exemplo: Promoção - 31
Valor de venda - 5.00
Valor da promoção - 07,00
Data - 14/03/2020 até 15/03/2020
Produto - 3030

Quando inserimos a segunda promoção o valor de venda passa a ser 6.00 Reais, assumindo assim o valor da promoção.

Qualquer dúvida estou a disposição.

Passos para reproduzir o problema:
1 - Abrir Gestão
2 - Aba Produtos - Promoções
3 - Gerar duas promoções com o mesmo produtos com datas diferentes.
4 - verificar o cadastro de produto o valor de venda que ficou.

Versão do sistema:
3.14.12
Arquivos externos necessários: (em anexo)

Embasamento legal: (caso necessário alteração no sistema apenas)



https://trello.com/c/Ps9w914m/3417-cliente-farroupilha-tamb%C3%A9m-solicitado-por-codebal

## Cliente: Farroupilha (também solicitado por Codebal)

User History:
Como Gerente do supermercado, necessito que ao abrir a loja, no início da manhã (7hs) as promoções agendadas estejam disponíveis para dar carga e emitir etiquetas. A loja deve estar preparada para receber os clientes até o horário de abertura da loja (8hs). Quando isso não ocorre preciso aguardar até as 8hs para ligar para o suporte da Telecon o que atrasa a abertura e gera transtorno nos caixas pois é necessário dar desconto e o cliente fica desconfiado.

Causas:
Servidor estar desligado
Gestão não estar em login_automático no servidor
Gestão com mensagem aguardando OK do usuário
...

Sugestão de solução:
Passar código do tmrManutenção para o Serviço Telecon.



https://trello.com/c/po8nk9r4/3098-promo%C3%A7%C3%A3o-ao-entrar-sair-da-promo%C3%A7%C3%A3o-agendada-registrar-pre%C3%A7o-alterado-para-o-usu%C3%A1rio-que-criou-a-promo%C3%A7%C3%A3o

## Promoção: Ao entrar/sair da promoção agendada, registrar preço alterado para o usuário que criou a promoção.

Atualmente o sistema registra a alteração de preço para o usuário "Telecon", o que gera confusão em alguns casos.



https://trello.com/c/oRoO4Agl/3270-ticket2020050910000614-filler-sugest%C3%A3o-de-melhoria-melhoria-tela-de-promo%C3%A7%C3%A3o-encarte

## Ticket#2020050910000614 — FILLER - Sugestão de Melhoria - Melhoria tela de Promoção/encarte


Cliente solicita que na tela de Cadastro do encarte, ao trocar a data do periodo do Encarte, perguntar se deseja prorrogar data para os produtos que estão dentro deste Encarte.