using FluentValidation;
using MeuLivroDeReceitas.Comunicacao.Request;
using MeuLivroDeReceitas.Exception;
using System.Text.RegularExpressions;

namespace MeuLivroDeReceitas.Application.UseCases.Usuario.Registrar;

public class RegistroUsuarioValidator : AbstractValidator<RequestRegistrarUsuarioJson>
{
    public RegistroUsuarioValidator()
    {
        RuleFor(u => u.Nome).NotEmpty().WithMessage(ResourceErrorMessage.NOME_USUARIO_VAZIO);
        RuleFor(u => u.Email).NotEmpty().WithMessage(ResourceErrorMessage.EMAIL_USUARIO_VAZIO);
        RuleFor(u => u.Telefone).NotEmpty().WithMessage(ResourceErrorMessage.TELEFONE_USUARIO_VAZIO);
        RuleFor(u => u.Senha).NotEmpty().WithMessage(ResourceErrorMessage.SENHA_USUARIO_VAZIO);
        When(u => !string.IsNullOrWhiteSpace(u.Email), () =>
        {
            RuleFor(u => u.Email).EmailAddress().WithMessage(ResourceErrorMessage.EMAIL_USUARIO_INVALIDO);
        });
        When(u => !string.IsNullOrWhiteSpace(u.Senha), () =>
        {
            RuleFor(u => u.Senha.Length).GreaterThanOrEqualTo(6).WithMessage(ResourceErrorMessage.SENHA_USUARIO_MININO_6_CARACTERES);
        });
        When(u => !string.IsNullOrWhiteSpace(u.Telefone), () =>
        {
            RuleFor(u => u.Telefone).Custom((telefone, contexto) =>
            {
                string padraoTelefone = "[0-9]{2} [1-9]{1} [0-9]{4}-[0-9]{4}";
                var isMatch = Regex.IsMatch(telefone, padraoTelefone);
                if (!isMatch)
                {
                    contexto.AddFailure(new FluentValidation.Results.ValidationFailure(nameof(telefone), ResourceErrorMessage.TELEFONE_USUARIO_INVALIDO));
                }
            });
        });
    }
}
