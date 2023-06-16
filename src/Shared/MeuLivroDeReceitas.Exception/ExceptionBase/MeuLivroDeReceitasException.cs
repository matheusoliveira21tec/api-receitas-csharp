using System.Runtime.Serialization;

namespace MeuLivroDeReceitas.Exception.ExceptionBase;

[Serializable]
public class MeuLivroDeReceitasException : SystemException
{
    public MeuLivroDeReceitasException(string mensagem) : base(mensagem)
    {
    }

    protected MeuLivroDeReceitasException(SerializationInfo info, StreamingContext context) : base(info, context)
    {
    }
}