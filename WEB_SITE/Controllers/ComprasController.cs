using Microsoft.AspNetCore.Mvc;
using WEB_SITE.Models.ViewModelSP;

namespace WEB_SITE.Controllers
{
    public class ComprasController : Controller
    {
        private readonly IHttpClientFactory _http;
        public ComprasController(IHttpClientFactory http)
        {
            _http = http;
        }
        public async Task<IActionResult> ReporteComprasAdmin()
        {
            return View();
        }

        public async Task<JsonResult> GetReporteComprasAdmin()
        {
            var client = _http.CreateClient("Base");
            var response = await client.GetFromJsonAsync<List<ReporteCompras>>("Compras");
            var modelView = response.Select(x => new
            {
                idCompra=x.idCompra.ToString(),
                user = x.Username,
                client = x.CLIENTE,
                email = x.Email,
                fechaCompra = x.FechaCompra.HasValue ? x.FechaCompra.Value.ToString("dd/MM/yyyy") : String.Empty,
                total = "Q." + x.Total.ToString()
            }).ToList();

            return Json(new { data = modelView });
        }
    }
}
