using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Routing.Patterns;

public class Usuario
{   
    public int Id { get; set; }
    public string? Nombre { get; set; }
    public string Password { get; set; } = string.Empty;
    public string? Rol { get; set; }

    public Usuario() {}
    public Usuario(int id, string nombre, string password, string rol)
    {   
        this.Id = id;
        this.Nombre = nombre;
        this.Password = password; 
        this.Rol = rol;
    }

}