using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using fornecedor_api.Interfaces;
using fornecedor_api.Models.Entities;
using Microsoft.IdentityModel.Tokens;

namespace fornecedor_api.Services;

public class AuthServices : IAuthServices
{
    public string GenerateToken(Usuario usuario)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(Settings.Secret);
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new Claim[]
            {
                new Claim(ClaimTypes.Name, usuario.Login.ToString()),
                new Claim(ClaimTypes.Sid, usuario.Id.ToString())
            }),
            Expires = DateTime.UtcNow.AddHours(2),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };
        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }
}