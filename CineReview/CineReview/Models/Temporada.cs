namespace CineReview.Models
{
    public class Temporada : IAvaliavel
    {
        //Variáveis de apoio:
        private string _titulo, _sinopse, _classificacaoIndicativa;
        private int _numeroTemporada;

        //Propriedades próprias_____________________________________________________________________________________________________________________________
        public Guid Id { get; set; }
        public int NumeroTemporada 
        { 
            get => _numeroTemporada;
            set
            {
                if (value <= 0) throw new ArgumentException("O número da temporada deve ser maior que zero.");
                _numeroTemporada = value;
            }
        }
        public string Titulo 
        { 
            get => _titulo;
            set
            {
                if (string.IsNullOrWhiteSpace(value)) throw new ArgumentException("O título da temporada é obrigatório.");
                _titulo = value;
            }
        }
        public string Sinopse 
        { 
            get => _sinopse;
            set
            {
                if (string.IsNullOrWhiteSpace(value)) throw new ArgumentException("A sinopse é obrigatória.");
                _sinopse = value;
            }
        }
        public string ClassificacaoIndicativa 
        { 
            get => _classificacaoIndicativa;
            set
            {
                if (string.IsNullOrWhiteSpace(value)) throw new ArgumentException("A classificação indicativa é obrigatória.");
                _classificacaoIndicativa = value;
            }
        }
        public DateOnly DataLancamento { get; set; }
        public List<Episodio> Episodios { get; private set; }
        public List<Equipe> Equipe { get; private set; }

        //Propriedade de interface_____________________________________________________________________________________________________________________________
        public List<Avaliacao> Avaliacoes { get; private set; }
        public double NotaMediaGeral 
        {
            get
            {
                if (Avaliacoes.Count == 0) return 0;
                return Math.Round(Avaliacoes.Average(a => a.GetMediaGeral()), 2);
            }
        }
        public double NotaMediaNarrativa 
        {
            get
            {
                if (Avaliacoes.Count == 0) return 0;
                return Math.Round(Avaliacoes.Average(a => a.GetMediaNarrativa()), 2);
            }
        }
        public double NotaMediaExecucao 
        {
            get
            {
                if (Avaliacoes.Count == 0) return 0;
                return Math.Round(Avaliacoes.Average(a => a.GetMediaExecucao()), 2);
            }
        }
        public double NotaMediaVisual 
        {
            get
            {
                if (Avaliacoes.Count == 0) return 0;
                return Math.Round(Avaliacoes.Average(a => a.GetMediaVisual()), 2);
            }
        }
        public double NotaMediaAuditiva 
        {
            get
            {
                if (Avaliacoes.Count == 0) return 0;
                return Math.Round(Avaliacoes.Average(a => a.GetMediaAuditiva()), 2);
            }
        }

        //Construtor_____________________________________________________________________________________________________________________________
        public Temporada(int numeroTemporada, string titulo, string sinopse, string classificacaoIndicativa, DateOnly dataLancamento)
        {
            Id = Guid.NewGuid();
            NumeroTemporada = numeroTemporada;
            Titulo = titulo;
            Sinopse = sinopse;
            ClassificacaoIndicativa = classificacaoIndicativa;
            DataLancamento = dataLancamento;
            Episodios = new List<Episodio>();
            Equipe = new List<Equipe>();
            Avaliacoes = new List<Avaliacao>();
        }

        //Método da interface_____________________________________________________________________________________________________________________________
        
        public void AdicionarAvaliacao(Avaliacao avaliacao)
        {
            if (avaliacao == null) throw new ArgumentNullException("O objeto avaliação não pode ser nulo.");
            if (Avaliacoes.Any(a => a.UsuarioId == avaliacao.UsuarioId)) throw new InvalidOperationException("Este usuário já avaliou esta temporada.");
            Avaliacoes.Add(avaliacao);
        }

        //Métodos_____________________________________________________________________________________________________________________________
        public void AdicionarEpisodio(Episodio episodio)
        {
            if (episodio == null) throw new ArgumentNullException("O objeto episódio não pode ser nulo.");
            Episodios.Add(episodio);
        }

        public void AdicionarMembroEquipe(Equipe membro)
        {
            if (membro == null) throw new ArgumentNullException(nameof(membro), "Não é possível adicionar um membro de equipe vazio.");
            if (Equipe.Any(m => m.NomeCompleto == membro.NomeCompleto)) throw new InvalidOperationException($"O membro '{membro.NomeCompleto}' já faz parte da equipe.");
            Equipe.Add(membro);
        }
    }
}
