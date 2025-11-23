namespace CineReview.Models
{
    public class Administrador : Usuario
    {
        public Administrador(string nomeUsuario, string email, string senhaHash) : base(nomeUsuario, email, senhaHash)
        {
        }

        //Métodos_____________________________________________________________________________________________________________________________
        public void CadastrarMidia(Midia midia)
        {
            if (midia == null)            
                throw new ArgumentNullException(nameof(midia), "Digite as informações para cadastrar!");  
            
            ValidarDadosComuns(midia);
            
        }

        public void EditarMidia(Midia midia)
        {
            if (midia == null)
                throw new ArgumentNullException(nameof(midia), "Digite as informações para cadastrar!");
            if (midia.Id == Guid.Empty)
                throw new ArgumentException("Não é possível editar uma mídia sem ID válido.", nameof(midia.Id));

            ValidarDadosComuns(midia);
        }

        public void RemoverMidia(Guid midiaId)
        {
            if (midiaId == Guid.Empty)
                throw new ArgumentException("ID inválido para remoção.");
        }

        private void ValidarDadosComuns(Midia midia)
        {
            if (string.IsNullOrWhiteSpace(midia.Titulo))
                throw new ArgumentException("O título da mídia é obrigatório.", nameof(midia.Titulo));

            if (string.IsNullOrWhiteSpace(midia.Genero))
                throw new ArgumentException("O gênero da mídia é obrigatório.", nameof(midia.Genero));

            if (midia.Duracao <= TimeSpan.Zero)
                throw new ArgumentException("A duração deve ser maior que zero.", nameof(midia.Duracao));

            if (midia.DataLancamento == default)
                throw new ArgumentException("A data de lançamento é inválida.", nameof(midia.DataLancamento));
        }
    }
}
