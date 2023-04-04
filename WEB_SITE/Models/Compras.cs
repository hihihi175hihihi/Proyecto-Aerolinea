using System.ComponentModel.DataAnnotations;

namespace WEB_SITE.Models;

public partial class Compras
{
    [Key]
    public int idCompra { get; set; }

    public int? idCliente { get; set; }

    public DateTime? FechaCompra { get; set; }

    public decimal? Total { get; set; }
}
