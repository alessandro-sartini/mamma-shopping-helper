using Microsoft.EntityFrameworkCore;
using mamma_shopping_helper.Data;
using mamma_shopping_helper.Model;
using mamma_shopping_helper.DTOs;

namespace mamma_shopping_helper.Service
{
    public class ListeDellaSpesaService : IListeDellaSpesaService
    {
        private readonly MammaDbContext _context;

        public ListeDellaSpesaService(MammaDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<ListaDellaSpesa>> GetAllListeAsync()
        {
            return await _context.ListeDellaSpesa
                .OrderByDescending(l => l.DataCreazione)
                .ToListAsync();
        }

        public async Task<ListaDellaSpesa?> GetListaByIdAsync(int id)
        {
            return await _context.ListeDellaSpesa
                .Include(l => l.Prodotti)
                .FirstOrDefaultAsync(l => l.Id == id);
        }

        public async Task<ListaDellaSpesa> CreateListaAsync(ListaDellaSpesa lista)
        {
            lista.DataCreazione = DateTime.Now;
            lista.Conclusa = false;

            _context.ListeDellaSpesa.Add(lista);
            await _context.SaveChangesAsync();

            return lista;
        }

        public async Task<bool> UpdateListaAsync(int id, ListaDellaSpesa lista)
        {
            var listaEsistente = await _context.ListeDellaSpesa.FindAsync(id);
            if (listaEsistente == null)
                return false;

            listaEsistente.Titolo = lista.Titolo;
            listaEsistente.Descrizione = lista.Descrizione;
            listaEsistente.Conclusa = lista.Conclusa;

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteListaAsync(int id)
        {
            var lista = await _context.ListeDellaSpesa.FindAsync(id);
            if (lista == null)
                return false;

            _context.ListeDellaSpesa.Remove(lista);
            await _context.SaveChangesAsync();
            return true;
        }

     
        public async Task<bool> ToggleConclusaAsync(int id)
        {
        
            var lista = await _context.ListeDellaSpesa.FindAsync(id);

            if (lista == null)
                return false;

            lista.Conclusa = !lista.Conclusa;

            await _context.SaveChangesAsync();
            return true;
        }

      
    }
}
