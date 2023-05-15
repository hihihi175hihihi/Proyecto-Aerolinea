namespace WEB_SITE.Models
{
    public class MenuVM
    {
        public int IdAccessRoles { get; set; }
        public int IdRol { get; set; }
        public int IdAccess { get; set; }
        public string? Rol { get; set; }
        public string? Access { get; set; }
        public string? Url { get; set; }
        public int idCategoriesMenu { get; set; }
        public string? Categoria { get; set; }
        public string? Icon { get; set; }
    }
}
