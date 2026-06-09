using AppPracticaASP.NET.DTO.Productos;
using AppPracticaASP.NET.Models;

namespace AppPracticaASP.NET.Repository.Interfaces
{
    public interface IProductosRepository
    {
        Task<IEnumerable<Product>>? GetAll(ProductosFilterDTO data);
    }
}
