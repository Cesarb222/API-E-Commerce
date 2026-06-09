using AppPracticaASP.NET.DTO;
using AppPracticaASP.NET.Models;
using AppPracticaASP.NET.Services;
using AppPracticaASP.NET.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;




namespace AppPracticaASP.NET.Controllers
{
    //Toma como nombre de la clase, seria /api/Usuario/
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly IUsuarioService _usuarioService;
        public UsuarioController(IUsuarioService usuarioService)
        {
            _usuarioService = usuarioService;
        }
        [HttpGet("listar")]
        [Authorize(Roles = "Admin")]
        
        //IEnumerable<T> es una interfaz fundamental en C# que representa una secuencia de elementos sobre la cual puedes iterar (recorrer)
        public async Task<ActionResult<IEnumerable<User>>> listarUsuarios()
        {
            //string? buenas = Request.Query["hola"];
            //if (buenas != null) Console.WriteLine(buenas);

            var usuarios = await _usuarioService.GetALL();
            return Ok(usuarios);
        }




        [HttpPut("edit/{guid}")]
        public async Task<ActionResult<UserResponseDTO>> actualizarUsuario(string guid, [FromBody] UserCreateDTO userCreateDTO)
        {
            var usuario = await _usuarioService.ActualizarUsuario(guid, userCreateDTO);
            if (usuario == null) return NotFound();
            return Ok(usuario);
        }

    }
}
