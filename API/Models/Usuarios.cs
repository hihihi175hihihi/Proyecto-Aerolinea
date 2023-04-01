using System.ComponentModel.DataAnnotations;

namespace API.Models;

public partial class Usuarios
{
    [Key]
    public int idUsuario { get; set; }

    public int? idRol { get; set; }

    public string? Username { get; set; }

    public string? Password { get; set; }

    public bool? Active { get; set; }
}
