Pré análise Scanntech - Promoções por BIN

Tempo de Análise (48hrs)
    - Modelagem das novas tabelas no banco de dados
    - Criação do template para a nova tela do PDV 
    - Planejamento, documentação e cadastro de tarefas 

Banco de Dados (4 hrs)
    - Criar nova tabela VendasFormasDadosTEF
    - Criar tabela PackVirtualCartoes (CodPack, CodTipoPag , Descricao)
    - Criar tabela PackVirtualCartoesBins (CodPack, CodTipPag, Bin)

Pack Virtual (64 hrs)
    - Criar classes telecode VendasFormasDadosTEF, PackVirtualCartoes e CartoesBins
    - Adicionar novo recurso nos modelos dos packs virtuais 
    - Adicionar formas (CRUD) para manipular a tabela PackVirtualCartoes na tela do pack virtual 
    - Atualizar triggers para enviar comandos para os PDVS
    - Adicionar novas tabelas na exportação para os PDVS

Telecon PDV (91 hrs)    
    - Criar tela para apresentar opções de pagamento com desconto para o usuário selecionar
    - Validar se o pack possui regra de cartão e chamar a nova tela ao entrar em formas de pagamento
    - Tratar mensagens no teclado reduzido + bips de alerta
    - Ler a bin do cartão no momento que inserir o cartão
    - Apresentar o valor no pinpad de acordo com a bin do cartão (com ou sem desconto)
    - Ler novos campos de retorno do Tef (NSU, bin, últimos dígitos cartão, nro autorização)
    - Inserir novos campos do TEF no arquivo .atu 
    - Estornar/Cancelar TEF se a bin não for a da promoção

Gerenciador (4 hrs)
    - Atualizar para ler os novos campos no .atu e inserir no servidor na tabela VendasFormasDadosTEF    

Serviço Telecon (30 hrs)
    - Atualizar para a API 3.0 
    - Importar formas de pagamento da API e inserir nas tabelas PackVirtualCartoes e PackVirtualCartoesBins 
    - Inserir os novos campos das vendas (bin, últimos dígitos cartão , nro autorização)

Testes de software (62 hrs)
    - Planejamento de testes (Pack Virtual, Telecon PDV, Gerenciador, Serviço Telecon)
    - Execução de testes




Fluxo para vendas com promoção por meio de pagamento:
Na compra de itens com promoção por meio de pagamento, ao apresentar o subtotal será validado as promoções disponíveis para os produtos da compra.
Com isto, deve ser apresentado a lista de opções de pagamento dos cartões participantes da promoção, para que o operador do caixa possa validar com o cliente se o meio de pagamento será feito de forma que a promoção seja válida, caso positivo, o operador de caixa deve selecionar o modo de pagamento contemplado na promoção. Ou então o desconto não será calculado.
Recomendamos a habilitação junto a empresa que vocês tem TEF para que consigam ler o BIN do cartão no momento que o cartão e inserido no PINPAD, conseguindo assim já validar se o BIN e elegível ou não para a promoção.
O sistema deve apresentar no pinpad o valor levando em consideração o desconto para o meio de pagamento selecionado.
Uma vez autorizada a transação, é verificado se realmente foi feito o pagamento com os BINS validos; caso não, dá erro e será estornado o valor (esse ponto seria apenas uma segurança a mais para validação no final).


•                    Sobre a documentação das promoções por forma de pagos estão na documentação da API 3.0: 
•                    Venda:
DOCUMENTAÇÃO - Instruções envio de venda
Necessário acrescentar os campos:
•                    codigoCanalVenta;
•                    bin;
•                    ultimosDigitosTarjeta;
•                    numeroAutorizacion;
•                    codigoTarjeta.

