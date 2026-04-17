```sql
CREATE PROCEDURE [dbo].[sp_DuplicarProduto_ParaRebaixa]
    @CodOriginal FLOAT,
    @NovoValorVenda MONEY,
    @CodNovoGerado FLOAT OUTPUT
AS
BEGIN
    SET NOCOUNT ON;

    BEGIN TRY
        BEGIN TRANSACTION;

        DECLARE @CodNovo FLOAT;

        -- Define o novo código
        SELECT @CodNovo = ISNULL(MAX(Codigo), 0) + 1 FROM Produtos;
        
        -- Alimenta a variável de saída para a aplicação
        SET @CodNovoGerado = @CodNovo;

        -- ==============================================================================
        -- 1. TABELA PRINCIPAL: Produtos (Alterando Descrição e Valores)
        -- ==============================================================================
        INSERT INTO Produtos (
            Codigo, Descricao, DescricaoReduzida, Grupo, Unidade, ValorVenda, MargemLucro, Custo, Qtd, QtdMinima, 
            Aliquota, Config, Validade, IPI, DT_ULTIMA_VENDA, QT_EMBALAGEM, CD_ASSOCIADO, ICMS_COMPRA, DT_CADASTRO, 
            CD_APLICACAO, VALOR_NO_PDV, ST, BASE_REDUZIDA_ICMS, Custo_Gerencial, CnpjCliente, CFOP, LOCALIZACAO, 
            CD_SITUACAO_PRODUTO, DT_ULT_ALTERACAO, PesoBruto, CodAliquotaPisCofins, NCM, MVA, Markup, 
            VINCULARPRECOVENDACOMITENSFORMULA, FatorCustoDePaiParaFilho, VincularCustoGerencialTotalItens, 
            CodSistemaAntigo, Qtd_Embalagem_Venda, csosn, CODVASILHAME, CodSecao, CodFornecedorTabela, 
            CodUltimaClassificacao, ExcluirNaManutencao, NaturezaReceita, AliquotaIcmsNFSaidas, 
            CodigoUltimoRecalcularEstoque, IgnoraBCClient, BloqueiaMultiplicacao, EX_Tipi, ProdutoCaixa, 
            UsaRefUndBasica, QtdRefUndBasica, CodRefUndBasica, CodigoCest, PercentualDiferimento, 
            CodTipoItem0200SPED, CodTipoFormula, CNPJFabricante, NaoUsaDepartamentoBalanca, EscalaNaoRelevante, 
            FundoCombateaPobreza, AliquotaICMSST, AliquotaFCPST, BaseReduzidaST, PropagaBaseSTCheiaFormulas, 
            PermitirVenderProdEstNegativoTerceiro, ControlaValidade, ValidadeMinRecebimento, DiasValidade, 
            AproveitaStBlocoE, SincronizadoCadastro, QuantidadeMaximaVendidaPorItem, 
            CodMotivoDescontoVenderItensSeparadamente, PercTotalPrecoVendaItensFormula, FormulaUsaPromo, 
            IgnoraRelatorioCompraVenda, InfAdicionaisProduto, DifereCemPorcNaoCons, PreEmbalado, 
            PesagemEstoqueSeguranca, PesagemMonitoraEstoque, IgnoraConferenciaDePeso, Tara, 
            FormulaUsaArredondamento, Notabilidade
        )
        SELECT 
            @CodNovo, 
            LEFT(Descricao + ' (REBAIXA)', 50), 
            DescricaoReduzida, Grupo, Unidade, 
            @NovoValorVenda, 
            MargemLucro, Custo, Qtd, QtdMinima, 
            Aliquota, Config, Validade, IPI, NULL, QT_EMBALAGEM, CD_ASSOCIADO, ICMS_COMPRA, GETDATE(), 
            CD_APLICACAO, 
            @NovoValorVenda, 
            ST, BASE_REDUZIDA_ICMS, Custo_Gerencial, CnpjCliente, CFOP, LOCALIZACAO, 
            CD_SITUACAO_PRODUTO, GETDATE(), PesoBruto, CodAliquotaPisCofins, NCM, MVA, Markup, 
            VINCULARPRECOVENDACOMITENSFORMULA, FatorCustoDePaiParaFilho, VincularCustoGerencialTotalItens, 
            CodSistemaAntigo, Qtd_Embalagem_Venda, csosn, CODVASILHAME, CodSecao, CodFornecedorTabela, 
            CodUltimaClassificacao, ExcluirNaManutencao, NaturezaReceita, AliquotaIcmsNFSaidas, 
            CodigoUltimoRecalcularEstoque, IgnoraBCClient, BloqueiaMultiplicacao, EX_Tipi, ProdutoCaixa, 
            UsaRefUndBasica, QtdRefUndBasica, CodRefUndBasica, CodigoCest, PercentualDiferimento, 
            CodTipoItem0200SPED, CodTipoFormula, CNPJFabricante, NaoUsaDepartamentoBalanca, EscalaNaoRelevante, 
            FundoCombateaPobreza, AliquotaICMSST, AliquotaFCPST, BaseReduzidaST, PropagaBaseSTCheiaFormulas, 
            PermitirVenderProdEstNegativoTerceiro, ControlaValidade, ValidadeMinRecebimento, DiasValidade, 
            AproveitaStBlocoE, SincronizadoCadastro, QuantidadeMaximaVendidaPorItem, 
            CodMotivoDescontoVenderItensSeparadamente, PercTotalPrecoVendaItensFormula, FormulaUsaPromo, 
            IgnoraRelatorioCompraVenda, InfAdicionaisProduto, DifereCemPorcNaoCons, PreEmbalado, 
            PesagemEstoqueSeguranca, PesagemMonitoraEstoque, IgnoraConferenciaDePeso, Tara, 
            FormulaUsaArredondamento, Notabilidade
        FROM Produtos 
        WHERE Codigo = @CodOriginal;

        -- ==============================================================================
        -- 2. TABELA DE LOJAS: ProdutoLojas
        -- ==============================================================================
        INSERT INTO ProdutoLojas (
            codLoja, codProduto, valorProduto, ValorNoPdv, DataUltimaVenda, 
            QtdUltimaCompra, DataUltimaCompra, Giro4, ValorUltimaBaseStRetido, 
            ValorUltimoICMSSubstituto, QtdMinimaEstoque, Ativo
        )
        SELECT 
            codLoja, 
            @CodNovo, 
            @NovoValorVenda, 
            @NovoValorVenda, 
            NULL, 0, NULL, 0, 0, 0, 
            QtdMinimaEstoque, Ativo
        FROM ProdutoLojas 
        WHERE codProduto = @CodOriginal;

        -- ==============================================================================
        -- 3. TABELA DE BARRAS: PRODUTO_BARRAS
        -- ==============================================================================
        INSERT INTO PRODUTO_BARRAS (CD_PRODUTO, BARRAS, COMPLEMENTO)
        SELECT 
            @CodNovo, 
            LEFT('R' + BARRAS, 15), 
            COMPLEMENTO
        FROM PRODUTO_BARRAS 
        WHERE CD_PRODUTO = @CodOriginal;

        -- ==============================================================================
        -- 4. TABELA DE SELF CHECKOUT: ProdutosSelfCheckout
        -- ==============================================================================
        INSERT INTO ProdutosSelfCheckout (
            CodProduto, ToleranciaPeso, VendaControladaNoSelf, PosicaoUltimoRegistroPeso, 
            Peso1, Peso2, Peso3, Peso4, Peso5, DataHoraCalibragem
        )
        SELECT 
            @CodNovo, ToleranciaPeso, VendaControladaNoSelf, PosicaoUltimoRegistroPeso, 
            Peso1, Peso2, Peso3, Peso4, Peso5, DataHoraCalibragem
        FROM ProdutosSelfCheckout 
        WHERE CodProduto = @CodOriginal;

        -- ==============================================================================
        -- 5. TABELA DE LISTA DE PRODUTOS: LISTA_PRODUTOS
        -- ==============================================================================
        INSERT INTO LISTA_PRODUTOS (CD_LISTA, CD_PRODUTO, COR)
        SELECT CD_LISTA, @CodNovo, COR
        FROM LISTA_PRODUTOS 
        WHERE CD_PRODUTO = @CodOriginal;

        -- ==============================================================================
        -- 6. HISTÓRICO DE CUSTOS (INSERÇÃO EM BLOCO)
        -- ==============================================================================
        INSERT INTO CustoHistorico (
            CodLoja, CodProduto, CodProdutoPai, Origem, DtCusto, CustoMedio, 
            CustoGerencial, CustoMargem, DtInsercao, CodOperador, CodNota, 
            SequenciaNFP, AlteraCusto, ValorBaseStRetido, AliquotaIcmsStRetido, 
            AliquotaFcpStRetido, ReducaoBaseStRetido
        )
        SELECT 
            CODLOJA, 
            @CodNovo, 
            NULL, 
            1, 
            GETDATE(), 
            ISNULL(CUSTOMEDIO, 0),     -- Ajustado para usar o Custo Médio real da view
            ISNULL(CUSTOGERENCIAL, 0), 
            ISNULL(CUSTOGERENCIAL, 0), 
            GETDATE(), 
            1, 
            NULL, 
            NULL, 
            1, 
            0, 0, 0, 0
        FROM VW_ProdutoCustoLoja
        WHERE CODPRODUTO = @CodOriginal;

        -- ==============================================================================
        -- 7. ATUALIZAÇÃO DE CUSTOS NA TABELA PRINCIPAL (Produtos)
        -- ==============================================================================
        DECLARE @CustoMedio MONEY = 0;
        DECLARE @CustoGerencial MONEY = 0;

        -- Coleta o custo consolidado a partir do produto original
        SELECT TOP 1 
            @CustoGerencial = ISNULL(CUSTOGERENCIAL, 0), 
            @CustoMedio = ISNULL(CUSTOMEDIO, 0)
        FROM VW_ProdutoCustoLoja 
        WHERE CODPRODUTO = @CodOriginal;

        -- Aplica o update diretamente no produto novo recém-criado
        UPDATE Produtos
        SET 
            Custo = @CustoMedio,
            Custo_Gerencial = @CustoGerencial
        WHERE Codigo = @CodNovo;

        -- ==============================================================================
        -- 8. INTEGRAÇÃO PDV
        -- ==============================================================================
        DECLARE @CodLoja INT;
        DECLARE @ComandoPDV NVARCHAR(MAX);
        DECLARE @ValorStr VARCHAR(20);

        SET @ValorStr = REPLACE(CAST(@NovoValorVenda AS NUMERIC(18,2)), ',', '.');

        DECLARE curLojas CURSOR LOCAL FORWARD_ONLY FOR
        SELECT codigo FROM lojas;

        OPEN curLojas;
        FETCH NEXT FROM curLojas INTO @CodLoja;

        WHILE @@FETCH_STATUS = 0
        BEGIN
            SET @ComandoPDV = 'insert into produtoLojas(codLoja, codProduto, valorProduto) values(' + 
                              CAST(@CodLoja AS VARCHAR) + ', ' + 
                              CAST(@CodNovo AS VARCHAR) + ', ' + 
                              @ValorStr + ')';
            
            EXEC SP_GRAVA_COMANDOS_PDV @ComandoPDV;
            
            FETCH NEXT FROM curLojas INTO @CodLoja;
        END

        CLOSE curLojas;
        DEALLOCATE curLojas;

        COMMIT TRANSACTION;

        SELECT @CodNovo AS CodigoNovoProdutoRebaixa;

    END TRY
    BEGIN CATCH
        ROLLBACK TRANSACTION;
        THROW; 
    END CATCH
END
```