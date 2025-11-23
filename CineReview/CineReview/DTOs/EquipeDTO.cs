using System.ComponentModel.DataAnnotations;

namespace CineReview.DTOs
{
    public class CriarMembroEquipeDto
    {
        public Guid? MidiaId { get; set; }
        public Guid? TemporadaId { get; set; }

        [Required] public string NomeCompleto { get; set; }
        [Required] public List<string> Funcoes { get; set; }
    }

    public class CriarAtorDto : CriarMembroEquipeDto
    {
        [Required] public string Papel { get; set; }
    }

    public class CriarTecnicoDto : CriarMembroEquipeDto
    {
    }

    public class EquipeRespostaDto
    {
        public Guid Id { get; set; }
        public string Tipo { get; set; }
        public string NomeCompleto { get; set; }
        public List<string> Funcoes { get; set; }
        public string? Papel { get; set; }
    }
}