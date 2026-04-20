using System.Security.Principal;
using System.Text;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

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
    public ActionResult login([FromBody] Usuario usuario)
    {       
        var token = _authService.login(usuario.Nombre, usuario.Password);

        if(token == null)
        {
            return Unauthorized();
        }

        return Ok(token);

    }

}