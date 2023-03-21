using System;
using System.Collections.Generic;

namespace API.Models;

public partial class Bitacora
{
    public decimal IdBitacora { get; set; }

    public string Accion { get; set; } = null!;

    public string Error { get; set; } = null!;

    public string Request { get; set; } = null!;

    public string Response { get; set; } = null!;

    public string Tabla { get; set; } = null!;

    public decimal IdUsuario { get; set; }

    public DateTime Fecha { get; set; }

    public virtual Usuario IdUsuarioNavigation { get; set; } = null!;
}
