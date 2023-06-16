namespace MeuLivroDeReceitas.Domain.Repositorios.Usuario;

public interface IUsuarioReadOnlyRepositorio
{
    Task<bool> ExisteUsuarioComEmail(string email);
}
