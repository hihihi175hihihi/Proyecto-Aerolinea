using System.ComponentModel.DataAnnotations;

namespace WEB_SITE.Models;

public partial class Roles
{
    [Key]
    public int idRol { get; set; }

    public string? Rol { get; set; }

}
