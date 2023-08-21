﻿using System.ComponentModel.DataAnnotations;

namespace WEB_SITE.Models;

public partial class Empleados
{
    [Key]
    public int idEmpleado { get; set; }
    [Display(Name = "Usuario")]
    public int? idUsuario { get; set; }

    public string? CodigoEmpleado { get; set; }

    public string? Nombres { get; set; }

    public string? Apellidos { get; set; }

    public string? Direccion { get; set; }

    public string? Telefono { get; set; }
    [Display(Name = "Cargo")]
    public int? idCargo { get; set; }
}

public partial class EmpleadosVM
{
    public int idEmpleado { get; set; }
    [Display(Name = "Usuario")]
    public int? idUsuario { get; set; }

    public string? username { get; set; }

    public string? CodigoEmpleado { get; set; }

    public string? Nombres { get; set; }

    public string? Apellidos { get; set; }

    public string? Direccion { get; set; }

    public string? Telefono { get; set; }
    [Display(Name = "Cargo")]
    public int? idCargo { get; set; }
    public string? cargo { get; set; }
}
