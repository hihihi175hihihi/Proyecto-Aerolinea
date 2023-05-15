using System.Reflection;
using System.Text;
using AerolineLibrary;
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
            var user = new
            {
                username = model.Email,
                password = Encrypt.GetHash(model.Password)
                };
            var content = JsonConvert.SerializeObject(user);
            var contenido = new StringContent(content, Encoding.UTF8, "application/json");
            var respuesta = await client.PostAsync("Authentication/login", contenido);
            if (respuesta.IsSuccessStatusCode)
            {
                string responseContent = await respuesta.Content.ReadAsStringAsync();
                dynamic resultado = JObject.Parse(responseContent);
                string rol = resultado.rol.ToString();
                int idRol = Convert.ToInt32(resultado.idRol.ToString());
                HttpContext.Session.SetString("Rol", rol);
                var menuList = await GetMenu(idRol, client);
                var menuListJson = JsonConvert.SerializeObject(menuList);
                HttpContext.Session.SetString("MenuList", menuListJson);
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

        private async Task<List<MenuVM>> GetMenu(int idRol, HttpClient client)
        {
            var respuesta = await client.GetFromJsonAsync<List<MenuVM>>($"AccessRoles/GetAccessRolesByRol/{idRol}");
            if (respuesta == null)
            {
                return null;
            }
            return respuesta;
        }

        public IActionResult Registry()
        {
            
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Registry(RegistryRequest model)
        {
            model.Password = Encrypt.GetHash(model.Password);
            var client = _http.CreateClient("Base");
            var content = JsonConvert.SerializeObject(model);
            var contenido = new StringContent(content, Encoding.UTF8, "application/json");
            var respuesta = await client.PostAsync("Authentication/registryUser", contenido);
            if (respuesta.IsSuccessStatusCode)
            {
                string responseContent = await respuesta.Content.ReadAsStringAsync();
                dynamic resultado = JObject.Parse(responseContent);
                string idUsuario, idCliente,Username;
                (idUsuario, idCliente, Username) = (resultado.idUsuario.ToString(), resultado.idCliente.ToString(), resultado.username.ToString());
                HttpContext.Session.SetString("idUsuario", idUsuario);
                HttpContext.Session.SetString("idCliente", idCliente);
                HttpContext.Session.SetString("User", Username);
                return RedirectToAction("registryUserActivate", "Login");
            }
                return Redirect("Error");
        }
        public IActionResult forgotPassword()
        {
            return View();
        }
        public IActionResult forgotPasswordToken()
        {
            return View();
        }
        public IActionResult ChangePassword()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> forgotPassword(RegistryRequest model)
        {
            var client = _http.CreateClient("Base");
            var content = JsonConvert.SerializeObject(model);
            var contenido = new StringContent(content, Encoding.UTF8, "application/json");
            var respuesta = await client.PostAsync("Authentication/RequestResetPassword", contenido);
            if (respuesta.IsSuccessStatusCode)
            {
                string responseContent = await respuesta.Content.ReadAsStringAsync();
                dynamic resultado = JObject.Parse(responseContent);
                string idUsuario, idCliente, Username;
                (idUsuario, idCliente, Username) = (resultado.idUsuario.ToString(), resultado.idCliente.ToString(), resultado.username.ToString());
                HttpContext.Session.SetString("idUsuario", idUsuario);
                return RedirectToAction("forgotPasswordToken", "Login");
            }
            return Redirect("Error");
            
        }
        [HttpPost]
        public async Task<IActionResult> ValidateTokenResetPassword(TokenVM model)
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
            var respuesta = await client.PostAsync("Authentication/ValidateTokenResetPassword", contenido);
            if (respuesta.IsSuccessStatusCode)
            {
                return RedirectToAction("ChangePassword", "Login");
            }
            TempData["ErrorToken"] = "El Token es Incorrecto";
            return View(model);
          
        }
        [HttpPost]
        public async Task<IActionResult> ChangePassword(ChangePasswordVM model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            if (!model.Password.Equals(model.ConfirmationPassword))
            {
                TempData["Error"] = "Las Password No Coinciden";
                return View(model);
            }
            var client = _http.CreateClient("Base");
            var cPassword = new ChangePassword()
            {
                idUsuario = Convert.ToInt32(HttpContext.Session.GetString("idUsuario")),
                Password = Encrypt.GetHash(model.Password)
            };
            var content = JsonConvert.SerializeObject(cPassword);
            var contenido = new StringContent(content, Encoding.UTF8, "application/json");
            var respuesta = await client.PostAsync("Authentication/ResetPassword", contenido);
            if (respuesta.IsSuccessStatusCode)
            {
                HttpContext.Session.Clear();
                return RedirectToAction("Login", "Login");
            }
            return View(model);
            
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
                    return RedirectToAction("Index", "Vuelos");
            }

            return View(model);
        }
        public IActionResult registryUserActivate()
        {
            return View();
        }

        [HttpPost]
        public IActionResult registryUserActivate(TokenVM model)
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
            var respuesta = client.PostAsync("Authentication/registryUserActivate", contenido);
            if (respuesta.Result.IsSuccessStatusCode)
            {
                return RedirectToAction("Login", "Login");
            }
            return Redirect("Error");
        }
        
        public IActionResult Logout(){
            HttpContext.Session.Clear();
            return RedirectToAction("Login", "Login");
        }
    }
}
