using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SistemaCursos.Models;
using SistemaCursos.Data;

namespace SistemaCursos.Controllers
{
    public class InscripcionesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public InscripcionesController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Inscripciones.Include(i => i.Curso).Include(i => i.Estudiante);
            return View(await applicationDbContext.ToListAsync());
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var inscripcionModel = await _context.Inscripciones
                .Include(i => i.Curso)
                .Include(i => i.Estudiante)
                .FirstOrDefaultAsync(m => m.inscripcion_id == id);

            if (inscripcionModel == null)
            {
                return NotFound();
            }

            return View(inscripcionModel);
        }

        public IActionResult Create()
        {
            ViewData["curso_id"] = new SelectList(_context.Cursos, "curso_id", "nombre");
            ViewData["estudiante_id"] = new SelectList(_context.Estudiantes, "estudiante_id", "nombre");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("inscripcion_id,curso_id,estudiante_id,fecha_inscripcion")] InscripcionModel inscripcionModel)
        {
            if (ModelState.IsValid)
            {
                _context.Add(inscripcionModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["curso_id"] = new SelectList(_context.Cursos, "curso_id", "nombre", inscripcionModel.curso_id);
            ViewData["estudiante_id"] = new SelectList(_context.Estudiantes, "estudiante_id", "nombre", inscripcionModel.estudiante_id);
            return View(inscripcionModel);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var inscripcionModel = await _context.Inscripciones.FindAsync(id);
            if (inscripcionModel == null)
            {
                return NotFound();
            }
            ViewData["curso_id"] = new SelectList(_context.Cursos, "curso_id", "nombre", inscripcionModel.curso_id);
            ViewData["estudiante_id"] = new SelectList(_context.Estudiantes, "estudiante_id", "nombre", inscripcionModel.estudiante_id);
            return View(inscripcionModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("inscripcion_id,curso_id,estudiante_id,fecha_inscripcion")] InscripcionModel inscripcionModel)
        {
            if (id != inscripcionModel.inscripcion_id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(inscripcionModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!InscripcionModelExists(inscripcionModel.inscripcion_id))
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
            ViewData["curso_id"] = new SelectList(_context.Cursos, "curso_id", "nombre", inscripcionModel.curso_id);
            ViewData["estudiante_id"] = new SelectList(_context.Estudiantes, "estudiante_id", "nombre", inscripcionModel.estudiante_id);
            return View(inscripcionModel);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var inscripcionModel = await _context.Inscripciones
                .Include(i => i.Curso)
                .Include(i => i.Estudiante)
                .FirstOrDefaultAsync(m => m.inscripcion_id == id);
            if (inscripcionModel == null)
            {
                return NotFound();
            }

            return View(inscripcionModel);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var inscripcionModel = await _context.Inscripciones.FindAsync(id);
            if (inscripcionModel != null)
            {
                _context.Inscripciones.Remove(inscripcionModel);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool InscripcionModelExists(int id)
        {
            return _context.Inscripciones.Any(e => e.inscripcion_id == id);
        }
    }
}