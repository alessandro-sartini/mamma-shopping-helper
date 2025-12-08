using mamma_shopping_helper.Model;

namespace mamma_shopping_helper.Service
{
    public interface IListeDellaSpesaService
    {
        Task<IEnumerable<ListaDellaSpesa>> GetAllListeAsync(
            string? orderBy = "DataCreazione",
            bool ascending = false,
            string? creataDa = null,
            bool? conclusa = null);

        Task<ListaDellaSpesa?> GetListaByIdAsync(int id);
        Task<ListaDellaSpesa> CreateListaAsync(ListaDellaSpesa lista);
        Task<bool> UpdateListaAsync(int id, ListaDellaSpesa lista);
        Task<bool> DeleteListaAsync(int id);
        Task<bool> ToggleConclusaAsync(int id);
    }
}
