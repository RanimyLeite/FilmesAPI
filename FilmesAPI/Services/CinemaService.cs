using AutoMapper;
using FilmesApi.Data.Dtos;
using FilmesAPI.Data;
using FilmesAPI.Data.Dtos;
using FilmesAPI.Models;
using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FilmesAPI.Services
{
    public class CinemaService
    {
        private AppDbContext _context;
        private IMapper _mapper;

        public CinemaService(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public ReadCinemaDto AdicionaCinema(CreateCinemaDto cinemaDto)
        {
            Cinema cinema = _mapper.Map<Cinema>(cinemaDto);
            _context.Cinemas.Add(cinema);
            _context.SaveChanges();
            return _mapper.Map<ReadCinemaDto>(cinema);
        }

        public List<ReadCinemaDto> RecuperaCinemas()
        {
            List<Cinema> cinemas = _context.Cinemas.ToList();

            return _mapper.Map<List<ReadCinemaDto>>(cinemas);
        }

        public ReadCinemaDto RecuperaCinemasPorId(int id)
        {
            Cinema cinema = _context.Cinemas.FirstOrDefault(cinema => cinema.Id == id);
            if (cinema != null)
            {
                ReadCinemaDto cinemaDto = _mapper.Map<ReadCinemaDto>(cinema);

                return cinemaDto;
            }
            return null;
        }

        public Result AtualizaCinema(int id, UpdateCinemaDto cinemaDto)
        {
            Cinema cinema = _context.Cinemas.FirstOrDefault(cinema => cinema.Id == id);
            if (cinema == null)
            {
                return Result.Fail("Cinema não encontrado!");
            }
            _mapper.Map(cinemaDto, cinema);
            _context.SaveChanges();

            return Result.Ok();
        }

        public Result DeletaCinema(int id)
        {
            Cinema cinema = _context.Cinemas.FirstOrDefault(cinema => cinema.Id == id);
            if (cinema == null)
            {
                return Result.Fail("Cinema não encontrado!");
            }
            _context.Remove(cinema);
            _context.SaveChanges();

            return Result.Ok();
        }
    }
}
