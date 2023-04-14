using System.Text;
using Microsoft.AspNetCore.Mvc;
using WEB_SITE.Models.ViewModelSP;
using System.Text.Json;
using WEB_SITE.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace WEB_SITE.Controllers
{
    public class VuelosController : Controller
    {
        private readonly IHttpClientFactory _http;
        public VuelosController(IHttpClientFactory http)
        {
            _http = http;
        }
        
        public async Task<IActionResult> Index()
        {
            var client = _http.CreateClient("Base");
            var filtro = new filtrosParaVuelos();
            filtro.idUsuario = 1;
            var content = JsonSerializer.Serialize(filtro);
            var contenido = new StringContent(content, Encoding.UTF8, "application/json");
            var respuesta = await client.PostAsync("Vuelos/Filtros", contenido);
            var response = await respuesta.Content.ReadFromJsonAsync<List<FiltrosVuelos>>();
            if (response != null)
            {
                foreach (var item in response)
                {
                    item.DiaSemana = GetDayNameFromNumber(item.DiaSemana);
                }
            
                ViewData["Ciudades"] = await GetCiudades();
                ViewData["Paises"] = await GetPaises();
            return View(response);
            }
            return View(new List<FiltrosVuelos>());
        }
        [HttpPost]
        public async Task<IActionResult> Filtrado(filtrosParaVuelos filtro)
        {
            var client = _http.CreateClient("Base");
            filtro.idUsuario = 1;
            var content = JsonSerializer.Serialize(filtro);
            var contenido = new StringContent(content, Encoding.UTF8, "application/json");
            var respuesta = await client.PostAsync("Vuelos/Filtros", contenido);
            var response = await respuesta.Content.ReadFromJsonAsync<List<FiltrosVuelos>>();
            if (response != null)
            {
                foreach (var item in response)
                {
                    item.DiaSemana = GetDayNameFromNumber(item.DiaSemana);
                }
                
                ViewData["Ciudades"] = await GetCiudades();
                ViewData["Paises"] = await GetPaises();
                return Json(response);
            }
            return Json(new List<FiltrosVuelos>());
        }

        private async Task<List<SelectListItem>> GetCiudades()
        {
            var client = _http.CreateClient("Base");
            var result = await client.GetFromJsonAsync<List<Ciudades>>("Ciudades");
            var ciudades = result.ConvertAll(t =>
            {
                return new SelectListItem()
                {
                    Text = t.Ciudad,
                    Value = t.idCiudad.ToString(),
                    Selected = false
                };
            });
            return ciudades;
        }
        private async Task<List<SelectListItem>> GetPaises()
        {
            var client = _http.CreateClient("Base");
            var result = await client.GetFromJsonAsync<List<Paises>>("Paises");
            var paises = result.ConvertAll(t =>
            {
                return new SelectListItem()
                {
                    Text = t.Pais,
                    Value = t.idPais.ToString(),
                    Selected = false
                };
            });
            return paises;
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
