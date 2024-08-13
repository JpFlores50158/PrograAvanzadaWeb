using System.ComponentModel.DataAnnotations;

namespace pro_GestorTareasProyectos.Models
{
    public class Comentario
    {
        [Key]
        public int ID { get; set; }
        public int TareaID { get; set; }
        public Tarea Tarea { get; set; }
        public int UsuarioID { get; set; }
        public Usuario Usuario { get; set; }
        public string Mensaje { get; set; }
        public DateTime FechaCreacion { get; set; } 
    }
}
