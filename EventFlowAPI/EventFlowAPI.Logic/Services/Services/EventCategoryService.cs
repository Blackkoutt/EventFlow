﻿using EventFlowAPI.DB.Entities;
using EventFlowAPI.Logic.DTO.RequestDto;
using EventFlowAPI.Logic.DTO.ResponseDto;
using EventFlowAPI.Logic.Services.Interfaces;
using EventFlowAPI.Logic.Services.Services.BaseServices;
using EventFlowAPI.Logic.UnitOfWork;

namespace EventFlowAPI.Logic.Services.Services
{
    public sealed class EventCategoryService(IUnitOfWork unitOfWork) :
        GenericService<
            EventCategory,
            EventCategoryRequestDto,
            EventCategoryResponseDto
        >(unitOfWork),
        IEventCategoryService
    {
        protected async sealed override Task<bool> IsSameEntityExistInDatabase(EventCategoryRequestDto entityDto, int? id = null)
        {
            var entities = await _repository.GetAllAsync(q =>
                      q.Where(entity => entity.Name == entityDto.Name)
                  );

            return base.IsEntityWithOtherIdExistInList(entities, id);
        }
    }
}
