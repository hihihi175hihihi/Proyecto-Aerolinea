using System;
using System.Collections.Generic;

namespace API.Models;

public partial class ComprasDetalle
{
    public decimal IdComprasDetalle { get; set; }

    public decimal IdCompra { get; set; }

    public decimal IdVuelo { get; set; }

    public decimal Cantidad { get; set; }

    public virtual Compra IdCompraNavigation { get; set; } = null!;

    public virtual Vuelo IdVueloNavigation { get; set; } = null!;
}
