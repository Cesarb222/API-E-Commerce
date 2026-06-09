using System;
using System.Collections.Generic;

namespace AppPracticaASP.NET.Models;

public partial class Category
{
    public int Id { get; set; }

    public string Nombre { get; set; } = null!;

    public string? Descripcion { get; set; }

    public virtual ICollection<Product> Products { get; set; } = new List<Product>();
}
