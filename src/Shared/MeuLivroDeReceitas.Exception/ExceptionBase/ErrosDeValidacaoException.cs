namespace MeuLivroDeReceitas.Exception.ExceptionBase;

public class ErrosDeValidacaoException : MeuLivroDeReceitasException
{
    public List<string> Errors { get; set; }

    public ErrosDeValidacaoException(List<string> errors)
    {
        Errors = errors;
    }
}

