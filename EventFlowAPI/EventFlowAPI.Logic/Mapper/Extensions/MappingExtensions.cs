using AutoMapper;
using EventFlowAPI.DB.Entities.Abstract;
using EventFlowAPI.Logic.DTO.Interfaces;

namespace EventFlowAPI.Logic.Mapper.Extensions
{
    public static class MappingExtensions
    {
        private static IMapper? _mapper;

        public static void Configure(IMapper mapper)
        {
            _mapper = mapper;
        }
        public static T AsDto<T>(this IEntity entity)
        {
            return _mapper!.Map<T>(entity);
        }
        public static T AsEntity<T>(this IRequestDto dto)
        {
            return _mapper!.Map<T>(dto);
        }
    }
}
