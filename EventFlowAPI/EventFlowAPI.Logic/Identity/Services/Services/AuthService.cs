using EventFlowAPI.DB.Entities;
using EventFlowAPI.Logic.Errors;
using EventFlowAPI.Logic.Identity.DTO.RequestDto;
using EventFlowAPI.Logic.Identity.DTO.ResponseDto;
using EventFlowAPI.Logic.Identity.Helpers;
using EventFlowAPI.Logic.Identity.Services.Interfaces;
using EventFlowAPI.Logic.Identity.Services.Services.BaseServices;
using EventFlowAPI.Logic.Mapper.Extensions;
using EventFlowAPI.Logic.Repositories.Interfaces;
using EventFlowAPI.Logic.ResultObject;
using EventFlowAPI.Logic.Services.OtherServices.Interfaces;
using EventFlowAPI.Logic.UnitOfWork;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Serilog;

namespace EventFlowAPI.Logic.Identity.Services.Services
{
    public class AuthService(
        UserManager<User> userManager,
        IHttpContextAccessor httpContextAccessor,
        IConfiguration configuration,
        IEmailSenderService emailSender,
        IUnitOfWork unitOfWork,
        IJWTGeneratorService jwtGeneratorService) : BaseAuthService(userManager, httpContextAccessor, configuration, unitOfWork, jwtGeneratorService), IAuthService
    {
        private readonly IEmailSenderService _emailSender = emailSender; 
        
        public async Task<Error> VerifyUser()
        {
            Log.Information("Veryfying1");
            var currentUserIdResult = GetCurrentUserId();
            if (!currentUserIdResult.IsSuccessful)
                return currentUserIdResult.Error;

            Log.Information("Veryfying3");

            var userId = currentUserIdResult.Value;

            Log.Information($"UserId {userId}");

            var _userRepository = (IUserRepository)_unitOfWork.GetRepository<User>();

            var user = await _userRepository.GetOneAsync(userId);
            if (user is null)
                return UserError.UserNotFound;

            user.IsVerified = true;
            _userRepository.Update(user);
            await _unitOfWork.SaveChangesAsync();

            return Error.None;
        }
        
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
            user.RegisteredDate = DateTime.Now;
            user.IsVerified = false;

            var result = await _userManager.CreateAsync(user, requestDto.Password);
            if (!result.Succeeded)
            {
                var errors = result.Errors.ToDictionary(e => e.Code, e => e.Description);
                return Result<UserRegisterResponseDto>.Failure(new AuthError(errors).ErrorsWhileCreatingUser);
            }

            await _userManager.AddToRoleAsync(user, Roles.User.ToString());

            var roles = new List<string>(){ Roles.User.ToString()};
            var token = _jwtGeneratorService.GenerateToken(user, roles);

            string activationLink = $"http://localhost:5173/sign-in/?confirm={Uri.EscapeDataString(token)}";

            await _emailSender.SendVerificationEmail(user.Email!, user.Name, activationLink);

            return Result<UserRegisterResponseDto>.Success();
        }

        public async Task<Result<LoginResponseDto>> Login(LoginRequestDto? requestDto)
        {
            if (requestDto == null)
                return Result<LoginResponseDto>.Failure(Error.NullParameter);

            var user = await _userManager.FindByEmailAsync(requestDto.Email);
            if (user == null || !await _userManager.CheckPasswordAsync(user, requestDto.Password))
                return Result<LoginResponseDto>.Failure(AuthError.InvalidEmailOrPassword);

            if (!user.IsVerified)
                return Result<LoginResponseDto>.Failure(AuthError.UserNotVerified);

            var roles = await GetRolesForCurrentUser(user);

            var responseDto = new LoginResponseDto
            {
                Token = _jwtGeneratorService.GenerateToken(user, roles),
            };

            return Result<LoginResponseDto>.Success(responseDto);
        }

    }
}
