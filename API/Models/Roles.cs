using System.ComponentModel.DataAnnotations;

namespace API.Models;

public partial class Roles
{
    [Key]
    public int idRol { get; set; }

    public string? Rol { get; set; }

}
