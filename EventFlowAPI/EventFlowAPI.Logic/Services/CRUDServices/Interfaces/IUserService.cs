using EventFlowAPI.Logic.DTO.RequestDto;
using EventFlowAPI.Logic.DTO.ResponseDto;
using EventFlowAPI.Logic.Helpers;
using EventFlowAPI.Logic.ResultObject;

namespace EventFlowAPI.Logic.Services.CRUDServices.Interfaces
{
    public interface IUserService
    {
        Task<Result<BlobResponseDto>> GetUserPhoto();
        Task<Result<object>> SetUserInfo(UserDataRequestDto userDataRequestDto);
    }
}
