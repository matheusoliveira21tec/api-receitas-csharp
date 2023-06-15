using FluentAssertions;
using MeuLivroDeReceitas.Application.UseCases.Usuario.Registrar;
using MeuLivroDeReceitas.Comunicacao.Request;
using MeuLivroDeReceitas.Exception;
using Utilitario.Tests;
using Xunit;

namespace Validators.Test.Usuario.Registrar;

public class RegistrarUsuarioValidatorTest
{
    [Fact]
    public void Validar_Sucesso()
    {
        var validator = new RegistroUsuarioValidator();

        var request = RequestRegistrarUsuarioJsonBuilder.Construir();

        var resultado = validator.Validate(request);

        resultado.IsValid.Should().BeTrue();
    }
    [Fact]
    public void Validar_Erro_Nome_Vazio()
    {
        var validator = new RegistroUsuarioValidator();

        var request = RequestRegistrarUsuarioJsonBuilder.Construir();
        request.Nome = String.Empty;

        var resultado = validator.Validate(request);

        resultado.IsValid.Should().BeFalse();
        resultado.Errors.Should().ContainSingle().And.Contain(error => error.ErrorMessage.Equals(ResourceErrorMessage.NOME_USUARIO_VAZIO));
    }

    [Fact]
    public void Validar_Erro_Email_Vazio()
    {
        var validator = new RegistroUsuarioValidator();

        var request = RequestRegistrarUsuarioJsonBuilder.Construir();
        request.Email = String.Empty;

        var resultado = validator.Validate(request);

        resultado.IsValid.Should().BeFalse();
        resultado.Errors.Should().ContainSingle().And.Contain(error => error.ErrorMessage.Equals(ResourceErrorMessage.EMAIL_USUARIO_VAZIO));
    }

    [Fact]
    public void Validar_Erro_Telefone_Vazio()
    {
        var validator = new RegistroUsuarioValidator();

        var request = RequestRegistrarUsuarioJsonBuilder.Construir();
        request.Telefone = String.Empty;

        var resultado = validator.Validate(request);

        resultado.IsValid.Should().BeFalse();
        resultado.Errors.Should().ContainSingle().And.Contain(error => error.ErrorMessage.Equals(ResourceErrorMessage.TELEFONE_USUARIO_VAZIO));
    }

    [Fact]
    public void Validar_Erro_Senha_Vazia()
    {
        var validator = new RegistroUsuarioValidator();

        var request = RequestRegistrarUsuarioJsonBuilder.Construir();
        request.Senha = String.Empty;

        var resultado = validator.Validate(request);

        resultado.IsValid.Should().BeFalse();
        resultado.Errors.Should().ContainSingle().And.Contain(error => error.ErrorMessage.Equals(ResourceErrorMessage.SENHA_USUARIO_VAZIO));
    }

    [Theory]
    [InlineData(1)]
    [InlineData(2)]
    [InlineData(3)]
    [InlineData(4)]
    [InlineData(5)]
    public void Validar_Erro_Senha_Minimo_6_Caracteres(int tamanhoSenha)
    {
        var validator = new RegistroUsuarioValidator();

        var request = RequestRegistrarUsuarioJsonBuilder.Construir(tamanhoSenha);

        var resultado = validator.Validate(request);

        resultado.IsValid.Should().BeFalse();
        resultado.Errors.Should().ContainSingle().And.Contain(error => error.ErrorMessage.Equals(ResourceErrorMessage.SENHA_USUARIO_MININO_6_CARACTERES));
    }

    [Fact]
    public void Validar_Erro_Email_Invalido()
    {
        var validator = new RegistroUsuarioValidator();

        var request = RequestRegistrarUsuarioJsonBuilder.Construir();
        request.Email = "email";

        var resultado = validator.Validate(request);

        resultado.IsValid.Should().BeFalse();
        resultado.Errors.Should().ContainSingle().And.Contain(error => error.ErrorMessage.Equals(ResourceErrorMessage.EMAIL_USUARIO_INVALIDO));
    }

    [Fact]
    public void Validar_Erro_Telefone_Invalido()
    {
        var validator = new RegistroUsuarioValidator();

        var request = RequestRegistrarUsuarioJsonBuilder.Construir();
        request.Telefone = "986326147";

        var resultado = validator.Validate(request);

        resultado.IsValid.Should().BeFalse();
        resultado.Errors.Should().ContainSingle().And.Contain(error => error.ErrorMessage.Equals(ResourceErrorMessage.TELEFONE_USUARIO_INVALIDO));
    }
}
