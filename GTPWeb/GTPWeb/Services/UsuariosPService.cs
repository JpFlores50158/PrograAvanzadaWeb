using GTPWeb.Models;
using Microsoft.EntityFrameworkCore;
using System.Text;
using System.Text.Json;


namespace GTPWeb.Services
{
    public class UsuariosPService : IUsuariosPService
    {
        private readonly GTPContext _context;

        public UsuariosPService(GTPContext context)
        {
            _context = context;
        }

       
        public List<UsuarioEnProyecto> ListaUsuarioP(int proyectoId)
        {
            return  _context.UsuariosEnProyectos
                .Include(u => u.Proyecto)
                .Include(u => u.Rol)
                .Include(u => u.Usuario)
                .Where(u => u.ProyectoID == proyectoId)
                .ToList();
        }
        public UsuarioEnProyecto ListaUsuariosPUsuario(int usuarioId)
        {
            return _context.UsuariosEnProyectos
                .Include(u => u.Proyecto)
                .Include(u => u.Rol)
                .Include(u => u.Usuario)
                .Where(u => u.UsuarioID == usuarioId)
                .FirstOrDefault();
        }


    }
}
