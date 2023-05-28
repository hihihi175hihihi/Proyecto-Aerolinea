using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using WEB_SITE.Models;
using WEB_SITE.Services;

namespace WEB_SITE.Controllers
{
    public class AccessRolesController : Controller
    {
        private readonly IHttpClientFactory _http;
        public AccessRolesController(IHttpClientFactory http)
        {
            _http = http;
        }
        public async Task<IActionResult> Index()
        {
            var client = _http.CreateClient("Base");
            var response = await client.GetFromJsonAsync<List<AccessRolesVM>>("AccessRoles");
            if (response == null)
            {
                return View(new List<AccessRolesVM>());
            }
            return View(response);
        }
        public async Task<IActionResult> Create()
        {

            (ViewData["ListadoRoles"],ViewData["ListadoAccesos"])= await GetAccesosRoles();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(AccessRoles model)
        {
            if (!ModelState.IsValid)
            {
                TempData["ErrorCreateAccesosRoles"] = "El Permiso no fue Creado";
                return View("Error");
            }
            if (model.idAccess==null || model.idRol==null)
            {
                (ViewData["ListadoRoles"], ViewData["ListadoAccesos"]) = await GetAccesosRoles();
                TempData["ErrorCreateAccesosRoles"] = "El Permiso no fue Creado";
                return View(model);
            }
            var client = _http.CreateClient("Base");
            var response = await client.PostAsJsonAsync("AccessRoles", model);
            if (!response.IsSuccessStatusCode)
            {
                return RedirectToAction("Error");
            }
            TempData["SuccessCreateAccessRoles"] = "Permiso creado correctamente";
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Modify(int id)
        {
            var client = _http.CreateClient("Base");
            var response = await client.GetFromJsonAsync<AccessRoles>($"AccessRoles/{id}");
            if (response == null)
            {
                return RedirectToAction("Error");
            }
            (ViewData["ListadoRoles"], ViewData["ListadoAccesos"]) = await GetAccesosRoles(response.idRol,response.idAccess);
            return View(response);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Modify(AccessRolesVM model)
        {
            if (!ModelState.IsValid)
            {
                TempData["ErrorModifyAccesosRoles"] = "El Permiso no fue Modificado";
                return View("Error");
            }
            var client = _http.CreateClient("Base");
            var response = await client.PutAsJsonAsync($"AccessRoles/{model.idAccessRoles}", model);
            if (!response.IsSuccessStatusCode)
            {
                return RedirectToAction("Error");
            }
            TempData["SuccessModifyAccessRole"] = "Permiso Modificado correctamente";
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var client = _http.CreateClient("Base");
            var response = await client.DeleteAsync($"AccessRoles/{id}");
            if (!response.IsSuccessStatusCode)
            {
                return Json(new { success = false });
            }
            return Json(new { success = true });
        }

        private async Task<(List<SelectListItem>,List<SelectListItem>)> GetAccesosRoles(int? rol=null,int? acceso = null)
        {
            var client = _http.CreateClient("Base");
            
            var  roles=new List<SelectListItem>();
            var  access=new List<SelectListItem>();
            //Roles
            var responseRoles = await client.GetFromJsonAsync<List<Rols>>($"Roles");
            if (responseRoles != null)
            {

                if (rol == null)
                {
                    roles = responseRoles.ToSelectListItems(
                       r => r.Rol,
                       r => r.idRol.ToString()
                       );
                }
                else
                {
                     roles = responseRoles.ToSelectListItems(
                       r => r.Rol,
                       r => r.idRol.ToString(),
                       rol.ToString()
                       );
                }
            }
            //Access
            var responseAccess = await client.GetFromJsonAsync<List<Access>>($"Accesses");
            if (responseAccess != null)
            {
                if (acceso == null)
                {
                    access = responseAccess.ToSelectListItems(
                   a => String.Concat(a.Name, "- ", a.URL),
                   a => a.idAccess.ToString()
                   );
                }
                else
                {
                    access = responseAccess.ToSelectListItems(
                   a => String.Concat(a.Name, "- ", a.URL),
                   a => a.idAccess.ToString(),
                   acceso.ToString()
                   );
                }
            }
            return (roles, access);
        }
    }
}
