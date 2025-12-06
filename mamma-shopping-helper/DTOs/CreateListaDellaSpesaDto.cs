using System.ComponentModel.DataAnnotations;

namespace mamma_shopping_helper.DTOs
{
    public class CreateListaDellaSpesaDto
    {
        [Required(ErrorMessage = "Il titolo è obbligatorio")]
        [MaxLength(200, ErrorMessage = "Il titolo non può superare 200 caratteri")]
        public string Titolo { get; set; }

        [MaxLength(500, ErrorMessage = "La descrizione non può superare 500 caratteri")]
        public string? Descrizione { get; set; }
    }
}
