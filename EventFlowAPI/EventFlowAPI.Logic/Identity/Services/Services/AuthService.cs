using EventFlowAPI.DB.Entities;
using EventFlowAPI.Logic.Errors;
using EventFlowAPI.Logic.Identity.DTO.RequestDto;
using EventFlowAPI.Logic.Identity.DTO.ResponseDto;
using EventFlowAPI.Logic.Identity.Helpers;
using EventFlowAPI.Logic.Identity.Services.Interfaces;
using EventFlowAPI.Logic.Identity.Services.Services.BaseServices;
using EventFlowAPI.Logic.Mapper.Extensions;
using EventFlowAPI.Logic.ResultObject;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;

namespace EventFlowAPI.Logic.Identity.Services.Services
{
    public class AuthService(
        UserManager<User> userManager,
        IHttpContextAccessor httpContextAccessor,
        IConfiguration configuration,
        IJWTGeneratorService jwtGeneratorService) : BaseAuthService(userManager, httpContextAccessor, configuration, jwtGeneratorService), IAuthService
    {

        public async Task<Result<UserRegisterResponseDto>> RegisterUser(UserRegisterRequestDto? requestDto)
        {
            if(requestDto == null)
            {
                return Result<UserRegisterResponseDto>.Failure(Error.NullParameter);
            }

            var isUserWithSameEmailExistInDB = await _userManager.FindByEmailAsync(requestDto.Email);
            if(isUserWithSameEmailExistInDB != null)
            {
                return Result<UserRegisterResponseDto>.Failure(AuthError.EmailAlreadyTaken);
            }

            var user = requestDto.AsEntity<User>();

            var result = await _userManager.CreateAsync(user, requestDto.Password);
            if (!result.Succeeded)
            {
                var errors = result.Errors.ToDictionary(e => e.Code, e => e.Description);
                return Result<UserRegisterResponseDto>.Failure(new AuthError(errors).ErrorsWhileCreatingUser);
            }

            await _userManager.AddToRoleAsync(user, Roles.User.ToString());

            return Result<UserRegisterResponseDto>.Success();
        }

        public async Task<Result<LoginResponseDto>> Login(LoginRequestDto? requestDto)
        {
            if (requestDto == null)
            {
                return Result<LoginResponseDto>.Failure(Error.NullParameter);
            }
            var user = await _userManager.FindByEmailAsync(requestDto.Email);
            if (user == null || !await _userManager.CheckPasswordAsync(user, requestDto.Password))
            {
                return Result<LoginResponseDto>.Failure(AuthError.InvalidEmailOrPassword);
            }
            var roles = await GetRolesForCurrentUser(user);

            var responseDto = new LoginResponseDto
            {
                Token = _jwtGeneratorService.GenerateToken(user, roles),
            };

            return Result<LoginResponseDto>.Success(responseDto);
        }

    }
}
