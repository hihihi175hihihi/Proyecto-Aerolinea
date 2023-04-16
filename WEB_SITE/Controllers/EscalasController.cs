using Microsoft.AspNetCore.Mvc;
using WEB_SITE.Models;

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
