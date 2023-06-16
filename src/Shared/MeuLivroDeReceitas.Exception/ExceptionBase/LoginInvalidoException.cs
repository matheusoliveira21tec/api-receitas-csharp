using System.Runtime.Serialization;

namespace MeuLivroDeReceitas.Exception.ExceptionBase;

[Serializable]
public class LoginInvalidoException : MeuLivroDeReceitasException
{
    public LoginInvalidoException() : base(ResourceErrorMessage.LOGIN_INVALIDO)
    {
    }

    protected LoginInvalidoException(SerializationInfo info, StreamingContext context) : base(info, context)
    {
    }
}