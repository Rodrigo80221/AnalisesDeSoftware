## Torus

Passo 1: Validar a configuração UTILIZA_SISTEMA_CATRACA_MORPHEUS.

Passo 2: Caso habilitada, utilizar o método RetornarCodigoComandaBanco para normalizar o código da comanda informado.

Passo 3: Consultar a tabela Pedidos utilizando o campo CodPedidoVendedor, comparando os 5 primeiros dígitos da comanda através de StartsWith(comandaNormalizada).

Abaixo o método RetornarCodigoComandaBanco e o exemplo de consulta. 


``` C#
using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

public static class ComandaHelper
{
    /// <summary>
    /// Recebe um código de comanda digitado manualmente ou lido por código de barras
    /// e retorna o código da comanda no formato padrão de 5 dígitos.
    /// 
    /// Exemplos de entrada:
    /// 1111111012345 (ARGUS)
    /// 7777012345    (MASTERCHEFF)
    /// 01234         (manual)
    /// 1234          (manual)
    /// 
    /// Saída:
    /// 01234
    /// </summary>
    public static string RetornarCodigoComandaBanco(string codigoLido)
    {
        if (string.IsNullOrWhiteSpace(codigoLido))
            throw new ArgumentException("Código da comanda não informado.");

        // Remove qualquer caractere que não seja número
        string codigo = new string(codigoLido.Where(char.IsDigit).ToArray());

        if (string.IsNullOrWhiteSpace(codigo))
            throw new ArgumentException("Código da comanda inválido.");

        // ----------------------------
        // PADRÃO ARGUS
        // ----------------------------
        // Exemplo:
        // 1111111012345
        // 1111111 = prefixo
        // 01234   = comanda
        // 5       = dígito verificador
        if (codigo.StartsWith("1111111") && codigo.Length >= 13)
        {
            string comanda = codigo.Substring(7, 5);
            return comanda.PadLeft(5, '0');
        }

        // ----------------------------
        // PADRÃO MASTERCHEFF
        // ----------------------------
        // 7777 + comanda(5) + DV
        // Exemplo:
        // 7777012345
        if (codigo.StartsWith("7777") && codigo.Length >= 10)
        {
            string comanda = codigo.Substring(4, 5);
            return comanda.PadLeft(5, '0');
        }

        // Caso exista prefixo mas sem DV
        if (codigo.StartsWith("7777") && codigo.Length == 9)
        {
            string comanda = codigo.Substring(4, 5);
            return comanda.PadLeft(5, '0');
        }

        // ----------------------------
        // DIGITAÇÃO MANUAL
        // ----------------------------
        // Exemplo:
        // 01234
        // 1234
        if (codigo.Length <= 5)
            return codigo.PadLeft(5, '0');

        // ----------------------------
        // CASO GENÉRICO
        // ----------------------------
        // assume que:
        // comanda(5) + DV
        if (codigo.Length >= 6)
        {
            string comanda = codigo.Substring(codigo.Length - 6, 5);
            return comanda.PadLeft(5, '0');
        }

        throw new ArgumentException("Não foi possível identificar o código da comanda.");
    }

    /// <summary>
    /// Busca um pedido no banco de dados utilizando o código da comanda.
    /// 
    /// O banco armazena sempre:
    /// CodPedidoVendedor = comanda(5) + dígito verificador
    /// 
    /// Exemplo:
    /// 012345
    /// 
    /// Como o dígito verificador pode variar, utilizamos StartsWith
    /// para comparar apenas os 5 primeiros dígitos da comanda.
    /// </summary>
    public static async Task<Pedido?> BuscarPedidoPorComandaAsync(
        MeuDbContext context,
        string codigoInformado)
    {
        // Normaliza o código recebido
        string comandaNormalizada = RetornarCodigoComandaBanco(codigoInformado);

        // Consulta o pedido
        var pedido = await context.Pedidos
            .Where(p => p.CodPedidoVendedor.StartsWith(comandaNormalizada))
            .OrderByDescending(p => p.IdPedido) // pega o pedido mais recente da comanda
            .FirstOrDefaultAsync();

        return pedido;
    }
}
```