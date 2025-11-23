using System.Collections.Generic;
using System.Linq;

namespace CineReview.Models
{
    public class Usuario
    {
        public Guid Id { get; set; }
        public string NomeUsuario { get; set; }
        public string Email { get; set; }
        public string SenhaHash { get; private set; }
        public List<Avaliacao> Avaliacoes { get; private set; }
        public List<Midia> Favoritos { get; private set; }

        //Construtor_____________________________________________________________________________________________________________________________
        public Usuario(string nomeUsuario, string email, string senhaHash)
        {
            Id = Guid.NewGuid();
            NomeUsuario = nomeUsuario;
            Email = email;
            SenhaHash = senhaHash;
            Avaliacoes = new List<Avaliacao>();
            Favoritos = new List<Midia>();
        }

        //Métodos
        public bool ValidarSenha(string senha)
        {
            if (string.IsNullOrWhiteSpace(senha))
                throw new ArgumentNullException(nameof(senha), "A senha não pode ser vazia.");

            return SenhaHash == HashSimples(senha);
        }

        public void DefinirSenha(string novaSenha)
        {
            if (string.IsNullOrWhiteSpace(novaSenha) || novaSenha.Length < 6) 
                throw new ArgumentException("A senha deve ter no mínimo 6 caracteres e não pode ser vazia.");

            SenhaHash = HashSimples(novaSenha);
        }

        private string HashSimples(string senha)
        {
            return senha.GetHashCode().ToString();
        }





        public void FavoritarMidia(Midia midia)
        {
            if (midia == null)
                throw new ArgumentNullException(nameof(midia), "A mídia a ser favoritada não pode ser nula.");

            
            if (!Favoritos.Any(f => f.Id == midia.Id))
            {
                Favoritos.Add(midia);
            }
        }

        public void RemoverFavorito(Guid midiaId)
        {
            if (midiaId == Guid.Empty)
                throw new ArgumentException("ID inválido para remoção.");

            Favoritos.RemoveAll(f => f.Id == midiaId);
        }
    }
}
