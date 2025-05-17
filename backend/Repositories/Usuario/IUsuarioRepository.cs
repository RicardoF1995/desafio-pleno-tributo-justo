using backend.Models;

namespace backend.Repositories
{
    public interface IUsuarioRepository
    {
        Task CriarUsuario(Usuario usuario);
        Task<bool> VerificarUsuarioExistentePorNome(string nomeUsuario);
        Task<Usuario?> LoginUsuario(string nomeUsuario);
    }
}
