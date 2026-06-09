using AppPracticaASP.NET.DTO.Productos;
using AppPracticaASP.NET.Models;
using AppPracticaASP.NET.Repository.Interfaces;
using AppPracticaASP.NET.Services.Interfaces;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace AppPracticaASP.NET.Services
{
    public class ProductosServices : IProductosService
    {
        private readonly IProductosRepository _productosRepository;
        public ProductosServices(IProductosRepository productosRepository) { 
            _productosRepository = productosRepository;
        }
        public async Task<IEnumerable<ProductoResponseDTO>> Listar(ProductosFilterDTO filtros)
        {
            var respuesta = await _productosRepository.GetAll(filtros);
            if (respuesta == null) return null;
            var lista = respuesta.Select(p => MapProducto(p)).ToList();
            return lista;
        }

        private ProductoResponseDTO MapProducto(Product p)
        {
            return new ProductoResponseDTO
            {
                Id = p.Id,
                Nombre = p.Nombre,
                Descripcion = p.Descripcion,
                Precio = p.Precio,
                Stock = p.Stock,
                ImagenUrl = p.ImagenUrl,
                CategoriaNombre = p.Category?.Nombre ?? "Sin categoría"
            };
        }
    }
}
