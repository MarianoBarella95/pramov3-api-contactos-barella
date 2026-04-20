using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;

public class AuthService
{
    private readonly TokenProvider _tokenProvider;
    private readonly Usuario user = new Usuario(new Guid(), "Mariano", "1905", "Admin");

    public AuthService(TokenProvider tokenProvider)
    {
        _tokenProvider = tokenProvider;
    }


    public string? login(string nombre, string password)
    {      
        if(nombre != user.Nombre || password != user.Password)
        {
            return null;
        } 
        else
        {
            return _tokenProvider.generarToken(user);
        }
    }
}