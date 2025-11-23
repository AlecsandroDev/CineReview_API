using CineReview.Data;
using CineReview.DTOs;
using CineReview.Models;
using Microsoft.EntityFrameworkCore;
using System;

namespace CineReview.Services
{
    public class EquipeService : IEquipeService
    {
        private readonly DataContext _context;

        public EquipeService(DataContext context)
        {
            _context = context;
        }

        public EquipeRespostaDto AdicionarAtor(CriarAtorDto dto)
        {
            // Validação: Tem que mandar pelo menos um ID
            if (dto.MidiaId == null && dto.TemporadaId == null)
                throw new Exception("É necessário informar uma Mídia ou uma Temporada.");

            // Criar o objeto Ator (Herda de Equipe)
            var ator = new Ator(dto.NomeCompleto, dto.Funcoes, dto.Papel);

            // Vincular (Gambiarra técnica elegante para o EF Core)
            // Como não mapeamos explicitamente MidiaId na classe Equipe, usamos o Entry para definir a Shadow Property
            // OU, se você adicionou as propriedades na classe Equipe como sugeri antes, use direto.
            // Vou assumir a forma mais robusta via navegação:

            if (dto.MidiaId.HasValue)
            {
                var midia = _context.Midias.Include(m => m.Equipe).FirstOrDefault(m => m.Id == dto.MidiaId);
                if (midia == null) throw new Exception("Mídia não encontrada.");

                midia.AdicionarMembroEquipe(ator); // Usa o método do seu Modelo Midia
            }
            else if (dto.TemporadaId.HasValue)
            {
                var temporada = _context.Temporadas.Include(t => t.Equipe).FirstOrDefault(t => t.Id == dto.TemporadaId);
                if (temporada == null) throw new Exception("Temporada não encontrada.");

                temporada.AdicionarMembroEquipe(ator); // Usa o método do seu Modelo Temporada
            }

            // O EF Core entende que o ator foi adicionado à lista e salva
            // Mas para garantir que salve na tabela Equipe corretamente como filho:
            _context.Equipes.Add(ator);
            _context.SaveChanges();

            return ConverterParaDto(ator, "Ator");
        }

        public EquipeRespostaDto AdicionarTecnico(CriarTecnicoDto dto)
        {
            if (dto.MidiaId == null && dto.TemporadaId == null)
                throw new Exception("É necessário informar uma Mídia ou uma Temporada.");

            var tecnico = new EquipeTecnica(dto.NomeCompleto, dto.Funcoes);

            if (dto.MidiaId.HasValue)
            {
                var midia = _context.Midias.Include(m => m.Equipe).FirstOrDefault(m => m.Id == dto.MidiaId);
                if (midia == null) throw new Exception("Mídia não encontrada.");
                midia.AdicionarMembroEquipe(tecnico);
            }
            else if (dto.TemporadaId.HasValue)
            {
                var temporada = _context.Temporadas.Include(t => t.Equipe).FirstOrDefault(t => t.Id == dto.TemporadaId);
                if (temporada == null) throw new Exception("Temporada não encontrada.");
                temporada.AdicionarMembroEquipe(tecnico);
            }

            _context.Equipes.Add(tecnico);
            _context.SaveChanges();

            return ConverterParaDto(tecnico, "Tecnico");
        }

        public List<EquipeRespostaDto> ListarPorMidia(Guid midiaId)
        {
            // Busca equipes onde a Shadow Property "MidiaId" é igual ao parametro
            // Como o EF gerencia isso "por baixo dos panos", a forma mais segura é buscar a Midia e incluir a equipe
            var midia = _context.Midias
                .Include(m => m.Equipe)
                .FirstOrDefault(m => m.Id == midiaId);

            if (midia == null) return new List<EquipeRespostaDto>();

            return midia.Equipe.Select(e => ConverterParaDto(e, e is Ator ? "Ator" : "Tecnico")).ToList();
        }

        public List<EquipeRespostaDto> ListarPorTemporada(Guid temporadaId)
        {
            var temporada = _context.Temporadas
                .Include(t => t.Equipe)
                .FirstOrDefault(t => t.Id == temporadaId);

            if (temporada == null) return new List<EquipeRespostaDto>();

            return temporada.Equipe.Select(e => ConverterParaDto(e, e is Ator ? "Ator" : "Tecnico")).ToList();
        }

        // Método auxiliar para evitar repetição de código
        private EquipeRespostaDto ConverterParaDto(Equipe equipe, string tipo)
        {
            return new EquipeRespostaDto
            {
                Id = equipe.Id,
                NomeCompleto = equipe.NomeCompleto,
                Funcoes = equipe.Funcoes,
                Tipo = tipo,
                // Se for Ator, faz o cast para pegar o Papel, senão null
                Papel = (equipe as Ator)?.Papel
            };
        }
    }
}