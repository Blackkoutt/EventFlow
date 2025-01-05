using EventFlowAPI.DB.Entities;
using EventFlowAPI.Logic.DTO.RequestDto;
using EventFlowAPI.Logic.DTO.ResponseDto;
using EventFlowAPI.Logic.DTO.UpdateRequestDto;
using EventFlowAPI.Logic.Helpers;
using EventFlowAPI.Logic.Query;
using EventFlowAPI.Logic.ResultObject;
using EventFlowAPI.Logic.Services.CRUDServices.Interfaces.BaseInterfaces;

namespace EventFlowAPI.Logic.Services.CRUDServices.Interfaces
{
    public interface IUserService : IGenericService<
            User,
            UserDataRequestDto,
            UpdateUserDataRequestDto,
            UserResponseDto,
            UserDataQuery
        >
    {
        Task<Result<BlobResponseDto>> GetUserPhoto();
        Task<Result<UserResponseDto>> GetOneAsync(string id);
        Task<Result<object>> SetUserInfo(UserDataRequestDto userDataRequestDto);
    }
}
