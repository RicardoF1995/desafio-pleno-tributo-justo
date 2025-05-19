using backend.Models;

namespace backend.Repositories
{
    public interface IItemNotaRepository
    {
        Task InserirItemNotaAsync(ItemNota itemNota, int notaFiscalId);
        Task<List<ItemNota>> BuscarItensPorNotaFiscalAsync(int notaFiscalId);
    }
}
