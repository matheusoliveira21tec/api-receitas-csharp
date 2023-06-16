using MeuLivroDeReceitas.Application.Services.Criptografia;
using MeuLivroDeReceitas.Application.Services.Token;
using MeuLivroDeReceitas.Application.UseCases.Login;
using MeuLivroDeReceitas.Application.UseCases.Usuario.Registrar;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace MeuLivroDeReceitas.Application;

public static class Bootstraper
{
    public static void AddApplication(this IServiceCollection services, IConfiguration configuration)
    {
        AdicionarChaveAdicionalSenha(services, configuration);
        AdicionarTokenJWT(services, configuration);
        AdicionarUseCases(services);
    }

    private static void AdicionarChaveAdicionalSenha(IServiceCollection services, IConfiguration configuration) 
    {
        var section = configuration.GetRequiredSection("Configuracoes:ChaveAdicionalSenha");

        services.AddScoped(options => new EncriptadorDeSenha(section.Value));
    }

    private static void AdicionarTokenJWT(IServiceCollection services, IConfiguration configuration)
    {
        var sectionTempoVida = configuration.GetRequiredSection("Configuracoes:TempoVidaToken");
        var sectionChaveToken = configuration.GetRequiredSection("Configuracoes:ChaveToken");
        services.AddScoped(options => new TokenController(int.Parse(sectionTempoVida.Value), sectionChaveToken.Value));
    }
    private static void AdicionarUseCases(IServiceCollection services)
    {
        services.AddScoped<IRegistroUsuarioUseCase, RegistroUsuarioUseCase>()
            .AddScoped<ILoginUseCase, LoginUseCase>();
    }
}
