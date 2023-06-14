using MeuLivroDeReceitas.Application.UseCases.Usuario.Registrar;
using MeuLivroDeReceitas.Comunicacao.Request;
using MeuLivroDeReceitas.Comunicacao.Response;
using MeuLivroDeReceitas.Exception;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MeuLivroDeReceitas.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class UsuarioController : ControllerBase
{

    [HttpPost]
    [ProducesResponseType(typeof(ResponseUsuarioRegistradoJson), StatusCodes.Status201Created)]
    public async Task<IActionResult> RegistrarUsuario(
        [FromServices] IRegistroUsuarioUseCase useCase,
        [FromBody] RequestRegistrarUsuarioJson request)
    {
        var resposta =await useCase.Executar(request);
        return Created(string.Empty, resposta);
    }
}