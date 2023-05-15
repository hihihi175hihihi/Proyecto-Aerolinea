using System.Reflection;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using WEB_SITE.Models;
using WEB_SITE.Services;

namespace WEB_SITE.Controllers
{
    [ValidateMenu(Rol = new[] { "Administrador" })]
    public class UsuariosController : Controller
    {
        //2)
        private readonly IHttpClientFactory _http;
        public UsuariosController(IHttpClientFactory clientFactory)
        {
            _http = clientFactory;
        }
        [HttpGet]
        // 1) Solamente la vista index
        public ActionResult Index()
        {
            return View();
        }

        // 3) Metodo que retorna el listado de usuarios y lo carga en la vista Index
        [HttpGet]
        public async Task<JsonResult> ListarUsuarios()
        {
            var client = _http.CreateClient("Base");
            var response =await  client.GetFromJsonAsync<IEnumerable<UsuariosVM>>("Usuarios");
            var modelView = response.Select(x => new
            {
                idUsuario = x.idUsuario.ToString(),
                user = x.Username ?? String.Empty,
                roles = x.nombreRol ?? String.Empty,
                password = x.Password ?? String.Empty,
                active = x.Active.ToString() ?? String.Empty
            }).ToList();

            return Json(new { data = modelView });
        }
        // GET: UsuariosController/Edit/5
        public async Task<IActionResult> Modificar(int id)
        {
            var client = _http.CreateClient("Base");
            var response = await client.GetFromJsonAsync<Usuarios>("Usuarios/"+id);
            if (response == null)
            {
                return RedirectToAction("Error");
            }
            var roles = await client.GetFromJsonAsync<List<Rols>>("Roles");
            if (roles == null)
            {
                return RedirectToAction("Error");
            }
            var ListadoRoles = roles.ConvertAll(d =>
            {
                if (d.idRol != response.idRol)
                {
                    return new SelectListItem()
                    {
                        Text = d.Rol.ToString(),
                        Value = d.idRol.ToString(),
                        Selected = false
                    };
                }
                else
                {
                    return new SelectListItem()
                    {
                        Text = d.Rol.ToString(),
                        Value = d.idRol.ToString(),
                        Selected = true
                    };

                }

            });
            ViewData["ListadoRoles"] = ListadoRoles;
            var estado = new List<SelectListItem> {
                new SelectListItem{Text="Enable",Value="true"},
                new SelectListItem{Text="Disable",Value="false"}
            };
            if (response.Active??false)
            {
                estado[0].Selected=true;
            }
            else
            {
                estado[1].Selected = false;
            }
            ViewData["estado"] = estado;
            return View(response);
        }


        // GET: UsuariosController/Create
        public async Task<IActionResult> Create()
        {
            var client = _http.CreateClient("Base");
            var roles = await client.GetFromJsonAsync<List<Rols>>("Roles");
            var ListadoRoles = roles.ConvertAll(d =>
            {
                    return new SelectListItem()
                    {
                        Text = d.Rol.ToString(),
                        Value = d.idRol.ToString(),
                        Selected = false
                    };
            });
            ViewData["ListadoRoles"] = ListadoRoles;
            return View();
        }

        // POST: UsuariosController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Usuarios model)
        {
            model.Active = false;
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var client = _http.CreateClient("Base");
            var response = await client.PostAsJsonAsync("Usuarios", model);
            if (!response.IsSuccessStatusCode)
            {
                return RedirectToAction("Error");
            }
            TempData["Success"] = "Usuario creado correctamente";
            return RedirectToAction("Index");
        }

       

        // POST: UsuariosController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Usuarios model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var client = _http.CreateClient("Base");
            var response = await client.PutAsJsonAsync($"Usuarios/{model.idUsuario}" , model);
            if (!response.IsSuccessStatusCode)
            {
                return RedirectToAction("Error");
            }
            TempData["Modify"] = "El usuario fue modificado";
            return RedirectToAction("Index");
        }

        // POST: UsuariosController/Delete/5
        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var client = _http.CreateClient("Base");
            var response = await client.DeleteAsync($"Usuarios/{id}");
            if (!response.IsSuccessStatusCode)
            {
                return Json(new { success = false });
            }
            return Json(new { success = true });
        }
    }
}
