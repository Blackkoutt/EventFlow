using EventFlowAPI.DB.Entities;
using EventFlowAPI.Logic.Errors;
using EventFlowAPI.Logic.Identity.DTO.RequestDto;
using EventFlowAPI.Logic.Identity.DTO.ResponseDto;
using EventFlowAPI.Logic.Identity.Helpers;
using EventFlowAPI.Logic.Identity.Services.Interfaces;
using EventFlowAPI.Logic.ResultObject;
using EventFlowAPI.Logic.UnitOfWork;
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
        IUnitOfWork unitOfWork,
        IJWTGeneratorService jwtGeneratorService) : BaseAuthService(userManager, httpContextAccessor, configuration, unitOfWork, jwtGeneratorService)
    {
        public async Task<Result<LoginResponseDto>> Login(ExternalLoginRequest externalLoginRequest)
        {
            Console.WriteLine($"\n\n\n\n\n\n\n\n\nexternalCode {externalLoginRequest.Code}");
            if (externalLoginRequest.Code == null)
            {
                return Result<LoginResponseDto>.Failure(Error.NullParameter);
            }
            var token = await ExchangeCodeForTokenAsync(externalLoginRequest.Code);

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
            Console.WriteLine($"user {user is null}");
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

            Console.WriteLine("Response content: " + content);

            if (!response.IsSuccessStatusCode)
            {
                Console.WriteLine("Error response: " + response.StatusCode);
                return null;
            }

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
                IsVerified = true,
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
