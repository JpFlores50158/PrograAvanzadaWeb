using pro_GestorTareasProyectos.Models;

namespace pro_GestorTareasProyectos.Services
{
    public interface ILoginService
    {
        Task<Usuario> validarLogin(string email, string contraseña);
         Task<int> RegistrarUsuario(Usuario entidad);
    }
}
