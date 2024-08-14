using AutoMapper;
using EventFlowAPI.DB.Entities.Abstract;
using EventFlowAPI.Logic.DTO.Interfaces;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;

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
            T result;
            try
            {
                result = _mapper!.Map<T>(entity);
            }
            catch (AutoMapperMappingException)
            {
                throw;
            }
            return result;
        }
        public static T AsEntity<T>(this IRequestDto dto)
        {
            T result;
            try
            {
                result = _mapper!.Map<T>(dto);
            }
            catch (AutoMapperMappingException)
            {
                throw;
            }
            return result;
        }
        public static IEntity MapTo(this IRequestDto dto, IEntity entity)
        {
            try
            {
                return _mapper!.Map(dto, entity);
            }
            catch (AutoMapperMappingException)
            {
                throw;
            }
        }
        public static IEntity MakeCopyFrom(this IEntity targetEntity, IEntity fromEntity)
        {
            try
            {
                return _mapper!.Map(fromEntity, targetEntity);
            }
            catch (AutoMapperMappingException)
            {
                throw;
            }
        }

        public static IRequestDto MapTo(this IEntity entity, IRequestDto dto)
        {
            try
            {
                return _mapper!.Map(entity, dto);
            }
            catch (AutoMapperMappingException)
            {
                throw;
            }
        }
    }
}
