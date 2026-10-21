namespace Comex;

public class Cliente : IIdentificavel
{
    public string Nome { get; }
    public string CPF { get; }

    public string Email { get; }
    public string Profissao { get; }  
    
    public string Telefone { get; }

    public Endereco Endereco { get; }
        
        
    public Cliente(string nome, string cpf, string email, string profissao, string telefone, Endereco endereco)
    {
        Nome = nome;
        CPF = cpf;
        Email = email;
        Profissao = profissao;
        Telefone = telefone;
        Endereco = endereco;
    }

    public string Identificar()
    {
        return $"Nome: {Nome}, CPF: {CPF}";
    }
}
