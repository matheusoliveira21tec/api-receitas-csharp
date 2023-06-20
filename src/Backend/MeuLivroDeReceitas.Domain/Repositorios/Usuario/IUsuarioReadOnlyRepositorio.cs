namespace MeuLivroDeReceitas.Domain.Repositorios.Usuario;

public interface IUsuarioReadOnlyRepositorio
{
    Task<bool> ExisteUsuarioComEmail(string email);
    Task<Entidades.Usuario> Login(string email, string password);
    Task <Entidades.Usuario>RecuperarPorEmail(string email);
}
