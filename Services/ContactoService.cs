using System.IO.Compression;
using System.Security.Cryptography.X509Certificates;
using Microsoft.VisualBasic;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using SQLitePCL;
using pramov3_ao_barella.Models;

public class ContactoService
{
    // LISTA QUE ALMACENA LOS CONTACTOS EN MEMORIA
    // public readonly List<Contacto> _contactos = new List<Contacto>
    // {
    //       new Contacto
    //       {
    //         Id = 1,
    //         Nombre = "Mariano",
    //         Apellido = "Barella",
    //         Telefono = "3517048673",
    //         email = "marianobarella1@gmail.com",
    //       },
    //       new Contacto
    //       {
    //         Id = 2,
    //         Nombre = "Juan Carlos",
    //         Apellido = "Pérez",
    //         Telefono = "3517048673",
    //         email = "jcperez@gmail.com",
    //       },
    //       new Contacto
    //       {
    //         Id = 3,
    //         Nombre = "Rodrigo",
    //         Apellido = "Rodríguez",
    //         Telefono = "3517048673",
    //         email = "rrodriguez@gmail.com",
    //       }
    // };

    // ID DE LA BBDD
    private readonly DbA358b2Pam3Context _context;

    public ContactoService(DbA358b2Pam3Context context)
    {
        _context = context;
    }
     
    //private int id = 3;

    // OBTENER TODOS LOS CONTACTOS
    public List<Contacto> ObtenerTodos()
    {
        var res = _context.Contactos.ToList();

        if (res == null)
        {
            return null;
        }
        else
        {
            return res;
        }
    }

    // BUSCAR CONTACTO POR ID
    public Contacto? ObtenerPorId(int id) => _context.Contactos.FirstOrDefault(c => c.Id == id);

    // CREAR UN CONTACTO NUEVO
    public Contacto Crear(Contacto contacto)
    {

        _context.Contactos.Add(contacto);
        _context.SaveChanges();
        return contacto;
    }

    // EDITAR UN CONTACTO 
    public bool Editar(int id, Contacto contactoActualizado)
    {
        var res = _context.Contactos.FirstOrDefault(x => x.Id == id);

        if (res == null)
        {
            return false;
        }

        res.Nombre = contactoActualizado.Nombre;
        res.Apellido = contactoActualizado.Apellido;
        res.Telefono = contactoActualizado.Telefono;
        res.Email = contactoActualizado.Email;

        _context.SaveChanges();
        return true;
    }

    // ELIMINAR UN CONTACTO
    public bool Eliminar(int id)
    {   
        var res = _context.Contactos.FirstOrDefault(x => x.Id == id);

        if (res == null)
        {
            return false;
        }

        _context.Contactos.Remove(res);
        _context.SaveChanges();
        return true;    
    }
}