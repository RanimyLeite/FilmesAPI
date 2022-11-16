using AutoMapper;
using FilmesAPI.Data;
using FilmesAPI.Data.Dtos;
using FilmesAPI.Models;
using FilmesAPI.Services;
using FluentResults;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FilmesAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FilmeController : ControllerBase
    {
        private FilmeService _filmeService;

        public FilmeController(FilmeService filmeService)
        {
            _filmeService = filmeService;
        }

        [HttpPost]
        public IActionResult AddFilme([FromBody] CreateFilmeDto filmeDto)
        {
            ReadFilmeDto readFilmeDto = _filmeService.AddFilme(filmeDto);

            return CreatedAtAction(nameof(GetFilmeById), new { Id = readFilmeDto.Id }, readFilmeDto);
        }

        [HttpGet]
        public IActionResult GetFilmes([FromQuery] int? classificacaoEtaria = null)
        {
            List<ReadFilmeDto> readFilmeDto = _filmeService.GetFilmes(classificacaoEtaria);

            if(readFilmeDto != null)
            {
                return Ok(readFilmeDto);
            }

            return NotFound();
        }

        [HttpGet("{id}")]
        //IActionResult serve para podermos retornar Ok, NotFound e outros verbos http
        public IActionResult GetFilmeById(int id)
        {
            ReadFilmeDto readFilmeDto = _filmeService.GetFilmeById(id);

            if (readFilmeDto != null)
            {
                return Ok(readFilmeDto);
            }

            return NotFound();
        }

        [HttpPut("{id}")]
        public IActionResult UpdateFilme(int id, [FromBody] UpdateFilmeDto filmeDto)
        {
            Result resultado = _filmeService.UpdateFilme(id, filmeDto);

            if (resultado.IsFailed) return NotFound();
            
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteFilme(int id)
        {
            Result resultado = _filmeService.DeleteFilme(id);

            if (resultado.IsFailed) return NotFound();

            return NoContent();
        }
    }
}
