using Comex;
using Xunit;

namespace Comex.Tests;

public class ComexServiceTests
{
    [Fact]
    public void CadastrarCliente_DeveCadastrarEAplicarBloqueioDeDuplicidade()
    {
        ComexService service = new ComexService();
        Cliente cliente = CriarCliente("João", "12345678900");

        bool primeiroCadastro = service.CadastrarCliente(cliente);
        bool segundoCadastro = service.CadastrarCliente(cliente);

        Assert.True(primeiroCadastro);
        Assert.False(segundoCadastro);
    }

    [Fact]
    public void AjustarPreco_DeveAtualizarPrecoQuandoProdutoExiste()
    {
        ComexService service = new ComexService();
        Produto produto = new Produto("Mouse", 100m);
        service.CadastrarProduto(produto);

        bool resultado = service.TentarAjustarPrecoDeProduto("Mouse", 120m);

        Assert.True(resultado);
        Assert.Equal(120m, produto.Preco);
    }

    [Fact]
    public void AdicionarProdutoNoCarrinho_DeveFalharQuandoClienteNaoExiste()
    {
        ComexService service = new ComexService();
        service.CadastrarProduto(new Produto("Teclado", 200m));

        bool resultado = service.TentarAdicionarProdutoNoCarrinho("ClienteInexistente", "Teclado");

        Assert.False(resultado);
    }

    [Fact]
    public void FecharCompra_DeveRetornarProdutosETotalCorreto()
    {
        ComexService service = new ComexService();
        Cliente cliente = CriarCliente("Maria", "98765432100");
        service.CadastrarCliente(cliente);

        Produto produto1 = new Produto("Livro", 50m);
        Produto produto2 = new Produto("Fone", 150m);

        service.CadastrarProduto(produto1);
        service.CadastrarProduto(produto2);

        service.TentarAdicionarProdutoNoCarrinho("Maria", "Livro");
        service.TentarAdicionarProdutoNoCarrinho("Maria", "Fone");

        bool resultado = service.TentarFecharCompra("Maria", out List<Produto> produtos, out decimal total);

        Assert.True(resultado);
        Assert.Equal(2, produtos.Count);
        Assert.Equal(200m, total);
    }

    [Fact]
    public void ListarProdutosOrdenadosPorNome_DeveOrdenarCorretamente()
    {
        ComexService service = new ComexService();
        service.CadastrarProduto(new Produto("Teclado", 200m));
        service.CadastrarProduto(new Produto("Mouse", 100m));
        service.CadastrarProduto(new Produto("Adaptador", 50m));

        List<string> nomes = service.ListarProdutosOrdenadosPorNome()
            .Select(produto => produto.Nome)
            .ToList();

        Assert.Equal(new List<string> { "Adaptador", "Mouse", "Teclado" }, nomes);
    }

    [Fact]
    public void ListarProdutosOrdenadosPorPreco_DeveOrdenarCorretamente()
    {
        ComexService service = new ComexService();
        service.CadastrarProduto(new Produto("Produto A", 300m));
        service.CadastrarProduto(new Produto("Produto B", 100m));
        service.CadastrarProduto(new Produto("Produto C", 200m));

        List<decimal> precos = service.ListarProdutosOrdenadosPorPreco()
            .Select(produto => produto.Preco)
            .ToList();

        Assert.Equal(new List<decimal> { 100m, 200m, 300m }, precos);
    }

    private static Cliente CriarCliente(string nome, string cpf)
    {
        Endereco endereco = new Endereco("Rua A", "10", "Cidade", "SP", "Centro", "Apto 1");
        return new Cliente(nome, cpf, "email@teste.com", "Dev", "11999999999", endereco);
    }
}
