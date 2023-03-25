using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace API.Models;

public partial class AccessRoles
{
    [Key]
    public int idAccessRoles { get; set; }

    public int? idRol { get; set; }

    public int? idAccess { get; set; }
}
