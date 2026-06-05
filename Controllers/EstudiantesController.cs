using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SistemaCursos.Models;
using SistemaCursos.Data;

namespace SistemaCursos.Controllers
{
    public class EstudiantesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public EstudiantesController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _context.Estudiantes.ToListAsync());
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var estudianteModel = await _context.Estudiantes
                .FirstOrDefaultAsync(m => m.estudiante_id == id);
            if (estudianteModel == null)
            {
                return NotFound();
            }

            return View(estudianteModel);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("estudiante_id,nombre,apellido,email,fecha_nacimiento")] EstudianteModel estudianteModel)
        {
            if (ModelState.IsValid)
            {
                _context.Add(estudianteModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(estudianteModel);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var estudianteModel = await _context.Estudiantes.FindAsync(id);
            if (estudianteModel == null)
            {
                return NotFound();
            }
            return View(estudianteModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("estudiante_id,nombre,apellido,email,fecha_nacimiento")] EstudianteModel estudianteModel)
        {
            if (id != estudianteModel.estudiante_id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(estudianteModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EstudianteModelExists(estudianteModel.estudiante_id))
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
            return View(estudianteModel);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var estudianteModel = await _context.Estudiantes
                .FirstOrDefaultAsync(m => m.estudiante_id == id);
            if (estudianteModel == null)
            {
                return NotFound();
            }

            return View(estudianteModel);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var estudianteModel = await _context.Estudiantes.FindAsync(id);
            if (estudianteModel != null)
            {
                _context.Estudiantes.Remove(estudianteModel);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EstudianteModelExists(int id)
        {
            return _context.Estudiantes.Any(e => e.estudiante_id == id);
        }
    }
}