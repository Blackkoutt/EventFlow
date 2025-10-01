using EventFlowAPI.DB.Entities;
using EventFlowAPI.Logic.Enums;
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
        IJWTGeneratorService jwtGeneratorService) : BaseExternalAuthService(userManager, httpContextAccessor, configuration, unitOfWork, jwtGeneratorService, AuthConfiguration.GoogleAuth), IGoogleAuthService
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
            return $"https://accounts.google.com/o/oauth2/v2/auth?" +
                   $"response_type=code" +
                   $"&client_id={_appId}" +
                   $"&redirect_uri={UrlEncoder.Default.Encode(_redirectURI)}" +
                   $"&scope=openid%20profile%20email";
        }


        protected sealed override HttpRequestMessage GetExchangeCodeForTokenRequest(string code)
        {
            return new HttpRequestMessage(HttpMethod.Post, "https://oauth2.googleapis.com/token")
            {
                Content = new FormUrlEncodedContent(new Dictionary<string, string>
                {
                    {"code", code},
                    {"client_id", _appId},
                    {"client_secret", _appSecret},
                    {"redirect_uri", $"http://localhost:5173/sign-in"},
                    {"grant_type", "authorization_code"}
                })
            };
        }


        private async Task<Payload> VerifyGoogleTokenAsync(string? idToken)
        {
            return await ValidateAsync(idToken, new ValidationSettings
            {
                Audience = new List<string> { _appId }
            });
        }

        protected sealed override string GetProviderName() => ExternalAuthProvider.GOOGLE.ToString();
    }
}
