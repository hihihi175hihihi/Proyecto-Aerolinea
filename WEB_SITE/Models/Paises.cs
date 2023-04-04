using System.ComponentModel.DataAnnotations;

namespace WEB_SITE.Models;

public partial class Paises
{
    [Key]
    public int idPais { get; set; }

    public string? Pais { get; set; }
    
}
