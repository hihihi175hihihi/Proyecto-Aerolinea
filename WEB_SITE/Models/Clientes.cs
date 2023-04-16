using System.ComponentModel.DataAnnotations;

namespace WEB_SITE.Models;

public partial class Clientes
{
    [Key]
    public int idCliente { get; set; }

    public int? idUsuario { get; set; }

    public string? Nombres { get; set; }

    public string? Apellidos { get; set; }

    public string? DPI { get; set; }

    public string? Telefono { get; set; }

    public string? Email { get; set; }
}

public partial class ClientesVM
{
    public int idCliente { get; set; }

    public int? idUsuario { get; set; }
    public string? username { get; set; }

    public string? Nombres { get; set; }

    public string? Apellidos { get; set; }

    public string? DPI { get; set; }

    public string? Telefono { get; set; }

    public string? Email { get; set; }
}
