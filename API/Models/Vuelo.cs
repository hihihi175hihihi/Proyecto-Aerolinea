using System;
using System.Collections.Generic;

namespace API.Models;

public partial class Vuelo
{
    public decimal IdVuelo { get; set; }

    public decimal IdCiudadOrigen { get; set; }

    public decimal IdCiudadDestino { get; set; }

    public decimal Precio { get; set; }

    public decimal MaxPasajeros { get; set; }

    public virtual ICollection<ComprasDetalle> ComprasDetalles { get; } = new List<ComprasDetalle>();

    public virtual ICollection<Escala> Escalas { get; } = new List<Escala>();

    public virtual ICollection<FrecuenciaVuelo> FrecuenciaVuelos { get; } = new List<FrecuenciaVuelo>();

    public virtual Ciudade IdCiudadDestinoNavigation { get; set; } = null!;

    public virtual Ciudade IdCiudadOrigenNavigation { get; set; } = null!;

    public virtual ICollection<Wishlist> Wishlists { get; } = new List<Wishlist>();
}
