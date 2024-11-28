using EventFlowAPI.Logic.Identity.DTO.RequestDto;
using EventFlowAPI.Logic.Identity.Services.Interfaces;
using EventFlowAPI.Logic.Services.CRUDServices.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Security.Claims;

namespace EventFlowAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController(
        IAuthService authService,
        IUserService userService,
        IGoogleAuthService googleAuthService,
        IFacebookAuthService facebookAuthService) : ControllerBase
    {
        private readonly IAuthService _authService = authService;
        private readonly IUserService _userService = userService;
        private readonly IGoogleAuthService _googleAuthService = googleAuthService;
        private readonly IFacebookAuthService _facebookAuthService = facebookAuthService;


        [HttpGet("validate")]
        [Authorize]
        public IActionResult ValidateUser()
        {
            var userClaims = User.Claims.ToList();
            return Ok(new
            {
                id = userClaims.FirstOrDefault(c => c.Type == "Id")?.Value,
                name = userClaims.FirstOrDefault(c => c.Type == "Name")?.Value,
                surname = userClaims.FirstOrDefault(c => c.Type == "Surname")?.Value,
                email = userClaims.FirstOrDefault(c => c.Type == "Email")?.Value,
                dateOfBirth = userClaims.FirstOrDefault(c => c.Type == "DateOfBirth")?.Value,
                roles = userClaims.Where(c => c.Type == "Role").Select(c => c.Value).ToList()
            });
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userRegisterRequestDto"></param>
        /// <returns></returns>
        /// <remarks>
        /// Sample request:
        /// 
        ///     {
        ///         "name": "Jan",
        ///         "surname": "Kowalski",
        ///         "email": "eventflow.test@interia.pl",
        ///         "dateOfBirth": "1985-04-25",
        ///         "password": "789!@#qwe",
        ///         "confirmPassword": "789!@#qwe"
        ///     }
        ///       
        /// </remarks>
        [HttpPost("register")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> RegisterUser([FromBody] UserRegisterRequestDto userRegisterRequestDto)
        {
            var result = await _authService.RegisterUser(userRegisterRequestDto);
            return result.IsSuccessful ? Ok() : BadRequest(result.Error.Details);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="loginRequestDto"></param>
        /// <returns>JWT token</returns>
        /// <remarks>
        /// Sample request:
        /// 
        ///     {
        ///         "email": "mateusz.strapczuk1@gmail.com",
        ///         "password": "qazzaq1@WSX"
        ///     }
        ///       
        /// </remarks>
        [HttpPost("login")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]        
        public async Task<IActionResult> Login([FromBody] LoginRequestDto loginRequestDto)
        {
            var result = await _authService.Login(loginRequestDto);
            if (!result.IsSuccessful)
            {
                return result.Error.Details!.Code switch
                {
                    HttpStatusCode.BadRequest => BadRequest(result.Error.Details),
                    HttpStatusCode.Unauthorized => Unauthorized(result.Error.Details),
                    _ => StatusCode((int)HttpStatusCode.InternalServerError, result.Error.Details)
                };
            }
            return Ok(result.Value);
        }

        [HttpGet("signin-google")]
        public IActionResult SiginGoogle() => Redirect(_googleAuthService.GetLinkToSigninPage());

        [HttpGet("signin-facebook")]
        public IActionResult SiginFacebook() => Redirect(_facebookAuthService.GetLinkToSigninPage());

        [HttpPost("google-login")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> LoginViaGoogle([FromBody] ExternalLoginRequest externalLoginRequest)
        {
            var result = await _googleAuthService.Login(externalLoginRequest);
            if (!result.IsSuccessful)
            {
                return result.Error.Details!.Code switch
                {
                    HttpStatusCode.BadRequest => BadRequest(result.Error.Details),
                    HttpStatusCode.Unauthorized => Unauthorized(result.Error.Details),
                    _ => StatusCode((int)HttpStatusCode.InternalServerError, result.Error.Details)
                };
            }
            return Ok(result.Value);
        }

        [HttpPost("facebook-login")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> LoginViaFacebook([FromBody] ExternalLoginRequest externalLoginRequest)
        {
            var result = await _facebookAuthService.Login(externalLoginRequest);
            if (!result.IsSuccessful)
            {
                return result.Error.Details!.Code switch
                {
                    HttpStatusCode.BadRequest => BadRequest(result.Error.Details),
                    HttpStatusCode.Unauthorized => Unauthorized(result.Error.Details),
                    _ => StatusCode((int)HttpStatusCode.InternalServerError, result.Error.Details)
                };
            }
            return Ok(result.Value);
        }

        [HttpGet("info")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> Info()
        {
            var result = await _userService.GetCurrentUser();
            return result.IsSuccessful ? Ok(result.Value) : BadRequest(result.Error.Details);
        }


    }
}
