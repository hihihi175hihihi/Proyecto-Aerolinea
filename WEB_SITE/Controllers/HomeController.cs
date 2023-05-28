using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using WEB_SITE.Models;
using WEB_SITE.Models.ViewModelSP;
using WEB_SITE.Services;

namespace WEB_SITE.Controllers
{
   
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IHttpClientFactory _http;

        public HomeController(ILogger<HomeController> logger, IHttpClientFactory http)
        {
            _logger = logger;
            _http = http;
        }

        public async Task<IActionResult> Index()
        {
            var client = _http.CreateClient("Base");
            var response = await client.GetFromJsonAsync<List<ComprasPorMesAnioViewModel>>("Compras/GetComprasMesActual");
            var TotalComprasDia = await client.GetFromJsonAsync<TotalVentasxDia>("DashBoard/TotalVentasxDia");
            ViewData["TotalComprasDia"] = TotalComprasDia.Total??0;
            var TotalVentasAnual = await client.GetFromJsonAsync<TotalVentasAnual>("DashBoard/TotalVentasAnual");
            ViewData["TotalVentasAnual"] = TotalVentasAnual.TotalAnual??0;
            return View(response);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}