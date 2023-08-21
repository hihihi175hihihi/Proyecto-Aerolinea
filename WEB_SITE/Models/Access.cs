using System.ComponentModel.DataAnnotations;

namespace WEB_SITE.Models;

public partial class Access
{
    [Key]
    public int idAccess { get; set; }

    public string? Name { get; set; }

    public string? URL { get; set; }
    public int idCategoriesMenu { get; set; }
    public string? Icon { get; set; }
}
