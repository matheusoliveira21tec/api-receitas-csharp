﻿using MeuLivroDeReceitas.Domain.Repositorios.Usuario;
using Moq;

namespace Utilitario.Tests.Repositorios;

public class UsuarioReadOnlyRepositorioBuilder
{
    private static UsuarioReadOnlyRepositorioBuilder _instance;
    private readonly Mock<IUsuarioReadOnlyRepositorio> _repositorio;

    private UsuarioReadOnlyRepositorioBuilder()
    {
        if (_repositorio is null)
        {
            _repositorio = new Mock<IUsuarioReadOnlyRepositorio>();
        }
    }

    public static UsuarioReadOnlyRepositorioBuilder Instancia()
    {
        _instance = new UsuarioReadOnlyRepositorioBuilder();
        return _instance;
    }

    public UsuarioReadOnlyRepositorioBuilder ExisteUsuarioComEmail(string email)
    {
        if (!string.IsNullOrEmpty(email))
            _repositorio.Setup(i => i.ExisteUsuarioComEmail(email)).ReturnsAsync(true);

        return this;
    }

    public UsuarioReadOnlyRepositorioBuilder Login(MeuLivroDeReceitas.Domain.Entidades.Usuario usuario)
    {
        _repositorio.Setup(i => i.Login(usuario.Email, usuario.Senha)).ReturnsAsync(usuario);

        return this;
    }

    public IUsuarioReadOnlyRepositorio Construir()
    {
        return _repositorio.Object;
    }
}