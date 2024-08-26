using GTPWeb.Models;
using Microsoft.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using System.Text;
using System.Text.Json;


namespace GTPWeb.Services
{
    public class ProyectoService : IProyectoService
    {
        private readonly GTPContext _context;

        public ProyectoService(GTPContext context)
        {
            _context = context;
        }

        public double CalcularProgresoProyecto(int proyectoId)
        {
            
            var tareas = _context.Tareas.Where(t => t.ProyectoID == proyectoId).ToList();

            if (tareas.Count == 0)
            {
                return 0; 
            }

           
            var tareasCompletadas = tareas.Count(t => t.Estado == "Completada"); 

            
            var progreso = (double)tareasCompletadas / tareas.Count * 100;

            return progreso;
        }
        public int CantidadTareasProyecto(int proyectoId)
        {

            var tareas = _context.Tareas.Where(t => t.ProyectoID == proyectoId).ToList();

            if (tareas.Count == 0)
            {
                return 0;
            }


            var tareasNoCompletadas = tareas.Count(t => t.Estado != "Completada");


            

            return tareasNoCompletadas;
        }
        public string NombreProyecto(int proyectoId)
        {

            var nombre = _context.Proyectos.Where(t => t.ID == proyectoId).FirstOrDefault();


            return nombre.Nombre;
        }
        public async Task<IEnumerable<UsuarioEnProyecto>> ListaUsuarioPAsync(int proyectoId)
        {
            return await _context.UsuariosEnProyectos
                .Include(u => u.Proyecto)
                .Include(u => u.Rol)
                .Include(u => u.Usuario)
                .Where(u => u.ProyectoID == proyectoId)
                .ToListAsync();
        }
        public string NombreLider(int proyectoId) {

            var nombre = _context.UsuariosEnProyectos
              .Include(u => u.Proyecto)
              .Include(u => u.Rol)
            .Include(u => u.Usuario)
              .Where(u => u.ProyectoID == proyectoId && u.Rol.Nombre == "Admin")
              .FirstOrDefault();
            
            return nombre.Usuario.CorreoElectronico;
        }


    }
}
