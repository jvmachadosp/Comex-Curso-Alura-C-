namespace Comex;

public class Livro : Produto, IIdentificavel
{
    public string Isbn { get; set; }
    public int TotalPaginas { get; set; }

    public Livro(string nome, decimal preco, string isbn, int totalPaginas) : base(nome, preco)
    {
        Isbn = isbn;
        TotalPaginas = totalPaginas;
    }

    public string Identificar()
    {
        return $"Nome: {Nome}, ISBN: {Isbn}";
    }
}