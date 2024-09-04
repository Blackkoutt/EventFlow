using EventFlowAPI.Logic.ResultObject;

namespace EventFlowAPI.Logic.Services.CRUDServices.Interfaces.BaseInterfaces
{
    public interface IGenericService<TEntity, TRequestDto, TResponseDto>
        where TEntity : class
        where TRequestDto : class
        where TResponseDto : class
    {
        Task<Result<TResponseDto>> AddAsync(TRequestDto? requestDto);
        Task<Result<IEnumerable<TResponseDto>>> GetAllAsync();
        Task<Result<TResponseDto>> GetOneAsync(int id);
        Task<Result<TResponseDto>> UpdateAsync(int id, TRequestDto? requestDto);
        Task<Result<TResponseDto>> DeleteAsync(int id);

    }
}
