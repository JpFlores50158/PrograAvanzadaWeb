using System.ComponentModel.DataAnnotations;
using System.Threading;

namespace pro_GestorTareasProyectos.Models
{
    public class Usuario
    {
        [Key]
        public int ID { get; set; }
        public string Nombre { get; set; }
        public string CorreoElectronico { get; set; }
        public string Contrasena { get; set; }

        public List<Tarea> Tareas { get; set; }
        public List<Comentario> Comentarios { get; set; }
        public List<UsuarioEnProyecto> UsuariosEnProyectos { get; set; }
    }
}
