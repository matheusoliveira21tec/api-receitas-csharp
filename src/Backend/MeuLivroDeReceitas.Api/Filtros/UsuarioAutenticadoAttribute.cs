﻿using MeuLivroDeReceitas.Application.Services.Token;
using MeuLivroDeReceitas.Comunicacao.Response;
using MeuLivroDeReceitas.Domain.Repositorios.Usuario;
using MeuLivroDeReceitas.Exception;
using MeuLivroDeReceitas.Exception.ExceptionBase;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.IdentityModel.Tokens;

namespace MeuLivroDeReceitas.Api.Filtros
{
    public class UsuarioAutenticadoAttribute : AuthorizeAttribute, IAsyncAuthorizationFilter
    {
        private readonly TokenController _tokenController;
        private readonly IUsuarioReadOnlyRepositorio _repositorio;

        public UsuarioAutenticadoAttribute(TokenController tokenController, IUsuarioReadOnlyRepositorio repositorio)
        {
            _tokenController = tokenController;
            _repositorio = repositorio;
        }

        public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
        {
            try
            {
                var token = TokenNaRequisicao(context);
                var emailUsuario = _tokenController.RecuperarEmail(token);

                var usuario = await _repositorio.RecuperarPorEmail(emailUsuario);

                if (usuario is null)
                {
                    throw new MeuLivroDeReceitasException(string.Empty);
                }
            }
            catch (SecurityTokenExpiredException)
            {
                TokenExpirado(context);
            }
            catch
            {
                UsuarioSemPermissao(context);
            }
        }

        private static string TokenNaRequisicao(AuthorizationFilterContext context)
        {
            var authorization = context.HttpContext.Request.Headers["Authorization"].ToString();

            if (string.IsNullOrWhiteSpace(authorization))
            {
                throw new MeuLivroDeReceitasException(string.Empty);
            }

            return authorization["Bearer".Length..].Trim();
        }

        private static void TokenExpirado(AuthorizationFilterContext context)
        {
            context.Result = new UnauthorizedObjectResult(new ResponseErrorJson(ResourceErrorMessage.TOKEN_EXPIRADO));
        }

        private static void UsuarioSemPermissao(AuthorizationFilterContext context)
        {
            context.Result = new UnauthorizedObjectResult(new ResponseErrorJson(ResourceErrorMessage.USUARIO_SEM_PERMISSAO));
        }
    }
}
