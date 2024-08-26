using GTPWeb.Models;

namespace GTPWeb.Services
{
    public interface IRolesService
    {
        public List<UsuarioEnProyecto> RolUsuarioPorProyecto(int? ProyectoId, int? UsuarioId);
        public UsuarioEnProyecto RolUsuario(int? UsuarioId);
    }
}
