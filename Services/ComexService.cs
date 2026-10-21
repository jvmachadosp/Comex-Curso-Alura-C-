namespace Comex;

public class ComexService
{
    private readonly Dictionary<string, Cliente> clientesRegistrados = new();
    private readonly Dictionary<string, Produto> produtosRegistrados = new();
    private readonly Dictionary<string, List<Produto>> carrinhoCompras = new();

    public bool CadastrarCliente(Cliente cliente)
    {
        if (clientesRegistrados.ContainsKey(cliente.Nome))
        {
            return false;
        }

        clientesRegistrados.Add(cliente.Nome, cliente);
        return true;
    }

    public IEnumerable<Cliente> ListarClientes()
    {
        return clientesRegistrados.Values;
    }

    public bool CadastrarProduto(Produto produto)
    {
        if (produtosRegistrados.ContainsKey(produto.Nome))
        {
            return false;
        }

        produtosRegistrados.Add(produto.Nome, produto);
        return true;
    }

    public IEnumerable<Produto> ListarProdutosOrdenadosPorNome()
    {
        return produtosRegistrados.Values.OrderBy(produto => produto.Nome);
    }

    public IEnumerable<Produto> ListarProdutosOrdenadosPorPreco()
    {
        return produtosRegistrados.Values
            .OrderBy(produto => produto.Preco)
            .ThenBy(produto => produto.Nome);
    }

    public bool TentarAjustarPrecoDeProduto(string nomeDoProduto, decimal novoPreco)
    {
        if (!produtosRegistrados.TryGetValue(nomeDoProduto, out Produto? produto))
        {
            return false;
        }

        produto.Preco = novoPreco;
        return true;
    }

    public bool ClienteExiste(string nomeDoCliente)
    {
        return clientesRegistrados.ContainsKey(nomeDoCliente);
    }

    public bool TentarAdicionarProdutoNoCarrinho(string nomeDoCliente, string nomeDoProduto)
    {
        if (!clientesRegistrados.ContainsKey(nomeDoCliente))
        {
            return false;
        }

        if (!produtosRegistrados.TryGetValue(nomeDoProduto, out Produto? produto))
        {
            return false;
        }

        if (!carrinhoCompras.TryGetValue(nomeDoCliente, out List<Produto>? listaDeProdutos))
        {
            listaDeProdutos = new List<Produto>();
            carrinhoCompras.Add(nomeDoCliente, listaDeProdutos);
        }

        listaDeProdutos.Add(produto);
        return true;
    }

    public bool TentarFecharCompra(string nomeDoCliente, out List<Produto> produtosDoCarrinho, out decimal totalCompra)
    {
        produtosDoCarrinho = new List<Produto>();
        totalCompra = 0;

        if (!carrinhoCompras.TryGetValue(nomeDoCliente, out List<Produto>? listaDeProdutos))
        {
            return false;
        }

        produtosDoCarrinho = new List<Produto>(listaDeProdutos);

        foreach (var produto in listaDeProdutos)
        {
            totalCompra += produto.Preco;
        }

        return true;
    }
}
