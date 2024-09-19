using EventFlowAPI.DB.Entities;
using EventFlowAPI.Logic.DTO.RequestDto;
using EventFlowAPI.Logic.DTO.ResponseDto;
using EventFlowAPI.Logic.Identity.DTO.RequestDto;
using EventFlowAPI.Logic.ResultObject;
using EventFlowAPI.Logic.Services.CRUDServices.Interfaces.BaseInterfaces;

namespace EventFlowAPI.Logic.Services.CRUDServices.Interfaces
{
    public interface IUserService :
        IGenericService<
            User,
            UserRegisterRequestDto,
            UserResponseDto
        >
    {
        Task<Result<UserResponseDto>> GetCurrentUser();
        Task<Result<UserResponseDto>> GetOneAsync(string? id);
    }
}
