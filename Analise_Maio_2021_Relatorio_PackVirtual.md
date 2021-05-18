


## Alterar Genericos DataGridViewPersonalizado
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

## Criar formulario FrmRelatorioPack
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
1. Verificar nome dos campos, acertar    
1. No filtro de data carregar padrão com a visualização mensal no mês atual.
1. Carregar o grid de modelos de promoção utilizando `ModelosPack.RetornarListaComboParaPackVirtual` e `ModelosPack.RetornarListaComboParaDescontoParaAtacado`
    * O combo deve ter o checkbox para escolher várias opções.
1. Carregar o grid de lojas
    * O combo deve ter o checkbox para escolher várias opções.
1. Implementar a buscar por pack virtual, criar um formulário de pesquisa herdando o FrmPesquisa, listando os campos Código, Descricão, DtInicial, DtFinal, Encarte
    * Teclando F4 no campo código deve abrir a tela de busca
1. Implementar a busca de encartes. Utilizar o mesmo padrão que no form FrmPackVirtual
    * Teclando F4 no campo código deve abrir a tela de busca
1. Finalizar o grid de consulta, adicionar enum para as colunas, realizar mais algum ajuste necessário.
1. Implementar contador de linhas
1. Implementar o sair para fechar o form

## Implementar Consulta do FrmRelatorioPack

1. Realizar select e carregar os dados de acordo com os filtros. Utilizar a classe ModelosPack ()


1. Ao  carregar o grid mostrar um gif de carregamento igual a tela FrmAnalVendaConjunta
1. Executar a consulta em uma thread igual a tela FrmAnalVendaConjunta

Ao selecionar um encarte desabilitar a consulta por datas??
## Implementar a impressão do FrmRelatorioPack




melhorar checkboxes

após a mensagem já adicionar o produto no inserir produtos em massa

retirar mensagem dos associados, pensar em outra estratégia 

