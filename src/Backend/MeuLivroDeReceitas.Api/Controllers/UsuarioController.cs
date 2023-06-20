using MeuLivroDeReceitas.Api.Filtros;
using MeuLivroDeReceitas.Application.UseCases.Usuario.AlterarSenha;
using MeuLivroDeReceitas.Application.UseCases.Usuario.Registrar;
using MeuLivroDeReceitas.Comunicacao.Request;
using MeuLivroDeReceitas.Comunicacao.Response;
using Microsoft.AspNetCore.Mvc;

namespace MeuLivroDeReceitas.Api.Controllers;

public class UsuarioController : MeuLivroDeReceitasController
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

    [HttpPut]
    [Route("alterar-senha")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ServiceFilter(typeof(UsuarioAutenticadoAttribute))]
    public async Task<IActionResult> AlterarSenha(
           [FromServices] IAlterarSenhaUseCase useCase,
           [FromBody] RequestAlterarSenhaJson request)
    {
        await useCase.Executar(request);

        return NoContent();
    }
}