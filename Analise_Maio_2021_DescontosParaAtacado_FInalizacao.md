## Parte 2 Tarefa 1: Criar funções no C# para comunicar com o VB6

Resumo: Criar função no c# para enviar ao vb uma lista para popular um combo de pack virtual e desconto atacado

1. Na `partial class ModeloPack` temos os dois procedimentos abaixo

`public List<Modelo> RetornarListaModelosDePack()`
`public List<Modelo> RetornarListaComboParaDescontoParaAtacado()`

* Criar na classe ` C# PackVirtualVB6 ` os procedimentos `RetornarListaModelosDePackVB6()` e `RetornarListaComboParaDescontoParaAtacadoVB6()` que chame os respectivos procedimentos da classe ModeloPack e converta o list para um array de string para que possa ser consumido pelo VB6.
> Apenas criar será testado nas próximas tarefas

2. Na `partial class ModeloPack` temos o procedimento abaixo

`RetornarModeloPelaDescricao(string descricaoModelo)`

* Criar na classe ` C# PackVirtualVB6 ` os procedimentos `RetornarModeloPelaDescricaoVB6(string descricaoModelo)` que chame o respectivo procedimento da classe ModeloPack e retorne um inteiro para que possa ser consumido pelo VB6.
> Apenas criar será testado nas próximas tarefas

## Parte 2 Tarefa 2: Criar tabela EtiquetasRelacionamentos
1. Criar verifica banco para criar a tabela
    ``` sql
    CREATE TABLE EtiquetasRelacionamentos
    (
    CodEtiqueta tinyint,
    ModeloPack tinyint
    )

    ALTER TABLE EtiquetasRelacionamentos ADD CONSTRAINT FK_Etiquetas_EtiquetasRelacionamentos
    FOREIGN KEY	(CodEtiqueta) REFERENCES Etiquetas (Codigo) ON DELETE CASCADE ON UPDATE CASCADE;

    Alter table EtiquetasRelacionamentos Add Constraint Ak_EtiquetasRelacionamentos_ModeloPack Unique (ModeloPack);
    ```
>Observações:
>frmLogin.lblInicializando.Caption = "Criar tabela EtiquetasRelacionamentos"
>Utilizar o Funcoes.fExisteObjeto     


## Parte 2 Tarefa 3: Alinhar frame de configurações
1. Alinhar o frame de configurações do FrmImpressao. O check box Referência a unidade básica está invadindo os campos laterais, e os texts do check box preço atacado estão desagrupados. Deixar semelhante a imagem abaixo mas com todos os componetes alinhados.
![](https://github.com/Rodrigo80221/AnalisesDeSoftware/blob/main/Imagens/FrmImpressao_Configuracoes.png?raw=true)
2. Ajustar o tab index.
3. Ao selecionar o option button cartaz, o a coluna "Cod. Pack" está com o título cortado, aumentar.

## Parte 2 Tarefa 4: Criar aba "Relacionar Etiquetas" no frmImpressao
1. Adicionar a aba `&Relacionar Etiquetas`
1. Tratar no Form Load para a aba só ficar visível caso o módulo do pack virtual ou o módulo Desconto para atacado estiver ativo
1. Inserir os componetes conforme a imagem abaixo
    ![](https://github.com/Rodrigo80221/AnalisesDeSoftware/blob/main/Imagens/FrmImpresssao_Aba_RelacionarEtiquetas.jpg?raw=true)
1. Utilizar o ícone de ajuda que já foi utilizado no cadastro de produtos.
1. Ajustar o Tab index
1. Utilizar o Grid padrão Telecon + Selecionar linha inteira + Colunas fixas
1. Carregar o combo de etiquetas com os dados da tabela etiquetas
1. Carregar o combo Tipo de Promoção utilizando as funções criadas no c#, Listar de acordo com o option button selecionado.
1. Implementar o botão adicionar. No adicionar já adicionar na tabela EtiquetasRelacionamentos, para obter o CodigoModelo utilizar a função criada no C# passando a descrição selecionada no combo.
1. Caso o usuário adicione um tipo de promoção que já esteja na grade mostrar a mensagem abaixo e impedir
    >O tipo de promoção já possui uma etiqueta relacionada!
1. O remover deve mostrar a mensagem abaixo yes/no padrão "Não" e remover tda tabela EtiquetasRelacionamentos
    >A relação "NomeDaEtiqueta" -> "TipoDePromoção" será excluída deseja continuar?
1. Tratar no sCorrigeBugTab para não dar o bug ao maximizar e minimizar novamente
1. Tratar no maximizar para se ajustar a tela para ampliar as bordas do frame
1. Adicionar texto abaixo no ícone de ajuda 
> Relacione uma etiqueta com um Pack Virtual ou com um Desconto para Atacado!
>
> Definindo uma etiqueta para um Pack Virtual ou um Desconto para Atacado, a tela imprimirá sempre a etiqueta de acordo com a configuração, ignorando a etiqueta configurada na aba  "Config. Etiqueta" 

1. Cadastrar etiquetas e relacionar com diversos packs testando a estrutura, corrigir eventuais falhas


## Parte 2 Tarefa 5: Imprimir as etiquetas de acordo com a relação configurada 
> Essa é a tarefa com maior complexidade e maior impacto e poderemos ter problemas de performance.
> Atualmente a central de impressão possui uma etiqueta selecionada e os objetos ficam carregados na tela de acordo com a etiqueta, ao imprimir lemos a posição dos objetos na tela. Caso selecionarmos um produto que possua Pack Virtual ou Desconto para Atacado teremos que carregar a etiqueta na tela para imprimir e após retornar novamente a etiqueta padrão para os produtos sem pack

Tarefa: 
- Ao imprimir uma etiqueta teremos que verificar se o produto possui um pack virtual vigente ou desconto para atacado (essa informação já está no grid)
- Caso haja iremos buscar o modelo na tabela pack virtual e verificar se existe uma etiqueta relacionada na tabela EtiquetasRelacionamentos
- Caso haja teremos que carregar essa etiqueta na tela, pois a impressão verifica a localização dos objetos na tela
- Após teremos que carregar novamente a etiqueta padrão na tela.

Existem várias formas para programar a tarefa, abaixo darei apenas um direcionamento que utilizei para testar se seria possível a alteração.

1. A programação efetuada na tela deve ter comentário por linha de código adicionada para facilitar a manutenção e entendimento das alterações.
1. Antes de realizar qualquer alteração na tela realizar uma impressão para teste de performance, pode ser utilizando a impressora do windows
    * Carregar a grade com 40 produtos, 20 com pack e 20 sem pack, eles precisam ser adicionados intercalados, 1 produto com pack e outro sem pack, assim teremos um teste de stress com um cenário que exigirá muitas alterações nos objetos em tela. Marcar o tempo de impressão
1. Criar a função `fRetornarEtiquetaPack` onde passamos o ModeloPack por parâmetro e a função retorna o código da etiqueta.
1. Na grade de impressão temos o campo PackVirtual.Codigo, adicionar mais uma columa oculta (width=0) para guardar o campo PackVirtual.ModeloPack. Tratar para carregar na grade.
1. Ao carregar uma etiqueta pelo list salvar a etiqueta selecionada no registro do windows em `SaveSetting "Gestao", "Impressao", "ArquivoEtq","XXXX"`
    * O registro já está sendo usando no unload e no load
1. No procedimento   `sUsaImpressoraWindows` temos um for dentro do outro. Dentro do 2º for programar:
    * Se o produto possuir Pack Virtual (coluna da grade) buscar a etiqueta configurada para esse pack com a função `fRetornarEtiquetaPack`. Chamar o procedimento `fMontarEtiqueta` e carrega-la na tela
    * Se o próximo produto não possuir pack teremos que carregar novamente o layout que foi salvo no registro do windows.
    * Se para carregar as etiquetas vc utilizar o procedimento `sCarregarConfigEtiquetas` terá criar um parâmentro para não executar a configuração `picEtiqueta.ScaleMode = vbPoints` , ou terá que passar ela para outro local que chama essa função, ou fazer bkp da configuração, ou algo do tipo, caso contrário irá alterar essa configuração e implicará na alteração do layout do procedimento `sUsaImpressoraWindows`
    * Tratar para não carregar sempre a etiqueta, caso o próximo produto use a mesma etiqueta não deveremos carrega-la para evitar problemas de performance.
    * No final do for a etiqueta carregada deverá ser a etiqueta padrão
1. Fazer um tratamento semelhante para a impressão da etiqueta argox no `sImprimeEtiqueta` 

## Parte 2 Tarefa 6: Teste de integração 
1. Pegar o tempo de impressão registrado na tarefa 5 e comparar com a tela após a alteração, se houver uma diferença de tempo considerável verificar com a análise. (Teremos que agrupar as impressões de alguma forma.)
2. Criar diversos packs e etiquetas, validando o funcionamento da tela:
    * Imprimir e criar novas etiqueta
    * Alterar etiquetas para os packs
    * Excluir etiquetas
    * Imprimir, maximizar, minimizar e imprimir
3. Corrigir eventuais falhas encontradas    






