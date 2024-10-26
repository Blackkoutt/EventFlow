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
    public sealed class OrganizerService(IUnitOfWork unitOfWork) :
        GenericService<
            Organizer,
            OrganizerRequestDto,
            OrganizerResponseDto
        >(unitOfWork),
        IOrganizerService
    {
        public sealed override async Task<Result<IEnumerable<OrganizerResponseDto>>> GetAllAsync(QueryObject query)
        {
            var organizerQuery = query as OrganizerQuery;
            if (organizerQuery == null)
                return Result<IEnumerable<OrganizerResponseDto>>.Failure(QueryError.BadQueryObject);

            var records = await _repository.GetAllAsync(q => q.ByName(organizerQuery)
                                                              .SortBy(organizerQuery.SortBy, organizerQuery.SortDirection));
            var response = MapAsDto(records);
            return Result<IEnumerable<OrganizerResponseDto>>.Success(response);
        }
        protected async sealed override Task<bool> IsSameEntityExistInDatabase(OrganizerRequestDto entityDto, int? id = null)
        {
            var entities = await _repository.GetAllAsync(q =>
                      q.Where(entity => entity.Name == entityDto.Name)
                  );

            return IsEntityWithOtherIdExistInList(entities, id);
        }
    }
}
