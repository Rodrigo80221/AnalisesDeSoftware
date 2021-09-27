
# Tela atual
![image](https://user-images.githubusercontent.com/80394522/134715217-19a16656-0331-43b7-ac67-d312644803b5.png)

# Controle de Entradas Novo
![image](https://user-images.githubusercontent.com/80394522/134574560-8527e151-4d1a-4d2c-a914-f03aa8768b52.png)

#NF Entradas Novo
![image](https://user-images.githubusercontent.com/80394522/134583578-7d93c0bb-aa6e-4d01-b9e6-cc106614f220.png)

# Relatório atual
![image](https://user-images.githubusercontent.com/80394522/134434627-9bc9ad85-6680-4bde-8069-d16be2231109.png)

# Relatório com o vendedor
![image](https://user-images.githubusercontent.com/80394522/134434940-105bc441-8e08-40d6-bd40-52e619ab4d5e.png)





# Pedido de Compra: Criar castro de Vendedores e Filtro de Produtos por Vendedor (Santana, Nova Compra)

Alguns fornecedores possuem mais que um vendedor atendendo os supermercados, um para cada "pasta", ou seja uma linha de produtos ou marcas. Isso causa dificuldade para o comprador que não consegue filtrar na tela do Compras apenas os produtos de determinado vendedor.

Atualmente sugerimos ao usuário (comprador) fazer filtros por Grupos ou criar lista de códigos, porém ambas não tem boa usabilidade na hora do pedido.

É comum o usuário solicitar um filtro por "Vendedor" ao fazer o pedido. 

Atualmente nossa estrutura para armazenar o vendedor é um campo livre para digitação no cadastro do pedido, isso dificulta um filtro seguro.

**Tarefas:**
**1) Criar um cadastro de Vendedores**
- Criar tabela VendedoresFornecedor (CodVendedor (PK Identity), CodFornecedor, Nome, telefone, e-mail, MixPorVendedor(bit))
- Popular a nova tabela criada com dados históricos da tabela Pedidos (distinct de Pedidos.codFornecedor e Pedidos.Vendedor) O e-mail e fone deve ser do último pedido do vendedor
- Criar campo CodVendedor (Null, Foregin key VendedoresFornecedor) na tabela PedidoCompras (OBS: pode ser null pois o usuário pode não informar vendedor caso desejar)
- Atualizar PedidoCompras.CodVendedor buscando os dados populados na tabela "VendedoresFornecedor"
- Remover da tabela PedidoCompra os antigos campos Vendedor, Fone, E-mail.
- Criar tela de Crud para o usuário cadastrar/alterar os dados da tabela VendedoresFornecedor (será chamada apenas pela tela do Pedido de Compra)
- Na tela do Pedido, criar label link "Vendedor" permitindo chamar tela de cadastro do vendedor para criar novo ao alterar um existente.
- Criar botão de procura (lupa) e popular campos na tela buscando dos novos campos nas tabelas PedidoCompras.CodVendedor e VendedoresFornecedor
- Alterar os rpts dos Pedidos de Compra para buscar os campos da tabela nova.
- Criar coluna no grid da busca do pedido e  filtro por nome do vendedor na tela de busca do pedido.

**2) Criar relação de VendedoresProdutos**
- Criar tabela VendedoresProdutos (codVendedor, CodProduto)
- Popular nova tabela com dados dos últimos 3 pedidos do vendedor
- Na tela do pedido de compras, nos filtros de produtos do pedido, criar novo filtro "Vendedor". Deve listar os vendedores disponíveis do fornecedor, e já trazer escolhido o vendedor selecionado no pedido como padrão.
- Criar checkbox para ativar/desabitar o filtro, sugerindo o vendedor do pedido (semelhante ao filtro atual de "Fornecedor"). Padrão desmarcado, salvar escolha em "VendedorFornecedores.MixPorVendedor)
- Caso vendedor selecionado e checkbox desmarcado, fazer left join no select que retorna os produtos ainda não adicionados ao pedido para indicar se o produto está no mix do Vendedor ou não, assinalando com fonte itálico nome do produto que estiver fora do mix do vendedor.
- Caso vendedor selecionado e checkbox marcado, fazer Inner join no select que retorna os produtos para retornar na pesquisa dos produtos ainda não adicionados ao pedido apenas os produtos dentro do mix do vendedor.
- Caso vendedor não selecionado não deve ser feito nenhum join de vendedores.
- Criar menu no clique direito do produto "Desvincular produto do Vendedor"
- Ao "desvincular produto do Fornecedor" deve também desvincular do mix de todos os Vendedores, caso esteja.
- Ao inserir o produto em "PedidoCompraProduto" caso um vendedor estiver selecionado e o produto fora do mix do vendedor, inserir também em VendedoresProdutos.



pernambuco 2 clientes

na nota fiscal em informações complementares
vem o vendedor
na tela de notas colocar também quem foi o vendedor daquela nota 

no relatório de vendas por período por vendedor
no pedido de compras tem um atalho para esse relatório, filtro por fornecedor e vendedor

impressão na telinha de cadastro

colocar uma chamada da telinha em fornecedores




## Criar tabela VendedoresFornecedor -------------- OK
(será que precisa o código do fornecedor??)
- Criar tabela VendedoresFornecedor (CodVendedor (PK Identity), CodFornecedor, Nome, telefone, e-mail, MixPorVendedor(bit))
## Popular tabela VendedoresFornecedor -------------- OK
(neste caso serão somente os produtos que tem pedido então?)
- Popular a nova tabela criada com dados históricos da tabela Pedidos (distinct de Pedidos.codFornecedor e Pedidos.Vendedor) O e-mail e fone deve ser do último pedido do vendedor
## Criar campo PedidoCompras.CodVendedor -------------- OK
- Criar campo CodVendedor (Null, Foregin key VendedoresFornecedor) na tabela PedidoCompras (OBS: pode ser null pois o usuário pode não informar vendedor caso desejar)
- Atualizar PedidoCompras.CodVendedor buscando os dados populados na tabela "VendedoresFornecedor"
- Remover da tabela PedidoCompra os antigos campos Vendedor, Fone, E-mail.


## Criar tabela VendedoresProdutos -------------- OK
- Criar tabela VendedoresProdutos (codVendedor, CodProduto)
## Popular tabela VendedoresProdutos -------------- OK
- Popular nova tabela com dados dos últimos 3 pedidos do vendedor
## Criar tela para manipular os Vendedores dos fornecedores -------------- OK
- Criar tela de Crud para o usuário cadastrar/alterar os dados da tabela VendedoresFornecedor (será chamada apenas pela tela do Pedido de Compra)

## Alterações na tela pedido de compra

* Label link "Vendedor"
    * Na tela do Pedido, criar label link "Vendedor" permitindo chamar tela de cadastro do vendedor para criar novo ou alterar um existente.
* Lupa para selecionar o vendedor (acho que não precisa)
    * Criar botão de procura (lupa) e popular campos na tela buscando dos novos campos nas tabelas PedidoCompras.CodVendedor e VendedoresFornecedor
* Criar filtro "Vendedor" no filtro de produtos com funcionamento semelhante ao do fornecedor
    * Na tela do pedido de compras, nos filtros de produtos do pedido, criar novo filtro "Vendedor". Deve listar os vendedores disponíveis do fornecedor, e  já trazer escolhido o vendedor selecionado no pedido como padrão.
* Criar checkbox para o vendedor, semelhante ao do fornecedor
    * Criar checkbox para ativar/desabitar o filtro, sugerindo o vendedor do pedido (semelhante ao filtro atual de "Fornecedor"). Padrão desmarcado, salvar escolha em "VendedorFornecedores.MixPorVendedor)
    * Caso vendedor selecionado e checkbox desmarcado, fazer left join no select que retorna os produtos ainda não adicionados ao pedido para indicar se o produto está no mix do Vendedor ou não, assinalando com fonte *** itálico *** nome do produto que estiver fora do mix do vendedor.
    * Caso vendedor selecionado e checkbox marcado, fazer Inner join no select que retorna os produtos para retornar na pesquisa dos produtos ainda não adicionados ao pedido apenas os produtos dentro do mix do vendedor.
    * Caso vendedor não selecionado não deve ser feito nenhum join de vendedores.
* Criar menu para desvincular produto ao vendedor - tratar tbm no menu do fornecedor
    * Criar menu no clique direito do produto "Desvincular produto do Vendedor"
    * Ao "desvincular produto do Fornecedor" deve também desvincular do mix de todos os Vendedores, caso esteja.
* Ao inserir o produto em pedido compra produto add tbm no mix do vendedor
    * Ao inserir o produto em "PedidoCompraProduto" caso um vendedor estiver selecionado e o produto fora do mix do vendedor, inserir também em VendedoresProdutos.
## Alterar RPTs
- Alterar os rpts dos Pedidos de Compra para buscar os campos da tabela nova.
## Criar coluna na busca de pedidos de compra
- Criar coluna no grid da busca do pedido e  filtro por nome do vendedor na tela de busca do pedido.








# ALTERAÇÕES NO BANCO DE DADOS  

## Tarefa 1: Criar função no atualiza banco fCriarCadastroDeVendedoresNasCompras  PARTE 1

1. Criar função fCriarCadastroDeVendedoresNasCompras
1. Adicionar o comando abaixo
1. Testar com Funcoes.fExisteObjeto, se a tabela FornecedorVendedores já estiver no banco de dados dar um `Exit Sub`

``` SQL
CREATE TABLE [dbo].[FornecedorVendedores](
	[CodVendedor] [int] NOT NULL IDENTITY,
	[CodFornecedor] [int] NOT NULL,
	[Nome] [nvarchar](50),
	[Fone] [nvarchar](14) NOT NULL DEFAULT (''),
	[Email] [nvarchar](64) NOT NULL DEFAULT (''),
	[MixPorVendedor] [bit] NOT NULL DEFAULT (0), CONSTRAINT [PK_CodVendedor] PRIMARY KEY CLUSTERED ([CodVendedor] ASC))
GO
ALTER TABLE [dbo].[FornecedorVendedores]  WITH NOCHECK ADD  CONSTRAINT [FK_Fornecedor_Vendedores] FOREIGN KEY([CodFornecedor])  
REFERENCES [dbo].[Fornecedores] ([codigo]) ON DELETE CASCADE ON UPDATE CASCADE
GO
ALTER TABLE [dbo].[FornecedorVendedores] ADD CONSTRAINT chk_NomeVendedor CHECK (LEN(Nome) > 1)
```



## Tarefa 2: Continuar função fCriarCadastroDeVendedoresNasCompras PARTE 2

1. Incrementar na função fCriarCadastroDeVendedoresNasCompras
1. Ao final verificar se atendeu o requisito abaixo:
- Os vendedores cadastrados nos pedidos devem ser migrados para a tabela FornecedorVendedores
- O e-mail e fone deve ser do último pedido do vendedor

``` SQL
INSERT INTO [FornecedorVendedores] ([CodFornecedor],[Nome],[Fone],[Email])
SELECT 
CodFornecedor
, Vendedor
, ISNULL((SELECT TOP 1 VendedorFone from PedidoCompra PC 
	      WHERE PC.CodFornecedor = PDC.CodFornecedor AND PC.Vendedor = PDC.Vendedor AND PC.VendedorFone <> '' 
		  ORDER BY DataPedido DESC),'')
, ISNULL((SELECT TOP 1 VendedorMail from PedidoCompra PC 
          WHERE PC.CodFornecedor = PDC.CodFornecedor AND PC.Vendedor = PDC.Vendedor AND PC.VendedorMail <> '' 
		  ORDER BY DataPedido DESC),'')
FROM PedidoCompra PDC
WHERE Vendedor IS NOT NULL AND Vendedor <> ''
AND NOT EXISTS (SELECT * FROM FornecedorVendedores FV WHERE FV.CodFornecedor <> PDC.CodFornecedor AND FV.Nome <> PDC.Vendedor) 
GROUP BY CodFornecedor, Vendedor
```

## Tarefa 3: Continuar função fCriarCadastroDeVendedoresNasCompras PARTE 3

1. Incrementar na função fCriarCadastroDeVendedoresNasCompras
1. Ao final verificar se atendeu o requisito abaixo:
- Criar campo CodVendedor na tabela PedidoCompras 
- Atualizar PedidoCompras.CodVendedor buscando os dados populados na tabela "VendedoresFornecedor"
- Remover da tabela PedidoCompra os antigos campos Vendedor, Fone, E-mail.

``` SQL
ALTER TABLE PedidoCompra ADD [CodVendedor] [int] NULL;
GO
ALTER TABLE PedidoCompra WITH NOCHECK ADD CONSTRAINT FK_PedidoCompra_FornecedorVendedores FOREIGN KEY ([CodVendedor]) REFERENCES DBO.FornecedorVendedores (CodVendedor);
```

``` SQL
UPDATE PC SET PC.[CodVendedor] = FV.[CodVendedor] from PedidoCompra PC INNER JOIN FornecedorVendedores FV ON (PC.CodFornecedor = FV.CodFornecedor AND PC.Vendedor = FV.Nome)
```

``` SQL
ALTER TABLE PedidoCompra DROP COLUMN Vendedor
GO
ALTER TABLE PedidoCompra DROP COLUMN VendedorFone
GO
ALTER TABLE PedidoCompra DROP COLUMN VendedorMail
```


## Tarefa 4: Continuar função fCriarCadastroDeVendedoresNasCompras PARTE 4

1. Incrementar na função fCriarCadastroDeVendedoresNasCompras
1. Ao final verificar se atendeu o requisito abaixo:
- Criar tabela VendedoresProdutos (codVendedor, CodProduto)
- Popular nova tabela com dados dos últimos 3 pedidos do vendedor

``` SQL
CREATE TABLE [dbo].[FornecedorVendedoresProdutos](
	[CodVendedor] [int] NOT NULL,
	[CodProdut] [float] NOT NULL)
GO
ALTER TABLE [dbo].[FornecedorVendedoresProdutos]  WITH NOCHECK ADD  CONSTRAINT [FK_FornecedorVendedoresProdutos_FornecedorVendedores] FOREIGN KEY([CodVendedor])  
REFERENCES [dbo].[FornecedorVendedores] ([CodVendedor]) ON DELETE CASCADE ON UPDATE CASCADE
GO
ALTER TABLE [dbo].[FornecedorVendedoresProdutos]  WITH NOCHECK ADD CONSTRAINT [FK_FornecedorVendedoresProdutos_Produtos] FOREIGN KEY([CodProduto])  
REFERENCES [dbo].[Produtos] ([codigo]) ON DELETE CASCADE ON UPDATE CASCADE
```

``` SQL
DECLARE @CodVendedor INTEGER
DECLARE NOVOS CURSOR LOCAL FOR

SELECT CodVendedor FROM PedidoCompra WHERE CodVendedor IS NOT NULL group by CodVendedor  

OPEN NOVOS;  
FETCH NEXT FROM NOVOS INTO @CodVendedor;  
WHILE @@FETCH_STATUS = 0  
BEGIN  
	INSERT INTO FornecedorVendedoresProdutos
	SELECT @CodVendedor, CodProduto FROM PedidoCompraProdutos PCP WHERE PCP.CodPedidoCompra in
	(SELECT TOP 3 PC.Codigo FROM PedidoCompra PC where PC.CodVendedor = @CodVendedor group by PC.CodVendedor, PC.DataPedido, PC.Codigo ORDER BY PC.CodVendedor, DataPedido desc)

    FETCH NEXT FROM NOVOS INTO @CodVendedor;  
END  
CLOSE NOVOS;  
DEALLOCATE NOVOS; 
```


## Tarefa 6: Atualizar/Criar classes C# com Telecode
1. Criar a classe FornecedorVendedores pelo aplicativo do Telecode no namespace Telecon.GestaoComercial.Biblioteca.Pedidos

1. Criar a classe FornecedorVendedoresProdutos pelo aplicativo do Telecode no namespace Telecon.GestaoComercial.Biblioteca.Pedidos

1. RECRIAR classe Telecon.GestaoComercial.Biblioteca.Pedidos => PedidoCompra.Telecode pelo aplicativo do Telecode
- Resultado esperado: 
    - Deverão ser removidas da classe as colunas Vendedor, VendedorFone e VendedorEmail;
    - Deverá ser add a nova coluna CodVendedor


## Tarefa 7: Compatibilizar classe PedidoCompra do c# para utilizar os novos campos 

> Tela a ser alterada: Pedidos => Controle de Cargas Pedidos

> Resumo: Ao criar uma nova carga e selecionarmos um pedido, devemos consultar o vendedor na tabela >FornecedorVendedores nos locais abaixo:
>- Ao buscar um pedido pelo formulário de busca
>- Ao selecionar o pedido
>- Ao inserir o pedido na grade
>- Ao selecionar a carga na tela inicial

> Após dar um rebuild teremos algumas linhas que ficarão incompatíveis, corrigir os itens abaixo:

1. Compatibilizar a classe PedidoDeCompra Telecon.GestaoComercial.Biblioteca.Pedidos.PedidoCompra
* Alterar os locais abaixo para utilizar pedidoCompra.CodVendedor em vez dos campos antigos seguindo os padrões dos procedimentos
    * ConverterDataReaderPesquisaPedidoCompra
    * ConverterDataReaderResumidoPesquisa
    * ConverterDataReaderGeral

## Tarefa 8: Compatibilizar tela FrmBuscaPedidosCompra do c# para utilizar os novos campos 

1. Compatibilizar o formulário de busca de busca GestaoComercial.Formularios.Outros.FrmBuscaPedidosCompra
* Alterar o precedimento FrmBuscaPedidosVenda_CarregaStringSql. Utilizar a função FornecedorVendedores.ConsultarChave passando o item.CodVendedor

2. Verificar o funcionamento da pesquisa pelo critério, se necessário programar para funcionar

## Tarefa 9: Compatibilizar tela FrmControleCargas do c# para utilizar os novos campos

1. Compatibilizar o controle de cargas GestaoComercial.Formularios.PedidosVendas.FrmControleCargas
* Alterar o procedimento TxtCodigoPedido_KeyDown. 
    * Para preencher o  txtVendedorPedido.Text utilizar a função FornecedorVendedores.ConsultarChave passando o pedido.CodVendedor
    * Abaixo dessa linha atribuir ao txtVendedorPedido.Tag o pedido.CodVendedor

* Alterar o procedimento AtualizaGridPedidosConformeObjetoPrincipal. Utilizar a função FornecedorVendedores.ConsultarChave passando o pedidoCompra.CodVendedor
* Alterar o procedimento AdicionaPedidosCompraDeCargaExistente. Utilizar a função FornecedorVendedores.ConsultarChave passando o pedido.CodVendedor
* Alterar o procedimento BtnAddPedido_Click. Atribuir ao pedidoAdd.CodVendedor o conteúdo do campo  txtVendedorPedido.Tag
    * No procedimento LimpaCamposTextPedidos limpar o txtVendedorPedido.Tag


## Tarefa 10: Compatibilizar a tela FrmPesquisaPedidosCompra do c# para utilizar os novos campos

1. Compatibilizar o FrmPesquisaPedidosCompra GestaoComercial.Formularios.PedidosVendas.FrmControleCargas   
* Alterar o procedimento CarregarPagina. Utilizar a função FornecedorVendedores.ConsultarChave passando o pedido.CodVendedor
* Alterar o procedimento BtCarga_Click. Precisaremos buscar o código do vendedor no pedido. Criar na classe PedidoCompra o procedimento RetornarCodVendedor que receba o código do pedido e retorne o código do vendedor


## Tarefa 11: Teste de integração do controle de cargas 
* Dar um rebuild no C# e verificar se todos os locais foram contemplados pela análise, corrigir eventuais problemas, caso necessário retornar para a análise.
* Testar os recursos da tela alterados, verificar se está buscando corretamente o vendedor do pedido. Avaliar os recursos de busca, adicionar na grade e pesquisa inicial.
* Verificar se o form de busca de pedidos funcionou corretamente 


## Tarefa 12: Criar tela para manipular os dados dos Vendedores

![image](https://user-images.githubusercontent.com/80394522/134396374-20abcab9-8f8d-4f9e-a29f-24c71913320a.png)

![image](https://user-images.githubusercontent.com/80394522/134260261-62b36ea5-09e3-42ba-a7b6-82e041a1cff1.png)


1. Add formulário FrmFornecedorVendedores no projeto do gestão c#
Link: [link github](https://github.com/Rodrigo80221/AnalisesDeSoftware/tree/main/Classes/FrmFornecedorVendedores)
Add no caminho: GestaoComercial.Formularios.PedidosVendas

1. Implentar formulário c# nas regras abaixo
* Implementar os eventos da barraCadastro. Verificar exemplos em outras telas
* Para atualização e persistência dos dados utilizar as classes FornecedorVendedores, FornecedorVendedoresProdutos e FornecedorProduto
* Para pesquisar os fornecedores utilizar o formulário GestaoComercial.Formularios.Produtos.FrmBuscaFornecedor
* Os campos Cód no Forncedor e última compra deverão vir dos produtos dos fornecedores
* Adicionar também a coluna, ultimo custo. Foi decidido após a criação do protótipo. 

> No novo form implementar as configurações padrões da Telecon.
1. Esc deve ferchar a tela
1. Enter deverá funcionar como tab
1. Ícone do gestão na janela
1. Não deve ter o botão de maximizar
1. Não deve permitir resize na tela
    Propriedade do form: BorderStyle: 1 - Fixed Single
1. O Fechar/Sair cancela a operação, caso alterando na barra de cadastros, mostrar mensagem de aviso.
1. Deve abrir centralizado. 
    Propriedade do form: StartUpPosition = 2 - CenterScreen
1. Deve estar correto quanto ao tab index ao finalizar a tela
1. F4 no campo Cod. Fornecedor deve abrir a busca de fornecedor
1. Implementar tratamentos da ampulheta do mouse no click dos botões de lupa, gravar, excluir, alterar e atualiza tela.

## Tarefa 13: Adicionar opção de imprimir grade no FrmFornecedorVendedores
1. Adicionar um botão ou ícone de imprimir abaixo da grade de produtos
1. Chamar o imprime grid do genéricos
> Pode usar como modelo o formuário GestaoComercial.Formularios.Indicadores.FrmAnalVendaConjunta


## Tarefa 13: Alterar FrmPedidoCompra - Inserir combobox para o vendedor

![image](https://user-images.githubusercontent.com/80394522/134574560-8527e151-4d1a-4d2c-a914-f03aa8768b52.png)

1. Trocar o txtVendedor por um cboVendedor
1. Colorir o BackColor do txtFone e txtEMail de amarelo "&H00C0FFFF&" e desabilitar os campos para edição
1. Criar o procedimento sCarregarComboVendedores que recebe o codigo do fornecedor (double) por parâmetro. Carregar o código no .ItemData
* Neste procedimento carregar o combo cboVendedor com os vendedores disponíveis para o fornecedor do parâmetro de acordo com a tabela FornecedorVendedores. 
* Na última posição do combo carregar um campo vazio para caso o usuário queira deixar o vendedor em branco
1. Chamar esse procedimento no início do procedimento sCarregaVendedor passando o ldbCodigoFornecedor
* Atualizar a consulta do sCarregaVendedor
* No sCarregaVendedor após a consulta posicionar o combo no vendedor utilizado no último pedido desse fornecedor na tabela PedidoCompra oriundo da consulta, caso não haja ou seja null deixar o combo na posição vazio
> utilizar Funcoes.fProcuraItemData
1. No if do sCarregaVendedor retirar o trecho abaixo
``` vb
7       txtVendedor.Text = rsVendedor("Vendedor") & ""
8       txtFone.Text = rsVendedor("VendedorFone") & ""
9       txtEMail.Text = rsVendedor("VendedorMail") & ""
``` 
1. Alterar procedimento sLimparCampos
* Colocar na última posição do combo, que deverá ser a posição em branco
`cboVendedor.ListIndex = cboVendedor.ListCount - 1`

## Tarefa 14: Alterar FrmPedidoCompra - Tratar eventos do cboVendedor
1. Programar o evento KeyPress utilizando Funcoes.fBuscaCombo seguindo o padrão dos outros combos
1. Programar o envento change do cboVendedor, ao alterar o item do combo deverá preencher os campos de email e telefone de acordo com a tabela FornecedorVendedores
1. Testar se com esse tratamento ao alterar o fornecedor já estará preenchendo esses campos, ajustar caso necessário

## Tarefa 15: Alterar FrmPedidoCompra - Criar Label Link Vendedores

1. Acima do combo de Vendedores inserir um label link conforme a imagem
1. Tratar para chamar a tela do C# nas condições abaixo
* Se o combo Vendedor estiver vazio chamar com o botão novo habilitado assim como ocorre no label link de fornecedor, enviar mensagem para o sistema S do C# seguindo o padrão utilizado nas outras telas.
* Se o combo Vendedor estiver preenchido abrir na posição do vendedor
* Inserir tooltip: Abrir o Cadastro de Vendedores dos Fornecedores

## Tarefa 16: Alterar FrmPedidoCompra - fGravaPedido

1. Alterar o procedimento fGravaPedido, retirar do sql os campos vendedor, vendedorFone e VendedorMail e adicionar o campo CodVendedor caso haja um vendedor selecionado, do contrário inserir null
1. Testar comportamento

## Tarefa 17: Alterar FrmPedidoCompra - sGerarRelatorioPorLote
1. Alterar o procedimento sGerarRelatorioPorLote
* Nos dois from add a tabela FornecedorVendedores com left join unindo pelo CodVendedor
* Nas duas consultas em vez de buscar por `PC.Vendedor, PC.VendedorMail`
buscar por `FornecedorVendedores.Vendedor, FornecedorVendedores.VendedorMail`
1. Testar comportamento

## Tarefa 18: Alterar FrmPedidoCompra - txtNumeroPedido_LostFocus
1. Alterar o procedimento txtNumeroPedido_LostFocus
* substituir o código abaixo pelo preencimento do campo cboVendedor
``` VB
35          txtVendedor.Text = rsPedidoCompra("Vendedor") & ""
36          txtFone.Text = rsPedidoCompra("VendedorFone") & ""
37          txtEMail.Text = rsPedidoCompra("VendedorMail") & ""
```

## Tarefa 19: Alterar FrmPedidoCompra - sImprimirRelatorio
1. Alterar o rpt do relatório `Pedido Compra.rpt"` 
1. No rpt adicionar a tabela FornecedorVendedores
1. Criar relacionamento LeftJoin entre a tabela FornecedorVendedores e PedidoCompra
1. Remover os campos antigos e add os novos campos da tabela FonecedorVendedores no relatório
1. Testar comportamento

## Tarefa 20: Alterar FrmPedidoCompra - Inserir e tratar o checkbox chkFiltroVendedor

1. Inserir o check box chkFiltroVendedor na tela conforme a imagem
* se necessário aumentar minimamente o frame de filtros para colocar os labels de observações de 2 em 2 abrindo espaço para novas configurações
* Colocar tooltip: Filtra apenas produtos do vendedor selecionado. (Após o cadastro de um pedido com vendedor selecionado os produtos serão adicionados para esse vendedor.)

1. Alterar o procedimento cboVendedor_Change
* caso o combo cboVendedor_Change tenha um vendedor selecionado deixar o componente chkFiltroVendedor.visible = true, caso esteja em branco deixar false
* Caso tenha vendedor e o campo FornecedorVendedor.MixPorVendedor seja true marcar o  checkbox chkFiltroVendedor, caso seja false desmarcar
* verificar se ao selecionar um fornecedor está trazendo corretamente o vendedor e marcando o checkbox de acordo com o banco

1. Alterar o procedimento sAjustaMenorQue1024x768
* Ajustar o chkFiltroVendedor nas resoluções assim como o chkFiltroFornecedor

1. Alterar o procedimento lblLimpar_Click
* Adicionar chkFiltroVendedor.Value = vbUnchecked

1. Implementar o chkFiltroVendedor_Click
* Add o código abaixo
``` vb
   If Me.ActiveControl.name = "chkFiltroVendedor" Then
       Call sListaProdutos
   End If
```

## Tarefa 21: Alterar FrmPedidoCompra - Tratar o checkbox chkFiltroVendedor no sListaProdutos

1. Remover o procedimento sListaProdutosbkp, não está sendo utilizado

1. Alterar o procedimento sListaProdutos
* Adicionar if no procedimento para caso o chkFiltroVendedor = vbchecked adicionar um filtro 
`sFiltro = sFiltro & " AND P.Codigo In (Select Distinct FP.CD_Produto From FornecedorVendedoresProdutos where CodVendedor =  "item data do cboVendedor")` 
* Fazer similar ao que já acontece com o chkFiltroFornecedor

1. No final da tarefa verificar se só isso já resolve para filtrar os produtos do vendedor. Verificar se ao desmarchar o checkbox do vendedor e alterar a quantidade de um produto fora do mix do vendedor, ao marcar o checkbox do vendedor de novo o produto alterado continuará na grade. Ajustar caso necessário ou verificar com a análise.


## Tarefa 22: Alterar FrmPedidoCompra - Tratar o checkbox chkFiltroVendedor no sInserirProdutosFornecedores

1. Criar o procedimento sSalvarDadosDoVendedor.
1. Chamar o procedimento sSalvarDadosDoVendedor dentro do fGravaPedido após a chamada do sAtualizarDadosGiroEstoque
1. Implemenentar o procedimento sSalvarDadosDoVendedor
* Atualizar o campo FornecedorVendedor.MixPorVendedor =  chkFiltroVendedor.Value , de acordo com o vendedor e o fornecedor selecionado
* O código do vendedor está no item do combo e do fornecedor em uma variável local ldbCodigoFornecedor
* Caso não haja vendedor selecionado sair da sub


## Tarefa 23: Alterar FrmPedidoCompra - Inserir produtos do pedido no MIX do vendedor

1. Continuar implementando o procedimento sInserirProdutosFornecedores
1. Após salvar o campo FornecedorVendedor.MixPorVendedor salvar os produtos do pedido na tabela FornecedorVendedoresProdutos de acordo com as regras abaixo:
* Atualizar a tabela FornecedorVendedoresProdutos com os produtos do pedido caso esses produtos ainda não estejam lá. 
* O insert dever ter uma estrutura semelhante ao que inserimos no verifica banco `INSERT INTO FornecedorVendedoresProdutos`
* Inserir sempre que for selecionado o nome de um vendedor





## Tarefa 20: Alterar FrmPedidoCompra - menu desassociar 
> Ao clicar bom botão direito na grade temos a opção de desvincular o produto do fornecedor. Implementar o mesmo funcionamento com os vendedores

1. Criar o procedimento sDesvincularProdutoFornecedorVendedores
1. Criar os 3 menus para chamar esse procedimento. Os menus variam de acordo com o produto da grade, se tem quantidade, se é bonificação ou se é Trocas

> Caminho do menu: Tools => Menu Editor (Na função design do Form)

>Fazer similar a fornecedor
sDesvincularProdutoFornecedor
mnuDesvincularFornecedor_Click
mnuDesvincularFornecedorBonf_Click
mnuDesvincularFornecedorTrocas_Click

## Tarefa 21: Criar outras chamadas para a tela de Cadastro de Vendedores
> Criar módulo para Gerenciar o recurso FrmFornecedorVendedores nos Sistema S

Resultado esperado: A tela Cadastro de Vendedores deverá ser chamada pelo menu de fornecedores da tela clássica e pelo setores de Pedidos e Fornecedores do Sistemas S e R

1. Criar verifica banco para inserir módulo. Seguir padrão de outros módulos criados

- Utilizar os procedimentos:
`sInserirModulo`
`sInserirModuloSistemasRS`
- Executar update em operadores_modulos para ativar o módulo

Descrição das variáveis que serão utilizadas:

`sGrupo = PEDIDOS,FORNECEDORES`
`sDescrição = Cadastro de Vendedores dos Fornecedores`
`sPalavraChave = FrmFornecedorVendedores`

1. Tratar em mdlGestao.sCarregaModulos, conforme padrão dos procedimentos

1. Chamar na tela clássica no menu de  Fornecedores > Fornecedor Vendedores

1. Tratar no frmModulos.cmdRestaurar_Click conforme padrão do procedimento

## Tarefa 23: Alterar pedido de compra grande
1. Tratar o insert do procedimento FrmPedidoCompraProdutos.fInserirPedidoCabecalho. Substituir os campos Vendedor, Email e Fone por CodVendedor.
1. Excluir variáveis que não serão mais utilizadas.
1. Verificar o básico do funcionamento na tela.


## Tarefa 24: Alterar relatório Análise Mensal de Vendas
 1. Inserir os campos conforme a imagem adicionando o campo Vendedor, reorganizando e alinhando os outros componentes
    * O componente Text deve ser do tipo `Genéricos.Controles.Busca` assim como o do Fonecedor
1. Altear a classe GestaoComercial.Formularios.Indicadores.FrmAnaliseMensalVendas:
* Na `partial class FrmAnaliseMensalVendas` criar a propriedade `private Telecon.Genericos.Controles.Busca busVendedor;`
* No FrmAnaliseMensalVendas implementar o procedimento `busVendedor_CarregaDados()` para implementar o busVendedor. Seguir todo o Modelo da propriedade busFornecedor
    * usar busVendedor.Colunas = 2 (Código e Nome);
1. Criar novo parâmentro no procedimento `RelAnaliseMensalVendas.Consultar` e implementar a consulta da mesma forma que ocorre no fornecedor


## Tarefa 25: Criar campo no NF_Entradas para inserir o Pedido e o Vendedor

1. Adicionar os campos na tela conforme a imagem
1. Cuidar o TabIndex

## Tarefa 26: Tornar funcional o campo do pedido e do vendedor 

Vendedor
1. Criar procedimento `sCarregarComboVendedores` 
1. Chamar após selecionar um fornecedor
1. Programar o Label Link do Vendedor assim como na tela do Pedido de Compra

Pedido
1. Programar a busca de pedido
1. Programar o Label Link do Pedido
usar nas notas de entrada sCarregarPedidoCompra






















ficou pendente saber como abre o menu trocas botão direito

colocar pedidos na nota
verifica o status do pedido e marcar como entregue 
consulta de pedidos com dados básicos 
somente pedidos autorizados , avisar que não está autorizado
se buscar pelo pedido já preenche o vendedor
o pedido tem que estar autorizado
encerrar o pedido
abrir o pedido

primeiro selecione um pedido
o chama o pedido

data da entrega do pedido para a data da entrada da nota
encerrado = true



controle de entradas - mostrar o valor de venda no pedido
colorir
tooltip
bloquear a alteração de preço
só de nota que tiver relação com o pedido





