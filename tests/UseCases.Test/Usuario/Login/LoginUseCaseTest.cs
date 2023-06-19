using FluentAssertions;
using MeuLivroDeReceitas.Application.UseCases.Login;
using MeuLivroDeReceitas.Exception.ExceptionBase;
using MeuLivroDeReceitas.Exception;
using Utilitario.Tests.Criptografia;
using Utilitario.Tests.Entidades;
using Utilitario.Tests.Repositorios;
using Utilitario.Tests.Token;
using Xunit;

namespace UseCases.Test.Login.FazerLogin;

public class LoginUseCaseTest
{
    [Fact]
    public async Task Validar_Sucesso()
    {
        (var usuario, var senha) = UsuarioBuilder.Construir();

        var useCase = CriarUseCase(usuario);

        var resposta = await useCase.Executar(new MeuLivroDeReceitas.Comunicacao.Request.RequestLoginJson
        {
            Email = usuario.Email,
            Senha = senha
        });

        resposta.Should().NotBeNull();
        resposta.Nome.Should().Be(usuario.Nome);
        resposta.Token.Should().NotBeNullOrWhiteSpace();
    }

    [Fact]
    public async Task Validar_Erro_Senha_Invalida()
    {
        (var usuario, var senha) = UsuarioBuilder.Construir();

        var useCase = CriarUseCase(usuario);

        Func<Task> acao = async () =>
        {
            await useCase.Executar(new MeuLivroDeReceitas.Comunicacao.Request.RequestLoginJson
            {
                Email = usuario.Email,
                Senha = "senhaInvalida"
            });
        };

        await acao.Should().ThrowAsync<LoginInvalidoException>()
            .Where(exception => exception.Message.Equals(ResourceErrorMessage.LOGIN_INVALIDO));
    }

    [Fact]
    public async Task Validar_Erro_Email_Invalido()
    {
        (var usuario, var senha) = UsuarioBuilder.Construir();

        var useCase = CriarUseCase(usuario);

        Func<Task> acao = async () =>
        {
            await useCase.Executar(new MeuLivroDeReceitas.Comunicacao.Request.RequestLoginJson
            {
                Email = "email@invalido.com",
                Senha = senha
            });
        };

        await acao.Should().ThrowAsync<LoginInvalidoException>()
            .Where(exception => exception.Message.Equals(ResourceErrorMessage.LOGIN_INVALIDO));
    }

    [Fact]
    public async Task Validar_Erro_Email_Senha_Invalido()
    {
        (var usuario, var senha) = UsuarioBuilder.Construir();

        var useCase = CriarUseCase(usuario);

        Func<Task> acao = async () =>
        {
            await useCase.Executar(new MeuLivroDeReceitas.Comunicacao.Request.RequestLoginJson
            {
                Email = "email@invalido.com",
                Senha = "senhaInvalida"
            });
        };

        await acao.Should().ThrowAsync<LoginInvalidoException>()
            .Where(exception => exception.Message.Equals(ResourceErrorMessage.LOGIN_INVALIDO));
    }

    private static LoginUseCase CriarUseCase(MeuLivroDeReceitas.Domain.Entidades.Usuario usuario)
    {
        var encriptador = EncriptadorDeSenhaBuilder.Instancia();
        var token = TokenControllerBuilder.Instancia();
        var repositorioReadOnly = UsuarioReadOnlyRepositorioBuilder.Instancia().Login(usuario).Construir();

        return new LoginUseCase(repositorioReadOnly, encriptador, token);
    }
}