using System.ComponentModel.DataAnnotations;

namespace GTPWeb.Models
{
    public class Comentario
    {
        [Key]
        public int ID { get; set; }

        [Required]
        public int TareaID { get; set; }

        [Required]
        public Tarea Tarea { get; set; }

        [Required]
        public int UsuarioID { get; set; }

        [Required]
        public Usuario Usuario { get; set; }

        [Required(ErrorMessage = "El mensaje es obligatorio.")]
        [StringLength(1000, ErrorMessage = "El mensaje no puede superar los 1000 caracteres.")]
        public string Mensaje { get; set; }

        [DataType(DataType.DateTime)]
        [Display(Name = "Fecha de Creación")]
        public DateTime FechaCreacion { get; set; }
    }
}
