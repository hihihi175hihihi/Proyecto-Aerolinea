using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace API.Models;

public partial class Compras
{
    [Key]
    public int idCompra { get; set; }

    public int? idCliente { get; set; }

    public DateTime? FechaCompra { get; set; }

    public decimal? Total { get; set; }
}
