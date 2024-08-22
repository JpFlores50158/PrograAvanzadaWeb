using Microsoft.AspNetCore.Mvc;
using GTPWeb.Models;
using GTPWeb.Services;

namespace GTPWeb.Controllers
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
                HttpContext.Session.SetInt32("UserId", usuario.Id);
                HttpContext.Session.SetString("NombreUsuario", usuario.Nombre);
                return RedirectToAction("Index", "PaginaPrincipal");
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
        [HttpPost]
        public IActionResult CerrarSesion()
        {
            // Eliminar la sesión del usuario
            HttpContext.Session.Remove("UserId");

            // Redirigir al inicio de sesión o a la página de inicio
            return RedirectToAction("InicioSesion");
        }
    }
}
