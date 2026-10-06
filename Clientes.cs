namespace Comex;

public class Cliente
{
    public string Nome { get; }
    public string CPF { get; }

    public Cliente(string nome, string cpf)
    {
        Nome = nome;
        CPF = cpf;
    }
}
