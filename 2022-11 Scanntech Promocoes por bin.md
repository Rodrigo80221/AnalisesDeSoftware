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




## Fluxo para vendas com promoção por meio de pagamento:
Na compra de itens com promoção por meio de pagamento, ao apresentar o subtotal será validado as promoções disponíveis para os produtos da compra.

Com isto, deve ser apresentado a lista de opções de pagamento dos cartões participantes da promoção, para que o operador do caixa possa validar com o cliente se o meio de pagamento será feito de forma que a promoção seja válida, caso positivo, o operador de caixa deve selecionar o modo de pagamento contemplado na promoção. Ou então o desconto não será calculado.

Recomendamos a habilitação junto a empresa que vocês tem TEF para que consigam ler o BIN do cartão no momento que o cartão e inserido no PINPAD, conseguindo assim já validar se o BIN e elegível ou não para a promoção.

O sistema deve apresentar no pinpad o valor levando em consideração o desconto para o meio de pagamento selecionado.
Uma vez autorizada a transação, é verificado se realmente foi feito o pagamento com os BINS validos; caso não, dá erro e será estornado o valor (esse ponto seria apenas uma segurança a mais para validação no final).


Sobre a documentação das promoções por forma de pagos estão na documentação da API 3.0: 
Venda:

DOCUMENTAÇÃO - Instruções envio de venda
Necessário acrescentar os campos:
• codigoCanalVenta;
• bin;
• ultimosDigitosTarjeta;
• numeroAutorizacion;
• codigoTarjeta.

![image](https://user-images.githubusercontent.com/80394522/201927970-8cc47c98-7a48-4223-ad67-33961b176941.png)

![image](https://user-images.githubusercontent.com/80394522/201928009-033bd56a-2f86-4fd9-a9c9-61ed602a8756.png)

![image](https://user-images.githubusercontent.com/80394522/201928035-18d40909-6af4-4ef5-b39b-1b4fefd5caaa.png)

• Promoções:
DOCUMENTAÇÃO - Get Promoções

![image](https://user-images.githubusercontent.com/80394522/201928126-f048af9d-48bd-473d-97a6-db74d71b4696.png)


• Tipos de promoções:
DOCUMENTAÇÃO - Tipos de promoções

![image](https://user-images.githubusercontent.com/80394522/201928196-cc5495ee-7b3c-4623-ba3c-09ddbdad8460.png)

• Modelo de Json por Tipo de Promoções:
DOCUMENTAÇÃO - Json por tipo de promoção

![image](https://user-images.githubusercontent.com/80394522/201928269-2884e0cc-646e-4e45-b3b6-4b3c90ebcaeb.png)


![image](https://user-images.githubusercontent.com/80394522/201929432-60cb523b-13fc-41bf-a894-2fd4eaa0f1b4.png)

Segue parâmetros API de teste:
Usuário: integrador_test@teleconsistemas.com.br
Clave: integrador
ID Red: 457 (DEMO_PRECIOS)


Cassio,

Conforme relatei em outro e-mail, acrescento neste e-mail a @Gabriela Pinho ela irá auxiliar em todo o processo de desenvolvimento.
Por gentileza, incluir o desenvolvimento do lado de vocês  para iniciarmos o projeto.

"Gabriela Pinho" <gpinho@scanntech.com>

![image](https://user-images.githubusercontent.com/80394522/201929055-4137b3d4-8e83-4e2f-9547-315a0aa3fd0d.png)





``` sql
CREATE TABLE PackVirtualFormasPgtoTEF 
( 
CodPack bigint, 
CodTipoPagamento bigint, 
Descricao nvarchar(512) not null, 
PRIMARY KEY (CodPack,CodTipoPagamento) 
) 

ALTER TABLE PackVirtualFormasPgtoTEF  WITH CHECK ADD  CONSTRAINT [FK__PackVirtualFormasPgtoTEF] FOREIGN KEY([CodPack]) 
REFERENCES [dbo].[PackVirtual] ([Codigo]) 
ON UPDATE CASCADE 
ON DELETE CASCADE

CREATE TABLE PackVirtualFormasPgtoTEFBins 
( 
CodPack bigint, 
CodTipoPagamento bigint, 
BIN nvarchar(6) not null, 
PRIMARY KEY (CodPack,CodTipoPagamento,BIN) 
) 

ALTER TABLE PackVirtualFormasPgtoTEFBins  WITH CHECK ADD  CONSTRAINT [FK__PackVirtualFormasPgtoTEFBins] FOREIGN KEY([CodPack],[CodTipoPagamento]) 
REFERENCES [dbo].[PackVirtualFormasPgtoTEF] (CodPack,CodTipoPagamento) 
ON UPDATE CASCADE 
ON DELETE CASCADE 





```



``` sql access

CREATE TABLE PackVirtualFormasPgtoTEF 
( 
CodPack Int, 
CodTipoPagamento Float, 
Descricao Char(255) Not Null
) 

ALTER TABLE PackVirtualFormasPgtoTEF Add  CONSTRAINT PK_PackVirtualFormasPgtoTEF Primary Key (CodPack,CodTipoPagamento)

ALTER TABLE PackVirtualFormasPgtoTEF Add  CONSTRAINT FK_PackVirtualFormasPgtoTEF Foreign Key(CodPack) 
REFERENCES PackVirtual (Codigo) 
On UPDATE CASCADE 
On DELETE CASCADE
                                                                                          
CREATE TABLE PackVirtualFormasPgtoTEFBins 
( 
CodPack Int, 
CodTipoPagamento Float, 
BIN Char(6) Not Null 
) 
 
ALTER TABLE PackVirtualFormasPgtoTEFBins Add  CONSTRAINT PK_PackVirtualFormasPgtoTEFBins Primary Key (CodPack,CodTipoPagamento,BIN) 

ALTER TABLE PackVirtualFormasPgtoTEFBins Add  CONSTRAINT FK_PackVirtualFormasPgtoTEFBins Foreign Key(CodPack,CodTipoPagamento) 
REFERENCES PackVirtualFormasPgtoTEF (CodPack,CodTipoPagamento) 
On UPDATE CASCADE 
On DELETE CASCADE 

```