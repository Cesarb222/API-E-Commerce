namespace AppPracticaASP.NET.DTO.Productos
{
    public class ProductoResponseDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string? Descripcion { get; set; }
        public decimal Precio { get; set; }
        public int Stock { get; set; }
        public string? ImagenUrl { get; set; }
        public string CategoriaNombre { get; set; } = string.Empty;
    }
}
