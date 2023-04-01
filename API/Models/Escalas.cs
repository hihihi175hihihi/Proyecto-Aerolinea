using System.ComponentModel.DataAnnotations;

namespace API.Models;

public partial class Escalas
{
    [Key]
    public int? idEscala { get; set; }

    public int? idVuelo { get; set; }

    public int? idCiudadEscala { get; set; }

    public string? DuracionEscala { get; set; }

    public string? DuracionLlegada { get; set; }
}
public class EscalasVuelos
{

    public string? CIUDAD_ESCALA { get; set; }

    public string? DuracionEscala { get; set; }

    public string? DuracionLlegada { get; set; }
}

