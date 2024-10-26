using EventFlowAPI.DB.Entities;
using EventFlowAPI.Logic.DTO.RequestDto;
using EventFlowAPI.Logic.DTO.ResponseDto;
using EventFlowAPI.Logic.Errors;
using EventFlowAPI.Logic.Extensions;
using EventFlowAPI.Logic.Mapper.Extensions;
using EventFlowAPI.Logic.Query.Abstract;
using EventFlowAPI.Logic.Query;
using EventFlowAPI.Logic.ResultObject;
using EventFlowAPI.Logic.Services.CRUDServices.Interfaces;
using EventFlowAPI.Logic.UnitOfWork;
using EventFlowAPI.Logic.DTO.UpdateRequestDto;
using EventFlowAPI.Logic.Services.CRUDServices.Services.BaseServices;
using EventFlowAPI.Logic.DTO.Interfaces;
using EventFlowAPI.Logic.Identity.Helpers;
using System.Linq;

namespace EventFlowAPI.Logic.Services.CRUDServices.Services
{
    public sealed class HallTypeService(IUnitOfWork unitOfWork, IUserService userService) :
        GenericService<
            HallType,
            HallTypeRequestDto,
            UpdateHallTypeRequestDto,
            HallTypeResponseDto
        >(unitOfWork, userService),
        IHallTypeService
    {
        public sealed override async Task<Result<IEnumerable<HallTypeResponseDto>>> GetAllAsync(QueryObject query)
        {
            var hallTypeQuery = query as HallTypeQuery;
            if (hallTypeQuery == null)
                return Result<IEnumerable<HallTypeResponseDto>>.Failure(QueryError.BadQueryObject);

            var records = await _repository.GetAllAsync(q => q.Where(s => !s.IsDeleted)
                                                              .ByName(hallTypeQuery)
                                                              .SortBy(hallTypeQuery.SortBy, hallTypeQuery.SortDirection));
            var response = MapAsDto(records);
            return Result<IEnumerable<HallTypeResponseDto>>.Success(response);
        }

        public sealed override async Task<Result<HallTypeResponseDto>> AddAsync(HallTypeRequestDto? requestDto)
        {
            var validationError = await ValidateEntity(requestDto);
            if (validationError != Error.None)
                return Result<HallTypeResponseDto>.Failure(validationError);

            var equipments = await _unitOfWork.GetRepository<Equipment>().GetAllAsync(q =>
                            q.Where(e => requestDto!.EquipmentIds.Contains(e.Id)));

            var hallType = new HallType
            {
                Name = requestDto!.Name,
                Description = requestDto!.Description,
                Equipments = equipments.ToList()
            };

            await _repository.AddAsync(hallType);
            await _unitOfWork.SaveChangesAsync();

            var response = MapAsDto(hallType);

            return Result<HallTypeResponseDto>.Success(response);
        }

        public sealed override async Task<Result<HallTypeResponseDto>> UpdateAsync(int id, UpdateHallTypeRequestDto? requestDto)
        {
            var validationError = await ValidateEntity(requestDto, id);
            if (validationError != Error.None)
                return Result<HallTypeResponseDto>.Failure(validationError);

            var hallType = await _repository.GetOneAsync(id);
            if (hallType == null)
                return Result<HallTypeResponseDto>.Failure(Error.NotFound);

            var equipments = await _unitOfWork.GetRepository<Equipment>().GetAllAsync(q =>
                q.Where(e => requestDto!.EquipmentIds.Contains(e.Id)));

            hallType.Name = requestDto!.Name;
            hallType.Description = requestDto!.Description; 
            hallType.Equipments = equipments.ToList();

            _repository.Update(hallType);
            await _unitOfWork.SaveChangesAsync();

            var response = MapAsDto(hallType);

            return Result<HallTypeResponseDto>.Success(response);
        }

        public sealed override async Task<Result<object>> DeleteAsync(int id)
        {
            var validationResult = await ValidateBeforeDelete(id);
            if (!validationResult.IsSuccessful)
                return Result<object>.Failure(validationResult.Error);

            var entity = validationResult.Value.Entity;
            var user = validationResult.Value.User;

            // Meybe only active 
            if (entity.Halls.Any())
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
            var userResult = await _userService.GetCurrentUser();
            if (!userResult.IsSuccessful)
                return userResult.Error;

            var user = userResult.Value;
            if (!user.IsInRole(Roles.Admin))
                return AuthError.UserDoesNotHavePremissionToResource;

            if (id != null && id < 0)
                return Error.RouteParamOutOfRange;

            if (requestDto == null)
                return Error.NullParameter;

            List<int> equipmentIds = [];
            switch (requestDto)
            {
                case HallTypeRequestDto hallTypeRequestDto:
                    equipmentIds = hallTypeRequestDto.EquipmentIds;
                    break;
                case UpdateHallTypeRequestDto updateHallTypeRequestDto:
                    equipmentIds = updateHallTypeRequestDto.EquipmentIds;
                    break;
                default:
                    return Error.BadRequestType;
            }

            var isSameEntityExistsResult = await IsSameEntityExistInDatabase(requestDto, id);
            if (!isSameEntityExistsResult.IsSuccessful) return isSameEntityExistsResult.Error;

            var isSameEntityExistInDb = isSameEntityExistsResult.Value;
            if (isSameEntityExistInDb)
                return Error.SuchEntityExistInDb;

            if (equipmentIds.Count() != equipmentIds.Distinct().Count())
                return TicketTypeError.TicketDuplicates;
            var equipments = await _unitOfWork.GetRepository<Equipment>().GetAllAsync();
            var equipmentIdsInDb = equipments.Select(e => e.Id).ToList();
            if (!equipmentIds.All(id => equipmentIdsInDb.Contains(id)))
                return TicketTypeError.NotFound;

            if (id != null)
            {
                var entity = await _repository.GetOneAsync((int)id);
                if (entity == null || entity.IsDeleted)
                    return Error.NotFound;
            }

            return Error.None;
        }

        protected sealed override async Task<Result<bool>> IsSameEntityExistInDatabase(IRequestDto requestDto, int? id = null)
        {
            var result = (await _repository.GetAllAsync(q =>
                      q.Where(entity =>
                        entity.Id != id &&
                        entity.Name.ToLower() == ((INameableRequestDto)requestDto).Name.ToLower() &&
                        !entity.IsDeleted
                      ))).Any();

            return Result<bool>.Success(result);
        }

        protected sealed override HallTypeResponseDto MapAsDto(HallType entity)
        {
            var responseDto = entity.AsDto<HallTypeResponseDto>();
            responseDto.Equipments = entity.Equipments.Select(eq => eq.AsDto<EquipmentResponseDto>()).ToList();
            return responseDto;
        }
    }
}
