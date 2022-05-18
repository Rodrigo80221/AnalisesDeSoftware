### 1. ALTERAÇÃO DA CLASSE MAPA DE CALOR > Telecon.GestaoComercial.Biblioteca.Outros.MapaCalor

1. Criar o enum abaixo na classe que NÂO é sobrescrita pelo telecode

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


1. Criar o procedimento abaixo na classe que NÂO é sobrescrita pelo telecode

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



### 2. INCLUSÃO DO MAPA DE CALOR NO BOTÃO DO YOUTUBE (COLOCAR NO INÍCIO DO CÓDIGO DO BOTÃO)


``` csharp
            if (!CwbSite.Address.Contains("youtube"))
            {
                MapaCalor.RegistrarNoMapaDeCalor(Utilitarios.ObterConexao(), XXXXX, VariaveisGlobais.CodOperador, VariaveisGlobais.CodLoja);
            }		
```	    

			
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
