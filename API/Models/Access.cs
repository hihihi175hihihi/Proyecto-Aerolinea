﻿using System.ComponentModel.DataAnnotations;

namespace API.Models;

public partial class Access
{
    [Key]
    public int idAccess { get; set; }

    public string? Name { get; set; }

    public string? URL { get; set; }
    public int idCategoriesMenu { get; set; }
    public string? Icon { get; set; }
}
