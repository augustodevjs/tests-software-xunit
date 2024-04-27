using Feature.Core;

namespace Feature.Clientes;

public class Cliente : Entity
{
    public bool Ativo { get; set; }
    public string Nome { get; set; }
    public string Email { get; set; }
    public string Sobrenome { get; set; }
    public DateTime DataCadastro { get; set; }
    public DateTime DataNascimento { get; set; }

    protected Cliente() { }

    public Cliente(
        Guid id, 
        string nome, 
        string sobrenome, 
        DateTime dataNascimento, 
        string email, 
        bool ativo,
        DateTime dataCadastro
    )
    {
        Id = id;
        Nome = nome;
        Email = email;
        Ativo = ativo;
        Sobrenome = sobrenome;
        DataCadastro = dataCadastro;
        DataNascimento = dataNascimento;
    }

    public string NomeCompleto() => $"{Nome} {Sobrenome}";

    public bool EhEspecial() => DataCadastro < DateTime.Now.AddYears(-3) && Ativo;

    public void Inativar() => Ativo = false;

    public override bool EhValido()
    {
        ValidationResult = new ClienteValidator().Validate(this);
        return ValidationResult.IsValid;
    }
}

