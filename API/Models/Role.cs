using System;
using System.Collections.Generic;

namespace API.Models;

public partial class Role
{
    public decimal IdRol { get; set; }

    public string Rol { get; set; } = null!;

    public virtual ICollection<Accessrole> Accessroles { get; } = new List<Accessrole>();

    public virtual ICollection<Usuario> Usuarios { get; } = new List<Usuario>();
}
