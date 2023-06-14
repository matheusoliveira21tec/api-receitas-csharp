using MeuLivroDeReceitas.Comunicacao.Request;
using MeuLivroDeReceitas.Comunicacao.Response;

namespace MeuLivroDeReceitas.Application.UseCases.Usuario.Registrar;

public interface IRegistroUsuarioUseCase
{
    Task<ResponseUsuarioRegistradoJson> Executar(RequestRegistrarUsuarioJson request);
}
