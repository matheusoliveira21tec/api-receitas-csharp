using MeuLivroDeReceitas.Application.Services.Criptografia;

namespace Utilitario.Tests.Criptografia;

public class EncriptadorDeSenhaBuilder
{
    public static EncriptadorDeSenha Instancia()
    {
        return new EncriptadorDeSenha("ABCD123");
    }
}
