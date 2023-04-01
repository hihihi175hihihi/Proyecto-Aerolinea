using API.Models;

namespace API.Services
{
    public interface IHasEscalas
    {
        string? ESCALAS_JSON { get; set; }
        List<EscalasVuelos>? Escalas { get; set; }
    }
}
