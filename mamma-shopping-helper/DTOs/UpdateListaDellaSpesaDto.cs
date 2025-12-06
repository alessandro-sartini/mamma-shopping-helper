using System.ComponentModel.DataAnnotations;

namespace mamma_shopping_helper.DTOs
{
    public class UpdateListaDellaSpesaDto
    {
        [Required(ErrorMessage = "Il titolo è obbligatorio")]
        [MaxLength(200)]
        public string Titolo { get; set; }

        [MaxLength(500)]
        public string? Descrizione { get; set; }

        [Required]
        public bool Conclusa { get; set; }

    }
}
