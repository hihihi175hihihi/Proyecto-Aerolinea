using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace API.Models;

public partial class Ciudades
{
    [Key]
    public int idCiudad { get; set; }

    public string? Ciudad { get; set; }

    public int? idPais { get; set; }

}
