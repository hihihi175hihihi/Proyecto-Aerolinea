using System;
using System.Collections.Generic;

namespace API.Models;

public partial class Acceso
{
    public decimal IdAccess { get; set; }

    public string Name { get; set; } = null!;

    public string Url { get; set; } = null!;

    public virtual ICollection<Accessrole> Accessroles { get; } = new List<Accessrole>();
}
