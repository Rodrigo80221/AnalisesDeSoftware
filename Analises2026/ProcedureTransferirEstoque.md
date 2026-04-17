```sql
CREATE PROCEDURE [dbo].[sp_TransferirEstoque_Rebaixa]
    @CodProduto FLOAT,
    @CodProdutoRebaixa FLOAT,
    @QuantidadeTransferida MONEY, -- Parâmetro unificado
    @CodLojaSaida INT,
    @CodLojaEntrada INT,
    @TipoAjusteSaida INT = 2,   -- Padrão 2 
    @TipoAjusteEntrada INT = 1  -- Padrão 1 
AS
BEGIN
    SET NOCOUNT ON;

    BEGIN TRY
        BEGIN TRANSACTION;

        -- ==============================================================================
        -- 1. BUSCA DOS CUSTOS GERENCIAIS
        -- ==============================================================================
        DECLARE @CustoSaida MONEY;
        DECLARE @CustoEntrada MONEY;

        SELECT @CustoSaida = ISNULL(CUSTOGERENCIAL, 0) 
        FROM VW_ProdutoCustoLoja 
        WHERE CODPRODUTO = @CodProduto AND CODLOJA = @CodLojaSaida;

        SELECT @CustoEntrada = ISNULL(CUSTOGERENCIAL, 0) 
        FROM VW_ProdutoCustoLoja 
        WHERE CODPRODUTO = @CodProdutoRebaixa AND CODLOJA = @CodLojaEntrada;

        -- ==============================================================================
        -- 2. CRIAÇÃO DA OBSERVAÇÃO (Justificativa do Ajuste)
        -- ==============================================================================
        DECLARE @CodObs FLOAT;
        SELECT @CodObs = ISNULL(MAX(CODIGO), 0) + 1 FROM OBS;

        -- Convertendo FLOAT para BIGINT antes do VARCHAR
        INSERT INTO OBS (CODIGO, DESCRICAO) 
        VALUES (
            @CodObs, 
            'Transferência sistêmica para rebaixa de preço. Origem: ' + CAST(CAST(@CodProduto AS BIGINT) AS VARCHAR) + 
            ' Destino: ' + CAST(CAST(@CodProdutoRebaixa AS BIGINT) AS VARCHAR)
        );

        -- ==============================================================================
        -- 3. CABEÇALHO E ITEM: SAÍDA (Retirando do produto original)
        -- ==============================================================================
        DECLARE @CodAjusteSaida FLOAT;
        SELECT @CodAjusteSaida = ISNULL(MAX(CODIGO), 0) + 1 FROM AJUSTES;

        INSERT INTO AJUSTES (
            CODIGO, DT_AJUSTE, CD_OBS, CONFIG, CD_Tipo, ORIGEM, 
            CodTipoEstoque, CodLoja, OrigemCaixaria, GeradoPeloSistema
        )
        VALUES (
            @CodAjusteSaida, GETDATE(), @CodObs, NULL, @TipoAjusteSaida, 0, 
            1, @CodLojaSaida, 0, 0
        );

        INSERT INTO AJUSTE_PRODUTOS (
            CD_AJUSTE, CD_PRODUTO, QUANTIDADE, QT_ESTOQUE, CUSTO, Custo_Gerencial, sequencia
        )
        VALUES (
            @CodAjusteSaida, @CodProduto, 0, (@QuantidadeTransferida * -1), @CustoSaida, @CustoSaida, NULL
        );

        -- ==============================================================================
        -- 4. CABEÇALHO E ITEM: ENTRADA (Injetando no produto rebaixado)
        -- ==============================================================================
        DECLARE @CodAjusteEntrada FLOAT;
        SELECT @CodAjusteEntrada = @CodAjusteSaida + 1; 

        INSERT INTO AJUSTES (
            CODIGO, DT_AJUSTE, CD_OBS, CONFIG, CD_Tipo, ORIGEM, 
            CodTipoEstoque, CodLoja, OrigemCaixaria, GeradoPeloSistema
        )
        VALUES (
            @CodAjusteEntrada, GETDATE(), @CodObs, NULL, @TipoAjusteEntrada, 0, 
            1, @CodLojaEntrada, 0, 0
        );

        INSERT INTO AJUSTE_PRODUTOS (
            CD_AJUSTE, CD_PRODUTO, QUANTIDADE, QT_ESTOQUE, CUSTO, Custo_Gerencial, sequencia
        )
        VALUES (
            @CodAjusteEntrada, @CodProdutoRebaixa, 0, @QuantidadeTransferida, @CustoEntrada, @CustoEntrada, NULL
        );

        COMMIT TRANSACTION;
        PRINT 'Transferência gerada! Saída: ' + CAST(CAST(@CodAjusteSaida AS BIGINT) AS VARCHAR) + ' / Entrada: ' + CAST(CAST(@CodAjusteEntrada AS BIGINT) AS VARCHAR);

    END TRY
    BEGIN CATCH
        ROLLBACK TRANSACTION;
        PRINT 'Falha ao processar a transferência de estoque.';
        THROW; 
    END CATCH
END
GO
```