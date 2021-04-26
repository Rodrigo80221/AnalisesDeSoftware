using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Telecon.Genericos.Classes.BancoDeDados;
using Telecon.Genericos.Classes.TiposDados;
using Telecon.GestaoComercial.Biblioteca.Outros;

namespace Telecon.GestaoComercial.Biblioteca.PackVirtual
{
    //Esta classe não será sobreescrita pelo Telecode
    public partial class PackVirtual
    {
        public static bool TestarCampos(IBanco banco, PackVirtual packVirtual)
        {
            return true;
        }


        public static List<PackVirtual> ConsultarTodos(IBanco banco)
        {
            return ConsultarSQL(banco, " ");
        }


        public static bool GerarInsertComandosPDV(IBanco banco, PackVirtual packVirtual, List<PackVirtualGrupo1> grupos1, List<PackVirtualGrupo2> grupos2)
        {
            try
            {
                var sql = "";
                string campos = "", valores = "";

                var bancoAccess = new Access("");

                campos += "Codigo, ";
                valores += bancoAccess.ObterDuplo(packVirtual.Codigo) + ",";
                campos += "Descricao, ";
                valores += bancoAccess.ObterTexto(packVirtual.Descricao == null ? "" : packVirtual.Descricao, 256) + ",";
                campos += "DtInicial, ";
                valores += bancoAccess.ObterDataHora(packVirtual.DtInicial) + ",";
                campos += "DtFinal, ";
                valores += bancoAccess.ObterDataHora(packVirtual.DtFinal) + ",";
                campos += "ModeloPack, ";
                valores += bancoAccess.ObterInteiro(packVirtual.ModeloPack) + ",";
                campos += "QtdRegra, ";
                valores += bancoAccess.ObterMoeda(packVirtual.QtdRegra, 3) + ",";
                campos += "ValidoClienteNaoIdent, ";
                valores += bancoAccess.ObterVerdadeiroFalso(packVirtual.ValidoClienteNaoIdent) + ",";
                campos += "TipoAjusteValor, ";
                valores += bancoAccess.ObterTexto(packVirtual.TipoAjusteValor, 2) + ",";
                campos += "AjusteUltimaCasaDecimal, ";
                valores += bancoAccess.ObterDuplo(Convert.ToDouble(packVirtual.AjusteUltimaCasaDecimal)) + ",";
                campos += "VlrRegra, ";
                valores += bancoAccess.ObterMoeda(packVirtual.VlrRegra, 2) + ",";
                campos += "QuantidadeLimite";
                valores += bancoAccess.ObterInteiro(packVirtual.QuantidadeLimite);

                sql = "Insert into PackVirtual(" + campos + ") Values(" + valores + ")";

                banco.ExecutarComando("EXEC SP_GRAVA_COMANDOS_PDV 'DELETE FROM PackVirtual where codigo = " + packVirtual.Codigo + "'");
                sql = sql.Replace("'", "''");
                banco.ExecutarComando("EXEC SP_GRAVA_COMANDOS_PDV '" + sql + "'");
                banco.ExecutarComando("EXEC SP_GRAVA_COMANDOS_PDV 'DELETE FROM PACKVIRTUALGRUPO1 WHERE CODPACK =  " + packVirtual.Codigo + "'");
                foreach (var packVirtualGrupo1 in grupos1)
                {
                    campos = "";
                    valores = "";

                    campos += "CodPack, ";
                    valores += bancoAccess.ObterDuplo(packVirtualGrupo1.CodPack) + ",";

                    campos += "CodProduto ";
                    valores += bancoAccess.ObterDuplo(packVirtualGrupo1.CodProduto);

                    sql = "Insert into PackVirtualGrupo1(" + campos + ") Values(" + valores + ")";
                    sql = sql.Replace("'", "''");
                    banco.ExecutarComando("EXEC SP_GRAVA_COMANDOS_PDV '" + sql + "'");
                }

                banco.ExecutarComando("EXEC SP_GRAVA_COMANDOS_PDV 'DELETE FROM PACKVIRTUALGRUPO2 WHERE CODPACK = " + packVirtual.Codigo + "'");
                foreach (var packVirtualGrupo2 in grupos2)
                {
                    campos = "";
                    valores = "";

                    campos += "CodPack, ";
                    valores += bancoAccess.ObterDuplo(packVirtualGrupo2.CodPack) + ",";

                    campos += "CodProduto ";
                    valores += bancoAccess.ObterDuplo(packVirtualGrupo2.CodProduto);

                    sql = "Insert into PackVirtualGrupo2(" + campos + ") Values(" + valores + ")";
                    sql = sql.Replace("'", "''");
                    banco.ExecutarComando("EXEC SP_GRAVA_COMANDOS_PDV '" + sql + "'");
                }

                var lojas = PackVirtualLojas.ObterLojasCodigoPack(banco, packVirtual.Codigo);
                banco.ExecutarComando("EXEC SP_GRAVA_COMANDOS_PDV 'DELETE FROM PackVirtualLojas WHERE CODPACK = " + packVirtual.Codigo + "'");
                foreach (var loja in lojas)
                {
                    campos = "";
                    valores = "";

                    campos += "CodPack, ";
                    valores += banco.ObterDuplo(packVirtual.Codigo) + ",";

                    campos += "CodLoja ";
                    valores += banco.ObterInteiro(loja.CodLoja);

                    sql = "Insert into PackVirtualLojas(" + campos + ") Values(" + valores + ")";
                    sql = sql.Replace("'", "''");
                    banco.ExecutarComando("EXEC SP_GRAVA_COMANDOS_PDV '" + sql + "'");
                }
                var formas = PackVirtualFormasPgto.ConsultarFormas(banco, packVirtual.Codigo);
                banco.ExecutarComando("EXEC SP_GRAVA_COMANDOS_PDV 'DELETE FROM PackVirtualFormasPgto WHERE CODPACK = " + packVirtual.Codigo + "'");
                foreach (var forma in formas)
                {
                    campos = "";
                    valores = "";

                    campos += "CodPack, ";
                    valores += banco.ObterDuplo(packVirtual.Codigo) + ",";

                    campos += "CodFormaPGto ";
                    valores += banco.ObterInteiro(forma.CodFormaPgto);

                    sql = "Insert into PackVirtualFormasPgto(" + campos + ") Values(" + valores + ")";
                    sql = sql.Replace("'", "''");
                    banco.ExecutarComando("EXEC SP_GRAVA_COMANDOS_PDV '" + sql + "'");
                }
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }


        private static List<PackVirtual> ConsultarPackScannTechPorLoja(IBanco banco, int codigoLoja)
        {
            var packVirtualLoja = PackVirtualLojas.ConsultarPackVirtualLojas(banco, codigoLoja);

            var codigoPack = " ";

            var listaVirtualPack = new List<PackVirtual>();

            if (packVirtualLoja != null)
            {
                codigoPack = packVirtualLoja.Aggregate(codigoPack, (current, item) => current + (", " + item.CodPack));

                codigoPack.Remove(1);
                codigoPack.Remove(codigoPack.Length);

                listaVirtualPack = ConsultarSQL(banco, " Where CodScanTech <> 0 and Codigo in (" + codigoPack + ")");
            }

            return listaVirtualPack;
        }

        private static List<PackVirtual> ConsultarPackPorLoja(IBanco banco, int codigoLoja)
        {
            var packVirtualLoja = PackVirtualLojas.ConsultarPackVirtualLojas(banco, codigoLoja);

            var codigoPack = " ";

            var listaVirtualPack = new List<PackVirtual>();

            if (packVirtualLoja != null)
            {
                codigoPack = packVirtualLoja.Aggregate(codigoPack, (current, item) => current + (", " + item.CodPack));

                codigoPack.Remove(1);
                codigoPack.Remove(codigoPack.Length);

                listaVirtualPack = ConsultarSQL(banco, " Where Codigo in (" + codigoPack + ")");
            }

            return listaVirtualPack;
        }

        public static List<PackVirtual> ConsultarPacksAtuais(IBanco banco, int codigoLoja)
        {
            var packVirtualLoja = PackVirtualLojas.ConsultarPackVirtualLojas(banco, codigoLoja);

            var codigoPack = " ";

            var listaVirtualPack = new List<PackVirtual>();

            if (packVirtualLoja != null)
            {
                codigoPack = packVirtualLoja.Aggregate(codigoPack, (current, item) => current + (", " + item.CodPack));

                codigoPack.Remove(1);
                codigoPack.Remove(codigoPack.Length - 1);

                listaVirtualPack = ConsultarSQL(banco,
                                                "Where Codigo in (" + codigoPack +
                                                ") And (DT.Inicial >= getDate() Or DT.Final <= getDate())");
            }

            return listaVirtualPack;
        }


        private static List<PackVirtual> ConsultarPackPorData(IBanco banco, DateTime dataInicial, DateTime dataFinal,
                                                              int codigoLoja)
        {
            var packVirtualLoja = PackVirtualLojas.ConsultarPackVirtualLojas(banco, codigoLoja);

            var codigoPack = " ";

            var listaVirtualPack = new List<PackVirtual>();

            if (packVirtualLoja != null)
            {
                codigoPack = packVirtualLoja.Aggregate(codigoPack, (current, item) => current + (", " + item.CodPack));

                codigoPack.Remove(1);
                codigoPack.Remove(codigoPack.Length - 1);

                listaVirtualPack = ConsultarSQL(banco,
                                                "Where Codigo in (" + codigoPack + ") And (DT.Inicial >= " + dataInicial +
                                                " Or DT.Final <= " + dataFinal + ")");
            }

            return listaVirtualPack;
        }

        public static int ObterProximoCodigo(IBanco banco)
        {
            var consultaSQL = "Select Max(Codigo)+1 as Codigo from PackVirtual";

            var dr = banco.Consultar(consultaSQL);

            var codigo = 1;


            if (dr.Read())
            {
                if (!string.IsNullOrEmpty(dr["Codigo"].ToString()))
                    codigo = int.Parse(dr["Codigo"].ToString());
            }

            return codigo;
        }

        public static string buscarValorAtualLoja(IBanco banco, string codLoja, string codigoProduto)
        {
            string retorno = "0";
            string sql;

            if (codLoja == "0")
            {
                sql = " Select Codigo from Lojas where Nome like '%matriz%'";
                var drLoja = banco.Consultar(sql.ToString());
                while (drLoja.Read())
                {
                    codLoja = drLoja["Codigo"].ToString();
                }
                if (codLoja == "0")
                    codLoja = "1";
            }

            sql = "  Select valorProduto from ProdutoLojas where codProduto = " + codigoProduto + " and codLoja = " + codLoja;
            var dr = banco.Consultar(sql.ToString());
            while (dr.Read())
            {
                retorno = dr["valorProduto"].ToString();
            }
            return retorno;
        }

        public static string buscaValorPreco2(IBanco banco, string codLoja, string codigoProduto, string codigoPack)
        {
            string sql;
            string retorno = "0";

            if (codLoja == "0")
            {
                sql = " Select Codigo from Lojas where Nome like '%matriz%'";
                var drLoja = banco.Consultar(sql.ToString());
                while (drLoja.Read())
                {
                    codLoja = drLoja["Codigo"].ToString();
                }
                if (codLoja == "0")
                    codLoja = "1";
            }

            sql = " Select PP.VlrPreco2 From PackVirtualLojas PL ";
            sql += " INNER JOIN PackVirtualPreco2 PP ON (PP.CodPack = PL.CodPack) ";
            sql += " INNER JOIN PackVirtual PV on (PV.Codigo = PP.CodPack) ";
            sql += " WHERE PP.CodProduto = " + codigoProduto;
            sql += " AND PL.CodLoja = " + codLoja;
            if (codigoPack != "0")
                sql += " AND PV.Codigo = " + codigoPack;

            var dr = banco.Consultar(sql.ToString());

            if (dr.Read())
                retorno = dr["VlrPreco2"].ToString();
            else
                retorno = PackVirtual.buscarValorAtualLoja(banco, codLoja, codigoProduto);

            return retorno;
        }

        public static void deletarTabelasPack(IBanco banco, string tabela, double codPack)
        {
            StringBuilder comandoSQL = new StringBuilder();
            comandoSQL.Append(" Delete From " + tabela + " Where CodPack = " + codPack);
            banco.ExecutarComando(comandoSQL.ToString());
        }

        public static void ForcarEnvioPreco2(IBanco banco, double codPack)
        {
            banco.ExecutarComando(" Update PackVirtual Set EnviouPreco2 = 0 where Codigo = " + codPack);
        }

        public static bool Alterar(IBanco banco, PackVirtual packVirtual)
        {
            return AlterarSQL(banco, packVirtual);
        }

        public static bool Excluir(IBanco banco, PackVirtual packVirtual)
        {
            return ExcluirSQL(banco, packVirtual);
        }

        public static bool Gravar(IBanco banco, PackVirtual packVirtual)
        {
            return GravarSQL(banco, packVirtual);
        }

        public static bool Inserir(IBanco banco, PackVirtual packVirtual)
        {
            return InserirSQL(banco, packVirtual);
        }
    }

    public class PackVirtualSimplificado
    {
        #region Propriedades

        //public bool Selecao { get; set; }
        public double Codigo { get; set; }
        public string Descricao { get; set; }
        public string ModeloPack { get; set; }

        public DateTime DtInicial { get; set; }
        public DateTime DtFinal { get; set; }
        public string CodGrupoCliente { get; set; }
        public int QuantidadeLimite { get; set; }
        public int CodScannTech { get; set; }

        #endregion

        #region contrutores

        public PackVirtualSimplificado(double codigo, string descricao, string modeloPack,
                                       DateTime dtInicial, DateTime dtFinal, string codGrupoCliente, int quantidadeLimite, int codScannTech)
        {
            //Selecao = selecao;
            Codigo = codigo;
            Descricao = descricao;
            ModeloPack = modeloPack;
            DtInicial = dtInicial;
            DtFinal = dtFinal;
            CodGrupoCliente = codGrupoCliente;
            QuantidadeLimite = quantidadeLimite;
            CodScannTech = codScannTech;
        }

        #endregion

        public static PackVirtualSimplificado ConverterPackVirtualParaSimplificado(PackVirtual packVirtual, string grupoCliente)
        {
            var modeloPack = "";

            switch (packVirtual.ModeloPack)
            {
                case 01:
                    modeloPack = "Leve X Pague Y";
                    break;

                case 02:
                    modeloPack = "Pague 1 centavo ou mais em outro produto";
                    break;

                case 03:
                    modeloPack = "Pague menos a partir de x unidades (atacado)";
                    break;

                case 04:
                    modeloPack = "Pague menos por unidade";
                    break;

                case 05:
                    modeloPack = "Pague 1 centavo ou mais no próximo produto";
                    break;

                case 06:
                    modeloPack = "Leve x e Receba desconto percentual";
                    break;

                case 07:
                    modeloPack = "Leve x e Receba desconto por unidade";
                    break;

                case 08:
                    modeloPack = "Valor Diferenciado (Preço 2)";
                    break;

                case 09:
                    modeloPack = "Pague x porcento a menos a partir de x unidades (atacado)";
                    break;

                case 10:
                    modeloPack = "Leve R$ X e Receba desconto por unidade no produto x";
                    break;

                case 11:
                    modeloPack = "Leve R$ X e Receba desconto percentual no produto x";
                    break;

                default:
                    modeloPack = "Selecione um modelo";
                    break;
            }

            var packVirtualSimplificado = new PackVirtualSimplificado(packVirtual.Codigo, packVirtual.Descricao, modeloPack, packVirtual.DtInicial, packVirtual.DtFinal, grupoCliente, packVirtual.QuantidadeLimite, packVirtual.CodScannTech);

            return packVirtualSimplificado;
        }
    }
}