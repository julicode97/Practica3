using System.ComponentModel.DataAnnotations;

namespace Practica3.Web.Models
{
    public class Barrio
    {
        public int Id { get; set; }
        [MaxLength(50, ErrorMessage = "El campo {0} debe contener al menos un caracter")]
        [Required]
        public string Nombre { get; set; }

    }
}
