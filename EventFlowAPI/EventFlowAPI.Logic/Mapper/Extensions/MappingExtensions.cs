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
            try
            {
                return _mapper!.Map<T>(entity);
            }
            catch (AutoMapperMappingException) 
            {
                throw;
            }
        }
        public static T AsEntity<T>(this IRequestDto dto)
        {
            try
            {
                return _mapper!.Map<T>(dto);
            }
            catch (AutoMapperMappingException)
            {
                throw;
            }
        }
    }
}
