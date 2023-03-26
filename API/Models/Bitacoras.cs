using System.ComponentModel.DataAnnotations;

namespace API.Models;

public partial class Bitacoras
{
    [Key]
    public int idBitacora { get; set; }
    public string Tipo { get; set; }
    public string? Action { get; set; }

    public string? Error { get; set; }

    public string? Request { get; set; }

    public string? Response { get; set; }
        
    public DateTime? Fecha { get; set; }
    
}
