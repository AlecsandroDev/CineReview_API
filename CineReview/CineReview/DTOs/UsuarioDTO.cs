using System.ComponentModel.DataAnnotations;

namespace CineReview.DTOs
{
    public class CriarUsuarioDTO
    {
        [Required(ErrorMessage = "O nome é obrigatório")]
        public string NomeUsuario { get; set; }

        [Required(ErrorMessage = "O email é obrigatório")]
        [EmailAddress(ErrorMessage = "Email inválido")]
        public string Email { get; set; }

        [Required(ErrorMessage = "A senha é obrigatória")]
        [MinLength(6, ErrorMessage = "A senha deve ter no mínimo 6 caracteres")]
        public string Senha { get; set; }
    }

    public class LoginUsuarioDTO
    {
        [Required] public string Email { get; set; }
        [Required] public string Senha { get; set; }
    }

    public class UsuarioRespostaDTO
    {
        public Guid Id { get; set; }
        public string NomeUsuario { get; set; }
        public string Email { get; set; }
    }

    public class AtualizarUsuarioDTO
    {
        [Required] public string NomeUsuario { get; set; }
        [Required][EmailAddress] public string Email { get; set; }
    }
}