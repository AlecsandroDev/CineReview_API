using CineReview.Data;
using CineReview.DTOs;
using CineReview.Models;
using CineReview.Service.Avaliacao; // Verifique se este é o namespace correto da sua interface
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CineReview.Services
{
    public class AvaliacaoService : IAvaliacaoService   
    {
        private readonly DataContext _context;

        public AvaliacaoService(DataContext context)
        {
            _context = context;
        }

        public AvaliacaoRespostaDto Avaliar(CriarAvaliacaoDto dto)
        {
            var usuario = _context.Usuarios.Find(dto.UsuarioId);
            if (usuario == null) throw new Exception("Usuário não encontrado.");

            bool existeMidia = _context.Midias.Any(m => m.Id == dto.AvaliadoId);
            bool existeTemporada = _context.Temporadas.Any(t => t.Id == dto.AvaliadoId);

            if (!existeMidia && !existeTemporada)
                throw new Exception("Obra (Filme/Série/Temporada) não encontrada.");

            bool jaAvaliou = _context.Avaliacoes.Any(a =>
                a.UsuarioId == dto.UsuarioId &&
                a.AvaliadoId == dto.AvaliadoId);

            if (jaAvaliou) throw new Exception("Você já avaliou esta obra.");

            // Usando o construtor completo
            var novaAvaliacao = new CineReview.Models.Avaliacao(
                usuario, dto.AvaliadoId,
                dto.NotaTrama, dto.NotaRitmo, dto.NotaDevPersonagens, dto.NotaConstrucaoMundo, dto.NotaTematica,
                dto.NotaAtuacao, dto.NotaEdicao, dto.NotaDirecao,
                dto.NotaArte, dto.NotaCinematografia, dto.NotaCenarios, dto.NotaFigurinos, dto.NotaEfeitosVisuais, dto.NotaQualidadeImagem,
                dto.NotaScore, dto.NotaEfeitosSonoros
            );

            _context.Avaliacoes.Add(novaAvaliacao);
            _context.SaveChanges();

            return ConverterParaDto(novaAvaliacao);
        }

        public List<AvaliacaoRespostaDto> ListarTodas()
        {
            var lista = _context.Avaliacoes
                    .Include(a => a.Usuario)
                    .ToList();

            return lista.Select(a => ConverterParaDto(a)).ToList();
        }

        public List<AvaliacaoRespostaDto> ListarPorMidia(Guid midiaId)
        {
            var avaliacoesDoBanco = _context.Avaliacoes
                .Include(a => a.Usuario)
                .Where(a => a.AvaliadoId == midiaId)
                .ToList();

            return avaliacoesDoBanco.Select(a => ConverterParaDto(a)).ToList();
        }

        public void Deletar(Guid id)
        {
            var avaliacao = _context.Avaliacoes.Find(id);
            if (avaliacao == null) throw new Exception("Avaliação não encontrada.");
            _context.Avaliacoes.Remove(avaliacao);
            _context.SaveChanges();
        }

        // Método auxiliar privado (não faz parte da interface pública)
        private AvaliacaoRespostaDto ConverterParaDto(CineReview.Models.Avaliacao a)
        {
            return new AvaliacaoRespostaDto
            {
                Id = a.Id,
                NomeUsuario = a.Usuario?.NomeUsuario ?? "Anônimo",
                AvaliadoId = a.AvaliadoId,
                MediaGeral = a.GetMediaGeral(),
                DataAvaliacao = a.DataAvaliacao
            };
        }

        public List<AvaliacaoRespostaDto> ListarTodos()
        {
            var lista = _context.Avaliacoes
                    .Include(a => a.Usuario)
                    .ToList();

            // Converte para o DTO
            return lista.Select(a => ConverterParaDto(a)).ToList();
        }
    }
}