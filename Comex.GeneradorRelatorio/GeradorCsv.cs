using System.Globalization;
using System.Text;

namespace Comex.GeneradorRelatorio;

/// <summary>
/// Gerador de relatório de produtos no formato CSV.
/// </summary>
public class GeradorCsv : IGeradorRelatorio
{
    /// <summary>
    /// Exporta os produtos para um arquivo CSV (separador ';', UTF-8).
    /// </summary>
    /// <param name="produtos">Produtos a serem exportados.</param>
    /// <param name="caminhoArquivo">Caminho (relativo ou absoluto) do arquivo CSV a ser gerado.</param>
    /// <returns>Caminho absoluto do arquivo gerado.</returns>
    /// <exception cref="ArgumentNullException">Se <paramref name="produtos"/> for nulo.</exception>
    /// <exception cref="ArgumentException">Se <paramref name="caminhoArquivo"/> for vazio.</exception>
    public string Exportar(IEnumerable<ProdutoRelatorioDto> produtos, string caminhoArquivo)
    {
        ArgumentNullException.ThrowIfNull(produtos);
        ArgumentException.ThrowIfNullOrWhiteSpace(caminhoArquivo);

        var sb = new StringBuilder();
        sb.AppendLine("Nome;Preco;Quantidade");

        foreach (var p in produtos)
        {
            sb.Append(Escapar(p.Nome)).Append(';')
              .Append(p.Preco.ToString(CultureInfo.InvariantCulture)).Append(';')
              .Append(p.Quantidade.ToString(CultureInfo.InvariantCulture))
              .AppendLine();
        }

        var caminhoCompleto = Path.GetFullPath(caminhoArquivo);
        var diretorio = Path.GetDirectoryName(caminhoCompleto);
        if (!string.IsNullOrEmpty(diretorio))
            Directory.CreateDirectory(diretorio);

        File.WriteAllText(caminhoCompleto, sb.ToString(), new UTF8Encoding(true));
        return caminhoCompleto;
    }

    private static string Escapar(string valor)
    {
        if (valor.IndexOfAny([';', '"', '\n', '\r']) < 0)
            return valor;
        return $"\"{valor.Replace("\"", "\"\"")}\"";
    }
}
