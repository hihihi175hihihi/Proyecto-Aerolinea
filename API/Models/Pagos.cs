using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace API.Models;

public partial class Pagos
{
    [Key]
    public int idPago { get; set; }

    public int? idCompra { get; set; }

    public DateTime? FechaPago { get; set; }

    public decimal? MontoPago { get; set; }
    
}
