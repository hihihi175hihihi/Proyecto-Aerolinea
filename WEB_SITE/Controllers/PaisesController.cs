using Microsoft.AspNetCore.Mvc;
using WEB_SITE.Models;
using WEB_SITE.Services;

namespace WEB_SITE.Controllers
{
    
    public class PaisesController : Controller
    {
        private readonly IHttpClientFactory _http;
        public PaisesController(IHttpClientFactory http)
        {
            _http = http;
        }
        public async Task<IActionResult> Index()
        {
            var client = _http.CreateClient("Base");
            var response = await client.GetFromJsonAsync<List<Paises>>("Paises");
            if (response == null)
            {
                return View(new List<Rols>());
            }
            return View(response);
        }

        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Paises model)
        {
            if (!ModelState.IsValid)
            {
                TempData["ErrorCreatePais"] = "Error al crear el Pais";
                return View(model);
            }
            var client = _http.CreateClient("Base");
            var response = await client.PostAsJsonAsync("Paises", model);
            if (!response.IsSuccessStatusCode)
            {
                return Redirect("Error");
            }
            TempData["SuccessCreatePais"] = "Pais creado correctamente";
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> Modify(int id)
        {
            var client = _http.CreateClient("Base");
            var response = await client.GetFromJsonAsync<Paises>($"Paises/{id}");
            if (response == null)
            {
                return RedirectToAction("Error");
            }
            return View(response);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Modify(Paises model)
        {
            if (!ModelState.IsValid)
            {
                TempData["ErrorModifyPais"] = "Error al modificar el Pais";
                return View("Error");
            }
            var client = _http.CreateClient("Base");
            var response = await client.PutAsJsonAsync($"Paises/{model.idPais}", model);
            if (!response.IsSuccessStatusCode)
            {
                return RedirectToAction("Error");
            }
            TempData["SuccessModifyPais"] = "Pais Modificado correctamente";
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var client = _http.CreateClient("Base");
            var response = await client.DeleteAsync($"Paises/{id}");
            if (!response.IsSuccessStatusCode)
            {
                return Json(new { success = false });
            }
            return Json(new { success = true });
        }

    }
}
