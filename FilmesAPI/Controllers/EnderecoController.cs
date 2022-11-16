using AutoMapper;
using FilmesApi.Models;
using FilmesAPI.Data;
using FilmesAPI.Data.Dtos;
using FilmesAPI.Models;
using FilmesAPI.Services;
using FluentResults;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FilmesAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class EnderecoController : ControllerBase
    {
        private AppDbContext _context;
        private IMapper _mapper;

        private EnderecoService _enderecoService;

        public EnderecoController(AppDbContext context, IMapper mapper, EnderecoService enderecoService)
        {
            _context = context;
            _mapper = mapper;
            _enderecoService = enderecoService;
        }

        [HttpPost]
        public IActionResult AdicionaEndereco([FromBody] CreateEnderecoDto enderecoDto)
        {
            ReadEnderecoDto endereco = _enderecoService.AdicionaEndereco(enderecoDto);
    
            return CreatedAtAction(nameof(RecuperaEnderecosPorId), new { Id = endereco.Id }, endereco);
        }

        [HttpGet]
        public IActionResult RecuperaEnderecos()
        {
            List<ReadEnderecoDto> enderecos = _enderecoService.RecuperaEnderecos();

            if (enderecos != null)
            {
                return Ok(enderecos);
            }

            return NotFound();
        }

        [HttpGet("{id}")]
        public IActionResult RecuperaEnderecosPorId(int id)
        {
            ReadEnderecoDto enderecoDto = _enderecoService.RecuperaEnderecosPorId(id);

            if (enderecoDto != null) return Ok(enderecoDto);
            
            return NotFound();
        }

        [HttpPut("{id}")]
        public IActionResult AtualizaEndereco(int id, [FromBody] UpdateEnderecoDto enderecoDto)
        {
            Result result = _enderecoService.AtualizaEndereco(id, enderecoDto);

            if (result.IsFailed) return NotFound();

            return NoContent();
        }


        [HttpDelete("{id}")]
        public IActionResult DeletaEndereco(int id)
        {
            Result result = _enderecoService.DeleteEndereco(id);

            if (result.IsFailed) return NotFound();

            return NoContent();
        }

    }
}