using EventFlowAPI.DB.Entities;
using EventFlowAPI.Logic.Identity.DTO.RequestDto;
using EventFlowAPI.Logic.Identity.DTO.ResponseDto;
using EventFlowAPI.Logic.ResultObject;

namespace EventFlowAPI.Logic.Identity.Services.Interfaces
{
    public interface IAuthService
    {
        Task<Result<UserRegisterResponseDto>> RegisterUser(UserRegisterRequestDto? requestDto);
        Task<Result<LoginResponseDto>> Login(LoginRequestDto? requestDto);
        Task<Result<string>> GetCurrentUserId();
        Task<IList<string>?> GetRolesForCurrentUser(User user);
    }
}
