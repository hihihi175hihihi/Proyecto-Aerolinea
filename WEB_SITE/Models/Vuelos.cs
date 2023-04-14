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

public class vueloById
{
    public int? idVuelo { get; set; }
    public string? CIUDAD_ORIGEN { get; set; }
    public string? CIUDAD_DESTINO { get; set; }
    public decimal? Precio { get; set; }
}
