using GTPWeb.Models;

namespace GTPWeb.Services
{
    public interface ILoginService
    {
        Task<Usuario> validarLogin(string email, string contraseña);
         Task<int> RegistrarUsuario(Usuario entidad);
    }
}
