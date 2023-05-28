using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
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
                TempData["ErrorCreateFrecuenciaVuelo"] = "Error al crear la frecuencia de vuelo";
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
            TempData["SuccessCreateFrecuenciaVuelo"] = "Frecuencia creada correctamente";
            return RedirectToAction("Index");
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
                    vuelos=response.ToSelectListItems(
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
        public class Day
        {
            public string Id { get; set; }
            public string Name { get; set; }
        }
        public async Task<IActionResult> Modify(int id)
        {
            var client = _http.CreateClient("Base");
            var response = await client.GetFromJsonAsync<FrecuenciaVuelo>($"FrecuenciaVuelos/{id}");
            if (response == null)
            {
                return RedirectToAction("Error");
            }
            List<Day> days = new List<Day>
            {
                new Day { Id = "1", Name = "Lunes" },
                new Day { Id = "2", Name = "Martes" },
                new Day { Id = "3", Name = "Miércoles" },
                new Day { Id = "4", Name = "Jueves" },
                new Day { Id = "5", Name = "Viernes" },
                new Day { Id = "6", Name = "Sábado" },
                new Day { Id = "7", Name = "Domingo" }
            };
            ViewData["ListadoDias"] = days.ToSelectListItems(
                d=>d.Name,
                d=>d.Id.ToString(),
                response.DiaSemana
                );
            return View(response);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Modify(FrecuenciaVuelo model)
        {
            if (!ModelState.IsValid)
            {
                TempData["ErrorModifyFrecuenciaVuelo"] = "Error al modificar la frecuencia de vuelo";
                return View("Error");
            }
            var client = _http.CreateClient("Base");
            var response = await client.PutAsJsonAsync($"FrecuenciaVuelos/{model.idFrecuenciaVuelo}", model);
            if (!response.IsSuccessStatusCode)
            {
                return RedirectToAction("Error");
            }
            TempData["SuccessModifyFrecuenciaVuelo"] = "Frecuencia de Vuelo modificada correctamente";
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var client = _http.CreateClient("Base");
            var response = await client.DeleteAsync($"FrecuenciaVuelos/{id}");
            if (!response.IsSuccessStatusCode)
            {
                return Json(new { success = false });
            }
            return Json(new { success = true });
        }
    }
}
