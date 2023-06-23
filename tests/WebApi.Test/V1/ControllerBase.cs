using MeuLivroDeReceitas.Comunicacao.Request;
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

    protected async Task<HttpResponseMessage> PostRequest(string metodo, object body, string token = "", string cultura = "")
    {
        AutorizarRequisicao(token);
        AlterarCulturaRequisicao(cultura);

        var jsonString = JsonConvert.SerializeObject(body);

        return await _client.PostAsync(metodo, new StringContent(jsonString, Encoding.UTF8, "application/json"));
    }

    protected async Task<HttpResponseMessage> PutRequest(string metodo, object body, string token = "", string cultura = "")
    {
        AutorizarRequisicao(token);

        var jsonString = JsonConvert.SerializeObject(body);

        return await _client.PutAsync(metodo, new StringContent(jsonString, Encoding.UTF8, "application/json"));
    }

    protected async Task<string> Login(string email, string senha)
    {
        var requisicao = new RequestLoginJson
        {
            Email = email,
            Senha = senha
        };

        var resposta = await PostRequest("login", requisicao);

        await using var responstaBody = await resposta.Content.ReadAsStreamAsync();

        var responseData = await JsonDocument.ParseAsync(responstaBody);

        return responseData.RootElement.GetProperty("token").GetString();
    }

    private void AutorizarRequisicao(string token)
    {
        if (!string.IsNullOrWhiteSpace(token) && !_client.DefaultRequestHeaders.Contains("Authorization"))
        {
            _client.DefaultRequestHeaders.Add("Authorization", $"Bearer {token}");
        }
    }

     private void AlterarCulturaRequisicao(string cultura)
    {
        if (!string.IsNullOrWhiteSpace(cultura))
        {
            if (_client.DefaultRequestHeaders.Contains("Accept-Language"))
            {
                _client.DefaultRequestHeaders.Remove("Accept-Language");
            }

            _client.DefaultRequestHeaders.Add("Accept-Language", cultura);
        }
    }
}

    

   

   

   
   

   