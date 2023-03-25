using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace API.Models;

public partial class Paises
{
    [Key]
    public int idPais { get; set; }

    public string? Pais { get; set; }
    
}
