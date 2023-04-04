using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace WEB_SITE.Services
{
    public class ValidateSession:ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (context.HttpContext.Session.GetString("User") == null)
            {
                context.Result = new RedirectToActionResult("Index", "Login",null);
            }

            base.OnActionExecuting(context);
        }
    }
}
