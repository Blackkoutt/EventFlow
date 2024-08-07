using EventFlowAPI.Logic.DTO.Interfaces;

namespace EventFlowAPI.Logic.Services.Interfaces.BaseInterfaces
{
    public interface IGenericService <TEntity, TResponseDto> 
        where TEntity : class
        where TResponseDto : class
    {
        Task AddAsync(IRequestDto requestDto);
        Task<IEnumerable<TResponseDto>> GetAllAsync();
        Task<TResponseDto> GetOneAsync(int id);
        Task UpdateAsync(IRequestDto requestDto);
        Task DeleteAsync(int id);

    }
}
