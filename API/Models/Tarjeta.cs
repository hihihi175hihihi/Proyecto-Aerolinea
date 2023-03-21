using System;
using System.Collections.Generic;

namespace API.Models;

public partial class Tarjeta
{
    public decimal IdTarjeta { get; set; }

    public decimal IdCliente { get; set; }

    public string TokenCard { get; set; } = null!;

    public decimal Last4 { get; set; }

    public string ExpMonth { get; set; } = null!;

    public string ExpYear { get; set; } = null!;

    public decimal Brand { get; set; }

    public virtual Cliente IdClienteNavigation { get; set; } = null!;
}
