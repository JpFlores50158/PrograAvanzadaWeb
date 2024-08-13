using Microsoft.AspNetCore.Mvc;

namespace pro_GestorTareasProyectos.Controllers
{
    public class ProyectoController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult CrearProyecto()
        {
            return View();
        }
        public IActionResult VerProyectos()
        {
            return View();
        }

        public IActionResult VerTareas()
        {
            return View();
        }
    }
}
