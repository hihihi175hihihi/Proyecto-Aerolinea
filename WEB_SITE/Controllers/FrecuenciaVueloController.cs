using Microsoft.AspNetCore.Mvc;
using WEB_SITE.Models;

namespace WEB_SITE.Controllers
{
    public class FrecuenciaVueloController : Controller
    {
        private readonly IHttpClientFactory _http;
        public FrecuenciaVueloController(IHttpClientFactory http)
        {
            _http = http;
        }
        public async Task<IActionResult> Index()
        {
            var client = _http.CreateClient("Base");
            var response = await client.GetFromJsonAsync<List<FrecuenciaVueloVM>>("FrecuenciaVuelos");
            if (response == null)
            {
                return View(new List<FrecuenciaVueloVM>());
            }
            foreach (var item in response)
            {
                item.DiaSemana = GetDayNameFromNumber(item.DiaSemana);
            }
            return View(response);
        }

        public IActionResult Create()
        {
            return View();
        }
        public IActionResult Modify()
        {
            return View();
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
