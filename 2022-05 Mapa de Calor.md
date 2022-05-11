## 1. ALTERAÇÃO DA CLASSE MAPA DE CALOR > Telecon.GestaoComercial.Biblioteca.Outros.MapaCalor

``` csharp
        public static bool RegistrarNoMapaDeCalor(IBanco banco, int codModuloCustomerSuccess, int codOperador, int codLoja)
        {
            try
            {
                banco.AbrirConexao();
                var mapaCalor = new MapaCalor
                {
                    CodLoja = codLoja,
                    CodOperador = codOperador,
                    DataHora = DateTime.Now,
                    CodModuloCustomerSuccess = codModuloCustomerSuccess
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
		
## 2. INCLUSÃO DO MAPA DE CALOR NO BOTÃO DO YOUTUBE (COLOCAR NO INÍCIO DO CÓDIGO DO BOTÃO)

`` csharp
            if (!CwbSite.Address.Contains("youtube"))
            {
                MapaCalor.RegistrarNoMapaDeCalor(Utilitarios.ObterConexao(), XXXXX, VariaveisGlobais.CodOperador, VariaveisGlobais.CodLoja);
            }		
```	    
			
## 3. ALTERAÇÃO DA CLASSE DE COMUNICAÇÃO JAVASCRIPT > GestaoComercial.Classes.ComunicacaoCwbSite

`` csharp
        public void registrarNoMapaDeCalor(int codModuloCustomerSuccess)
        {
            
            MapaCalor.RegistrarNoMapaDeCalor(Utilitarios.ObterConexao(), codModuloCustomerSuccess, VariaveisGlobais.CodOperador, VariaveisGlobais.CodLoja);
        }		
```	
		
## 4. SITE RD STATION
 	
	- EDIÇÃO AVANÇADA > JAVASCRIPT EM BODY

`` csharp
		<script type="text/javascript">
			
			document.getElementById("rd-button-kx0rm8t8").onclick = function () { ComunicacaoCwbSite.registrarNoMapaDeCalor(XXXXX); }

		</script>	
```		
