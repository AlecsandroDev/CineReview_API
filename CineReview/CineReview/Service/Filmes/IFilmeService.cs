using CineReview.DTOs;

namespace CineReview.Services
{
    public interface IFilmeService
    {
        FilmeRespostaDto Cadastrar(CriarFilmeDto dto);
        List<FilmeRespostaDto> ListarTodos();
        FilmeRespostaDto BuscarPorId(Guid id);
        void Atualizar(Guid id, CriarFilmeDto dto);
        void Deletar(Guid id);
    }
}