```sql


-- ==============================================================================
-- DECLARAÇÃO DE VARIÁVEIS PARA O PROCESSO
-- ==============================================================================
DECLARE @CodOriginal FLOAT = 8182370;   -- Código do produto que está vencendo
DECLARE @NovoPrecoRebaixa MONEY = 9.99; -- Preço promocional do novo produto
DECLARE @QuantidadeRebaixa MONEY = 11;   -- Quantidade que vai mudar de preço
DECLARE @LojaOrigem INT = 1;             -- Loja de onde sai o estoque antigo
DECLARE @LojaDestino INT = 1;            -- Loja onde entra o estoque novo (geralmente a mesma)

-- Variável em branco que vai receber o novo código gerado pelo banco
DECLARE @CodigoGeradoRebaixa FLOAT; 


-- ==============================================================================
-- PASSO 1: DUPLICAR O PRODUTO E PEGAR O CÓDIGO NOVO
-- ==============================================================================
EXEC sp_DuplicarProduto_ParaRebaixa 
    @CodOriginal = @CodOriginal, 
    @NovoValorVenda = @NovoPrecoRebaixa, 
    @CodNovoGerado = @CodigoGeradoRebaixa OUTPUT; -- Captura o retorno aqui

PRINT '---------------------------------------------------'
PRINT 'PRODUTO DUPLICADO COM SUCESSO.'
PRINT 'CÓDIGO ORIGINAL: ' + CAST(CAST(@CodOriginal AS BIGINT) AS VARCHAR)
PRINT 'NOVO CÓDIGO (REBAIXA): ' + CAST(CAST(@CodigoGeradoRebaixa AS BIGINT) AS VARCHAR)
PRINT '---------------------------------------------------'


-- ==============================================================================
-- PASSO 2: TRANSFERIR O ESTOQUE ENTRE ELES
-- ==============================================================================
-- Só executa a transferência se a duplicação gerou um código válido
IF @CodigoGeradoRebaixa IS NOT NULL AND @CodigoGeradoRebaixa > 0
BEGIN
    EXEC sp_TransferirEstoque_Rebaixa 
        @CodProduto = @CodOriginal, 
        @CodProdutoRebaixa = @CodigoGeradoRebaixa, 
        @QuantidadeTransferida = @QuantidadeRebaixa, -- Parâmetro único ajustado
        @CodLojaSaida = @LojaOrigem, 
        @CodLojaEntrada = @LojaDestino,
        @TipoAjusteSaida = 2,  -- Ajuste para o código correto de saída do seu ERP
        @TipoAjusteEntrada = 1; -- Ajuste para o código correto de entrada do seu ERP

    PRINT 'ESTOQUE TRANSFERIDO E TRIGGERS ACIONADAS.'
END
ELSE
BEGIN
    PRINT 'ERRO: O código de rebaixa não foi gerado. A transferência de estoque foi cancelada.'
END
```