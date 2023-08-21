using System.ComponentModel.DataAnnotations;

namespace WEB_SITE.Models;

public partial class Usuarios
{
    [Key]
    public int idUsuario { get; set; }

    public int? idRol { get; set; }

    public string? Username { get; set; }

    public string? Password { get; set; }

    public bool? Active { get; set; }
}
public class LoginViewModel
{
    [Required]
    [Display(Name = "Email")]
    public string Email { get; set; }

    [Required]
    [DataType(DataType.Password)]
    [Display(Name = "Contraseña")]
    public string Password { get; set; }
   
}
public partial class UsuariosVM
{
    public int idUsuario { get; set; }

    public int? idRol { get; set; }

    public string? Username { get; set; }
    public string? nombreRol { get; set; }

    public string? Password { get; set; }

    public bool? Active { get; set; }
}
public partial class ChangePassword
{

    public int? idUsuario { get; set; }
    public string? Password { get; set; }
}
public partial class ChangePasswordVM
{

    public int? idUsuario { get; set; }
    [Display(Name = "Contraseña Nueva")]
    [Required]
    public string? Password { get; set; }
    [Display(Name = "Repita Contraseña Nueva")]
    [Required]
    public string? ConfirmationPassword { get; set; }
}
