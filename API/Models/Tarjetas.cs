using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace API.Models;

public partial class Tarjetas
{
    [Key]
    public int idTarjeta { get; set; }

    public int? idCliente { get; set; }

    public string? TokenCard { get; set; }

    public int? Last4 { get; set; }

    public string? ExpMonth { get; set; }

    public string? ExpYear { get; set; }

    public string? Brand { get; set; }
    public int? Cvs { get; set; }
}
