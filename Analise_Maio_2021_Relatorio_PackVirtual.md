## Parte 2 Tarefa 1: Alterar Genericos DataGridViewPersonalizado
1. Adicionar Classes (no github salvar link como...)
    * Adicionar as classe abaixo em Genericos>Classes>Controles>Classes
    [DataGridViewCellExHelper.cs](https://github.com/Rodrigo80221/AnalisesDeSoftware/blob/main/Classes/DataGridViewCellExHelper.cs)
    [DataGridViewHelper.cs](https://github.com/Rodrigo80221/AnalisesDeSoftware/blob/main/Classes/DataGridViewHelper.cs)
    [DataGridViewTextBoxCellEx.cs](https://github.com/Rodrigo80221/AnalisesDeSoftware/blob/main/Classes/DataGridViewTextBoxCellEx.cs)
    [ISpannedCell.cs](https://github.com/Rodrigo80221/AnalisesDeSoftware/blob/main/Classes/ISpannedCell.cs)
1. Alterar método ExecutarFormatacaoPadrao da classe DataGridViewersonalizado.cs
    * Alterar o método ExecutarFormatacaoPadrao colocando o código abaixo

    ``` Csharp
        public void ExecutarFormatacaoPadrao(int nroColunas)
        {
            base.AllowUserToResizeRows = false;
            base.AllowUserToAddRows = false;
            base.AllowUserToDeleteRows = false;
            base.Columns.Clear();

            for (int aux = 0; aux < nroColunas; aux++)
            {
                var col = new DataGridViewTextBoxColumnEx();
                this.Columns.Add(col);
            }

        }
    ```
1. Criar os 2 procedimentos abaixo na classe classe DataGridViewersonalizado.cs

    ``` Csharp
        /// <summary>
        /// Realiza um merge de uma célula com a coluna seguinte
        /// </summary>
        /// <param name="linha">Linha que receberá o valor</param>
        /// <param name="coluna">Coluna que receberá o valor</param>
        /// <param name="qtdColunas">Quantidades de colunas em que será feito merge</param>
        public void JuntarColunas(int linha, int coluna, int qtdColunas)
        {

            var cell = (DataGridViewTextBoxCellEx)this.Rows[linha].Cells[coluna];
            cell.ColumnSpan = qtdColunas;

        }
    ```

    ``` Csharp
        /// <summary>
        /// Realiza um merge de uma célula com a linha de baixo, requer a linha de baixo já adicionada.
        /// </summary>
        /// <param name="linha">Linha que receberá o valor</param>
        /// <param name="coluna">Coluna que receberá o valor</param>
        /// <param name="qtdColunas">Quantidades de linhas em que será feito merge</param>
        public void JuntarLinhas(int linha, int coluna, int qtdLinhas)
        {

            var cell = (DataGridViewTextBoxCellEx)this.Rows[linha].Cells[coluna];
            cell.RowSpan = qtdLinhas;

        }
    ```    

##  Parte 2 Tarefa 2: Criar formulario FrmRelatorioPack
>Criar formulário conforme o template abaixo

![](https://github.com/Rodrigo80221/AnalisesDeSoftware/blob/main/Imagens/FrmRelatorioPack4.png?raw=true)

1. Adicionar protótipo ao projeto (no github salvar link como...)
> Adicionar em GestaoComercial>Formularios>PackVirtual
    [FrmRelatorioPack](https://github.com/Rodrigo80221/AnalisesDeSoftware/tree/main/Classes/FormRelatorioPack)
1. No novo form implementar as configurações padrões da Telecon
    * Fechar com esc
    * Enter como tab
    * Ícone na janela
    * O Sair fecha e cancela a operação
    * Não deve permitir resize na tela
    * Deve abrir centralizado. 
    * Deve estar correto quanto ao tab index ao finalizar a tela
    * Não deve ter os botões de maximizar    
1. Colocar nome nos objetos da tela padronizando-os

##  Parte 2 Tarefa 3: Padronizar objeto de data
1. No filtro de data carregar padrão com a visualização mensal no mês atual.

##  Parte 2 Tarefa 4: Carregar combo de modelos com checkbox 
1. Carregar o grid de modelos de promoção utilizando `ModelosPack.RetornarListaComboParaPackVirtual` e `ModelosPack.RetornarListaComboParaDescontoParaAtacado`
    * O combo deve ter o checkbox para escolher várias opções. Padrão todos marcados

##  Parte 2 Tarefa 5: Carregar combo de lojas com checkbox    
1. Carregar o grid de lojas
    * O combo deve ter o checkbox para escolher várias opções. Padrão todos marcados

##  Parte 2 Tarefa 6: Implementar os compos de busca por pack     
1. Implementar a busca por pack virtual, criar um formulário de pesquisa herdando o FrmPesquisa, listando os campos Código, Descricão, DtInicial, DtFinal, Encarte
    * Teclando F4 no campo código deve abrir a tela de busca

##  Parte 2 Tarefa 7: Implementar os campos de busca por encarte    
1. Implementar a busca de encartes. Utilizar o mesmo padrão que no form FrmPackVirtual
    * Teclando F4 no campo código deve abrir a tela de busca

##  Parte 2 Tarefa 8: Padronizar grade de consulta    
1. Finalizar o grid de consulta, adicionar enum para as colunas, realizar mais algum ajuste necessário.
    * A primeira coluna -/+ deixar centralizada, na imagem não está.

##  Parte 2 Tarefa 9: Implementar contador de linhas e sair    
1. Implementar contador de linhas
1. Implementar o sair para fechar o form

##  Parte 2 Tarefa 10: Implementar Consulta do FrmRelatorioPack
> A tarefa possui uma consulta base, nesta consulta por questões de performance retorna junto os dados dos produtuos e os dados dos packs. O registro do pack irá retornar com as colunas CodProduto/Unidade/BARRAS como null, esse será o registro pai. Os filhos possuem codigo do produto e estão no mesmo codpack.

1. Implementar o botão de consulta realizando uma programação semelhante ao relatório FrmAnalVendaConjunta. Criar uma classe para a consulta, executar uma Thread, colocar um gif de carregando, alterar o ponteiro do mouse, etc.
1. Utilizar como base para a consulta no sql abaixo. O sql foi uma adaptação do relatório Analise de venda conjunta.
[SelectPackVirtualRelatorio2](https://github.com/Rodrigo80221/AnalisesDeSoftware/blob/main/SQL/SelectPackVirtualRelatorio2.sql)
1. Implementar o combo de lojas com checkbox, implementar o insert na tabela @lojas da consulta sql
1. Implementar o combo de modelos com checkbox, implementar o insert na tabela @ModeloPack da consulta sql. Implementar junto os check boxes da tela (Pack Virtual e Desconto Para Atacado). Lembrando que esses modelos deverão ser buscados na classe ModelosPack
1. Implementar a busca por encarte ou codigo do pack. No fim da consulta sql tem o local do filtro comentado.
1. Ao selecionar um encarte as datas inicio e final deverão ser alteradas para a mesma data do encarte 
1. Implementar a busca por datas, caso desmarcada a data inicial ou final colocar datas distantes para a data desmarcada 
1. Verificar a formatação das colunas para cada tipo de dados, inclusive a qtd vendida conforme o tipo de dados do produto listado, inteiro ou fracionário. 
1. Ao clicar no - ou + deverá expandir ou retrair a linha
1. Ao listar o relatório mostrar inicialmente todos os registros recolhidos. Criar uma função para que no momento em que o usuário expandir uma linha leia o recordset e carregue os filhos (ler o recordset, não deverá ir no banco novamente)
1. No título da primeira coluna colocar o sinal de + ou - também, se clicar nela recolher todos os pais ou expandir todos

##  Parte 2 Tarefa 11: Implementar o duplo clique no grid
1. Ao clicar 2 vezes no produto ou no pack virtual deverá abrir a tela do pack virtual no registro correspondente


##  Parte 2 Tarefa 12: Implementar a impressão do FrmRelatorioPack
1. Implementar a opção da impressão no mesmo formato do FrmAnalVendaConjunta

##  Parte 2 Tarefa 13: Testes de integração
1. Verificar alguns packs no banco de dados bem como suas vendas e verificar o relatório.
1. Criar diferentes packs com produtos de tributação diferente, realizar algumas vendas e bater com o relatório abc de mercadorias.
1. Comparar os valores também com o cabeçalho do relatório Analise Mensal de Vendas.
1. Fazer um teste com 1 produto com diferimento conforme a última atualização feita. Fazer um pack, fazer uma nota de entrada, realizar uma venda para o produto, verificar o campo margem no relatório abc de mercadorias, e no relatório atual. 








