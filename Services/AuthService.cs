using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.IdentityModel.Tokens;
using pramov3_ao_barella.Models;

public class AuthService
{
    private readonly TokenProvider _tokenProvider;
    // private readonly Usuario user = new Usuario(new Guid(), "Mariano", "1905", "Admin");
    private readonly DbA358b2Pam3Context _context;


    public AuthService(TokenProvider tokenProvider, DbA358b2Pam3Context context)
    {
        _tokenProvider = tokenProvider;
        _context = context;
    }

    public Usuario? crearUsuario(string nombre, string password)
    {   
        if (_context.Usuarios.Any(x => x.UserName == nombre))
        {
            return null;
        }

        Usuario user = new();
        user.UserName = nombre;
        user.Password = password;
        user.Rol = "Admin";
        _context.Usuarios.Add(user);
        _context.SaveChanges();
        return user;

    }

    public string? login(string nombre, string password)
    {      
        if(nombre.IsNullOrEmpty() || password.IsNullOrEmpty())
        {
            return null;
        } 

        var user = _context.Usuarios.FirstOrDefault(x => x.UserName == nombre && x.Password == password);

        if (user == null || user.Password != password)
        {
            return null;
        }

        return _tokenProvider.generarToken(user);
    }
}