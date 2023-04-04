using System.ComponentModel.DataAnnotations;

namespace WEB_SITE.Models;

public partial class Pagos
{
    [Key]
    public int idPago { get; set; }

    public int? idCompra { get; set; }

    public DateTime? FechaPago { get; set; }

    public decimal? MontoPago { get; set; }
    
}
