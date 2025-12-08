using System.ComponentModel.DataAnnotations;

namespace mamma_shopping_helper.Model
{
    public class ListaDellaSpesa
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Il titolo è obbligatorio")]
        [MaxLength(200, ErrorMessage = "Il titolo non può superare 200 caratteri")]
        public string Titolo { get; set; }

        [MaxLength(500, ErrorMessage = "La descrizione non può superare 500 caratteri")]
        public string? Descrizione { get; set; }

        [Required]
        public DateTime DataCreazione { get; set; } = DateTime.Now;

        public bool Conclusa { get; set; } = false;

        public DateTime DataUltimaModifica { get; set; }

        [Required]
        [MaxLength(100)]
        public string CreataDa { get; set; } = string.Empty;
        public ICollection<Prodotto> Prodotti { get; set; } = [];
    }
}