using System.Text;
using Microsoft.AspNetCore.Mvc;
using WEB_SITE.Models.ViewModelSP;
using System.Text.Json;
using WEB_SITE.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using WEB_SITE.Services;

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
            filtro.idUsuario = Convert.ToInt32(HttpContext.Session.GetString("idUsuario"));
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
            filtro.idUsuario = Convert.ToInt32(HttpContext.Session.GetString("idUsuario"));
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

        public IActionResult ListadoVuelosAdmin(){
            return View();
        }
        [HttpGet]
        public async Task<JsonResult> ListadoVuelos()
        {
            var client = _http.CreateClient("Base");
            var response = await client.GetFromJsonAsync<IEnumerable<FiltrosVuelos>>("Vuelos");
            foreach (var item in response)
            {
                item.DiaSemana = GetDayNameFromNumber(item.DiaSemana);
            }
            return Json(new { data = response });
        }

        public async Task<IActionResult> Create()
        {
            var client = _http.CreateClient("Base");
            var result = await client.GetFromJsonAsync<List<Ciudades>>("Ciudades");
            ViewData["ListaCiudades"] = result.ToSelectListItems(
               a => a.Ciudad,
               a => a.idCiudad.ToString()
               );
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(VuelosVM model)
        {
            if (!ModelState.IsValid)
            {
                TempData["ErrorCreateVuelo"] = "Error al crear el vuelo";
                return View(model);
            }
            var client = _http.CreateClient("Base");
            var vuelo = new Vuelos()
            {
                idCiudadOrigen = model.idCiudadOrigen,
                idCiudadDestino=model.idCiudadDestino,
                Precio=model.Precio
            };
            var response = await client.PostAsJsonAsync("Vuelos", vuelo);
            if (!response.IsSuccessStatusCode)
            {
                return RedirectToAction("Error");
            }
            var resultadoVuelo = await response.Content.ReadFromJsonAsync<Vuelos>();
            var frecuencia = new FrecuenciaVuelo()
            {
                idVuelo = resultadoVuelo.idVuelo,
                HoraLlegada = model.HoraLlegada,
                HoraSalida = model.HoraSalida,
                DiaSemana = model.DiaSemana
            };
            var responseFrecuencia = await client.PostAsJsonAsync("FrecuenciaVuelos", frecuencia);
            if (!responseFrecuencia.IsSuccessStatusCode)
            {
                return RedirectToAction("Error");
            }
            TempData["SuccessCreateVuelo"] = "El vuelo fue creado correctamente";
            return RedirectToAction("ListadoVuelosAdmin");
        }
        
        public async Task<IActionResult> Modify(int id)
        {
            var client = _http.CreateClient("Base");
            var result = await client.GetFromJsonAsync<Vuelos>($"Vuelos/Vuelo/{id}");
            if (result == null)
            {
                return Redirect("Error");
            }
            var resultFrecuencia = await client.GetFromJsonAsync<FrecuenciaVuelo>($"FrecuenciaVuelos/GetFrecuencia/{id}");
            var resultCiudades = await client.GetFromJsonAsync<List<Ciudades>>("Ciudades");
            ViewData["ListaCiudadesOrigen"] = resultCiudades.ToSelectListItems(
               a => a.Ciudad,
               a => a.idCiudad.ToString(),
               result.idCiudadOrigen.ToString());
            ViewData["ListaCiudadesDestino"] = resultCiudades.ToSelectListItems(
               a => a.Ciudad,
               a => a.idCiudad.ToString(),
               result.idCiudadDestino.ToString());
            var model = new VuelosVM()
            {
                idVuelo = result.idVuelo,
                idFrecuenciaVuelo = resultFrecuencia.idFrecuenciaVuelo,
                idCiudadOrigen = result.idCiudadOrigen,
                idCiudadDestino = result.idCiudadDestino,
                Precio = result.Precio,
                HoraLlegada = resultFrecuencia.HoraLlegada,
                HoraSalida = resultFrecuencia.HoraSalida,
                DiaSemana = resultFrecuencia.DiaSemana
            };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Modify(VuelosVM model)
        {
            if (!ModelState.IsValid)
            {
                TempData["ErrorModifyVuelo"] = "Error al modificar el vuelo";
                return View(model);
            }
            var vuelo = new Vuelos()
            {
                idVuelo = model.idVuelo,
                idCiudadOrigen = model.idCiudadOrigen,
                idCiudadDestino = model.idCiudadDestino,
                Precio = model.Precio
            };
            var client = _http.CreateClient("Base");
            var response = await client.PutAsJsonAsync($"Vuelos/{model.idVuelo}", vuelo);
            if (!response.IsSuccessStatusCode)
            {
                return RedirectToAction("Error");
            }
            var frecuencia = new FrecuenciaVuelo()
            {
                idFrecuenciaVuelo=model.idFrecuenciaVuelo,
                idVuelo = model.idVuelo,
                HoraLlegada = model.HoraLlegada,
                HoraSalida = model.HoraSalida,
                DiaSemana = model.DiaSemana
            };
            var responseFrecuencia = await client.PutAsJsonAsync($"FrecuenciaVuelos/{model.idFrecuenciaVuelo}", frecuencia);
            if (!responseFrecuencia.IsSuccessStatusCode)
            {
                return RedirectToAction("Error");
            }
            TempData["SuccessModifyVuelo"] = "El vuelo fue modificado correctamente";
            return RedirectToAction("ListadoVuelosAdmin");
        }
        
        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var client = _http.CreateClient("Base");
            var response = await client.DeleteAsync($"Vuelos/{id}");
            if (!response.IsSuccessStatusCode)
            {
                return Json(new { success = false });
            }
            return Json(new { success = true });
        }

    }
}
