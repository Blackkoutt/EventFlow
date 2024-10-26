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

namespace EventFlowAPI.Logic.Services.CRUDServices.Services
{
    public sealed class SeatTypeService(IUnitOfWork unitOfWork) :
        GenericService<
            SeatType,
            SeatTypeRequestDto,
            SeatTypeResponseDto
        >(unitOfWork),
        ISeatTypeService
    {
        public sealed override async Task<Result<IEnumerable<SeatTypeResponseDto>>> GetAllAsync(QueryObject query)
        {
            var seatTypeQuery = query as SeatTypeQuery;
            if (seatTypeQuery == null)
                return Result<IEnumerable<SeatTypeResponseDto>>.Failure(QueryError.BadQueryObject);

            var records = await _repository.GetAllAsync(q => q.ByName(seatTypeQuery)
                                                              .SortBy(seatTypeQuery.SortBy, seatTypeQuery.SortDirection));
            var response = MapAsDto(records);
            return Result<IEnumerable<SeatTypeResponseDto>>.Success(response);
        }

        protected async sealed override Task<bool> IsSameEntityExistInDatabase(SeatTypeRequestDto entityDto, int? id = null)
        {
            var entities = await _repository.GetAllAsync(q =>
                      q.Where(entity => entity.Name == entityDto.Name)
                  );

            return IsEntityWithOtherIdExistInList(entities, id);
        }
    }
}
