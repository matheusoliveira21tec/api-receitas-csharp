namespace MeuLivroDeReceitas.Domain.Repositorios.Usuario;

public interface IUsuarioUpdateOnlyRepositorio
{
    void Update(Entidades.Usuario Usuario);
    Task<Entidades.Usuario> RecuperarPorId(long id);
}
