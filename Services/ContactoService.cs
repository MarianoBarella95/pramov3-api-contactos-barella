using System.IO.Compression;
using System.Security.Cryptography.X509Certificates;
using Microsoft.VisualBasic;

public class ContactoService
{
    // LISTA QUE ALMACENA LOS CONTACTOS EN MEMORIA
    public readonly List<Contacto> _contactos = new List<Contacto>
    {
          new Contacto
          {
            Id = 1,
            Nombre = "Mariano",
            Apellido = "Barella",
            Telefono = "3517048673",
            email = "marianobarella1@gmail.com",
          },
          new Contacto
          {
            Id = 2,
            Nombre = "Fiamma",
            Apellido = "Somavilla",
            Telefono = "3517048673",
            email = "fiammusoma@gmail.com",
          },
          new Contacto
          {
            Id = 3,
            Nombre = "Nélida",
            Apellido = "Mebar",
            Telefono = "3517048673",
            email = "nmebar@gmail.com",
          }
        };

    private int id = 3;

    // OBTENER TODOS LOS CONTACTOS
    public List<Contacto> ObtenerTodos() => _contactos;

    // BUSCAR CONTACTO POR ID
    public Contacto? ObtenerPorId(int id) => _contactos.FirstOrDefault(c => c.Id == id);

    // CREAR UN CONTACTO NUEVO
    public Contacto Crear(Contacto contacto)
    {
        contacto.Id = id + 1;
        _contactos.Add(contacto);
        return contacto;
    }

    // EDITAR UN CONTACTO 
    public bool Editar(int id, Contacto contactoActualizado)
    {
        var res = _contactos.FirstOrDefault(x => x.Id == id);

        if (res == null)
        {
            return false;
        }

        res.Nombre = contactoActualizado.Nombre;
        res.Apellido = contactoActualizado.Apellido;
        res.Telefono = contactoActualizado.Telefono;
        res.email = contactoActualizado.email;


        return true;
    }

    // ELIMINAR UN CONTACTO
    public bool Eliminar(int id)
    {   
        var res = _contactos.FirstOrDefault(x => x.Id == id);

        if (res == null)
        {
            return false;
        }

        _contactos.Remove(res);
        return true;    
    }

    //CONSTRUCTOR    
    // public ContactoService()
    // {   
    //     List<Contacto> nuevaLista = new List<Contacto>
    //     {
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
    //         Nombre = "Fiamma",
    //         Apellido = "Somavilla",
    //         Telefono = "3517048673",
    //         email = "fiammusoma@gmail.com",
    //       },
    //       new Contacto
    //       {
    //         Id = 3,
    //         Nombre = "Nélida",
    //         Apellido = "Mebar",
    //         Telefono = "3517048673",
    //         email = "nmebar@gmail.com",
    //       } 
    //     };

    //     foreach (var item in nuevaLista)
    //     {
    //         _contactos.Add(item);
    //     }
    // }
}