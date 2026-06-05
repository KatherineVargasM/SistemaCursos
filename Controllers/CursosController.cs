using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SistemaCursos.Models;
using SistemaCursos.Data;

namespace SistemaCursos.Controllers
{
    public class CursosController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CursosController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _context.Cursos.ToListAsync());
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cursomodel = await _context.Cursos
                .FirstOrDefaultAsync(m => m.curso_id == id);
            if (cursomodel == null)
            {
                return NotFound();
            }

            return View(cursomodel);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("curso_id,nombre,descripcion,fecha_inicio,fecha_fin")] CursoModel cursomodel)
        {
            if (ModelState.IsValid)
            {
                _context.Add(cursomodel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(cursomodel);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cursomodel = await _context.Cursos.FindAsync(id);
            if (cursomodel == null)
            {
                return NotFound();
            }
            return View(cursomodel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("curso_id,nombre,descripcion,fecha_inicio,fecha_fin")] CursoModel cursomodel)
        {
            if (id != cursomodel.curso_id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(cursomodel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CursoModelExists(cursomodel.curso_id))
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
            return View(cursomodel);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cursomodel = await _context.Cursos
                .FirstOrDefaultAsync(m => m.curso_id == id);
            if (cursomodel == null)
            {
                return NotFound();
            }

            return View(cursomodel);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var cursomodel = await _context.Cursos.FindAsync(id);
            if (cursomodel != null)
            {
                _context.Cursos.Remove(cursomodel);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CursoModelExists(int id)
        {
            return _context.Cursos.Any(e => e.curso_id == id);
        }
    }
}