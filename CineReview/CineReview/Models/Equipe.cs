namespace CineReview.Models
{
    public abstract class Equipe
    {
        public Guid Id { get; set; }
        public string NomeCompleto { get; set; }
        public List<string> Funcoes { get; private set; } = new List<string>();

        protected Equipe(string nomeCompleto, List<string> funcoes)
        {
            Id = Guid.NewGuid();
            NomeCompleto = nomeCompleto;
            Funcoes = funcoes;
        }
    }
}
