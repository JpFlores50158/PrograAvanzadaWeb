using System.ComponentModel.DataAnnotations;

namespace pro_GestorTareasProyectos.Models
{
    public class Rol
    {
        [Key]
        public int ID { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }

        public List<UsuarioEnProyecto> UsuariosEnProyectos { get; set; }
    }
}
