using System.ComponentModel.DataAnnotations;

namespace mamma_shopping_helper.DTOs
{
    public class CreateListaDellaSpesaDto
    {
        [Required(ErrorMessage = "Il titolo è obbligatorio")]
        [MaxLength(200)]
        public string Titolo { get; set; } = string.Empty;

        [MaxLength(500)]
        public string? Descrizione { get; set; }

        [MaxLength(100)]
        public string? CreataDa { get; set; }
    }
}
