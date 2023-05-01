using System.ComponentModel.DataAnnotations;

namespace WEB_SITE.Models;

public partial class Vuelos
{
    [Key]
    public int idVuelo { get; set; }
    [Display(Name = "Ciudad Origen")]
    public int? idCiudadOrigen { get; set; }
    [Display(Name = "Ciudad Destino")]
    public int? idCiudadDestino { get; set; }

    public decimal? Precio { get; set; }

}
public partial class VuelosVM
{
    public int idVuelo { get; set; }
    public int idFrecuenciaVuelo { get; set; }
    [Display(Name = "Ciudad Origen")]
    public int? idCiudadOrigen { get; set; }
    [Display(Name = "Ciudad Destino")]
    public int? idCiudadDestino { get; set; }

    public decimal? Precio { get; set; }
    public string? DiaSemana { get; set; }

    public string? HoraSalida { get; set; }

    public string? HoraLlegada { get; set; }

}
public class vueloById
{
    public int? idVuelo { get; set; }
    public string? CIUDAD_ORIGEN { get; set; }
    public string? CIUDAD_DESTINO { get; set; }
    public decimal? Precio { get; set; }
}
