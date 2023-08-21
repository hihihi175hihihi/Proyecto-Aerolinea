using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using WEB_SITE.Models;
using WEB_SITE.Models.ViewModelSP;
using WEB_SITE.Services;

namespace WEB_SITE.Controllers
{
    
    public class EscalasController : Controller
    {
        private readonly IHttpClientFactory _http;
        public EscalasController(IHttpClientFactory http)
        {
            _http = http;
        }
        public async Task<IActionResult> Index()
        {
            var client = _http.CreateClient("Base");
            var response = await client.GetFromJsonAsync<List<EscalasVM>>("Escalas");
            if (response == null)
            {
                return View(new List<EscalasVM>());
            }
            return View(response);
        }

        public async Task<IActionResult> Create()
        {
            (ViewData["ListadoCiudad"], ViewData["ListadoVuelos"]) = await GetVueloCiudad();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Escalas model)
        {
            if (!ModelState.IsValid)
            {
                TempData["ErrorCreateEscalas"] = "La escala no fue Creada";
                return View("Error");
            }
            if (model.idVuelo == null || model.idCiudadEscala == null)
            {
                (ViewData["ListadoCiudad"], ViewData["ListadoVuelos"]) = await GetVueloCiudad();
                TempData["ErrorCreate"] = "La escala no fue Creada";
                return View(model);
            }
            var client = _http.CreateClient("Base");
            var response = await client.PostAsJsonAsync("Escalas", model);
            if (!response.IsSuccessStatusCode)
            {
                return RedirectToAction("Error");
            }
            TempData["SuccessCreateEscalas"] = "Escala creada correctamente";
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Modify(int id)
        {
            var client = _http.CreateClient("Base");
            var response = await client.GetFromJsonAsync<Escalas>($"Escalas/{id}");
            if (response == null)
            {
                return RedirectToAction("Error");
            }
            (ViewData["ListadoCiudad"], ViewData["ListadoVuelos"]) = await GetVueloCiudad(response.idCiudadEscala, response.idVuelo);
            return View(response);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Modify(Escalas model)
        {
            if (!ModelState.IsValid)
            {
                TempData["ErrorModifyEscalas"] = "La Escala no fue modificada";
                return View("Error");
            }
            var client = _http.CreateClient("Base");
            var response = await client.PutAsJsonAsync($"Escalas/{model.idEscala}", model);
            if (!response.IsSuccessStatusCode)
            {
                return RedirectToAction("Error");
            }
            TempData["SuccessModifyEscalas"] = "Escala modificada correctamente";
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var client = _http.CreateClient("Base");
            var response = await client.DeleteAsync($"Escalas/{id}");
            if (!response.IsSuccessStatusCode)
            {
                return Json(new { success = false });
            }
            return Json(new { success = true });
        }


        private async Task<(List<SelectListItem>, List<SelectListItem>)> GetVueloCiudad(int? vuelo = null, int? ciudad = null)
        {
            var client = _http.CreateClient("Base");

            var vuelos = new List<SelectListItem>();
            var ciudades = new List<SelectListItem>();


            //Vuelo
            var responseVuelo = await client.GetFromJsonAsync<List<FiltrosVuelos>>($"Vuelos");
            if (responseVuelo != null)
            {
                if (vuelo == null)
                {

                    vuelos = responseVuelo.ToSelectListItems(
                    v => String.Concat("#", v.idVuelo.ToString(), "-", v.CIUDAD_ORIGEN, "/", v.CIUDAD_DESTINO),
                    v => v.idVuelo.ToString()
                    );

                }
                else
                {
                    vuelos = responseVuelo.ToSelectListItems(
                    v => String.Concat("#", v.idVuelo.ToString(), "-", v.CIUDAD_ORIGEN, "/", v.CIUDAD_DESTINO),
                    v => v.idVuelo.ToString(),
                    vuelo.ToString()
                    );
                }

            }

            //Ciudad
            var responseCiudad = await client.GetFromJsonAsync<List<Ciudades>>($"Ciudades");
            if (responseCiudad != null)
            {
                if (ciudad == null)
                {
                    ciudades = responseCiudad.ToSelectListItems(
                    ciu => ciu.Ciudad,
                    ciu => ciu.idCiudad.ToString()
                    );
                }
                else
                {
                    ciudades = responseCiudad.ToSelectListItems(
                    ciu => ciu.Ciudad,
                    ciu => ciu.idCiudad.ToString(),
                    ciudad.ToString()
                    );
                }
            }
                
            return (vuelos, ciudades);
        }
    }
}



