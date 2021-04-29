using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Telecon.Genericos.Controles;

namespace Telecon.GestaoComercial.Biblioteca.PackVirtual
{

    public partial class ModeloPack
    {

        
        public Modelo Leve3Pague2 { get; }
        public Modelo Leve3Ganhe1 { get; }
        public Modelo Leve3Ganhe1ProdutoDiferente { get; }
        // ================
        public Modelo Leve3RecebaDescontoEmOutroProdutoDiferenteMONETARIO{ get; }
        public Modelo Leve3RecebaDescontoEmOutroProdutoDiferentePORCENTAGEM { get; }
        // ================
        public Modelo Leve6PagueMenosEmCadaUnidMONETARIO { get; }
        public Modelo Leve6PagueMenosEmCadaUnidPORCENTAGEM { get; }
        // ================
        public Modelo ApartirDe6PagueMenosMONETARIO { get; }
        public Modelo ApartirDe6PagueMenosPORCENTAGEM { get; }
        // ================
        public Modelo NasComprasAcimaDe50GanheDescontoNestesProdutosMONETARIO { get; }
        public Modelo NasComprasAcimaDe50GanheDescontoNestesProdutosPORCENTAGEM { get; }
        // ================
        public Modelo ValorDiferenciadoPreco2 { get; }
        public Modelo ApartirDeXGanheDescontoAtacado { get; }
        public Modelo ApartirDeXGanheDescontoProdutosAssociadosAtacado { get; }

        public ModeloPack()
        {
            // ================
            Leve3Pague2 = ConstruirLeve3Pague2();
            Leve3Ganhe1 = ConstruirLeve3Ganhe1();
            Leve3Ganhe1ProdutoDiferente = ConstruirLeve3Ganhe1ProdutoDiferente();
            // ================
            Leve3RecebaDescontoEmOutroProdutoDiferenteMONETARIO = ConstruirLeve3RecebaDescontoEmOutroProdutoDiferente(FormatoDoTxtValorRegra.MoedaComOpcional);
            Leve3RecebaDescontoEmOutroProdutoDiferentePORCENTAGEM = ConstruirLeve3RecebaDescontoEmOutroProdutoDiferente(FormatoDoTxtValorRegra.PorcentagemComOpcional);
            // ================
            Leve6PagueMenosEmCadaUnidMONETARIO = ConstruirLeve6PagueMenosEmCadaUnid(FormatoDoTxtValorRegra.MoedaComOpcional);
            Leve6PagueMenosEmCadaUnidPORCENTAGEM = ConstruirLeve6PagueMenosEmCadaUnid(FormatoDoTxtValorRegra.PorcentagemComOpcional);
            // ================
            ApartirDe6PagueMenosMONETARIO = ConstruirApartirDe6PagueMenos(FormatoDoTxtValorRegra.MoedaComOpcional);
            ApartirDe6PagueMenosPORCENTAGEM = ConstruirApartirDe6PagueMenos(FormatoDoTxtValorRegra.PorcentagemComOpcional);
            // ================
            NasComprasAcimaDe50GanheDescontoNestesProdutosMONETARIO = ConstruirNasComprasAcimaDe50GanheDescontoNestesProdutos(FormatoDoTxtValorRegra.MoedaComOpcional);
            NasComprasAcimaDe50GanheDescontoNestesProdutosPORCENTAGEM = ConstruirNasComprasAcimaDe50GanheDescontoNestesProdutos(FormatoDoTxtValorRegra.PorcentagemComOpcional);
            // ================
            ValorDiferenciadoPreco2 = ConstruirValorDiferenciadoPreco2();
            ApartirDeXGanheDescontoAtacado = ConstruirApartirDeXGanheDescontoAtacado();
            ApartirDeXGanheDescontoProdutosAssociadosAtacado = ConstruirApartirDeXGanheDescontoProdutosAssociadosAtacado();
        }
        

        private Modelo ConstruirLeve3Pague2()
        {
            #region Leve 3 Pague 2            

            string ajuda = "Exemplo de promoção: " + Environment.NewLine;
            ajuda += "Compre 12 unidades do refrigerante XXX e pague apenas 10 unidades.";

            Modelo modelo = new Modelo(
                "Leve 3 Pague 2",
                TipoDePack.PackVirtual,
                1,
                FormatoDoTxtValorRegra.UnitárioSemOpcional,
                "Leve X Pague Y",
                ajuda,//exemplosDePromocao
                false,//exibirObjetosPreco2
                false,//exibirPanelGrupo2
                true,//exibirObjetosGruposClientes
                true,//exibirObjetosEncarte
                true,//exibirGroupBoxValores
                true,//exibirObjetosRegras
                false,//exibir_gbAjustarQuebra
                true,//exibir_gbxLimitePack
                "Leve ",// "Compre ",
                "unidade(s) ",//"unidade(s) deste(s) produto(s) "
                "Pague ",//"Pague apenas "
                "  unidade(s)",//"  unidade(s)"
                0,
                0,
                CaixaTexto.TipoFormato.Inteiro,
                CaixaTexto.TipoFormato.Inteiro);
           
            return modelo;

            #endregion
        }


        private Modelo ConstruirLeve3Ganhe1()
        {
            #region Leve 3 e ganhe 1 

            var ajuda = "Exemplo de promoção 1: " + Environment.NewLine;
            ajuda += "Compre 5 biscoitos da marca XXX e ganhe 1 biscoito." + Environment.NewLine + Environment.NewLine;

            ajuda += "Exemplo de promoção 2: " + Environment.NewLine;
            ajuda += "Compre 5 biscoitos da marca XXX e pague menos no 6º biscoito." + Environment.NewLine + Environment.NewLine;

            ajuda += "Obs: Preenchar com 0 (zero) para produto grátis ou insira um valor para o produto." + Environment.NewLine + Environment.NewLine;

            Modelo modelo = new Modelo("Leve 3 e ganhe 1",
                TipoDePack.PackVirtual,
                5,
                FormatoDoTxtValorRegra.MoedaSemOpcional,
                "Pague 1 centavo ou mais no próximo produto",
                ajuda,//exemplosDePromocao
                false,//exibirObjetosPreco2
                false,//exibirPanelGrupo2
                true,//exibirObjetosGruposClientes
                true,//exibirObjetosEncarte
                true,//exibirGroupBoxValores
                true,//exibirObjetosRegras
                false,//exibir_gbAjustarQuebra
                false,//exibir_gbxLimitePack                
                "Leve ",//"A cada "
                "unidade(s) ",//"unidade(s) deste(s) produto(s) "
                "Pague R$",//"Pague R$"
                " na próxima unidade",//" na próxima unidade do(s) mesmo(s) produto(s)"
                0,
                0,
                CaixaTexto.TipoFormato.Inteiro,
                CaixaTexto.TipoFormato.Moeda);

             #endregion

            return modelo;
        }




        private Modelo ConstruirLeve3Ganhe1ProdutoDiferente()
        {

            #region Leve 3 e ganhe 1 produto diferente  

            var ajuda = "Exemplo de promoção 1: " + Environment.NewLine;
            ajuda += "Compre 2 unidades da Pizza XXX e ganhe um bombom da marca YYY." + Environment.NewLine + Environment.NewLine;

            ajuda += "Exemplo de promoção 2: " + Environment.NewLine;
            ajuda += "Compre 2 unidades da Pizza XXX e pague R$5,00 no refrigerante 2 litros" + Environment.NewLine + Environment.NewLine;

            ajuda += "Observação: Para produtos grátis informe 0 (zero) no preço do próximo produto." + Environment.NewLine + Environment.NewLine;

            Modelo modelo = new Modelo("Leve 3 e ganhe 1 produto diferente",
                TipoDePack.PackVirtual,
                2,
                FormatoDoTxtValorRegra.MoedaSemOpcional,
                "Pague 1 centavo ou mais em outro produto",
                ajuda,//exemplosDePromocao
                false,//exibirObjetosPreco2
                true,//exibirPanelGrupo2
                true,//exibirObjetosGruposClientes
                true,//exibirObjetosEncarte
                true,//exibirGroupBoxValores
                true,//exibirObjetosRegras
                false,//exibir_gbAjustarQuebra
                false,//exibir_gbxLimitePack
                "Compre ",//"Compre "
                "unidade(s) ",//"unidade(s) deste(s) produto(s) "
                "Pague R$",//"Pague apenas R$"
                "  nestes outros produtos",//  nestes outros produtos
                0,
                0,
                CaixaTexto.TipoFormato.Inteiro,
                CaixaTexto.TipoFormato.Moeda                
                );
           
            #endregion

            return modelo;
        }





        private Modelo ConstruirLeve3RecebaDescontoEmOutroProdutoDiferente(ModeloPack.FormatoDoTxtValorRegra modeloPack)
        {

            #region Leve 3 e ganhe desconto em outro produto diferente (R$ ou %)

            if (modeloPack == FormatoDoTxtValorRegra.MoedaComOpcional)
            {
                var ajuda = "Exemplo de promoção: " + Environment.NewLine;
                ajuda += "Compre 2 Pizzas XXX e ganhe desconto de R$ 3,00 no refrigerante YYY." + Environment.NewLine + Environment.NewLine;
                ajuda += "Obs:No exemplo acima você pode adicionar vários sabores de pizzas no quadro da esquerda ou vários sabores de refrigerante no quadro da direita" + Environment.NewLine + Environment.NewLine;

                Modelo modelo = new Modelo("Leve 3 e receba desconto em outro produto diferente (R$)",
                    TipoDePack.PackVirtual,
                    7,
                    FormatoDoTxtValorRegra.MoedaComOpcional,
                    "Leve x e Receba desconto por unidade",
                    ajuda,//exemplosDePromocao
                    false,//exibirObjetosPreco2
                    true,//exibirPanelGrupo2
                    true,//exibirObjetosGruposClientes
                    true,//exibirObjetosEncarte
                    true,//exibirGroupBoxValores
                    true,//exibirObjetosRegras
                    false,//exibir_gbAjustarQuebra
                    false,//exibir_gbxLimitePack
                    "Compre ",//"Compre "
                    " unidade(s) ",//" unidade(s) deste(s) produto(s)"
                    "Ganhe R$",//"Receba R$ "
                    " de desconto nestes outros produtos",//" de desconto nestes outros produtos"
                    0,
                    0,
                    CaixaTexto.TipoFormato.Inteiro,
                    CaixaTexto.TipoFormato.Moeda);

                return modelo;
            }           
            else if (modeloPack == FormatoDoTxtValorRegra.PorcentagemComOpcional)
            {
                var ajuda = "Exemplo de promoção: " + Environment.NewLine;
                ajuda += "Compre 2 Pizzas XXX e ganhe desconto 10% no refrigerante YYY." + Environment.NewLine + Environment.NewLine;
                ajuda += "Obs:No exemplo acima você pode adicionar vários sabores de pizzas no quadro da esquerda ou vários sabores de refrigerante no quadro da direita" + Environment.NewLine + Environment.NewLine;

                Modelo modelo = new Modelo("Leve 3 e receba desconto em outro produto diferente (%)",
                    TipoDePack.PackVirtual,
                    6,
                    FormatoDoTxtValorRegra.PorcentagemComOpcional,
                    "Leve x e Receba desconto percentual",
                    ajuda,//exemplosDePromocao
                    false,//exibirObjetosPreco2
                    true,//exibirPanelGrupo2
                    true,//exibirObjetosGruposClientes
                    true,//exibirObjetosEncarte
                    true,//exibirGroupBoxValores
                    true,//exibir_gbAjustarQuebra
                    false,//exibir_gbAjustarQuebra
                    false,//exibir_gbxLimitePack
                    "Compre ",//"Compre "
                    " unidade(s) ",//" unidade(s) deste(s) produto(s)"
                    "Receba ",//"Receba "
                    "% de desconto nestes outros produtos",//"% de desconto nestes outros produtos"
                    0,
                    0,
                    CaixaTexto.TipoFormato.Inteiro,
                    CaixaTexto.TipoFormato.Moeda);

                return modelo;

            }
            else
            {
                return null;
            }

            #endregion



        }




        private Modelo ConstruirLeve6PagueMenosEmCadaUnid(ModeloPack.FormatoDoTxtValorRegra modeloPack)
        {
            #region Leve 6 e pague menos em cada Unid. (R$ ou %)


            if (modeloPack == FormatoDoTxtValorRegra.MoedaComOpcional)
            {
                var ajuda = "Exemplo de promoção: " + Environment.NewLine;
                ajuda += "Compre 12 caixas de leite da marca XXX e pague R$ 1,00 cada caixa." + Environment.NewLine + Environment.NewLine;
                ajuda += "Observação: A 13ª caixa de leite custará o preço original." + Environment.NewLine + Environment.NewLine;

                Modelo modelo = new Modelo("Leve 6 e pague menos em cada Unid. (R$)",
                    TipoDePack.PackVirtual,
                    4,
                    FormatoDoTxtValorRegra.MoedaComOpcional,
                    "Pague menos por unidade",
                    ajuda,//exemplosDePromocao
                    false,//exibirObjetosPreco2
                    false,//exibirPanelGrupo2
                    true,//exibirObjetosGruposClientes
                    true,//exibirObjetosEncarte
                    true,//exibirGroupBoxValores
                    true,//exibirObjetosRegras
                    false,//exibir_gbAjustarQuebra
                    true,//exibir_gbxLimitePack
                    "Leve ",//"Compre "
                    " unidade(s) ",//"unidade(s) deste(s) produto(s) "
                    "Pague R$",//"Pague apenas R$"
                    " por unidade(s)",//" por unidade(s)"
                    0,
                    0,
                    CaixaTexto.TipoFormato.Inteiro,
                    CaixaTexto.TipoFormato.Moeda);
                return modelo;
            }
            else if (modeloPack == FormatoDoTxtValorRegra.PorcentagemComOpcional)
            {
                var ajuda = "Exemplo de promoção: " + Environment.NewLine;
                ajuda += "Compre 12 caixas de leite da marca XXX receba 10% de desconto em cada caixa." + Environment.NewLine + Environment.NewLine;
                ajuda += "Observação: A 13ª caixa de leite custará o preço original." + Environment.NewLine + Environment.NewLine;

                Modelo modelo = new Modelo("Leve 6 e pague menos em cada Unid. (%)",
                    TipoDePack.PackVirtual,
                    12,
                    FormatoDoTxtValorRegra.PorcentagemComOpcional,
                    "Pague x porcento a menos por unidade",
                    ajuda,//exemplosDePromocao
                    false,//exibirObjetosPreco2
                    false,//exibirPanelGrupo2
                    true,//exibirObjetosGruposClientes
                    true,//exibirObjetosEncarte
                    true,//exibirGroupBoxValores
                    true,//exibirObjetosRegras
                    false,//exibir_gbAjustarQuebra
                    true,//exibir_gbxLimitePack
                    "Compre ",//"Compre "
                    " unidade(s) ",//"unidade(s) deste(s) produto(s) "
                    "Receba ",//"Receba "
                    "% de desconto nestes produtos",//"% de desconto nestes produtos"
                    0,
                    0,
                    CaixaTexto.TipoFormato.Inteiro,
                    CaixaTexto.TipoFormato.Moeda);
                return modelo;
            }
            else
            {
                return null;
            }
                
            #endregion      
           
        }


        private Modelo ConstruirApartirDe6PagueMenos(ModeloPack.FormatoDoTxtValorRegra modeloPack)
        {
            #region A partir de 6 pague menos  (R$ ou %)

            if (modeloPack == FormatoDoTxtValorRegra.MoedaComOpcional)
            {
                var ajuda = "Exemplo de promoção: " + Environment.NewLine;
                ajuda += "A partir de 12 latas de Refri pague 1 real em cada unidade." + Environment.NewLine + Environment.NewLine;
                ajuda += "Observação: Você pode colocar vários tipos ou sabores na grade, eles serão somados na contagem." + Environment.NewLine + Environment.NewLine;

                Modelo modelo = new Modelo("A partir de 6 pague menos (R$)",
                    TipoDePack.PackVirtual,
                    3,
                    FormatoDoTxtValorRegra.MoedaComOpcional,
                    "Pague menos a partir de x unidades (atacado)",
                    ajuda,//exemplosDePromocao
                    false,//exibirObjetosPreco2
                    false,//exibirPanelGrupo2
                    true,//exibirObjetosGruposClientes
                    true,//exibirObjetosEncarte
                    true,//exibirGroupBoxValores
                    true,//exibirObjetosRegras
                    false,//exibir_gbAjustarQuebra
                    false,//exibir_gbxLimitePack
                    "Compre ",//"Compre "
                    "unidade(s) ",//"unidade(s) deste(s) produto(s) "
                    "Pague R$",//"Pague apenas R$"
                    " por unidade(s)",//" por unidade(s)"
                    0,
                    0,
                    CaixaTexto.TipoFormato.Inteiro,
                    CaixaTexto.TipoFormato.Moeda);
                return modelo;
            }
            else if (modeloPack == FormatoDoTxtValorRegra.PorcentagemComOpcional)
            {
                var ajuda = "Exemplo de promoção: " + Environment.NewLine;
                ajuda += "A partir de 12 latas de Refri ganhe 10% de desconto em cada unidade." + Environment.NewLine + Environment.NewLine;
                ajuda += "Observação: Você pode colocar vários tipos ou sabores na grade, eles serão somados na contagem." + Environment.NewLine + Environment.NewLine;

                Modelo modelo = new Modelo("A partir de 6 pague menos (%)",
                    TipoDePack.PackVirtual,
                    9,
                    FormatoDoTxtValorRegra.PorcentagemComOpcional,
                    "Pague x porcento a menos a partir de x unidades (atacado)",
                    ajuda,//exemplosDePromocao
                    false,//exibirObjetosPreco2
                    false,//exibirPanelGrupo2
                    true,//exibirObjetosGruposClientes
                    true,//exibirObjetosEncarte
                    true,//exibirGroupBoxValores
                    true,//exibirObjetosRegras
                    true,//exibir_gbAjustarQuebra
                    false,//exibir_gbxLimitePack
                    "Compre a partir de ",//"Compre a partir de "
                    " unidade(s) ",//"unidade(s) deste(s) produto(s) "
                    "Ganhe ",//"Receba"
                    "% de desconto por unidade(s).",//" % de desconto por unidade(s)."
                    0,
                    0,
                    CaixaTexto.TipoFormato.Inteiro,
                    CaixaTexto.TipoFormato.Moeda);
                return modelo;
            }
            else
            {
                return null;
            }

            #endregion
        }




        private Modelo ConstruirNasComprasAcimaDe50GanheDescontoNestesProdutos(ModeloPack.FormatoDoTxtValorRegra modeloPack)
        {
            #region Nas compras acima de R$ 50,00 ganhe desconto nestes produtos (R$ ou %)


            if (modeloPack == FormatoDoTxtValorRegra.MoedaComOpcional)
            {
                var ajuda = "Exemplo de promoção 1: " + Environment.NewLine;
                ajuda += "Compre R$ 50,00 em produtos e ganhe R$ 10,00 de desconto no produto x." + Environment.NewLine + Environment.NewLine;
                ajuda += "Exemplo de promoção 2: " + Environment.NewLine;
                ajuda += "Compre R$ 50,00 em produtos e ganhe R$ 10,00 de desconto nos produtos da lista/grupo/marca" + Environment.NewLine + Environment.NewLine;
                ajuda += "Observação: No exemplo acima as compras de R$ 50,00 não podem conter os produtos que irão ganhar desconto." + Environment.NewLine + Environment.NewLine;

                Modelo modelo = new Modelo("Nas compras acima de R$ 50,00 ganhe desconto nestes produtos (R$)",
                    TipoDePack.PackVirtual,
                    10,
                    FormatoDoTxtValorRegra.MoedaComOpcional,
                    "Leve RS x e receba desconto por unidade no produto x",
                    ajuda,//exemplosDePromocao
                    false,//exibirObjetosPreco2
                    false,//exibirPanelGrupo2
                    true,//exibirObjetosGruposClientes
                    true,//exibirObjetosEncarte
                    true,//exibirGroupBoxValores
                    true,//exibirObjetosRegras
                    false,//exibir_gbAjustarQuebra
                    false,//exibir_gbxLimitePack
                    "Compre R$",//"Compre R$ "
                    " na loja ",//" em mercadorias (fora deste pack) "
                    "e receba R$",//"Receba R$ "
                    " de desconto nestes produtos",//" de desconto por unidade nestes produtos"
                    0,
                    0,
                    CaixaTexto.TipoFormato.Inteiro,
                    CaixaTexto.TipoFormato.Moeda);
                return modelo;
            }
            else if (modeloPack == FormatoDoTxtValorRegra.PorcentagemComOpcional)
            {
                var ajuda = "Exemplo de promoção 1: " + Environment.NewLine;
                ajuda += "Compre R$ 50,00 em produtos e ganhe 10 % de desconto no produto x." + Environment.NewLine + Environment.NewLine;
                ajuda += "Exemplo de promoção 2: " + Environment.NewLine;
                ajuda += "Compre R$ 50,00 em produtos e ganhe 10 % de desconto nos produtos da lista/grupo/marca" + Environment.NewLine + Environment.NewLine;
                ajuda += "Observação: No exemplo acima as compras de R$ 50,00 não podem conter os produtos que irão ganhar desconto." + Environment.NewLine + Environment.NewLine;

                Modelo modelo = new Modelo("Nas compras acima de R$ 50,00 ganhe desconto nestes produtos (%)",
                    TipoDePack.PackVirtual,
                    11,
                    FormatoDoTxtValorRegra.PorcentagemComOpcional,
                    "Leve RS x e receba desconto percentual no produto x",
                    ajuda,//exemplosDePromocao
                    false,//exibirObjetosPreco2
                    false,//exibirPanelGrupo2
                    true,//exibirObjetosGruposClientes
                    true,//exibirObjetosEncarte
                    true,//exibirGroupBoxValores
                    true,//exibirObjetosRegras
                    false,//exibir_gbAjustarQuebra
                    false,//exibir_gbxLimitePack
                    "Compre R$ ",// "Compre R$ 
                    " na loja ",// " em mercadorias (fora deste pack) "
                    "e receba ",//"Receba "
                    "% de desconto nestes produtos",//"% de desconto nestes produtos"
                    0,
                    0,
                    CaixaTexto.TipoFormato.Inteiro,
                    CaixaTexto.TipoFormato.Moeda);
                return modelo;
            }
            else
            {
                return null;
            }

            #endregion

        }



        private Modelo ConstruirValorDiferenciadoPreco2()
        {
            #region Valor Diferenciado (Preço 2) KW

            var ajuda = "Opção para enviar um preço diferenciado de produto para a KW. " + Environment.NewLine;
            ajuda += "Após o cadastro será necessário o envio do mercador ou imediata.";

            Modelo modelo = new Modelo("Valor Diferenciado (Preço 2)",
                TipoDePack.PackVirtual,
                8,
                FormatoDoTxtValorRegra.Oculto,
                "Valor Diferenciado (Preço 2)",
                ajuda,//exemplosDePromocao
                true,//exibirObjetosPreco2
                false,//exibirPanelGrupo2
                false,//exibirObjetosGruposClientes
                false,//exibirObjetosEncarte
                false,//exibirGroupBoxValores
                false,//exibirObjetosRegras
                false,//exibir_gbAjustarQuebra
                false,//exibir_gbxLimitePack
                "Valor Diferenciado (Preço 2)",
                "",
                "",
                "",
                0,
                0,
                CaixaTexto.TipoFormato.Inteiro,
                CaixaTexto.TipoFormato.Moeda);

            return modelo;

            #endregion

        }




        private Modelo ConstruirApartirDeXGanheDescontoAtacado()
        {

            #region A partir de X unidades ganhe desconto %

            var ajuda = "Exemplo de promoção: " + Environment.NewLine;
            ajuda += "A partir de 12 latas de Refri ganhe 10% de desconto em cada unidade." + Environment.NewLine + Environment.NewLine;

            ajuda += "Observação: Cada produto informado na grade será contado individualmente por código de barras." + Environment.NewLine + Environment.NewLine;


            Modelo modelo = new Modelo("A partir de X unidades ganhe desconto (%)",
                TipoDePack.PackVirtual,
                13,
                FormatoDoTxtValorRegra.PorcentagemComOpcional,
                "",
                ajuda,//exemplosDePromocao
                false,//exibirObjetosPreco2
                false,//exibirPanelGrupo2
                true,//exibirObjetosGruposClientes
                true,//exibirObjetosEncarte
                true,//exibirGroupBoxValores
                true,//exibirObjetosRegras
                true,//exibir_gbAjustarQuebra
                false,//exibir_gbxLimitePack
                "A partir de ",//"Compre a partir de "
                " unidade(s)",//"unidade(s) de 1 destes produtos "
                " Ganhe ",//"Receba"
                "% de desconto por unidade(s).",//" % de desconto por unidade(s)."
                0,
                0,
                CaixaTexto.TipoFormato.Inteiro,
                CaixaTexto.TipoFormato.Moeda);
            return modelo;

            #endregion

        }




        private Modelo ConstruirApartirDeXGanheDescontoProdutosAssociadosAtacado()
        {
            #region A partir de X unidades ganhe desconto % (Produtos Associados)

            var ajuda = "Exemplo de promoção: " + Environment.NewLine;
            ajuda += "A partir de 6 refrescos (qualquer sabor) ganhe 10% de desconto em cada unidade." + Environment.NewLine + Environment.NewLine;
            ajuda += "Observação: Você pode colocar vários tipos ou sabores na grade, eles serão somados na contagem." + Environment.NewLine + Environment.NewLine;

            Modelo modelo = new Modelo("A partir de X unidades ganhe desconto (%) (Produtos Associados)",
                TipoDePack.PackVirtual,
                14,
                FormatoDoTxtValorRegra.PorcentagemComOpcional,
                "",
                ajuda,//exemplosDePromocao
                false,//exibirObjetosPreco2
                false,//exibirPanelGrupo2
                true,//exibirObjetosGruposClientes
                true,//exibirObjetosEncarte
                true,//exibirGroupBoxValores
                true,//exibirObjetosRegras
                true,//exibir_gbAjustarQuebra
                false,//exibir_gbxLimitePack
                "Leve ",//"Compre a partir de "
                "unidade(s) ",//"unidade(s) entre este(s) produto(s) "
                "Receba ",//"Receba"
                "% de desconto por unidade(s).",//" % de desconto por unidade."
                0,
                0,
                CaixaTexto.TipoFormato.Inteiro,
                CaixaTexto.TipoFormato.Moeda);

            return modelo;

            #endregion

        }

        private List<Modelo> RetornarListaComboParaPackVirtual(bool UsaKW) 
        {
            var modelos = new ModeloPack();

            List<Modelo> lista = new List<Modelo>();
            lista.Add(modelos.Leve3Pague2);
            lista.Add(modelos.Leve3Ganhe1);
            lista.Add(modelos.Leve3Ganhe1ProdutoDiferente);
            lista.Add(modelos.Leve3RecebaDescontoEmOutroProdutoDiferentePORCENTAGEM);
            lista.Add(modelos.Leve6PagueMenosEmCadaUnidPORCENTAGEM);
            lista.Add(modelos.ApartirDe6PagueMenosPORCENTAGEM);
            lista.Add(modelos.NasComprasAcimaDe50GanheDescontoNestesProdutosPORCENTAGEM);            
            if (UsaKW) { lista.Add(modelos.NasComprasAcimaDe50GanheDescontoNestesProdutosPORCENTAGEM); }

            return lista;
        }

        public List<Modelo> RetornarListaComboParaDescontoParaAtacado()
        {
            var modelos = new ModeloPack();

            List<Modelo> lista = new List<Modelo>();
            lista.Add(modelos.ApartirDeXGanheDescontoAtacado);
            lista.Add(modelos.ApartirDeXGanheDescontoProdutosAssociadosAtacado);

            return lista;
        }

        public List<Modelo> RetornarListaModelosDePack()
        {
            var modelos = new ModeloPack();

            List<Modelo> lista = new List<Modelo>();

            lista.Add(modelos.Leve3Pague2);
            lista.Add(modelos.Leve3Ganhe1);
            lista.Add(modelos.Leve3Ganhe1ProdutoDiferente);
            // ================
            lista.Add(modelos.Leve3RecebaDescontoEmOutroProdutoDiferenteMONETARIO);
            lista.Add(modelos.Leve3RecebaDescontoEmOutroProdutoDiferentePORCENTAGEM);
            // ================
            lista.Add(modelos.Leve6PagueMenosEmCadaUnidMONETARIO);
            lista.Add(modelos.Leve6PagueMenosEmCadaUnidPORCENTAGEM);
            // ================
            lista.Add(modelos.ApartirDe6PagueMenosMONETARIO);
            lista.Add(modelos.ApartirDe6PagueMenosPORCENTAGEM);
            // ================
            lista.Add(modelos.NasComprasAcimaDe50GanheDescontoNestesProdutosMONETARIO);
            lista.Add(modelos.NasComprasAcimaDe50GanheDescontoNestesProdutosPORCENTAGEM);
            // ================
            lista.Add(modelos.ValorDiferenciadoPreco2);
            lista.Add(modelos.ApartirDeXGanheDescontoAtacado);
            lista.Add(modelos.ApartirDeXGanheDescontoProdutosAssociadosAtacado);

            return lista;
        }

        public Modelo RetornarModeloPeloCodModeloPack(int codModeloPack)
        {
            foreach (Modelo modelo in RetornarListaModelosDePack())
            {
                if (modelo.CodModeloPack == codModeloPack)
                    return modelo;
            }
            return null;
        }

        public Modelo RetornarModeloPelaDescricao(string descricaoModelo)
        {
            foreach (Modelo modelo in RetornarListaModelosDePack())
            {
                if (modelo.DescricaoComercial == descricaoModelo)
                    return modelo;
            }
            return null;
        }


    }
}
