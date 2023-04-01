using API.Models;
using Newtonsoft.Json;

namespace API.Services
{
    public static class DeserializeEscalas
    {
        public static void DeserializeEscalasJson<T>(this List<T> list) where T : class
        {
            if (typeof(IHasEscalas).IsAssignableFrom(typeof(T)))
            {
                foreach (var item in list)
                {
                    var hasEscalasItem = item as IHasEscalas;

                    if (!string.IsNullOrEmpty(hasEscalasItem.ESCALAS_JSON))
                    {
                        hasEscalasItem.Escalas = JsonConvert.DeserializeObject<List<EscalasVuelos>>(hasEscalasItem.ESCALAS_JSON);
                    }
                }
            }
        }
    }
}
