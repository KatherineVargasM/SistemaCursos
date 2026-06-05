
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SistemaCursos.Models;
using SistemaCursos.Data;

public class InscripcionesController : Controller
{
    private readonly ApplicationDbContext _context;

    public InscripcionesController(ApplicationDbContext context)
    {
        _context = context;
    }

    // GET: INSCRIPCIONMODELS
    public async Task<IActionResult> Index()    
    {
        return View(await _context.Inscripciones.ToListAsync());
    }

    // GET: INSCRIPCIONMODELS/Details/5
    public async Task<IActionResult> Details(int? inscripcion_id)
    {
        if (inscripcion_id == null)
        {
            return NotFound();
        }

        var inscripcionmodel = await _context.Inscripciones
            .FirstOrDefaultAsync(m => m.inscripcion_id == inscripcion_id);
        if (inscripcionmodel == null)
        {
            return NotFound();
        }

        return View(inscripcionmodel);
    }

    // GET: INSCRIPCIONMODELS/Create
    public IActionResult Create()
    {
        return View();
    }

    // POST: INSCRIPCIONMODELS/Create
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("inscripcion_id,curso_id,estudiante_id,fecha_inscripcion,Curso,Estudiante")] InscripcionModel inscripcionmodel)
    {
        if (ModelState.IsValid)
        {
            _context.Add(inscripcionmodel);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        return View(inscripcionmodel);
    }

    // GET: INSCRIPCIONMODELS/Edit/5
    public async Task<IActionResult> Edit(int? inscripcion_id)
    {
        if (inscripcion_id == null)
        {
            return NotFound();
        }

        var inscripcionmodel = await _context.Inscripciones.FindAsync(inscripcion_id);
        if (inscripcionmodel == null)
        {
            return NotFound();
        }
        return View(inscripcionmodel);
    }

    // POST: INSCRIPCIONMODELS/Edit/5
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int? inscripcion_id, [Bind("inscripcion_id,curso_id,estudiante_id,fecha_inscripcion,Curso,Estudiante")] InscripcionModel inscripcionmodel)
    {
        if (inscripcion_id != inscripcionmodel.inscripcion_id)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                _context.Update(inscripcionmodel);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!InscripcionModelExists(inscripcionmodel.inscripcion_id))
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
        return View(inscripcionmodel);
    }

    // GET: INSCRIPCIONMODELS/Delete/5
    public async Task<IActionResult> Delete(int? inscripcion_id)
    {
        if (inscripcion_id == null)
        {
            return NotFound();
        }

        var inscripcionmodel = await _context.Inscripciones
            .FirstOrDefaultAsync(m => m.inscripcion_id == inscripcion_id);
        if (inscripcionmodel == null)
        {
            return NotFound();
        }

        return View(inscripcionmodel);
    }

    // POST: INSCRIPCIONMODELS/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int? inscripcion_id)
    {
        var inscripcionmodel = await _context.Inscripciones.FindAsync(inscripcion_id);
        if (inscripcionmodel != null)
        {
            _context.Inscripciones.Remove(inscripcionmodel);
        }

        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    private bool InscripcionModelExists(int? inscripcion_id)
    {
        return _context.Inscripciones.Any(e => e.inscripcion_id == inscripcion_id);
    }
}
