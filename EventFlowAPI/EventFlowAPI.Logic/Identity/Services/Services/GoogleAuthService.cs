using EventFlowAPI.DB.Entities;
using EventFlowAPI.Logic.Errors;
using EventFlowAPI.Logic.Identity.DTO.ResponseDto;
using EventFlowAPI.Logic.Identity.Helpers;
using EventFlowAPI.Logic.Identity.Services.Interfaces;
using EventFlowAPI.Logic.Identity.Services.Services.BaseServices;
using EventFlowAPI.Logic.ResultObject;
using Google.Apis.Auth.OAuth2.Responses;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using static Google.Apis.Auth.GoogleJsonWebSignature;

namespace EventFlowAPI.Logic.Identity.Services.Services
{
    public class GoogleAuthService(
        UserManager<User> userManager,
        IHttpContextAccessor httpContextAccessor,
        IConfiguration configuration,
        IJWTGeneratorService jwtGeneratorService) : BaseAuthService(userManager, httpContextAccessor, configuration, jwtGeneratorService), IGoogleAuthService
    {
        public async Task<Result<LoginResponseDto>> LoginViaGoogle(string? code)
        {
            if (code == null)
            {
                return Result<LoginResponseDto>.Failure(Error.NullParameter);
            }

            var token = await ExchangeCodeForTokenAsync(code);

            if (token == null)
            {
                return Result<LoginResponseDto>.Failure(AuthError.CannotExchangeCodeForToken);
            }

            var payload = await VerifyGoogleTokenAsync(token.IdToken);
            if (payload == null)
            {
                return Result<LoginResponseDto>.Failure(AuthError.GoogleTokenVerificationFailed);
            }

            var user = await _userManager.FindByEmailAsync(payload.Email);
            if (user == null)
            {
                user = await CreateNewUserAsync(payload);
            }

            var roles = await GetRolesForCurrentUser(user);

            var responseDto = new LoginResponseDto
            {
                Token = _jwtGeneratorService.GenerateToken(user, roles),
            };

            return Result<LoginResponseDto>.Success(responseDto);
        }


        private async Task<TokenResponse?> ExchangeCodeForTokenAsync(string code)
        {
            var tokenRequest = GetExchangeCodeForTokenRequest(code);

            var client = new HttpClient();
            var response = await client.SendAsync(tokenRequest);

            var content = await response.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<TokenResponse>(content);
        }


        private HttpRequestMessage GetExchangeCodeForTokenRequest(string code)
        {
            return new HttpRequestMessage(HttpMethod.Post, "https://oauth2.googleapis.com/token")
            {
                Content = new FormUrlEncodedContent(new Dictionary<string, string>
                {
                    {"code", code},
                    {"client_id", _configuration.GetSection("Authentication:Google")["clientId"]!},
                    {"client_secret", _configuration.GetSection("Authentication:Google")["clientSecret"]!},
                    {"redirect_uri", "https://localhost:7229/api/auth/google-login"},
                    {"grant_type", "authorization_code"}
                })
            };
        }

        private async Task<Payload> VerifyGoogleTokenAsync(string idToken)
        {
            var settings = new ValidationSettings
            {
                Audience = new List<string> { _configuration.GetSection("Authentication:Google")["clientId"]! }
            };
            return await ValidateAsync(idToken, settings);
        }


        private async Task<User> CreateNewUserAsync(Payload payload)
        {
            var user = new User { UserName = payload.Email, Email = payload.Email };
            await _userManager.CreateAsync(user);
            await _userManager.AddToRoleAsync(user, Roles.User);
            return user;
        }
    }
}
