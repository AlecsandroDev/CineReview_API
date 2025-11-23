using System.ComponentModel.DataAnnotations;

namespace CineReview.DTOs
{
    public class CriarFilmeDto
    {
        [Required] public string Titulo { get; set; }
        [Required] public string Genero { get; set; }
        [Required] public string Sinopse { get; set; }
        [Required] public TimeSpan Duracao { get; set; }
        [Required] public string ClassificacaoIndicativa { get; set; }
        [Required] public DateOnly DataLancamento { get; set; }
    }

    public class FilmeRespostaDto
    {
        public Guid Id { get; set; }
        public string Titulo { get; set; }
        public string Genero { get; set; }
        public double NotaMediaGeral { get; set; }
    }
}