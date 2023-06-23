using AutoMapper;
using MeuLivroDeReceitas.Application.Services.UsuarioLogado;
using MeuLivroDeReceitas.Comunicacao.Request;
using MeuLivroDeReceitas.Comunicacao.Response;
using MeuLivroDeReceitas.Domain.Repositorios;
using MeuLivroDeReceitas.Domain.Repositorios.Receita;
using MeuLivroDeReceitas.Exception.ExceptionBase;

namespace MeuLivroDeReceitas.Application.UseCases.Receita.Registrar;
public class RegistrarReceitaUseCase : IRegistrarReceitaUseCase
{
    private readonly IMapper _mapper;
    private readonly IUnidadeDeTrabalho _unidadeDeTrabalho;
    private readonly IUsuarioLogado _usuarioLogado;
    private readonly IReceitaWriteOnlyRepositorio _repositorio;

    public RegistrarReceitaUseCase(IMapper mapper, IUnidadeDeTrabalho unidadeDeTrabalho, IUsuarioLogado usuarioLogado, IReceitaWriteOnlyRepositorio repositorio)
    {
        _mapper = mapper;
        _unidadeDeTrabalho = unidadeDeTrabalho;
        _usuarioLogado = usuarioLogado;
        _repositorio = repositorio;
    }

    public async Task<ResponseReceitaJson> Executar(RequestReceitaJson requisicao)
    {
        Validar(requisicao);

        var usuarioLogado = await _usuarioLogado.RecuperarUsuario();

        var receita = _mapper.Map<Domain.Entidades.Receita>(requisicao);
        receita.UsuarioId = usuarioLogado.Id;

        await _repositorio.Registrar(receita);

        await _unidadeDeTrabalho.Commit();

        return _mapper.Map<ResponseReceitaJson>(receita);
    }

    private static void Validar(RequestReceitaJson requisicao)
    {
        var validator = new RegistrarReceitaValidator();
        var resultado = validator.Validate(requisicao);

        if (!resultado.IsValid)
        {
            var mensagesDeErro = resultado.Errors.Select(c => c.ErrorMessage).ToList();
            throw new ErrosDeValidacaoException(mensagesDeErro);
        }
    }
}
