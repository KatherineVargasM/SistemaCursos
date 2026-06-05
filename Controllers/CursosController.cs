
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SistemaCursos.Models;
using SistemaCursos.Data;

public class CursosController : Controller
{
    private readonly ApplicationDbContext _context;

    public CursosController(ApplicationDbContext context)
    {
        _context = context;
    }

    // GET: CURSOMODELS
    public async Task<IActionResult> Index()    
    {
        return View(await _context.Cursos.ToListAsync());
    }

    // GET: CURSOMODELS/Details/5
    public async Task<IActionResult> Details(int? curso_id)
    {
        if (curso_id == null)
        {
            return NotFound();
        }

        var cursomodel = await _context.Cursos
            .FirstOrDefaultAsync(m => m.curso_id == curso_id);
        if (cursomodel == null)
        {
            return NotFound();
        }

        return View(cursomodel);
    }

    // GET: CURSOMODELS/Create
    public IActionResult Create()
    {
        return View();
    }

    // POST: CURSOMODELS/Create
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("curso_id,nombre,descripcion,fecha_inicio,fecha_fin,Inscripciones")] CursoModel cursomodel)
    {
        if (ModelState.IsValid)
        {
            _context.Add(cursomodel);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        return View(cursomodel);
    }

    // GET: CURSOMODELS/Edit/5
    public async Task<IActionResult> Edit(int? curso_id)
    {
        if (curso_id == null)
        {
            return NotFound();
        }

        var cursomodel = await _context.Cursos.FindAsync(curso_id);
        if (cursomodel == null)
        {
            return NotFound();
        }
        return View(cursomodel);
    }

    // POST: CURSOMODELS/Edit/5
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int? curso_id, [Bind("curso_id,nombre,descripcion,fecha_inicio,fecha_fin,Inscripciones")] CursoModel cursomodel)
    {
        if (curso_id != cursomodel.curso_id)
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

    // GET: CURSOMODELS/Delete/5
    public async Task<IActionResult> Delete(int? curso_id)
    {
        if (curso_id == null)
        {
            return NotFound();
        }

        var cursomodel = await _context.Cursos
            .FirstOrDefaultAsync(m => m.curso_id == curso_id);
        if (cursomodel == null)
        {
            return NotFound();
        }

        return View(cursomodel);
    }

    // POST: CURSOMODELS/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int? curso_id)
    {
        var cursomodel = await _context.Cursos.FindAsync(curso_id);
        if (cursomodel != null)
        {
            _context.Cursos.Remove(cursomodel);
        }

        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    private bool CursoModelExists(int? curso_id)
    {
        return _context.Cursos.Any(e => e.curso_id == curso_id);
    }
}
