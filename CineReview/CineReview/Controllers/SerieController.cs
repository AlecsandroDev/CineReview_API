using CineReview.DTOs;
using CineReview.Services;
using Microsoft.AspNetCore.Mvc;

namespace CineReview.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SerieController : ControllerBase
    {
        private readonly ISerieService _service;

        public SerieController(ISerieService service)
        {
            _service = service;
        }

        [HttpPost]
        public IActionResult Cadastrar([FromBody] CriarSerieDto dto)
        {
            try { return Ok(_service.Cadastrar(dto)); }
            catch (Exception ex) { return BadRequest(ex.Message); }
        }

        [HttpGet]
        public IActionResult ListarTodas()
        {
            return Ok(_service.ListarTodas());
        }

        [HttpGet("{id}")]
        public IActionResult BuscarPorId(Guid id)
        {
            try { return Ok(_service.BuscarPorId(id)); }
            catch (Exception ex) { return NotFound(ex.Message); }
        }

        [HttpPut("{id}")]
        public IActionResult Atualizar(Guid id, [FromBody] CriarSerieDto dto)
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