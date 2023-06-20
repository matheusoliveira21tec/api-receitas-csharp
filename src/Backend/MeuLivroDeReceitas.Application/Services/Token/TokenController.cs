using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace MeuLivroDeReceitas.Application.Services.Token;

public class TokenController
{
    private const string emailAlias = "eml";
    private readonly double _tempoDeVidaDoTokenEmMinutos;
    private readonly string _chaveDeSeguranca;

    public TokenController(double tempoDeVidaDoTokenEmMinutos, string chaveDeSeguranca)
    {
        _tempoDeVidaDoTokenEmMinutos = tempoDeVidaDoTokenEmMinutos;
        _chaveDeSeguranca = chaveDeSeguranca;
    }
    public string GerarToken(string email)
    {
        var claims = new List<Claim>
        {
            new Claim(emailAlias, email)
        };
        var tokenHandler = new JwtSecurityTokenHandler();

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.UtcNow.AddMinutes(_tempoDeVidaDoTokenEmMinutos),
            SigningCredentials = new SigningCredentials(SymmetricKey(),SecurityAlgorithms.HmacSha256Signature)
        };

        var securityToken = tokenHandler.CreateToken(tokenDescriptor);

        return tokenHandler.WriteToken(securityToken);
    }

    public ClaimsPrincipal ValidarToken(string token)
    {
        var tokenHandler = new JwtSecurityTokenHandler();

        var parametrosValidacao = new TokenValidationParameters
        {
            RequireExpirationTime = true,
            IssuerSigningKey = SymmetricKey(),
            ClockSkew = new TimeSpan(0),
            ValidateIssuer = false,
            ValidateAudience = false
        };

        var claims =  tokenHandler.ValidateToken(token, parametrosValidacao, out _);

        return claims;

    }
    public string RecuperarEmail(string token)
    {
        var claims = ValidarToken(token);

        return claims.FindFirst(emailAlias).Value;
    }
    private SymmetricSecurityKey SymmetricKey()
    {
        var symmetricKey = Convert.FromBase64String(_chaveDeSeguranca);
        return new SymmetricSecurityKey(symmetricKey);
    }
}
