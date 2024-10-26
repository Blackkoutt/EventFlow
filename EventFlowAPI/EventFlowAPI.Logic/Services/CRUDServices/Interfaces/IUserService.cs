using EventFlowAPI.Logic.DTO.ResponseDto;
using EventFlowAPI.Logic.ResultObject;

namespace EventFlowAPI.Logic.Services.CRUDServices.Interfaces
{
    public interface IUserService
    {
        Task<Result<UserResponseDto>> GetCurrentUser();
        Task<Result<UserResponseDto>> GetOneAsync(string? id);
    }
}
