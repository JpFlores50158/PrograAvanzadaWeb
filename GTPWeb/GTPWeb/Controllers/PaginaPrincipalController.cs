using GTPWeb.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GTPWeb.Controllers
{
    [TypeFilter(typeof(FiltroSeguridad))]
    public class PaginaPrincipalController : Controller
    {

        private readonly GTPContext _context;

        public PaginaPrincipalController(GTPContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            var userId = HttpContext.Session.GetInt32("UserId");

            if (userId == null)
            {

                return RedirectToAction("InicioSesion", "Login");
            }
            var proyectos = _context.Proyectos
          .Where(p => p.UsuariosEnProyectos.Any(up => up.UsuarioID == userId))
          .ToList();

            return View(proyectos);
        }

      
    }
}
