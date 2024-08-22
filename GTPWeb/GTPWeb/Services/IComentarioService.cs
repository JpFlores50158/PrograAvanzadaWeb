using GTPWeb.Models;
using Microsoft.EntityFrameworkCore;

namespace GTPWeb.Services
{
    public interface IComentarioService
    {
        public List<Comentario> ListaComentarios(int tareaId);
    }
}
