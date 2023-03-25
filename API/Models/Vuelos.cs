using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace API.Models;

public partial class Vuelos
{
    [Key]
    public int idVuelo { get; set; }

    public int? idCiudadOrigen { get; set; }

    public int? idCiudadDestino { get; set; }

    public decimal? Precio { get; set; }

    public int? MaxPasajeros { get; set; }
}
