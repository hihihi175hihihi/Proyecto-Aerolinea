﻿using System.ComponentModel.DataAnnotations;

namespace WEB_SITE.Models;

public partial class Tokens
{
    [Key]
    public int idToken { get; set; }

    public int? idUsuario { get; set; }

    public string? Token { get; set; }

    public DateTime? CreateAt { get; set; }

    public DateTime? Expiration { get; set; }

    public bool? Active { get; set; }
}
public partial class resetPassword
{
    [Key]
    public int idToken { get; set; }

    public int? idUsuario { get; set; }

    public string? Token { get; set; }

    public DateTime? CreateAt { get; set; }

    public DateTime? Expiration { get; set; }

    public bool? Active { get; set; }
    public string? Password { get; set; }
}
public class TokenVM
{
    [Required]
    [Display(Name = "Token")]
    public string Token { get; set; }
}