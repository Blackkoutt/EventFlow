using EventFlowAPI.DB.Entities.Abstract;
using EventFlowAPI.Logic.DTO.Interfaces;
using EventFlowAPI.Logic.Query.Abstract;
using EventFlowAPI.Logic.ResultObject;

namespace EventFlowAPI.Logic.Services.CRUDServices.Interfaces.BaseInterfaces
{
    public interface IGenericService<TEntity, TRequestDto, TUpdateRequestDto, TResponseDto, TQuery>
        where TEntity : class, IEntity
        where TRequestDto : class, IRequestDto
         where TUpdateRequestDto : class, IRequestDto
        where TResponseDto : class, IResponseDto
        where TQuery : QueryObject
    {
        Task<Result<TResponseDto>> AddAsync(TRequestDto? requestDto);
        Task<Result<IEnumerable<TResponseDto>>> GetAllAsync(TQuery query);
        Task<Result<TResponseDto>> GetOneAsync(int id);
        Task<Result<TResponseDto>> UpdateAsync(int id, TUpdateRequestDto? requestDto);
        Task<Result<object>> DeleteAsync(int id);

    }
}
