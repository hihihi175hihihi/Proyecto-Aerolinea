using System.ComponentModel.DataAnnotations;

namespace WEB_SITE.Models;

public partial class Compras
{
    [Key]
    public int idCompra { get; set; }

    public int? idCliente { get; set; }

    public DateTime? FechaCompra { get; set; } = DateTime.Now;

    public decimal? Total { get; set; }
}

public class RequestCompraCardSave
{
    public int? idCliente { get; set; }

    public DateTime? FechaCompra { get; set; } = DateTime.Now;

    public decimal? Total { get; set; }
    public int? idVuelo { get; set; }

    public int? Cantidad { get; set; }
    public int idTarjeta { get; set; }
    

}
