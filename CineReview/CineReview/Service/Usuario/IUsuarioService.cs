using CineReview.DTOs;
using CineReview.Models;

namespace CineReview.Service.Usuario
{
    public interface IUsuarioService
    {
        UsuarioRespostaDTO Cadastrar(CriarUsuarioDTO dto);
        UsuarioRespostaDTO Login(LoginUsuarioDTO dto);
        List<UsuarioRespostaDTO> ListarTodos();
        void Atualizar(Guid id, AtualizarUsuarioDTO dto);
        void Deletar(Guid id);
    }
}
