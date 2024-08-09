using EventFlowAPI.Logic.DTO.Interfaces;
using EventFlowAPI.Logic.ResultObject;

namespace EventFlowAPI.Logic.Services.Interfaces.BaseInterfaces
{
    public interface IGenericService <TEntity, TResponseDto> 
        where TEntity : class
        where TResponseDto : class
    {
        Task<Result<TResponseDto>> AddAsync(IRequestDto? requestDto);
        Task<Result<IEnumerable<TResponseDto>>> GetAllAsync();
        Task<Result<TResponseDto>> GetOneAsync(int id);
        Task<Result<TResponseDto>> UpdateAsync(int id, IRequestDto requestDto);
        Task<Result<TResponseDto>> DeleteAsync(int id);

    }
}
