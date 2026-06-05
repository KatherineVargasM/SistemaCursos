
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SistemaCursos.Models;
using SistemaCursos.Data;

public class EstudiantesController : Controller
{
    private readonly ApplicationDbContext _context;

    public EstudiantesController(ApplicationDbContext context)
    {
        _context = context;
    }

    // GET: ESTUDIANTEMODELS
    public async Task<IActionResult> Index()    
    {
        return View(await _context.Estudiantes.ToListAsync());
    }

    // GET: ESTUDIANTEMODELS/Details/5
    public async Task<IActionResult> Details(int? estudiante_id)
    {
        if (estudiante_id == null)
        {
            return NotFound();
        }

        var estudiantemodel = await _context.Estudiantes
            .FirstOrDefaultAsync(m => m.estudiante_id == estudiante_id);
        if (estudiantemodel == null)
        {
            return NotFound();
        }

        return View(estudiantemodel);
    }

    // GET: ESTUDIANTEMODELS/Create
    public IActionResult Create()
    {
        return View();
    }

    // POST: ESTUDIANTEMODELS/Create
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("estudiante_id,nombre,apellido,email,fecha_nacimiento,Inscripciones")] EstudianteModel estudiantemodel)
    {
        if (ModelState.IsValid)
        {
            _context.Add(estudiantemodel);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        return View(estudiantemodel);
    }

    // GET: ESTUDIANTEMODELS/Edit/5
    public async Task<IActionResult> Edit(int? estudiante_id)
    {
        if (estudiante_id == null)
        {
            return NotFound();
        }

        var estudiantemodel = await _context.Estudiantes.FindAsync(estudiante_id);
        if (estudiantemodel == null)
        {
            return NotFound();
        }
        return View(estudiantemodel);
    }

    // POST: ESTUDIANTEMODELS/Edit/5
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int? estudiante_id, [Bind("estudiante_id,nombre,apellido,email,fecha_nacimiento,Inscripciones")] EstudianteModel estudiantemodel)
    {
        if (estudiante_id != estudiantemodel.estudiante_id)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                _context.Update(estudiantemodel);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EstudianteModelExists(estudiantemodel.estudiante_id))
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
        return View(estudiantemodel);
    }

    // GET: ESTUDIANTEMODELS/Delete/5
    public async Task<IActionResult> Delete(int? estudiante_id)
    {
        if (estudiante_id == null)
        {
            return NotFound();
        }

        var estudiantemodel = await _context.Estudiantes
            .FirstOrDefaultAsync(m => m.estudiante_id == estudiante_id);
        if (estudiantemodel == null)
        {
            return NotFound();
        }

        return View(estudiantemodel);
    }

    // POST: ESTUDIANTEMODELS/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int? estudiante_id)
    {
        var estudiantemodel = await _context.Estudiantes.FindAsync(estudiante_id);
        if (estudiantemodel != null)
        {
            _context.Estudiantes.Remove(estudiantemodel);
        }

        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    private bool EstudianteModelExists(int? estudiante_id)
    {
        return _context.Estudiantes.Any(e => e.estudiante_id == estudiante_id);
    }
}
