using System;
using System.Collections.Generic;

namespace API.Models;

public partial class Escala
{
    public decimal IdEscala { get; set; }

    public decimal IdVuelo { get; set; }

    public decimal IdCiudadEscala { get; set; }

    public string DuracionEscala { get; set; } = null!;

    public string DuracionLlegada { get; set; } = null!;

    public virtual Ciudade IdCiudadEscalaNavigation { get; set; } = null!;

    public virtual Vuelo IdVueloNavigation { get; set; } = null!;
}
