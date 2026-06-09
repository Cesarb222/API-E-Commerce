using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace AppPracticaASP.NET.Models;

public partial class User
{

    public Guid? Id { get; set; }

    public string Nombre { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string PasswordHash { get; set; } = null!;

    public string? Rol { get; set; } = null!;

    public DateTime? FechaRegistro { get; set; }

    public virtual ICollection<Order>? Orders { get; set; } = new List<Order>();

    public virtual ICollection<Review>? Reviews { get; set; } = new List<Review>();
}
