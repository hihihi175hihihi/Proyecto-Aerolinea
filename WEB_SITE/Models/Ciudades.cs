using System.ComponentModel.DataAnnotations;

namespace WEB_SITE.Models;

public partial class Ciudades
{
    [Key]
    public int idCiudad { get; set; }

    public string? Ciudad { get; set; }

    public int? idPais { get; set; }

}
public partial class CiudadesVM
{
    public int idCiudad { get; set; }

    public string? Ciudad { get; set; }

    public int? idPais { get; set; }
    public string? Pais { get; set; }

}
