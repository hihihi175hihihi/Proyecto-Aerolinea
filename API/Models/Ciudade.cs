using System;
using System.Collections.Generic;

namespace API.Models;

public partial class Ciudade
{
    public decimal IdCiudad { get; set; }

    public string NombreCiudad { get; set; } = null!;

    public decimal IdPais { get; set; }

    public virtual ICollection<Escala> Escalas { get; } = new List<Escala>();

    public virtual Paise IdPaisNavigation { get; set; } = null!;

    public virtual ICollection<Vuelo> VueloIdCiudadDestinoNavigations { get; } = new List<Vuelo>();

    public virtual ICollection<Vuelo> VueloIdCiudadOrigenNavigations { get; } = new List<Vuelo>();
}
