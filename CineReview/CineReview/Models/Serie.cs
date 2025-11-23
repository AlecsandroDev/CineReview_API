
namespace CineReview.Models
{
    public class Serie : Midia
    {
        public override double NotaMediaGeral 
        {
            get
            {
                if (Temporadas.Count == 0) return 0;
                return Math.Round(Temporadas.Average(t => t.NotaMediaGeral), 2);
            }
        }

        public override double NotaMediaNarrativa 
        {
            get
            {
                if (Temporadas.Count == 0) return 0;
                return Math.Round(Temporadas.Average(t => t.NotaMediaNarrativa), 2);
            }
        }

        public override double NotaMediaExecucao 
        {
            get
            {
                if (Temporadas.Count == 0) return 0;
                return Math.Round(Temporadas.Average(t => t.NotaMediaExecucao), 2);
            }
        }

        public override double NotaMediaVisual 
        {
            get
            {
                if (Temporadas.Count == 0) return 0;
                return Math.Round(Temporadas.Average(t => t.NotaMediaVisual), 2);
            }
        }

        public override double NotaMediaAuditiva 
        {
            get
            {
                if (Temporadas.Count == 0) return 0;
                return Math.Round(Temporadas.Average(t => t.NotaMediaAuditiva), 2);
            }
        }

        public List<Temporada> Temporadas { get; private set; }

        //Construtor_____________________________________________________________________________________________________________________________
        public Serie(string titulo, string genero, string sinopse, TimeSpan duracao, string classificacaoIndicativa, DateOnly dataLancamento) : base(titulo, genero, sinopse, duracao, classificacaoIndicativa, dataLancamento)
        {
            Temporadas = new List<Temporada>();
        }

        //Métodos_____________________________________________________________________________________________________________________________
        public void AdicionarTemporada(Temporada temporada)
        {
            if (temporada == null) throw new ArgumentNullException(nameof(temporada), "Não é possível adicionar uma temporada vazia.");
            if (Temporadas.Any(t => t.NumeroTemporada == temporada.NumeroTemporada)) throw new InvalidOperationException($"A Temporada {temporada.NumeroTemporada} já existe nesta série.");
            Temporadas.Add(temporada);
        }
    }
}
