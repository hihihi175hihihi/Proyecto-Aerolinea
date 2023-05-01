using Microsoft.AspNetCore.Mvc;
using WEB_SITE.Models;

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
                return View(model);
            }
            var client = _http.CreateClient("Base");
            var response = await client.PostAsJsonAsync("Paises", model);
            if (!response.IsSuccessStatusCode)
            {
                return Redirect("Error");
            }
            TempData["CreatePais"] = "Pais creado correctamente";
            return RedirectToAction("Index");
        }
        public IActionResult Modify()
        {
            return View();
        }
    }
}
