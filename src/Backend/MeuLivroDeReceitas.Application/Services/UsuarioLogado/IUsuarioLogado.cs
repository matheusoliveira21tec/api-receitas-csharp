namespace MeuLivroDeReceitas.Application.Services.UsuarioLogado;

public interface IUsuarioLogado
{
    Task<Domain.Entidades.Usuario> RecuperarUsuario();
}
