using System;
using System.Collections.Generic;

namespace API.Models;

public partial class Cargo
{
    public decimal IdCargo { get; set; }

    public string? Cargo1 { get; set; }

    public virtual ICollection<Empleado> Empleados { get; } = new List<Empleado>();
}
