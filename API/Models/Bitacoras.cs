using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace API.Models;

public partial class Bitacoras
{
    [Key]
    public int idBitacora { get; set; }

    public string? Action { get; set; }

    public string? Error { get; set; }

    public string? Request { get; set; }

    public string? Response { get; set; }

    public string? Tabla { get; set; }

    public int? idUsuario { get; set; }

    public DateTime? Fecha { get; set; }
    
}
