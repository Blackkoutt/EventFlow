using Azure.Core;
using EventFlowAPI.DB.Entities;
using EventFlowAPI.Logic.Errors;
using EventFlowAPI.Logic.Identity.DTO.ResponseDto;
using EventFlowAPI.Logic.Identity.Services.Interfaces;
using EventFlowAPI.Logic.Identity.Services.Services.BaseServices;
using EventFlowAPI.Logic.ResultObject;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System.Text.Encodings.Web;


namespace EventFlowAPI.Logic.Identity.Services.Services
{
    public class FacebookAuthService(
        UserManager<User> userManager,
        IHttpContextAccessor httpContextAccessor,
        IConfiguration configuration,
        IJWTGeneratorService jwtGeneratorService) : BaseExternalAuthService(userManager, httpContextAccessor, configuration, jwtGeneratorService), IFacebookAuthService
    {


        protected sealed override async Task<Result<ExternalLoginUserResponse>> GetInfoAboutUser(TokenResponse token)
        {
            using var client = new HttpClient();
            var response = await client.GetAsync($"https://graph.facebook.com/me?fields=id,name,email,birthday&access_token={token.Access_Token}");

            if (!response.IsSuccessStatusCode)
            {
                return Result<ExternalLoginUserResponse>.Failure(AuthError.FailedToGetUserData);
            }

            var content = await response.Content.ReadAsStringAsync();

            var userData = JsonConvert.DeserializeObject<ExternalLoginUserResponse>(content) ?? null;

            if (userData == null)
            {
                return Result<ExternalLoginUserResponse>.Failure(AuthError.FailedToGetUserData);
            }
            return Result<ExternalLoginUserResponse>.Success(userData);
        }


        protected sealed override HttpRequestMessage GetExchangeCodeForTokenRequest(string code)
        {
            return new HttpRequestMessage(HttpMethod.Post, "https://graph.facebook.com/v10.0/oauth/access_token")
            {
                Content = new FormUrlEncodedContent(new Dictionary<string, string>
                {
                    {"client_id", _configuration.GetSection("Authentication:Facebook")["AppId"]!},
                    {"client_secret", _configuration.GetSection("Authentication:Facebook")["AppSecret"]!},
                    {"redirect_uri", "https://localhost:7229/api/auth/facebook-login"},
                    {"code", code},
                    {"grant_type", "authorization_code"}
                })
            };
        }

        public sealed override string GetLinkToSigninPage()
        {
            var appId = _configuration.GetSection("Authentication:Facebook")["AppId"]!;

            var request = _httpContextAccessor.HttpContext?.Request;
            var baseURL = $"{request?.Scheme}://{request?.Host}";

            var redirectURI = $"{baseURL}/api/auth/facebook-login";

            return $"https://www.facebook.com/v10.0/dialog/oauth?" +
                   $"client_id={appId}" +
                   $"&redirect_uri={UrlEncoder.Default.Encode(redirectURI)}" +
                   $"&response_type=code" +
                   $"&scope=email,public_profile";
        }


        protected sealed override string GetProviderName() => "FACEBOOK";
    }
}
