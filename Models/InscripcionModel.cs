using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SistemaCursos.Models
{
    public class InscripcionModel
    {
        [Key]
        [Column(Order = 1)]
        public int inscripcion_id { get; set; }

        [Column(Order = 2)]
        [Required(ErrorMessage = "Debe seleccionar un curso.")]
        [Display(Name = "Curso")]
        public int curso_id { get; set; }

        [Column(Order = 3)]
        [Required(ErrorMessage = "Debe seleccionar un estudiante.")]
        [Display(Name = "Estudiante")]
        public int estudiante_id { get; set; }

        [Column(Order = 4)]
        [Required(ErrorMessage = "La Fecha de Inscripción es obligatoria.")]
        [DataType(DataType.Date)]
        [Display(Name = "Fecha de Inscripción")]
        public DateTime fecha_inscripcion { get; set; } = DateTime.Now;

        [ForeignKey("curso_id")]
        public CursoModel? Curso { get; set; }

        [ForeignKey("estudiante_id")]
        public EstudianteModel? Estudiante { get; set; }
    }
}