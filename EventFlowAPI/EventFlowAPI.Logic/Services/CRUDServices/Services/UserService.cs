﻿using EventFlowAPI.DB.Entities;
using EventFlowAPI.Logic.DTO.ResponseDto;
using EventFlowAPI.Logic.Errors;
using EventFlowAPI.Logic.Identity.Services.Interfaces;
using EventFlowAPI.Logic.Mapper.Extensions;
using EventFlowAPI.Logic.Repositories.Interfaces;
using EventFlowAPI.Logic.ResultObject;
using EventFlowAPI.Logic.Services.CRUDServices.Interfaces;
using EventFlowAPI.Logic.UnitOfWork;

namespace EventFlowAPI.Logic.Services.CRUDServices.Services
{
    public sealed class UserService(IUnitOfWork unitOfWork, IAuthService authService) : IUserService
    {
        private readonly IAuthService _authService = authService;
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        public async Task<Result<UserResponseDto>> GetCurrentUser()
        {
            var currentUserIdResult = _authService.GetCurrentUserId();
            if (!currentUserIdResult.IsSuccessful)
                return Result<UserResponseDto>.Failure(currentUserIdResult.Error);

            var user = await GetOneAsync(currentUserIdResult.Value);
            if (user is null)
                return Result<UserResponseDto>.Failure(UserError.UserNotFound);

            if (string.IsNullOrEmpty(user.Value.Email))
                return Result<UserResponseDto>.Failure(UserError.UserEmailNotFound);

            return Result<UserResponseDto>.Success(user.Value);
        }
        private UserResponseDto MapAsDto(User entity)
        {
            var userDto = entity.AsDto<UserResponseDto>();
            userDto.UserData = entity.UserData?.AsDto<UserDataResponseDto>();
            userDto.UserRoles = entity.Roles.Select(r => r.Name).ToList();
            return userDto;
        }

        public async Task<Result<UserResponseDto>> GetOneAsync(string? id)
        {
            if (id == null || id == string.Empty)
                return Result<UserResponseDto>.Failure(Error.RouteParamOutOfRange);

            var _userRepository = (IUserRepository)_unitOfWork.GetRepository<User>();
            var record = await _userRepository.GetOneAsync(id);

            if (record == null)
                return Result<UserResponseDto>.Failure(Error.NotFound);

            var response = MapAsDto(record);

            return Result<UserResponseDto>.Success(response);
        }
    }
}
