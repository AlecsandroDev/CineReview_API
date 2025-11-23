using System.ComponentModel.DataAnnotations;

namespace CineReview.DTOs
{
    public class CriarAvaliacaoDto
    {
        [Required]
        public Guid UsuarioId { get; set; } // Quem está avaliando

        [Required]
        public Guid AvaliadoId { get; set; } // ID do Filme ou da Temporada

        // --- Notas Narrativas ---
        [Range(0, 10)] public int NotaTrama { get; set; }
        [Range(0, 10)] public int NotaRitmo { get; set; }
        [Range(0, 10)] public int NotaDevPersonagens { get; set; }
        [Range(0, 10)] public int NotaConstrucaoMundo { get; set; }
        [Range(0, 10)] public int NotaTematica { get; set; }

        // --- Notas Execução ---
        [Range(0, 10)] public int NotaAtuacao { get; set; }
        [Range(0, 10)] public int NotaEdicao { get; set; }
        [Range(0, 10)] public int NotaDirecao { get; set; }

        // --- Notas Visuais ---
        [Range(0, 10)] public int NotaArte { get; set; }
        [Range(0, 10)] public int NotaCinematografia { get; set; }
        [Range(0, 10)] public int NotaCenarios { get; set; }
        [Range(0, 10)] public int NotaFigurinos { get; set; }
        [Range(0, 10)] public int NotaEfeitosVisuais { get; set; }
        [Range(0, 10)] public int NotaQualidadeImagem { get; set; }

        // --- Notas Auditivas ---
        [Range(0, 10)] public int NotaScore { get; set; }
        [Range(0, 10)] public int NotaEfeitosSonoros { get; set; }
    }

    public class AvaliacaoRespostaDto
    {
        public Guid Id { get; set; }
        public string NomeUsuario { get; set; }
        public Guid AvaliadoId { get; set; }
        public double MediaGeral { get; set; } 
        public DateTime DataAvaliacao { get; set; }
    }
}
