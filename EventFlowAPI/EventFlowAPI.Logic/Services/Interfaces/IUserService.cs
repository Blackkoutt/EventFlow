using EventFlowAPI.DB.Entities;
using EventFlowAPI.Logic.DTO.RequestDto;
using EventFlowAPI.Logic.DTO.ResponseDto;
using EventFlowAPI.Logic.Identity.DTO.RequestDto;
using EventFlowAPI.Logic.ResultObject;
using EventFlowAPI.Logic.Services.Interfaces.BaseInterfaces;

namespace EventFlowAPI.Logic.Services.Interfaces
{
    public interface IUserService : 
        IGenericService<
            User,
            UserRegisterRequestDto,
            UserResponseDto
        >
    {
        Task<Result<UserResponseDto>> GetCurrentUserInfo();
        Task<Result<UserResponseDto>> GetOneAsync(string? id);
    }
}
