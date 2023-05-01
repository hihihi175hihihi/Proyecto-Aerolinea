using System.ComponentModel.DataAnnotations;

namespace WEB_SITE.Models;

public partial class AccessRoles
{
    [Key]
    public int idAccessRoles { get; set; }
    [Display(Name ="Acceso")]
    public int? idAccess { get; set; }
    [Display(Name = "Rol")]
    public int? idRol { get; set; }

}

public partial class AccessRolesVM
{
    public int idAccessRoles { get; set; }
    public int? idRol { get; set; }
    public string? rol { get; set; }
    public string? access { get; set; }
    public int? idAccess { get; set; }
}
