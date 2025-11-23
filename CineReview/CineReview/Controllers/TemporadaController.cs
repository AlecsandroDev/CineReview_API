using CineReview.DTOs;
using CineReview.Services;
using Microsoft.AspNetCore.Mvc;

namespace CineReview.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TemporadaController : ControllerBase
    {
        private readonly ITemporadaService _service;

        public TemporadaController(ITemporadaService service)
        {
            _service = service;
        }

        [HttpPost]
        public IActionResult CadastrarTemporada([FromBody] CriarTemporadaDto dto)
        {
            try { return Ok(_service.CadastrarTemporada(dto)); }
            catch (Exception ex) { return BadRequest(ex.Message); }
        }

        [HttpPost("episodio")]
        public IActionResult AdicionarEpisodio([FromBody] CriarEpisodioDto dto)
        {
            try
            {
                _service.AdicionarEpisodio(dto);
                return Ok("Episódio adicionado com sucesso.");
            }
            catch (Exception ex) { return BadRequest(ex.Message); }
        }
    }
}