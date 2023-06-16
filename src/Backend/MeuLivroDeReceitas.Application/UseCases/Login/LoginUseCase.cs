using MeuLivroDeReceitas.Application.Services.Criptografia;
using MeuLivroDeReceitas.Application.Services.Token;
using MeuLivroDeReceitas.Comunicacao.Request;
using MeuLivroDeReceitas.Comunicacao.Response;
using MeuLivroDeReceitas.Domain.Repositorios.Usuario;
using MeuLivroDeReceitas.Exception.ExceptionBase;

namespace MeuLivroDeReceitas.Application.UseCases.Login;

public class LoginUseCase : ILoginUseCase
{
    private readonly IUsuarioReadOnlyRepositorio _usuarioReadOnlyRepositorio;
    private readonly EncriptadorDeSenha _encriptadorDeSenha;
    private readonly TokenController _tokenController;
    public LoginUseCase(IUsuarioReadOnlyRepositorio usuarioReadOnlyRepositorio, EncriptadorDeSenha encriptadorDeSenha, TokenController tokenController)
    {
        _usuarioReadOnlyRepositorio = usuarioReadOnlyRepositorio;
        _encriptadorDeSenha = encriptadorDeSenha;
        _tokenController = tokenController;
    }

    public async Task<ResponseLoginJson> Executar(RequestLoginJson request)
    {
        var senhaCriptografada = _encriptadorDeSenha.Criptografar(request.Senha);

        var usuario = await _usuarioReadOnlyRepositorio.Login(request.Email, senhaCriptografada);

        if (usuario is null)
        {
            throw new LoginInvalidoException();
        }

        return new ResponseLoginJson
        {
            Nome = usuario.Nome,
            Token = _tokenController.GerarToken(usuario.Email)
        };
    }
}
