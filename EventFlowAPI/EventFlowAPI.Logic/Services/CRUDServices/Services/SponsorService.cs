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
    public sealed class SponsorService(IUnitOfWork unitOfWork) :
        GenericService<
            Sponsor,
            SponsorRequestDto,
            SponsorResponseDto
        >(unitOfWork),
        ISponsorService
    {
        public sealed override async Task<Result<IEnumerable<SponsorResponseDto>>> GetAllAsync(QueryObject query)
        {
            var sponsorQuery = query as SponsorQuery;
            if (sponsorQuery == null)
                return Result<IEnumerable<SponsorResponseDto>>.Failure(QueryError.BadQueryObject);

            var records = await _repository.GetAllAsync(q => q.ByName(sponsorQuery)
                                                              .SortBy(sponsorQuery.SortBy, sponsorQuery.SortDirection));
            var response = MapAsDto(records);
            return Result<IEnumerable<SponsorResponseDto>>.Success(response);
        }
        protected async sealed override Task<bool> IsSameEntityExistInDatabase(SponsorRequestDto entityDto, int? id = null)
        {
            var entities = await _repository.GetAllAsync(q =>
                      q.Where(entity => entity.Name == entityDto.Name)
                  );

            return IsEntityWithOtherIdExistInList(entities, id);
        }
    }
}
