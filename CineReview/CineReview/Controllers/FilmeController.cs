using CineReview.DTOs;
using CineReview.Services;
using Microsoft.AspNetCore.Mvc;

namespace CineReview.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FilmeController : ControllerBase
    {
        private readonly IFilmeService _service;

        public FilmeController(IFilmeService service)
        {
            _service = service;
        }

        [HttpPost]
        public IActionResult Cadastrar([FromBody] CriarFilmeDto dto)
        {
            try
            {
                var filme = _service.Cadastrar(dto);
                return CreatedAtAction(nameof(BuscarPorId), new { id = filme.Id }, filme);
            }
            catch (Exception ex) { return BadRequest(ex.Message); }
        }

        [HttpGet]
        public IActionResult ListarTodos()
        {
            return Ok(_service.ListarTodos());
        }

        [HttpGet("{id}")]
        public IActionResult BuscarPorId(Guid id)
        {
            try { return Ok(_service.BuscarPorId(id)); }
            catch (Exception ex) { return NotFound(ex.Message); }
        }

        [HttpPut("{id}")]
        public IActionResult Atualizar(Guid id, [FromBody] CriarFilmeDto dto)
        {
            try
            {
                _service.Atualizar(id, dto);
                return NoContent();
            }
            catch (Exception ex) { return BadRequest(ex.Message); }
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