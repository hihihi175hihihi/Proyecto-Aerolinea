using Microsoft.AspNetCore.Mvc;
using WEB_SITE.Models;
using WEB_SITE.Models.ViewModelSP;
using WEB_SITE.Services;

namespace WEB_SITE.Controllers
{
    [ValidateMenu(Rol = new[] { "Administrador", "Empleado", "Usuario" })]
    public class DetallesCompraController : Controller
    {
        private readonly IHttpClientFactory _http;
        public DetallesCompraController(IHttpClientFactory http)
        {
            _http = http;
        }

        
        public async Task<JsonResult> DetallesCompraAdmin(int id)
        {
            var client = _http.CreateClient("Base");
            var response = await client.GetFromJsonAsync<List<DetalleCompra>>("ComprasDetalle/" + id);
            return Json(response);
        }
    }
}
