using backend.Models;

namespace backend.Repositories
{
    public interface INotaFiscalRepository
    {
        Task<int> InserirNotaFiscalAsync(int empresaId, NotaFiscal nota);
        Task<List<NotaFiscal>> BuscarNotasPorEmpresaAsync(int empresaId);
    }
}
