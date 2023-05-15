using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using WEB_SITE.Models;
using WEB_SITE.Models.ViewModelSP;
using WEB_SITE.Services;

namespace WEB_SITE.Controllers
{
    [ValidateMenu(Rol = new[] { "Usuario" })]
    public class WishListController : Controller
    {
        private readonly IHttpClientFactory _http;
        public WishListController(IHttpClientFactory http)
        {
            _http = http;
        }
        [HttpPost]
        public async Task<IActionResult> Add(int id)
        {
            var client = _http.CreateClient("Base");
            var wishList=new WishList(){
                idUsuario = Convert.ToInt32(HttpContext.Session.GetString("idUsuario")),
                idVuelo = id,
                FechaSave = DateTime.Now
            };
            var response = await client.PostAsJsonAsync("WishLists", wishList);
            if (!response.IsSuccessStatusCode)
            {
                return Json(new { success = false });
            }
            return Json(new { success = true });
        }
        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var client = _http.CreateClient("Base");
            var wishList = new WishList()
            {
                idUsuario = Convert.ToInt32(HttpContext.Session.GetString("idUsuario")),
                idVuelo = id,
                FechaSave = DateTime.Now
            };
            wishList.idUsuario = 1;
            var content = JsonConvert.SerializeObject(wishList);
            var contenido = new StringContent(content, Encoding.UTF8, "application/json");
            var response = await client.PostAsync($"WishLists/GetData", contenido);
            if (!response.IsSuccessStatusCode)
            {
                return Json(new { success = false });
            }
            var result = await response.Content.ReadFromJsonAsync<WishList>();
            var respuesta = await client.DeleteAsync($"WishLists/{result.idWishList}");
            if (!respuesta.IsSuccessStatusCode)
            {
                return Json(new { success = false });
            }
            return Json(new { success = true });
        }
        public async Task<IActionResult> Index()
        {
            var client = _http.CreateClient("Base");
            var oUser = Convert.ToInt32(HttpContext.Session.GetString("idUsuario"));
            var respuesta = await client.GetAsync($"WishLists/{oUser}");
            var response = await respuesta.Content.ReadFromJsonAsync<List<WishListxUsuario>>();
            if (response != null)
            {
                foreach (var item in response)
                {
                    item.DiaSemana = GetDayNameFromNumber(item.DiaSemana);
                }

                ViewData["Ciudades"] = await GetCiudades();
                ViewData["Paises"] = await GetPaises();
                return View(response);
            }
            return View(new List<FiltrosVuelos>());
        }
        private async Task<List<SelectListItem>> GetCiudades()
        {
            var client = _http.CreateClient("Base");
            var result = await client.GetFromJsonAsync<List<Ciudades>>("Ciudades");
            var ciudades = result.ConvertAll(t =>
            {
                return new SelectListItem()
                {
                    Text = t.Ciudad,
                    Value = t.idCiudad.ToString(),
                    Selected = false
                };
            });
            return ciudades;
        }
        private async Task<List<SelectListItem>> GetPaises()
        {
            var client = _http.CreateClient("Base");
            var result = await client.GetFromJsonAsync<List<Paises>>("Paises");
            var paises = result.ConvertAll(t =>
            {
                return new SelectListItem()
                {
                    Text = t.Pais,
                    Value = t.idPais.ToString(),
                    Selected = false
                };
            });
            return paises;
        }
        public static string GetDayNameFromNumber(string dayNumber)
        {
            return dayNumber switch
            {
                "1" => "Lunes",
                "2" => "Martes",
                "3" => "Miércoles",
                "4" => "Jueves",
                "5" => "Viernes",
                "6" => "Sábado",
                "7" => "Domingo"
            };
        }
    }
}
