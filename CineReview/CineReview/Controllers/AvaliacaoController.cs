using CineReview.DTOs;
using CineReview.Service.Avaliacao;
using CineReview.Services;
using Microsoft.AspNetCore.Mvc;

namespace CineReview.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AvaliacaoController : ControllerBase
    {
        private readonly IAvaliacaoService _service;

        public AvaliacaoController(IAvaliacaoService service)
        {
            _service = service;
        }

        [HttpPost]
        public IActionResult Avaliar([FromBody] CriarAvaliacaoDto dto)
        {
            try
            {
                var resultado = _service.Avaliar(dto);
                return Ok(resultado);
            }
            catch (Exception ex) { return BadRequest(ex.Message); }
        }

        [HttpGet("midia")]
        public IActionResult ListarTodos()
        {
            return Ok(_service.ListarTodos());
        }

        [HttpGet("midia/{id}")]
        public IActionResult ListarPorMidia(Guid id)
        {
            return Ok(_service.ListarPorMidia(id));
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