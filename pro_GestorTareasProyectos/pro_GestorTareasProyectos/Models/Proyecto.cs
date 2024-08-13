using System.ComponentModel.DataAnnotations;

namespace pro_GestorTareasProyectos.Models
{
    public class Proyecto
    {
        [Key]
        public int ID { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public DateTime? FechaInicio { get; set; }
        public DateTime? FechaFin { get; set; }

        public List<Tarea> Tareas { get; set; }
        public List<UsuarioEnProyecto> UsuariosEnProyectos { get; set; }    
    }
}
