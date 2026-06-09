using AppPracticaASP.NET.DTO.Productos;
using AppPracticaASP.NET.Models;
using Microsoft.AspNetCore.Mvc;

namespace AppPracticaASP.NET.Services.Interfaces
{
    public interface IProductosService
    {
        Task<IEnumerable<ProductoResponseDTO>> Listar(ProductosFilterDTO data);
    }
}
