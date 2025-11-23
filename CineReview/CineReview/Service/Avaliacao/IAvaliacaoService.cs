using CineReview.DTOs;

namespace CineReview.Service.Avaliacao
{
    public interface IAvaliacaoService
    {
        AvaliacaoRespostaDto Avaliar(CriarAvaliacaoDto dto);
        List<AvaliacaoRespostaDto> ListarTodos();
        List<AvaliacaoRespostaDto> ListarPorMidia(Guid midiaId);
        void Deletar(Guid id);
    }
}
