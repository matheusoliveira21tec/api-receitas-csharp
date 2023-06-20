using FluentAssertions;
using MeuLivroDeReceitas.Application.UseCases.Usuario.AlterarSenha;
using MeuLivroDeReceitas.Exception;
using Utilitario.ParaOsTestes.Requisicoes;
using Utilitario.Tests.Request;
using Xunit;

namespace Validators.Test.Usuario.AlterarSenha;

public class AlterarSenhaValidatorTest
{
    [Fact]
    public void Validar_Sucesso()
    {
        var validator = new AlterarSenhaValidator();

        var requisicao = RequestAlterarSenhaJsonBuilder.Construir();

        var resultado = validator.Validate(requisicao);

        resultado.IsValid.Should().BeTrue();
    }

    [Theory]
    [InlineData(1)]
    [InlineData(2)]
    [InlineData(3)]
    [InlineData(4)]
    [InlineData(5)]
    public void Validar_Erro_Senha_Invalido(int tamanhoSenha)
    {
        var validator = new AlterarSenhaValidator();

        var requisicao = RequestAlterarSenhaJsonBuilder.Construir(tamanhoSenha);

        var resultado = validator.Validate(requisicao);

        resultado.IsValid.Should().BeFalse();
        resultado.Errors.Should().ContainSingle().And.Contain(error => error.ErrorMessage.Equals(ResourceErrorMessage.SENHA_USUARIO_MININO_6_CARACTERES));
    }

    [Fact]
    public void Validar_Erro_Senha_Vazio()
    {
        var validator = new AlterarSenhaValidator();

        var requisicao = RequestAlterarSenhaJsonBuilder.Construir();
        requisicao.NovaSenha = string.Empty;

        var resultado = validator.Validate(requisicao);

        resultado.IsValid.Should().BeFalse();
        resultado.Errors.Should().ContainSingle().And.Contain(error => error.ErrorMessage.Equals(ResourceErrorMessage.SENHA_USUARIO_VAZIO));
    }
}
