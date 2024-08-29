using EventFlowAPI.DB.Entities;
using EventFlowAPI.Logic.Errors;
using EventFlowAPI.Logic.Identity.DTO.RequestDto;
using EventFlowAPI.Logic.Identity.DTO.ResponseDto;
using EventFlowAPI.Logic.Identity.Helpers;
using EventFlowAPI.Logic.Identity.Services.Interfaces;
using EventFlowAPI.Logic.Mapper.Extensions;
using EventFlowAPI.Logic.ResultObject;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace EventFlowAPI.Logic.Identity.Services.Services
{
    public class AuthService(
        UserManager<User> userManager,
        IJWTGeneratorService jwtGeneratorService,
        IHttpContextAccessor httpContextAccessor) : IAuthService
    {
        private readonly UserManager<User> _userManager = userManager;
        private readonly IJWTGeneratorService _jwtGeneratorService = jwtGeneratorService;
        private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;

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

            await _userManager.AddToRoleAsync(user, Roles.User);

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
        public async Task<Result<string>> GetCurrentUserId()
        {
            var userEmail = _httpContextAccessor.HttpContext?.User.Identity?.Name;
            if(userEmail == null)
            {
                return Result<string>.Failure(AuthError.CanNotConfirmIdentity);
            }
            var user = await _userManager.FindByEmailAsync(userEmail);
            if (user == null)
            {
                return Result<string>.Failure(AuthError.CanNotFoundUserInDB);
            }
            return Result<string>.Success(user.Id);
        }
            

        public async Task<IList<string>?> GetRolesForCurrentUser(User user) => await _userManager.GetRolesAsync(user);
    }
}
