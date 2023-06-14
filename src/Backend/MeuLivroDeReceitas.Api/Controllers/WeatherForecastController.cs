using MeuLivroDeReceitas.Application.UseCases.Usuario.Registrar;
using MeuLivroDeReceitas.Exception;
using Microsoft.AspNetCore.Mvc;

namespace MeuLivroDeReceitas.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class WeatherForecastController : ControllerBase
{

    [HttpGet(Name = "GetWeatherForecast")]
    public async Task<IActionResult> Get([FromServices] IRegistroUsuarioUseCase useCase)
    {
        var resposta =await useCase.Executar(new Comunicacao.Request.RequestRegistrarUsuarioJson {
        Nome = "Matheus oliveira",
        Email = "teste@gmail.com",
        Telefone = "51 9 8674-8714",
        Senha = "teste123"
        });
        return Ok(resposta);
    }
}