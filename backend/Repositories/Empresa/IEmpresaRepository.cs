using backend.Models;

namespace backend.Repositories
{
    public interface IEmpresaRepository
    {
        Task<List<Empresa>> BuscarTodasEmpresasAsync();
        Task<int> InserirEmpresaAsync(Empresa empresa);
        Task<Empresa?> BuscarEmpresaPorCnpjAsync(string cnpj);
        Task<bool> VerificarEmpresaExistentePorCnpjAsync(string cnpj);
    }
}
