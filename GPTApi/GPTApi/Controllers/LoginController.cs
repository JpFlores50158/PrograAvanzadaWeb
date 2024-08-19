using GPTApi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace GPTApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly GTPContext _context;

        public LoginController(GTPContext context)
        {
            _context = context;
        }

        [HttpGet]
        [Route("Login")]
        public ActionResult Login(string usuario, string contraseña)
        {
            try
            {
                var Usuarios = _context.Usuarios.FromSqlRaw("Login @user, @contraseña",
                    new SqlParameter("@User", usuario),
                new SqlParameter("@Contraseña", contraseña)).ToList();

                return Ok(Usuarios.FirstOrDefault());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPost]
        [Route("Registrar")]
        public async Task<IActionResult> Registrar(Usuario usuario)
        {
            try
            {
                var nombreParam = new SqlParameter("@nombre", usuario.Nombre);
                var correoParam = new SqlParameter("@Correo", usuario.CorreoElectronico);
                var contrasenaParam = new SqlParameter("@Contrasena", usuario.Contrasena);

                var resultado = await _context.Database.ExecuteSqlRawAsync(
                    "EXEC Registrar @nombre, @Correo, @Contrasena",
                    nombreParam, correoParam, contrasenaParam);

                // Retorna el resultado como un entero
                return Ok(resultado);
            }
            catch (Exception ex)
            {
                // Retorna un código de error en caso de excepción
                return StatusCode(500, -2);
            }
        }



    }
}
