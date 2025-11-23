using CineReview.Data;
using CineReview.DTOs;
using CineReview.Models; // Namespace Models
using Microsoft.EntityFrameworkCore;
using System;

namespace CineReview.Services
{
    public class TemporadaService : ITemporadaService
    {
        private readonly DataContext _context;

        public TemporadaService(DataContext context)
        {
            _context = context;
        }

        public TemporadaRespostaDto CadastrarTemporada(CriarTemporadaDto dto)
        {
            // 1. Achar a Série
            var serie = _context.Midias.OfType<CineReview.Models.Serie>()
                        .Include(s => s.Temporadas)
                        .FirstOrDefault(s => s.Id == dto.SerieId);

            if (serie == null) throw new Exception("Série não encontrada.");

            // 2. Criar Temporada
            var novaTemporada = new CineReview.Models.Temporada(
                dto.NumeroTemporada, dto.Titulo, dto.Sinopse,
                dto.ClassificacaoIndicativa, dto.DataLancamento
            );

            // 3. Adicionar à Série (usando método da classe Serie para garantir regras)
            try
            {
                serie.AdicionarTemporada(novaTemporada);
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao adicionar temporada: {ex.Message}");
            }

            // O EF Core entende que a temporada foi adicionada à lista da série e salva tudo
            _context.Temporadas.Add(novaTemporada); // Adiciona explícito para garantir ID
            _context.SaveChanges();

            return new TemporadaRespostaDto
            {
                Id = novaTemporada.Id,
                Titulo = novaTemporada.Titulo,
                Numero = novaTemporada.NumeroTemporada,
                NotaMedia = 0
            };
        }

        public void AdicionarEpisodio(CriarEpisodioDto dto)
        {
            var temporada = _context.Temporadas
                            .Include(t => t.Episodios)
                            .FirstOrDefault(t => t.Id == dto.TemporadaId);

            if (temporada == null) throw new Exception("Temporada não encontrada.");

            var novoEpisodio = new CineReview.Models.Episodio(
                dto.NumeroEpisodio, dto.Titulo, dto.Sinopse, dto.Duracao
            );

            temporada.AdicionarEpisodio(novoEpisodio);
            _context.SaveChanges();
        }

        public List<TemporadaRespostaDto> ListarPorSerie(Guid serieId)
        {
            return _context.Temporadas
               .Include(t => t.Avaliacoes) // Necessário para média
               .Where(t => EF.Property<Guid>(t, "SerieId") == serieId) // Truque se não mapeamos SerieId na classe Temporada
                                                                       // OU, se o EF criou a FK shadow property "SerieId", isso funciona.
                                                                       // Como não colocamos "public Guid SerieId" na classe Temporada.cs,
                                                                       // vamos filtrar buscando as temporadas da série via objeto Serie para ser mais seguro:

               // Abordagem Segura:
               .AsEnumerable() // Traz pra memória se a FK for complicada
               .Where(t => _context.Midias.OfType<CineReview.Models.Serie>()
                           .Any(s => s.Id == serieId && s.Temporadas.Contains(t)))
               .Select(t => new TemporadaRespostaDto
               {
                   Id = t.Id,
                   Titulo = t.Titulo,
                   Numero = t.NumeroTemporada,
                   NotaMedia = t.NotaMediaGeral
               }).ToList();
        }
    }
}