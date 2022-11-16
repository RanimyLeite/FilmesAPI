using FilmesAPI.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FilmesAPI.Data.Dtos.Sessao
{
    public class ReadSessaoDto
    {
        public int Id { get; set; }
        public virtual Cinema Cinema { get; set; }
        public virtual Filme Filme { get; set; }
        public DateTime HorarioDeInicio { get; set; }
        public DateTime HorarioDeEncerramento { get; set; }
    }
}
