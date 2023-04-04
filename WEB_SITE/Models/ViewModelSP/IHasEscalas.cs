namespace WEB_SITE.Models.ViewModelSP
{
    public interface IHasEscalas
    {
        string? ESCALAS_JSON { get; set; }
        List<EscalasVuelos>? Escalas { get; set; }
    }
}
