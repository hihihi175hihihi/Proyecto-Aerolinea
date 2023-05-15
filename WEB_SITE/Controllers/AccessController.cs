using Microsoft.AspNetCore.Mvc;
using WEB_SITE.Models;
using WEB_SITE.Services;

namespace WEB_SITE.Controllers
{
    //[ValidateMenu(Rol =new[] { "Administrador" })]
    public class AccessController : Controller
    {
        private readonly IHttpClientFactory _http;
        public AccessController(IHttpClientFactory http)
        {
            _http = http;
        }
        public async Task<IActionResult> Index()
        {
            var client = _http.CreateClient("Base");
            var response = await client.GetFromJsonAsync<List<Access>>("Accesses");
            if (response == null)
            {
                return View(new List<Access>());
            }
            return View(response);
        }
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Access model)
        {
            if (!ModelState.IsValid)
            {
                return View("Error");
            }
            var client = _http.CreateClient("Base");
            var response = await client.PostAsJsonAsync("Accesses", model);
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
            var response = await client.GetFromJsonAsync<Access>($"Accesses/{id}");
            if (response == null)
            {
                return RedirectToAction("Error");
            }
            return View(response);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Modify(Access model)
        {
            if (!ModelState.IsValid)
            {
                return View("Error");
            }
            var client = _http.CreateClient("Base");
            var response = await client.PutAsJsonAsync($"Accesses/{model.idAccess}", model);
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
            var response = await client.DeleteAsync($"Accesses/{id}");
            if (!response.IsSuccessStatusCode)
            {
                return Json(new { success = false });
            }
            return Json(new { success = true });
        }
    }
}
