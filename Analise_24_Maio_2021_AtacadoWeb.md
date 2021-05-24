Possibilidade 1: 
    * Ter outro produto para a caixa/fardo... , ele seria o produto pai
    * Adiciona o item na fórmula dele 
    * Puxa os pais no controle de entradas
    * Cria um campo, que seria a embalagem de atacado, que seria também o incremento do site, e talvez seja o mesmo campo da fórmula

    Ruim:
    * Aumentaria muito a base de dados
    * Aumentaria muito o banco compartilhado?
    * Necessitaria de mais gerencia
    * Perderíamos as fotos

    Bom:
    * Teriamos tudo certo nos relatórios
    * Teríamos um histórico correto de venda
    * Poderíamos fazer notas para o DUN



Possibilidade 2:    
    * Adicionar o código DUN no cadastro de produtos
    * Adicionar quantidade de varejo para ele, que já seria também o incremento do site
    * Adicionar um preço de venda
    * Teria que tratar nas triggers para deixar o estoque correto
    * Tratar ele nas nf saídas e nf entradas e pedidos, para buscar o campo qtd??? ou não permitir estes produtos, prod apenas para compra ou venda
    * Puxar os dois no controle de entradas

    Ruim
    * perderia o histórico de vendas


    Bom:
    * manteria as fotos



    







    copetti augusto dun 

    pedido de compra usar o dun, tbm transferencias


 Tratar ele nas nf saídas e nf entradas e pedidos, para buscar o campo qtd??? ou não permitir estes produtos, prod apenas para compra ou venda

 desativar o produto apenas para compra ou venda

