using FluentAssertions;
using MeuLivroDeReceitas.Application.UseCases.Receita.Registrar;
using MeuLivroDeReceitas.Exception;
using MeuLivroDeReceitas.Exception.ExceptionBase;
using Utilitario.Tests.Entidades;
using Utilitario.Tests.Mapper;
using Utilitario.Tests.Repositorios;
using Utilitario.Tests.Request;
using Utilitario.Tests.UsuarioLogado;
using Xunit;

namespace UseCases.Test.Receita.Registrar;
public class RegistrarReceitaUseCaseTest
{
    [Fact]
    public async Task Validar_Sucesso()
    {
        (var usuario, var _) = UsuarioBuilder.Construir();

        var useCase = CriarUseCase(usuario);

        var requisicao = RequestReceitaBuilder.Construir();

        var resposta = await useCase.Executar(requisicao);

        resposta.Should().NotBeNull();

        resposta.Id.Should().NotBeNullOrWhiteSpace();
        resposta.Titulo.Should().Be(requisicao.Titulo);
        resposta.Categoria.Should().Be(requisicao.Categoria);
        resposta.ModoPreparo.Should().Be(requisicao.ModoPreparo);
        resposta.TempoPreparo.Should().Be(requisicao.TempoPreparo);
    }

    [Fact]
    public async Task Validar_Erro_Email_Vazio()
    {
        (var usuario, var senha) = UsuarioBuilder.Construir();

        var useCase = CriarUseCase(usuario);

        var requisicao = RequestReceitaBuilder.Construir();
        requisicao.Ingredientes.Clear();

        Func<Task> acao = async () => { await useCase.Executar(requisicao); };

        await acao.Should().ThrowAsync<ErrosDeValidacaoException>()
            .Where(exception => exception.Errors.Count == 1 && exception.Errors.Contains(ResourceErrorMessage.RECEITA_MINIMO_UM_INGREDIENTE));
    }

    private static RegistrarReceitaUseCase CriarUseCase(MeuLivroDeReceitas.Domain.Entidades.Usuario usuario)
    {
        var usuarioLogado = UsuarioLogadoBuilder.Instancia().RecuperarUsuario(usuario).Construir();
        var mapper = MapperBuilder.Instancia();
        var repositorio = ReceitaWriteOnlyRepositorioBuilder.Instancia().Construir();
        var unidadeDeTrabalho = UnidadeDeTrabalhoBuilder.Instancia().Construir();

        return new RegistrarReceitaUseCase(mapper, unidadeDeTrabalho, usuarioLogado, repositorio);
    }
}
