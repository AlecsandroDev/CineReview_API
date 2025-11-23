using CineReview.DTOs;

namespace CineReview.Services
{
    public interface IEquipeService
    {
        EquipeRespostaDto AdicionarAtor(CriarAtorDto dto);
        EquipeRespostaDto AdicionarTecnico(CriarTecnicoDto dto);
        List<EquipeRespostaDto> ListarPorMidia(Guid midiaId);
        List<EquipeRespostaDto> ListarPorTemporada(Guid temporadaId);
    }
}