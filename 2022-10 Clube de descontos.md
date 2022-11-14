## Épico Pack Valor 2 + Gerenciador de Imagens


# Pack Virtual 
1. Criar novo Pack Virtual (Valor 2)
    - Possibilitar inserir um valor de promoção para o produto
    - A promoção deve ser validada por forma de pagamento
    - A promoção deve ser validada por grupo de clientes 
    - Colocar preço por lojas na grade
    - Validar produtos associados ao inserir na grade 
    - Devemos ter a validação por limite de cpf
        - O limite de cpf deverá ser por produto
        - O limite de cpf deverá ser controlado na vigência da campanha, ou seja, deverá levar vendas passadas em consideração
        - Promoções com limite por cpf deverá valer apenas para vendas com cliente ou cpf atribuído
        - Promoções sem limite por cpf valerão para todos clientes
2. Realizar correção na tela do pack virtual para inserir de forma performárica na tabela comandosPDV
    - Apenas dos itens que realmente foram alterados, atualmente são alterados todos os itens sempre
3. Realizar correções no duplicar, tinha quebrado compatibilidade com a coluna de lojas criada anteriormente  
4. Realizar correções no duplicar referentes a packs com % ou R$  
5. Realizar correções no duplo clique da grade referentes a packs com % ou R$, as vezes não abria  
6. Realizar correções no maximizar e restaurar + botão expandir/recolher, estava se perdendo o layout 
7. Realizar correção no pack do atacado, não estava aparecendo o preço de atacado
8. Adicionar valor por loja no modelo do Clube de Descontos 
    
# URL Foto Produto
1. Criar tela para gerenciar imagens dos produtos
2. Criar módulo Gerenciar Imagens
3. Possibilitar selecionar uma imagem para o produto
4. Possibilitar inserir um url de uma imagem para o produto
5. Fazer similar a tela de imagens do qqCusta


# PDV / Gerenciador
1. Criar nova regra de pack preço 2 
2. Validar limite por CPF 
    - Mesmo caso o atu da venda não suba para o servidor todos pdv deverão controlar o limite por cpf, desde que estejam conectados no servidor
    - Se for atribuído o cliente no pdv sem cpf na NFG deverá funcionar também



## Observação: Funcionamento do Controle do Limite por CPF 

- Ao enviar exportação é enviado na tabela (`PackVirtualPreco2ClientesComLimiteExcedido`) para os pdvs uma lista bem restrita com o cliente, o produto que esgotou o limite por cpf e o pack, apenas de packs que estão ativos e produtos que possuem limites por CPF (essa é uma lista enxuta sem as informações de quantidade, apenas uma lista negra)

- De vez 5 em 5 minutos essa lista é atualizada pelo gerenciador de forma otimizada com recordset desconectados. Esse procedimento só irá rodar caso haja um pack com essas características 

- Ao realizar a venda, no momento em que é atribuído um cliente ou um CPF o PDV irá realizar uma consulta online no servidor por meio de uma thread para verificar a quantidade já comprada pelo cliente dos produtos que possuem limite por CPF, caso a thread não retorne ou não retorne a tempo... o PDV irá se basear apenas na lista negra e no limite do produto na venda atual
    - Para otimizar o processo seria melhor que o mercado utilizasse a opção para inserir cpf no início da venda
    - A thread só irá fazer a pesquisa no servidor caso exista um pack com essas características
    - A thread irá inserir o resultado da busca no servidor na tabela `PackVirtualPreco2VendasPorCPF` ao validar o pack esse tabela irá fazer parte da consulta

- No pdv ao vender um produto e esgotar o limite do produto para determinado cliente o pdv avisa por thread todos os pdvs (insere um registro na tabela ComandosPDV (`PackVirtualPreco2ClientesComLimiteExcedido`: cliente + produto que estourou o limite na compra)













``` sql 



DROP TABLE dbo.PackVirtualPreco2                                                  
                                                  
CREATE TABLE dbo.PackVirtualPreco2(
	CodPack Int Not Null,
	CodProduto Float Not Null,
	VlrPreco2 Money Not Null,
	LimitePorCPF Int Null)	                                          
                                                  
ALTER TABLE PackVirtualPreco2 Add Primary Key (CodPack,CodProduto)
                                                  
ALTER TABLE dbo.PackVirtualPreco2 Add CONSTRAINT FK_PackVirtualPreco2_PackVirtual Foreign Key(CodPack)
REFERENCES dbo.PackVirtual (Codigo)                                                  
                                    
ALTER TABLE CLIENTES ADD ClubeDeDescontos bit null


CREATE TABLE PackVirtualPreco2ClientesComLimiteExcedido(
CodPack Int Not Null,
CodProduto Float Not Null,
CodCliente Money Not Null,
CodLoja Int Null)        
	
ALTER TABLE PackVirtualPreco2ClientesComLimiteExcedido Add Primary Key  (CodPack,CodProduto,CodCliente)       



CREATE TABLE PackVirtualPreco2VendasPorCPF (
CodPack Int Not Null,
CodProduto Float Not Null,
CodCliente Money Not Null,
CodLoja Int Not Null,
QtdVendida Money Not Null,
LimitePorCPF Money Not Null)

INSERT INTO PackVirtualPreco2VendasPorCPF (CodPack, CodProduto, CodCliente, CodLoja, QtdVendida, LimitePorCPF) 
VALUES ()



SELECT * FROM Municipios WHERE DESCRICAO LIKE '%CANOAS%'

INSERT INTO CLIENTES (codigo, nome, cpf, estado, EMAIL, ClubeDeDescontos, DiaPagamento, GRUPO, CIDADE)
SELECT (SELECT MAX(CODIGO) + 1 FROM CLIENTES) , 'RODRIGO GOULART DA ROSA', '013.614.61047', 'RS' , 'rodrigo80221@gmail.com', 0, 5, 1, 'CANOAS'




CREATE TABLE PackVirtualPreco2ClientesComLimiteExcedido(
CodPack Int Not Null,
CodProduto Float Not Null,
Cliente nVarChar(28) Not Null,
CodLoja Int Not Null)                    

CREATE TABLE PackVirtualPreco2VendasPorCPF (
CodPack Int Not Null,
CodProduto Float Not Null,
Cliente nVarChar(28) Not Null,
CodLoja Int Not Null,
QtdVendida Money Not Null,
LimitePorCPF Money Not Null)




```
