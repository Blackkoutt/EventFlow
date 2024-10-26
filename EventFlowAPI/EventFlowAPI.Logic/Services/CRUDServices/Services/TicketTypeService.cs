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
    public sealed class TicketTypeService(IUnitOfWork unitOfWork) :
        GenericService<
            TicketType,
            TicketTypeRequestDto,
            TicketTypeResponseDto
        >(unitOfWork),
        ITicketTypeService
    {
        public sealed override async Task<Result<IEnumerable<TicketTypeResponseDto>>> GetAllAsync(QueryObject query)
        {
            var ticketTypeQuery = query as TicketTypeQuery;
            if (ticketTypeQuery == null)
                return Result<IEnumerable<TicketTypeResponseDto>>.Failure(QueryError.BadQueryObject);

            var records = await _repository.GetAllAsync(q => q.ByName(ticketTypeQuery)
                                                              .SortBy(ticketTypeQuery.SortBy, ticketTypeQuery.SortDirection));
            var response = MapAsDto(records);
            return Result<IEnumerable<TicketTypeResponseDto>>.Success(response);
        }

        protected async sealed override Task<bool> IsSameEntityExistInDatabase(TicketTypeRequestDto entityDto, int? id = null)
        {
            var entities = await _repository.GetAllAsync(q =>
                      q.Where(entity => entity.Name == entityDto.Name)
                  );

            return IsEntityWithOtherIdExistInList(entities, id);
        }
    }
}
