using Bogus;
using MeuLivroDeReceitas.Comunicacao.Request;

namespace Utilitario.Tests.Request;

public class RequestRegistrarUsuarioJsonBuilder
{
    public static RequestRegistrarUsuarioJson Construir(int tamanhoSenha = 10)
    {
        return new Faker<RequestRegistrarUsuarioJson>()
            .RuleFor(r => r.Nome, f => f.Person.FullName)
            .RuleFor(r => r.Email, f => f.Internet.Email())
            .RuleFor(r => r.Senha, f => f.Internet.Password(tamanhoSenha))
            .RuleFor(r => r.Telefone, f => f.Phone.PhoneNumber("## ! ####-####").Replace("!", $"{f.Random.Int(min: 1, max: 9)}"));
    }
}
