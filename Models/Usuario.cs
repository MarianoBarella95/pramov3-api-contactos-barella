using Microsoft.AspNetCore.Routing.Patterns;

public class Usuario
{   
    public Guid Id { get; set; }
    public string? Nombre { get; set; }
    public string? Password { get; set; }
    public string? Rol { get; set; }

    public Usuario(Guid id, string nombre, string password, string rol)
    {   
        this.Id = id;
        this.Nombre = nombre;
        this.Password = password; 
        this.Rol = rol;
    }

}