using MeuLivroDeReceitas.Comunicacao.Response;
using MeuLivroDeReceitas.Exception;
using MeuLivroDeReceitas.Exception.ExceptionBase;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Net;

namespace MeuLivroDeReceitas.Api.Filtros;

public class FiltroDasExceptions : IExceptionFilter
{
    public void OnException(ExceptionContext context)
    {
        if(context.Exception is MeuLivroDeReceitasException)
        {
            TratarMeuLivroDeReceitasException(context);
        }
        else
        {
            LancarErroDesconhecido(context);
        }
    }
    private void TratarMeuLivroDeReceitasException(ExceptionContext context)
    {
        if (context.Exception is ErrosDeValidacaoException)
        {
            TratarErrosDeValidacaoException(context);
        }
        else
        {
            TratarOutrasException(context);
        }
    }

    private void TratarOutrasException(ExceptionContext context)
    {
        throw new NotImplementedException();
    }

    private void TratarErrosDeValidacaoException(ExceptionContext context)
    {
        var erroDeValidacaoException = context.Exception as ErrosDeValidacaoException;

        context.HttpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
        context.Result = new ObjectResult(new ResponseErrorJson(erroDeValidacaoException.Errors));
    }

    private void LancarErroDesconhecido(ExceptionContext context)
    {
        context.HttpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
        context.Result = new ObjectResult(new ResponseErrorJson(ResourceErrorMessage.ERRO_DESCONHECIDO));
    }
}
