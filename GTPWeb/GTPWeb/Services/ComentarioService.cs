using GTPWeb.Models;
using Microsoft.EntityFrameworkCore;
using System.Text;
using System.Text.Json;


namespace GTPWeb.Services
{
    public class ComentarioService : IComentarioService
    {
        private readonly GTPContext _context;

        public ComentarioService(GTPContext context)
        {
            _context = context;
        }

       
        public List<Comentario> ListaComentarios(int tareaId)
        {
            return _context.Comentarios.Include(c => c.Tarea).Include(c => c.Usuario).Where(c => c.TareaID == tareaId).ToList();
        }


    }
}
