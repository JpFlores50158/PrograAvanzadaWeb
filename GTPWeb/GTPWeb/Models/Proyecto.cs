using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GTPWeb.Models
{
    public class Proyecto
    {
        [Key]
        public int ID { get; set; }

        [Required(ErrorMessage = "El nombre es obligatorio.")]
        [StringLength(100, ErrorMessage = "El nombre no puede superar los 100 caracteres.")]
        public string Nombre { get; set; }

        [StringLength(500, ErrorMessage = "La descripción no puede superar los 500 caracteres.")]
        public string Descripcion { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Fecha de Inicio")]
        public DateTime? FechaInicio { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Fecha de Fin")]
        public DateTime? FechaFin { get; set; }


        public List<Tarea> Tareas { get; set; } = new List<Tarea>();

        public List<UsuarioEnProyecto> UsuariosEnProyectos { get; set; } = new List<UsuarioEnProyecto>();
    }
}
