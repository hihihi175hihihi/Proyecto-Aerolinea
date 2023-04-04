using System.ComponentModel.DataAnnotations;

namespace WEB_SITE.Models;

public partial class Vuelos
{
    [Key]
    public int idVuelo { get; set; }

    public int? idCiudadOrigen { get; set; }

    public int? idCiudadDestino { get; set; }

    public decimal? Precio { get; set; }
    
}
