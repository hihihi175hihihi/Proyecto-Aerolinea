using Microsoft.AspNetCore.Mvc;
using WEB_SITE.Models;
using WEB_SITE.Services;

namespace WEB_SITE.Controllers
{
    [ValidateMenu(Rol = new[] { "Administrador" })]
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


        public IActionResult Create()
        {
            return View();
        }
        public IActionResult Modify()
        {
            return View();
        }
    }
}
