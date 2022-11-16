using AutoMapper;
using FilmesApi.Models;
using FilmesAPI.Data;
using FilmesAPI.Data.Dtos.Gerente;
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
    public class GerenteController : ControllerBase
    {
        private GerenteService _gerenteService; 

        public GerenteController(GerenteService gerenteSevice)
        {
            //Ingeção de Dependencia
            _gerenteService = gerenteSevice;
        }

        [HttpPost]
        public IActionResult AddGerente([FromBody] CreateGerenteDto gerenteDto)
        {
            ReadGerenteDto gerente = _gerenteService.AddGerente(gerenteDto);

            return CreatedAtAction(nameof(GetGerenteById), new { Id = gerente.Id }, gerente);
        }

        [HttpGet("{id}")]
        public IActionResult GetGerenteById(int id)
        {
            ReadGerenteDto gerente = _gerenteService.GetGerenteById(id);

            if (gerente != null) return Ok(gerente);

            return NotFound();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteGerente(int id)
        {
            Result gerente = _gerenteService.DeleteGerente(id);

            if (gerente.IsFailed) return NotFound();

            return NoContent();
        }
    }
}
