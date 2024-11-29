using EventFlowAPI.Logic.Errors;
using EventFlowAPI.Logic.Identity.DTO.RequestDto;
using EventFlowAPI.Logic.Identity.DTO.ResponseDto;
using EventFlowAPI.Logic.Identity.Services.Interfaces.BaseInterfaces;
using EventFlowAPI.Logic.ResultObject;

namespace EventFlowAPI.Logic.Identity.Services.Interfaces
{
    public interface IAuthService : IBaseAuthService
    {
        Task<Error> VerifyUser();
        Task<Result<UserRegisterResponseDto>> RegisterUser(UserRegisterRequestDto? requestDto);
        Task<Result<LoginResponseDto>> Login(LoginRequestDto? requestDto);
    }
}
