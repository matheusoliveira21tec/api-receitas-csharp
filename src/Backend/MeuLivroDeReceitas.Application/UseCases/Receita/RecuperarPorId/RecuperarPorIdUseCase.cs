using AutoMapper;
using MeuLivroDeReceitas.Application.Services.UsuarioLogado;
using MeuLivroDeReceitas.Comunicacao.Response;
using MeuLivroDeReceitas.Domain.Repositorios.Receita;
using MeuLivroDeReceitas.Exception.ExceptionBase;
using MeuLivroDeReceitas.Exception;

namespace MeuLivroDeReceitas.Application.UseCases.Receita.RecuperarPorId;
public class RecuperarReceitaPorIdUseCase : IRecuperarReceitaPorIdUseCase
{
    private readonly IReceitaReadOnlyRepositorio _repositorio;
    private readonly IUsuarioLogado _usuarioLogado;
    private readonly IMapper _mapper;

    public RecuperarReceitaPorIdUseCase(
        IReceitaReadOnlyRepositorio repositorio,
        IUsuarioLogado usuarioLogado,
        IMapper mapper)
    {
        _mapper = mapper;
        _repositorio = repositorio;
        _usuarioLogado = usuarioLogado;
    }

    public async Task<ResponseReceitaJson> Executar(long id)
    {
        var usuarioLogado = await _usuarioLogado.RecuperarUsuario();

        var receita = await _repositorio.RecuperarPorId(id);

        await Validar(usuarioLogado, receita);

        return _mapper.Map<ResponseReceitaJson>(receita);
    }

    public async Task Validar(Domain.Entidades.Usuario usuarioLogado, Domain.Entidades.Receita receita)
    {
        if (receita is null || receita.UsuarioId != usuarioLogado.Id)
        {
            throw new ErrosDeValidacaoException(new List<string> { ResourceErrorMessage.RECEITA_NAO_ENCONTRADA });
        }
    }
}