namespace CineReview.Models
{
    public interface IAvaliavel
    {
        List<Avaliacao> Avaliacoes { get; }
        double NotaMediaGeral { get; }
        double NotaMediaNarrativa { get; }
        double NotaMediaExecucao { get; }
        double NotaMediaVisual { get; }
        double NotaMediaAuditiva { get; }

        /*As propriedades acima (abstratas) são para serem implementadas,
        na classe Filme e Temporada, a última não é uma classe filha de Midia, por isso a interface*/
        //Interface não é instanciada, por tanto não precisa de construtor

        //Métodos_____________________________________________________________________________________________________________________________
        void AdicionarAvaliacao(Avaliacao avaliacao);
    }
}
