using EventFlowAPI.DB.Entities;
using EventFlowAPI.Logic.Errors;
using EventFlowAPI.Logic.Identity.DTO.ResponseDto;
using EventFlowAPI.Logic.Identity.Helpers;
using EventFlowAPI.Logic.Identity.Services.Interfaces;
using EventFlowAPI.Logic.ResultObject;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

namespace EventFlowAPI.Logic.Identity.Services.Services.BaseServices
{
    public abstract class BaseExternalAuthService(
        UserManager<User> userManager,
        IHttpContextAccessor httpContextAccessor,
        IConfiguration configuration,
        IJWTGeneratorService jwtGeneratorService) : BaseAuthService(userManager, httpContextAccessor, configuration, jwtGeneratorService)
    {
        public async Task<Result<LoginResponseDto>> Login(string? code)
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

            var userInfoResult = await GetInfoAboutUser(token);
            if (!userInfoResult.IsSuccessful)
            {
                return Result<LoginResponseDto>.Failure(userInfoResult.Error);
            }
            if (userInfoResult.Value.Email == null)
            {
                return Result<LoginResponseDto>.Failure(AuthError.FailedToGetUserEmail);
            }

            var user = await _userManager.FindByEmailAsync(userInfoResult.Value.Email);

            if (user == null)
            {
                user = await CreateNewUserAsync(userInfoResult.Value);
            }

            var roles = await GetRolesForCurrentUser(user);

            var responseDto = new LoginResponseDto
            {
                Token = _jwtGeneratorService.GenerateToken(user, roles),
            };

            return Result<LoginResponseDto>.Success(responseDto);
        }


        protected async Task<TokenResponse?> ExchangeCodeForTokenAsync(string code)
        {
            var tokenRequest = GetExchangeCodeForTokenRequest(code);

            var client = new HttpClient();
            var response = await client.SendAsync(tokenRequest);

            var content = await response.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<TokenResponse>(content) ?? null;
        }


        private async Task<User> CreateNewUserAsync(ExternalLoginUserResponse user)
        {
            var nameAndSurnameList = user.Name.Split(" ").ToList();
            var nameAndSurnameDictionary = new Dictionary<string, string>()
            {
                { "Name", nameAndSurnameList.Any() ?  nameAndSurnameList.First() : string.Empty},
                { "Surname", nameAndSurnameList.Count > 1 ? nameAndSurnameList.Last() : string.Empty}
            };
            var newUser = new User
            {
                Name = nameAndSurnameDictionary["Name"],
                Surname = nameAndSurnameDictionary["Surname"],
                UserName = user.Email,
                Email = user.Email,
                Provider = GetProviderName()
            };
            await _userManager.CreateAsync(newUser);
            await _userManager.AddToRoleAsync(newUser, Roles.User.ToString());
            return newUser;
        }

        protected abstract string GetProviderName();
        protected abstract Task<Result<ExternalLoginUserResponse>> GetInfoAboutUser(TokenResponse token);
        public abstract string GetLinkToSigninPage();
        protected abstract HttpRequestMessage GetExchangeCodeForTokenRequest(string code);
    }
}
