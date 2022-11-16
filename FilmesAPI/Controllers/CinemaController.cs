using AutoMapper;
using FilmesApi.Data.Dtos;
using FilmesAPI.Data.Dtos;
using FilmesAPI.Services;
using FluentResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;

namespace FilmesAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CinemaController : ControllerBase
    {
        private CinemaService _cinemaService;

        public CinemaController(CinemaService cinemaService)
        {
            _cinemaService = cinemaService;
        }


        [HttpPost]
        public IActionResult AdicionaCinema([FromBody] CreateCinemaDto cinemaDto)
        {
            ReadCinemaDto cinema = _cinemaService.AdicionaCinema(cinemaDto);

            return CreatedAtAction(nameof(RecuperaCinemasPorId), new { Id = cinema.Id }, cinema);
        }

        [HttpGet]
        public IActionResult RecuperaCinemas()
        {
            List<ReadCinemaDto> cinemas = _cinemaService.RecuperaCinemas();

            if(cinemas != null)
            {
                return Ok(cinemas);
            }

            return NotFound();
        }

        [HttpGet("{id}")]
        public IActionResult RecuperaCinemasPorId(int id)
        {
            ReadCinemaDto cinemas = _cinemaService.RecuperaCinemasPorId(id);

            if (cinemas != null)
            {
                return Ok(cinemas);
            }
            
            return NotFound();
        }

        [HttpPut("{id}")]
        public IActionResult AtualizaCinema(int id, [FromBody] UpdateCinemaDto cinemaDto)
        {
            Result resultado = _cinemaService.AtualizaCinema(id, cinemaDto);

            if (resultado.IsFailed) return NotFound();
            
            return NoContent();
        }


        [HttpDelete("{id}")]
        public IActionResult DeletaCinema(int id)
        {
            Result resultado = _cinemaService.DeletaCinema(id);

            if (resultado.IsFailed) return NotFound();

            return NoContent();
        }

    }
}
