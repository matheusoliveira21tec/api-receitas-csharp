using MeuLivroDeReceitas.Domain.Entidades;
using MeuLivroDeReceitas.Domain.Repositorios.Usuario;
using Microsoft.EntityFrameworkCore;

namespace MeuLivroDeReceitas.Infrastructure.AcessoRepositorio.Repositorio;

public class UsuarioRepositorio : IUsuarioWriteOnlyRepositorio, IUsuarioReadOnlyRepositorio, IUsuarioUpdateOnlyRepositorio
{
    private readonly MeuLivroDeReceitasContext _contexto;

    public UsuarioRepositorio(MeuLivroDeReceitasContext contexto)
    {
        _contexto = contexto;
    }

    public  async Task Adicionar(Usuario usuario)
    {
       await _contexto.Usuarios.AddAsync(usuario);
    }

    public  async Task<bool> ExisteUsuarioComEmail(string email)
    {
       return await _contexto.Usuarios.AnyAsync(x => x.Email.Equals(email));
    }

    public async Task<Usuario> Login(string email, string password)
    {
        return await _contexto.Usuarios.AsNoTracking()
            .FirstOrDefaultAsync(u => u.Email.Equals(email) && u.Senha.Equals(password));
    }

    public async Task<Usuario> RecuperarPorEmail(string email)
    {
        return await _contexto.Usuarios.AsNoTracking()
           .FirstOrDefaultAsync(u => u.Email.Equals(email));
    }

    public async Task<Usuario> RecuperarPorId(long id)
    {
        return await _contexto.Usuarios.FirstOrDefaultAsync(u => u.Id == id);
    }

    public void Update(Usuario Usuario)
    {
        _contexto.Usuarios.Update(Usuario);
    }
}
