using Microsoft.AspNetCore.Mvc;

namespace WEB_SITE.Controllers
{
    public class CiudadesController : Controller
    {
        public IActionResult Index()
        {
            return View();
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
