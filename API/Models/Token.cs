using System;
using System.Collections.Generic;

namespace API.Models;

public partial class Token
{
    public decimal IdToken { get; set; }

    public decimal IdUsuario { get; set; }

    public string Token1 { get; set; } = null!;

    public DateTime CreateAt { get; set; }

    public DateTime Expiration { get; set; }

    public string Active { get; set; } = null!;

    public virtual Usuario IdUsuarioNavigation { get; set; } = null!;
}
