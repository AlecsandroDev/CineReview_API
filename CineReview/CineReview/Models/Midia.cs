namespace CineReview.Models
{
    public abstract class Midia
    {
        //Variáveis de apoio:
        private string _titulo, _genero, _sinopse, _classificacaoIndicativa;
        private TimeSpan _duracao;

        public Guid Id { get; set; }
        public string Titulo 
        { 
            get => _titulo;
            set
            {
                if (string.IsNullOrWhiteSpace(value)) throw new ArgumentException("O título é obrigatório.");
                _titulo = value;
            }
        }
        public string Genero 
        { 
            get => _genero;
            set
            {
                if (string.IsNullOrWhiteSpace(value)) throw new ArgumentException("O gênero é obrigatório.");
                _genero = value;
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
        public TimeSpan Duracao 
        { 
            get => _duracao;
            set
            {
                if (value <= TimeSpan.Zero) throw new ArgumentException("A duração deve ser maior que zero.");
                _duracao = value;
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
        public abstract double NotaMediaGeral { get; }
        public abstract double NotaMediaNarrativa { get; }
        public abstract double NotaMediaExecucao { get; }
        public abstract double NotaMediaVisual { get; }
        public abstract double NotaMediaAuditiva { get; }
        public List<Equipe> Equipe { get; private set; }

        protected Midia(string titulo, string genero, string sinopse, TimeSpan duracao, string classificacaoIndicativa, DateOnly dataLancamento) 
        {
            Id = Guid.NewGuid();
            Titulo = titulo;
            Genero = genero;
            Sinopse = sinopse;
            Duracao = duracao;
            ClassificacaoIndicativa = classificacaoIndicativa;
            DataLancamento = dataLancamento;
            Equipe = new List<Equipe>();
        }

        public void AdicionarMembroEquipe(Equipe membro)
        {
            if (membro == null) throw new ArgumentNullException(nameof(membro), "Não é possível adicionar um membro de equipe vazio.");
            if (Equipe.Any(m => m.NomeCompleto == membro.NomeCompleto)) throw new InvalidOperationException($"O membro '{membro.NomeCompleto}' já faz parte da equipe.");
            Equipe.Add(membro);
        }
    }
}
