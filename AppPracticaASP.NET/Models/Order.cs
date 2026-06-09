using System;
using System.Collections.Generic;

namespace AppPracticaASP.NET.Models;

public partial class Order
{
    public int Id { get; set; }

    public Guid UserId { get; set; }

    public DateTime FechaCreacion { get; set; }

    public string Estado { get; set; } = null!;

    public decimal Total { get; set; }

    public virtual ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();

    public virtual User User { get; set; } = null!;
}
