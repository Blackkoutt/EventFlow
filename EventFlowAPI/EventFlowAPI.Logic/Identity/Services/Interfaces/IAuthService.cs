using EventFlowAPI.DB.Entities;
using EventFlowAPI.Logic.Identity.DTO.RequestDto;
using EventFlowAPI.Logic.Identity.DTO.ResponseDto;
using EventFlowAPI.Logic.Identity.Services.Interfaces.BaseInterfaces;
using EventFlowAPI.Logic.ResultObject;
using Google.Apis.Auth.OAuth2.Responses;

namespace EventFlowAPI.Logic.Identity.Services.Interfaces
{
    public interface IAuthService : IBaseAuthService
    {
        Task<Result<UserRegisterResponseDto>> RegisterUser(UserRegisterRequestDto? requestDto);
        Task<Result<LoginResponseDto>> Login(LoginRequestDto? requestDto);
    }
}
