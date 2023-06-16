﻿using FluentAssertions;
using MeuLivroDeReceitas.Exception;
using System.Net;
using System.Text.Json;
using Utilitario.Tests.Request;
using Xunit;

namespace WebApi.Test.V1.Usuario.Registrar;

public class RegistrarUsuarioTeste : ControllerBase
{
    private const string METODO = "usuario";

    public RegistrarUsuarioTeste(MeuLivroReceitaWebApplicationFactory<Program> factory) : base(factory)
    {

    }

    [Fact]
    public async Task Validar_Sucesso()
    {
        var requisicao = RequestRegistrarUsuarioJsonBuilder.Construir();

        var resposta = await PostRequest(METODO, requisicao);

        resposta.StatusCode.Should().Be(HttpStatusCode.Created);

        await using var responstaBody = await resposta.Content.ReadAsStreamAsync();

        var responseData = await JsonDocument.ParseAsync(responstaBody);

        responseData.RootElement.GetProperty("token").GetString().Should().NotBeNullOrWhiteSpace();
    }
    [Fact]
    public async Task Validar_Erro_Nome_Vazio()
    {
        var requisicao = RequestRegistrarUsuarioJsonBuilder.Construir();
        requisicao.Nome = "";

        var resposta = await PostRequest(METODO, requisicao);

        resposta.StatusCode.Should().Be(HttpStatusCode.BadRequest);

        await using var responstaBody = await resposta.Content.ReadAsStreamAsync();

        var responseData = await JsonDocument.ParseAsync(responstaBody);

        var erros = responseData.RootElement.GetProperty("messages").EnumerateArray();

        erros.Should().ContainSingle().And.Contain(c => c.GetString().Equals(ResourceErrorMessage.NOME_USUARIO_VAZIO));
    }

}
