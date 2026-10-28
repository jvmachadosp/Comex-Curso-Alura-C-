namespace Comex;

public class Menu
{
    private readonly ComexService service;

    public Menu(ComexService service)
    {
        this.service = service;
    }

    public async Task ExibirMenu()
    {
        bool executando = true;

        while (executando)
        {
            ExibirTituloDaOpcao("COMEX");
            Console.WriteLine("Selecione uma opção:");
            Console.WriteLine("Digite 1 para cadastrar cliente");
            Console.WriteLine("Digite 2 para listar clientes");
            Console.WriteLine("Digite 3 para cadastrar produto");
            Console.WriteLine("Digite 4 para alterar preço de produto");
            Console.WriteLine("Digite 5 para adicionar produto no carrinho");
            Console.WriteLine("Digite 6 para fechar compra");
            Console.WriteLine("Digite 7 para listar produtos por nome");
            Console.WriteLine("Digite 8 para listar produtos por preço");
            Console.WriteLine("Digite 9 para buscar produtos em API externa");
            Console.WriteLine("Digite -1 para finalizar o programa");

            Console.Write("\nDigite a sua opção: ");
            string opcaoEscolhida = Console.ReadLine()!;

            if (!int.TryParse(opcaoEscolhida, out int opcaoEscolhidaNumerica))
            {
                Console.WriteLine("Opção inválida");
                continue;
            }

            switch (opcaoEscolhidaNumerica)
            {
                case 1:
                    CadastrarCliente();
                    break;
                case 2:
                    ListarClientes();
                    break;
                case 3:
                    CadastrarProduto();
                    break;
                case 4:
                    AjustarPrecoDeProduto();
                    break;
                case 5:
                    AdicionarProdutoNoCarrinho();
                    break;
                case 6:
                    FecharCompra();
                    break;
                case 7:
                    ListarProdutosPorNome();
                    break;
                case 8:
                    ListarProdutosPorPreco();
                    break;
                case 9:
                    await ConsultarApiExterna();
                    break;
                case -1:
                    Console.WriteLine("Tchau tchau :)");
                    executando = false;
                    break;
                default:
                    Console.WriteLine("Opção inválida");
                    break;
            }
        }
    }

    private static void ExibirTituloDaOpcao(string titulo)
    {
        int quantidadeDeLetras = titulo.Length;
        string asteriscos = string.Empty.PadLeft(quantidadeDeLetras, '*');
        Console.WriteLine(asteriscos);
        Console.WriteLine(titulo);
        Console.WriteLine(asteriscos + "\n");
    }

    private void CadastrarCliente()
    {
        Console.Clear();
        ExibirTituloDaOpcao("Cadastro de clientes");
        Console.Write("Digite o nome do cliente: ");
        string nomeDoCliente = Console.ReadLine()!;
        Console.Write("Digite o CPF  do cliente: ");
        string cpfDoCliente = Console.ReadLine()!;
        Console.Write("Digite o email do cliente: ");
        string emailDoCliente = Console.ReadLine()!;
        Console.Write("Digite a profissão do cliente: ");
        string profissaoDoCliente = Console.ReadLine()!;
        Console.Write("Digite o telefone do cliente: ");
        string telefoneDoCliente = Console.ReadLine()!;

        Console.Write("Digite a rua do cliente: ");
        string ruaDoCliente = Console.ReadLine()!;
        Console.Write("Digite o número da casa: ");
        string numeroDoCliente = Console.ReadLine()!;
        Console.Write("Digite a cidade: ");
        string cidadeDoCliente = Console.ReadLine()!;
        Console.Write("Digite o estado: ");
        string estadoDoCliente = Console.ReadLine()!;
        Console.Write("Digite o bairro: ");
        string bairroDoCliente = Console.ReadLine()!;
        Console.Write("Digite o complemento: ");
        string complementoDoCliente = Console.ReadLine()!;

        Endereco enderecoDoCliente = new Endereco(
            ruaDoCliente,
            numeroDoCliente,
            cidadeDoCliente,
            estadoDoCliente,
            bairroDoCliente,
            complementoDoCliente
        );

        Cliente cliente = new Cliente(
            nomeDoCliente,
            cpfDoCliente,
            emailDoCliente,
            profissaoDoCliente,
            telefoneDoCliente,
            enderecoDoCliente
        );

        if (service.CadastrarCliente(cliente))
        {
            Console.WriteLine($"O cliente {nomeDoCliente} foi registrado com sucesso!");
            Console.WriteLine($"Usuário {nomeDoCliente} ({cpfDoCliente}) cadastrado com sucesso.");
        }
        else
        {
            Console.WriteLine("Cliente já cadastrado.");
        }

        Thread.Sleep(3000);
        Console.Clear();
    }

    private void ListarClientes()
    {
        Console.Clear();
        ExibirTituloDaOpcao("Lista de Clientes");

        foreach (var cliente in service.ListarClientes())
        {
            Console.WriteLine($"Nome: {cliente.Nome}, CPF: {cliente.CPF}");
        }

        Console.WriteLine("\nPressione qualquer tecla para voltar ao menu...");
        Console.ReadKey();
    }

    private void CadastrarProduto()
    {
        Console.Clear();
        ExibirTituloDaOpcao("Cadastro de Produtos");
        Console.Write("Digite o nome do produto: ");
        string nomeDoProduto = Console.ReadLine()!;
        Console.Write("Digite o preço do produto: ");
        decimal precoDoProduto = decimal.Parse(Console.ReadLine()!);
        Produto produto = new Produto(nomeDoProduto, precoDoProduto);

        Console.Write("Deseja adicionar descrição? (s/n): ");
        string adicionarDescricao = Console.ReadLine()!;
        if (adicionarDescricao.Trim().ToLower() == "s")
        {
            Console.Write("Digite a descrição do produto: ");
            produto.Descricao = Console.ReadLine()!;
        }

        Console.Write("Deseja adicionar quantidade? (s/n): ");
        string adicionarQuantidade = Console.ReadLine()!;
        if (adicionarQuantidade.Trim().ToLower() == "s")
        {
            Console.Write("Digite a quantidade do produto: ");
            if (int.TryParse(Console.ReadLine(), out int quantidadeProduto))
            {
                produto.Quantidade = quantidadeProduto;
            }
        }

        if (service.CadastrarProduto(produto))
        {
            Console.WriteLine($"Produto {nomeDoProduto} cadastrado com sucesso.");
        }
        else
        {
            Console.WriteLine("Produto já cadastrado.");
        }

        Thread.Sleep(3000);
        Console.Clear();
    }

    private void AjustarPrecoDeProduto()
    {
        Console.Clear();
        ExibirTituloDaOpcao("Ajustar preço de produto");

        Console.Write("Qual o nome do produto que você deseja alterar? ");
        string? produtoAlterarPreco = Console.ReadLine();

        if (string.IsNullOrWhiteSpace(produtoAlterarPreco))
        {
            Console.WriteLine("Nome de produto inválido.");
            return;
        }

        Console.Write("Digite o novo preço: ");
        string? entradaPreco = Console.ReadLine();

        if (!decimal.TryParse(entradaPreco, out decimal novoPreco))
        {
            Console.WriteLine("Preço inválido.");
            return;
        }

        if (service.TentarAjustarPrecoDeProduto(produtoAlterarPreco, novoPreco))
        {
            Console.WriteLine("Preço atualizado com sucesso.");
            return;
        }

        Console.WriteLine("Produto não cadastrado no sistema.");
    }

    private void AdicionarProdutoNoCarrinho()
    {
        Console.Clear();
        ExibirTituloDaOpcao("Adicionar produto ao carrinho");

        Console.Write("Qual usuário está comprando?");
        string usuarioComprando = Console.ReadLine()!;

        if (!service.ClienteExiste(usuarioComprando))
        {
            Console.WriteLine("Cliente não encontrado.");
            return;
        }

        Console.Write("Qual o produto quer adicionar no carrinho?");
        string produtoAddCarrinho = Console.ReadLine()!;

        if (!service.TentarAdicionarProdutoNoCarrinho(usuarioComprando, produtoAddCarrinho))
        {
            Console.WriteLine("Produto não encontrado.");
            return;
        }

        Console.WriteLine($"Produto {produtoAddCarrinho} adicionado ao carrinho do usuário {usuarioComprando}");
    }

    private void FecharCompra()
    {
        Console.Clear();
        ExibirTituloDaOpcao("Fechar compra");

        Console.Write("Qual cliente quer fechar a compra?");
        string clienteFechandoCompra = Console.ReadLine()!;

        if (!service.TentarFecharCompra(clienteFechandoCompra, out List<Produto> listaDeProdutos, out decimal totalCompra))
        {
            Console.WriteLine("Cliente não tem carrinho de compras.");
            return;
        }

        foreach (var produto in listaDeProdutos)
        {
            Console.WriteLine($"Produto: {produto.Nome} | Preco: {produto.Preco}");
        }

        Console.WriteLine($"O total da compra foi {totalCompra}");
    }

    private void ListarProdutosPorNome()
    {
        Console.Clear();
        ExibirTituloDaOpcao("Produtos por nome");

        foreach (var produto in service.ListarProdutosOrdenadosPorNome())
        {
            Console.WriteLine($"Produto: {produto.Nome} | Preco: {produto.Preco}");
        }

        Console.WriteLine("\nPressione qualquer tecla para voltar ao menu...");
        Console.ReadKey();
    }

    private void ListarProdutosPorPreco()
    {
        Console.Clear();
        ExibirTituloDaOpcao("Produtos por preço");

        foreach (var produto in service.ListarProdutosOrdenadosPorPreco())
        {
            Console.WriteLine($"Produto: {produto.Nome} | Preco: {produto.Preco}");
        }

        Console.WriteLine("\nPressione qualquer tecla para voltar ao menu...");
        Console.ReadKey();
    }

    private async Task ConsultarApiExterna()
    {
        Console.Clear();
        ExibirTituloDaOpcao("Consulta API externa");

        try
        {
            List<Produto> produtosExternos = await service.BuscarProdutosExternosAsync();

            Console.WriteLine("Consulta realizada com sucesso.");
            Console.WriteLine("Produtos desserializados:");

            foreach (Produto produto in produtosExternos)
            {
                Console.WriteLine($"Nome: {produto.Nome} | Preço: {produto.Preco} | Descrição: {produto.Descricao}");
            }
        }
        catch (HttpRequestException ex)
        {
            Console.WriteLine($"Erro ao consultar a API externa: {ex.Message}");
        }
        catch (System.Text.Json.JsonException ex)
        {
            Console.WriteLine($"Erro ao desserializar resposta da API: {ex.Message}");
        }
        catch (TaskCanceledException)
        {
            Console.WriteLine("A consulta da API externa excedeu o tempo limite.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Erro inesperado durante a consulta: {ex.Message}");
        }

        Console.WriteLine("\nPressione qualquer tecla para voltar ao menu...");
        Console.ReadKey();
    }
}
