using Microsoft.EntityFrameworkCore;
using Microsoft.Net.Http.Headers;

public class DataContext : DbContext
{   
    public DataContext(DbContextOptions<DataContext> options) : base(options){}
    public DbSet<Contacto> Contactos { get; set; }
    public DbSet<Usuario> Usuarios { get; set; }    
}