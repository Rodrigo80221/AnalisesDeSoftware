# Épico: Melhorias no Pack Virtual
Data de início da análise: Abril de 2021

## Problema a ser resolvido
1. Com a criação da tela Desconto para Atacado não faz mais sentido ter packs de atacado na tela do Pack Virtual
1. Nos modelos da tela Pack Virtual temos descrições citando `1 centavo`, texto necessário devidos as impressoras fiscais que cairam em desuso. 
1. Corrigir descrições do help de modelos novos criados. 
1. Adicionar informações de pack virtual e modelo de desconto no cadastro de produtos. 
1. Corrigir mensagens em diversos locais do sistema incluindo informações sobre o módulo Desconto para Atacado
1. Criar o modelo de desconto para produtos associados na tela Descontos Para Atacado
1. Criar relatórios de Pack Virtual e Desconto para atacado

## Impactos
1. Módulo Descontos Para Atacado
1. Módulo Pack Virtual
1. Cadastro de Produtos 

## Pré Requisitos
O épico Descontos Para Atacado deverá estar concluído

## Solução
1. A tela será refatorada para possibilitar a alteração das descrições dos packs, criação de relatórios, criação da aba _Relacionar Etiquetas_ na central de impressão e criação de novos tipos promocionais no futuro. Para isso criada a classe ModeloPack que terá todas as informações de cada pack virtual ou modelo de desconto.

1. O modelos de pack terão a descrição alterada para facilitar a identificação das promoções
    ![](https://github.com/Rodrigo80221/MARKDOWN/blob/main/Imagens/ComboBoxPackVirtual.jpg?raw=true)
    * Será adicionado o radio button `Porcentagem (%)`/ `Monetário (R$)`
    ![](https://github.com/Rodrigo80221/MARKDOWN/blob/main/Imagens/PackVirtual_RadioButton.jpg?raw=true)

1. O help será alterado melhorando os exemplos e observações. 

1. Será inserido o combobox "Modelos de Atacado" no cadastro de produtos para facilitar inserir um produto em um desconto para atacado ou altera-lo. 

    ![](https://github.com/Rodrigo80221/AnalisesDeSoftware/blob/main/Imagens/FrmCadProdutos_FramePromocao.jpg?raw=true)
    *As validações no combo serão as mesmas que estão na tela Desconto Para atacado

1. Será criado o novo modelo de desconto para atacado `A partir de X unidades ganhe desconto (%) (Produtos Associados)`

## Tarefa 1: Criar registros para testes e criar classes C#
1. Ativar o módulo da KW, cadastrar um pack de cada modelo, anotar todos, esses dados serão utilizados na última tarefa.
1. Adicionar no projeto `Telecon.GestaoComercial.Biblioteca.PackVirtual` os arquivos 
[Modelo.cs](https://github.com/Rodrigo80221/MARKDOWN/blob/main/Classes/Modelo.cs)
[ModelosPack.cs](https://github.com/Rodrigo80221/MARKDOWN/blob/main/Classes/ModelosPack.cs)
[TipoDePack.cs](https://github.com/Rodrigo80221/MARKDOWN/blob/main/Classes/TipoDePack.cs)

1. criar a propriedade local no formulário FrmPackVirtual
``` c#
private Telecon.GestaoComercial.Biblioteca.PackVirtual.ModeloPack _modelos = new Pack.ModeloPack();
``` 
## Tarefa 2: Atualizar o CarregarDgvGrupo1 utilizando a nova classe
1. No CarregarDgvGrupo1 trocar o texto no 1º if para utilizar a propriedade _modelo 
trocar
`"Pague x porcento a menos a partir de x unidades (atacado)"`
por 
`_modelos.ApartirDe6PagueMenosPORCENTAGEM.DescricaoComercial`
Utilizar essa estrutura nas próximas validações, mudando somente o modelo de pack, se ficar com dúvidas na classe ModeloPack temos a propriedade DescricaoAntiga onde temos a antiga descrição que era exibida no combo.
1.  No CarregarDgvGrupo1 trocar o 2º if para utilizar a propriedade _modelo.xxx.DescricaoComercial 
1.  No CarregarDgvGrupo1 trocar o 3º if para utilizar a propriedade _modelo.xxx.DescricaoComercial 

## Tarefa 3: Atualizar o CarregarComboModeloPack utilizando a nova classe
1. Carregar o combo atravez do list _modelos.RetornarListaComboParaPackVirtual 
utilizar o código abaixo para carregar os modelos no combo
``` C#
    comboBox1.Items.Add("Selecione um exemplo de promoção");
    foreach (ModeloPack.Modelo modelo in _modelos.RetornarListaComboParaPackVirtual(VerModuloKW))
    {
        comboBox1.Items.Add(modelo);
    }
```
2. No combo da primeira aba a primeira opção é `Todos`
3. No combo da Segunda aba a primeira opção é `Selecione um exemplo de promoção` 
4. Alterar o caption do lblModeloDoPack para `Exemplos de Pack` 
* Deixar como na imagem abaixo
![](https://github.com/Rodrigo80221/MARKDOWN/blob/main/Imagens/ComboBoxPackVirtual.jpg?raw=true)
1. Adicionar um option button para selecionar Porcentagem ou Valor Monetário
* Deixar como na imagem abaixo
![](https://github.com/Rodrigo80221/MARKDOWN/blob/main/Imagens/PackVirtual_RadioButton.jpg?raw=true)
1. acertar o tabindex dos componentes

## Tarefa 4: Substituir o switch case do procedimento cboModeloPack_SelectedIndexChanged

1. Criar código para substituir o switch case. Buscar o modelo utilizando `_modelo.RetornarModeloPelaDescricao`, setar todos os procedimentos e objetos utilizando as propriedades do objeto modelo.
    * Todo swith deverá ser substituindo ficando semelhante ao exemplo abaixo
    ```  
        var modelo = _modelo.RetornarModeloPelaDescricao(text_combobox)            

        exibirObjetosPreco2 (modelo.ExibirObjetosPreco2)
        exibirPanelGrupo2 (modelo.ExibirPanelGrupo2)
        exibirObjetosGruposClientes (modelo.ExibirObjetosGruposClientes)
        exibirObjetosEncarte (modelo.ExibirObjetosEncarte)
        exibirGroupBoxValores (modelo.ExibirGroupBoxValores)
        exibirObjetosRegras (modelo.ExibirObjetosRegras)
        exibir_gbAjustarQuebra (modelo.Exibir_gbAjustarQuebra)
        exibir_gbxLimitePack (modelo.Exibir_gbxLimitePack)       
    ```
1. O txtQtdRegra e o txtValorRegra deverão receber a formatação também de acordo com a propriedade
`modelo.Formato_txtQtdRegra` e `modelo.Formato_txtValorRegra` 

1. Temos a propriedade modelo.observação que teremos que fazer algumas alterações
    * remover o lblProdutoGratis e adicionar o lblObservacao com a fonte no padrão normal.
    * colocar após os objetos de descrição conforme a imagem abaixo
    ![](https://github.com/Rodrigo80221/MARKDOWN/blob/main/Imagens/PackVirtual_lblProdutoGratis2.jpg?raw=true)
    * Configurar para o texto nele poder ficar em duas linhas
    * se a propriedade modelo.observação retornar algum text deixar o lblObservação visível e adicionar o texto da propriedade, do contrário deixar invisível.

1. Além dos controles acima implementar os radion buttons no controle da tela 
    * O radio button terá o comportamento dependendo do enum FormatoDoTxtValorRegra
        * Caso Oculto,UnitárioSemOpcional ou MoedaSemOpcional os 2 radion buttons deverão ficar invisíveis
        * Caso MoedaComOpcional ou PorcentagemComOpcional deixar os 2 radion buttons deverão ficar visíveis
        * Caso MoedaComOpcional marcar o opcional `Monetário (R$)`
        * Caso PorcentagemComOpcional marcar o opcional `Porcentagem (%)`    
        * criar um procedimento com essa regra
                    
1. Recriar o procedimento de posicionar os labels e os texts. tendo como base apenas o primeiro label, depois alterar a propriedade `.left` dos outros labels e texts para ficarem alinhados. Criar o pocedimento PosicionarObjetosDeRegras, ele deve ser chamado após o `DefinirValoresLabels` programar para poder substuir o procedimento PosicionarLayoutLabel e o PosicionarLayoutTextBox. Os procedimentos atuais além de difícil manutenção não estão funcionando muito bem. E as descrições irão mudar totalmente.
    * Inserir o lblObservacao na programação do DefinirValoresLabels, para ele ficar após todos os labels como no exemplo abaixo

    ![](https://github.com/Rodrigo80221/MARKDOWN/blob/main/Imagens/PackVirtual_lblProdutoGratis2.jpg?raw=true)

1. Os procedimentos de layout excluídos são utilizados na tela FrmDescontoAtacado, atualizar lá também.

## Tarefa 5: Implementar o radio button Monetário/ Porcentagem

1. Ao alterar o radio button alterar a descrição entre (%) ou (R$)
exemplo: 
Se for selecionado no combo 
`A partir de 6 pague menos (%)`
ao mudar o radion buton para monetário (R$) substituir na string `(%)` por `(R$)

## Tarefa 6: Acertar txtValorRegra_Leave
1. No txtValorRegra_Leave acertar o if para utilizar a propriedade _modelo.xxx.DescricaoComercial

## Tarefa 7: alterar o procedimento RetornarPackVirtualSimplificado
1. Remover o Swith e no lugar chamar o `_modelo.RetornarModeloPeloCodPack`

## Tarefa 8: Acertar dgvGrupo1_CellMouseClick
1. No dgvGrupo1_CellMouseClick acertar o if para utilizar a propriedade _modelo.xxx.DescricaoComercial

## Tarefa 9: Acertar dgvGrupo1_CellMouseClick
1. No TestarCampos acertar os ifs para utilizar a propriedade _modelo.xxx.DescricaoComercial

## Tarefa 10: Acertar RetornarPackVirtual
1. substituir o código abaixo 
```c#
    if (cboModeloPack.SelectedIndex >= 8 && !_moduloKw)
    {
        packVirtual.ModeloPack = cboModeloPack.SelectedIndex + 1; //Devido ao pack do Tischler visível apenas para eles
    }
    else
    {
        packVirtual.ModeloPack = cboModeloPack.SelectedIndex;
    }
```
por 
``` C#
 packVirtual.ModeloPack = _modelo.RetornarModeloPelaDescricao(descricaoQueEstaNocombo).codModeloPack`
 ```
1. Acertar o último if para utilizar a propriedade _modelo.xxx.DescricaoComercial

## Tarefa 11: Acertar o btnSalvar_Click
1. No btnSalvar_Click acertar os ifs para utilizar a propriedade _modelo.xxx.DescricaoComercial
## Tarefa 12: Acertar o ptbQuestao_Click
1. No ptbQuestao_Click remover o switch case e exibir a propriedade _modelos.xxx.ExemplosDePromocao. Realizar um foreach assim como nos métodos do ModelosPack, buscando em todos da lista
1. Se o usuario clicar no botão de ajuda sem ter selecionado um exemplo de pack (no índice zero do combo) mostrar a mensagem abaixo:
>Primeiro selecione um exemplo de Pack Virtual e após clique neste ícone para vericar os modelos possíveis para a opção selecionada!

## Tarefa 13: Acertar o txtValorRegra_TextChanged
1. No txtValorRegra_TextChanged acertar os ifs para utilizar a propriedade _modelo.xxx.DescricaoComercial

## Tarefa 14: Acertar o txtQtdRegra_Leave
1. No txtQtdRegra_Leave acertar os ifs para utilizar a propriedade _modelo.xxx.DescricaoComercial

## Tarefa 15: Remover procedimento ConverterPackVirtualParaSimplificado
 1. Remover procedimento que parece não esta sendo usado. `public static PackVirtualSimplificado ConverterPackVirtualParaSimplificado(PackVirtual packVirtual, string grupoCliente)`

## Tarefa 16: Acertar pesquisas 

1. Na Telas Pack Virtual e DescontosParaAtacado temos uma query que carrega a pesquisa inicial da tela. Na tela pack virtual tirar o código CodPack <> 13. Na tela DescontosParaAtacado tirar o CodPack = 13. Não iremos tratar pelo código porque quando criarmos novos códigos teremos que adicionar na query. Para corrigir:
* Na tela pack virtual alterar o procedimento RetornarPackVirtualSimplificado deve retornar só se o TipoDePack = PackVirtual
* Na tela Descontos Para atacado criar um override para este procedimento retornarndo só o pack que TipoDePack = DescontoParaAtacado

1. No procedimento CarregarPacks 
substituir
``` C#
     if (cboModeloPackPesquisa.SelectedIndex >= 8 && !_moduloKw)
        complemetoSQL.AppendLine(" And PV.ModeloPack = " + (cboModeloPackPesquisa.SelectedIndex + 1));
    else if (cboModeloPackPesquisa.SelectedIndex != 0)
        complemetoSQL.AppendLine(" And PV.ModeloPack = " + cboModeloPackPesquisa.SelectedIndex);
```
por 
``` C#
        complemetoSQL.AppendLine(" And PV.ModeloPack = " + _modelos.RetornarModeloPelaDescricao(cboModeloPackPesquisa.Text).CodModeloPack);
```

1. Igualmente no procedimento CarregarPacks

``` C#
                if (packVirtual.ModeloPack >= 8 && _moduloKw)
                    cboModeloPack.SelectedIndex = packVirtual.ModeloPack + 1;
                else
                    cboModeloPack.SelectedIndex = packVirtual.ModeloPack;
```

1. Igualmente No procedimento dgvPackFiltro_CellMouseDoubleClick

``` C#
                if (packVirtual.ModeloPack >= 8 && !_moduloKw)
                    cboModeloPack.SelectedIndex = packVirtual.ModeloPack - 1; //Devido ao pack do Tischler visível apenas para eles
                else
                    cboModeloPack.SelectedIndex = packVirtual.ModeloPack;
```

## Tarefa 17: Percorrer tela do pack virtual em busca de código obsoleto
1. Verificar na tela se ficou alguma descrição antiga dos packs em algum if. Corrigir se ocorrer

## Tarefa 18: Atualizar o formulário FrmDescontoParaAtacado para utilizar a classe _modelo 
1. Foram criados alguns procedimentos nesse formulário que sobreescrevem os procedimentos que alteramos nas tarefas anteriores. Acerta-los no mesmo formato.
1. utilizar a propriedade _modelos nos ifs, no carrega tela, no salvar, e ao carregar os combos, da mesma forma que na tela pack virtual. 
1. Ao carregar os combos na segunda aba não precisamos colocar a primeira opção `Selecione um exemplo de pack`. Ja deverá abrir na opção `A partir de X unidades ganhe desconto (%)`. Carregar da mesma forma que na tarefa 3. Deverão ser carregados 2 modelos no combo. Utilizar o `_modelo.RetornarListaComboParaDescontoParaAtacado`

## Tarefa 19: Implementar a opção de produtos associados no form FrmDescontoParaAtacado e no PDV
1. Na aba cadastro ao selecionar esse modelo implementar para montar a tela de acordo com a classe _modelos.ApartirDeXGanheDescontoProdutosAssociadosAtacado
1. Verificar o salvar. O salvar deverá ter o mesmo comportamento do pack
_modelos.ApartirDe6PagueMenosPORCENTAGEM, deverá salvar da mesma forma nas tabelas.
1. Ao adicionar um produto que possua produtos associados no grid deverá ter o mesmo tratamento que no pack virtual _modelos.ApartirDe6PagueMenosPORCENTAGEM, deverá perguntar se deseja adicionar os produtos associados a ele na grade. 
1. Implementar o botão `Buscar Produtos por Grupo` no modelo de produtos associados. Trocar o text do botão para `Buscar Produtos associados`. A funcionalidade deverá ser a mesma mas em vez de buscar nos grupos deverá buscar nos produtos associados. Se necessário duplicar o form de busca por gupos. Não deverá aparecer o 2º form de produtos. 
    * Neste caso não deverá dar a mensagem do adicionar referente aos produtos associados. Apenas a outra mensagem referente ao produto já estar presente em outro desconto para atacado.
1. Implementar o Modelo de Desconto no Telecon PDV 3. Ele terá o mesmo funcionamento que no pack virtual _modelos.ApartirDe6PagueMenosPORCENTAGEM. Modelo = 9 no banco de dados. A única coisa que faremos no pdv é que ele fará o mesmo tratamento que fazia para o modelo 9 para o modelo 14 igualmente.
* No pdv na função mdlValidarRegras.fValidarPack no case 9 adicionar também o 13, deixando os dois funcionando da mesma forma (9,14)
1. Testar cadastro e funcionamento do novo Desconto para Atacado no PDV
1. Na aba de pesquisa o modelo de produtos associados já deve estar sendo listado e o filtro funcionando, testar. 
## Tarefa 20: Adicionar facilitador para o Desconto de Atacado no Cadastro de Produtos 

1. Criar o combobox cboDescontoAtacado no frame de promoções do cadastro de produto conforme o exemplo abaixo

    ![](https://github.com/Rodrigo80221/AnalisesDeSoftware/blob/main/Imagens/FrmCadProdutos_FramePromocao.jpg?raw=true)
1. Acertar o tabIndex
1. Criar a função `string fRetornarModelosDescontosParaAtacado` no c# no classe PackVirtualVB6 seguindo o passo abaixo (as funções nessa classe recebem a conexão por parâmetro, fazer da mesma forma)
    * Criar uma variál string e inserir nela separado por "," todos os modelos de Desconto Para atacado. Para pegar os modelos percorrer o list Modelo. Essa variável deverá ser o retorno da função 

1. No frmCadProdutos do vb criar o procedimento sExibirComboDescontosAtacado. Realizar a consulta abaixo

    `Select codigo, Descricao from PackVirtual where codmodelo in ClasseC#.fRetornarModelosDescontosParaAtacado order by codigo desc`

    * Apenas caso retorne algum registro exibir o combo cboDescontoAtacado
    * No combo inserir todos os registros que retornarem na consulta ordenados 
    * Verificar se o produto faz parte de um desses packs na tabela PackVirtualGrupo1, se fizer posicionar o combo no pack do produto. Caso não possua deixar vazio na posição 0

1. Caso o usuário selecione um desconto para atacado para algum produto ou altere o Desconto de Atacado do produto, no salvar teremos que fazer um testa campos e salvar no banco.
    * O testa campos que precisamos está no c# no formulário FrmDescontoParaAtacado nos procedimentos `PerguntarProdutosAssociados` e `CriticarProdutoJaUtilizado`. E não estão acessíveis no vb.
    * No gestão biblioteca criar o diretório `DescontoParaAtacado`, Criar a Classe `DescontoParaAtacado.cs` e passar apenas o código desses procedimentos para lá, podemos passar o objeto de conexão por parâmetro se necessário. Fazer o FrmDescontoAtacado consumir os procedimentos nessa classe.
    * Na classe PackVirtualVB6 chamar também esses procedimentos para poderem serem consumidos pelo vb
    * Acertar a estrutura para utilizar o mesmo código. Salvar o produto do cadastro de produtos no Desconto de Atacado selecionado.
## Tarefa 21: Adicionar informação sobre Desconto Pack/Atacado no Cadastro de Produtos 
* Criar a função abaixo no c# na classe PackVirtualVB6.cs, a função foi feita apenas aqui no texto, e não está funcional, corrigir na tarefa...

``` Csharp
    public string RetornarDescricaoModeloPack(string packDescricao, float qtdRegra, float valorRegra, int CodModeloPack)
    {
        var modelo = new Telecon.GestaoComercial.Biblioteca.PackVirtual.ModeloPack();

        var m = modelo.RetornarModeloPeloCodModeloPack(CodModeloPack);

        string texto = packDescricao  + " ( " + m.DescricaoLabel1 + System.Convert.ToString(valorRegra) + m.DescricaoLabel2 + m.DescricaoLabel3 + System.Convert.ToString(valorRegra) + m.DescricaoLabel4 + " )";

        return texto;
    }
```
1. No cadastro de produto na aba de lojas, criar uma descrição semelhante a que já temos para as promoções. 
* Carregar as descrições no grid, e ao passar o mouse exibir a descrição
* Utilizar a função do c# RetornarDescricaoModeloPack, realizar nela as alterações necessárias para formatar corretamente os campos, não testei a função apenas criei para manter o modelo. 
* Programar de acordo com a imagem abaixo
![](https://github.com/Rodrigo80221/MARKDOWN/blob/main/Imagens/CadProdutosPromocao4.jpg?raw=true)
![](https://github.com/Rodrigo80221/MARKDOWN/blob/main/Imagens/CadProdutosPromocao3.jpg?raw=true)
* Veja que no desconto para atacado não possui datas
* Na descrição da promoção temos uma frase semelhante, corrigir crase no "à"
* Ao clicar no label deverá abrir a tela do pack virtual ou desconto para atacado na aba de pesquisa exibindo somente o respectivo registro.
* Testar as programações realizadas no cadastro de produtos com packs que possuam 1 e 2 grades no cadastro. Testar também com os dois tipos de desconto para atacado.
1. Alterar a mensagem abaixo no `frmCadProdutos.fValidaProdutoPackVirtualVigente` 
substituir
``` 
MsgBox "Não é permitido inativar um produto que esteja em um pack virtual vigente!", vbInformation, "Aviso"
```
por 
``` 
MsgBox "Não é permitido inativar um produto que esteja em um pack virtual vigente ou tenha um desconto para atacado!", vbInformation, "Aviso"
```
## Tarefa 22: Corrigir detalhes de mensagebox

1. No procedimento `frmImportaCupomSaidas.cmdAdicionar_Click` substituir a mensagem
`MsgBox "Alguns produtos contidos neste cupom possui Pack Virtual."`
por 
`MsgBox "Alguns produtos contidos neste cupom possuem Pack Virtual ou Desconto Atacado"`

1. No formulário `frmCadGruposClientes` alterar o caption do controle `chkNaoAplicarDescontoProdutoPack`, substituir por 
`Não aplicar o desconto no Pack Virtual ou Desconto Atacado`

1. No formulário c# FrmDescontoParaAtacado, possui algumas mensagens novas criadas, torcar o "Pack Virtual" por "Desconto Atacado".

 ## Tarefa 23: Ajustes finais e testes de integração
1. Refazer os packs que foram feitos na tarefa 1 e comparar todos para ver se ficaram iguais no banco de dados.
1. Ajustar possíveis diferenças ou erros
1. Verificar se os packs estão funcionando no pdv de acordo com os exemplos dados no ponto de interrogação da tela.

























# tarefa retirada 


* Criar as funções abaixo no c# no classe PackVirtualVB6.cd
``` Csharp
    public int RetornarTipoDePack(int CodModeloPack)
    {
        var modelo = new Telecon.GestaoComercial.Biblioteca.PackVirtual.ModeloPack();
        var tipoPack = modelo.RetornarModeloPeloCodModeloPack(CodModeloPack).Tipo;

        return (int)tipoPack;
    }
```

``` Csharp
    public string RetornarDescricaoModeloPack(float qtdRegra, float valorRegra, int CodModeloPack)
    {
        var modelo = new Telecon.GestaoComercial.Biblioteca.PackVirtual.ModeloPack();

        var m = modelo.RetornarModeloPeloCodModeloPack(CodModeloPack);

        string texto = m.DescricaoLabel1 + System.Convert.ToString(valorRegra) + m.DescricaoLabel2 + m.DescricaoLabel3 + System.Convert.ToString(valorRegra) + m.DescricaoLabel4;

        return texto;
    }
```
* Instanciar a classe Telecon_GestaoComercial_Biblioteca.PackVirtualVB6 no VB no cadastro de produtos

* No form frmCadProdutos criar a função fRetornarPackVirtualVigente que retorne o código da tabela pack virtual. A função deve ser semelhante a fValidaProdutoPackVirtualVigente mas deve levar em consideração a loja logada.
* No procedimento sAtualizaCamposPromocao, utilizar a função  fRetornarPackVirtualVigente e RetornarTipoDePack para montar uma descrição. Adicionar e implementar um label de acordo com as imagens abaixo.
![](https://github.com/Rodrigo80221/MARKDOWN/blob/main/Imagens/CadProdutosPromocao.jpg?raw=true)
![](https://github.com/Rodrigo80221/MARKDOWN/blob/main/Imagens/CadProdutosPromocao2.jpg?raw=true)
* O label deve ficar por padrão invisível, deverá ficar visível caso o produto possua um pack virtual ou desconto para atacado com data vigente para a loja logada.
* O `RetornarTipoDePack` deverá ser utilizado para saber se é um pack virtual ou um desconto para atacado.
* Ao clicar no label deverá abrir a tela do pack virtual ou desconto para atacado na aba de pesquisa exibindo somente o respectivo registro.
