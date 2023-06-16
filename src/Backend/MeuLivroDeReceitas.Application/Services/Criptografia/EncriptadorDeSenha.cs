using System.Security.Cryptography;
using System.Text;
using System.Text.Unicode;

namespace MeuLivroDeReceitas.Application.Services.Criptografia;

public class EncriptadorDeSenha
{
    private readonly string _chaveAdicional;
    
    public EncriptadorDeSenha(string chaveAdicional)
    {
        _chaveAdicional = chaveAdicional;
    }

    public string Criptografar(string senha)
    {
       var senhaComChaveAdicional = $"{senha}{_chaveAdicional}";
       var bytes = Encoding.UTF8.GetBytes(senhaComChaveAdicional);
       var sha512 = SHA512.Create();
       byte[] hashBytes = sha512.ComputeHash(bytes);

       return StringBytes(hashBytes);
    }

    private static string StringBytes(byte[] bytes)
    {
        StringBuilder sb = new StringBuilder();
        foreach (byte b in bytes)
        {
            var hex = b.ToString("x2");
            sb.Append(hex);
        }
        return sb.ToString();
    }
}
