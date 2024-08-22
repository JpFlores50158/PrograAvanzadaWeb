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
    public class UsuarioEnProyectosController : Controller
    {
        private readonly GTPContext _context;

        public UsuarioEnProyectosController(GTPContext context)
        {
            _context = context;
        }

        // GET: UsuarioEnProyectos
        public async Task<IActionResult> Index()
        {
            var gTPContext = _context.UsuariosEnProyectos.Include(u => u.Proyecto).Include(u => u.Rol).Include(u => u.Usuario);
            return View(await gTPContext.ToListAsync());
        }

        // GET: UsuarioEnProyectos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var usuarioEnProyecto = await _context.UsuariosEnProyectos
                .Include(u => u.Proyecto)
                .Include(u => u.Rol)
                .Include(u => u.Usuario)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (usuarioEnProyecto == null)
            {
                return NotFound();
            }

            return View(usuarioEnProyecto);
        }

        // GET: UsuarioEnProyectos/Create
        [HttpGet]
        public IActionResult Create(int id)
        {
            // Obtenemos los IDs de los usuarios que ya están en el proyecto
            var usuariosEnProyecto = _context.UsuariosEnProyectos
                .Where(up => up.ProyectoID == id)
                .Select(up => up.UsuarioID)
                .ToList();

            // Filtramos los usuarios que no están en la lista de usuarios en el proyecto
            var usuariosDisponibles = _context.Usuarios
                .Where(u => !usuariosEnProyecto.Contains(u.Id))
                .ToList();

            var roles = _context.Roles
                .Where(u =>u.Nombre !="Admin")
                .ToList();

            // Llenamos el ViewBag con los datos filtrados
            ViewBag.RolID = new SelectList(roles, "ID", "Nombre");
            ViewData["ProyectoID"] = id;

            ViewBag.UsuarioID = new SelectList(usuariosDisponibles, "Id", "Nombre");

            return View();
        }


        // POST: UsuarioEnProyectos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ProyectoID,Email,RolID")] UsuarioEnProyecto usuarioEnProyecto)
        {
             var usuario = _context.Usuarios.Where(u => u.CorreoElectronico == usuarioEnProyecto.Email).FirstOrDefault();
            if (usuario == null) {

                ViewData["ProyectoID"] = new SelectList(_context.Proyectos, "ID", "Nombre", usuarioEnProyecto.ProyectoID);
                ViewData["RolID"] = new SelectList(_context.Roles, "ID", "Nombre", usuarioEnProyecto.RolID);
                TempData["ErrorMessage"] = "No hay ningun usuario con ese correo";
                return View(usuarioEnProyecto);
            }
            usuarioEnProyecto.UsuarioID = usuario.Id;
                _context.Add(usuarioEnProyecto);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index", "Tareas", new { id = usuarioEnProyecto.ProyectoID });

           
        }

        // GET: UsuarioEnProyectos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var usuarioEnProyecto = await _context.UsuariosEnProyectos.FindAsync(id);
            if (usuarioEnProyecto == null)
            {
                return NotFound();
            }

            var roles = _context.Roles
               .Where(u => u.Nombre != "Admin")
               .ToList();

            ViewData["ProyectoID"] = new SelectList(_context.Proyectos, "ID", "Nombre", usuarioEnProyecto.ProyectoID);
            ViewData["RolID"] = new SelectList(roles, "ID", "Nombre", usuarioEnProyecto.RolID);
            ViewData["UsuarioID"] = new SelectList(_context.Usuarios, "Id", "Contrasena", usuarioEnProyecto.UsuarioID);
            return View(usuarioEnProyecto);
        }

        // POST: UsuarioEnProyectos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,ProyectoID,UsuarioID,RolID")] UsuarioEnProyecto usuarioEnProyecto)
        {
            if (id != usuarioEnProyecto.ID)
            {
                return NotFound();
            }

            
                try
                {
                    _context.Update(usuarioEnProyecto);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UsuarioEnProyectoExists(usuarioEnProyecto.ID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Index", "Tareas", new { id = usuarioEnProyecto.ProyectoID });
            
            ViewData["ProyectoID"] = new SelectList(_context.Proyectos, "ID", "Nombre", usuarioEnProyecto.ProyectoID);
            ViewData["RolID"] = new SelectList(_context.Roles, "ID", "Nombre", usuarioEnProyecto.RolID);
            ViewData["UsuarioID"] = new SelectList(_context.Usuarios, "Id", "Nombre", usuarioEnProyecto.UsuarioID);
            return View(usuarioEnProyecto);
        }

        // GET: UsuarioEnProyectos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var usuarioEnProyecto = await _context.UsuariosEnProyectos
                .Include(u => u.Proyecto)
                .Include(u => u.Rol)
                .Include(u => u.Usuario)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (usuarioEnProyecto == null)
            {
                return NotFound();
            }

            return View(usuarioEnProyecto);
        }

        // POST: UsuarioEnProyectos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            // Encuentra el UsuarioEnProyecto
            var usuarioEnProyecto = _context.UsuariosEnProyectos
                .Include(up => up.Tareas) // Incluye las tareas relacionadas
                .ThenInclude(t => t.Comentarios) // Incluye los comentarios de las tareas
                .FirstOrDefault(up => up.ID == id);

            if (usuarioEnProyecto != null)
            {
                // 1. Eliminar los Comentarios relacionados con las Tareas
                var tareas = usuarioEnProyecto.Tareas.ToList();
                foreach (var tarea in tareas)
                {
                    var comentarios = _context.Comentarios
                        .Where(c => c.TareaID == tarea.ID)
                        .ToList();

                    _context.Comentarios.RemoveRange(comentarios);
                }

                // 2. Eliminar las Tareas relacionadas con el UsuarioEnProyecto
                _context.Tareas.RemoveRange(tareas);

                // 3. Eliminar el UsuarioEnProyecto
                _context.UsuariosEnProyectos.Remove(usuarioEnProyecto);

                // Guardar los cambios en la base de datos
                _context.SaveChanges();
            }
            return RedirectToAction("Index", "Tareas", new { id = usuarioEnProyecto.ProyectoID });
        }



        [HttpGet]
        public async Task<IActionResult> DeleteUsuarioNoAdmin(int id)
        {
            // Encuentra el UsuarioEnProyecto
            var usuarioEnProyecto = _context.UsuariosEnProyectos
                .Include(up => up.Tareas)
                .Where(up => up.UsuarioID == id)
                .FirstOrDefault();

            if (usuarioEnProyecto != null)
            {
                // 1. Eliminar las Tareas relacionadas con el UsuarioEnProyecto (opcional)
                var tareas = _context.Tareas
                    .Where(t => t.UsuarioEnProyectoID == id)
                    .ToList();

                _context.Tareas.RemoveRange(tareas);

                // 2. Eliminar el UsuarioEnProyecto
                _context.UsuariosEnProyectos.Remove(usuarioEnProyecto);

                // Guardar los cambios en la base de datos
                _context.SaveChanges();
            }
            return RedirectToAction("Index", "PaginaPrincipal");
        }

        private bool UsuarioEnProyectoExists(int id)
        {
            return _context.UsuariosEnProyectos.Any(e => e.ID == id);
        }
    }
}
