using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Practica3.Web.Models
{
    public class Barrio
    {
        public int Id { get; set; }
        [MaxLength(50, ErrorMessage = "El campo {0} debe contener al menos un caracter")]
        [Required]
        public string Nombre { get; set; }

        [JsonIgnore] //lo ignora en la respuesta json
        public int MunicipioId { get; set; }

    }
}
