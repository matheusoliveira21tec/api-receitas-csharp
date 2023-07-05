﻿using AutoMapper;
using MeuLivroDeReceitas.Application.Services.UsuarioLogado;
using MeuLivroDeReceitas.Comunicacao.Response;

namespace MeuLivroDeReceitas.Application.UseCases.Usuario.RecuperarPerfil;
public class RecuperarPerfilUseCase : IRecuperarPerfilUseCase
{
    private readonly IMapper _mapper;
    private readonly IUsuarioLogado _usuarioLogado;

    public RecuperarPerfilUseCase(IMapper mapper, IUsuarioLogado usuarioLogado)
    {
        _mapper = mapper;
        _usuarioLogado = usuarioLogado;
    }

    public async Task<ResponsePerfilUsuarioJson> Executar()
    {
        var usuario = await _usuarioLogado.RecuperarUsuario();

        return _mapper.Map<ResponsePerfilUsuarioJson>(usuario);
    }
}
