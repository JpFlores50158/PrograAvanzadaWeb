using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using GTPWeb.Models;

namespace GTPWeb.Controllers
{
    [TypeFilter(typeof(FiltroSeguridad))]
    public class TareasController : Controller
    {
        private readonly GTPContext _context;

        public TareasController(GTPContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> Index(int id)
        {
            var gTPContext = _context.Tareas.Include(t => t.Proyecto).Include(t => t.UsuarioEnProyecto).Where(t => t.ProyectoID == id);
            ViewData["idProyecto"] = id;
            return View(await gTPContext.ToListAsync());
        }

        // GET: Tareas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tarea = await _context.Tareas
                .Include(t => t.Proyecto)
                .Include(t => t.UsuarioEnProyecto)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (tarea == null)
            {
                return NotFound();
            }

            return View(tarea);
        }

        // GET: Tareas/Create
        [HttpGet]
        public IActionResult Create(int id)
        {
            // Obtén el proyecto especificado por el id
            var proyecto = _context.Proyectos.Find(id);

            if (proyecto == null)
            {
                return NotFound(); // Retorna un error si el proyecto no existe
            }

            // Filtra los usuarios en el proyecto especificado
            var usuariosEnProyecto = _context.UsuariosEnProyectos
                .Where(up => up.ProyectoID == id)
                .Select(up => new
                {
                    up.ID,
                    UsuarioNombre = $"{up.Usuario.Nombre} ({up.Usuario.CorreoElectronico})" 
                })
                .ToList();

            // Configura el ViewData para el select list
            ViewData["UsuarioEnProyectoID"] = new SelectList(usuariosEnProyecto, "ID", "UsuarioNombre");

            // Opcional: Agrega la lista de proyectos si necesitas mostrarla en la vista
            ViewData["idProyecto"] = id;

            return View();
        }

        // POST: Tareas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Titulo,Descripcion,FechaVencimiento,Prioridad,Categoria,Estado,ProyectoID,UsuarioEnProyectoID")] Tarea tarea)
        {
            tarea.Estado = "Pendiente";
                _context.Add(tarea);
                await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index), new { id = tarea.ProyectoID });

            ViewData["idProyecto"] = tarea.ProyectoID;
            ViewData["UsuarioEnProyectoID"] = new SelectList(_context.UsuariosEnProyectos, "ID", "ID", tarea.UsuarioEnProyectoID);
            return View(tarea);
        }

        // GET: Tareas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tarea = await _context.Tareas.FindAsync(id);
            if (tarea == null)
            {
                return NotFound();
            }

            // Filtra los usuarios en el proyecto especificado
            var usuariosEnProyecto = _context.UsuariosEnProyectos
                .Where(up => up.ProyectoID == tarea.ProyectoID)
                .Select(up => new
                {
                    up.ID,
                    UsuarioNombre = $"{up.Usuario.Nombre} ({up.Usuario.CorreoElectronico})"
                })
                .ToList();

            // Configura el ViewData para el select list
            ViewData["UsuarioEnProyectoID"] = new SelectList(usuariosEnProyecto, "ID", "UsuarioNombre");
            return View(tarea);
        }

        // POST: Tareas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Titulo,Descripcion,FechaVencimiento,Prioridad,Categoria,Estado,ProyectoID,UsuarioEnProyectoID")] Tarea tarea)
        {
            if (id != tarea.ID)
            {
                return NotFound();
            }
              
           
                try
                {
                    _context.Update(tarea);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TareaExists(tarea.ID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
            return RedirectToAction(nameof(Index), new { id = tarea.ProyectoID });

            var usuariosEnProyecto = _context.UsuariosEnProyectos
                .Where(up => up.ProyectoID == id)
                .Select(up => new
                {
                    up.ID,
                    UsuarioNombre = $"{up.Usuario.Nombre} ({up.Usuario.CorreoElectronico})"
                })
                .ToList();

            // Configura el ViewData para el select list
            ViewData["UsuarioEnProyectoID"] = new SelectList(usuariosEnProyecto, "ID", "UsuarioNombre");
            return View(tarea);
        }

        // GET: Tareas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tarea = await _context.Tareas
                .Include(t => t.Proyecto)
                .Include(t => t.UsuarioEnProyecto)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (tarea == null)
            {
                return NotFound();
            }

            return View(tarea);
        }

        // POST: Tareas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            // Encuentra la tarea
            var tarea = _context.Tareas
                .Include(t => t.Comentarios) // Incluye comentarios para la eliminación
                .FirstOrDefault(t => t.ID == id);

            if (tarea != null)
            {
               
                var comentarios = _context.Comentarios
                    .Where(c => c.TareaID == id)
                    .ToList();

                _context.Comentarios.RemoveRange(comentarios);

                
                _context.Tareas.Remove(tarea);

               
                _context.SaveChanges();
            }
            return RedirectToAction(nameof(Index), new { id = tarea.ProyectoID });
        }

        private bool TareaExists(int id)
        {
            return _context.Tareas.Any(e => e.ID == id);
        }



        [HttpGet]
        public async Task<IActionResult> Interactiva(int id)
        {
            var tareas = await _context.Tareas
                                .Where(t => t.ProyectoID == id)
                                .Include(t => t.UsuarioEnProyecto)
                                .ToListAsync();

            // Agrupar tareas por estado
            var tareasPorEstado = tareas.GroupBy(t => t.Estado)
                                        .ToDictionary(g => g.Key, g => g.ToList());

            ViewData["idProyecto"] = id;
            return View(tareasPorEstado);
        }
        [HttpGet]
        [Route("Tareas/EditKaban")]
        public async Task<IActionResult> EditKaban(int id, string newStatus)
        {
            var tarea = await _context.Tareas.FindAsync(id);

            if (tarea == null)
            {
                return NotFound();
            }

            tarea.Estado = newStatus;

            try
            {
                _context.Update(tarea);
                await _context.SaveChangesAsync();
                return Json(new { success = true });
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                return StatusCode(500, "Internal server error.");
            }
        }





    }
}
