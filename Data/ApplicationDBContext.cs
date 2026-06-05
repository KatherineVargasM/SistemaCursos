using Microsoft.EntityFrameworkCore;
using SistemaCursos.Models;

namespace SistemaCursos.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<CursoModel> Cursos { get; set; }
        public DbSet<EstudianteModel> Estudiantes { get; set; }
        public DbSet<InscripcionModel> Inscripciones { get; set; }
    }
}