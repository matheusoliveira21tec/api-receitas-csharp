using AutoMapper;
using MeuLivroDeReceitas.Application.Services.Criptografia;
using MeuLivroDeReceitas.Application.Services.Token;
using MeuLivroDeReceitas.Comunicacao.Request;
using MeuLivroDeReceitas.Comunicacao.Response;
using MeuLivroDeReceitas.Domain.Repositorios;
using MeuLivroDeReceitas.Exception.ExceptionBase;

namespace MeuLivroDeReceitas.Application.UseCases.Usuario.Registrar;

public class RegistroUsuarioUseCase : IRegistroUsuarioUseCase
{
    private readonly IUsuarioWriteOnlyRepositorio _repo;
    private readonly IMapper _mapper;
    private readonly IUnidadeDeTrabalho _unidadeDeTrabalho;
    private readonly EncriptadorDeSenha _encriptadorDeSenha;
    private readonly TokenController _tokenController;

    public RegistroUsuarioUseCase(IUsuarioWriteOnlyRepositorio repo, IMapper mapper, IUnidadeDeTrabalho unidadeDeTrabalho, EncriptadorDeSenha encriptadorDeSenha, TokenController tokenController)
    {
        _repo = repo;
        _mapper = mapper;
        _unidadeDeTrabalho = unidadeDeTrabalho;
        _encriptadorDeSenha = encriptadorDeSenha;
        _tokenController = tokenController;
    }
    public async Task<ResponseUsuarioRegistradoJson> Executar(RequestRegistrarUsuarioJson request)
    {
        Validar(request);

        var entidade = _mapper.Map<Domain.Entidades.Usuario>(request);
        entidade.Senha = _encriptadorDeSenha.Criptografar(request.Senha);
        await _repo.Adicionar(entidade);
        await _unidadeDeTrabalho.Commit();
        var token = _tokenController.GerarToken(entidade.Email);
        return new ResponseUsuarioRegistradoJson
        {
            Token = token
        };
    }

    private void Validar(RequestRegistrarUsuarioJson request)
    {
       var validator = new RegistroUsuarioValidator();
       var result  = validator.Validate(request);

        if (!result.IsValid)
        {
            var errorMessages = result.Errors.Select(error => error.ErrorMessage).ToList();
            throw new ErrosDeValidacaoException(errorMessages);
        }
    }
}
