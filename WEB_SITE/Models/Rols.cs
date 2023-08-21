using System.ComponentModel.DataAnnotations;

namespace WEB_SITE.Models;

public partial class Rols
{
    [Key]
    public int idRol { get; set; }

    public string? Rol { get; set; }

}
