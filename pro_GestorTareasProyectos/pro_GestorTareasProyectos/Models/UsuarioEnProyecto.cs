using System.ComponentModel.DataAnnotations;

namespace pro_GestorTareasProyectos.Models
{
    public class UsuarioEnProyecto
    {
        [Key]
        public int ID { get; set; }
        public int ProyectoID { get; set; }
        public Proyecto Proyecto { get; set; }
        public int UsuarioID { get; set; }
        public Usuario Usuario { get; set; }
        public int RolID { get; set; }
        public Rol Rol { get; set; }
    }
}
