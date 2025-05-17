using System.ComponentModel.DataAnnotations;

namespace backend.DTOs
{
    public class UsuarioDTO
    {
        [Required(ErrorMessage = "O nome do usuário é obrigatório.")]
        [StringLength(50, MinimumLength = 5, ErrorMessage = "O nome deve ter entre 5 e 50 caracteres.")]
        public string NomeUsuario { get; set; }

        [Required(ErrorMessage = "A senha é obrigatória.")]
        [StringLength(100, MinimumLength = 5, ErrorMessage = "A senha deve ter pelo menos 5 caracteres.")]
        public string Senha { get; set; }
    }
}
