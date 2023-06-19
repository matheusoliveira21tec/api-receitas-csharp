using MeuLivroDeReceitas.Application.UseCases.Login;
using MeuLivroDeReceitas.Comunicacao.Request;
using MeuLivroDeReceitas.Comunicacao.Response;
using Microsoft.AspNetCore.Mvc;

namespace MeuLivroDeReceitas.Api.Controllers;

public class LoginController : MeuLivroDeReceitasController
{
    [HttpPost]
    [ProducesResponseType(typeof(ResponseLoginJson), StatusCodes.Status200OK)]
    public async Task<IActionResult> Login(
            [FromServices] ILoginUseCase useCase,
            [FromBody] RequestLoginJson requisicao)
    {
        var resposta = await useCase.Executar(requisicao);

        return Ok(resposta);
    }
}