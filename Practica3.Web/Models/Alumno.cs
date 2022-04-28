using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Practica3.Web.Models
{
    public class Alumno
    {
        public int Id { get; set; }
        [Required]
        [MinLength(2, ErrorMessage = "El campo {0} debe contener al menos tres caracteres"), MaxLength(30, ErrorMessage = "El campo {0} debe contener al menos 30 caracteres")]
        public string Nombres { get; set; }

        [Required]
        [MaxLength(30, ErrorMessage = "El campo {0} debe contener al menos un caracter")]
        public string Apellidos { get; set; }

        [Required]
        [RegularExpression("^[0-9]{8,11}")]
        public string Documento { get; set; }

        [Required]
        [MaxLength(50, ErrorMessage = "El campo {0} debe contener al menos un caracter")]
        public string Direccion { get; set; }

        [Required]
        [MaxLength(15, ErrorMessage = "El campo {0} debe contener al menos un caracter")]
        public string Telefono { get; set; }

        [Required]
        [EmailAddress(ErrorMessage = "Escriba un correo válido")]
        public string Correo { get; set; }

        [Required]
        [Range(0, 11, ErrorMessage = "Ingresa un valor válido")]
        public int Grado { get; set; }

        [Required]
        [Range(0, 120, ErrorMessage = "Ingresa un valor válido")]
        public int Edad { get; set; }

        public ICollection<Municipio> Municipios { get; set; }
        [DisplayName("Municipios Number")]
        public int MunicipiosNumber => Municipios == null ? 0 : Municipios.Count;
    }
}
