using Microsoft.AspNetCore.Mvc;
using WEB_SITE.Models;
using WEB_SITE.Services;

namespace WEB_SITE.Controllers
{
    [ValidateMenu(Rol = new[] { "Administrador" })]
    public class CargoController : Controller
    {
        private readonly IHttpClientFactory _http;
        public CargoController(IHttpClientFactory http)
        {
            _http = http;
        }
        public async Task<IActionResult> Index()
        {
            var client = _http.CreateClient("Base");
            var response = await client.GetFromJsonAsync<List<Cargos>>("Cargo");
            
            if (response == null)
            {
                return View(new List<Cargos>());
            }
            else
            {
                return View(response);
            }
            
         
        }
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Cargos model)
        {
            if (!ModelState.IsValid)
            {
                return View("Error");
            }
            var client = _http.CreateClient("Base");
            var response = await client.PostAsJsonAsync("Cargo", model);
            if (!response.IsSuccessStatusCode)
            {
                return RedirectToAction("Error");
            }
            //TempData["Success"] = "Usuario creado correctamente";
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Modify(int id)
        {
            var client = _http.CreateClient("Base");
            var response = await client.GetFromJsonAsync<Cargos>($"Cargo/{id}");
            if (response == null)
            {
                return RedirectToAction("Error");
            }
            return View(response);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Modify(Cargos model)
        {
            if (!ModelState.IsValid)
            {
                return View("Error");
            }
            var client = _http.CreateClient("Base");
            var response = await client.PutAsJsonAsync($"Cargo/{model.idCargo}", model);
            if (!response.IsSuccessStatusCode)
            {
                return RedirectToAction("Error");
            }
            //TempData["Success"] = "Usuario creado correctamente";
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var client = _http.CreateClient("Base");
            var response = await client.DeleteAsync($"Cargo/{id}");
            if (!response.IsSuccessStatusCode)
            {
                return Json(new { success = false });
            }
            return Json(new { success = true });
        }

    }
}
