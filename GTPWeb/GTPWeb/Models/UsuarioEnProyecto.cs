using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GTPWeb.Models
{
    public class UsuarioEnProyecto
    {
        [Key]
        public int ID { get; set; }

        [Required]
        public int ProyectoID { get; set; }

        [ForeignKey(nameof(ProyectoID))]
        public Proyecto Proyecto { get; set; }

        [Required]
        public int UsuarioID { get; set; }

        [ForeignKey(nameof(UsuarioID))]
        public Usuario Usuario { get; set; }

        [Required]
        public int RolID { get; set; }

        [ForeignKey(nameof(RolID))]
        public Rol Rol { get; set; }

        public List<Tarea> Tareas { get; set; }

        
        [NotMapped]
        public string? Email { get; set; }
    }
}
