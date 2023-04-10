using Microsoft.AspNetCore.Mvc;
using WEB_SITE.Models.ViewModelSP;

namespace WEB_SITE.Controllers
{
    public class DetallesCompraController : Controller
    {
        private readonly IHttpClientFactory _http;
        public DetallesCompraController(IHttpClientFactory http)
        {
            _http = http;
        }

        /// <summary>
        /// Metodo que da el detalle de compra para administrador
        /// </summary>
        [HttpGet("{id}")]
        public async Task<JsonResult> DetallesCompraAdmin(int id)
        {
            var client = _http.CreateClient("Base");
            var response = await client.GetFromJsonAsync<List<DetalleCompra>>("ComprasDetalles/" + id);
            return Json(response);
        }
    }
}
