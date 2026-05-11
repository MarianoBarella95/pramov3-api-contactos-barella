using System.Security.Cryptography.X509Certificates;
using System.Text;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using pramov3_ao_barella.Models;


public class TokenProvider(IConfiguration configuration)
{
    public string generarToken(Usuario usuario)
    {   
        
        //secretKey FUE GENERADA MEDIANTE UNA HERRAMIENTA WEB
        //PARA APORTAR MAYOR SEGURIDAD DE CIFRADO
        string? secretKey = configuration["Jwt:Secret"];
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));

        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(
                [
                    new Claim(ClaimTypes.NameIdentifier, usuario.Id.ToString()),
                    new Claim(ClaimTypes.Actor, usuario.UserName.ToString()),
                    new Claim(ClaimTypes.Role, usuario.Rol.ToString())
                ]
            ),
            Expires = DateTime.UtcNow.AddMinutes(configuration.GetValue<int>("Jwt:ExpirationInMinutes")),
            SigningCredentials = credentials, 
            Issuer = configuration[("Jwt:Issuer")],
            Audience = configuration[("Jwt:Audience")]       
        };

        var handler = new JsonWebTokenHandler();

        string token = handler.CreateToken(tokenDescriptor);

        return token;
    }
}