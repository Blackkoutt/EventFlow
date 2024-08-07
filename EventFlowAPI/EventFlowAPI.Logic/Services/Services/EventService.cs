using EventFlowAPI.DB.Entities;
using EventFlowAPI.DB.Entities.Abstract;
using EventFlowAPI.Logic.DTO.ResponseDto;
using EventFlowAPI.Logic.Mapper.Extensions;
using EventFlowAPI.Logic.Services.Interfaces;
using EventFlowAPI.Logic.Services.Services.BaseServices;
using EventFlowAPI.Logic.UnitOfWork;

namespace EventFlowAPI.Logic.Services.Services
{
    public sealed class EventService(IUnitOfWork unitOfWork) : GenericService<Event, EventResponseDto>(unitOfWork), IEventService
    {
        public override sealed async Task<IEnumerable<EventResponseDto>> GetAllAsync()
        {
            //AutoMapperMappingException
            try
            {
                var records = await _repository.GetAllAsync();
                return records.Select(entity =>
                {
                    var entityDto = entity.AsDto<EventResponseDto>();
                    entityDto.Category = entity.Category?.AsDto<EventCategoryResponseDto>();
                    entityDto.Details = entity.Details?.AsDto<EventDetailsResponseDto>();
                    entityDto.Hall = entity.Hall?.AsDto<HallResponseDto>();
                    return entityDto;
                });
            }
            catch
            {
                throw;
            }
        }
        public override sealed async Task<EventResponseDto> GetOneAsync(int id)
        {
            // ArgumentOutOfRangeException, KeyNotFoundException, AutoMapperMappingException
            try
            {
                var record = (IEntity)await _repository.GetOneAsync(id);
                return record.AsDto<EventResponseDto>();
            }
            catch
            {
                throw;
            }

        }
    }
}
