using CineReview.Data;
using CineReview.DTOs;
using CineReview.Models;
using Microsoft.EntityFrameworkCore;
using System;

namespace CineReview.Services
{
    public class FilmeService : IFilmeService
    {
        private readonly DataContext _context;

        public FilmeService(DataContext context)
        {
            _context = context;
        }

        public FilmeRespostaDto Cadastrar(CriarFilmeDto dto)
        {
            if (_context.Midias.Any(m => m.Titulo == dto.Titulo))
                throw new Exception("Já existe um filme com este título.");

            var novoFilme = new CineReview.Models.Filme(
                dto.Titulo, dto.Genero, dto.Sinopse, dto.Duracao,
                dto.ClassificacaoIndicativa, dto.DataLancamento
            );

            _context.Midias.Add(novoFilme);
            _context.SaveChanges();

            return new FilmeRespostaDto
            {
                Id = novoFilme.Id,
                Titulo = novoFilme.Titulo,
                Genero = novoFilme.Genero,
                NotaMediaGeral = novoFilme.NotaMediaGeral
            };
        }

        public List<FilmeRespostaDto> ListarTodos()
        {
            return _context.Midias.OfType<CineReview.Models.Filme>()
                .Select(f => new FilmeRespostaDto
                {
                    Id = f.Id,
                    Titulo = f.Titulo,
                    Genero = f.Genero,
                    NotaMediaGeral = f.NotaMediaGeral
                }).ToList();
        }

        public FilmeRespostaDto BuscarPorId(Guid id)
        {
            var filme = _context.Midias.OfType<CineReview.Models.Filme>().FirstOrDefault(f => f.Id == id);
            if (filme == null) throw new Exception("Filme não encontrado.");

            return new FilmeRespostaDto
            {
                Id = filme.Id,
                Titulo = filme.Titulo,
                Genero = filme.Genero,
                NotaMediaGeral = filme.NotaMediaGeral
            };
        }

        public void Atualizar(Guid id, CriarFilmeDto dto)
        {
            var filme = _context.Midias.OfType<CineReview.Models.Filme>().FirstOrDefault(f => f.Id == id);
            if (filme == null) throw new Exception("Filme não encontrado.");

            filme.Titulo = dto.Titulo;
            filme.Genero = dto.Genero;
            filme.Sinopse = dto.Sinopse;
            filme.Duracao = dto.Duracao;
            filme.ClassificacaoIndicativa = dto.ClassificacaoIndicativa;
            filme.DataLancamento = dto.DataLancamento;

            try
            {
                _context.SaveChanges();
            }
            catch (DbUpdateException ex)
            {
                var mensagemErro = ex.InnerException?.Message ?? ex.Message;
                throw new Exception($"Erro ao atualizar no banco: {mensagemErro}");
            }
        }

        public void Deletar(Guid id)
        {
            var filme = _context.Midias.Find(id);
            if (filme == null) throw new Exception("Filme não encontrado.");

            _context.Midias.Remove(filme);
            _context.SaveChanges();
        }
    }
}