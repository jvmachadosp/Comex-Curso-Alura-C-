using System.Net.Http.Headers;
using Comex;

Dictionary<string, Cliente> clientesRegistrados = new();
Dictionary<string, Produto> produtosRegistrados = new();
Dictionary<string, List<Produto>> carrinhoCompras = new();

void ExibirTituloDaOpcao(string titulo)
{
    int quantidadeDeLetras = titulo.Length;
    string asteriscos = string.Empty.PadLeft(quantidadeDeLetras, '*');
    Console.WriteLine(asteriscos);
    Console.WriteLine(titulo);
    Console.WriteLine(asteriscos + "\n");
}

void ExibirMenuDeOpcoes()
{
    ExibirTituloDaOpcao("COMEX");
    Console.WriteLine("Selecione uma opção:");
    Console.WriteLine("Digite 1 para cadastrar cliente");
    Console.WriteLine("Digite 2 para listar clientes");
    Console.WriteLine("Digite 3 para cadastrar produto");
    Console.WriteLine("Digite 4 para alterar preço de produto");
    Console.WriteLine("Digite 5 para adicionar produto no carrinho");
    Console.WriteLine("Digite 6 para fechar compra");
    Console.WriteLine("Digite -1 para finalizar o programa");

    Console.Write("\nDigite a sua opção: ");
    string opcaoEscolhida = Console.ReadLine()!;
    int opcaoEscolhidaNumerica = int.Parse(opcaoEscolhida);

    switch (opcaoEscolhidaNumerica)
    {
        case 1:
            CadastraCliente();
            break;
        case 2:
            ListaClientes();
            break;
        case 3:
            CadastraProduto();
            break;
        case 4:
            AjustarPrecoDeProduto();
            break;
        case 5:
            AdicionaProdutoNoCarrinho();
            break;
        case 6:
            FechaCompra();
            break;
        case -1:
            Console.WriteLine("Tchau tchau :)");
            break;
        default:
            Console.WriteLine("Opção inválida");
            ExibirMenuDeOpcoes();
            break;
    }
}

void CadastraCliente()
{
    Console.Clear();
    ExibirTituloDaOpcao("Cadastro de clientes");
    Console.Write("Digite o nome do cliente: ");
    string nomeDoCliente = Console.ReadLine()!;
    Console.Write("Digite o CPF  do cliente: ");
    string cpfDoCliente = Console.ReadLine()!;
    Cliente cliente = new Cliente(nomeDoCliente, cpfDoCliente);
    clientesRegistrados.Add(nomeDoCliente, cliente);
    Console.WriteLine($"O cliente {nomeDoCliente} foi registrado com sucesso!");
    Console.WriteLine($"Usuário {nomeDoCliente} ({cpfDoCliente}) cadastrado com sucesso.");
    Thread.Sleep(3000);
    Console.Clear();
    ExibirMenuDeOpcoes();
}

void ListaClientes()
{
    Console.Clear();
    ExibirTituloDaOpcao("Lista de Clientes");
    foreach (var cliente in clientesRegistrados.Values)
    {
        Console.WriteLine($"Nome: {cliente.Nome}, CPF: {cliente.CPF}");
    }
    Console.WriteLine("\nPressione qualquer tecla para voltar ao menu...");
    Console.ReadKey();
    ExibirMenuDeOpcoes();
}

void CadastraProduto()
{
    Console.Clear();
    ExibirTituloDaOpcao("Cadastro de Produtos");
    Console.Write("Digite o nome do produto: ");
    string nomeDoProduto = Console.ReadLine()!;
    Console.Write("Digite o preço do produto: ");
    decimal precoDoProduto = decimal.Parse(Console.ReadLine()!);
    Produto produto = new Produto(nomeDoProduto, precoDoProduto);
    produtosRegistrados.Add(nomeDoProduto, produto);
    Console.WriteLine($"Produto {nomeDoProduto} cadastrado com sucesso.");
    Thread.Sleep(3000);
    Console.Clear();
    ExibirMenuDeOpcoes();
}

void AjustarPrecoDeProduto()
{
    Console.Clear();
    ExibirTituloDaOpcao("Ajustar preço de produto");

    Console.Write("Qual o nome do produto que você deseja alterar? ");
    string? produtoAlterarPreco = Console.ReadLine();

    if (string.IsNullOrWhiteSpace(produtoAlterarPreco))
    {
        Console.WriteLine("Nome de produto inválido.");
        ExibirMenuDeOpcoes();
        return;
    }

    if (produtosRegistrados.TryGetValue(produtoAlterarPreco, out Produto? produto))
    {
        Console.Write("Digite o novo preço: ");
        string? entradaPreco = Console.ReadLine();

        if (decimal.TryParse(entradaPreco, out decimal novoPreco))
        {
            produto.Preco = novoPreco;
            Console.WriteLine("Preço atualizado com sucesso.");
        }
        else
        {
            Console.WriteLine("Preço inválido.");
        }
    } 
    else
    {
        Console.WriteLine("Produto não cadastrado no sistema.");
    }
    ExibirMenuDeOpcoes();
}

void AdicionaProdutoNoCarrinho()
{
    Console.Clear();
    ExibirTituloDaOpcao("Adicionar produto ao carrinho");

    Console.Write("Qual usuário está comprando?");
    string usuarioComprando = Console.ReadLine()!;

    if (!clientesRegistrados.ContainsKey(usuarioComprando))
    {
        Console.WriteLine("Cliente não encontrado.");
        ExibirMenuDeOpcoes();
    }

    Console.Write("Qual o produto quer adicionar no carrinho?");
    string produtoAddCarrinho = Console.ReadLine()!;

    if (!produtosRegistrados.TryGetValue(produtoAddCarrinho, out Produto? produto))
    {
        Console.WriteLine("Produto não encontrado.");
        ExibirMenuDeOpcoes();
        return;
    }

    if (!carrinhoCompras.TryGetValue(usuarioComprando, out List<Produto>? listaDeProdutos))
    {
        listaDeProdutos = new List<Produto>();
        carrinhoCompras.Add(usuarioComprando, listaDeProdutos);
    }

    listaDeProdutos.Add(produto);

    Console.WriteLine($"Produto {produtoAddCarrinho} adicionado ao carrinho do usuário {usuarioComprando}");

    ExibirMenuDeOpcoes();
}

void FechaCompra()
{
    Console.Clear();
    ExibirTituloDaOpcao("Fechar compra");

    Console.Write("Qual cliente quer fechar a compra?");
    string clienteFechandoCompra = Console.ReadLine()!;
    if (carrinhoCompras.TryGetValue(clienteFechandoCompra, out List<Produto>? listaDeProdutos))
    {
        decimal totalCompra = 0;

        foreach (var produto in listaDeProdutos)
        {
            Console.WriteLine($"Produto: {produto.Nome} | Preco: {produto.Preco}");
            totalCompra = totalCompra + produto.Preco;
        }

        Console.WriteLine($"O total da compra foi {totalCompra}");

    } else
    {
        Console.WriteLine("Cliente não tem carrinho de compras.");
        ExibirMenuDeOpcoes();
    }




    ExibirMenuDeOpcoes();
}

ExibirMenuDeOpcoes();