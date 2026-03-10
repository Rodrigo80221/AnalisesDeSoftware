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
    /// e retorna o código normalizado.
    ///
    /// Retornos possíveis:
    /// • 5 dígitos → apenas a comanda
    /// • 6 dígitos → comanda + dígito verificador
    ///
    /// Exemplos:
    /// 1234 -> 01234
    /// 01234 -> 01234
    /// 012345 -> 012345
    /// 1111111012345 -> 01234
    /// 7777012345 -> 01234
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
        // Usuário digitou comanda + DV
        // Ex: 012345
        // ----------------------------
        if (codigo.Length == 6)
            return codigo;

        // ----------------------------
        // ARGUS
        // 1111111 + comanda(5) + DV
        // ----------------------------
        if (codigo.StartsWith("1111111") && codigo.Length >= 13)
        {
            string comanda = codigo.Substring(7, 5);
            return comanda.PadLeft(5, '0');
        }

        // ----------------------------
        // MASTERCHEFF
        // 7777 + comanda(5) + DV
        // ----------------------------
        if (codigo.StartsWith("7777") && codigo.Length >= 10)
        {
            string comanda = codigo.Substring(4, 5);
            return comanda.PadLeft(5, '0');
        }

        // Prefixo sem DV
        if (codigo.StartsWith("7777") && codigo.Length == 9)
        {
            string comanda = codigo.Substring(4, 5);
            return comanda.PadLeft(5, '0');
        }

        // ----------------------------
        // Digitação manual
        // ----------------------------
        if (codigo.Length <= 5)
            return codigo.PadLeft(5, '0');

        // ----------------------------
        // Caso genérico
        // assume comanda(5) + DV
        // ----------------------------
        if (codigo.Length > 6)
        {
            string comanda = codigo.Substring(codigo.Length - 6, 5);
            return comanda.PadLeft(5, '0');
        }

        throw new ArgumentException("Não foi possível identificar o código da comanda.");
    }

    /// <summary>
    /// Busca um pedido utilizando o código informado pelo usuário.
    ///
    /// Regras:
    /// • Se possuir 6 dígitos → consulta exata (comanda + DV)
    /// • Se possuir 5 dígitos → consulta usando StartsWith
    ///
    /// O banco armazena:
    /// CodPedidoVendedor = comanda(5) + DV
    /// </summary>
    public static async Task<Pedido?> BuscarPedidoPorComandaAsync(
        MeuDbContext context,
        string codigoInformado)
    {
        string codigoNormalizado = RetornarCodigoComandaBanco(codigoInformado);

        Pedido? pedido;

        if (codigoNormalizado.Length == 6)
        {
            // Consulta exata quando possui DV
            pedido = await context.Pedidos
                .FirstOrDefaultAsync(p => p.CodPedidoVendedor == codigoNormalizado);
        }
        else
        {
            // Consulta pela comanda (ignora DV)
            pedido = await context.Pedidos
                .Where(p => p.CodPedidoVendedor.StartsWith(codigoNormalizado))
                .OrderByDescending(p => p.IdPedido)
                .FirstOrDefaultAsync();
        }

        return pedido;
    }
}

```