namespace Comex.GeneradorRelatorio;

/// <summary>
/// Dados de um produto a serem exportados em um relatório.
/// </summary>
/// <param name="Nome">Nome do produto.</param>
/// <param name="Preco">Preço unitário do produto.</param>
/// <param name="Quantidade">Quantidade em estoque.</param>
public record ProdutoRelatorioDto(string Nome, decimal Preco, int Quantidade);
