using System.ComponentModel.DataAnnotations;

namespace pro_GestorTareasProyectos.Models
{
    public class Tarea
    {
        [Key]
        public int ID { get; set; }
        public string Titulo { get; set; }
        public string Descripcion { get; set; }
        public DateTime? FechaVencimiento { get; set; }
        public string Prioridad { get; set; }
        public string Categoria { get; set; }
        public int ProyectoID { get; set; }
        public Proyecto Proyecto { get; set; }
        public int UsuarioID { get; set; }
        public Usuario Usuario { get; set; }

        public List<Comentario> Comentarios { get; set; }
    }
}
