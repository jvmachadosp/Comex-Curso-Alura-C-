using System.Text.Json;
using Comex.GeneradorRelatorio;

namespace Comex;

public class MenuGerarRelatorioCsv : Menu
{
    private const string ArquivoJson = "produtos.json";
    private const string ArquivoCsv = "relatorio-produtos.csv";

    public MenuGerarRelatorioCsv(ComexService service) : base(service)
    {
    }

    public override string Descricao => "gerar relatório CSV de produtos";

    public override Task ExecutarAsync()
    {
        Console.Clear();
        ExibirTituloDaOpcao("Gerar relatório CSV");

        if (!File.Exists(ArquivoJson))
        {
            Console.WriteLine($"Arquivo {ArquivoJson} não encontrado. Consulte a API externa (opção 9) primeiro.");
            AguardarTecla();
            return Task.CompletedTask;
        }

        try
        {
            string json = File.ReadAllText(ArquivoJson);
            List<ProdutoApiResponse> produtos = JsonSerializer.Deserialize<List<ProdutoApiResponse>>(json) ?? new();

            List<ProdutoRelatorioDto> dtos = produtos
                .Where(p => p.Preco > 0 && !string.IsNullOrWhiteSpace(p.Nome))
                .OrderByDescending(p => p.Preco)
                .Select(p => new ProdutoRelatorioDto(p.Nome, p.Preco, 0))
                .ToList();

            IGeradorRelatorio gerador = new GeradorCsv();
            string caminhoSalvo = gerador.Exportar(dtos, ArquivoCsv);

            Console.WriteLine($"{dtos.Count} produtos exportados.");
            Console.WriteLine($"Relatório salvo em: {caminhoSalvo}");
        }
        catch (JsonException ex)
        {
            Console.WriteLine($"Erro ao ler {ArquivoJson}: {ex.Message}");
        }
        catch (IOException ex)
        {
            Console.WriteLine($"Erro ao gerar o relatório: {ex.Message}");
        }

        AguardarTecla();
        return Task.CompletedTask;
    }

    private static void AguardarTecla()
    {
        Console.WriteLine("\nPressione qualquer tecla para voltar ao menu...");
        Console.ReadKey();
    }
}
