using System;
using System.Collections.Generic;

namespace API.Models;

public partial class Accessrole
{
    public decimal IdAccessRoles { get; set; }

    public decimal IdRol { get; set; }

    public decimal IdAccess { get; set; }

    public virtual Acceso IdAccessNavigation { get; set; } = null!;

    public virtual Role IdRolNavigation { get; set; } = null!;
}
