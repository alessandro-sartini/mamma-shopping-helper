using mamma_shopping_helper.Model;

namespace mamma_shopping_helper.Service
{
    public interface IProdottoService
    {
        //Base
        Task<IEnumerable<Prodotto>> GetAllProdottiAsync();

        Task<Prodotto?> GetProdottoByIdAsync(int id);

        Task<Prodotto> CreateProdottoAsync(Prodotto prodotto);  

        Task<bool> UpdateProdottoAsync(int id, Prodotto prodotto);

        Task<bool> DeleteProdottoAsync(int id);


        Task<IEnumerable<Prodotto>> GetProdottiByListaIdAsync(int listaId);

        // prodotto come acquistato/non acquistato
        Task<bool> ToggleAcquistatoAsync(int id);

        // Incrementa quantità 
        Task<bool> IncrementaQuantitaAsync(int id, int quantita = 1);

        //  Decrementa quantità
        Task<bool> DecrementaQuantitaAsync(int id, int quantita = 1);

        //Filtra prodotti per utente
        Task<IEnumerable<Prodotto>> GetProdottiByUserNameAsync(string userName);

    }
}
