using System.ComponentModel.DataAnnotations;

namespace WEB_SITE.Models;

public partial class Cargos
{
    [Key]
    public int idCargo { get; set; }

    public string? Cargo { get; set; }
    
}
