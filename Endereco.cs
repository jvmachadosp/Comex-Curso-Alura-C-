namespace Comex;

public class Endereco
{
    public string Rua { get; }
    public string Numero { get; }
    public string Cidade { get; }
    public string Estado { get; }
    public string Bairro { get; }
    public string Complemento { get; }
    public Endereco(string rua, string numero, string cidade, string estado, string bairro, string complemento)
    {
        Rua = rua;
        Numero = numero;
        Cidade = cidade;
        Estado = estado;
        Bairro = bairro;
        Complemento = complemento;
    }
}