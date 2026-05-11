using System.Security.Principal;
using System.Text;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using pramov3_ao_barella.Models;
using pramov3_ao_barella.DTO;

[ApiController]
[Route("api/auth")]

public class AuthController : ControllerBase
{
    private readonly AuthService? _authService;

    public AuthController(AuthService service)
    {
        _authService = service;
    }

    [AllowAnonymous]
    [HttpPost("login")]
    public ActionResult login([FromBody] LoginRequest loginRequest)
    {       
        var token = _authService.login(loginRequest.Email, loginRequest.Password);

        if(token == null)
        {
            return Unauthorized();
        }

        return Ok(token);

    }

    [AllowAnonymous]
    [HttpPost("register")]
    public ActionResult register([FromBody] Usuario usuario)
    {

        var nuevoUsuario = _authService.crearUsuario(usuario.UserName, usuario.Password);

        if (nuevoUsuario == null)
        {
            return BadRequest();            
        }

        return Ok();
    }


}