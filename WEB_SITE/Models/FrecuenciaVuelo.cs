using System.ComponentModel.DataAnnotations;

namespace WEB_SITE.Models;

public partial class FrecuenciaVuelo
{
    public int idFrecuenciaVuelo { get; set; }

    [Display(Name = "Vuelo")]
    public int? idVuelo { get; set; }

    public string? DiaSemana { get; set; }

    public string? HoraSalida { get; set; }

    public string? HoraLlegada { get; set; }   
    
}

public partial class FrecuenciaVueloVM
{
    public int idFrecuenciaVuelo { get; set; }

    [Display(Name ="Vuelo")]
    public int? idVuelo { get; set; }

    public string? DiaSemana { get; set; }

    public string? HoraSalida { get; set; }

    public string? HoraLlegada { get; set; }

}

