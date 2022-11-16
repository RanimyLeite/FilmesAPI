using AutoMapper;
using FilmesAPI.Data.Dtos.Sessao;
using FilmesAPI.Models;

namespace FilmesAPI.Profiles
{
    public class SessaoProfile : Profile
    {
        public SessaoProfile()
        {
            //AutoMap para gravar no banco
            CreateMap<CreateSessaoDto, Sessao>();

            //AutoMap para resgatar do banco
            CreateMap<Sessao, ReadSessaoDto>() //Essas 3 linhas abaixo serven somente para calcular o horario de inicio da sessão
                .ForMember(dto => dto.HorarioDeInicio, opts => opts
                .MapFrom(dto => 
                dto.HorarioDeEncerramento.AddMinutes(dto.Filme.Duracao*(-1))));
        }
    }

}
