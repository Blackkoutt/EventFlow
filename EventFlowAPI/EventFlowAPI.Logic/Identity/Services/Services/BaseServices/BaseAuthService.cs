using EventFlowAPI.DB.Entities;
using EventFlowAPI.Logic.Errors;
using EventFlowAPI.Logic.Identity.Services.Interfaces;
using EventFlowAPI.Logic.Identity.Services.Interfaces.BaseInterfaces;
using EventFlowAPI.Logic.ResultObject;
using EventFlowAPI.Logic.UnitOfWork;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Serilog;

namespace EventFlowAPI.Logic.Identity.Services.Services.BaseServices
{
    public abstract class BaseAuthService(
        UserManager<User> userManager,
        IHttpContextAccessor httpContextAccessor,
        IConfiguration configuration,
        IUnitOfWork unitOfWork,
        IJWTGeneratorService jwtGeneratorService) : IBaseAuthService
    {
        protected readonly UserManager<User> _userManager = userManager;
        protected readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;
        protected readonly IJWTGeneratorService _jwtGeneratorService = jwtGeneratorService;
        protected readonly IConfiguration _configuration = configuration;
        protected readonly IUnitOfWork _unitOfWork = unitOfWork;

        public Result<string> GetCurrentUserId()
        {
            var httpContext = _httpContextAccessor.HttpContext;
            if (httpContext == null)
                return Result<string>.Failure(AuthError.HttpContextNotAvailable);

            var userIdentity = httpContext.User.Identities.FirstOrDefault();
            if (userIdentity == null)
                return Result<string>.Failure(AuthError.CanNotConfirmIdentity);

            var userId = userIdentity.Claims.FirstOrDefault(c => c.Type == "id")?.Value;
            if(userId == null)
                return Result<string>.Failure(AuthError.UserHaventIdClaim);

            return Result<string>.Success(userId);
        }
        public async Task<IList<string>?> GetRolesForCurrentUser(User user) => await _userManager.GetRolesAsync(user);
    }
}
