using System.Transactions;

namespace CineReview.Models
{
    public class Avaliacao
    {
        private Avaliacao() { }

        //Variáveis de apoio para setters:
        private Usuario _usuario;
        public Guid Id { get; private set; }
        public DateTime DataAvaliacao { get; private set; }
        public Guid UsuarioId { get; private set; }
        public Guid AvaliadoId { get; private set; }
        public Usuario Usuario
        {
            get => _usuario;
            private set
            {
                if (value == null) throw new ArgumentNullException($"Objeto {nameof(value)} não pode ser vazio!");
                _usuario = value;
            }
        }


        //Varíaveis de apoio para setters:
        private int _notaTrama, _notaRitmo, _notaDevPersonagens, _notaConstrucaoMundo, _notaTematica, _notaAtuacao, _notaEdicao, _notaDirecao, _notaArte, _notaCinematografia, _notaCenarios, _notaFigurinos, _notaEfeitosVisuais, _notaQualidadeImagem, _notaScore, _notaEfeitosSonoros;

        //Notas de Narrativa (0-10)_____________________________________________________________________________________________________________________________
        public int NotaTrama 
        { 
            get => _notaTrama;
            set
            {
                ValidarNota(value, nameof(NotaTrama));
                _notaTrama = value;
            } 
        }
        public int NotaRitmo
        {
            get => _notaRitmo;
            set
            {
                ValidarNota(value, nameof(NotaRitmo));
                _notaRitmo = value;
            }
        }
        public int NotaDevPersonagens
        {
            get => _notaDevPersonagens;
            set
            {
                ValidarNota(value, nameof(NotaDevPersonagens));
                _notaDevPersonagens = value;
            }
        }
        public int NotaConstrucaoMundo
        {
            get => _notaConstrucaoMundo;
            set
            {
                ValidarNota(value, nameof(NotaConstrucaoMundo));
                _notaConstrucaoMundo = value;
            }
        }
        public int NotaTematica
        {
            get => _notaTematica;
            set
            {
                ValidarNota(value, nameof(NotaTematica));
                _notaTematica = value;
            }
        }

        //Notas de Execução (0-10)_____________________________________________________________________________________________________________________________
        public int NotaAtuacao
        {
            get => _notaAtuacao;
            set
            {
                ValidarNota(value, nameof(NotaAtuacao));
                _notaAtuacao = value;
            }
        }
        public int NotaEdicao
            {
            get => _notaEdicao;
            set
            {
                ValidarNota(value, nameof(NotaEdicao));
                _notaEdicao = value;
            }
        }
        public int NotaDirecao
            {
            get => _notaDirecao;
            set
            {
                ValidarNota(value, nameof(NotaDirecao));
                _notaDirecao = value;
            }
        }

        //Notas Visuais (0-10)_____________________________________________________________________________________________________________________________
        public int NotaArte
            {
            get => _notaArte;
            set
            {
                ValidarNota(value, nameof(NotaArte));
                _notaArte = value;
            }
        }
        public int NotaCinematografia
            {
            get => _notaCinematografia;
            set
            {
                ValidarNota(value, nameof(NotaCinematografia));
                _notaCinematografia = value;
            }
        }
        public int NotaCenarios
            {
            get => _notaCenarios;
            set
            {
                ValidarNota(value, nameof(NotaCenarios));
                _notaCenarios = value;
            }
        }
        public int NotaFigurinos
            {
            get => _notaFigurinos;
            set
            {
                ValidarNota(value, nameof(NotaFigurinos));
                _notaFigurinos = value;
            }
        }
        public int NotaEfeitosVisuais
            {
            get => _notaEfeitosVisuais;
            set
            {
                ValidarNota(value, nameof(NotaEfeitosVisuais));
                _notaEfeitosVisuais = value;
            }
        }
        public int NotaQualidadeImagem
            {
            get => _notaQualidadeImagem;
            set
            {
                ValidarNota(value, nameof(NotaQualidadeImagem));
                _notaQualidadeImagem = value;
            }
        }

        //Notas Auditivas (0-10)_____________________________________________________________________________________________________________________________
        public int NotaScore
            {
            get => _notaScore;
            set
            {
                ValidarNota(value, nameof(NotaScore));
                _notaScore = value;
            }
        }
        public int NotaEfeitosSonoros
            {
            get => _notaEfeitosSonoros;
            set
            {
                ValidarNota(value, nameof(NotaEfeitosSonoros));
                _notaEfeitosSonoros = value;
            }
        }

        //Construtor_____________________________________________________________________________________________________________________________
        public Avaliacao(Usuario usuario, Guid avaliadoId,
            int notaTrama, int notaRitmo, int notaDevPersonagens, int notaConstrucaoMundo, int notaTematica,
            int notaAtuacao, int notaEdicao, int notaDirecao,
            int notaArte, int notaCinematografia, int notaCenarios, int notaFigurinos, int notaEfeitosVisuais, int notaQualidadeImagem,
            int notaScore, int notaEfeitosSonoros)
        {
            Id = Guid.NewGuid();
            DataAvaliacao = DateTime.UtcNow; // Define a data de agora

            // Vincula o usuário
            Usuario = usuario;
            UsuarioId = usuario.Id;

            AvaliadoId = avaliadoId;
            NotaTrama = notaTrama;
            NotaRitmo = notaRitmo;
            NotaDevPersonagens = notaDevPersonagens;
            NotaConstrucaoMundo = notaConstrucaoMundo;
            NotaTematica = notaTematica;
            NotaAtuacao = notaAtuacao;
            NotaEdicao = notaEdicao;
            NotaDirecao = notaDirecao;
            NotaArte = notaArte;
            NotaCinematografia = notaCinematografia;
            NotaCenarios = notaCenarios;
            NotaFigurinos = notaFigurinos;
            NotaEfeitosVisuais = notaEfeitosVisuais;
            NotaQualidadeImagem = notaQualidadeImagem;
            NotaScore = notaScore;
            NotaEfeitosSonoros = notaEfeitosSonoros;
        }

        //Métodos_____________________________________________________________________________________________________________________________
        public double GetMediaNarrativa()
        {
            double media = (NotaTrama + NotaRitmo + NotaDevPersonagens + NotaConstrucaoMundo + NotaTematica) / 5.0;
            return Math.Round(media, 2);
        }
        public double GetMediaExecucao()
        {
            double media = (NotaAtuacao + NotaEdicao + NotaDirecao) / 3.0;
            return Math.Round(media, 2);
        }
        public double GetMediaVisual()
        {
            double media = (NotaArte + NotaCinematografia + NotaCenarios + NotaFigurinos + NotaEfeitosVisuais + NotaQualidadeImagem) / 6.0;
            return Math.Round(media, 2);
        }
        public double GetMediaAuditiva()
        {
            double media = (NotaScore + NotaEfeitosSonoros) / 2.0;
            return Math.Round(media, 2);
        }
        public double GetMediaGeral()
        {
            double media = (GetMediaNarrativa() + GetMediaExecucao() + GetMediaVisual() + GetMediaAuditiva()) / 4.0;
            return Math.Round(media, 2);
        }

        private void ValidarNota(double valor, string nomePropriedade)
        {
            if (valor < 0 || valor > 10)
                throw new ArgumentException($"A {nomePropriedade} deve ser entre 0 e 10.");
        }

    }
}
