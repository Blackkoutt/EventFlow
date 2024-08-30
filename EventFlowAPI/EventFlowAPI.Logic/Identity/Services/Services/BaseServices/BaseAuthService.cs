using EventFlowAPI.DB.Entities;
using EventFlowAPI.Logic.Errors;
using EventFlowAPI.Logic.Identity.Services.Interfaces;
using EventFlowAPI.Logic.Identity.Services.Interfaces.BaseInterfaces;
using EventFlowAPI.Logic.ResultObject;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;

namespace EventFlowAPI.Logic.Identity.Services.Services.BaseServices
{
    public abstract class BaseAuthService(
        UserManager<User> userManager,
        IHttpContextAccessor httpContextAccessor,
        IConfiguration configuration,
        IJWTGeneratorService jwtGeneratorService) : IBaseAuthService
    {
        protected readonly UserManager<User> _userManager = userManager;
        protected readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;
        protected readonly IJWTGeneratorService _jwtGeneratorService = jwtGeneratorService;
        protected readonly IConfiguration _configuration = configuration;

        public async Task<Result<string>> GetCurrentUserId()
        {
            var httpContext = _httpContextAccessor.HttpContext;
            if (httpContext == null)
            {
                return Result<string>.Failure(AuthError.HttpContextNotAvailable);
            }
            var userEmail = httpContext.User.Identity?.Name;
            if (userEmail == null)
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
