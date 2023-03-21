using System;
using System.Collections.Generic;

namespace API.Models;

public partial class Empleado
{
    public decimal IdEmpleado { get; set; }

    public decimal IdUsuario { get; set; }

    public string CodigoEmpleado { get; set; } = null!;

    public string Nombre { get; set; } = null!;

    public string Apellido { get; set; } = null!;

    public string Direccion { get; set; } = null!;

    public string Telefono { get; set; } = null!;

    public decimal IdCargo { get; set; }

    public virtual Cargo IdCargoNavigation { get; set; } = null!;

    public virtual Usuario IdUsuarioNavigation { get; set; } = null!;
}
