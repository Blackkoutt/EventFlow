using EventFlowAPI.DB.Entities;
using EventFlowAPI.Logic.Errors;
using EventFlowAPI.Logic.Identity.DTO.ResponseDto;
using EventFlowAPI.Logic.Identity.Services.Interfaces;
using EventFlowAPI.Logic.Identity.Services.Services.BaseServices;
using EventFlowAPI.Logic.ResultObject;
using EventFlowAPI.Logic.UnitOfWork;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using System.Text.Encodings.Web;
using static Google.Apis.Auth.GoogleJsonWebSignature;

namespace EventFlowAPI.Logic.Identity.Services.Services
{
    public class GoogleAuthService(
        UserManager<User> userManager,
        IHttpContextAccessor httpContextAccessor,
        IConfiguration configuration,
        IUnitOfWork unitOfWork,
        IJWTGeneratorService jwtGeneratorService) : BaseExternalAuthService(userManager, httpContextAccessor, configuration, unitOfWork, jwtGeneratorService), IGoogleAuthService
    {

        protected sealed override async Task<Result<ExternalLoginUserResponse>> GetInfoAboutUser(DTO.ResponseDto.TokenResponse token)
        {
            var payload = await VerifyGoogleTokenAsync(token.Id_Token);
            if (payload == null)
            {
                return Result<ExternalLoginUserResponse>.Failure(AuthError.GoogleTokenVerificationFailed);
            }
            var userData = new ExternalLoginUserResponse
            {
                Name = payload.Name,
                Email = payload.Email
            };

            return Result<ExternalLoginUserResponse>.Success(userData);
        }

        public sealed override string GetLinkToSigninPage()
        {
            var clientId = _configuration.GetSection("Authentication:Google")["clientId"]!;

            var request = _httpContextAccessor.HttpContext?.Request;
            var baseURL = $"{request?.Scheme}://{request?.Host}";

            //var redirectURI = $"{baseURL}/api/auth/google-login";
            var redirectURI = $"http://localhost:5173/sign-in";

            return $"https://accounts.google.com/o/oauth2/v2/auth?" +
                   $"response_type=code" +
                   $"&client_id={clientId}" +
                   $"&redirect_uri={UrlEncoder.Default.Encode(redirectURI)}" +
                   $"&scope=openid%20profile%20email";
        }


        protected sealed override HttpRequestMessage GetExchangeCodeForTokenRequest(string code)
        {
            return new HttpRequestMessage(HttpMethod.Post, "https://oauth2.googleapis.com/token")
            {
                Content = new FormUrlEncodedContent(new Dictionary<string, string>
                {
                    {"code", code},
                    {"client_id", _configuration.GetSection("Authentication:Google")["clientId"]!},
                    {"client_secret", _configuration.GetSection("Authentication:Google")["clientSecret"]!},
                    {"redirect_uri", $"http://localhost:5173/sign-in"},
                    {"grant_type", "authorization_code"}
                })
            };
        }


        private async Task<Payload> VerifyGoogleTokenAsync(string? idToken)
        {
            var settings = new ValidationSettings
            {
                Audience = new List<string> { _configuration.GetSection("Authentication:Google")["clientId"]! }
            };
            return await ValidateAsync(idToken, settings);
        }


        protected sealed override string GetProviderName() => "GOOGLE";
    }
}
