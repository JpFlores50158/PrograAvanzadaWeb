using GTPWeb.Models;
using Microsoft.AspNetCore.Mvc;

namespace GTPWeb.Controllers
{
    [TypeFilter(typeof(FiltroSeguridad))]
    public class PaginaPrincipalController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

      
    }
}
