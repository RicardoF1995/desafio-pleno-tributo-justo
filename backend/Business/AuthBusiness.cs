using backend.Models;
using backend.DTOs;
using backend.Repositories;
using System.Threading.Tasks;

namespace backend.Business
{
    public class AuthBusiness
    {
        private readonly IUsuarioRepository _usuarioRepository;

        public AuthBusiness(IUsuarioRepository usuarioRepository)
        {
            _usuarioRepository = usuarioRepository;
        }

        public async Task<bool> CadastrarUsuarioAsync(UsuarioDTO usuarioDTO)
        {
            var usuarioExistente = await _usuarioRepository.VerificarUsuarioExistentePorNomeAsync(usuarioDTO.NomeUsuario);
            if (usuarioExistente)
                return false;

            var novoUsuario = new Usuario
            {
                NomeUsuario = usuarioDTO.NomeUsuario,
                SenhaHash = BCrypt.Net.BCrypt.HashPassword(usuarioDTO.Senha)
            };

            await _usuarioRepository.CadastrarUsuarioAsync(novoUsuario);
            return true;
        }

        public async Task<Usuario?> ValidarLoginAsync(UsuarioDTO usuarioDTO)
        {
            var usuarioDb = await _usuarioRepository.LoginUsuarioAsync(usuarioDTO.NomeUsuario);
            if (usuarioDb == null)
                return null;

            bool senhaValida = BCrypt.Net.BCrypt.Verify(usuarioDTO.Senha, usuarioDb.SenhaHash);
            if (!senhaValida)
                return null;

            return usuarioDb;
        }
    }
}
