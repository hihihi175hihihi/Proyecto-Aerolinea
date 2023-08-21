using System.Data;
using Microsoft.AspNetCore.Mvc;
using WEB_SITE.Models;
using WEB_SITE.Services;

namespace WEB_SITE.Controllers
{
    public class CiudadesController : Controller
    {
        private readonly IHttpClientFactory _http;
        public CiudadesController(IHttpClientFactory http)
        {
            _http = http;
        }
        public async Task<IActionResult> Index()
        {
            var client = _http.CreateClient("Base");
            var response = await client.GetFromJsonAsync<List<CiudadesVM>>("Ciudades");
            if (response == null)
            {
                return View(new List<CiudadesVM>());
            }
            return View(response);
        }


        public async Task<IActionResult> Create()
        {
            var client = _http.CreateClient("Base");
            var response = await client.GetFromJsonAsync<List<Paises>>("Paises");
            ViewData["Paises"] = response.ToSelectListItems(
                       r => r.Pais,
                       r => r.idPais.ToString()
                       );
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Ciudades model)
        {
            if (!ModelState.IsValid)
            {
                TempData["ErrorCreateCiudad"] = "La ciudad no fue creada";
                return View("Error");
            }
            var client = _http.CreateClient("Base");
            var response = await client.PostAsJsonAsync("Ciudades", model);
            if (!response.IsSuccessStatusCode)
            {
                return RedirectToAction("Error");
            }
            TempData["SuccessCreateCiudad"] = "Ciudad creada correctamente";
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Modify(int id)
        {
            var client = _http.CreateClient("Base");
            var response = await client.GetFromJsonAsync<Ciudades>($"Ciudades/{id}");
            if (response == null)
            {
                return RedirectToAction("Error");
            }
            var responses = await client.GetFromJsonAsync<List<Paises>>("Paises");
            ViewData["Paises"] = responses.ToSelectListItems(
                       r => r.Pais,
                       r => r.idPais.ToString()
                       );
            return View(response);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Modify(Ciudades model)
        {
            if (!ModelState.IsValid)
            {
                TempData["ErrorModifyCiudad"] = "La ciudad no fue modificada";
                return View("Error");
            }
            var client = _http.CreateClient("Base");
            var response = await client.PutAsJsonAsync($"Ciudades/{model.idCiudad}", model);
            if (!response.IsSuccessStatusCode)
            {
                return RedirectToAction("Error");
            }
            TempData["SuccessModifyCiudad"] = "Ciudad modificada correctamente";
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var client = _http.CreateClient("Base");
            var response = await client.DeleteAsync($"Ciudades/{id}");
            if (!response.IsSuccessStatusCode)
            {
                return Json(new { success = false });
            }
            return Json(new { success = true });
        }
    }
}
