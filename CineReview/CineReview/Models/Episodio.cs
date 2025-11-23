namespace CineReview.Models
{
    public class Episodio
    {
        //Variáveis de apoio para setters:
        private int _numeroEpisodio;
        private TimeSpan _duracao;

        public Guid Id { get; set; }
        public int NumeroEpisodio
        {
            get => _numeroEpisodio;
            set
            {
                if (value <= 0) throw new ArgumentOutOfRangeException($"Número de episódio deve ser maior que zero!");
                _numeroEpisodio = value;
            }
        }
        public string Titulo { get; set; }
        public string Sinopse { get; set; }
        public TimeSpan Duracao
        {
            get => _duracao;
            set
            {
                if (value < TimeSpan.Zero) throw new ArgumentOutOfRangeException("Duração não pode conter números negativos.");
                _duracao = value;
            }
        }

        public Episodio(int numeroEpisodio, TimeSpan duracao)
        {
            Id = Guid.NewGuid();
            NumeroEpisodio = numeroEpisodio;
            Duracao = duracao;
        }

        public Episodio(int numeroEpisodio, string titulo, string sinopse, TimeSpan duracao) 
        {
            Id= Guid.NewGuid();
            NumeroEpisodio= numeroEpisodio;
            Titulo = titulo;
            Sinopse = sinopse;
            Duracao = duracao;
        }
    }
}
