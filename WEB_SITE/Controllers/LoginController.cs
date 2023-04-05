using Microsoft.AspNetCore.Mvc;
using WEB_SITE.Models;

namespace WEB_SITE.Controllers
{
    public class LoginController : Controller
    {
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Login(LoginViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            return View();

        }

        public IActionResult Registry()
        {
            return View();
        }

        public IActionResult forgotPassword()
        {
            return View();
        }
        public IActionResult login2Auth()
        {
            return View();
        }
        public IActionResult registryUserActivate()
        {
            return View();
        }
    }
}
