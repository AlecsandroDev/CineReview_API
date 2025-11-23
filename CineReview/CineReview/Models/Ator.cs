
namespace CineReview.Models
{
    public class Ator : Equipe
    {
        public string Papel { get; set; }

        public Ator(string nomeCompleto, List<string> funcoes, string papel) : base(nomeCompleto, funcoes)
        {
            Papel = papel;
        }
    }
}
