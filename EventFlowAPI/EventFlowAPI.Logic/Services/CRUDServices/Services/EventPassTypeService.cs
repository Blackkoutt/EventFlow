using EventFlowAPI.DB.Entities;
using EventFlowAPI.Logic.DTO.RequestDto;
using EventFlowAPI.Logic.DTO.ResponseDto;
using EventFlowAPI.Logic.Errors;
using EventFlowAPI.Logic.Extensions;
using EventFlowAPI.Logic.Query.Abstract;
using EventFlowAPI.Logic.Query;
using EventFlowAPI.Logic.ResultObject;
using EventFlowAPI.Logic.Services.CRUDServices.Interfaces;
using EventFlowAPI.Logic.Services.CRUDServices.Services.BaseServices;
using EventFlowAPI.Logic.UnitOfWork;
using EventFlowAPI.Logic.DTO.UpdateRequestDto;
using EventFlowAPI.Logic.DTO.Interfaces;
using EventFlowAPI.Logic.Identity.Helpers;

namespace EventFlowAPI.Logic.Services.CRUDServices.Services
{
    public sealed class EventPassTypeService(IUnitOfWork unitOfWork, IUserService userService) :
        GenericService<
            EventPassType,
            EventPassTypeRequestDto,
            UpdateEventPassTypeRequestDto,
            EventPassTypeResponseDto
        >(unitOfWork, userService),
        IEventPassTypeService
    {
        public sealed override async Task<Result<IEnumerable<EventPassTypeResponseDto>>> GetAllAsync(QueryObject query)
        {
            var eventPassTypeQuery = query as EventPassTypeQuery;
            if (eventPassTypeQuery == null)
                return Result<IEnumerable<EventPassTypeResponseDto>>.Failure(QueryError.BadQueryObject);

            var records = await _repository.GetAllAsync(q => q.Where(s => !s.IsDeleted)
                                                              .ByName(eventPassTypeQuery)
                                                              .ByPrice(eventPassTypeQuery)
                                                              .SortBy(eventPassTypeQuery.SortBy, eventPassTypeQuery.SortDirection));
            var response = MapAsDto(records);
            return Result<IEnumerable<EventPassTypeResponseDto>>.Success(response);
        }

        public sealed override async Task<Result<object>> DeleteAsync(int id)
        {
            var validationResult = await ValidateBeforeDelete(id);
            if (!validationResult.IsSuccessful)
                return Result<object>.Failure(validationResult.Error);

            var entity = validationResult.Value.Entity;
            var user = validationResult.Value.User;

            // Meybe only active 
            if (entity.EventPasses.Any())
            {
                entity.IsDeleted = true;
                entity.DeleteDate = DateTime.Now;
                _repository.Update(entity);
            }
            else
            {
                _repository.Delete(entity);
            }

            await _unitOfWork.SaveChangesAsync();

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
                if (entity == null || entity.IsDeleted)
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
                        !entity.IsDeleted
                      ))).Any();

            return Result<bool>.Success(result);
        }
    }
}
