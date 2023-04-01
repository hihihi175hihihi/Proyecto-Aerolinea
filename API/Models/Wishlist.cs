using System.ComponentModel.DataAnnotations;

namespace API.Models;

public partial class WishList
{
    [Key]
    public int idWishList { get; set; }

    public int? idUsuario { get; set; }

    public int? idVuelo { get; set; }

    public DateTime? FechaSave { get; set; }
    
}
