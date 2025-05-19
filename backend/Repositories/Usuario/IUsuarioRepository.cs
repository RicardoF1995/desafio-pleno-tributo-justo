using backend.Models;

namespace backend.Repositories
{
    public interface IUsuarioRepository
    {
        Task CadastrarUsuarioAsync(Usuario usuario);
        Task<bool> VerificarUsuarioExistentePorNomeAsync(string nomeUsuario);
        Task<Usuario?> LoginUsuarioAsync(string nomeUsuario);
    }
}
