using CineReview.Models;
using Microsoft.EntityFrameworkCore;
using System;

namespace CineReview.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

        public DbSet<Usuario> Usuarios { get; set; }

      
        public DbSet<Midia> Midias { get; set; } 
        public DbSet<Temporada> Temporadas { get; set; }
        public DbSet<Episodio> Episodios { get; set; }

        public DbSet<Equipe> Equipes { get; set; } 

        public DbSet<Avaliacao> Avaliacoes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);


            // Família Usuário: Uma tabela, coluna 'TipoUsuario' define quem é quem
            modelBuilder.Entity<Usuario>()
                .HasDiscriminator<string>("TipoUsuario")
                .HasValue<Usuario>("Comum")
                .HasValue<Administrador>("Admin");

            // Família Mídia: Uma tabela para Filmes e Séries
            modelBuilder.Entity<Midia>()
                .HasDiscriminator<string>("TipoMidia")
                .HasValue<Filme>("Filme")
                .HasValue<Serie>("Serie");

            // Família Equipe
            modelBuilder.Entity<Equipe>()
                .HasDiscriminator<string>("TipoMembro")
                .HasValue<Ator>("Ator")
                .HasValue<EquipeTecnica>("Tecnico");

            // --- RELACIONAMENTOS ESPECÍFICOS ---

            // Configurar lista de Favoritos (Muitos-para-Muitos entre Usuario e Midia)
            modelBuilder.Entity<Usuario>()
                .HasMany(u => u.Favoritos)
                .WithMany(); // Cria uma tabela de junção automática

            // Configurar Série -> Temporadas -> Episódios
            modelBuilder.Entity<Serie>()
                .HasMany(s => s.Temporadas)
                .WithOne()
                .OnDelete(DeleteBehavior.Cascade); // Se apagar a série, apaga as temporadas

            modelBuilder.Entity<Temporada>()
                .HasMany(t => t.Episodios)
                .WithOne()
                .OnDelete(DeleteBehavior.Cascade);

            // --- CONFIGURAÇÃO DA AVALIAÇÃO (O Ponto Crítico) ---
            /* Como "IAvaliavel" é uma interface e o EF Core não mapeia interfaces diretamente
               para relacionamentos, e sua Avaliação tem um "AvaliadoId" (Guid),
               vamos deixar o relacionamento "solto" (sem chave estrangeira forte) 
               para não complicar sua apresentação de terça-feira.
               
               Apenas garantimos que Avaliação pertence a um Usuário.
            */
            modelBuilder.Entity<Avaliacao>()
                .HasOne(a => a.Usuario)
                .WithMany(u => u.Avaliacoes)
                .HasForeignKey(a => a.UsuarioId);
        }
    }
}
