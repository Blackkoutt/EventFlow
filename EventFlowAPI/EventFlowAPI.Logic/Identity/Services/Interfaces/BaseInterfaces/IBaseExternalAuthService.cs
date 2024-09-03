using EventFlowAPI.Logic.Identity.DTO.ResponseDto;
using EventFlowAPI.Logic.ResultObject;

namespace EventFlowAPI.Logic.Identity.Services.Interfaces.BaseInterfaces
{
    public interface IBaseExternalAuthService : IBaseAuthService
    {
        string GetLinkToSigninPage();
        Task<Result<LoginResponseDto>> Login(string? code);
    }
}
