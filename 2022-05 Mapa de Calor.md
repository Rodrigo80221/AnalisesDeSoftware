### 1. ALTERAÇÃO DA CLASSE MAPA DE CALOR > Telecon.GestaoComercial.Biblioteca.Outros.MapaCalor

1.1. Criar o enum abaixo na classe que NÂO é sobrescrita pelo telecode

``` csharp
        public enum CodCustomerSuccess
        {
            AjusteDeSaldo_ContasDeMovimentacao = 1,
            FluxoDeCaixa = 2,
            DRE_Gerencial = 3,
            SaldoDeCaixaDosPDVs = 4,
            PedidoDeCompra = 5,
            PackVirtual = 6,
            ProdutosSemMovimentacao = 7,
            Excluido = 8,
            ControleDeEntradas = 9,
            DiferencaDeCompraEVenda = 10,
            AbcDeMercadorias = 11,
            ApuracaoDeImpostos = 12,
            AvaliacaoDoEstoque = 13,
            PlanejamentoDeCompraEVenda = 14,
            RelAnaliseDeComportamentoDaLoja = 15,
            RelAnaliseDeVendaConjunta = 16,
            RelAnaliseMensalDeVendas = 17,
            RelVendasPorHora = 18,
            RelatorioDeMapaDeReposicao = 19,
            RelatorioDeVendasPorPeriodo = 20,
            ClientesCadastradosOnline = 21,
            Boletos = 22,
            RH = 23,
            AppVendasExternas = 24,
            AppColetorAndroid = 25,
            SalaDeComando = 26,
            SimuladorDePrecos = 27,
            TelaClassica = 28,
            NotasFiscaisComErros = 29
        }
```


1.2. Criar o procedimento abaixo na classe que NÂO é sobrescrita pelo telecode

``` csharp
        public static bool RegistrarNoMapaDeCalor(IBanco banco, CodCustomerSuccess codModuloCustomerSuccess, int codOperador, int codLoja)
        {
            try
            {
                banco.AbrirConexao();
                var mapaCalor = new MapaCalor
                {
                    CodLoja = codLoja,
                    CodOperador = codOperador,
                    DataHora = DateTime.Now,
                    CodModuloCustomerSuccess = (int)codModuloCustomerSuccess
                };

                return MapaCalor.Inserir(banco, mapaCalor);

            }
            catch (Exception)
            {
                return false;
            }
            finally
            {
                if (banco != null) banco.FecharConexao();
            }

        }		
```



### 2. INCLUSÃO DO MAPA DE CALOR NOS BOTÕES DO SISTEMA S (COLOCAR NO INÍCIO DO CÓDIGO DE CADA BOTÃO)

2.1. Alterar o procedimento FrmPrincipal.CarregarNavegador adicionando o parâmetro MapaCalor.CodCustomerSuccess codCustomerSuccess
	- Neste procedimento após o código `_estadoBrowser = _estadoChromiumBrowser.Normal;` add o código abaixo
	``` csharp
	MapaCalor.RegistrarNoMapaDeCalor(Utilitarios.ObterConexao(), codCustomerSuccess, VariaveisGlobais.CodOperador, VariaveisGlobais.CodLoja);	
	```	

2.2. Criar na classe MapaCalor o procedimento RetornarCodCustomerSucessDaLPDoSetor que receba um parâmetro do tipo `Setor.Setores` e retorne um `MapaCalor.CodCustomerSuccess`. Realizar os cruzamentos manualmente. Ex. Entra Setores.Compras e retorna CodCustomerSuccess.SistemaS_LPCompras

	- Alterar o procedimento CarregarLandingPage para inserir no mapa de calor utilizando MapaCalor.RegistrarNoMapaDeCalor. inserir o código dentro do Invoke.
	- Testar nos alertas do sistema S (sininhos) e também no botão 












    

2.2. Utilizar a mesma lógica para os botões abaixo

Sistema S - Base de Conhecimento
Sistema S - Portal do Cliente

2.3. No botão de menu alterar o procedimento AlterarEstadoMenuTelaClassica colocando o código abaixo no else

``` csharp
 MapaCalor.RegistrarNoMapaDeCalor(Utilitarios.ObterConexao(), CodCustomerSuccess.SistemaS_MenuEBuscaDeFormularios, VariaveisGlobais.CodOperador, VariaveisGlobais.CodLoja);
```




Sistema S - Processos ???

			
### 3. ALTERAÇÃO DA CLASSE DE COMUNICAÇÃO JAVASCRIPT > GestaoComercial.Classes.ComunicacaoCwbSite


``` csharp
        public void registrarNoMapaDeCalor(int codModuloCustomerSuccess)
        {
            
            MapaCalor.RegistrarNoMapaDeCalor(Utilitarios.ObterConexao(), codModuloCustomerSuccess, VariaveisGlobais.CodOperador, VariaveisGlobais.CodLoja);
        }		
```	
		
### 4. SITE RD STATION
 	
	- EDIÇÃO AVANÇADA > JAVASCRIPT EM BODY

``` javascript
		<script type="text/javascript">
			
			document.getElementById("rd-button-kx0rm8t8").onclick = function () { ComunicacaoCwbSite.registrarNoMapaDeCalor(XXXXX); }

		</script>	
```		
