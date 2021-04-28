
criar classe de packs virtuais 
colocar uma propriedade para saber se é pack ou modelo de desconto
nas telas listas de acordo com essa propriedade
alterar nomenclaturas
criar desconto para atacado para os associados 
Arrumar help dos packs mais novos para ajudar o usuário

--------------------------

devemos ter uma relação com o modelo de etiqueta

colocar associação da etiqueta na tela de desconto para atacado

fazer o frm impressão imprimir de acordo com o pack
revisar a tela de impressão na questão do pack virtual

Colocar pack virtual ou desconto para atacado no cadastro de produtos 


Verificar a coluna Pack nos grids e o pack nas mensagens

Importa cupom fiscal > coluna grid
FrmImpressão > Coluna grid (na etiqueta e no cartaz) no cartaz está cortando a descrição


colocar observação nos packs de compra em mercadorias
** Observação: Para fechar o valor da compra não pode conter os produtos que irão receber desconto.

colocar observação nos packs virtuais de formato atacado
** Observação: Cada produto informado na grade será contado individualmente por código de barras.


criar os relatórios 



## Tarefa 1: Criar propriedade _modelos para acessar 
1. Ativar o módulo da KW, pode ser pelo config da tabela módulos no banco de dados. Realizar um pack de cada modelo, anotar todos para comparar com a tela no final da alteração, devem ser mantidos o codmodelo.
1. criar propriedade local no formulário
``` c#
private Telecon.GestaoComercial.Biblioteca.PackVirtual.ModeloPack _modelos = new Pack.ModeloPack();
``` 

## tarefa 2: Acertar CarregarDgvGrupo1
1. No CarregarDgvGrupo1 trocar o texto no 1º if para utilizar a propriedade _modelo 
`"Pague x porcento a menos a partir de x unidades (atacado)"`
por _modelos.ApartirDe6PagueMenosPORCENTAGEM.DescricaoComercial
Utilizar essa estrutura nas próximas validações
1.  No CarregarDgvGrupo1 trocar o 2º if para utilizar a propriedade _modelo.xxx.DescricaoComercial 
`cboModeloPack.Text.Equals("Pague x porcento a menos a partir de x unidades (atacado)"))`
1.  No CarregarDgvGrupo1 trocar o 3º if para utilizar a propriedade _modelo.xxx.DescricaoComercial 
`"Valor Diferenciado (Preço 2)"`

## tarefa 3: Acertar CarregarComboModeloPack
1. Carregar o combo atravez do list _modelos.RetornarListaComboParaPackVirtual 
utilizar o código abaixo para carregar os modelos
``` C#
    foreach(ModeloPack.Modelo modelo in ModeloPack.RetornarListaComboParaPackVirtual(true))
    {
        comboBox1.Items.Add(modelo);
    }
```
1. No combo da primeira aba a primeira opção é `Todos`
1. No combo da Segunda aba a primeira opção é `Selecione um exemplo de promoção` 
1. Alterar o caption do lblModeloDoPack para `Exemplos de Pack` 

algo similar a imagem abaixo
![](https://github.com/Rodrigo80221/MARKDOWN/blob/main/Imagens/ComboBoxPackVirtual.jpg?raw=true)




## tarefa 4: Substituir o switch case do procedimento cboModeloPack_SelectedIndexChanged

1. Criar código para substituir o switch case. Buscar o modelo utilizando `_modelo.RetornarModeloPelaDescricao`, setar todos os procedimentos e objetos utilizando as propriedades do objeto modelo.

1. Recriar o procedimento de posicionar os labels e os texts. tendo como base apenas o primeiro label, depois alterar a propriedade `.left` dos outros labels e texts para ficarem alinhados. Criar o pocedimento PosicionarObjetosDeRegras, ele deve ser chamado após o `DefinirValoresLabels` programar para poder substuir o procedimento PosicionarLayoutLabel e o PosicionarLayoutTextBox.

## tarefa 5: Acertar txtValorRegra_Leave
1. No txtValorRegra_Leave acertar o if para utilizar a propriedade _modelo.xxx.DescricaoComercial

## tarefa 6: alterar o procedimento RetornarPackVirtualSimplificado
1. Remover o Swith e no lugar chamar o `_modelo.RetornarModeloPeloCodPack`

## tarefa 7: Acertar dgvGrupo1_CellMouseClick
1. No dgvGrupo1_CellMouseClick acertar o if para utilizar a propriedade _modelo.xxx.DescricaoComercial


## tarefa 8: Acertar dgvGrupo1_CellMouseClick
1. No TestarCampos acertar os ifs para utilizar a propriedade _modelo.xxx.DescricaoComercial

## tarefa 9: Acertar RetornarPackVirtual
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
 packVirtual.ModeloPack = _modelo.RetornarModeloPelaDescricao(descricaoQueEstaNocombo).codPack`
 ```
1. Acertar o último if para utilizar a propriedade _modelo.xxx.DescricaoComercial


## tarefa 10: Acertar o btnSalvar_Click
1. No btnSalvar_Click acertar os ifs para utilizar a propriedade _modelo.xxx.DescricaoComercial

## tarefa 11: Acertar o ptbQuestao_Click
1. No ptbQuestao_Click remover o switch case e exibir a propriedade _modelos.xxx.ExemplosDePromocao. Realizar um foreach assim como nos métodos do ModelosPack, buscando em todos da lista

## tarefa 12: Acertar o txtValorRegra_TextChanged
1. No txtValorRegra_TextChanged acertar os ifs para utilizar a propriedade _modelo.xxx.DescricaoComercial

## tarefa 13: Acertar o txtQtdRegra_Leave
1. No txtQtdRegra_Leave acertar os ifs para utilizar a propriedade _modelo.xxx.DescricaoComercial

## tarefa 14: Remover procedimento ConverterPackVirtualParaSimplificado
 1. Remover procedimento que parece não esta sendo usado. `public static PackVirtualSimplificado ConverterPackVirtualParaSimplificado(PackVirtual packVirtual, string grupoCliente)`

## tarefa 15: Acertar pesquisas 

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
        complemetoSQL.AppendLine(" And PV.ModeloPack = " + _modelos.RetornarModeloPelaDescricao(cboModeloPackPesquisa.Text).CodPack);
```

1. Igualmente No procedimento CarregarPacks

carregaPack
``` C#
                if (packVirtual.ModeloPack >= 8 && _moduloKw)
                    cboModeloPack.SelectedIndex = packVirtual.ModeloPack + 1;
                else
                    cboModeloPack.SelectedIndex = packVirtual.ModeloPack;
```

1. Igualmente No procedimento CarregarPacks

dgvPackFiltro_CellMouseDoubleClick
``` C#
                if (packVirtual.ModeloPack >= 8 && !_moduloKw)
                    cboModeloPack.SelectedIndex = packVirtual.ModeloPack - 1; //Devido ao pack do Tischler visível apenas para eles
                else
                    cboModeloPack.SelectedIndex = packVirtual.ModeloPack;
```

## Tarefa 16: Adicionar classe no formulário FrmDescontoParaAtacado
1. Foram criados alguns procedimentos nesse formulário que sobreescrevem os procedimentos que alteramos nas tarefas anteriores, acertálos no mesmo formato.
1. Criar a propriedade _modelos, utilizar a propriedade _modelos nos ifs, no carrega tela, no salvar, e ao carregar os combos, da mesma forma que na tela pack virtual. 
1. Ao carregar os combos na segunda aba não precisamos colocar a primeira opção `Selecione um exemplo de pack`. Ja deverá abrir na opção `A partir de X unidades ganhe desconto (%)`. Carregar da mesma forma que na tarefa 3. Deverão ser carregados 2 modelos no combo. Utilizar o `_modelo.RetornarListaComboParaDescontoParaAtacado`

## Tarefa 17: Implementar a opção de produtos associados no form FrmDescontoParaAtacado e no PDV
1. Na aba de pesquisa o modelo de produtos associados já deve estar sendo listado e o filtro funcionando, testar. 
1. Na aba cadastro ao selecionar esse modelo montar a tela de acordo com a classe _modelos.ApartirDeXGanheDescontoProdutosAssociadosAtacado
1. Implementar o salvar. O salvar deverá ter o mesmo comportamento do pack
_modelos.ApartirDe6PagueMenosPORCENTAGEM, deverá salvar da mesma forma nas tabelas.
1. Ao adicionar um produto que possua produtos associados no grid deverá ter o mesmo tratamento que no pack virtual _modelos.ApartirDe6PagueMenosPORCENTAGEM, deverá perguntar se deseja adicionar os produtos associados a ele na grade. 
1. Implementar o botão `Buscar Produtos por Grupo` no modelo de produtos associados. Trocar o text do botão para `Buscar Produtos associados`. A funcionalidade deverá ser a mesma mas em vez de buscar nos grupos deverá buscar nos produtos associados. Se necessário duplicar o form de busca por gupos.
1. Implementar o Modelo de Desconto no Telecon PDV 3. Ele terá o mesmo funcionamento que no pack virtual _modelos.ApartirDe6PagueMenosPORCENTAGEM. Modelo = 9 no banco de dados. A única coisa que faremos no pdv é que ele fará o mesmo tratamento que fazia para o modelo 9 para o modelo 14 igualmente.
* No pdv na função mdlValidarRegras.fValidarPack no case 9 adicionar também o 13, deixando os dois funcionando da mesma forma (9,14)
1. Testar cadastro e funcionamento do novo Desconto para Atacado no PDV


## Tarefa 17: Alterar o caption do label lblProdutoGratis
1. substiuir o text `Os valores 0,00 ou 0,01 indicam que o produto será gratis.`
por `R$0,00 indica que o produto será gratis.` 


## Tarefa 18: Adicionar informação sobre Desconto Pack/Atacado no Cadastro de Produtos

1. Criar um label no frame de promoções do cadastro de produto
* Criar a função abaixo no c# no classe PackVirtualVB6.cd
``` c#
        public int RetornarTipoDePack(int CodModeloPack)
        {
            var modelo = new Telecon.GestaoComercial.Biblioteca.PackVirtual.ModeloPack();
            var tipoPack = modelo.RetornarModeloPeloCodModeloPack(CodModeloPack).Tipo;

            return (int)tipoPack;
        }
```
* Instanciar a classe Telecon_GestaoComercial_Biblioteca.PackVirtualVB6 no VB no cadastro de produtos
* No form frmCadProdutos no procedimento sAtualizaCamposPromocao consultar na tabela packvirtual e nas tabelas relacionadas (similar ao frmImpressao.fAbreRecordSet) buscando um pack virtual vigente para a loja que está logada, e para o produto em questão. Essa consultado deve retornar o campo PackVirtual.ModeloPack que deverá ser passado por parâmetro para a função c# `RetornarTipoDePack`, se for pack virtual ou desconto atacado mostrar o label abaixo adequado ao tipo que retornou 








Na aba dados gerais no frame de promoções colocar um aviso?

Detalhar na aba dados complementares


 "frmCadProdutos", Err, "fValidaProdutoPackVirtualVigente", Erl, True
MsgBox "Não é permitido inativar um produto que esteja em um pack virtual vigente!", vbInformation, "Aviso"












"frmImportaCupomSaidas", Err, "cmdAdicionar_Click"
MsgBox "Alguns produtos contidos neste cupom possui Pack Virtual."


ver "frmCadGruposClientes", Err, "cadGruposClientes_AtualizaTela"
chkNaoAplicarDescontoProdutoPack



 ## tarefa 15: Ajustes finais e testes de integração

 ver procedimentos que foram substuídos na tela desconto atacado e também o código lá da tela, 

 para listar no combo, listar apenas os de pack virtual, ou desconto atacado, listar , verificar a pesquisa 