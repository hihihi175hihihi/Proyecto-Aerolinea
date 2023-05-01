using Microsoft.AspNetCore.Mvc;
using WEB_SITE.Models;

namespace WEB_SITE.Controllers
{
    public class RolesController : Controller
    {
        private readonly IHttpClientFactory _http;
        public RolesController(IHttpClientFactory http)
        {
            _http = http;
        }
        public async Task<IActionResult> Index()
        {
            var client = _http.CreateClient("Base");
            var response = await client.GetFromJsonAsync<List<Rols>>("Roles");
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
        public async Task<IActionResult> Create(Rols model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var client = _http.CreateClient("Base");
            var response = await client.PostAsJsonAsync("Roles", model);
            if (!response.IsSuccessStatusCode)
            {
                return Redirect("Error");
            }
            TempData["CreateRol"] = "Rol creado correctamente";
            return RedirectToAction("Index"); 
        }
        public IActionResult Modify()
            {
                return View();
            }
        }
    }
    
