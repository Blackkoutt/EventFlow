using EventFlowAPI.DB.Entities;
using EventFlowAPI.DB.Entities.Abstract;
using EventFlowAPI.Logic.DTO.ResponseDto;
using EventFlowAPI.Logic.Mapper.Extensions;
using EventFlowAPI.Logic.ResultObject;
using EventFlowAPI.Logic.Services.Interfaces;
using EventFlowAPI.Logic.Services.Services.BaseServices;
using EventFlowAPI.Logic.UnitOfWork;

namespace EventFlowAPI.Logic.Services.Services
{
    public sealed class EventService(IUnitOfWork unitOfWork) : GenericService<Event, EventResponseDto>(unitOfWork), IEventService
    {
        public override sealed async Task<Result<IEnumerable<EventResponseDto>>> GetAllAsync()
        {
            //AutoMapperMappingException
            var records = await _repository.GetAllAsync();
            var result = records.Select(entity =>
            {
                var entityDto = entity.AsDto<EventResponseDto>();
                entityDto.Category = entity.Category?.AsDto<EventCategoryResponseDto>();
                entityDto.Details = entity.Details?.AsDto<EventDetailsResponseDto>();
                entityDto.Hall = entity.Hall?.AsDto<HallResponseDto>();
                return entityDto;
            });
            return Result<IEnumerable<EventResponseDto>>.Success(result);
        }
        public override sealed async Task<Result<EventResponseDto>> GetOneAsync(int id)
        {
            // ArgumentOutOfRangeException, KeyNotFoundException, AutoMapperMappingException
            var record = (IEntity?)await _repository.GetOneAsync(id);
            var result = record.AsDto<EventResponseDto>();
            return Result<EventResponseDto>.Success(result);
        }
    }
}
