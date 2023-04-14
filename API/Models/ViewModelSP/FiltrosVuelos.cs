using API.Services;

namespace API.Models.ViewModelSP
{
    public class FiltrosVuelos:IHasEscalas
    {
        public int? idVuelo { get; set; }
        public string? DiaSemana { get; set; }
        public string? HoraSalida { get; set; }
        public string? HoraLlegada { get; set; }
        public string? CIUDAD_ORIGEN { get; set; }
        public string? CIUDAD_DESTINO { get; set; }
        public string? PAIS_ORIGEN { get; set; }
        public string? PAIS_DESTINO { get; set; }
        public decimal? Precio { get; set; }
        public string? ESCALAS_JSON { get; set; }
        public List<EscalasVuelos>? Escalas { get; set; }
        public int? WishlistStatus { get; set; }
    }
    public class filtrosParaVuelos
    {
        public int? idUsuario { get; set; }
        public int? idVuelo { get; set; }
        public int? CIUDAD_ORIGEN { get; set; }
        public int? CIUDAD_DESTINO { get; set; }
        public int? PAIS_ORIGEN { get; set; }
        public int? PAIS_DESTINO { get; set; }
        public int? hasEscalas { get; set; }
        public int? DiaSemana { get; set; }
        public decimal? PrecioMin { get; set; }
        public decimal? PrecioMax { get; set; }
        public string? ORDENARPRECIOAS { get; set; }
        
    }
}
