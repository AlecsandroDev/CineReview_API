using System.ComponentModel.DataAnnotations;

namespace CineReview.DTOs
{
    public class CriarEpisodioDto
    {
        [Required] public Guid TemporadaId { get; set; }
        [Required] public int NumeroEpisodio { get; set; }
        [Required] public string Titulo { get; set; }
        [Required] public string Sinopse { get; set; }
        [Required] public TimeSpan Duracao { get; set; }
    }
}