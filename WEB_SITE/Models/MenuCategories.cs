using System.ComponentModel.DataAnnotations;

namespace WEB_SITE.Models
{
    public class MenuCategories
    {
        [Key]
        public int idCategoriesMenu { get; set; }
        public string? Categoria { get; set; }
    }
}