using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using WEB_SITE.Models;
using WEB_SITE.Services;

namespace WEB_SITE.Controllers
{
    [ValidateMenu(Rol = new[] { "Administrador" })]
    public class EmpleadosController : Controller
    {
        private readonly IHttpClientFactory _http;
        public EmpleadosController(IHttpClientFactory http)
        {
            _http = http;
        }
        public async Task<IActionResult> Index()
        {
            var client = _http.CreateClient("Base");
            var response = await client.GetFromJsonAsync<List<EmpleadosVM>>("Empleados");
            if (response == null)
            {
                return View(new List<EmpleadosVM>());
            }
            return View(response);
        }
        public async Task<IActionResult> Create()
        {
            (ViewData["ListadoUsuarios"], ViewData["ListadoCargos"]) = await GetUsuarioCargo();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(EmpleadosVM model)
        {
            if (!ModelState.IsValid)
            {
                return View("Error");
            }
            if (model.idUsuario == null || model.idCargo == null)
            {
                (ViewData["ListadoUsuarios"], ViewData["ListadoCargos"]) = await GetUsuarioCargo();
                TempData["ErrorCreate"] = "El Usuario/Cargo no fue Creado";
                return View(model);
            }
            var client = _http.CreateClient("Base");
            var response = await client.PostAsJsonAsync("empleados", model);
            if (!response.IsSuccessStatusCode)
            {
                return RedirectToAction("Error");
            }
            //TempData["Success"] = "Usuario creado correctamente";
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Modify(int id)
        {
            var client = _http.CreateClient("Base");
            var response = await client.GetFromJsonAsync<Empleados>($"empleados/{id}");
            if (response == null)
            {
                return RedirectToAction("Error");
            }
            (ViewData["ListadoUsuarios"], ViewData["ListadoCargos"]) = await GetUsuarioCargo(response.idUsuario, response.idCargo);
            return View(response);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Modify(Empleados model)
        {
            if (!ModelState.IsValid)
            { 
                return View("Error");
            }
            var client = _http.CreateClient("Base");
            var response = await client.PutAsJsonAsync($"empleados/{model.idEmpleado}", model);
            if (!response.IsSuccessStatusCode)
            {
                return RedirectToAction("Error");
            }
            //TempData["Success"] = "Usuario creado correctamente";
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var client = _http.CreateClient("Base");
            var response = await client.DeleteAsync($"Empleados/{id}");
            if (!response.IsSuccessStatusCode)
            {
                return Json(new { success = false });
            }
            return Json(new { success = true });
        }

        private async Task<(List<SelectListItem>, List<SelectListItem>)> GetUsuarioCargo(int? usuario = null, int? cargo = null)
        {
            var client = _http.CreateClient("Base");

            var usuarios = new List<SelectListItem>();
            var cargos = new List<SelectListItem>();

            //Usuario
            var responseUsuarios = await client.GetFromJsonAsync<List<Usuarios>>($"Usuarios");
            if (responseUsuarios != null)
            {
                if (usuario == null)
                {
                usuarios = responseUsuarios.ToSelectListItems(
                u => u.Username,
                u => u.idUsuario.ToString()
                );
                }
                else
                {

                    usuarios = responseUsuarios.ToSelectListItems(
                    u => u.Username,
                    u => u.idUsuario.ToString(),
                    usuario.ToString()
                    );

                }
            }

            //Cargos
            var responseCargos = await client.GetFromJsonAsync<List<Cargos>>($"Cargo");
            if (responseCargos != null)
            {
                if (cargo == null)
                {

                    cargos = responseCargos.ToSelectListItems(
                    c => c.Cargo,
                    c => c.idCargo.ToString()
                    );

                }
                else
                {

                    cargos = responseCargos.ToSelectListItems(
                    c => c.Cargo,
                    c => c.idCargo.ToString(),
                    cargo.ToString()
                    );

                }
            }
                




            return (usuarios, cargos);
        }



    }
}