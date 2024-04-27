using FluentValidation;

namespace Feature.Clientes;

public class ClienteValidator : AbstractValidator<Cliente>
{
    public ClienteValidator()
    {
        RuleFor(c => c.Nome)
            .NotEmpty().WithMessage("Por favor, certifique-se de ter inserido o nome")
            .Length(2, 150).WithMessage("O nome deve ter entre 2 e 150 caracteres");

        RuleFor(c => c.Sobrenome)
            .NotEmpty().WithMessage("Por favor, certifique-se de ter inserido o sobrenome")
            .Length(2, 150).WithMessage("O Sobrenome deve ter entre 2 e 150 caracteres");

        RuleFor(c => c.DataNascimento)
            .NotEmpty()
            .Must(HaveMinimumAge)
            .WithMessage("O cliente deve ter 18 anos ou mais");

        RuleFor(c => c.Email)
            .NotEmpty()
            .EmailAddress();

        RuleFor(c => c.Id)
            .NotEqual(Guid.Empty);
    }

    private static bool HaveMinimumAge(DateTime birthDate) => birthDate <= DateTime.Now.AddYears(-18);
}
