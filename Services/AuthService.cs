using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;

public class AuthService
{
    private readonly TokenProvider _tokenProvider;
    // private readonly Usuario user = new Usuario(new Guid(), "Mariano", "1905", "Admin");
    private readonly DataContext _context;


    public AuthService(TokenProvider tokenProvider, DataContext context)
    {
        _tokenProvider = tokenProvider;
        _context = context;
    }

    public Usuario? crearUsuario(string nombre, string password)
    {
        Usuario user = new();
        user.Nombre = nombre;
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

        var user = _context.Usuarios.FirstOrDefault(x => x.Nombre == nombre && x.Password == password);

        if (user == null || user.Password != password)
        {
            return null;
        }

        return _tokenProvider.generarToken(user);
    }
}