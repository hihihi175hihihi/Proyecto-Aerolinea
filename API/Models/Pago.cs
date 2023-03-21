using System;
using System.Collections.Generic;

namespace API.Models;

public partial class Pago
{
    public decimal IdPago { get; set; }

    public decimal IdCompra { get; set; }

    public DateTime FechaPago { get; set; }

    public decimal MontoPago { get; set; }

    public virtual Compra IdCompraNavigation { get; set; } = null!;
}
