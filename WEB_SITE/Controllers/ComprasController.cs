using System.Text;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.DotNet.Scaffolding.Shared.Project;
using WEB_SITE.Models;
using WEB_SITE.Models.ViewModelSP;
using WEB_SITE.Services;

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
        [HttpGet]
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
        public async Task<IActionResult> ReporteComprasCLI()
        {
            return View();
        }
        [HttpGet]
        public async Task<JsonResult> GetReporteComprasCLI()
        {
            var username = HttpContext.Session.GetString("User");
            var client = _http.CreateClient("Base");
            var response = await client.GetFromJsonAsync<List<ReporteCompras>>($"Compras/{username}");
            var modelView = response.Select(x => new
            {
                idCompra = x.idCompra.ToString(),
                user = x.Username,
                client = x.CLIENTE,
                email = x.Email,
                fechaCompra = x.FechaCompra.HasValue ? x.FechaCompra.Value.ToString("dd/MM/yyyy") : String.Empty,
                total = "Q." + x.Total.ToString()
            }).ToList();

            return Json(new { data = modelView });
        }
        public async Task<IActionResult> compraVuelo(int id)
        {
            var client = _http.CreateClient("Base");
            var response = await client.GetFromJsonAsync<FiltrosVuelos>("Vuelos/"+id);
            var modelView = new vueloById()
            {
                idVuelo=response.idVuelo,
                CIUDAD_ORIGEN=response.CIUDAD_ORIGEN,
                CIUDAD_DESTINO=response.CIUDAD_DESTINO,
                Precio=response.Precio
            };
            var idCliente = Convert.ToInt32(HttpContext.Session.GetString("idCliente"));
            //cambiar 1 por id cliente de la session
            var lTarjetas = await client.GetFromJsonAsync<List<TarjetasVM>>($"Tarjetas/GetTarjetasByCliente/{idCliente}");
            var tarjetas = lTarjetas.ConvertAll(t =>
            {
                return new SelectListItem()
                {
                    Text = String.Concat("xxxx-xxxx-xxxx-",t.Last4," / ",t.Brand),
                    Value = t.idTarjeta.ToString(),
                    Selected = false
                };
            });
            ViewData["Cards"] = tarjetas;
            return View(modelView);
        }

        [HttpPost]
        public async Task<IActionResult> ComprarConTarjetaGuardada(RequestCompraCardSave model)
        {
            if (model.idTarjeta == 0)
            {
                TempData["ErrorSeleccionTarjeta"] = "Error , no se selecciono una Tarjeta";
                return RedirectToAction("CompraVuelo");
            }
            var client = _http.CreateClient("Base");
            var compra = new Compras();
            compra.idCliente = Convert.ToInt32(HttpContext.Session.GetString("idCliente"));
            compra.Total = model.Total;
            var contentCompra = JsonSerializer.Serialize(compra);
            var contenidoCompra = new StringContent(contentCompra, Encoding.UTF8, "application/json");
            var respuestaCompra = await client.PostAsync("Compras", contenidoCompra);
            if (respuestaCompra.IsSuccessStatusCode)
            {
                var resultadoCompra = await respuestaCompra.Content.ReadFromJsonAsync<Compras>();
                var compraDetalle = new ComprasDetalle();
                compraDetalle.idVuelo = model.idVuelo;
                compraDetalle.Cantidad = model.Cantidad;
                compraDetalle.idCompra = resultadoCompra.idCompra;
                var contentCompraDetalle = JsonSerializer.Serialize(compraDetalle);
                var contenidoCompraDetalle = new StringContent(contentCompraDetalle, Encoding.UTF8, "application/json");
                var respuestaCompraDetalle = await client.PostAsync("ComprasDetalle", contenidoCompraDetalle);
                if (respuestaCompraDetalle.IsSuccessStatusCode)
                {
                    var pago = new PaymentRequest();
                    pago.saveCard = true;
                    pago.IdTarjeta = model.idTarjeta;
                    pago.IdCompra = resultadoCompra.idCompra;
                    pago.IdCliente = 1;
                    pago.MontoPago = model.Total??0;
                    var respuestaPago = await client.PostAsJsonAsync<PaymentRequest>("Payments/",pago);
                    if (respuestaPago.IsSuccessStatusCode)
                    {
                        return Json(new { success = true });
                    }
                    else
                    {
                        return Json(new { success = false });
                    }
                }
                else
                {
                    return Json(new { success = false });
                }
            }
            else
            {
                return Json(new { success = false });
            }

        }

        [HttpPost]
        public async Task<IActionResult> ComprarConTarjeta(PaymentRequestVM model)
        {
            var client = _http.CreateClient("Base");
            var compra = new Compras();
            compra.idCliente = Convert.ToInt32(HttpContext.Session.GetString("idCliente"));
            compra.Total = model.Total;
            var contentCompra = JsonSerializer.Serialize(compra);
            var contenidoCompra = new StringContent(contentCompra, Encoding.UTF8, "application/json");
            var respuestaCompra = await client.PostAsync("Compras", contenidoCompra);
            if (respuestaCompra.IsSuccessStatusCode)
            {
                var resultadoCompra = await respuestaCompra.Content.ReadFromJsonAsync<Compras>();
                var compraDetalle = new ComprasDetalle();
                compraDetalle.idVuelo = model.idVuelo;
                compraDetalle.Cantidad = model.Cantidad;
                compraDetalle.idCompra = resultadoCompra.idCompra;
                var contentCompraDetalle = JsonSerializer.Serialize(compraDetalle);
                var contenidoCompraDetalle = new StringContent(contentCompraDetalle, Encoding.UTF8, "application/json");
                var respuestaCompraDetalle = await client.PostAsync("ComprasDetalle", contenidoCompraDetalle);
                if (respuestaCompraDetalle.IsSuccessStatusCode)
                {
                    var pago = new PaymentRequest();
                    pago.saveCard = model.saveCard;
                    pago.IdCompra = resultadoCompra.idCompra;
                    pago.IdCliente = Convert.ToInt32(HttpContext.Session.GetString("idCliente"));
                    pago.MontoPago = model.Total ?? 0;
                    pago.NombreTarjeta = model.NombreTarjeta;
                    pago.TokenCard = model.TokenCard;
                    pago.ExpMonth = model.ExpMonth;
                    pago.ExpYear = model.ExpYear;
                    pago.Cvs = model.Cvs;
                    var respuestaPago = await client.PostAsJsonAsync<PaymentRequest>("Payments/", pago);
                    if (respuestaPago.IsSuccessStatusCode)
                    {
                        return Json(new { success = true });
                    }
                    else
                    {
                        return Json(new { success = false });
                    }
                }
                else
                {
                    return Json(new { success = false });
                }
            }
            else
            {
                return Json(new { success = false });
            }

        }
    }
}
