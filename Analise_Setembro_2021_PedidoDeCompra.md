# Épico: Controle de Vendedores nas Compras
Data de início do recurso: 16/09/2021  
## Problema principal a ser resolvido
Na tela de pedido de compras o usuário deseja informar o vendedor e receber a listagem de produtos desse vendedor.
Clientes envolvidos: Santana e Nova Compra
## Impactos
Pedidos de Compra
Notas de Entrada
Relatório Análise Mensal de Vendas
Controle de Cargas Pedidos
Pedido de Compra Tishler
## Pré Requisitos
Tela Pedido De Compras
    - Recurso para selecionar o vendedor;
    - Opção para visualizar apenas produtos desse vendedor;
    - Recurso para Desvincular produtos do vendedor;
Notas de Entradas
    - Recurso para relacionar a nota ao seu pedido de compra;
    - Recurso para selecionar o vendedor, para análisar vendas desse vendedor no relatório;
Relatório Análise Mensal de Vendas
    - Filtro de vendas apenas desse vendedor.

## Solução Final Analisada

### Atualização do Banco de dados
    * Criação da tabela FornecedorVendedores
        * Migração dos vendedores da tabela PedidoCompra para a tabela FornecedorVendedores
            * Nota: Manter o email e telefone do último pedido do vendedor
    * Criação da tabela NF_Entradas_Pedidos
    * Alteração da tabela PedidoCompras
        * Adicionar coluna CodVendedor
        * Atualizar coluna com o código dos vendedores
        * Remover da tabela os campos antigos Vendedor, Fone e Email
    * Criação da tabela VendedoresProdutos
        * Nota: Popular nova tabela com dados dos últimos 3 pedidos do vendedor
    * Criação dos módulos para os recursos do sistema S    
### Compatibilidade dos recursos do C#
    * Criar a classe Telecode FornecedorVendedores
    * Criar a classe Telecode FornecedorVendedoresProdutos
    * Recriar a classe Telecode PedidoCompra
    * Alterar formulário de busca FrmBuscaPedidosCompra do Controle de Cargas Pedidos
        * Buscar o campo de vendedor da nova tabela FornecedorVendedores
    * Alterar o formulário FrmControleCargas (Controle de Cargas Pedidos)
        * Ao selecionar o pedido buscar o vendedor da nova tabela FornecedorVendedores
        * Ao adicionar o pedido na grade buscar os dados do vendedor da tabela FornecedorVendedores
        * Ao atualizar a tela buscar os dados do vendedor da tabela FornecedorVendedores
    * Alterar o formulário de busca FrmPesquisaPedidosCompra do Controle de Cargas Pedidos
        * Buscar o campo de vendedor da nova tabela FornecedorVendedores
### Criar formulário C# FrmCadFornecedorVendedores (Cadastro de Vendedores dos Fornecedores)        
    * Recurso para cadastrar, visualizar e alterar dados de vendedores
    * Recurso para visualizar o mix de produtos do vendedor
    * Recurso para impressão do mix de produtos do vendedor
    * Chamada pela tela clássica e pelo sistemas S

### Alteração do formulário FrmPedidoCompra (Pedido de Compra)
    * Adicionar combo para seleção do vendedores
    * Deixar os campos de Telefone e Email apenas leitura
    * Criar label link para chamar o Cadastro de Vendedores
    * Atualizar novos campos nos relatórios
    * Criar a caixa de seleção Mix do Vendedor
        * Filtrar produtos do vendedor
    * Destacar na grade em itálico produtos que estão fora do Mix do Vendedor
    * Salvar Mix do Vendedor
    * Desvincular produtos do Mix do Vendedor
        * Nota: Ao desvincular produtos do fornecedor, desvincular também do Vendedor

### Alteração do formulário FrmPedidoCompraProdutos (Pedido de Compra Produtos)
    * Compatibilizar para utilizar os novos campos do vendedor da tabela FornecedorVendedores

### Alteração do relatório C# Análise Mensal de Vendas FrmAnaliseMensalVendas
    * Criação do Filtro por vendedor

### Criação do Formulário de Busca C# FrmBuscaPedidoCompra
    * Herança do FrmPesquisa utilizando a classe FornecedorVendedores
### Alteração do formulário FrmNFEntradas (Notas de Entrada)
    * Inserir campos para seleção do Pedido de Compra    
    * Inserir label link para os Pedidos de Compra
    * Inserir campo para seleção do Vendedor
    * Inserir label link para o Cad de vendedores
    * Realizar alterações ao gravar a nota
        * Associar produtos da nota ao Mix do Vendedor
        * Finalizar pedido de compra
        * Alterar Data Entrega do Pedido de Compra
        
### Alteração do formulário frmControleEntradas3 (Controle de Entradas)
    * Destacar Preço de Venda de produtos com preço de venda definido no Pedido de Compra
        * Nota: Preço de Venda bloqueado para edição
    * Colocar tooltip mostranto o motivo do destaque e o código do Pedido

 ## Tarefa 1: Criar função no atualiza banco fCriarCadastroDeVendedoresNasCompras  PARTE 1

1. Criar função fCriarCadastroDeVendedoresNasCompras
1. Adicionar os comandos abaixo

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
ALTER TABLE [dbo].[FornecedorVendedores] ADD CONSTRAINT chk_NomeVendedor CHECK (LEN(Nome) >= 1)
```

``` SQL
CREATE TABLE [dbo].[NF_Entradas_Pedidos](
	[CD_NOTA] [float] NOT NULL,
	[CodPedido] [float] NULL,
	[CodVendedor] [int] NULL, CONSTRAINT [PK_NF_Entradas_Pedidos] PRIMARY KEY CLUSTERED ([CD_NOTA] ASC))
GO
ALTER TABLE [dbo].[NF_Entradas_Pedidos]  WITH NOCHECK ADD  CONSTRAINT [FK_NF_EntradasPedidos_NFEntradas] FOREIGN KEY([CD_NOTA])  
REFERENCES [dbo].[NF_ENTRADAS] ([CD_NOTA]) ON DELETE CASCADE ON UPDATE CASCADE
GO
ALTER TABLE [dbo].[NF_Entradas_Pedidos]  WITH NOCHECK ADD  CONSTRAINT [FK_NF_EntradasPedidos_PedidoCompra] FOREIGN KEY([CodPedido])  
REFERENCES [dbo].[PedidoCompra] ([Codigo]) ON DELETE CASCADE
GO
ALTER TABLE [dbo].[NF_Entradas_Pedidos]  WITH NOCHECK ADD  CONSTRAINT [FK_NF_EntradasPedidos_FornecedorVendedores] FOREIGN KEY([CodVendedor])  
REFERENCES [dbo].[FornecedorVendedores] ([CodVendedor]) ON DELETE CASCADE
GO
ALTER TABLE [dbo].[NF_Entradas_Pedidos] ADD CONSTRAINT chk_NFEntradasPedidos_DadosNulos CHECK (CodPedido is not null or CodVendedor is not null)

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
	[CodProduto] [float] NOT NULL)
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


## Tarefa 5: Atualizar/Criar classes C# com Telecode
1. Criar a classe FornecedorVendedores pelo aplicativo do Telecode no namespace Telecon.GestaoComercial.Biblioteca.Pedidos

1. Criar a classe FornecedorVendedoresProdutos pelo aplicativo do Telecode no namespace Telecon.GestaoComercial.Biblioteca.Pedidos

1. RECRIAR classe Telecon.GestaoComercial.Biblioteca.Pedidos => PedidoCompra.Telecode pelo aplicativo do Telecode
- Resultado esperado: 
    - Deverão ser removidas da classe as colunas Vendedor, VendedorFone e VendedorEmail;
    - Deverá ser add a nova coluna CodVendedor


## Tarefa 6: Compatibilizar classe PedidoCompra do c# para utilizar os novos campos 

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

## Tarefa 7: Compatibilizar tela FrmBuscaPedidosCompra do c# para utilizar os novos campos 

1. Compatibilizar o formulário de busca de busca GestaoComercial.Formularios.Outros.FrmBuscaPedidosCompra
* Alterar o precedimento FrmBuscaPedidosVenda_CarregaStringSql. Utilizar a função FornecedorVendedores.ConsultarChave passando o item.CodVendedor

2. Verificar o funcionamento da pesquisa pelo critério, se necessário programar para funcionar

## Tarefa 8: Compatibilizar tela FrmControleCargas do c# para utilizar os novos campos

1. Compatibilizar o controle de cargas GestaoComercial.Formularios.PedidosVendas.FrmControleCargas
* Alterar o procedimento TxtCodigoPedido_KeyDown. 
    * Para preencher o  txtVendedorPedido.Text utilizar a função FornecedorVendedores.ConsultarChave passando o pedido.CodVendedor
    * Abaixo dessa linha atribuir ao txtVendedorPedido.Tag o pedido.CodVendedor

* Alterar o procedimento AtualizaGridPedidosConformeObjetoPrincipal. Utilizar a função FornecedorVendedores.ConsultarChave passando o pedidoCompra.CodVendedor
* Alterar o procedimento AdicionaPedidosCompraDeCargaExistente. Utilizar a função FornecedorVendedores.ConsultarChave passando o pedido.CodVendedor
* Alterar o procedimento BtnAddPedido_Click. Atribuir ao pedidoAdd.CodVendedor o conteúdo do campo  txtVendedorPedido.Tag
    * No procedimento LimpaCamposTextPedidos limpar o txtVendedorPedido.Tag


## Tarefa 9: Compatibilizar a tela FrmPesquisaPedidosCompra do c# para utilizar os novos campos

1. Compatibilizar o FrmPesquisaPedidosCompra GestaoComercial.Formularios.PedidosVendas.FrmControleCargas   
* Alterar o procedimento CarregarPagina. Utilizar a função FornecedorVendedores.ConsultarChave passando o pedido.CodVendedor
* Alterar o procedimento BtCarga_Click. Precisaremos buscar o código do vendedor no pedido. Criar na classe PedidoCompra o procedimento RetornarCodVendedor que receba o código do pedido e retorne o código do vendedor


## Tarefa 10: Teste de integração do controle de cargas 
* Dar um rebuild no C# e verificar se todos os locais foram contemplados pela análise, corrigir eventuais problemas, caso necessário retornar para a análise.
* Testar os recursos da tela alterados, verificar se está buscando corretamente o vendedor do pedido. Avaliar os recursos de busca, adicionar na grade e pesquisa inicial.
* Verificar se o form de busca de pedidos funcionou corretamente 


## Tarefa 11: Criar tela para manipular os dados dos Vendedores

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

## Tarefa 12: Adicionar opção de imprimir grade no FrmFornecedorVendedores
1. Adicionar um botão ou ícone de imprimir abaixo da grade de produtos
1. Chamar o imprime grid do genéricos
> Pode usar como modelo o formuário GestaoComercial.Formularios.Indicadores.FrmAnalVendaConjunta


## Tarefa 13: Alterar FrmPedidoCompra - Inserir combobox para o vendedor

![image](https://user-images.githubusercontent.com/80394522/135516183-b0d21fed-ce06-43d4-b903-19c6c09ca9a5.png)

1. Trocar o txtVendedor por um cboVendedor
1. Colorir o BackColor do txtFone e txtEMail de amarelo "&H00C0FFFF&" e desabilitar os campos para edição
1. No Funcoes criar o procedimento `Public void sCarregarComboVendedores` que recebe o combo e o codigo do fornecedor (double) por parâmetro. Carregar o CodVendedor no .ItemData
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
1. Criar em Funcoes a função `fAlterarFornecedorVendedores`, criar um parâmetro byval double dbCodVendedor.
1. Nesta função criada programar de acordo com os passos abaixo
1. Se o dbCodVendedor = 0 então chamar a tela c# com o novo habilitado para cadastrar
1. Se o dbCodVendedor <> 0 então chamar a tela c# com o alterar habilitado para atualizar um vendedor
* a função `fAlterarFornecedorVendedores` deve retornar o CodVendedor do vendedor cadastrado ou atualizado
* seguir o padrão de funcionamento utilizado nos outros label links dessa tela

1. Programar no keypress do link uma chamada para a função `Funcoes.fAlterarFornecedorVendedores`, após passar por essa função chamar novamente o procedimento `Funcoes.sCarregarComboVendedores`
    * Após esses passos posicionar o combo de vendedores no CodVendedor que retornou da função `fAlterarFornecedorVendedores` 


* Se o combo Vendedor estiver preenchido abrir na posição do vendedor

* Inserir tooltip no label link: Abrir o Cadastro de Vendedores dos Fornecedores

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

![image](https://user-images.githubusercontent.com/80394522/135516470-96e1ab04-1e97-40e4-81a2-def3c020f04b.png)

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
1. Implemenentar o procedimento sSalvarDadosDoVendedor seguindo as regras abaixo:
* Atualizar o campo FornecedorVendedor.MixPorVendedor =  chkFiltroVendedor.Value , de acordo com o vendedor e o fornecedor selecionado
* O código do vendedor está no item do combo e do fornecedor em uma variável local ldbCodigoFornecedor
* Caso não haja vendedor selecionado sair da sub


## Tarefa 23: Alterar FrmPedidoCompra - Inserir produtos do pedido no MIX do vendedor

> Continuar implementando o procedimento `sSalvarDadosDoVendedor`
1. Após salvar o campo FornecedorVendedor.MixPorVendedor salvar os produtos do pedido na tabela FornecedorVendedoresProdutos de acordo com as regras abaixo:

1. Dar um insert em `FornecedorVendedoresProdutos` adicionando nesta tabela todos os produtos que estão no pedido com qtd > 0, estão na tabela `Fornecedor_Produtos` e ainda não estão associados a esse vendedor
* Por motivos de performance utilizar um insert into FornecedorVendedoresProdutos where exists in PedidoCompraProdutos com Quantidade > 0 and exists in Fornecedor_Produtos and not exists in FornecedorVendedoresProdutos.
* Se não for selecionado um vendedor no combo dar um exit sub no início do procedimento


## Tarefa 24: Alterar FrmPedidoCompra - menu desassociar 

![image](https://user-images.githubusercontent.com/80394522/135518761-c17b14af-f980-4e31-898f-282157b22c7f.png)

> Ao clicar bom botão direito na grade temos a opção de desvincular o produto do fornecedor. Implementar o mesmo funcionamento com os vendedores

1. Criar o procedimento sDesvincularProdutoFornecedorVendedores
1. Criar os 3 menus para chamar esse procedimento. Os menus variam de acordo com o produto da grade, se tem quantidade, se é bonificação ou se é Trocas

> Caminho do menu: Tools => Menu Editor (Na função design do Form)

>Fazer similar a fornecedor
sDesvincularProdutoFornecedor
mnuDesvincularFornecedor_Click
mnuDesvincularFornecedorBonf_Click
mnuDesvincularFornecedorTrocas_Click

1. O menu deverá aparecer apenas se tivermos um vendedor selecionado no combo
1. Ao desvincular um produto do fornecedor e tivermos um vendedor selecionado, devemos também desvincular o produto ao vendedor, chamando o procedimento `sDesvincularProdutoFornecedorVendedores`


## Tarefa 25: Alterar FrmPedidoCompra - Deixar em itálico na grade produtos fora do MIX do vendedor

>Nesta tarefa criaremos a funcionalidade de deixar os itens fora do mix do vendedor em itálico na grade, mas por motivos de performance não realizaremos mais uma consulta no banco honerando a tela, iremos utilizar um recordset desconectado

1. Nas declarações do FrmPedidoCompra criar a variável abaixo
``` vb
Private lrsConsultaFfornecedorVendedoresProdutos As ADODB.Recordset
```
1. Criar o procedimento `fCarregarRecordsetVendedoresProdutos` para carregar os dados da tabela `FornecedorVendedoresProdrodutos` na memória do cliente (recordset lrsConsultaFfornecedorVendedoresProdutos) de acordo com as regras abaixo:
* Realizar consulta na tabela `FornecedorVendedoresProdrodutos` de acordo com o vendedor e o fornecedor passador por parâmentro
* Criar o procedimento no mesmo padrão do procedimento `frmControleEntradas3.fCarregarRecordAtacado`

1. Chamar o procedimento `fCarregarRecordsetVendedoresProdutos` nos locais abaixo:
* Ao alterar um item no combo do vendedor
* No final do procedimento `sDesvincularProdutoFornecedorVendedores` 
* No final do procedimento `sSalvarDadosDoVendedor`

1. Criar o procedimento `sDestacadarProdutosForaDoMixDoVendedor` de acordo com as regras abaixo:
* Deve receber por parâmetro
    * a linha da grade
    * código do fornecedor
* Verificar se o produto da linha está contido no recordset lrsConsultaFfornecedorVendedoresProdutos.Filter
    * Caso não esteja deixar o código do produto, o código de barras e a descrição do produto em itálico na grade
* Chamar o procedimento no final do loop do `sListaProdutos`    



## Tarefa 26: Criar outras chamadas para a tela de Cadastro de Vendedores
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

1. Tratar em Funcoes.sCarregaModulos, conforme padrão dos procedimentos

1. Chamar na tela clássica no menu de  Fornecedores > Fornecedor Vendedores

1. Tratar no frmModulos.cmdRestaurar_Click conforme padrão do procedimento

## Tarefa 27: Alterar pedido de compra grande
1. Tratar o insert do procedimento FrmPedidoCompraProdutos.fInserirPedidoCabecalho. Substituir os campos Vendedor, Email e Fone por CodVendedor.
1. Excluir variáveis que não serão mais utilizadas.
1. Verificar o básico do funcionamento na tela.


## Tarefa 28: Alterar relatório Análise Mensal de Vendas

![image](https://user-images.githubusercontent.com/80394522/135518929-1857dc2b-4118-4762-88a9-8558b0c3d041.png)

 1. Inserir os campos conforme a imagem adicionando o campo Vendedor, reorganizando e alinhando os outros componentes
    * O componente Text deve ser do tipo `Genéricos.Controles.Busca` assim como o do Fonecedor
1. Altear a classe GestaoComercial.Formularios.Indicadores.FrmAnaliseMensalVendas:
* Na `partial class FrmAnaliseMensalVendas` criar a propriedade `private Telecon.Genericos.Controles.Busca busVendedor;`
* No FrmAnaliseMensalVendas implementar o procedimento `busVendedor_CarregaDados()` para implementar o busVendedor. Seguir todo o Modelo da propriedade busFornecedor
    * usar busVendedor.Colunas = 2 (Código e Nome);
1. Criar novo parâmentro no procedimento `RelAnaliseMensalVendas.Consultar` e implementar a consulta da mesma forma que ocorre no fornecedor


## Tarefa 29: Criar campos no NF_Entradas para inserir o Pedido e o Vendedor

![image](https://user-images.githubusercontent.com/80394522/135519699-f8d5731c-f528-4047-9f5f-8ac14705730c.png)

1. Adicionar os campos na tela conforme a imagem
1. O campo do código do pedido só deverá aceitar números (textMask = Integer)
1. Cuidar o TabIndex
1. Inserir um ícone de ajuda ao lado da busca do pedido, ao clicar mostrar a mensagem abaixo:
```
Ao selecionar um pedido de compra na nota você completará os passos abaixo:
- Preencherá o campo vendedor na nota de entrada caso ele tenha sido informado no pedido;
- Encerrará o Pedido de Compra;
- Atualizará a Data de Entrega do Pedido de Compra;
- Enviará para o Controle de Entradas o Valor de Venda do produto caso ele tenha sido definido no pedido.
```
## Tarefa 30: Tornar funcional os componentes de vendedor nas NF Entradas

1. Chamar o procedimento `Funcoes.sCarregarComboVendedores `após selecionar um fornecedor no `fBuscaFornecedor` acima da linha abaixo:
``` vb
71      fBuscaFornecedor = True
```
1. No procedimento `sLimpaCamposFornecedor` passar o combo do vendedor para o último List Index que deve ser o em branco
1. Programar o Label Link do Vendedor assim como na tela do Pedido de Compra chamando a função criada no Funcoes
    


## Tarefa 31: Criar formulário de busca de pedido de compra FrmBuscaPedidoCompra
1. Criar diretório `GestaoComercial.Formularios.PedidosCompras`
1. Adicionar Formulário no namespace `GestaoComercial.Formularios.PedidosCompras`
1. Adicionar herança do formulário `FrmPesquisa`. Este formulário deverá seguir o padrão dos formulários de pesquisa da Telecon
1. Implementar o `AdicionaColunasList`, (acertar as larguras da colunas)
``` c#
        base.AdicionarColunaLista("Nro. Pedido", 50, typeof(string));
        base.AdicionarColunaLista("Dt. Entrega", 50, typeof(string));
        base.AdicionarColunaLista("Fornecedor", 150, typeof(string));
        base.AdicionarColunaLista("Loja", 100, typeof(string));
        base.AdicionarColunaLista("Comprador", 100, typeof(string));
        base.AdicionarColunaLista("Data Pedido", 50, typeof(string));
```
1. Implementar critérios
=> Campo + Valor no Combo
``` c#
        base.AdicionarCriterio(typeof(int), "PedidoCompra.Codigo"; "Nro. Pedido");
        base.AdicionarCriterio(typeof(string), "PedidoCompra.DataEntrega", "Dt. Entrega");
        base.AdicionarCriterio(typeof(string), "Fornecedor.Fantasia", "Fornecedor");
        base.AdicionarCriterio(typeof(string), "Loja.Nome", "Loja");
        base.AdicionarCriterio(typeof(string), "Operador.Nome", "Comprador");
        base.AdicionarCriterio(typeof(string), "PedidoCompra.Datapedido", "Data Pedido");
```
1. Implementar Ordenação
``` c#
        base.AdicionarOrdenacao(typeof(int), "Nro. Pedido", "Nro. Pedido");
        base.AdicionarOrdenacao(typeof(string), "Dt. Entrega", "Dt. Entrega");
        base.AdicionarOrdenacao(typeof(string), "Fornecedor", "Fornecedor");
        base.AdicionarOrdenacao(typeof(string), "Loja", "Loja");
        base.AdicionarOrdenacao(typeof(string), "Comprador", "Comprador");
        base.AdicionarOrdenacao(typeof(string), "Data Pedido", "Data Pedido");
```
1. Implementar o `CarregaStringSql`
* utilizar a classe `PedidoCompra`
* tratar o cursor do mouse durante a pesquisa (trocar seta por ampulheta)



## Tarefa 32: Tornar funcional os componentes de Pedidos nas NF Entradas

Pedido
1. Programar o botão de busca de pedido, utilizar a tela c# criada FrmBuscaPedidoCompra
1. Programar o Keypress do código do pedido
* Ao digitar o código do pedido e der enter verificar se o pedido existe. Se existe atribuir esse código ao txt.Text. Se não existe apresentar mensagem.
* Se ao selecionar um pedido o fornecedor da nota estiver em branco dar a mensagem `Primeiro selecione o fornecedor!`, Colocar o foco no text do fornecedor
* Se o pedido existe e possui vendedor já posicionar o combo do vendedor, tratar para caso o vendedor do pedido não esteja no combo, pode acontecer caso o cnpj do pedido seja diferente do cnpj da nota, neste caso deixar o combo em branco.
* Se clicar F4 abrir a busca de pedidos c#, implentar a busca pela lupa
* Se o cliente utiliza o módulo de autorizaçao de pedidos (821) teremos que validar se o pedido foi autorizado, e somente se ele foi autorizado poderemos adicionar ele no text. 
Testar utilizando `If oUsuarioAcesso.Habilitado(821, giCodLojaPadrao) Then`. Caso não tenha sido autorizado mostrar a mensagem `O pedido não foi autorizado! Solicite autorização no pedido de compras.`. Neste caso deixar zero no text do pedido.

1. Programar o Label Link do Pedido
* Ao clicar no link se o código do pedido for <> 0 chamar `sAbrirForm frmPedidoCompra,dbCodPedido`


## Tarefa 33: Programar o Atualiza tela para atualizar os campos de pedido e vendedor

1. No procedimento `sConsultar` add no select o código abaixo
``` 
//Campos
NEP.CodVendedor
NEP.CodPedido
// Join
LEFT JOIN NF_Entradas_Pedidos NEP ON  N.CD_NOTA = NEP.CD_NOTA
```
1. No procedimento `AtualizaTela` após a linha ` 37  fBuscaFornecedor` posicionar o combobox do vendedor na posição correta recebida do [CodVendedor]
1. Após o CodVendedor carregar o text do pedido com o [CodPedido]. Carregar o .text e o .tag

## Tarefa 34: Gravar os novos campos das NF_Entradas

>Resumo: Ao inserirmos ou alterarmos os campos de pedido e vendedor nas notas de entradas temos que atualizar a tabela NF_Entradas_Pedidos e fechar o pedido de compra

1. Primeiro passo, criar no FrmNFEntradas o procedimento `sEncerrarPedido` `Public Sub sEncerrarPedido(ByVal iCodOperador As Integer, ByVal dbCodPedido As Double, ByVal dtDataEntrega As Datem, byval iCodVendedor as integer) `
* Add nele o código abaixo
``` vb
11          sSQL = "Update PedidoCompra "
12          sSQL = sSQL & "Set Finalizado = 1 , CodOperadorFinalizacao = " & iCodOperador & "," &  "DataEntrega =" & Funcoes.fTrataData(dtDataEntrega, True)
13          sSQL = sSQL & " Where Codigo = " & dbCodPedido
14          oConexao.Execute sSQL
```
Após esse código  dar um insert na tabela NF_Entradas_Pedidos com os dados de pedido, se vier zero colocar NULL

1. Segundo passo, criar no FrmNFEntradas o procedimento `sReabrirPedido` `Public Sub sReabrirPedido(ByVal dbCodPedido As Double) `
* Add nele o código abaixo
    ``` vb
        sSQL = "Update PedidoCompra "
        sSQL = sSQL & "Set Finalizado = 0"
        sSQL = sSQL & " Where Codigo = " & dbCodPedido
        oConexao.Execute sSQL
    ```
Após esse código  dar um delete na tabela NF_Entradas_Pedidos com os dados de pedido


1. Terceiro Passo: Criar o procedimento `sAtualizarDadosDePedidoDeCompra` e chamar ele no `cadNotas_Gravar` no final após o `sExibirAvisoFunRural`.
 Implementação do `sAtualizarDadosDePedidoDeCompra`
* caso inserindo `If cadNotas.sAcao = "Inserindo Registro" Then` 
    * se o o text do cód. pedido <> 0 chamar o `sEncerrarPedido` com dtDataEntrega recebendo a data de entrada da nota 
    * após caso o text do cód. pedido <> 0 
    * Dar o insert do vendedor em NF_Entradas_Pedido ou colocar null

* Caso Alterando `If cadNotas.sAcao = "Alterando Registro" Then`   
    * Se o .txt.Text do cód. pedido = 0 e txt.Tag <> 0 então o usuário removeu o pedido. 
        * chamar o sReabrirPedido para reabrir o pedido

    * Se o .txt.Text <> txt.Tag e <> 0 então o usuário alterou o pedido. 
        * Reabrir o pedido anterior sReabrirPedido txt.Tab
        * Encerrar o pedido atual sEncerrarPedido txt.Text
    * Dar o update do vendedor em NF_Entradas_Pedido ou colocar null

## Tarefa 35: Gravar os novos produtos das NF_Entradas no vendedor

1. Nas NFEntradas criar o procedimento `sinserirProdutosDoVendedor`
1. O procedimento deve ser chamado no final do `sAtualizarDadosDePedidoDeCompra`. (Executa no gravar e no alterar)
1. Dar um insert em `FornecedorVendedoresProdutos` adicionando nesta tabela todos os produtos que estão na nota e ainda não estão associados a esse vendedor
* Por motivos de performance utilizar um insert into FornecedorVendedoresProdutos where exists in NF_Entradas_produtos and not exists in FornecedorVendedoresProdutos. Tratar com distinct, lembrando que o produto pode vir duas vezes na nota


## Tarefa 36: Alterar Controle de entradas - Criar recordset desconectado de pedidos

>Fluxo do resultado esperado das tarefas referentes ao controle de entradas
- Usuário cria um pedido para o produto x
- No pedido o usuário seta um novo valor de venda para o produto x
- Usuário recebe o pedido e lança a nota de entrada
- Usuário relaciona o pedido na nota de entrada
- Sistema finaliza o pedido
- Usuário abre o controle de entradas no intervalo de datas da nota
- O produto x deverá ser listado e na coluna preço venda já deverá estar preenchido o valor inserido no pedido. Deverá ficar na cor amarela como no pedido, e não deve ser permitida a alteração do preço de venda.


1. Nas declarações do frmControleEntradas3 declarar o recordset abaixo junto com as declareções dos outros recorsets da tela 
``` vb
Private lrsPedidoProdutos As ADODB.Recordset
```
1. Criar o procedimento `sCarregarRecordsetPedidoProdutos`. Neste procedimento carregar o recordset `lrsPedidoProdutos` da mesma forma que foi feito na função `fCarregarRecordAtacado`
* Criar os parametros dtDataInicio e dtDataFim no procedimento e add no select abaixo. 
``` sql
SELECT NF.CD_NOTA,NFP.CodPedido,PCP.CodProduto,PCP.NovoValorVenda FROM 
NF_Entradas NF 
INNER JOIN [NF_Entradas_Pedidos] NFP ON NF.CD_NOTA = NFP.CD_NOTA
INNER JOIN [PedidoCompra] PC ON NFP.CodPedido = PC.Codigo 
INNER JOIN [PedidoCompraProdutos] PCP ON PC.Codigo = PCP.CodPedidoCompra 
WHERE DATAS
```
1. Chamar o procedimento `sCarregarRecordsetPedidoProdutos` no `sVisualizarDados` antes da a linha
``` vb
11  sCarregaDados
```
* chamar apenas se `gbChamouDoFormCentralColeta = false and gbChamadoPelosPedidos = false`
> Se a tela FrmControleEntradas3 for chamada pela tela de pedido de compra não deverá ter impacto


## Tarefa 37: Alterar Controle de entradas - Setar valor do pedido na grade e colorir

1. Nas declarações do frmControleEntradas3 declarar a constante abaixo junto com as declareções das outras colorações
``` vb
Private Const ldbCorAmareloPedidos As Double = &H80FFFF
```
> Abaixo iremos criar funções e códigos para colocar o preço de venda definido no pedido na grade , alterar a cor e colocar um tooltip

1. Criar a função `fRetornarValorDoPedido` que recebe o código nota e o código produto por parâmentro
* O código da nota será uma string e poderá ter vários códigos de notas separados por ","
* A função deverá retornar o campo `PCP.NovoValorVenda` do último pedido
* A função deverá pesquisar no `lrsPedidoProdutos.Filter` assim como na função `fObterDescricaoPack` buscando performance
* Se não encontrar nada retornar zero

> Para constar: No controle de entradas se utilizar a configuração **Ocultar Variação de Custo** ele agrupa os produtos iguais e coloca os cod notas agrupadas na coluna de notas, neste caso teremos que considerar que pode ter várias notas

1. Alterar a função `fRetornarValorDoPedido` para receber o parâmetro `ByRef dbCodPedido as double` setar essa váriavel com o código do pedido selecionado
* Se não encontrar nada atribuir zero ao dbCodPedido

1.  No procedimento `sAplicarRegraColoracoes` após o trexo de código abaixo:
``` vb
If CCur(.TextMatrix(.row, btValorVenda)) <> CCur(.TextMatrix(.row, btValorVendaAntesAlteracao)) And .CellBackColor <> &H80FFFF Then
    .Col = btValorVenda
    .CellBackColor = ldbCorCinza
    .CellForeColor = ldbCorAzul
    .negrito = True
End If
```
APÓS ESSE if inserir o código abaixo corrigindo os parâmetros necessários
``` vb
    if gbChamouDoFormCentralColeta = false and gbChamadoPelosPedidos = false then
            cValorPedido = fRetornarValorDoPedido (btCdNotasProdAgrupados,btCodigoProduto)
            If fValorPedido <> 0 Then
                .CellBackColor = &H80FFFF
                .TextMatrix(.row, btValorVenda) = format(cValorPedido, "#0.00")
                grdDados_FimDaEdicao (cValorPedido)
                .CellForeColor = ldbCorAzul
                .negrito = Trueta 
            End If
    end if 
```
1.  No procedimento `sAplicarRegraColoracoes` após o trexo de código abaixo:
``` vb
If .TextMatrix(iLinha, btValorVenda) <> .TextMatrix(iLinha, btValorVendaAntesAlteracao) And Trim(.TextMatrix(iLinha, btValorVendaAntesAlteracao)) <> "" Then
    If Funcoes.fLeConfiguracao(goConexao, "USA_PRECO_OFFLINE", "TRUE") = "FALSE" Then
        sMensagem = "Preço de venda alterado.    Valor Anterior: R$" & .TextMatrix(iLinha, btValorVendaAntesAlteracao)
    Else
        sMensagem = "Preço de venda alterado, aplique o valor e envie carga para os PDVs.    Valor Anterior: R$" & .TextMatrix(iLinha, btValorVendaAntesAlteracao)
    End If
End If
```
APÓS ESSE if inserir o código abaixo corrigindo os parâmetros necessários
``` vb
    dbCodPedido = 0
    fRetornarValorDoPedido (btCdNotasProdAgrupados,btCodigoProduto,dbCodPedido)

    If dbCodPedido <> 0 Then
        sMensagem = "Preço de venda definido no Pedido Nº " & dbCodPedido & "." & " " & sMensagem
    End If
```












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

* Label link "Vendedor" -------------- OK
    * Na tela do Pedido, criar label link "Vendedor" permitindo chamar tela de cadastro do vendedor para criar novo ou alterar um existente. -------------- OK
* Lupa para selecionar o vendedor (acho que não precisa) -------------- OK
    * Criar botão de procura (lupa) e popular campos na tela buscando dos novos campos nas tabelas PedidoCompras.CodVendedor e VendedoresFornecedor -------------- OK
* Criar filtro "Vendedor" no filtro de produtos com funcionamento semelhante ao do fornecedor -------------- OK
    * Na tela do pedido de compras, nos filtros de produtos do pedido, criar novo filtro "Vendedor". Deve listar os vendedores disponíveis do fornecedor, e  já trazer escolhido o vendedor selecionado no pedido como padrão. -------------- OK
* Criar checkbox para o vendedor, semelhante ao do fornecedor -------------- OK
    * Criar checkbox para ativar/desabitar o filtro, sugerindo o vendedor do pedido (semelhante ao filtro atual de "Fornecedor"). Padrão desmarcado, salvar escolha em "VendedorFornecedores.MixPorVendedor) -------------- OK



----------------------------

    * Caso vendedor selecionado e checkbox desmarcado, fazer left join no select que retorna os produtos ainda não adicionados ao pedido para indicar se o produto está no mix do Vendedor ou não, assinalando com fonte *** itálico *** nome do produto que estiver fora do mix do vendedor.
    dar update nos produtos nas notas de saída 
    dar update nos produtos nos pedidos??

    * Ao "desvincular produto do Fornecedor" deve também desvincular do mix de todos os Vendedores, caso esteja.
* Ao inserir o produto em pedido compra produto add tbm no mix do vendedor
    * Ao inserir o produto em "PedidoCompraProduto" caso um vendedor estiver selecionado e o produto fora do mix do vendedor, inserir também em VendedoresProdutos.
----------------------------


    * Caso vendedor selecionado e checkbox marcado, fazer Inner join no select que retorna os produtos para retornar na pesquisa dos produtos ainda não adicionados ao pedido apenas os produtos dentro do mix do vendedor.-------------- OK
    * Caso vendedor não selecionado não deve ser feito nenhum join de vendedores.-------------- OK
* Criar menu para desvincular produto ao vendedor - tratar tbm no menu do fornecedor-------------- OK
    * Criar menu no clique direito do produto "Desvincular produto do Vendedor"-------------- OK
    * Ao "desvincular produto do Fornecedor" deve também desvincular do mix de todos os Vendedores, caso esteja.
* Ao inserir o produto em pedido compra produto add tbm no mix do vendedor"-------------- OK
    * Ao inserir o produto em "PedidoCompraProduto" caso um vendedor estiver selecionado e o produto fora do mix do vendedor, inserir também em VendedoresProdutos.
## Alterar RPTs
- Alterar os rpts dos Pedidos de Compra para buscar os campos da tabela nova.
## Criar coluna na busca de pedidos de compra
- Criar coluna no grid da busca do pedido e  filtro por nome do vendedor na tela de busca do pedido.









colocar ajuda nas notas de entrada demonstrando o fluxo


(corrigir o bug data do pedido data da entrega)


ficou pendente saber como abre o menu trocas botão direito

validar questão do encerramento do pedido caso o usuário altere a nota, se eu não aviso nada deveria corrigir???



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


"Encerrar o pedido" 
R: Isso mesmo

"zerar o campo primeira compra"
R: não deve ser zerado, pois se estiver marcado significa que naquele pedido era a primeira compra do produto. A informação deve se manter para esse pedido, nos próximos o sistema identifica que já teve pedido e não popula mais esse campo.

"Atualizar o campo data entrega"
R: Isso mesmo

"Posso apenas desfazer o encerrado e deixar aberto de novo"
R: Isso mesmo

"acha que temos que fazer algo quanto a isso? se sim vou ter que salvar os campos"
R: Não precisamos fazer nada além de desfazer o encerrado e deixar aberto de novo.


