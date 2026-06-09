using AppPracticaASP.NET.DTO.Productos;
using AppPracticaASP.NET.Models;
using AppPracticaASP.NET.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace AppPracticaASP.NET.Repository
{
    public class ProductosRepository : IProductosRepository
    {
        private readonly BddEcomerceContext _context;

        public ProductosRepository(BddEcomerceContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Product>>? GetAll(ProductosFilterDTO data)
        {
            if (data == null) return null;
            var query = _context.Products.AsQueryable();

            if (data.CategoriaId.HasValue) query = query.Where(p=> p.CategoryId == data.CategoriaId.Value);
            if (!string.IsNullOrEmpty(data.Nombre)) query = query.Where(p => p.Nombre.Contains(data.Nombre));
            if (data.PrecioMin.HasValue) query = query.Where(p => p.Precio >= data.PrecioMin);
            if (data.PrecioMax.HasValue) query = query.Where(p => p.Precio <= data.PrecioMin);

            var resultado = await query
                .Include(u => u.Category)
                .Skip((data.Pagina - 1) * data.Cantidad)
                .Take(data.Cantidad)
                .ToListAsync();
            return resultado;
        }
    }

}
