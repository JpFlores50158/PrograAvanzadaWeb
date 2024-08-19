using Microsoft.AspNetCore.Mvc;
using pro_GestorTareasProyectos.Models;
using pro_GestorTareasProyectos.Services;

namespace pro_GestorTareasProyectos.Controllers
{

    public class LoginController : Controller
    {
        private readonly ILoginService _login;
        public LoginController(ILoginService login)
        {
            _login = login;
        }
        [HttpGet]
        public IActionResult InicioSesion()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> InicioSesion(Usuario entidad)
        {

            
            Usuario usuario = await _login.validarLogin(entidad.CorreoElectronico, entidad.Contrasena);
            if (usuario != null)
            {
                return RedirectToAction("Index", "Home");
            }
            TempData["ErrorMessage"] = "No se encontro su usuario.";
            return View();
        }
        [HttpGet]
        public IActionResult Registro()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Registro(Usuario entidad)
        {

            var resultado = await _login.RegistrarUsuario(entidad);

            if (resultado == 1)
            {
                return RedirectToAction("InicioSesion");
            }
            else if (resultado == -1)
            {
                TempData["ErrorMessage"] = "El correo electrónico ya está registrado.";
                return RedirectToAction("Registro"); // Redirige a la vista de registro
            }
            else
            {
                TempData["ErrorMessage"] = "Ocurrió un error al registrar el usuario.";
                return RedirectToAction("Registro"); // Redirige a la vista de registro
            }
        }
    }
}
