namespace WEB_SITE.Models.ViewModelSP
{
    public class GenerateItinerary : IHasEscalas
    {
        public string? DiaSemana { get; set; }
        public string? HoraSalida { get; set; }
        public string? HoraLlegada { get; set; }
        public string? CIUDAD_ORIGEN { get; set; }
        public string? CIUDAD_DESTINO { get; set; }
        public string? PAIS_ORIGEN { get; set; }
        public string? PAIS_DESTINO { get; set; }
    
        public string? ESCALAS_JSON { get; set; }
        public List<EscalasVuelos>? Escalas { get; set; }
    }
}
