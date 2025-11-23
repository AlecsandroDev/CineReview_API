using CineReview.Data;
using CineReview.DTOs;
using CineReview.Models;
using Microsoft.EntityFrameworkCore;
using System;

namespace CineReview.Services
{
    public class SerieService : ISerieService
    {
        private readonly DataContext _context;

        public SerieService(DataContext context)
        {
            _context = context;
        }

        public SerieRespostaDto Cadastrar(CriarSerieDto dto)
        {
            if (_context.Midias.Any(m => m.Titulo == dto.Titulo))
                throw new Exception("Série já cadastrada.");

            var novaSerie = new CineReview.Models.Serie(
                dto.Titulo, dto.Genero, dto.Sinopse,
                dto.DuracaoMediaEpisodio,
                dto.ClassificacaoIndicativa, dto.DataLancamento
            );

            _context.Midias.Add(novaSerie);
            _context.SaveChanges();

            return new SerieRespostaDto
            {
                Id = novaSerie.Id,
                Titulo = novaSerie.Titulo,
                Genero = novaSerie.Genero,
                NotaMediaGeral = 0,
                QtdTemporadas = 0
            };
        }

        public List<SerieRespostaDto> ListarTodas()
        {
            var series = _context.Midias.OfType<CineReview.Models.Serie>()
                .Include(s => s.Temporadas)
                .ThenInclude(t => t.Avaliacoes)
                .ToList();

            return series.Select(s => new SerieRespostaDto
            {
                Id = s.Id,
                Titulo = s.Titulo,
                Genero = s.Genero,
                NotaMediaGeral = s.NotaMediaGeral,
                QtdTemporadas = s.Temporadas.Count
            }).ToList();
        }

        public SerieRespostaDto BuscarPorId(Guid id)
        {
            var serie = _context.Midias.OfType<CineReview.Models.Serie>()
                .Include(s => s.Temporadas)
                .ThenInclude(t => t.Avaliacoes)
                .FirstOrDefault(s => s.Id == id);

            if (serie == null) throw new Exception("Série não encontrada");

            return new SerieRespostaDto
            {
                Id = serie.Id,
                Titulo = serie.Titulo,
                Genero = serie.Genero,
                NotaMediaGeral = serie.NotaMediaGeral,
                QtdTemporadas = serie.Temporadas.Count
            };
        }

        public void Atualizar(Guid id, CriarSerieDto dto)
        {
            var serie = _context.Midias.OfType<CineReview.Models.Serie>().FirstOrDefault(s => s.Id == id);
            if (serie == null) throw new Exception("Série não encontrada.");

            serie.Titulo = dto.Titulo;
            serie.Genero = dto.Genero;
            serie.Sinopse = dto.Sinopse;
            serie.ClassificacaoIndicativa = dto.ClassificacaoIndicativa;
            serie.DataLancamento = dto.DataLancamento;
            // Para alterar duração média precisaria expor setter na Model ou criar método

            _context.SaveChanges();
        }

        public void Deletar(Guid id)
        {
            var serie = _context.Midias.OfType<CineReview.Models.Serie>().FirstOrDefault(s => s.Id == id);
            if (serie == null) throw new Exception("Série não encontrada.");

            _context.Midias.Remove(serie);
            _context.SaveChanges();
        }
    }
}