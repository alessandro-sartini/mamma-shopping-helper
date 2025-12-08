using mamma_shopping_helper.Data;
using mamma_shopping_helper.Model;
using mamma_shopping_helper.Service;
using Microsoft.EntityFrameworkCore;

namespace mamma_shopping_helper.Service
{
    public class ProdottoService : IProdottoService
    {
        private readonly MammaDbContext _context;

        public ProdottoService(MammaDbContext context)
        {
            _context = context;
        }

        // prodotti
        public async Task<IEnumerable<Prodotto>> GetAllProdottiAsync()
        {
            return await _context.Prodotti
                .AsNoTracking()
                .OrderByDescending(p => p.DataAggiunta)
                .ToListAsync();
        }

        //prodotto per ID
        public async Task<Prodotto?> GetProdottoByIdAsync(int id)
        {
            return await _context.Prodotti
                .Include(p => p.ListaDellaSpesa)  
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        //Prodotti di una lista specifica
        public async Task<IEnumerable<Prodotto>> GetProdottiByListaIdAsync(int listaId)
        {
            return await _context.Prodotti
                .AsNoTracking()
                .Where(p => p.ListaDellaSpesaId == listaId)
                .OrderBy(p => p.Acquistato)      
                .ThenBy(p => p.Nome)            
                .ToListAsync();
        }

        // Prodotti di un utente specifico
        public async Task<IEnumerable<Prodotto>> GetProdottiByUserNameAsync(string userName)
        {
            return await _context.Prodotti
                .AsNoTracking()
                .Where(p => p.UserName == userName)
                .OrderByDescending(p => p.DataAggiunta)
                .ToListAsync();
        }

        //Crea nuovo prodotto
        public async Task<Prodotto> CreateProdottoAsync(Prodotto prodotto)
        {
            // Verifica che la lista esista
            var listaEsiste = await _context.ListeDellaSpesa
                .AnyAsync(l => l.Id == prodotto.ListaDellaSpesaId);

            if (!listaEsiste)
                throw new ArgumentException($"Lista con ID {prodotto.ListaDellaSpesaId} non trovata");

            // Imposta data aggiunta
            prodotto.DataAggiunta = DateTime.Now;
            prodotto.Acquistato = false;

            _context.Prodotti.Add(prodotto);
            await _context.SaveChangesAsync();

            return prodotto;
        }

        //  Aggiorna prodotto
        public async Task<bool> UpdateProdottoAsync(int id, Prodotto prodotto)
        {
            var prodottoEsistente = await _context.Prodotti.FindAsync(id);
            if (prodottoEsistente == null)
                return false;

            // Aggiorna solo i campi modificabili
            prodottoEsistente.Nome = prodotto.Nome;
            prodottoEsistente.Quantita = prodotto.Quantita;
            prodottoEsistente.UserName = prodotto.UserName;
            prodottoEsistente.Acquistato = prodotto.Acquistato;

            await _context.SaveChangesAsync();
            return true;
        }

        // Elimina prodotto
        public async Task<bool> DeleteProdottoAsync(int id)
        {
            var prodotto = await _context.Prodotti.FindAsync(id);
            if (prodotto == null)
                return false;

            _context.Prodotti.Remove(prodotto);
            await _context.SaveChangesAsync();
            return true;
        }

        //  Toggle acquistato
        public async Task<bool> ToggleAcquistatoAsync(int id)
        {
            var prodotto = await _context.Prodotti.FindAsync(id);

            if (prodotto == null)
                return false;

            prodotto.Acquistato = !prodotto.Acquistato;

            await _context.SaveChangesAsync();

            await CheckAndCompleteLista(prodotto.ListaDellaSpesaId);

            return true;
        }

        // autocompleto la lista se tutti prodotti comprati
        private async Task CheckAndCompleteLista(int listaId)
        {
            var lista = await _context.ListeDellaSpesa
                .Include(l => l.Prodotti)
                .FirstOrDefaultAsync(l => l.Id == listaId);

            if (lista == null || lista.Prodotti.Count == 0)
                return;

            // Controlla se TUTTI sono acquistati
            bool tuttiAcquistati = lista.Prodotti.All(p => p.Acquistato);

            if (tuttiAcquistati && !lista.Conclusa)
            {
                lista.Conclusa = true;
                await _context.SaveChangesAsync();
            }
            else if (!tuttiAcquistati && lista.Conclusa)
            {
                lista.Conclusa = false;
                await _context.SaveChangesAsync();
            }
        }

        // Incrementa quantità
        public async Task<bool> IncrementaQuantitaAsync(int id, int quantita = 1)
        {
            if (quantita <= 0)
                return false;

            var prodotto = await _context.Prodotti.FindAsync(id);
            if (prodotto == null)
                return false;

            prodotto.Quantita += quantita;
            await _context.SaveChangesAsync();
            return true;
        }

        //  Decrementa quantità
        public async Task<bool> DecrementaQuantitaAsync(int id, int quantita = 1)
        {
            if (quantita <= 0)
                return false;

            var prodotto = await _context.Prodotti.FindAsync(id);
            if (prodotto == null)
                return false;

            // No sotto 1
            prodotto.Quantita = Math.Max(1, prodotto.Quantita - quantita);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
