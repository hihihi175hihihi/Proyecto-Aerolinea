using Microsoft.AspNetCore.Mvc;
using WEB_SITE.Models;


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
        public IActionResult Create()
        {
            return View();
        }
        public IActionResult Modify()
        {
            return View();
        }
    }
}
