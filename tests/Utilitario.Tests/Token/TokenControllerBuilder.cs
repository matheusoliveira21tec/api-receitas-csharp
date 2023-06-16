using MeuLivroDeReceitas.Application.Services.Token;

namespace Utilitario.Tests.Token;

public class TokenControllerBuilder
{
    public static TokenController Instancia()
    {
        return new TokenController(1000, "MHBlUEdMNjlJRiVZQWcjUkBYWUpMSlk4TU5CUG9LUnE3MEhQQV5xTGprIW5vOHUxSEc=");
    }

    public static TokenController TokenExpirado()
    {
        return new TokenController(0.0166667, "eHFDZjRrZkJxZ05YVzhzMEVhTkpHT3UyKmIhQGtO");
    }
}
