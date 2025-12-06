using System.ComponentModel.DataAnnotations;

namespace mamma_shopping_helper.DTOs
{
    
    public class UpdateProdottoDto
    {
        [Required(ErrorMessage = "Il nome è obbligatorio")]
        [MaxLength(200)]
        public string Nome { get; set; }

        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "La quantità deve essere >= 1")]
        public int Quantita { get; set; }

        [Required(ErrorMessage = "Il nome utente è obbligatorio")]
        [MaxLength(100)]
        public string UserName { get; set; }

        [Required]
        public bool Acquistato { get; set; }

       
    }
}
