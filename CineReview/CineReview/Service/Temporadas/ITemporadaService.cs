using CineReview.DTOs;

namespace CineReview.Services
{
    public interface ITemporadaService
    {
        TemporadaRespostaDto CadastrarTemporada(CriarTemporadaDto dto);
        void AdicionarEpisodio(CriarEpisodioDto dto);
        List<TemporadaRespostaDto> ListarPorSerie(Guid serieId);
    }
}