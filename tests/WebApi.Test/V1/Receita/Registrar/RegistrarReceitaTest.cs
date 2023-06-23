﻿using FluentAssertions;
using MeuLivroDeReceitas.Exception;
using System.Net;
using System.Text.Json;
using Utilitario.Tests.Request;
using Xunit;

namespace WebApi.Test.V1.Receita.Registrar;
public class RegistrarReceitaTest : ControllerBase
{
    private const string METODO = "receitas";

    private MeuLivroDeReceitas.Domain.Entidades.Usuario _usuario;
    private string _senha;

    public RegistrarReceitaTest(MeuLivroReceitaWebApplicationFactory<Program> factory) : base(factory)
    {
        _usuario = factory.RecuperarUsuario();
        _senha = factory.RecuperarSenha();
    }

    [Fact]
    public async Task Validar_Sucesso()
    {
        var token = await Login(_usuario.Email, _senha);

        var requisicao = RequestReceitaBuilder.Construir();

        var resposta = await PostRequest(METODO, requisicao, token);

        resposta.StatusCode.Should().Be(HttpStatusCode.Created);

        await using var responstaBody = await resposta.Content.ReadAsStreamAsync();

        var responseData = await JsonDocument.ParseAsync(responstaBody);

        responseData.RootElement.GetProperty("id").GetString().Should().NotBeNullOrWhiteSpace();
        responseData.RootElement.GetProperty("titulo").GetString().Should().Be(requisicao.Titulo);
        responseData.RootElement.GetProperty("categoria").GetUInt16().Should().Be((ushort)requisicao.Categoria);
        responseData.RootElement.GetProperty("modoPreparo").GetString().Should().Be(requisicao.ModoPreparo);
        responseData.RootElement.GetProperty("tempoPreparo").GetInt32().Should().Be(requisicao.TempoPreparo);
    }

    [Theory]
    [InlineData("pt")]
    [InlineData("en")]
    public async Task Validar_Erro_Sem_Ingredientes(string cultura)
    {
        var token = await Login(_usuario.Email, _senha);
        var requisicao = RequestReceitaBuilder.Construir();
        requisicao.Ingredientes.Clear();

        var resposta = await PostRequest(METODO, requisicao, token, cultura: cultura);

        resposta.StatusCode.Should().Be(HttpStatusCode.BadRequest);

        await using var responstaBody = await resposta.Content.ReadAsStreamAsync();

        var responseData = await JsonDocument.ParseAsync(responstaBody);

        var erros = responseData.RootElement.GetProperty("messages").EnumerateArray();

        var mensagemEsperada = ResourceErrorMessage.ResourceManager.GetString("RECEITA_MINIMO_UM_INGREDIENTE", new System.Globalization.CultureInfo(cultura));
        erros.Should().ContainSingle().And.Contain(x => x.GetString().Equals(mensagemEsperada));
    }
}
