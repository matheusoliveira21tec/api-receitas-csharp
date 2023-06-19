using System.Runtime.Serialization;

namespace MeuLivroDeReceitas.Exception.ExceptionBase;

[Serializable]
public class ErrosDeValidacaoException : MeuLivroDeReceitasException
{
    public List<string> Errors { get; set; }

    public ErrosDeValidacaoException(List<string> errors) : base(string.Empty)
    {
        Errors = errors;
    }

    protected ErrosDeValidacaoException(SerializationInfo info, StreamingContext context) : base(info, context)
    {
    }
}