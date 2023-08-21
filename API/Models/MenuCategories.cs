using System.ComponentModel.DataAnnotations;

namespace API.Models
{
    public class MenuCategories
    {
        [Key]
        public int idCategoriesMenu { get; set; }
        public string? Categoria { get; set; }
    }
}