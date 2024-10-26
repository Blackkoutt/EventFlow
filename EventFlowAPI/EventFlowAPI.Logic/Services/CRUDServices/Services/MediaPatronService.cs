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
    public sealed class MediaPatronService(IUnitOfWork unitOfWork) :
        GenericService<
            MediaPatron,
            MediaPatronRequestDto,
            MediaPatronResponseDto
        >(unitOfWork),
        IMediaPatronService
    {
        public sealed override async Task<Result<IEnumerable<MediaPatronResponseDto>>> GetAllAsync(QueryObject query)
        {
            var mediaPatronQuery = query as MediaPatronQuery;
            if (mediaPatronQuery == null)
                return Result<IEnumerable<MediaPatronResponseDto>>.Failure(QueryError.BadQueryObject);

            var records = await _repository.GetAllAsync(q => q.ByName(mediaPatronQuery)
                                                              .SortBy(mediaPatronQuery.SortBy, mediaPatronQuery.SortDirection));
            var response = MapAsDto(records);
            return Result<IEnumerable<MediaPatronResponseDto>>.Success(response);
        }

        protected async sealed override Task<bool> IsSameEntityExistInDatabase(MediaPatronRequestDto entityDto, int? id = null)
        {
            var entities = await _repository.GetAllAsync(q =>
                     q.Where(entity => entity.Name == entityDto.Name)
                 );

            return IsEntityWithOtherIdExistInList(entities, id);
        }
    }
}
