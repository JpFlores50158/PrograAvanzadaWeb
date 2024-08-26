using GTPWeb.Models;
using Microsoft.EntityFrameworkCore;
using System.Text;
using System.Text.Json;


namespace GTPWeb.Services
{
    public class RolesService : IRolesService
    {
        private readonly GTPContext _context;

        public RolesService(GTPContext context)
        {
            _context = context;
        }


        public List<UsuarioEnProyecto> RolUsuarioPorProyecto(int? ProyectoId, int? UsuarioId)
{
    return _context.UsuariosEnProyectos
        .Include(u => u.Proyecto)
                .Include(u => u.Rol)
                .Include(u => u.Usuario)
        .Where(u => u.UsuarioID == UsuarioId && u.ProyectoID == ProyectoId)
        .ToList();
}

        public UsuarioEnProyecto RolUsuario(int? UsuarioId)
        {
            return _context.UsuariosEnProyectos
                .Include(u => u.Proyecto)
                .Include(u => u.Rol)
                .Include(u => u.Usuario)
                .Where(u => u.UsuarioID == UsuarioId)
                .FirstOrDefault();
        }


    }
}
