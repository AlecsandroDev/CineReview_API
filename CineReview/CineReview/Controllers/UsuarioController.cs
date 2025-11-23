using CineReview.DTOs;
using CineReview.Service.Usuario;
using CineReview.Services;
using Microsoft.AspNetCore.Mvc;

namespace CineReview.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsuarioController : ControllerBase
    {
        private readonly IUsuarioService _service;

        public UsuarioController(IUsuarioService service)
        {
            _service = service;
        }

        [HttpPost("cadastrar")]
        public IActionResult Cadastrar([FromBody] CriarUsuarioDTO dto)
        {
            try
            {
                var usuario = _service.Cadastrar(dto);
                return CreatedAtAction(nameof(ListarTodos), new { id = usuario.Id }, usuario);
            }
            catch (Exception ex) { return BadRequest(ex.Message); }
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginUsuarioDTO dto)
        {
            try { return Ok(_service.Login(dto)); }
            catch (Exception ex) { return Unauthorized(ex.Message); }
        }

        [HttpGet]
        public IActionResult ListarTodos()
        {
            return Ok(_service.ListarTodos());
        }

        [HttpPut("{id}")]
        public IActionResult Atualizar(Guid id, [FromBody] AtualizarUsuarioDTO dto)
        {
            try
            {
                _service.Atualizar(id, dto);
                return NoContent();
            }
            catch (Exception ex) { return NotFound(ex.Message); }
        }

        [HttpDelete("{id}")]
        public IActionResult Deletar(Guid id)
        {
            try
            {
                _service.Deletar(id);
                return NoContent();
            }
            catch (Exception ex) { return NotFound(ex.Message); }
        }
    }
}