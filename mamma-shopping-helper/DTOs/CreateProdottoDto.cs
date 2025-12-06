using System.ComponentModel.DataAnnotations;

namespace mamma_shopping_helper.DTOs
{
    public class CreateProdottoDto
    {

        [Required(ErrorMessage = "Il nome del prodotto è obbligatorio")]
        [MaxLength(200, ErrorMessage = "Il nome non può superare 200 caratteri")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "La quantità è obbligatoria")]
        [Range(1, int.MaxValue, ErrorMessage = "La quantità deve essere almeno 1")]
        public int Quantita { get; set; }

        [Required(ErrorMessage = "Il nome utente è obbligatorio")]
        [MaxLength(100, ErrorMessage = "Il nome utente non può superare 100 caratteri")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "L'ID della lista è obbligatorio")]
        public int ListaDellaSpesaId { get; set; }


    }
}