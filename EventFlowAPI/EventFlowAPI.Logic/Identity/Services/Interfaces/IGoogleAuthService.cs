using EventFlowAPI.Logic.Identity.DTO.ResponseDto;
using EventFlowAPI.Logic.Identity.Services.Interfaces.BaseInterfaces;
using EventFlowAPI.Logic.ResultObject;

namespace EventFlowAPI.Logic.Identity.Services.Interfaces
{
    public interface IGoogleAuthService : IBaseAuthService
    {
        Task<Result<LoginResponseDto>> LoginViaGoogle(string? code);

    }
}
