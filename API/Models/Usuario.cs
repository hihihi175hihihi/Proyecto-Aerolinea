using System;
using System.Collections.Generic;

namespace API.Models;

public partial class Usuario
{
    public decimal IdUsuario { get; set; }

    public decimal IdRol { get; set; }

    public string Username { get; set; } = null!;

    public string Password { get; set; } = null!;

    public string Active { get; set; } = null!;

    public virtual ICollection<Bitacora> Bitacoras { get; } = new List<Bitacora>();

    public virtual Cliente? Cliente { get; set; }

    public virtual Empleado? Empleado { get; set; }

    public virtual Role IdRolNavigation { get; set; } = null!;

    public virtual ICollection<Token> Tokens { get; } = new List<Token>();

    public virtual ICollection<Wishlist> Wishlists { get; } = new List<Wishlist>();
}
