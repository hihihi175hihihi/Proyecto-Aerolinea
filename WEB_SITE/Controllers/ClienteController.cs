using Microsoft.AspNetCore.Mvc;
using WEB_SITE.Models;

namespace WEB_SITE.Controllers
{
    public class ClienteController : Controller
    {
        private readonly IHttpClientFactory _http;
        public ClienteController(IHttpClientFactory http)
        {
            _http = http;
        }
        public async Task<IActionResult> Index()
        {
            var client = _http.CreateClient("Base");
            var response = await client.GetFromJsonAsync<List<Clientes>>("Clientes");
            if (response == null)
            {
                return View(new List<Clientes>());
            }
            return View(response);
        }
        public IActionResult Modify()
        {
            return View();
        }
    }
}
