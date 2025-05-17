using backend.Models;
using backend.DTOs;
using backend.Repositories;
using System.Threading.Tasks;

namespace backend.Business
{
    public class UsuarioBusiness
    {
        private readonly UsuarioRepository _usuarioRepository;

        public UsuarioBusiness(UsuarioRepository usuarioRepository)
        {
            _usuarioRepository = usuarioRepository;
        }

        public async Task<bool> CadastrarUsuarioAsync(UsuarioDTO usuarioDTO)
        {
            var usuarioExistente = await _usuarioRepository.VerificarUsuarioExistentePorNome(usuarioDTO.NomeUsuario);
            if (usuarioExistente)
                return false; // Já existe

            var novoUsuario = new Usuario
            {
                NomeUsuario = usuarioDTO.NomeUsuario,
                SenhaHash = BCrypt.Net.BCrypt.HashPassword(usuarioDTO.Senha); //aplicar hash para criptografia
            };

            await _usuarioRepository.CriarUsuario(novoUsuario);
            return true;
        }

        public async Task<Usuario?> ValidarLoginAsync(UsuarioDTO usuarioDTO)
        {
            //Busca o usuário no banco pelo nome
            var usuarioDb = await _usuarioRepository.LoginUsuario(usuarioDTO.NomeUsuario);
            if (usuarioDb == null)
                return null; // usuário não encontrado

            //Valida a senha usando BCrypt, comparando senha em texto com o hash salvo
            bool senhaValida = BCrypt.Net.BCrypt.Verify(usuarioDTO.Senha, usuarioDb.SenhaHash);
            if (!senhaValida)
                return null; // senha inválida

            //Retorna o usuário válido
            return usuarioDb;
        }
    }
}
