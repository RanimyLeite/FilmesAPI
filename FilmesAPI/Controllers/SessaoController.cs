using AutoMapper;
using FilmesAPI.Data;
using FilmesAPI.Data.Dtos.Sessao;
using FilmesAPI.Models;
using FilmesAPI.Services;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace FilmesAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SessaoController : ControllerBase
    {
        private SessaoService _sessaoService;

        public SessaoController(SessaoService sessaoService)
        {
            _sessaoService = sessaoService;
        }

        [HttpPost]
        public IActionResult AddSession([FromBody] CreateSessaoDto sessaoDto)
        {
            ReadSessaoDto session = _sessaoService.AddSession(sessaoDto);

            return CreatedAtAction(nameof(GetSessionById), new { Id = session.Id }, session);
        }

        [HttpGet("{id}")]
        public IActionResult GetSessionById(int id)
        {
            ReadSessaoDto session = _sessaoService.GetSessionById(id);

            if (session != null) return Ok(session);

            return NotFound();
        }
    }
}
