using AutoMapper;
using EventFlowAPI.DB.Entities.Abstract;
using EventFlowAPI.Logic.DTO.Interfaces;

namespace EventFlowAPI.Logic.Mapper.Profiles
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<IRequestDto, IEntity>();
            CreateMap<IEntity, IResponseDto>();
        }
    }
}
