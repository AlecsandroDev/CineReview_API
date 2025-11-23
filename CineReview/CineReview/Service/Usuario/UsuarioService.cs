using CineReview.Data;
using CineReview.DTOs;
using CineReview.Models;
using CineReview.Service.Usuario;
using Microsoft.EntityFrameworkCore;
using System;

namespace CineReview.Services
{
    public class UsuarioService : IUsuarioService
    {
        private readonly DataContext _context;

        public UsuarioService(DataContext context)
        {
            _context = context;
        }

        public UsuarioRespostaDTO Cadastrar(CriarUsuarioDTO dto)
        {
            if (_context.Usuarios.Any(u => u.Email == dto.Email))
                throw new Exception("Este email já está cadastrado.");

            string senhaHash = dto.Senha.GetHashCode().ToString();
            var novoUsuario = new Usuario(dto.NomeUsuario, dto.Email, senhaHash);

            _context.Usuarios.Add(novoUsuario);
            _context.SaveChanges();

            return new UsuarioRespostaDTO
            {
                Id = novoUsuario.Id,
                NomeUsuario = novoUsuario.NomeUsuario,
                Email = novoUsuario.Email
            };
        }

        public UsuarioRespostaDTO Login(LoginUsuarioDTO dto)
        {
            var usuario = _context.Usuarios.FirstOrDefault(u => u.Email == dto.Email);
            if (usuario == null) throw new Exception("Usuário ou senha inválidos.");

            if (!usuario.ValidarSenha(dto.Senha))
                throw new Exception("Usuário ou senha inválidos.");

            return new UsuarioRespostaDTO
            {
                Id = usuario.Id,
                NomeUsuario = usuario.NomeUsuario,
                Email = usuario.Email
            };
        }

        public List<UsuarioRespostaDTO> ListarTodos()
        {
            return _context.Usuarios.Select(u => new UsuarioRespostaDTO
            {
                Id = u.Id,
                NomeUsuario = u.NomeUsuario,
                Email = u.Email
            }).ToList();
        }

        public void Atualizar(Guid id, AtualizarUsuarioDTO dto)
        {
            var usuario = _context.Usuarios.Find(id);
            if (usuario == null) throw new Exception("Usuário não encontrado.");

            usuario.NomeUsuario = dto.NomeUsuario;
            usuario.Email = dto.Email;
            _context.SaveChanges();
        }

        public void Deletar(Guid id)
        {
            var usuario = _context.Usuarios.Find(id);
            if (usuario == null) throw new Exception("Usuário não encontrado.");

            _context.Usuarios.Remove(usuario);
            _context.SaveChanges();
        }
    }
}