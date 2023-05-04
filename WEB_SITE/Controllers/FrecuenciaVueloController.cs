using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using WEB_SITE.Models;
using WEB_SITE.Models.ViewModelSP;
using WEB_SITE.Services;

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

        public async Task<IActionResult> Create()
        {
            ViewData["ListadoVuelos"] = await GetVuelos();
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(FrecuenciaVuelo model)
        {
            if (!ModelState.IsValid)
            {
                ViewData["ListadoVuelos"] = await GetVuelos();
                return View(model);
            }
            if (String.IsNullOrEmpty(model.DiaSemana))
            {
                ViewData["ListadoVuelos"] = await GetVuelos();
                return View(model);
            }
            var client = _http.CreateClient("Base");
            var response = await client.PostAsJsonAsync("FrecuenciaVuelos", model);
            if (!response.IsSuccessStatusCode)
            {
                return Redirect("Error");
            }
            TempData["CreateFrecuenciaVuelo"] = "Frecuencia creada correctamente";
            return RedirectToAction("Index");
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
        private async Task<List<SelectListItem>> GetVuelos(int? vuelo = null)
        {
            var client = _http.CreateClient("Base");

            var vuelos = new List<SelectListItem>();
            var response = await client.GetFromJsonAsync<List<FiltrosVuelos>>("Vuelos");
            if (response != null)
            {

                if (vuelo == null)
                {
                    vuelos = response.ToSelectListItems(
                 v => String.Concat("#", v.idVuelo, "-", v.CIUDAD_ORIGEN, " / ", v.CIUDAD_DESTINO),
                 v => v.idVuelo.ToString()
                 );
                }
                else
                {
                    vuelos = response.ToSelectListItems(
                    v => String.Concat("#", v.idVuelo, "-", v.CIUDAD_ORIGEN, " / ", v.CIUDAD_DESTINO),
                    v => v.idVuelo.ToString(),
                    vuelo.ToString()
                    );
                }
            }
            return vuelos;
        }
    }
}
