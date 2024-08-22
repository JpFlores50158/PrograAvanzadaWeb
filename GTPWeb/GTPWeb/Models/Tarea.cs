using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GTPWeb.Models
{
    public class Tarea
    {
        [Key]
        public int ID { get; set; }

        [Required(ErrorMessage = "El título es obligatorio.")]
        [StringLength(100, ErrorMessage = "El título no puede superar los 100 caracteres.")]
        public string Titulo { get; set; }

        [StringLength(500, ErrorMessage = "La descripción no puede superar los 500 caracteres.")]
        public string Descripcion { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Fecha de Vencimiento")]
        public DateTime? FechaVencimiento { get; set; }

        [Required(ErrorMessage = "La prioridad es obligatoria.")]
        [StringLength(50, ErrorMessage = "La prioridad no puede superar los 50 caracteres.")]
        public string Prioridad { get; set; }

        [Required(ErrorMessage = "La categoría es obligatoria.")]
        [StringLength(50, ErrorMessage = "La categoría no puede superar los 50 caracteres.")]
        public string Categoria { get; set; }

        [Required(ErrorMessage = "El estado es obligatorio.")]
        public string Estado { get; set; }

        [Required]
        public int ProyectoID { get; set; }

        [ForeignKey(nameof(ProyectoID))]
        public Proyecto Proyecto { get; set; }

        [Required]
        public int UsuarioEnProyectoID { get; set; }

        [ForeignKey(nameof(UsuarioEnProyectoID))]
        public UsuarioEnProyecto UsuarioEnProyecto { get; set; }

        public List<Comentario> Comentarios { get; set; } 
    }
}
