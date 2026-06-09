using AppPracticaASP.NET.DTO.Productos;
using AppPracticaASP.NET.Models;
using AppPracticaASP.NET.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AppPracticaASP.NET.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductoController : ControllerBase
    {
        private readonly IProductosService _productosService;

        public ProductoController(IProductosService productosService)
        {
            _productosService = productosService;
        }

        [HttpGet("listar/filtros")]
        public async Task<ActionResult<Product>> listarProductos([FromQuery] ProductosFilterDTO data)
        {
            var respuesta = await _productosService.Listar(data);
            if (respuesta == null) return BadRequest("Error");
            return Ok(respuesta);
        }
    }
}
