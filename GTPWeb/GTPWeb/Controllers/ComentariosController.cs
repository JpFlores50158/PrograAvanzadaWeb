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
    public class ComentariosController : Controller
    {
        private readonly GTPContext _context;

        public ComentariosController(GTPContext context)
        {
            _context = context;
        }

        // GET: Comentarios
        public async Task<IActionResult> Index()
        {
            var gTPContext = _context.Comentarios.Include(c => c.Tarea).Include(c => c.Usuario);
            return View(await gTPContext.ToListAsync());
        }

        // GET: Comentarios/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var comentario = await _context.Comentarios
                .Include(c => c.Tarea)
                .Include(c => c.Usuario)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (comentario == null)
            {
                return NotFound();
            }

            return View(comentario);
        }

        // GET: Comentarios/Create
        [HttpGet]
        public IActionResult Create(int id)
        {

            ViewData["TareaID"] = id;
            
            return View();
        }

        // POST: Comentarios/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("TareaID,UsuarioID,Mensaje,FechaCreacion")] Comentario comentario)
        {
            DateTime fechaActual = DateTime.Now;
            comentario.FechaCreacion = fechaActual;
                _context.Add(comentario);
                await _context.SaveChangesAsync();
            var idProyecto = _context.Tareas.Where(t => t.ID == comentario.TareaID).FirstOrDefault();
            return RedirectToAction("Index", "Tareas", new { id = idProyecto.ProyectoID });

            ViewData["TareaID"] = new SelectList(_context.Tareas, "ID", "Titulo", comentario.TareaID);
            ViewData["UsuarioID"] = new SelectList(_context.Usuarios, "Id", "Contrasena", comentario.UsuarioID);
            return View(comentario);
        }

        // GET: Comentarios/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var comentario = await _context.Comentarios.FindAsync(id);
            if (comentario == null)
            {
                return NotFound();
            }
            
            return View(comentario);
        }

        // POST: Comentarios/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,TareaID,UsuarioID,Mensaje,FechaCreacion")] Comentario comentario)
        {
            if (id != comentario.ID)
            {
                return NotFound();
            }

           
                try
                {
                DateTime fechaActual = DateTime.Now;
                comentario.FechaCreacion = fechaActual;
                _context.Update(comentario);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ComentarioExists(comentario.ID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
            var idProyecto = _context.Tareas.Where(t => t.ID == comentario.TareaID).FirstOrDefault();
            return RedirectToAction("Index", "Tareas", new { id = idProyecto.ProyectoID });


            return View(comentario);
        }

       

        // POST: Comentarios/Delete/5
        [HttpGet]
       
        public async Task<IActionResult> Delete(int id)
        {
            var comentario = await _context.Comentarios.FindAsync(id);
            if (comentario != null)
            {
                _context.Comentarios.Remove(comentario);
            }

            await _context.SaveChangesAsync();
            var idProyecto = _context.Tareas.Where(t => t.ID == comentario.TareaID).FirstOrDefault();
            return RedirectToAction("Index", "Tareas", new { id = idProyecto.ProyectoID });
        }

        private bool ComentarioExists(int id)
        {
            return _context.Comentarios.Any(e => e.ID == id);
        }
    }
}
