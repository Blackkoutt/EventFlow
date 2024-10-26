using EventFlowAPI.Logic.Query.Abstract;
using EventFlowAPI.Logic.ResultObject;

namespace EventFlowAPI.Logic.Services.CRUDServices.Interfaces.BaseInterfaces
{
    public interface IGenericService<TEntity, TRequestDto, TUpdateRequestDto, TResponseDto>
        where TEntity : class
        where TRequestDto : class
        where TResponseDto : class
    {
        Task<Result<TResponseDto>> AddAsync(TRequestDto? requestDto);
        Task<Result<IEnumerable<TResponseDto>>> GetAllAsync(QueryObject query);
        Task<Result<TResponseDto>> GetOneAsync(int id);
        Task<Result<TResponseDto>> UpdateAsync(int id, TUpdateRequestDto? requestDto);
        Task<Result<object>> DeleteAsync(int id);

    }
}
