﻿using MeuLivroDeReceitas.Comunicacao.Request;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using Newtonsoft.Json;
using System.Text;
using System.Text.Json;
using Xunit;

namespace WebApi.Test.V1;

public class ControllerBase : IClassFixture<MeuLivroReceitaWebApplicationFactory<Program>>
{
    private readonly HttpClient _client;

    public ControllerBase(MeuLivroReceitaWebApplicationFactory<Program> factory)
    {
        _client = factory.CreateClient();
    }

    protected async Task<HttpResponseMessage> PostRequest(string metodo, object body)
    {
        var jsonString = JsonConvert.SerializeObject(body);

        return await _client.PostAsync(metodo, new StringContent(jsonString, Encoding.UTF8, "application/json"));
    }
}

    

   

   

   
   

   