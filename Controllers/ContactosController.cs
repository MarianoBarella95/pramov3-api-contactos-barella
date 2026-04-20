using System.Collections.Immutable;
using System.IO.Compression;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class ContactosController : ControllerBase
{
    public readonly ContactoService _contactoService; 

    public ContactosController(ContactoService contactoService)
    {
        _contactoService = contactoService;
    }
 

    [HttpGet]
    public ActionResult<List<Contacto>> ObtenerTodos() => Ok(_contactoService.ObtenerTodos());

    [HttpGet("{id}")]
    public ActionResult<Contacto> ObtenerPorId(int id)
    {   
        var contacto = _contactoService.ObtenerPorId(id);
        return contacto == null ? NotFound() : Ok(contacto);
    }
    
    [Authorize(Roles = "Admin")]
    [HttpPost]
    public ActionResult<Contacto> Crear(Contacto contacto)
    {   
        _contactoService.Crear(contacto);
        return CreatedAtAction(nameof(ObtenerPorId), new {id = contacto.Id}, contacto);
    }

    [Authorize(Roles = "Admin")]
    [HttpPut("{id}")]
    public ActionResult<Contacto> Editar(int id, Contacto contactoActualizado)
    {   
        bool edit = _contactoService.Editar(id, contactoActualizado);

        return edit ? NoContent() : NotFound();
    }
    
    [Authorize(Roles = "Admin")]
    [HttpDelete("{id}")]
    public ActionResult<Contacto> Eliminar(int id)
    {   
        bool eliminado = _contactoService.Eliminar(id);

        return eliminado ? NoContent() : NotFound(); 
    }
}