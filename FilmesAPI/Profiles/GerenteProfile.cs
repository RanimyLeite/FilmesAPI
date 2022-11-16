using AutoMapper;
using FilmesApi.Models;
using FilmesAPI.Data.Dtos.Gerente;
using FilmesAPI.Models;

namespace FilmesAPI.Profiles
{
    public class GerenteProfile : Profile
    {
        public GerenteProfile()
        {
            //AutoMap para gravar no banco
            CreateMap<CreateGerenteDto, Gerente>();
            //AutoMap para resgatar do banco
            CreateMap<Gerente, ReadGerenteDto>();
        }
    }
}
