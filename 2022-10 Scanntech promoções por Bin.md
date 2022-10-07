# Pré análise Scanntech - Promoções por BIN

Tempo de Análise (48hrs)
    - Modelagem das novas tabelas banco de dados
    - Criação do template para a nova tela do PDV 
    - Planejamento, documentação e cadastro de tarefas 

Banco de Dados (4 hrs)
    - Criar nova tabela VendasFormasDadosTEF
    - Criar tabela PackVirtualCartoes (CodPack, CodTipoPag , Descricao)
    - Criar tabela PackVirtualCartoesBins (CodPack, CodTipPag, Bin)

Pack Virtual (64 hrs)
    - Criar classes telecode VendasFormasDadosTEF, PackVirtualCartoes e CartoesBins
    - Adicionar novo recurso nos modelos dos packs virtuais 
    - Adicionar formas (CRUD) para manipular a tabela PackVirtualCartoes na tela do pack virtual (novas abas?)
    - Atualizar triggers para enviar comandos para os pdvs
    - Adicionar novas tabelas na exportação para os PDVS

Telecon PDV (91 hrs)    
    - Criar tela para apresentar opções de pagamento com desconto para o usuário selecionar
    - Validar se o pack possui regra de cartão e chamar a nova tela ao entrar em formas de pagamento
    - Tratar mensagens no teclado reduzido + bips de alerta
    - Ler a bin do cartão no momento que inserir o cartão
    - Apresentar o valor no pinpad de acordo com a bin do cartão (com ou sem desconto)
    - Ler novos campos de retorno do Tef (NSU, bin, últimos dígitos cartão, nro autorização)
    - Inserir novos campos do TEF no arquivo .ATU 
    - Estornar/Cancelar venda se a bin não for a da promoção

Gerenciador (4 hrs)
    - Atualizar para ler os novos campos no .atu e inserir no servidor na tabela VendasFormasDadosTEF    

Serviço Telecon (30 hrs)
    - Atualizar para a API 3.0 
    - Importar formas de pagamento da API e inserir no pack virtual
    - Inserir os novos campos das vendas (bin, últimos dígitos cartão , nro autorização)

Testes de software (185 hrs)
    - Planejamento de testes (Pack Virtual, Telecon PDV, Gerenciador, Serviço Telecon)
    - Execução de testes

