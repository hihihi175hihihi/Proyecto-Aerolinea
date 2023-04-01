namespace API.Models.ViewModelSP
{
    public class FiltrosVuelos
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
    }
}
