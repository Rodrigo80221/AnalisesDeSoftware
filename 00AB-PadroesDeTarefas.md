https://teleconsistemas.movidesk.com/kb/article/236515/ordenacao-nos-relatorios-genericos
Ordenação dos relatórios genéricos



## Tarefa X: Criar branch no git
Criar novo branch feature/EpicoDescontosParaAtacado


## Tarefa X: Criar módulo para Gerenciar o recurso XXXXXXXXXXX

1. Criar verifica banco para inserir módulo. Se o cliente não utiliza PDV o módulo não deve ser verificado. 

Utilizar:
`fVerificarSeUsaPDV()`
`sInserirModulo`
`sInserirModuloSistemasRS`

Executar update em operadores_modulos para ativar o módulo

Descrição das variáveis:

`sGrupo = (PDV,PEDIDOS,FINANCEIRO,RELATÓRIOS,NOTAS,CLIENTES,FORNECEDORES,PRODUTOS)`
`sDescrição = (Nome da Tela)`
`sPalavraChave = (Nome do Form)`

1. Tratar em mdlGestao.sCarregaModulos

    1.1 Chamar na tela clássica no menu de  PDV > Ocorrências com Supervisor

1. Tratar no frmModulos.cmdRestaurar_Click conforme padrão do procedimento


arquivo de categorias
https://docs.google.com/document/d/17U5pcJoUfe2-p-8dTkpBf5nLfqJjwwRKo9rcM5rgYxg/edit


## Tarefa X: Criar Verifica Banco

>Observações:
>Utilizar o Funcoes.fExisteObjeto 
>Utilizar o Funcoes.fExisteObjeto e excluir objeto caso já tenha sido criado
fExisteCampo("PedidoCompra", "Vendedor")
fExisteCampo("PedidoCompra", "VendedorFone")
fExisteCampo("PedidoCompra", "VendedorMail")
Cuidar time out da conexão, retornar time out no trata erro
Fechar recordset no trata erro
Reabilitar triggers e constraints no trata erro
Funcoes.fAtivarSingleUser goConexao, True


## Tarefa X: Adicionar combo

_KeyPress
KeyAscii = Funcoes.fBuscaCombo(cboFormaPgto, KeyAscii)


## Tarefa X: Criar novo form

Criar form XXXX
No novo form implementar as configurações padrões da Telecon
1. Fechar com esc
1. Enter como tab

        Propriedade do form: keyPreview = True

            Private Sub Form_KeyDown(KeyCode As Integer, Shift As Integer)
                On Error GoTo TrataErro

                Select Case KeyCode
                Case vbKeyReturn
                    If Funcoes.fControleAtivo(Me) <> "grdGrid.name" Then SendKeys "{TAB}"
                Case vbKeyEscape
                    If dtpFinal.Enabled = False Then Exit Sub
                    Unload Me
                Case vbKeyF1
                    KeyCode = 0
                End Select

                Exit Sub
            TrataErro:
                Funcoes.sLogErros App, "frmControleEntradasAtacado", Err, "Form_KeyDown", Erl, True
            End Sub

1. Ícone na janela
1. Abrir com vbmodal
1. Não deve ter os botões de maximizar
1. Não deve permitir resize na tela
        Propriedade do form: BorderStyle: 1 - Fixed Single

1. O Fechar/Sair cancela a operação, caso alterando na barra de cadastros, mostrar mensagem de aviso.

1. Deve abrir centralizado. 
        Propriedade do form: StartUpPosition = 2 - CenterScreen

1. Deve estar correto quanto ao tab index ao finalizar a tela
1. F4 abre a busca 
-- evento activeText_KeyDown
    If KeyCode = vbKeyF4 Then
        cmdBuscaGrupo_Click
    End If        
1. Implementar tratamentos da ampulheta do mouse no click dos botões de lupa, gravar, excluir, alterar e atualiza tela.




# Finalização/ Testes de Integração
1. Verificar tratamentos da ampulheta do mouse, deve ficar em espera em consultas e procedimentos demorados, deverá retornar para a seta após o procedimento. Em caso de erro deverá retornar também para o padrão no trataerro ou try catch

1. Bloquear campos de consulta/pesquisa em consultas e procedimentos demorados, deverá desbloquear após o procedimento finalizar. Em caso de erro deverá retornar também para o padrão no trataerro ou try catch

1. Procedimentos demorados no C# devem ser executados em threads



## Tarefa X: Criar formulário de busca
1. Adicionar Formulário no namespace `GestaoComercial.Formularios.`
1. Adicionar herança do formulário `FrmPesquisa`. Este formulário deverá seguir o padrão dos formulários de pesquisa da Telecon
1. Implementar o `AdicionaColunasList` , (acertar a largura)
``` c#
        base.AdicionarColunaLista("Código", 50, typeof(string));
        base.AdicionarColunaLista("Razao Social", 200, typeof(string));
        base.AdicionarColunaLista("Fantasia", 190, typeof(string));
        base.AdicionarColunaLista("CNPJ", 130, typeof(string));
```
1. Implementar critérios
=> Campo + Valor no Combo
``` c#
        base.AdicionarCriterio(typeof(int), "Codigo"; "Código");
        base.AdicionarCriterio(typeof(string), "Nome", "Razao Social");
        base.AdicionarCriterio(typeof(string), "Fantasia", "Fantasia");
        base.AdicionarCriterio(typeof(string), "CNPJ", "CNPJ");
```
1. Implementar Ordenação
``` c#
        base.AdicionarOrdenacao(typeof(int), "Codigo", "Código");
        base.AdicionarOrdenacao(typeof(string), "Nome", "Razao Social");
        base.AdicionarOrdenacao(typeof(string), "Fantasia", "Fantasia");
        base.AdicionarOrdenacao(typeof(string), "CNPJ", "CNPJ");
```
1. Implementar o `CarregaStringSql`
* utilizar a classe `Nome da Classe da tabela`
* tratar o cursor do mouse durante a pesquisa (trocar seta por ampulheta)



## Tarefa X: Recordset Desconectado

>Nesta tarefa criaremos a funcionalidade xyz, mas por motivos de performance não realizaremos mais uma consulta no banco honerando a tela, iremos utilizar um recordset desconectado

1. Nas declarações do FrmPedidoCompra criar a variável abaixo
``` vb
Private lrsConsultaFfornecedorVendedoresProdutos As ADODB.Recordset
```
1. Criar o procedimento `fCarregarRecordAtacado` para carregar os dados da tabela `FornecedorVendedoresProdrodutos` na memória do cliente (recordset lrsConsultaFfornecedorVendedoresProdutos) de acordo com as regras abaixo:
* Realizar consulta na tabela `FornecedorVendedoresProdrodutos` de acordo com o vendedor e o fornecedor
* Criar o procedimento no mesmo padrão do procedimento `frmControleEntradas3.fCarregarRecordAtacado`



## Tarefa X: Testes em alteração de preço



Teste alteração de carga ONLINE

Produto 18564 - valor de venda inicial R$ 2,69

Lançamento de nf entrada => custo = R$ 3,00 (OK)
alteração de preço pelo controle de entradas do produto 18564 para R$ 3,50 (OK)
verificar comandos pdv (OK)
verificar tabela de produtos (OK)
verifica tabela de produtoLojas (OK)
verifica tabela de preços alterados(OK)
realizar exportação (OK)
verificar preço no gestão e pdv (OK)


online + produto com a conf. (Vincular Preço de Venda a % Total dos itens)

Produto 18575- valor de venda inicial R$ 4,99
Produto 42141 (gelado)- valor de venda inicial R$ 5,19

Lançamento de nf entrada do produto 18575 => custo = R$ 5,00 (OK)
alteração de preço pelo controle de entradas do produto 18575 para R$ 6,50 (OK)
alteração de preço automática do produto 42141 (ok foi para 6,70)

verificar comandos pdv  (OK)
verificar tabela de produtos  (OK)
verifica tabela de produtoLojas  (OK)
verifica tabela de preços alterados(ERRO - VALOR_ANTERIOR do produto gelado)
realizar exportação  (OK)
verificar preço no gestão e pdv  (OK)

online + promoção 

Produto 40224 - valor de venda inicial R$ 3,80

Lançamento de nf entrada => custo = R$ 3,50 
criar promoção para o produto => R$ 3,59
alteração de preço pelo controle de entradas do produto 18564 para R$ 4,00
verificar comandos pdv   (OK)
verificar tabela de produtos   (OK)
verifica tabela de produtoLojas   (OK)
realizar exportação    (OK)
verificar preço no gestão e pdv   (OK)
Finalizar promoção    (OK)
verificar comandos pdv      (OK)
verificar tabela de produtos      (OK)
verifica tabela de produtoLojas    (OK) 
realizar exportação       (OK)
verificar preço no gestão e pdv     (OK) 

online + produtos associados 

Produto 12810- valor de venda inicial R$ 3,89
Produto 21341- valor de venda inicial R$ 3,89
Produto 5888- valor de venda inicial R$ 3,89


Lançamento de nf entrada produto 33251=> custo = R$ 2,92 (OK) 
alteração de preço pelo controle de entradas do produto 26744 para R$ 4,00 (OK)
verificar comandos pdv -todos produtos  (OK)
verificar tabela de produtos -todos produtos  (OK)
verifica tabela de produtoLojas -todos produtos  (OK)
verifica tabela de preços alterados -todos produtos  (OK)
exportação  (OK)
verificar preço no gestão e pdv















Teste alteração de carga OFFLINE

Produto 18564 - valor de venda inicial R$ 3,50

Lançamento de nf entrada => custo = R$ 3,50 
alteração de preço pelo controle de entradas do produto 18564 para R$ 4,00 
verificar comandos pdv (OK)
verificar tabela de produtos (OK) 
verifica tabela de produtoLojas (OK) 
verifica tabela de preços alterados (OK)
Enviar Carga para os PDVS (OK)
verificar comandos pdv (OK)
verificar tabela de produtos (OK) 
verifica tabela de produtoLojas (OK) 
verifica tabela de preços alterados (OK)
realizar exportação  (OK)
verificar preço no gestão e pdv   (OK)


OFFLINE + produto com a conf. (Vincular Preço de Venda a % Total dos itens)

Produto 18575- valor de venda inicial R$ 6,50
Produto 42141 (gelado)- valor de venda inicial R$ 6,70

Lançamento de nf entrada do produto 18575 => custo = R$ 5,50 
alteração de preço pelo controle de entradas do produto 18575 para R$ 7,00 
alteração de preço automática do produto 42141 (ok foi para 6,70)

verificar comandos pdv   (OK)
verificar tabela de produtos   (OK)
verifica tabela de produtoLojas  (OK) 
verifica tabela de preços alterados (OK)

Enviar Carga para os PDVS (OK)
verificar comandos pdv (OK)
verificar tabela de produtos (OK) 
verifica tabela de produtoLojas (OK) 
verifica tabela de preços alterados (OK)

realizar exportação   (OK)
verificar preço no gestão e pdv   (OK)

OFFLINE + promoção 

Produto 40224 - valor de venda inicial R$ 5,00

Lançamento de nf entrada => custo = R$ 4,50 
criar promoção para o produto => R$ 4,00

Enviar carga para o pdv  (OK)

alteração de preço pelo controle de entradas do produto 40224 para R$ 6,00

Enviar carga para o pdv  (OK)

verificar comandos pdv   (OK)  
verificar tabela de produtos     (OK)
verifica tabela de produtoLojas  (OK)
   
realizar exportação  (OK)    

verificar preço no gestão e pdv     (OK) 

Finalizar promoção (OK)    

verificar comandos pdv (OK)      
verificar tabela de produtos (OK)      
verifica tabela de produtoLojas (OK)     
      

Enviar carga para o pdv  (OK)

verificar comandos pdv (OK)      
verificar tabela de produtos (OK)      
verifica tabela de produtoLojas (OK) 

realizar exportação        (OK) 
verificar preço no gestão e pdv (OK) 



OFFLINE + produtos associados 

Produto 12810- valor de venda inicial R$ 4,00
Produto 21341- valor de venda inicial R$ 4,00
Produto 5888- valor de venda inicial R$ 4,00


Lançamento de nf entrada produto 12810 custo = R$ 3,5 

alteração de preço pelo controle de entradas do produto 12810 para R$ 5,50 

verificar comandos pdv -todos produtos (OK)  
verificar tabela de produtos -todos produtos (OK)  
verifica tabela de produtoLojas -todos produtos (OK)  
verifica tabela de preços alterados -todos produtos  (OK) 

enviar carga para os pdvs  (OK)  

verificar comandos pdv -todos produtos  (OK)   
verificar tabela de produtos -todos produtos  (OK)   
verifica tabela de produtoLojas -todos produtos  (OK)   
verifica tabela de preços alterados -todos produtos  (OK)   

exportação  
verificar preço no gestão e pdv


![image](https://user-images.githubusercontent.com/80394522/157093472-b351595b-a303-4b50-a3fe-9e54e2982c8c.png)
















                // aqui criar mensagem

                if (ex.Message.Contains("Could not load file or assembly 'CefSharp.Core.Runtime.dll' or one of its dependencies. The specified module could not be found."))
                {
                   if (Msg.Perguntar("Não é possível abrir o Sistema S" + Environment.NewLine + Environment.NewLine
                        + "O pacote de bibliotecas de runtime do Microsoft C e C++ está desatualizado" + Environment.NewLine + Environment.NewLine
                        + "Deseja instalar o pacote de atualizações?") == DialogResult.Yes)
                    {
                        string url = "http://aka.ms/vs/17/release/vc_redist.x86.exe";

                        string caminho = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + @"\Telecon_Sistemas" + @"\vc_redist.x86.exe.jpg";
                        
                       
                       Internet.BaixarArquivo(url, caminho);                                               
                    }
                }











        private void DownloadFrame()
        {
        
            using (WebClient webClient = new WebClient())
            {
                webClient.DownloadProgressChanged += new DownloadProgressChangedEventHandler(delegate (object sender, DownloadProgressChangedEventArgs e)
                {
                    Console.WriteLine("Downloaded:" + e.ProgressPercentage.ToString());
                    //Msg.Informar(e.ProgressPercentage.ToString());
                });

                webClient.DownloadFileCompleted += new System.ComponentModel.AsyncCompletedEventHandler
                    (delegate (object sender, System.ComponentModel.AsyncCompletedEventArgs e)
                    {
                        if (e.Error == null && !e.Cancelled)
                        {
                            Console.WriteLine("Download completed!");
                            //Msg.Informar("Download do arquivo concluído!");
                        }
                    });
                webClient.DownloadFileAsync(new Uri("https://ninite.com/.net4.8/ninite.exe"), @"C:\Telecon_Sistemas\ninite.exe");
            }

        }











               private void AtualizarFrame()
        {
            Process processo = new Process();
            processo.StartInfo.FileName = @"C:\Telecon_Sistemas\ninite.exe";
            bool existeArquivo = File.Exists(@"C:\Telecon_Sistemas\ninite.exe");
        
            if (existeArquivo)
            {
    
               processo.StartInfo.RedirectStandardOutput = true;
               processo.StartInfo.UseShellExecute = false;
               processo.StartInfo.CreateNoWindow = true;

               processo.Start();
               processo.WaitForExit();
                          
               // Reiniciar o micro
               var pergunta = Msg.PerguntarPadraoNao("Reinicie o Computador para que a atualização seja concluída. \n\nDeseja reiniciar agora?");

               if (pergunta == DialogResult.Yes)
               {
                    ReiniciarMicro();
               }

            }
            else
            {
                Msg.Informar("Arquivo (.exe) não localizado!");
                return;
            }
        }
