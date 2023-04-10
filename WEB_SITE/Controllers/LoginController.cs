using System.Text;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using WEB_SITE.Models;

namespace WEB_SITE.Controllers
{
    public class LoginController : Controller
    {
        private readonly IHttpClientFactory _http;
        public LoginController(IHttpClientFactory http)
        {
            _http = http;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
            [HttpPost]
            [ValidateAntiForgeryToken]
            public async Task<IActionResult> Login(LoginViewModel model)
            {

                if (!ModelState.IsValid)
                {
                    return View(model);
                }

                var client = _http.CreateClient("Base");
                var user =new 
                {
                    username = model.Email,
                    password = model.Password
                 };
                var content = JsonConvert.SerializeObject(user);
                var contenido = new StringContent(content, Encoding.UTF8, "application/json");
                var respuesta = await client.PostAsync("Authentication/login", contenido);
                if (respuesta.IsSuccessStatusCode)
                {
                    string responseContent = await respuesta.Content.ReadAsStringAsync();
                    dynamic resultado = JObject.Parse(responseContent);
                    string rol = resultado.rol.ToString();
                    if (rol.Equals("Usuario", StringComparison.OrdinalIgnoreCase))
                    {
                        string username = resultado.username.ToString();
                        string Usuario = resultado.idUsuario.ToString();
                        HttpContext.Session.SetString("User", username);
                        HttpContext.Session.SetString("idUsuario", Usuario);
                        return RedirectToAction("login2Auth","Login");
                    }
                    else
                    {
                        string username = resultado.username.ToString();
                        string Usuario = resultado.idUsuario.ToString();
                        HttpContext.Session.SetString("User", username);
                        HttpContext.Session.SetString("idUsuario", Usuario);
                        return RedirectToAction("Index","Home");
                    }
                }
            return View(model);
        }

        public IActionResult Registry()
        {
            return View();
        }

        public IActionResult forgotPassword()
        {
            return View();
        }
        [HttpGet]
        public IActionResult login2Auth()
        {
            return View();
        }
            [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> login2Auth(TokenVM model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var client = _http.CreateClient("Base");
            var token = new
            {
                idUsuario = Convert.ToInt32(HttpContext.Session.GetString("idUsuario")),
                token = model.Token
            };
            var content = JsonConvert.SerializeObject(token);
            var contenido = new StringContent(content, Encoding.UTF8, "application/json");
            var respuesta = await client.PostAsync("Authentication/login-2Auth", contenido);
            if (respuesta.IsSuccessStatusCode)
            {
                string responseContent = await respuesta.Content.ReadAsStringAsync();
                dynamic resultado = JObject.Parse(responseContent);

                string idCliente = resultado.idCliente.ToString();
                    HttpContext.Session.SetString("idCliente", idCliente);
                    return RedirectToAction("Index","Home");
            }

            return View(model);
        }
        public IActionResult registryUserActivate()
        {
            return View();
        }
    }
}
