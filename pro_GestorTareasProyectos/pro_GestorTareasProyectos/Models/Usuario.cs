using System.ComponentModel.DataAnnotations;

namespace pro_GestorTareasProyectos.Models
{
    public class Usuario
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "El nombre es obligatorio.")]
        [StringLength(100, ErrorMessage = "El nombre no puede exceder los 100 caracteres.")]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "El correo electrónico es obligatorio.")]
        [EmailAddress(ErrorMessage = "El formato del correo electrónico no es válido.")]
        [StringLength(100, ErrorMessage = "El correo electrónico no puede exceder los 100 caracteres.")]
        public string CorreoElectronico { get; set; }

        [Required(ErrorMessage = "La contraseña es obligatoria.")]
        [StringLength(8, ErrorMessage = "La contraseña debe tener al menos 6 caracteres y no más de 8 caracteres.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        public string Contrasena { get; set; }

        public List<Tarea> Tareas { get; set; }
        public List<Comentario> Comentarios { get; set; }
        public List<UsuarioEnProyecto> UsuariosEnProyectos { get; set; }
    }
}
