using Microsoft.AspNetCore.Mvc;
using WEB_SITE.Models.ViewModelSP;
using X.PagedList;

namespace WEB_SITE.Controllers
{
    public class VuelosController : Controller
    {
        private readonly IHttpClientFactory _http;
        public VuelosController(IHttpClientFactory http)
        {
            _http = http;
        }
        public async Task<IActionResult> Index(int? page, int pageSize = 10)
        {
            int pageNumber = (page ?? 1);
            var client = _http.CreateClient("Base");
            var response = await client.GetFromJsonAsync<List<FiltrosVuelos>>("Vuelos");
            if (response != null)
            {
                foreach (var item in response)
                {
                    item.DiaSemana = GetDayNameFromNumber(item.DiaSemana);
                }
            

            var items = response.OrderBy(i => i.idVuelo);
            return View(items.ToPagedList(pageNumber, pageSize));
            }
            return View(new List<FiltrosVuelos>());
        }
        public static string GetDayNameFromNumber(string dayNumber)
        {
            return dayNumber switch
            {
                "1" => "Lunes",
                "2" => "Martes",
                "3" => "Miércoles",
                "4" => "Jueves",
                "5" => "Viernes",
                "6" => "Sábado",
                "7" => "Domingo"
            };
        }
    }
}
