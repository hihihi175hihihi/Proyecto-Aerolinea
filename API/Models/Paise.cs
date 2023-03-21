using System;
using System.Collections.Generic;

namespace API.Models;

public partial class Paise
{
    public decimal IdPais { get; set; }

    public string NombrePais { get; set; } = null!;

    public virtual ICollection<Ciudade> Ciudades { get; } = new List<Ciudade>();
}
