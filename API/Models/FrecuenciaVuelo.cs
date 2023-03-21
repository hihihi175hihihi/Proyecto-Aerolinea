using System;
using System.Collections.Generic;

namespace API.Models;

public partial class FrecuenciaVuelo
{
    public decimal IdFrecuenciaVuelo { get; set; }

    public decimal IdVuelo { get; set; }

    public string DiaSemana { get; set; } = null!;

    public string HoraSalida { get; set; } = null!;

    public string HoraLlegada { get; set; } = null!;

    public virtual Vuelo IdVueloNavigation { get; set; } = null!;
}
