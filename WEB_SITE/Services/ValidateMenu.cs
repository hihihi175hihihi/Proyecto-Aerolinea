using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace WEB_SITE.Services
{
    public class ValidateMenu : ActionFilterAttribute
    {
        public string[] Rol { get; set; }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var userRole = context.HttpContext.Session.GetString("Rol");
            if (userRole == null || !Rol.Contains(userRole))
            {
                if (userRole.Equals("Administrador", StringComparison.InvariantCultureIgnoreCase))
                {
                    context.Result = new RedirectToActionResult("Index", "Home", null);
                }
                else if (userRole.Equals("Usuario", StringComparison.InvariantCultureIgnoreCase))
                {
                    context.Result = new RedirectToActionResult("Index", "Vuelos", null);
                }
                else if (userRole.Equals("Empleado", StringComparison.InvariantCultureIgnoreCase))
                {
                    context.Result = new RedirectToActionResult("Index", "Home", null);
                }
                else
                {
                    context.Result = new RedirectToActionResult("Index", "Vuelos", null);
                }
            }
            base.OnActionExecuting(context);
        }
    }
}
