using GTPWeb.Models;

namespace GTPWeb.Services
{
    public interface IUsuariosPService
    {
        public List<UsuarioEnProyecto> ListaUsuarioP(int proyectoId);
        public UsuarioEnProyecto ListaUsuariosPUsuario(int usuarioId);
    }
}
