using System;
using System.Collections.Generic;

namespace API.Models;

public partial class Cliente
{
    public decimal IdCliente { get; set; }

    public decimal IdUsuario { get; set; }

    public string Nombres { get; set; } = null!;

    public string Apellidos { get; set; } = null!;

    public string Dpi { get; set; } = null!;

    public string Telefono { get; set; } = null!;

    public string CorreoElectronico { get; set; } = null!;

    public DateTime FechaIngreso { get; set; }

    public virtual ICollection<Compra> Compras { get; } = new List<Compra>();

    public virtual Usuario IdUsuarioNavigation { get; set; } = null!;

    public virtual ICollection<Tarjeta> Tarjeta { get; } = new List<Tarjeta>();
}
