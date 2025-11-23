using System.Linq;

namespace CineReview.Models
{
    public class Filme : Midia, IAvaliavel
    {

        //Implementação da classe pai_____________________________________________________________________________________________________________________________
        public override double NotaMediaGeral
        {
            get
            {
                if (Avaliacoes.Count == 0) return 0;
                return Math.Round(Avaliacoes.Average(a => a.GetMediaGeral()), 2);
            }
        }
        public override double NotaMediaNarrativa
        {
            get
            {
                if (Avaliacoes.Count == 0) return 0;
                return Math.Round(Avaliacoes.Average(a => a.GetMediaNarrativa()), 2);
            }
        }
        public override double NotaMediaExecucao
        {
            get
            {
                if (Avaliacoes.Count == 0) return 0;
                return Math.Round(Avaliacoes.Average(a => a.GetMediaExecucao()), 2);
            }
        }
        public override double NotaMediaVisual
        {
            get
            {
                if (Avaliacoes.Count == 0) return 0;
                return Math.Round(Avaliacoes.Average(a => a.GetMediaVisual()), 2);
            }
        }
        public override double NotaMediaAuditiva
        {
            get
            {
                if (Avaliacoes.Count == 0) return 0;
                return Math.Round(Avaliacoes.Average(a => a.GetMediaAuditiva()), 2);
            }
        }

        //Implementação da interface_____________________________________________________________________________________________________________________________
        public List<Avaliacao> Avaliacoes { get; private set; }

        //Construtor_____________________________________________________________________________________________________________________________
        public Filme(string titulo, string genero, string sinopse, TimeSpan duracao, string classificacaoIndicativa, DateOnly dataLancamento) : base(titulo, genero, sinopse, duracao, classificacaoIndicativa, dataLancamento)
        {
            Avaliacoes = new List<Avaliacao>();
        }

        //Método da interface_____________________________________________________________________________________________________________________________
        public void AdicionarAvaliacao(Avaliacao avaliacao)
        {
            if (avaliacao == null) throw new ArgumentNullException(nameof(avaliacao), "Não é possível adicionar uma avaliação vazia.");
            if (Avaliacoes.Any(a => a.UsuarioId == avaliacao.UsuarioId)) throw new InvalidOperationException("Este usuário já avaliou este filme.");
            Avaliacoes.Add(avaliacao);
        }
    }
}
