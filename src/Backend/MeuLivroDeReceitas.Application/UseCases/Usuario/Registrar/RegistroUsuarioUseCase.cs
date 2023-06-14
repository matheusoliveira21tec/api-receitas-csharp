using AutoMapper;
using MeuLivroDeReceitas.Application.Services.Criptografia;
using MeuLivroDeReceitas.Application.Services.Token;
using MeuLivroDeReceitas.Comunicacao.Request;
using MeuLivroDeReceitas.Comunicacao.Response;
using MeuLivroDeReceitas.Domain.Repositorios;
using MeuLivroDeReceitas.Exception;
using MeuLivroDeReceitas.Exception.ExceptionBase;

namespace MeuLivroDeReceitas.Application.UseCases.Usuario.Registrar;

public class RegistroUsuarioUseCase : IRegistroUsuarioUseCase
{
    private readonly IUsuarioWriteOnlyRepositorio _repoWrite;
    private readonly IUsuarioReadOnlyRepositorio _repoRead;
    private readonly IMapper _mapper;
    private readonly IUnidadeDeTrabalho _unidadeDeTrabalho;
    private readonly EncriptadorDeSenha _encriptadorDeSenha;
    private readonly TokenController _tokenController;

    public RegistroUsuarioUseCase(IUsuarioWriteOnlyRepositorio repoWrite, IMapper mapper, IUnidadeDeTrabalho unidadeDeTrabalho, EncriptadorDeSenha encriptadorDeSenha, TokenController tokenController, IUsuarioReadOnlyRepositorio repoRead)
    {
        _repoWrite = repoWrite;
        _mapper = mapper;
        _unidadeDeTrabalho = unidadeDeTrabalho;
        _encriptadorDeSenha = encriptadorDeSenha;
        _tokenController = tokenController;
        _repoRead = repoRead;   
    }
    public async Task<ResponseUsuarioRegistradoJson> Executar(RequestRegistrarUsuarioJson request)
    {
        await Validar(request);

        var entidade = _mapper.Map<Domain.Entidades.Usuario>(request);
        entidade.Senha = _encriptadorDeSenha.Criptografar(request.Senha);
        await _repoWrite.Adicionar(entidade);
        await _unidadeDeTrabalho.Commit();
        var token = _tokenController.GerarToken(entidade.Email);
        return new ResponseUsuarioRegistradoJson
        {
            Token = token
        };
    }

    private async Task Validar(RequestRegistrarUsuarioJson request)
    {
       var validator = new RegistroUsuarioValidator();
       var result  = validator.Validate(request);

       var existeUsuariocomEmail = await  _repoRead.ExisteUsuarioComEmail(request.Email);
       if (existeUsuariocomEmail)
       {
            result.Errors.Add(new FluentValidation.Results.ValidationFailure("email", ResourceErrorMessage.EMAIL_JA_CADASTRADO));
       }

       if (!result.IsValid)
        {
            var errorMessages = result.Errors.Select(error => error.ErrorMessage).ToList();
            throw new ErrosDeValidacaoException(errorMessages);
        }
    }
}
