using System;
using System.Collections.Generic;

namespace API.Models;

public partial class Compra
{
    public decimal IdCompra { get; set; }

    public decimal IdCliente { get; set; }

    public DateTime FechaCompra { get; set; }

    public decimal Total { get; set; }

    public virtual ICollection<ComprasDetalle> ComprasDetalles { get; } = new List<ComprasDetalle>();

    public virtual Cliente IdClienteNavigation { get; set; } = null!;

    public virtual ICollection<Pago> Pagos { get; } = new List<Pago>();
}
