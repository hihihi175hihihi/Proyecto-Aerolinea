using System.Text;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using WEB_SITE.Models;

namespace WEB_SITE.Controllers
{
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
                idUsuario = 1,
                idVuelo = id,
                FechaSave = DateTime.Now
            };
            var response = await client.PostAsJsonAsync<WishList>("WishLists", wishList);
            if (!response.IsSuccessStatusCode)
            {
                return Json(new { data = false });
            }
            return Json(new { data = true });
        }
        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var client = _http.CreateClient("Base");
            var wishList = new WishList()
            {
                idUsuario = 1,
                idVuelo = id,
                FechaSave = DateTime.Now
            };
            wishList.idUsuario = 1;
            var content = JsonConvert.SerializeObject(wishList);
            var contenido = new StringContent(content, Encoding.UTF8, "application/json");
            var response = await client.PostAsync("WishLists/Delete", contenido);
            if (!response.IsSuccessStatusCode)
            {
                return Json(new { data = false });
            }
            return Json(new { data = true });
        }
    }
}
