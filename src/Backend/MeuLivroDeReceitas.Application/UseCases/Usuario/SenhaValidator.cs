using FluentValidation;
using MeuLivroDeReceitas.Exception;

namespace MeuLivroDeReceitas.Application.UseCases.Usuario;

public class SenhaValidator : AbstractValidator<string>
{
    public SenhaValidator()
    {
        RuleFor(senha => senha).NotEmpty().WithMessage(ResourceErrorMessage.SENHA_USUARIO_VAZIO);
        When(senha => !string.IsNullOrWhiteSpace(senha), () =>
        {
            RuleFor(senha => senha.Length).GreaterThanOrEqualTo(6).WithMessage(ResourceErrorMessage.SENHA_USUARIO_MININO_6_CARACTERES);
        });
    }
}