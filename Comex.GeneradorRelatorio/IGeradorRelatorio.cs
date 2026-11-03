namespace Comex.GeneradorRelatorio;

/// <summary>
/// Contrato para geradores de relatório de produtos.
/// </summary>
public interface IGeradorRelatorio
{
    /// <summary>
    /// Exporta os produtos informados para um arquivo.
    /// </summary>
    /// <param name="produtos">Produtos a serem exportados.</param>
    /// <param name="caminhoArquivo">Caminho (relativo ou absoluto) do arquivo a ser gerado.</param>
    /// <returns>Caminho absoluto do arquivo gerado.</returns>
    string Exportar(IEnumerable<ProdutoRelatorioDto> produtos, string caminhoArquivo);
}
