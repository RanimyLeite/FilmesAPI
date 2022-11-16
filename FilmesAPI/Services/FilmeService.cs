using AutoMapper;
using FilmesAPI.Data;
using FilmesAPI.Data.Dtos;
using FilmesAPI.Models;
using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FilmesAPI.Services
{
    public class FilmeService
    {
        private AppDbContext _context;
        private IMapper _mapper;

        public FilmeService(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public ReadFilmeDto AddFilme(CreateFilmeDto filmeDto)
        {
            //Usando o automapper vamos converter de FilmeDto para um Filme
            Filme filme = _mapper.Map<Filme>(filmeDto);

            //Adicionando filme ao banco usando o contexto
            _context.Filmes.Add(filme);

            //Salva no banco as informações do filme criado a cima
            _context.SaveChanges();

            return _mapper.Map<ReadFilmeDto>(filme);

        }

        public List<ReadFilmeDto> GetFilmes(int? classificacaoEtaria = null)
        {
            List<Filme> filmes;
            if (classificacaoEtaria == null)
            {
                filmes = _context.Filmes.ToList();
            }
            else
            {
                filmes = _context
                .Filmes.Where(filmes => filmes.ClassificacaoEtaria <= classificacaoEtaria).ToList();
            }

            if (filmes != null)
            {
                List<ReadFilmeDto> readFilmeDto = _mapper.Map<List<ReadFilmeDto>>(filmes);
                return readFilmeDto;
            }

            return null;
        }

        public ReadFilmeDto GetFilmeById(int id)
        {
            //Intera uma lista de filmes procurando com o filme que tenha o id igual ao da condição ou retorna null.
            Filme filme = _context.Filmes.FirstOrDefault(filme => filme.Id == id);

            if (filme != null)
            {
                //Usando o automapper vamos converter de Filme para um FilmeDto
                ReadFilmeDto filmeDto = _mapper.Map<ReadFilmeDto>(filme);

                return filmeDto;
            }

            return null;
        }

        public Result UpdateFilme(int id, UpdateFilmeDto filmeDto)
        {
            Filme filme = _context.Filmes.FirstOrDefault(filme => filme.Id == id);

            if (filme == null)
            {
                return Result.Fail("Filme não encontrado!");
            }

            //Nesse caso ele sobrescreve as informações de filme com as informações do filmeDto
            _mapper.Map(filmeDto, filme);
            _context.SaveChanges();
            return Result.Ok();
        }

        public Result DeleteFilme(int id)
        {
            Filme filme = _context.Filmes.FirstOrDefault(filme => filme.Id == id);
            if (filme == null)
            {
                return Result.Fail("Filme não encontrado!");
            }
            _context.Remove(filme);
            _context.SaveChanges();

            return Result.Ok();
        }
    }
}
