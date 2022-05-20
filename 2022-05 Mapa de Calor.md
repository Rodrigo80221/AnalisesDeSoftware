Sistema S - Base de Conhecimento (testar os 3 botões - base de conhecimento - hitorico de versoes - mural)

Sistema S - Portal do Cliente (Não precisa)

Sistema S - YouTube 

Sistema S - Processos (Fazer separado por processo - CONFIRMAR)

Sistema S - Menu e Busca de Formulários - colocar no clique da ferramenta

Sistema S - Setor geral - (colocar no clique da ferramenta - CONFIRMAR)

Sistema S - Tela Clássica 

--------------------------------------------------------------------------------------

Sistema S - LP Compras

Sistema S - LP Estoques

Sistema S - LP Retaguarda

Sistema S - LP Loja

Sistema S - LP Caixas

Sistema S - LP Açougue

Sistema S - LP Padaria

Sistema S - LP Hortifruti

Sistema S - LP T.I

Sistema S - LP Financeiro

Sistema S - LP Fiscal

Sistema S - LP Marketing

Sistema S - LP Jurídico

Sistema S - LP R.H.

Sistema S - LP Indicadores

--------------------------------------------------------------------------------------

Sistema S - LP Compras Artigo AAAA

Sistema S - LP Compras Artigo BBBB

--------------------------------------------------------------------------------------
--------------------------------------------------------------------------------------
--------------------------------------------------------------------------------------
--------------------------------------------------------------------------------------
--------------------------------------------------------------------------------------
--------------------------------------------------------------------------------------
--------------------------------------------------------------------------------------
--------------------------------------------------------------------------------------
--------------------------------------------------------------------------------------

### 1. ALTERAÇÃO DA CLASSE MAPA DE CALOR > Telecon.GestaoComercial.Biblioteca.Outros.MapaCalor

1.1. Criar o enum abaixo na classe que NÂO é sobrescrita pelo telecode

Descrição: Criar código para as próximas tarefas

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
	    	    	    	    
     	    BaseDeConhecimento = 31,
	    BC_BaseDeConhecimento = 32,
	    BC_HistoricoDeVersoes = 33,
	    BC_MuralDeIdeias = 34,
	    BC_BlogGrupoTelecon = 35
	    
	    
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

Descrição: Ao clicar nos botões abaixo registrar um registro na tabela MapaCalor no banco de dados. Após estar rodando nos clientes essa tabela é enviada para o sistema Customer Success da Telecon gerando informações gerenciais dos clientes.
O importante neste momento é apenas gerar a informação de registro corretamente na tabela do banco de dados, ou seja, se clicarmos no ícone do Youtube gerar um log na tabela. Devemos gerar esse log nas funcionalidades abaixo

Botões das Landing Pages (sininhos da tela do sistema S ou Botão Notificações na tela de cada setor)
Botão movidesk
botão youtube
botao chamado  movidesk

2.1. Alterar o procedimento FrmPrincipal.CarregarNavegador adicionando o parâmetro MapaCalor.CodCustomerSuccess codCustomerSuccess

	- Neste procedimento após o código `_estadoBrowser = _estadoChromiumBrowser.Normal;` add o código abaixo
	
``` csharp

	MapaCalor.RegistrarNoMapaDeCalor(Utilitarios.ObterConexao(), codCustomerSuccess, VariaveisGlobais.CodOperador, VariaveisGlobais.CodLoja);	

```	

2.2. Criar na classe MapaCalor o procedimento `RetornarCodCustomerSucessDaLPDoSetor` que receba um parâmetro do tipo `Setor.Setores` e retorne um `MapaCalor.CodCustomerSuccess`. Realizar os cruzamentos manualmente. Ex. Entra Setores.Compras e retorna CodCustomerSuccess.SistemaS_LPCompras

	- Alterar o procedimento CarregarLandingPage para utilizar o procedimento `RetornarCodCustomerSucessDaLPDoSetor` como parametro na chamada do procedimento `CarregarNavegador`
	
2.3. Adicionar o parâmetro criados em todas chamadas abaixo do procedimento CarregarLandingPage

	CarregarLinkTelaProcessos - CodCustomerSuccess.SistemaS_Processos
	btnMoviDesk_Click - CodCustomerSuccess.SistemaS_BaseDeConhecimento
	btnYouTube_Click - CodCustomerSuccess.SistemaS_YouTube
	btnChamadoMoviDesk_Click - CodCustomerSuccess.SistemaS_PortalDoCliente


3. Testar os botões abaixo para certificar que está criando corretamente o log na table MapaCalor, cuidar para ver se gerou apenas 1 vez ao abrir e fechar cada recurso.
Botões das Landing Pages (sininhos da tela do sistema S ou Botão Notificações na tela de cada setor)
Botão movidesk
botão youtube
botao chamado  movidesk

			
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
