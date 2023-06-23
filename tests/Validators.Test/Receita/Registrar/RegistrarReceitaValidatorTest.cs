using FluentAssertions;
using MeuLivroDeReceitas.Application.UseCases.Receita.Registrar;
using MeuLivroDeReceitas.Comunicacao.Enum;
using MeuLivroDeReceitas.Exception;
using Utilitario.Tests.Request;
using Xunit;

namespace Validators.Test.Receita.Registrar;
public class RegistrarReceitaValidatorTest
{
    [Fact]
    public void Validar_Sucesso()
    {
        var validator = new RegistrarReceitaValidator();

        var requisicao = RequestReceitaBuilder.Construir();

        var resultado = validator.Validate(requisicao);

        resultado.IsValid.Should().BeTrue();
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    [InlineData(1001)]
    [InlineData(1002)]
    [InlineData(100000)]
    public void Validar_Erro_Tempo_Preparo_Invalido(int tempo)
    {
        var validator = new RegistrarReceitaValidator();

        var requisicao = RequestReceitaBuilder.Construir();
        requisicao.TempoPreparo = tempo;

        var resultado = validator.Validate(requisicao);

        resultado.IsValid.Should().BeFalse();
        resultado.Errors.Should().ContainSingle().And.Contain(error => error.ErrorMessage.Equals(ResourceErrorMessage.TEMPO_PREPARO_INVALIDO));
    }

    [Fact]
    public void Validar_Erro_Titulo_Vazio()
    {
        var validator = new RegistrarReceitaValidator();

        var requisicao = RequestReceitaBuilder.Construir();
        requisicao.Titulo = string.Empty;

        var resultado = validator.Validate(requisicao);

        resultado.IsValid.Should().BeFalse();
        resultado.Errors.Should().ContainSingle().And.Contain(error => error.ErrorMessage.Equals(ResourceErrorMessage.TITULO_RECEITA_VAZIO));
    }

    [Fact]
    public void Validar_Erro_Categoria_Invalida()
    {
        var validator = new RegistrarReceitaValidator();

        var requisicao = RequestReceitaBuilder.Construir();
        requisicao.Categoria = (Categoria)1000;

        var resultado = validator.Validate(requisicao);

        resultado.IsValid.Should().BeFalse();
        resultado.Errors.Should().ContainSingle().And.Contain(error => error.ErrorMessage.Equals(ResourceErrorMessage.CATEGORIA_RECEITA_INVALIDA));
    }

    [Fact]
    public void Validar_Erro_ModoPreparo_Vazio()
    {
        var validator = new RegistrarReceitaValidator();

        var requisicao = RequestReceitaBuilder.Construir();
        requisicao.ModoPreparo = string.Empty;

        var resultado = validator.Validate(requisicao);

        resultado.IsValid.Should().BeFalse();
        resultado.Errors.Should().ContainSingle().And.Contain(error => error.ErrorMessage.Equals(ResourceErrorMessage.MODOPREPARO_RECEITA_VAZIO));
    }

    [Fact]
    public void Validar_Erro_ListaIngredientes_Vazio()
    {
        var validator = new RegistrarReceitaValidator();

        var requisicao = RequestReceitaBuilder.Construir();
        requisicao.Ingredientes.Clear();

        var resultado = validator.Validate(requisicao);

        resultado.IsValid.Should().BeFalse();
        resultado.Errors.Should().ContainSingle().And.Contain(error => error.ErrorMessage.Equals(ResourceErrorMessage.RECEITA_MINIMO_UM_INGREDIENTE));
    }

    [Fact]
    public void Validar_Erro_Produto_Ingrediente_Vazio()
    {
        var validator = new RegistrarReceitaValidator();

        var requisicao = RequestReceitaBuilder.Construir();
        requisicao.Ingredientes.First().Produto = string.Empty;

        var resultado = validator.Validate(requisicao);

        resultado.IsValid.Should().BeFalse();
        resultado.Errors.Should().ContainSingle().And.Contain(error => error.ErrorMessage.Equals(ResourceErrorMessage.RECEITA_INGREDIENTE_PRODUTO_VAZIO));
    }

    [Fact]
    public void Validar_Erro_Quantidade_Ingrediente_Vazio()
    {
        var validator = new RegistrarReceitaValidator();

        var requisicao = RequestReceitaBuilder.Construir();
        requisicao.Ingredientes.First().Quantidade = string.Empty;

        var resultado = validator.Validate(requisicao);

        resultado.IsValid.Should().BeFalse();
        resultado.Errors.Should().ContainSingle().And.Contain(error => error.ErrorMessage.Equals(ResourceErrorMessage.RECEITA_INGREDIENTE_QUANTIDADE_VAZIO));
    }

    [Fact]
    public void Validar_Erro_Ingrediente_Repetido()
    {
        var validator = new RegistrarReceitaValidator();

        var requisicao = RequestReceitaBuilder.Construir();
        requisicao.Ingredientes.Add(requisicao.Ingredientes.First());

        var resultado = validator.Validate(requisicao);

        resultado.IsValid.Should().BeFalse();
        resultado.Errors.Should().ContainSingle().And.Contain(error => error.ErrorMessage.Equals(ResourceErrorMessage.RECEITA_INGREDIENTES_REPETIDOS));
    }
}
