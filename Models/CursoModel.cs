using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SistemaCursos.Models
{
    public class CursoModel
    {
        [Key]
        [Column(Order = 1)]
        public int curso_id { get; set; }

        [Column(Order = 2)]
        [Required(ErrorMessage = "El campo Nombre es obligatorio.")]
        [StringLength(100, ErrorMessage = "El nombre no puede tener más de 100 caracteres.")]
        [Display(Name = "Nombre del Curso")]
        public string nombre { get; set; } = "";

        [Column(Order = 3)]
        [Required(ErrorMessage = "El campo Descripción es obligatorio.")]
        [MaxLength(500, ErrorMessage = "La descripción no puede exceder los 500 caracteres.")]
        [Display(Name = "Descripción")]
        public string descripcion { get; set; } = "";

        [Column(Order = 4)]
        [Required(ErrorMessage = "La Fecha de Inicio es obligatoria.")]
        [DataType(DataType.Date)]
        [Display(Name = "Fecha de Inicio")]
        public DateTime fecha_inicio { get; set; }

        [Column(Order = 5)]
        [Required(ErrorMessage = "La Fecha de Fin es obligatoria.")]
        [DataType(DataType.Date)]
        [Display(Name = "Fecha de Fin")]
        public DateTime fecha_fin { get; set; }

        public ICollection<InscripcionModel> Inscripciones { get; set; } = new List<InscripcionModel>();
    }
}