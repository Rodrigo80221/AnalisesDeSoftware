using Telecon.Genericos.Controles;

namespace Telecon.GestaoComercial.Biblioteca.PackVirtual
{

    public partial class ModeloPack
    {
        public class Modelo
        {
            public Modelo(string descricaoComercial, TipoDePack tipo, int codModeloPack, FormatoDoTxtValorRegra formatoDoOpcional, string descricaoAntiga, string exemplosDePromocao, bool exibirObjetosPreco2, bool exibirPanelGrupo2, bool exibirObjetosGruposClientes, bool exibirObjetosEncarte, bool exibirGroupBoxValores, bool exibirObjetosRegras, bool exibir_gbAjustarQuebra, bool exibir_gbxLimitePack, string descricaoLabel1, string descricaoLabel2, string descricaoLabel3, string descricaoLabel4, string observacao, int valorDefault_txtQtdRegra, int valorDefault_txtValorRegra, CaixaTexto.TipoFormato formato_txtQtdRegra, CaixaTexto.TipoFormato formato_txtValorRegra)
            {
                DescricaoComercial = descricaoComercial;
                Tipo = tipo;
                CodModeloPack = codModeloPack;
                FormatoDoOpcional = formatoDoOpcional;
                DescricaoAntiga = descricaoAntiga;
                ExemplosDePromocao = exemplosDePromocao;
                ExibirObjetosPreco2 = exibirObjetosPreco2;
                ExibirPanelGrupo2 = exibirPanelGrupo2;
                ExibirObjetosGruposClientes = exibirObjetosGruposClientes;
                ExibirObjetosEncarte = exibirObjetosEncarte;
                ExibirGroupBoxValores = exibirGroupBoxValores;
                ExibirObjetosRegras = exibirObjetosRegras;
                Exibir_gbxLimitePack = exibir_gbxLimitePack;
                Exibir_gbAjustarQuebra = exibir_gbAjustarQuebra;
                DescricaoLabel1 = descricaoLabel1;
                DescricaoLabel2 = descricaoLabel2;
                DescricaoLabel3 = descricaoLabel3;
                DescricaoLabel4 = descricaoLabel4;
                Observacao = observacao;
                ValorDefault_txtQtdRegra = valorDefault_txtQtdRegra;
                ValorDefault_txtValorRegra = valorDefault_txtValorRegra;
                Formato_txtQtdRegra = formato_txtQtdRegra;
                Formato_txtValorRegra = formato_txtValorRegra;
            }            

            public string DescricaoComercial { get; set; }
            //--------
            public TipoDePack Tipo { get; set; }
            //--------
            public int CodModeloPack { get; set; }
            public FormatoDoTxtValorRegra FormatoDoOpcional{ get; set; }
            //--------
             public string DescricaoAntiga { get; set; }
            //--------
            public string ExemplosDePromocao { get; set; }

            //--------

            #region Definição de quais objetos ficarão visíveis na tela 

            public bool ExibirObjetosPreco2 { get; set; } //  ExibirObjetosPreco2(false);
            public bool ExibirPanelGrupo2 { get; set; } // ExibirPanelGrupo2(!VALOR);
            public bool ExibirObjetosGruposClientes { get; set; } // ExibirObjetosGruposClientes(true);
            public bool ExibirObjetosEncarte { get; set; } // ExibirObjetosEncarte(true);
            public bool ExibirGroupBoxValores { get; set; } // ExibirGroupBoxValores(!VALOR);
            public bool ExibirObjetosRegras { get; set; } //  ExibirObjetosRegras(!VALOR);
            public bool Exibir_gbxLimitePack { get; set; } // gbxLimitePack
            public bool Exibir_gbAjustarQuebra { get; set; } // gbAjustarQuebra

            #endregion

            //--------

            #region Definir formatação e valores default para campos 

            public string DescricaoLabel1 { get; set; }
            public string DescricaoLabel2 { get; set; }
            public string DescricaoLabel3 { get; set; }
            public string DescricaoLabel4 { get; set; }
            public string Observacao { get; set; } // Ex label lblProdutoGratis

            public int ValorDefault_txtQtdRegra { get; set; }
            public int ValorDefault_txtValorRegra { get; set; }

            public CaixaTexto.TipoFormato Formato_txtQtdRegra { get; set; }
            public CaixaTexto.TipoFormato Formato_txtValorRegra { get; set; }

            #endregion

            //--------

            public override string ToString()
            {
                return this.DescricaoComercial;
            }

        }


    }
}
