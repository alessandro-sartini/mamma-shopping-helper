using mamma_shopping_helper.Model;

namespace mamma_shopping_helper.Data
{
    public static class DbSeeder
    {
        public static void SeedData(MammaDbContext context)
        {
            // Verifica se ci sono già dati
            if (context.ListeDellaSpesa.Any())
            {
                return; // Il database contiene già dati, non fare nulla
            }

            // Crea le liste della spesa
            var liste = new List<ListaDellaSpesa>
            {
                new ListaDellaSpesa
                {
                    Titolo = "Spesa settimanale famiglia",
                    Descrizione = "Lista principale per la spesa della settimana: frutta, verdura, carne e latticini",
                    DataCreazione = DateTime.Now.AddDays(-5),
                    Conclusa = false,
                    CreataDa = "Maria",
                    DataUltimaModifica = DateTime.Now.AddDays(-3)
                },
                new ListaDellaSpesa
                {
                    Titolo = "Preparazione cena di sabato",
                    Descrizione = "Ingredienti per la lasagna e il tiramisù per la cena con gli amici",
                    DataCreazione = DateTime.Now.AddDays(-3),
                    Conclusa = false,
                    CreataDa = "Paolo",
                    DataUltimaModifica = DateTime.Now.AddDays(-2)
                },
                new ListaDellaSpesa
                {
                    Titolo = "Colazione e merenda bambini",
                    Descrizione = "Biscotti, cereali, yogurt e frutta per colazione e merende scolastiche",
                    DataCreazione = DateTime.Now.AddDays(-2),
                    Conclusa = false,
                    CreataDa = "Anna",
                    DataUltimaModifica = DateTime.Now.AddDays(-1)
                },
                new ListaDellaSpesa
                {
                    Titolo = "Spesa completata - Novembre",
                    Descrizione = "Spesa mensile completata il mese scorso",
                    DataCreazione = DateTime.Now.AddDays(-30),
                    Conclusa = true,
                    CreataDa = "Elena",
                    DataUltimaModifica = DateTime.Now.AddDays(-28)
                },
                new ListaDellaSpesa
                {
                    Titolo = "Ingredienti pizza fatta in casa",
                    Descrizione = "Farina, lievito, mozzarella, pomodoro e ingredienti vari per pizza casalinga",
                    DataCreazione = DateTime.Now.AddDays(-1),
                    Conclusa = false,
                    CreataDa = "Davide",
                    DataUltimaModifica = DateTime.Now.AddHours(-6)
                }
            };

            context.ListeDellaSpesa.AddRange(liste);
            context.SaveChanges();

            // Prodotti per Lista 1
            var prodottiLista1 = new List<Prodotto>
            {
                new () { Nome = "Latte intero 1L", Quantita = 3, UserName = "Maria", DataAggiunta = DateTime.Now.AddDays(-5), Acquistato = false, ListaDellaSpesaId = liste[0].Id },
                new () { Nome = "Pane integrale", Quantita = 2, UserName = "Marco", DataAggiunta = DateTime.Now.AddDays(-5), Acquistato = false, ListaDellaSpesaId = liste[0].Id },
                new () { Nome = "Pomodori da insalata", Quantita = 1, UserName = "Maria", DataAggiunta = DateTime.Now.AddDays(-4), Acquistato = false, ListaDellaSpesaId = liste[0].Id },
                new () { Nome = "Mele Golden 1kg", Quantita = 2, UserName = "Luca", DataAggiunta = DateTime.Now.AddDays(-4), Acquistato = false, ListaDellaSpesaId = liste[0].Id },
                new () { Nome = "Banane", Quantita = 1, UserName = "Maria", DataAggiunta = DateTime.Now.AddDays(-4), Acquistato = false, ListaDellaSpesaId = liste[0].Id },
                new () { Nome = "Petto di pollo 500g", Quantita = 2, UserName = "Marco", DataAggiunta = DateTime.Now.AddDays(-5), Acquistato = false, ListaDellaSpesaId = liste[0].Id },
                new () { Nome = "Pasta penne 500g", Quantita = 4, UserName = "Maria", DataAggiunta = DateTime.Now.AddDays(-3), Acquistato = true, ListaDellaSpesaId = liste[0].Id },
                new () { Nome = "Olio extravergine oliva", Quantita = 1, UserName = "Marco", DataAggiunta = DateTime.Now.AddDays(-3), Acquistato = false, ListaDellaSpesaId = liste[0].Id },
                new () { Nome = "Zucchine fresche", Quantita = 1, UserName = "Luca", DataAggiunta = DateTime.Now.AddDays(-4), Acquistato = false, ListaDellaSpesaId = liste[0].Id },
                new () { Nome = "Yogurt greco naturale", Quantita = 6, UserName = "Maria", DataAggiunta = DateTime.Now.AddDays(-5), Acquistato = true, ListaDellaSpesaId = liste[0].Id }
            };

            // Prodotti per Lista 2
            var prodottiLista2 = new List<Prodotto>
            {
                new () { Nome = "Sfoglie lasagne fresche", Quantita = 2, UserName = "Sara", DataAggiunta = DateTime.Now.AddDays(-3), Acquistato = false, ListaDellaSpesaId = liste[1].Id },
                new () { Nome = "Carne macinata 500g", Quantita = 1, UserName = "Paolo", DataAggiunta = DateTime.Now.AddDays(-3), Acquistato = false, ListaDellaSpesaId = liste[1].Id },
                new () { Nome = "Passata di pomodoro 700g", Quantita = 2, UserName = "Sara", DataAggiunta = DateTime.Now.AddDays(-3), Acquistato = false, ListaDellaSpesaId = liste[1].Id },
                new () { Nome = "Besciamella", Quantita = 1, UserName = "Paolo", DataAggiunta = DateTime.Now.AddDays(-3), Acquistato = false, ListaDellaSpesaId = liste[1].Id },
                new () { Nome = "Parmigiano grattugiato", Quantita = 1, UserName = "Sara", DataAggiunta = DateTime.Now.AddDays(-3), Acquistato = true, ListaDellaSpesaId = liste[1].Id },
                new () { Nome = "Mascarpone 250g", Quantita = 2, UserName = "Paolo", DataAggiunta = DateTime.Now.AddDays(-2), Acquistato = false, ListaDellaSpesaId = liste[1].Id },
                new () { Nome = "Savoiardi", Quantita = 1, UserName = "Sara", DataAggiunta = DateTime.Now.AddDays(-2), Acquistato = false, ListaDellaSpesaId = liste[1].Id },
                new () { Nome = "Caffè espresso", Quantita = 1, UserName = "Paolo", DataAggiunta = DateTime.Now.AddDays(-2), Acquistato = false, ListaDellaSpesaId = liste[1].Id },
                new () { Nome = "Cacao amaro in polvere", Quantita = 1, UserName = "Sara", DataAggiunta = DateTime.Now.AddDays(-2), Acquistato = false, ListaDellaSpesaId = liste[1].Id },
                new () { Nome = "Uova fresche x6", Quantita = 1, UserName = "Paolo", DataAggiunta = DateTime.Now.AddDays(-3), Acquistato = true, ListaDellaSpesaId = liste[1].Id }
            };

            // Prodotti per Lista 3
            var prodottiLista3 = new List<Prodotto>
            {
                new () { Nome = "Biscotti integrali", Quantita = 3, UserName = "Anna", DataAggiunta = DateTime.Now.AddDays(-2), Acquistato = false, ListaDellaSpesaId = liste[2].Id },
                new () { Nome = "Cereali corn flakes", Quantita = 2, UserName = "Giuseppe", DataAggiunta = DateTime.Now.AddDays(-2), Acquistato = false, ListaDellaSpesaId = liste[2].Id },
                new () { Nome = "Yogurt alla fragola x4", Quantita = 2, UserName = "Anna", DataAggiunta = DateTime.Now.AddDays(-2), Acquistato = false, ListaDellaSpesaId = liste[2].Id },
                new () { Nome = "Succo di frutta ACE 1L", Quantita = 3, UserName = "Giuseppe", DataAggiunta = DateTime.Now.AddDays(-1), Acquistato = false, ListaDellaSpesaId = liste[2].Id },
                new () { Nome = "Marmellata albicocche", Quantita = 1, UserName = "Anna", DataAggiunta = DateTime.Now.AddDays(-2), Acquistato = false, ListaDellaSpesaId = liste[2].Id },
                new () { Nome = "Nutella 400g", Quantita = 1, UserName = "Giuseppe", DataAggiunta = DateTime.Now.AddDays(-1), Acquistato = true, ListaDellaSpesaId = liste[2].Id },
                new () { Nome = "Fette biscottate", Quantita = 2, UserName = "Anna", DataAggiunta = DateTime.Now.AddDays(-2), Acquistato = false, ListaDellaSpesaId = liste[2].Id },
                new () { Nome = "Miele millefiori", Quantita = 1, UserName = "Giuseppe", DataAggiunta = DateTime.Now.AddDays(-2), Acquistato = false, ListaDellaSpesaId = liste[2].Id }
            };

            // Prodotti per Lista 4 (completata)
            var prodottiLista4 = new List<Prodotto>
            {
                new() { Nome = "Detersivo piatti", Quantita = 2, UserName = "Elena", DataAggiunta = DateTime.Now.AddDays(-30), Acquistato = true, ListaDellaSpesaId = liste[3].Id },
                new () { Nome = "Carta igienica 12 rotoli", Quantita = 1, UserName = "Roberto", DataAggiunta = DateTime.Now.AddDays(-30), Acquistato = true, ListaDellaSpesaId = liste[3].Id },
                new () { Nome = "Detersivo lavatrice 3L", Quantita = 1, UserName = "Elena", DataAggiunta = DateTime.Now.AddDays(-30), Acquistato = true, ListaDellaSpesaId = liste[3].Id },
                new () { Nome = "Ammorbidente", Quantita = 1, UserName = "Roberto", DataAggiunta = DateTime.Now.AddDays(-30), Acquistato = true, ListaDellaSpesaId = liste[3].Id },
                new () { Nome = "Spugne cucina x10", Quantita = 1, UserName = "Elena", DataAggiunta = DateTime.Now.AddDays(-30), Acquistato = true, ListaDellaSpesaId = liste[3].Id }
            };

            // Prodotti per Lista 5
            var prodottiLista5 = new List<Prodotto>
            {
                new () { Nome = "Farina 00 1kg", Quantita = 2, UserName = "Davide", DataAggiunta = DateTime.Now.AddDays(-1), Acquistato = false, ListaDellaSpesaId = liste[4].Id },
                new () { Nome = "Lievito di birra fresco", Quantita = 2, UserName = "Francesca", DataAggiunta = DateTime.Now.AddDays(-1), Acquistato = false, ListaDellaSpesaId = liste[4].Id },
                new () { Nome = "Mozzarella fior di latte", Quantita = 3, UserName = "Davide", DataAggiunta = DateTime.Now.AddDays(-1), Acquistato = false, ListaDellaSpesaId = liste[4].Id },
                new () { Nome = "Pomodori pelati 400g", Quantita = 2, UserName = "Francesca", DataAggiunta = DateTime.Now.AddDays(-1), Acquistato = false, ListaDellaSpesaId = liste[4].Id },
                new () { Nome = "Basilico fresco", Quantita = 1, UserName = "Davide", DataAggiunta = DateTime.Now.AddHours(-12), Acquistato = false, ListaDellaSpesaId = liste[4].Id },
                new () { Nome = "Prosciutto cotto", Quantita = 1, UserName = "Francesca", DataAggiunta = DateTime.Now.AddHours(-10), Acquistato = false, ListaDellaSpesaId = liste[4].Id },
                new () { Nome = "Funghi champignon", Quantita = 1, UserName = "Davide", DataAggiunta = DateTime.Now.AddHours(-10), Acquistato = false, ListaDellaSpesaId = liste[4].Id },
                new () { Nome = "Salamino piccante", Quantita = 1, UserName = "Francesca", DataAggiunta = DateTime.Now.AddHours(-8), Acquistato = false, ListaDellaSpesaId = liste[4].Id },
                new () { Nome = "Olive nere denocciolate", Quantita = 1, UserName = "Davide", DataAggiunta = DateTime.Now.AddHours(-8), Acquistato = false, ListaDellaSpesaId = liste[4].Id }
            };

            // Aggiungi tutti i prodotti
            context.Prodotti.AddRange(prodottiLista1);
            context.Prodotti.AddRange(prodottiLista2);
            context.Prodotti.AddRange(prodottiLista3);
            context.Prodotti.AddRange(prodottiLista4);
            context.Prodotti.AddRange(prodottiLista5);

            context.SaveChanges();
        }
    }
}
