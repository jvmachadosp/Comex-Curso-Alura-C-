namespace Comex;

public class Produto
{
    public string Nome { get; }
    public decimal Preco { get; set;}
    
    public int Quantidade { get; set; }

    public string Descricao { get; set; } = string.Empty;
    public Produto(string nome, decimal preco)
    {
        Nome = nome;
        Preco = preco;
    }
}