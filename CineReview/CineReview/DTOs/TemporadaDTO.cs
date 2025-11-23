using System.ComponentModel.DataAnnotations;

namespace CineReview.DTOs
{
    public class CriarTemporadaDto
    {
        [Required] public Guid SerieId { get; set; }
        [Required] public int NumeroTemporada { get; set; }
        [Required] public string Titulo { get; set; }
        [Required] public string Sinopse { get; set; }
        [Required] public string ClassificacaoIndicativa { get; set; }
        [Required] public DateOnly DataLancamento { get; set; }
    }

    public class TemporadaRespostaDto
    {
        public Guid Id { get; set; }
        public string Titulo { get; set; }
        public int Numero { get; set; }
        public double NotaMedia { get; set; }
    }
}