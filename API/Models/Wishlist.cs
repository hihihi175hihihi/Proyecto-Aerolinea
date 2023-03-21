using System;
using System.Collections.Generic;

namespace API.Models;

public partial class Wishlist
{
    public decimal IdWishlist { get; set; }

    public decimal IdUsuario { get; set; }

    public decimal IdVuelo { get; set; }

    public DateTime FechaCreacion { get; set; }

    public virtual Usuario IdUsuarioNavigation { get; set; } = null!;

    public virtual Vuelo IdVueloNavigation { get; set; } = null!;
}
