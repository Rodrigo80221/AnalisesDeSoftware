## Tarefa 20: Criar tabela no verifica banco
1. Criar verifica banco para inserir a tabela abaixo

``` sql
CREATE TABLE Etiquetas
(
    Codigo int Identity Primary Key,
    Descricao varchar(50) not null,
    Arquivo varbinary(max) not null
)            
```

## Tarefa 21: Criar verifica banco para migrar etiquetas para o sql server
**Objetivo:** Atualmente salvamos as etiquetas da central de impressão na pasta `C:\Telecon_Sistemas\Gestao\Etiquetas`, e isso tem gerados alguns problemas de perda de arquivos, problemas de acesso nas pastas e substituição dos arquivos nas atualizações do sistema. Iremos a partir deste momento salvar as etiquetas no banco de dados. 

Na atualização do sistema iremos migrar as etiquetas para o banco de dados.
1. Criar atualiza banco para fazer backup das etiquetas. Programar conforme a lógica abaixo
* caso na pasta das etiquetas que está no bd rede, exemplo `\\Servidor\Telecon_Sistemas\Gestao\Etiquetas` possua arquivos de etiquetas, fazer backup delas, voltar um diretório e criar a pasta BkpEtiquetas exemplo `\\Servidor\Telecon_Sistemas\Gestao\BkpEtiquetas`
Salvar essas etiquetas no banco do gestão comercial na tabela Etiquetas

Abaixo um exemplo de inserção
``` sql
INSERT INTO Etiquetas (Descricao, Arquivo) 
Select 'Etiqueta', BulkColumn
FROM Openrowset(Bulk 'C:\Telecon_Sistemas\Gestao\Etiquetas\Etiqueta.etq', Single_Blob) as Arquivo
```            

2. Excluir os arquivos etq do diretório `Etiquetas`

## Tarefa 22: Criar C# LerArquivoDoBanco 

1. Criar em genéricos na classe SQL2000 o procedimento LerArquivoDoBanco retornando string. O procedimento deve ler um campo varbinary no banco de dados e retornar uma string.

código que utilizei como teste

``` Csharp
private void button1_Click(object sender, EventArgs e)
{

    Telecon.Genericos.Classes.BancoDeDados.Sql2000 banco = new Sql2000("(local)", "Gestao", "SA", "a2m8x7h5");
    banco.AbrirConexao();
    
    var consulta = banco.Consultar("select Arquivo from Etiquetas where Codigo = 1");

    byte[] Arquivo = null;
    string Texto = "";

    while(consulta.Read())
    {
        Arquivo = (byte[])consulta["Arquivo"];
    }

    Texto = Encoding.ASCII.GetString(Arquivo);

    Msg.Criticar(Texto);

}
```        

## Tarefa 22: Substituir o fileListBox por um ListBox no FrmImpressao

1. Substituir o fileListBox filArquivos por um listview lvwArquivos manter a mesma aparência

1. Tratar no FrmImpressao.Form_Load para listar os arquivos de acordo com as informações do banco de dados. No final do Form_Load tratar para buscar a última etiqueta utilizada.





fSalvarPropriedadeEtiqueta

