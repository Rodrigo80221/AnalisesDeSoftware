```Csharp
			.TextMatrix(iLinha, btLojaNome) = grsGeral("Loja")
            .TextMatrix(iLinha, btCodigoProduto) = Space(4) & grsGeral("CD_Produto")
            .TextMatrix(iLinha, btProdutoAtivo) = grsGeral("Ativo")
            .TextMatrix(iLinha, btDescricaoProduto) = grsGeral("Descricao")
            .TextMatrix(iLinha, btQtdProduto) = format(grsGeral("QT_Produto"), "#0.00")
            .TextMatrix(iLinha, btCustoGerencialAtual) = format(cCustoAtual, "#0.00")
            .TextMatrix(iLinha, btCustoGerencialAnterior) = format(grsGeral("CustoGerencialAnterior"), "#0.00")
            .TextMatrix(iLinha, btCustoMargemAtual) = format(cCustoMargem, "#0.00")
            .TextMatrix(iLinha, btCustoMargemAnterior) = format(grsGeral("CustoMargemAnterior"), "#0.00")

            cIcms_compra = fCalcularDiferimentoNoICMS(grsGeral("ICMS_COMPRA"), grsGeral("PercentualDiferimento"), grsGeral("estadoFornec"))

            .TextMatrix(iLinha, btIcmsCompra) = cIcms_compra
            .TextMatrix(iLinha, btValorFcpCompra) = grsGeral("ValorFcpEntrada")
            .TextMatrix(iLinha, btAliquotaFcpVenda) = grsGeral("AliquotaFcpVenda")
            .TextMatrix(iLinha, btBaseValorStRetido) = grsGeral("BaseValorStRetido")
            .TextMatrix(iLinha, btReducaoBaseStEfetivo) = grsGeral("ReducaoBaseStEfetivo")
            .TextMatrix(iLinha, btAliqIcmsStRetido) = grsGeral("AliquotaIcmsStRetido")
            .TextMatrix(iLinha, btAliqFcpStRetido) = grsGeral("AliquotaFcpStRetido")
            .TextMatrix(iLinha, btDataOperacao) = grsGeral("Data")
            .TextMatrix(iLinha, btLojaDesc) = cboLoja.List(Funcoes.fProcuraItemData(cboLoja, grsGeral("CodLoja")))
            .TextMatrix(iLinha, btCustoMedio) = format(grsGeral("CustoMedio"), "#0.00")
            .TextMatrix(iLinha, btCodPrecosAlterados) = IIf(IsNull(grsGeral("CodPrecosAlterados")), "", grsGeral("CodPrecosAlterados"))

            'se o produto estiver em promocao usa o campo valor da tabela promocao (Valor fora da promoção)
            If grsGeral("promocao") <> "" Or grsGeral("promocao") <> Null Then
                .TextMatrix(iLinha, btValorOriginal) = format(grsGeral("ValorForaPromocao"), "#0.00")

                .TextMatrix(iLinha, btValorVenda) = fArredondarValor(format(grsGeral("ValorForaPromocao"), "#0.00"))
                .TextMatrix(iLinha, btPromocao) = grsGeral("promocao")
                cValorAntes = grsGeral("ValorForaPromocao")

                sColorirLinhaPromocao iLinha

                .TextMatrix(iLinha, btMarkUp) = format(fMarkUp(grsGeral("CD_Produto"), IIf(bAlteraCusto, cCustoAtual, grsGeral("CustoGerencialAnterior")), grsGeral("valorForaPromocao")), "#0.00")
                .TextMatrix(iLinha, btMargem) = format(fMargem(IIf(bAlteraCusto, cCustoAtual, grsGeral("CustoGerencialAnterior")), grsGeral("valorForaPromocao"), grsGeral("Aliquota"), grsGeral("PisEntrada"), grsGeral("CofinsEntrada"), grsGeral("PisSaida"), grsGeral("CofinsSaida"), cIcms_compra, grsGeral("ValorFcpEntrada"), grsGeral("AliquotaFcpVenda"), grsGeral("BaseValorStRetido"), grsGeral("AliquotaIcmsStRetido"), grsGeral("BaseValorStRetido"), grsGeral("AliquotaFcpStRetido"), grsGeral("ReducaoBaseStEfetivo"), bApurarSt), "#0.00")

            ElseIf grsGeral("LOJAPACK") = cboLoja.ItemData(cboLoja.ListIndex) Or (grsGeral("LOJAPACK") <> "Null" And cboLoja.ItemData(cboLoja.ListIndex) = -1) Then

                .TextMatrix(iLinha, btValorOriginal) = format(grsGeral("ValorVenda"), "#0.00")
                .TextMatrix(iLinha, btValorVenda) = fArredondarValor(format(grsGeral("ValorVenda"), "#0.00"))
                cValorAntes = grsGeral("ValorVenda")
                'Se o produto estiver em um pack deve ficar verde, por isso foi usado o mesmo campo de promoção
                .TextMatrix(iLinha, btPromocao) = grsGeral("LOJAPACK")
                .TextMatrix(iLinha, btMarkUp) = format(fMarkUp(grsGeral("CD_Produto"), IIf(bAlteraCusto, cCustoAtual, grsGeral("CustoGerencialAnterior")), grsGeral("ValorVenda")), "#0.00")
                .TextMatrix(iLinha, btMargem) = format(fMargem(IIf(bAlteraCusto, cCustoAtual, grsGeral("CustoGerencialAnterior")), grsGeral("ValorVenda"), grsGeral("Aliquota"), grsGeral("PisEntrada"), grsGeral("CofinsEntrada"), grsGeral("PisSaida"), grsGeral("CofinsSaida"), cIcms_compra, grsGeral("ValorFcpEntrada"), grsGeral("AliquotaFcpVenda"), grsGeral("BaseValorStRetido"), grsGeral("AliquotaIcmsStRetido"), grsGeral("BaseValorStRetido"), grsGeral("AliquotaFcpStRetido"), grsGeral("ReducaoBaseStEfetivo"), bApurarSt), "#0.00")

                If Not lbUsaDescontoAtacado Then
                    sColorirLinhaPromocao iLinhaProdAnterior
                Else
                    If grsGeral("ModeloPack") <> 13 Then sColorirLinhaPromocao iLinhaProdAnterior
                End If

            Else

                .TextMatrix(iLinha, btValorOriginal) = format(grsGeral("ValorVenda"), "#0.00")


                .TextMatrix(iLinha, btValorVenda) = fArredondarValor(format(grsGeral("ValorVenda"), "#0.00"))
                cValorAntes = grsGeral("ValorVenda")
                .TextMatrix(iLinha, btMarkUp) = format(fMarkUp(grsGeral("CD_Produto"), IIf(bAlteraCusto, cCustoAtual, grsGeral("CustoGerencialAnterior")), grsGeral("ValorVenda")), "#0.00")
                .TextMatrix(iLinha, btMargem) = format(fMargem(IIf(bAlteraCusto, cCustoAtual, grsGeral("CustoGerencialAnterior")), grsGeral("ValorVenda"), grsGeral("Aliquota"), grsGeral("PisEntrada"), grsGeral("CofinsEntrada"), grsGeral("PisSaida"), grsGeral("CofinsSaida"), cIcms_compra, grsGeral("ValorFcpEntrada"), grsGeral("AliquotaFcpVenda"), grsGeral("BaseValorStRetido"), grsGeral("AliquotaIcmsStRetido"), grsGeral("BaseValorStRetido"), grsGeral("AliquotaFcpStRetido"), grsGeral("ReducaoBaseStEfetivo"), bApurarSt), "#0.00")

            End If
            .TextMatrix(iLinha, btAliquota) = format(grsGeral("Aliquota"), "#0.00")
            .TextMatrix(iLinha, btPisSaida) = format(grsGeral("PisSaida"), "#0.00")
            .TextMatrix(iLinha, btCofinsSaida) = format(grsGeral("CofinsSaida"), "#0.00")

            .TextMatrix(iLinha, btCofinsEntrada) = format(grsGeral("CofinsEntrada"), "#0.00")
            .TextMatrix(iLinha, btPisEntrada) = format(grsGeral("PisEntrada"), "#0.00")


            ''se o produto estiver em promocao usa o campo valor da tabela promocao (Valor fora da promoção)
            If grsGeral("promocao") <> "" Or grsGeral("promocao") <> Null Then
                .TextMatrix(iLinha, btValorVendaAntesAlteracao) = format(grsGeral("valorForaPromocao"), "#0.00")
            Else
                .TextMatrix(iLinha, btValorVendaAntesAlteracao) = format(grsGeral("ValorVenda"), "#0.00")
            End If
            .TextMatrix(iLinha, btCfop) = grsGeral("cfop")
            .TextMatrix(iLinha, btCodigoFornecedor) = grsGeral("CD_Fornecedor")

            If Not bAlteraCusto Then
                .TextMatrix(iLinha, btCustoExibido) = "*" & format(cCustoAtual, "#0.00")
                .Col = btCustoExibido
                .CellForeColor = ldbColoracaoAlteracaoDePreco

                lblAlteraCusto.Visible = True
            Else
                .TextMatrix(iLinha, btCustoExibido) = format(cCustoAtual, "#0.00")
            End If

            .TextMatrix(iLinha, btCustoMargemAtual) = format(cCustoMargem, "#0.00#")
            .TextMatrix(iLinha, btMarkUpIdeal) = format(grsGeral("MarkupIdeal"), "#0.00")
            .TextMatrix(iLinha, btMargemIdeal) = format(grsGeral("MargemIdeal"), "#0.00")

            .TextMatrix(iLinha, btCodFornecedorTabela) = IIf(IsNull(grsGeral("CodFornecedorTabela")), "", grsGeral("CodFornecedorTabela"))
            .TextMatrix(iLinha, btCodBarras) = grsGeral("barras")
            .TextMatrix(iLinha, btCodNota) = grsGeral("CD_NOTA")
            .TextMatrix(iLinha, btCdNotasProdAgrupados) = grsGeral("CD_NOTA")
            .TextMatrix(iLinha, btSequenciaNota) = grsGeral("SEQUENCIA")
            .TextMatrix(iLinha, btNumeroNota) = grsGeral("NUMERO")
            .TextMatrix(iLinha, btCodLoja) = grsGeral("CodLoja")

            .Col = btPicStatusPrecosAlterados

            cCustoAnt = format(grsGeral("CustoGerencialAnterior"), "#0.00")
            cCustoAtu = format(cCustoAtual, "#0.00")

            If cCustoAtu > cCustoAnt Then
                Set .goGrid.CellPicture = imgListPrecosAlterados.ListImages(eAumentou).Picture
            ElseIf cCustoAtu < cCustoAnt And bAlteraCusto Then
                Set .goGrid.CellPicture = imgListPrecosAlterados.ListImages(eDiminuiu).Picture
            Else
                Set .goGrid.CellPicture = imgListPrecosAlterados.ListImages(eIgual).Picture
            End If

            .Col = btMarkUp
            .CellBackColor = ldbColoracaoFornecedorMargensEVlrVenda_Cinza
            .Col = btMargem
            .CellBackColor = ldbColoracaoFornecedorMargensEVlrVenda_Cinza
            .Col = btValorVenda
            .CellBackColor = ldbColoracaoFornecedorMargensEVlrVenda_Cinza

            If Not IsNull(grsGeral("CodPrecosAlterados")) Then
                .Col = btValorVenda
                .CellForeColor = ldbColoracaoAlteracaoDePreco
            End If

            'CONFIG PARA VIZUALIZAR OU NÃO OS BONIFICADOS.
            If chkVisualizarBonificacao.Value = vbUnchecked And Right(grsGeral("CFOP"), 3) = 910 Then
                .RowHeight(CLng(iLinha)) = 0
                bInserirDescontoAtacado = False
            Else
                iOcultarLinhaFornecedor = 0
            End If

            dbUltimoFornecedor = grsGeral("CD_Fornecedor")

            If .Rows Mod 50 = 0 Then Me.Refresh

            'se estiver em promoção mantém o destaque da promoção
            If .TextMatrix(iLinha, btPromocao) = "" Then
                If fVerificarDiferencaMarkup(iLinha) Then sColorirDiferenca iLinha
                If fVerificarDiferencaMargem(iLinha) Then sColorirDiferenca iLinha
            End If

            If .RowHeight(CLng(iLinha)) <> 0 Then
                liNumeroProdutos = liNumeroProdutos + 1
                liSomaQtdProdutos = liSomaQtdProdutos + .TextMatrix(iLinha, btQtdProduto)
                lcSomaMarkup = lcSomaMarkup + CCur(.TextMatrix(iLinha, btQtdProduto) * .TextMatrix(iLinha, btMarkUp))
                lcSomaMargem = lcSomaMargem + CCur(.TextMatrix(iLinha, btQtdProduto) * .TextMatrix(iLinha, btMargem))
            End If

'            If grsGeral("CD_PRODUTO") = 56930 Then
'                DoEvents
'            End If

            ''Adicionar a linha do produto de atacado
            If lbUsaDescontoAtacado And bInserirDescontoAtacado And grsGeral("ModeloPack") = 13 Then
                .Rows = .Rows + 1

                .TextMatrix(.Rows - 1, btMarkUp) = format(fMarkUp(grsGeral("CD_Produto"), IIf(bAlteraCusto, cCustoAtual, grsGeral("CustoGerencialAnterior")), grsGeral("PrecoAtacado")), "#0.00")
                .TextMatrix(.Rows - 1, btMargem) = format(fMargem(IIf(bAlteraCusto, cCustoAtual, grsGeral("CustoGerencialAnterior")), grsGeral("PrecoAtacado"), grsGeral("Aliquota"), grsGeral("PisEntrada"), grsGeral("CofinsEntrada"), grsGeral("PisSaida"), grsGeral("CofinsSaida"), cIcms_compra, grsGeral("ValorFcpEntrada"), grsGeral("AliquotaFcpVenda"), grsGeral("BaseValorStRetido"), grsGeral("AliquotaIcmsStRetido"), grsGeral("BaseValorStRetido"), grsGeral("AliquotaFcpStRetido"), grsGeral("ReducaoBaseStEfetivo"), bApurarSt), "#0.00")
                .TextMatrix(.Rows - 1, btValorVenda) = fArredondarValor(format(grsGeral("PrecoAtacado"), "#0.00"))
                .TextMatrix(.Rows - 1, btDescontoAtacado) = grsGeral("DescricaoPack")

                .TextMatrix(.Rows - 1, btAjusteUltimaCasaDecimalAtacado) = grsGeral("AjusteUltimaCasaDecimal")
                .TextMatrix(.Rows - 1, btValorRegraAtacado) = grsGeral("VlrRegra")
                .TextMatrix(.Rows - 1, btTipoAjusteValorAtacado) = grsGeral("TipoAjusteValor")
                .TextMatrix(.Rows - 1, btCodPackAtacado) = grsGeral("CodPack")

                sColorirLinhaAtacado .Rows - 1
                sJuntarComMergeLinhaAtacado .Rows - 1

                .row = .Rows - 2
            End If

            If Not oUsuarioAcesso.PermiteAlterar(609, CInt(grsGeral("CodLoja"))) Then bBloquearAlterar = True
```