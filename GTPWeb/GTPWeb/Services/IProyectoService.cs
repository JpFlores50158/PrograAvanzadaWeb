using GTPWeb.Models;

namespace GTPWeb.Services
{
    public interface IProyectoService
    {
        public double CalcularProgresoProyecto(int proyectoId);
        public string NombreProyecto(int proyectoId);
        public Task<IEnumerable<UsuarioEnProyecto>> ListaUsuarioPAsync(int proyectoId);
        public int CantidadTareasProyecto(int proyectoId);
        public string NombreLider(int proyectoId);
    }
}
