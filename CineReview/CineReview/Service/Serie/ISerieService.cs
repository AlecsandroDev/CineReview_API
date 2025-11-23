using CineReview.DTOs;

namespace CineReview.Services
{
    public interface ISerieService
    {
        SerieRespostaDto Cadastrar(CriarSerieDto dto);
        List<SerieRespostaDto> ListarTodas();
        SerieRespostaDto BuscarPorId(Guid id);
        void Atualizar(Guid id, CriarSerieDto dto);
        void Deletar(Guid id);
    }
}