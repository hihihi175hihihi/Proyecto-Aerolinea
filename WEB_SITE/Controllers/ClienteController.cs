using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using WEB_SITE.Models;
using WEB_SITE.Services;

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
            var response = await client.GetFromJsonAsync<List<ClientesVM>>("Clientes");
            if (response == null)
            {
                return View(new List<ClientesVM>());
            }
            return View(response);
        }
        public async Task<IActionResult> Modify(int id)
        {
            var client = _http.CreateClient("Base");
            var response = await client.GetFromJsonAsync<Clientes>($"Clientes/{id}");
            if (response == null)
            {
                return RedirectToAction("Error");
            }
            ViewData["ListadoUssers"] = await GetUsuarios(response.idUsuario);
            return View(response);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Modify(Clientes model)
        {
            if (!ModelState.IsValid)
            {
                TempData["ErrorModifyCliente"] = "El cliente no fue modificado";
                return View("Error");
            }
            var client = _http.CreateClient("Base");
            var response = await client.PutAsJsonAsync($"Clientes/{model.idCliente}", model);
            if (!response.IsSuccessStatusCode)
            {
                return RedirectToAction("Error");
            }
            TempData["SuccessModifyCliente"] = "El cliente fue modificado correctamente";
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var client = _http.CreateClient("Base");
            var response = await client.DeleteAsync($"Clientes/{id}");
            if (!response.IsSuccessStatusCode)
            {
                return Json(new { success = false });
            }
            return Json(new { success = true });
        }

        private async Task<List<SelectListItem>> GetUsuarios(int? usuario = null)
        {
            var client = _http.CreateClient("Base");
            var Usuario = new List<SelectListItem>();
            
            //Usuario
            var responseUssers = await client.GetFromJsonAsync<List<Usuarios>>($"Usuarios");
            if (responseUssers != null)
            {
                if (usuario == null)
                {
                    Usuario = responseUssers.ToSelectListItems(
                    uc => uc.Username,
                    uc => uc.idUsuario.ToString()
                    );
                }
                else
                { 
                    Usuario = responseUssers.ToSelectListItems(
                    uc => uc.Username,
                    uc => uc.idUsuario.ToString(),
                    usuario.ToString()
                    );
                }
            }
            return (Usuario);
        }
    }
}
