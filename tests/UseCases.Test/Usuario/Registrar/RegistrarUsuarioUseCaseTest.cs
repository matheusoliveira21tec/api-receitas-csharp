using FluentAssertions;
using MeuLivroDeReceitas.Application.UseCases.Usuario.Registrar;
using MeuLivroDeReceitas.Exception.ExceptionBase;
using MeuLivroDeReceitas.Exception;
using Utilitario.Tests.Criptografia;
using Utilitario.Tests.Mapper;
using Utilitario.Tests.Repositorios;
using Utilitario.Tests.Request;
using Utilitario.Tests.Token;
using Xunit;

namespace UseCases.Test.Usuario.Registrar;

public class RegistrarUsuarioUseCaseTest
{
    [Fact]
    public async Task Validar_Sucesso()
    {
        var requisicao = RequestRegistrarUsuarioJsonBuilder.Construir();

        var useCase = CriarUseCase();

        var resposta = await useCase.Executar(requisicao);

        resposta.Should().NotBeNull();
        resposta.Token.Should().NotBeNullOrWhiteSpace();
    }

    [Fact]
    public async Task Validar_Erro_Email_Ja_Registrado()
    {
        var requisicao = RequestRegistrarUsuarioJsonBuilder.Construir();

        var useCase = CriarUseCase(requisicao.Email);

        Func<Task> acao = async () => { await useCase.Executar(requisicao); };

        await acao.Should().ThrowAsync<ErrosDeValidacaoException>()
            .Where(exception => exception.Errors.Count == 1 && exception.Errors.Contains(ResourceErrorMessage.EMAIL_JA_CADASTRADO));
    }

    [Fact]
    public async Task Validar_Erro_Email_Vazio()
    {
        var requisicao = RequestRegistrarUsuarioJsonBuilder.Construir();
        requisicao.Email = string.Empty;

        var useCase = CriarUseCase();

        Func<Task> acao = async () => { await useCase.Executar(requisicao); };

        await acao.Should().ThrowAsync<ErrosDeValidacaoException>()
            .Where(exception => exception.Errors.Count == 1 && exception.Errors.Contains(ResourceErrorMessage.EMAIL_USUARIO_VAZIO));
    }

    private static RegistroUsuarioUseCase CriarUseCase(string email = "")
    {
        var mapper = MapperBuilder.Instancia();
        var repositorio = UsuarioWriteOnlyRepositorioBuilder.Instancia().Construir();
        var unidadeDeTrabalho = UnidadeDeTrabalhoBuilder.Instancia().Construir();
        var encriptador = EncriptadorDeSenhaBuilder.Instancia();
        var token = TokenControllerBuilder.Instancia();
        var repositorioReadOnly = UsuarioReadOnlyRepositorioBuilder.Instancia().ExisteUsuarioComEmail(email).Construir();

        return new RegistroUsuarioUseCase(repositorio, mapper, unidadeDeTrabalho, encriptador, token, repositorioReadOnly);
    }
}