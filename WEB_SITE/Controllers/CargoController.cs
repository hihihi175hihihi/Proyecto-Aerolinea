using Microsoft.AspNetCore.Mvc;
using WEB_SITE.Models;

namespace WEB_SITE.Controllers
{
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
        public IActionResult Modify()
        {
            return View();
        }
    }
}
