﻿using EventFlowAPI.DB.Entities;
using EventFlowAPI.Logic.DTO.RequestDto;
using EventFlowAPI.Logic.DTO.ResponseDto;
using EventFlowAPI.Logic.Errors;
using EventFlowAPI.Logic.Extensions;
using EventFlowAPI.Logic.Query;
using EventFlowAPI.Logic.ResultObject;
using EventFlowAPI.Logic.Services.CRUDServices.Interfaces;
using EventFlowAPI.Logic.Services.CRUDServices.Services.BaseServices;
using EventFlowAPI.Logic.UnitOfWork;
using EventFlowAPI.Logic.DTO.UpdateRequestDto;
using EventFlowAPI.Logic.DTO.Interfaces;
using EventFlowAPI.Logic.Identity.Helpers;
using EventFlowAPI.DB.Entities.Abstract;

namespace EventFlowAPI.Logic.Services.CRUDServices.Services
{
    public sealed class EventPassTypeService(IUnitOfWork unitOfWork, IUserService userService) :
        GenericService<
            EventPassType,
            EventPassTypeRequestDto,
            UpdateEventPassTypeRequestDto,
            EventPassTypeResponseDto,
            EventPassTypeQuery
        >(unitOfWork, userService),
        IEventPassTypeService
    {
        public sealed override async Task<Result<IEnumerable<EventPassTypeResponseDto>>> GetAllAsync(EventPassTypeQuery query)
        {
            var records = await _repository.GetAllAsync(q => q.Where(ept => !ept.IsDeleted && !ept.IsSoftUpdated)
                                                              .ByName(query)
                                                              .ByPrice(query)
                                                              .SortBy(query.SortBy, query.SortDirection)
                                                              .GetPage(query.PageNumber, query.PageSize));
            var response = MapAsDto(records);
            return Result<IEnumerable<EventPassTypeResponseDto>>.Success(response);
        }

        public sealed override async Task<Result<EventPassTypeResponseDto>> UpdateAsync(int id, UpdateEventPassTypeRequestDto? requestDto)
        {
            var error = await ValidateEntity(requestDto, id);
            if (error != Error.None)
                return Result<EventPassTypeResponseDto>.Failure(error);

            var oldEntity = await _repository.GetOneAsync(id);
            if (oldEntity == null)
                return Result<EventPassTypeResponseDto>.Failure(Error.NotFound);

            await UpdateEntity(oldEntity, requestDto!, isSoftUpdate: oldEntity.EventPasses.Any());

            return Result<EventPassTypeResponseDto>.Success();
        }

        public sealed override async Task<Result<object>> DeleteAsync(int id)
        {
            var validationResult = await ValidateBeforeDelete(id);
            if (!validationResult.IsSuccessful)
                return Result<object>.Failure(validationResult.Error);

            var entity = validationResult.Value.Entity;
            var user = validationResult.Value.User;

            await DeleteEntity(entity, isSoftDelete: entity.EventPasses.Any());

            return Result<object>.Success();
        }

        protected sealed override async Task<Error> ValidateEntity(IRequestDto? requestDto, int? id = null)
        {
            if (id != null && id < 0)
                return Error.RouteParamOutOfRange;

            if (requestDto == null)
                return Error.NullParameter;

            if (id == null)
            {
                var isSameEntityExistsResult = await IsSameEntityExistInDatabase(requestDto, id);
                if (!isSameEntityExistsResult.IsSuccessful) return isSameEntityExistsResult.Error;

                var isSameEntityExistInDb = isSameEntityExistsResult.Value;
                if (isSameEntityExistInDb)
                    return Error.SuchEntityExistInDb;
            }

            var userResult = await _userService.GetCurrentUser();
            if (!userResult.IsSuccessful)
                return userResult.Error;

            var user = userResult.Value;
            if (!user.IsInRole(Roles.Admin))
                return AuthError.UserDoesNotHavePremissionToResource;

            if (id != null)
            {
                var entity = await _repository.GetOneAsync((int)id);
                if (entity == null || entity.IsDeleted || entity.IsSoftUpdated)
                    return Error.NotFound;
            }

            return Error.None;
        }

        protected async sealed override Task<Result<bool>> IsSameEntityExistInDatabase(IRequestDto requestDto, int? id = null)
        {
            if (requestDto is not INameableRequestDto) return Result<bool>.Failure(Error.BadRequestType);

            var result = (await _repository.GetAllAsync(q =>
                      q.Where(entity =>
                        entity.Id != id &&
                        entity.Name.ToLower() == ((INameableRequestDto)requestDto).Name.ToLower() &&
                        !entity.IsDeleted &&
                        !entity.IsSoftUpdated
                      ))).Any();

            return Result<bool>.Success(result);
        }
    }
}
