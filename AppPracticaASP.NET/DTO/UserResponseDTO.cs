using AppPracticaASP.NET.Models;

namespace AppPracticaASP.NET.DTO
{
    public class UserResponseDTO
    {
        public Guid? Id { get; set; }

        public string Nombre { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;

        public string Rol { get; set; } = string.Empty;

        public DateTime? FechaRegistro { get; set; }
        public ICollection<Order>? Orders { get; set; } = new List<Order>();

        public ICollection<Review>? Reviews { get; set; } = new List<Review>();


    }
}
