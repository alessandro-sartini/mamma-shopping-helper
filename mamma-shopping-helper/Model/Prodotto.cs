using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace mamma_shopping_helper.Model
{
    public class Prodotto
    {

        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Il nome del prodotto è obbligatorio")]
        [MaxLength(200, ErrorMessage = "Il nome del prodotto non può superare 200 caratteri")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "quantita` obbligatoria")]
        [Range(1, int.MaxValue, ErrorMessage = "La quantità deve essere almeno 1")]
        public int Quantita { get; set; }


        [Required(ErrorMessage = "Il nome dell'utente e obbligatorio")]
        [MaxLength(100, ErrorMessage = "Il nome utente non può superare 100 caratteri")]
        public string UserName { get; set; }

        [Required]
        public DateTime DataAggiunta { get; set; } = DateTime.Now;

        public bool Acquistato { get; set; } = false;

        [Required]
        public int ListaDellaSpesaId { get; set; }

        [ForeignKey("ListaDellaSpesaId")]
        [JsonIgnore]
        public ListaDellaSpesa ListaDellaSpesa { get; set; }
    }
}
