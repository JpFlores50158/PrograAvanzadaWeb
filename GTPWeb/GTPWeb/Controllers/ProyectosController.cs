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
    public class ProyectosController : Controller
    {
        private readonly GTPContext _context;

        public ProyectosController(GTPContext context)
        {
            _context = context;
        }

        // GET: Proyectos
        public async Task<IActionResult> Index()
        {
            var userId = HttpContext.Session.GetInt32("UserId");

            if (userId == null)
            {
                
                return RedirectToAction("InicioSesion", "Login");
            }
            var proyectos = _context.Proyectos
          .Where(p => p.UsuariosEnProyectos.Any(up => up.UsuarioID == userId))
          .ToList();

            return View(proyectos);
           
        }

        // GET: Proyectos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var proyecto = await _context.Proyectos
                .FirstOrDefaultAsync(m => m.ID == id);
            if (proyecto == null)
            {
                return NotFound();
            }

            return View(proyecto);
        }

        // GET: Proyectos/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Proyectos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Nombre,Descripcion,FechaInicio,FechaFin")] Proyecto proyecto)
        {
            
                _context.Add(proyecto);
                await _context.SaveChangesAsync();
               
                var userId = HttpContext.Session.GetInt32("UserId");

                if (userId != null)
                {
                    // Crear un registro en UsuarioEnProyecto
                    var usuarioEnProyecto = new UsuarioEnProyecto
                    {
                        ProyectoID = proyecto.ID,
                        UsuarioID = userId.Value,
                        RolID = 1
                    };
                    _context.UsuariosEnProyectos.Add(usuarioEnProyecto);
                    _context.SaveChanges();
                   

                }
                return RedirectToAction(nameof(Index));
            
        }

        // GET: Proyectos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var proyecto = await _context.Proyectos.FindAsync(id);
            if (proyecto == null)
            {
                return NotFound();
            }
            return View(proyecto);
        }

        // POST: Proyectos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Nombre,Descripcion,FechaInicio,FechaFin")] Proyecto proyecto)
        {
            if (id != proyecto.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(proyecto);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProyectoExists(proyecto.ID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(proyecto);
        }

       

 
        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            
            var proyecto = _context.Proyectos.Find(id);

            if (proyecto != null)
            {
                // 1. Eliminar Comentarios relacionados con las Tareas del Proyecto
                var comentarios = _context.Comentarios
                    .Where(c => c.Tarea.ProyectoID == id)
                    .ToList();

                _context.Comentarios.RemoveRange(comentarios);

                // 2. Eliminar Tareas relacionadas con el Proyecto
                var tareas = _context.Tareas
                    .Where(t => t.ProyectoID == id)
                    .ToList();

                _context.Tareas.RemoveRange(tareas);

                // 3. Eliminar UsuariosEnProyecto relacionados con el Proyecto
                var usuariosEnProyectos = _context.UsuariosEnProyectos
                    .Where(up => up.ProyectoID == id)
                    .ToList();

                _context.UsuariosEnProyectos.RemoveRange(usuariosEnProyectos);

                // 4. Finalmente, eliminar el Proyecto
                _context.Proyectos.Remove(proyecto);

                // Guardar los cambios en la base de datos
                _context.SaveChanges();
            }

            return RedirectToAction(nameof(Index)); ;
        }

        private bool ProyectoExists(int id)
        {
            return _context.Proyectos.Any(e => e.ID == id);
        }
    }
}
