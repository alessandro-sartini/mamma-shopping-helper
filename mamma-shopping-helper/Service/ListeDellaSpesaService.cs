using mamma_shopping_helper.Data;
using mamma_shopping_helper.Model;
using Microsoft.EntityFrameworkCore;

namespace mamma_shopping_helper.Service
{
    public class ListeDellaSpesaService : IListeDellaSpesaService
    {
        private readonly MammaDbContext _context;

        public ListeDellaSpesaService(MammaDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<ListaDellaSpesa>> GetAllListeAsync(
         string? orderBy = "DataCreazione",
         bool ascending = false,
         string? creataDa = null,
         bool? conclusa = null) 
        {
            var query = _context.ListeDellaSpesa
                .Include(l => l.Prodotti)
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(creataDa))
            {
                query = query.Where(l => l.CreataDa == creataDa);
            }

            if (conclusa.HasValue)
            {
                query = query.Where(l => l.Conclusa == conclusa.Value);
            }

            query = orderBy?.ToLower() switch
            {
                "datacreazione" => ascending
                    ? query.OrderBy(l => l.DataCreazione)
                    : query.OrderByDescending(l => l.DataCreazione),

                "dataultimamodifica" => ascending
                    ? query.OrderBy(l => l.DataUltimaModifica)
                    : query.OrderByDescending(l => l.DataUltimaModifica),

                "titolo" => ascending
                    ? query.OrderBy(l => l.Titolo)
                    : query.OrderByDescending(l => l.Titolo),

                _ => query.OrderByDescending(l => l.DataCreazione)
            };

            return await query.ToListAsync();
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
            lista.DataUltimaModifica = DateTime.Now;
            lista.Conclusa = false;

            // Se CreataDa vuoto, imposta Guest
            if (string.IsNullOrWhiteSpace(lista.CreataDa))
            {
                lista.CreataDa = "Guest";
            }

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
            listaEsistente.DataUltimaModifica = DateTime.Now; 
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
            lista.DataUltimaModifica = DateTime.Now; 

            await _context.SaveChangesAsync();

            return true;
        }
    }
}
