using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;

namespace GTPWeb.Models
{
    public class FiltroSeguridad : IActionFilter
    {
        public void OnActionExecuting(ActionExecutingContext context)
        {
            // Aquí se usa HttpContext.Session para verificar la sesión del usuario
            if (context.HttpContext.Session.GetString("NombreUsuario") == null)
            {
                // Redirige a la acción de inicio de sesión si la sesión del usuario no existe
                context.Result = new RedirectToActionResult("InicioSesion", "Login", null);
            }
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            // No es necesario para este caso, pero debes implementarlo para cumplir con la interfaz IActionFilter
        }
    }
}
