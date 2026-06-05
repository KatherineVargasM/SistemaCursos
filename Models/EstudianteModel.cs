using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SistemaCursos.Models
{
    public class EstudianteModel
    {
        [Key]
        [Column(Order = 1)]
        public int estudiante_id { get; set; }

        [Column(Order = 2)]
        [Required(ErrorMessage = "El campo Nombre es obligatorio.")]
        [MaxLength(50, ErrorMessage = "El campo Nombre no puede tener más de 50 caracteres.")]
        public string nombre { get; set; } = "";

        [Column(Order = 3)]
        [Required(ErrorMessage = "El campo Apellido es obligatorio.")]
        [MaxLength(50, ErrorMessage = "El campo Apellido no puede tener más de 50 caracteres.")]
        public string apellido { get; set; } = "";

        [Column(Order = 4)]
        [Required(ErrorMessage = "El campo Correo Electrónico es obligatorio.")]
        [EmailAddress(ErrorMessage = "Debe ser una dirección de correo electrónico válida.")]
        [Display(Name = "Correo Electrónico")]
        public string email { get; set; } = "";

        [Column(Order = 5)]
        [Required(ErrorMessage = "La Fecha de Nacimiento es obligatoria.")]
        [DataType(DataType.Date)]
        [Display(Name = "Fecha de Nacimiento")]
        public DateTime fecha_nacimiento { get; set; }

        public ICollection<InscripcionModel> Inscripciones { get; set; } = new List<InscripcionModel>();
    }
}