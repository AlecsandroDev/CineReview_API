using CineReview.DTOs;
using CineReview.Services;
using Microsoft.AspNetCore.Mvc;

namespace CineReview.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EquipeController : ControllerBase
    {
        private readonly IEquipeService _service;

        public EquipeController(IEquipeService service)
        {
            _service = service;
        }

        [HttpPost("ator")]
        public IActionResult AdicionarAtor([FromBody] CriarAtorDto dto)
        {
            try { return Ok(_service.AdicionarAtor(dto)); }
            catch (Exception ex) { return BadRequest(ex.Message); }
        }

        [HttpPost("tecnico")]
        public IActionResult AdicionarTecnico([FromBody] CriarTecnicoDto dto)
        {
            try { return Ok(_service.AdicionarTecnico(dto)); }
            catch (Exception ex) { return BadRequest(ex.Message); }
        }

        [HttpGet("midia/{midiaId}")]
        public IActionResult ListarPorMidia(Guid midiaId)
        {
            return Ok(_service.ListarPorMidia(midiaId));
        }

        [HttpGet("temporada/{temporadaId}")]
        public IActionResult ListarPorTemporada(Guid temporadaId)
        {
            return Ok(_service.ListarPorTemporada(temporadaId));
        }
    }
}