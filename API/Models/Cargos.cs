using System.ComponentModel.DataAnnotations;

namespace API.Models;

public partial class Cargos
{
    [Key]
    public int idCargo { get; set; }

    public string? Cargo { get; set; }
    
}
