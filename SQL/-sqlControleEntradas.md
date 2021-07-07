```Csharp
.TextMatrix(iLinhaProdAnterior, btCodigoProduto) = Space(4) & grsGeral("CD_Produto")
                        .TextMatrix(iLinha, btProdutoAtivo) = grsGeral("Ativo")
                        .TextMatrix(iLinhaProdAnterior, btDescricaoProduto) = grsGeral("Descricao")
                        .TextMatrix(iLinhaProdAnterior, btProdutoAtivo) = grsGeral("Ativo")
                        .TextMatrix(iLinhaProdAnterior, btQtdProduto) = format(grsGeral("QT_Produto"), "#0.00")
                        .TextMatrix(iLinhaProdAnterior, btCustoGerencialAtual) = format(cCustoAtual, "#0.00")
                        .TextMatrix(iLinhaProdAnterior, btCustoGerencialAnterior) = format(grsGeral("CustoGerencialAnterior"), "#0.00")
                        .TextMatrix(iLinhaProdAnterior, btCustoMargemAtual) = format(cCustoMargem, "#0.00")
                        .TextMatrix(iLinhaProdAnterior, btCustoMargemAnterior) = format(grsGeral("CustoMargemAnterior"), "#0.00")

                        cIcms_compra = fCalcularDiferimentoNoICMS(grsGeral("ICMS_COMPRA"), grsGeral("PercentualDiferimento"), grsGeral("estadofornec"))
                        .TextMatrix(iLinhaProdAnterior, btIcmsCompra) = cIcms_compra

                        .TextMatrix(iLinhaProdAnterior, btValorFcpCompra) = grsGeral("ValorFcpEntrada")
                        .TextMatrix(iLinhaProdAnterior, btAliquotaFcpVenda) = grsGeral("AliquotaFcpVenda")
                        .TextMatrix(iLinhaProdAnterior, btBaseValorStRetido) = grsGeral("BaseValorStRetido")
                        .TextMatrix(iLinhaProdAnterior, btReducaoBaseStEfetivo) = grsGeral("ReducaoBaseStEfetivo")
                        .TextMatrix(iLinhaProdAnterior, btAliqIcmsStRetido) = grsGeral("AliquotaIcmsStRetido")
                        .TextMatrix(iLinhaProdAnterior, btAliqFcpStRetido) = grsGeral("AliquotaFcpStRetido")
                        .TextMatrix(iLinhaProdAnterior, btDataOperacao) = grsGeral("Data")
                        .TextMatrix(iLinha, btLojaDesc) = cboLoja.List(Funcoes.fProcuraItemData(cboLoja, grsGeral("CodLoja")))
                        .TextMatrix(iLinhaProdAnterior, btCustoMedio) = format(grsGeral("CustoMedio"), "#0.00")
                        .TextMatrix(iLinha, btCodPrecosAlterados) = IIf(IsNull(grsGeral("CodPrecosAlterados")), "", grsGeral("CodPrecosAlterados"))

                        'se o produto estiver em promocao usa o campo valor da tabela promocao (Valor fora da promoção)
                        If grsGeral("promocao") <> "" Or grsGeral("promocao") <> Null Then
                            .TextMatrix(iLinhaProdAnterior, btValorOriginal) = format(grsGeral("ValorForaPromocao"), "#0.00")

                            .TextMatrix(iLinhaProdAnterior, btValorVenda) = fArredondarValor(format(grsGeral("ValorForaPromocao"), "#0.00"))
                            .TextMatrix(iLinhaProdAnterior, btPromocao) = grsGeral("promocao")
                            cValorAntes = grsGeral("ValorForaPromocao")

                            sColorirLinhaPromocao iLinhaProdAnterior

                            .TextMatrix(iLinhaProdAnterior, btMarkUp) = format(fMarkUp(grsGeral("CD_Produto"), IIf(bAlteraCusto, cCustoAtual, grsGeral("CustoGerencialAnterior")), grsGeral("valorForaPromocao")), "#0.00")
                            .TextMatrix(iLinhaProdAnterior, btMargem) = format(fMargem(IIf(bAlteraCusto, cCustoAtual, grsGeral("CustoGerencialAnterior")), grsGeral("valorForaPromocao"), grsGeral("Aliquota"), grsGeral("PisEntrada"), grsGeral("CofinsEntrada"), grsGeral("PisSaida"), grsGeral("CofinsSaida"), cIcms_compra, grsGeral("ValorFcpEntrada"), grsGeral("AliquotaFcpVenda"), grsGeral("BaseValorStRetido"), grsGeral("AliquotaIcmsStRetido"), grsGeral("BaseValorStRetido"), grsGeral("AliquotaFcpStRetido"), grsGeral("ReducaoBaseStEfetivo"), bApurarSt), "#0.00")



                        ElseIf grsGeral("LOJAPACK") = cboLoja.ItemData(cboLoja.ListIndex) Or (grsGeral("LOJAPACK") <> "Null" And cboLoja.ItemData(cboLoja.ListIndex) = -1) Then

                            .TextMatrix(iLinhaProdAnterior, btValorOriginal) = format(grsGeral("ValorVenda"), "#0.00")
                            .TextMatrix(iLinhaProdAnterior, btValorVenda) = fArredondarValor(format(grsGeral("ValorVenda"), "#0.00"))
                            cValorAntes = grsGeral("ValorVenda")
                            'Se o produto estiver em um pack deve ficar verde, por isso foi usado o mesmo campo de promoção
                            .TextMatrix(iLinhaProdAnterior, btPromocao) = grsGeral("LOJAPACK")
                            .TextMatrix(iLinhaProdAnterior, btMarkUp) = format(fMarkUp(grsGeral("CD_Produto"), IIf(bAlteraCusto, cCustoAtual, grsGeral("CustoGerencialAnterior")), grsGeral("ValorVenda")), "#0.00")
                            .TextMatrix(iLinhaProdAnterior, btMargem) = format(fMargem(IIf(bAlteraCusto, cCustoAtual, grsGeral("CustoGerencialAnterior")), grsGeral("ValorVenda"), grsGeral("Aliquota"), grsGeral("PisEntrada"), grsGeral("CofinsEntrada"), grsGeral("PisSaida"), grsGeral("CofinsSaida"), cIcms_compra, grsGeral("ValorFcpEntrada"), grsGeral("AliquotaFcpVenda"), grsGeral("BaseValorStRetido"), grsGeral("AliquotaIcmsStRetido"), grsGeral("BaseValorStRetido"), grsGeral("AliquotaFcpStRetido"), grsGeral("ReducaoBaseStEfetivo"), bApurarSt), "#0.00")

                            If Not lbUsaDescontoAtacado Then
                                sColorirLinhaPromocao iLinhaProdAnterior
                            Else
                                If grsGeral("ModeloPack") <> 13 Then sColorirLinhaPromocao iLinhaProdAnterior
                            End If

                        Else

                            .TextMatrix(iLinhaProdAnterior, btValorOriginal) = format(grsGeral("ValorVenda"), "#0.00")

                            .TextMatrix(iLinhaProdAnterior, btValorVenda) = fArredondarValor(format(grsGeral("ValorVenda"), "#0.00"))
                            cValorAntes = grsGeral("ValorVenda")
                            .TextMatrix(iLinhaProdAnterior, btMarkUp) = format(fMarkUp(grsGeral("CD_Produto"), IIf(bAlteraCusto, cCustoAtual, grsGeral("CustoGerencialAnterior")), grsGeral("ValorVenda")), "#0.00")
                            .TextMatrix(iLinhaProdAnterior, btMargem) = format(fMargem(IIf(bAlteraCusto, cCustoAtual, grsGeral("CustoGerencialAnterior")), grsGeral("ValorVenda"), grsGeral("Aliquota"), grsGeral("PisEntrada"), grsGeral("CofinsEntrada"), grsGeral("PisSaida"), grsGeral("CofinsSaida"), cIcms_compra, grsGeral("ValorFcpEntrada"), grsGeral("AliquotaFcpVenda"), grsGeral("BaseValorStRetido"), grsGeral("AliquotaIcmsStRetido"), grsGeral("BaseValorStRetido"), grsGeral("AliquotaFcpStRetido"), grsGeral("ReducaoBaseStEfetivo"), bApurarSt), "#0.00")

                        End If
                        .TextMatrix(iLinhaProdAnterior, btAliquota) = format(grsGeral("Aliquota"), "#0.00")
                        .TextMatrix(iLinhaProdAnterior, btPisSaida) = format(grsGeral("PisSaida"), "#0.00")
                        .TextMatrix(iLinhaProdAnterior, btCofinsSaida) = format(grsGeral("CofinsSaida"), "#0.00")

                        .TextMatrix(iLinhaProdAnterior, btCofinsEntrada) = format(grsGeral("CofinsEntrada"), "#0.00")
                        .TextMatrix(iLinhaProdAnterior, btPisEntrada) = format(grsGeral("PisEntrada"), "#0.00")

                        ''se o produto estiver em promocao usa o campo valor da tabela promocao (Valor fora da promoção)
                        If grsGeral("promocao") <> "" Or grsGeral("promocao") <> Null Then
                            .TextMatrix(iLinhaProdAnterior, btValorVendaAntesAlteracao) = format(grsGeral("valorForaPromocao"), "#0.00")
                        Else
                            .TextMatrix(iLinhaProdAnterior, btValorVendaAntesAlteracao) = format(grsGeral("ValorVenda"), "#0.00")
                        End If
                        .TextMatrix(iLinhaProdAnterior, btCfop) = grsGeral("cfop")
                        .TextMatrix(iLinhaProdAnterior, btCodigoFornecedor) = grsGeral("CD_Fornecedor")

                        If Not bAlteraCusto Then
                            .TextMatrix(iLinhaProdAnterior, btCustoExibido) = "*" & format(cCustoAtual, "#0.00")
                            .Col = btCustoExibido
                            .CellForeColor = ldbColoracaoAlteracaoDePreco

                            lblAlteraCusto.Visible = True
                        Else
                            .TextMatrix(iLinhaProdAnterior, btCustoExibido) = format(cCustoAtual, "#0.00")
                        End If

                        .TextMatrix(iLinhaProdAnterior, btCustoMargemAtual) = format(cCustoMargem, "#0.00#")
                        .TextMatrix(iLinhaProdAnterior, btMarkUpIdeal) = format(grsGeral("MarkupIdeal"), "#0.00")
                        .TextMatrix(iLinhaProdAnterior, btMargemIdeal) = format(grsGeral("MargemIdeal"), "#0.00")

                        .TextMatrix(iLinhaProdAnterior, btCodFornecedorTabela) = IIf(IsNull(grsGeral("CodFornecedorTabela")), "", grsGeral("CodFornecedorTabela"))
                        .TextMatrix(iLinhaProdAnterior, btCodBarras) = grsGeral("barras")
                        .TextMatrix(iLinhaProdAnterior, btCodNota) = grsGeral("CD_NOTA")
                        .TextMatrix(iLinhaProdAnterior, btCdNotasProdAgrupados) = grsGeral("CD_NOTA") & "," & .TextMatrix(iLinhaProdAnterior, btCdNotasProdAgrupados)
                        .TextMatrix(iLinhaProdAnterior, btSequenciaNota) = grsGeral("SEQUENCIA")
                        .TextMatrix(iLinhaProdAnterior, btNumeroNota) = grsGeral("NUMERO")
                        .TextMatrix(iLinhaProdAnterior, btCodLoja) = grsGeral("CodLoja")

                        cCustoAnt = format(grsGeral("CustoGerencialAnterior"), "#0.00")
                        cCustoAtu = format(cCustoAtual, "#0.00")
                        .Col = btPicStatusPrecosAlterados
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

                        'CONFIG PARA VIZUALIZAR OU NÃO OS BONIFICADOS.
                        If chkVisualizarBonificacao.Value = vbUnchecked And Right(grsGeral("CFOP"), 3) = 910 Then
                            .RowHeight(CLng(iLinhaProdAnterior)) = 0
                        Else
                            iOcultarLinhaFornecedor = 0
                        End If

                        dbUltimoFornecedor = grsGeral("CD_Fornecedor")

                        If .Rows Mod 50 = 0 Then Me.Refresh

                        'se estiver em promoção mantém o destaque da promoção
                        If .TextMatrix(iLinhaProdAnterior, btPromocao) = "" Then
                            If fVerificarDiferencaMarkup(iLinhaProdAnterior) Then sColorirDiferenca iLinhaProdAnterior
                            If fVerificarDiferencaMargem(iLinhaProdAnterior) Then sColorirDiferenca iLinhaProdAnterior
                        End If
                        If .RowHeight(CLng(iLinhaProdAnterior)) <> 0 Then
                            'liNumeroProdutos = liNumeroProdutos + 1
                            liSomaQtdProdutos = liSomaQtdProdutos - .TextMatrix(iLinhaProdAnterior, btQtdProduto)
                            liSomaQtdProdutos = liSomaQtdProdutos + format(grsGeral("QT_Produto"), "#0.00")
                            lcSomaMarkup = lcSomaMarkup - CCur(.TextMatrix(iLinhaProdAnterior, btQtdProduto) * .TextMatrix(iLinhaProdAnterior, btMarkUp))
                            lcSomaMarkup = lcSomaMarkup + CCur(format(grsGeral("QT_Produto"), "#0.00") * format(fMarkUp(grsGeral("CD_Produto"), IIf(bAlteraCusto, cCustoAtual, grsGeral("CustoGerencialAnterior")), grsGeral("ValorVenda")), "#0.00"))
                            lcSomaMargem = lcSomaMargem - CCur(.TextMatrix(iLinhaProdAnterior, btQtdProduto) * .TextMatrix(iLinhaProdAnterior, btMargem))
                            lcSomaMargem = lcSomaMargem + CCur(format(grsGeral("QT_Produto"), "#0.00") * format(fMargem(IIf(bAlteraCusto, cCustoAtual, grsGeral("CustoGerencialAnterior")), grsGeral("ValorVenda"), grsGeral("Aliquota"), grsGeral("PisEntrada"), grsGeral("CofinsEntrada"), grsGeral("PisSaida"), grsGeral("CofinsSaida"), cIcms_compra, grsGeral("ValorFcpEntrada"), grsGeral("AliquotaFcpVenda"), grsGeral("BaseValorStRetido"), grsGeral("AliquotaIcmsStRetido"), grsGeral("BaseValorStRetido"), grsGeral("AliquotaFcpStRetido"), grsGeral("ReducaoBaseStEfetivo"), bApurarSt), "#0.00"))
                        End If

                        If Not oUsuarioAcesso.PermiteAlterar(609, CInt(grsGeral("CodLoja"))) Then bBloquearAlterar = True

                    Else
                        .TextMatrix(iLinhaProdAnterior, btCdNotasProdAgrupados) = grsGeral("CD_NOTA") & "," & .TextMatrix(iLinhaProdAnterior, btCdNotasProdAgrupados)
                    End If
                    GoTo SaiLoopNaoAdicionaProdDuplicadoMenorCusto

                End If
            End If
```