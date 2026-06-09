using AppPracticaASP.NET.DTO;
using AppPracticaASP.NET.Filters;
using AppPracticaASP.NET.Services.Auth;
using AppPracticaASP.NET.Services.Interfaces;
using AppPracticaASP.NET.Services.Interfaces.Auth;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace AppPracticaASP.NET.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {

            _authService = authService;
            Console.WriteLine("AuthController instanciado ✅");
        }

        
        [HttpGet("islogged")]
        public ActionResult IsLogged()
        {
            string? cookie = Request.Cookies["JWT"];
            return Ok(cookie != null ? true : false);
        }

        [ServiceFilter(typeof(IsLoggedFilter))]
        [HttpPost("login")]
        public async Task<ActionResult<UserResponseDTO>> Login([FromBody] LoginDTO data)
        {
            if (data == null) return BadRequest("Bad Request");

            var respuesta = await _authService.Login(data);
            if (respuesta != null)
            {
                var (usuario, token) = respuesta.Value;
                Response.Cookies.Append("JWT", token,new CookieOptions
                {
                    HttpOnly = true,
                    Secure = true,
                    Expires = DateTime.UtcNow.AddHours(1)
                });
                return Ok(usuario);
            }
            return BadRequest("Error");
        }

        [ServiceFilter(typeof(IsLoggedFilter))]
        [HttpPost("register")]
        public async Task<ActionResult<UserResponseDTO>> Register([FromBody] UserCreateDTO data)
        {
            if (data == null) return BadRequest("Bad Request");
            var respuesta = await _authService.Registro(data);
            if (respuesta != null)
            {
                var (usuario, token) = respuesta.Value;
                Response.Cookies.Append("JWT", token, new CookieOptions
                {
                    HttpOnly = true,
                    Secure = true,
                    Expires = DateTime.UtcNow.AddHours(1)
                });
                return Ok(usuario);
            }
            return BadRequest("Error");
        }

        [HttpGet("logout")]
        public ActionResult LogOut()
        {
            Response.Cookies.Delete("JWT");
            return Ok("Sesion Cerrada");
        }
    }
}
