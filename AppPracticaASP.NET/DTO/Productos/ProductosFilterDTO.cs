namespace AppPracticaASP.NET.DTO.Productos
{
    public class ProductosFilterDTO
    {
      
        public int? CategoriaId { get; set; }
        public decimal? PrecioMin { get; set; }
        public decimal? PrecioMax { get; set; }
        public string? Nombre { get; set; }
        public int Pagina { get; set; } = 1;
        public int Cantidad { get; set; } = 10;
        
    }
}
