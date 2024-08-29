using EventFlowAPI.DB.Entities;
using EventFlowAPI.Logic.DTO.ResponseDto;
using EventFlowAPI.Logic.Errors;
using EventFlowAPI.Logic.Identity.DTO.RequestDto;
using EventFlowAPI.Logic.Identity.Services.Interfaces;
using EventFlowAPI.Logic.Mapper.Extensions;
using EventFlowAPI.Logic.Repositories.Interfaces;
using EventFlowAPI.Logic.ResultObject;
using EventFlowAPI.Logic.Services.Interfaces;
using EventFlowAPI.Logic.Services.Services.BaseServices;
using EventFlowAPI.Logic.UnitOfWork;

namespace EventFlowAPI.Logic.Services.Services
{
    public sealed class UserService(IUnitOfWork unitOfWork, IAuthService authService) :
        GenericService<
            User,
            UserRegisterRequestDto,
            UserResponseDto
        >(unitOfWork),
        IUserService
    {
        private readonly IAuthService _authService = authService;
        public async Task<Result<UserResponseDto>> GetCurrentUserInfo()
        {
            var getUserIdResult = await _authService.GetCurrentUserId();
            if (!getUserIdResult.IsSuccessful)
            {
                return Result<UserResponseDto>.Failure(getUserIdResult.Error);
            }
           return await GetOneAsync(getUserIdResult.Value);
        }
        protected sealed override UserResponseDto MapAsDto(User entity)
        {
            var userDto = entity.AsDto<UserResponseDto>();
            userDto.UserData = entity.UserData?.AsDto<UserDataResponseDto>();
            userDto.UserRoles = entity.Roles.Select(r => r.Name).ToList();
            return userDto;
        }
                                        
        public async Task<Result<UserResponseDto>> GetOneAsync(string? id)
        {
            if (id == null || id == string.Empty)
            {
                return Result<UserResponseDto>.Failure(Error.RouteParamOutOfRange);
            }

            var repository = _repository as IUserRepository;

            var record = await repository!.GetOneAsync(id);

            if (record == null)
            {
                return Result<UserResponseDto>.Failure(Error.NotFound);
            }

            var response = MapAsDto(record);

            return Result<UserResponseDto>.Success(response);
        }

        protected sealed override Task<bool> IsSameEntityExistInDatabase(UserRegisterRequestDto entityDto, int? id = null)
        {
            throw new NotImplementedException();
        }
    }
}
