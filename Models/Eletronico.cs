namespace Comex;

public class Eletronico : Produto
{
    public int Voltagem { get; set; }
    public int Potencia { get; set; }

    public Eletronico(string nome, decimal preco, int voltagem, int potencia) : base(nome, preco)
    {
        Voltagem = voltagem;
        Potencia = potencia;
    }
}