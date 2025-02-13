﻿using EventFlowAPI.Logic.Errors;
using EventFlowAPI.Logic.Identity.DTO.RequestDto;
using EventFlowAPI.Logic.Identity.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using System.Net;

namespace EventFlowAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController(
        IAuthService authService,
        IGoogleAuthService googleAuthService,
        IFacebookAuthService facebookAuthService) : ControllerBase
    {
        private readonly IAuthService _authService = authService;
        private readonly IGoogleAuthService _googleAuthService = googleAuthService;
        private readonly IFacebookAuthService _facebookAuthService = facebookAuthService;


        [HttpPatch("activate")]
        [Authorize]
        public async Task<IActionResult> ActivateUser()
        {
            Log.Information("Hellolooo01");
            var verifyError = await _authService.VerifyUser();
            return verifyError == Error.None ? Ok() : BadRequest(verifyError);
        }


        [HttpGet("validate")]
        [Authorize]
        public IActionResult ValidateUser()
        {
            var userClaims = User.Claims.ToList();

            return Ok(new
            {
                id = userClaims.FirstOrDefault(c => c.Type == "id")?.Value,
                name = userClaims.FirstOrDefault(c => c.Type == "name")?.Value,
                surname = userClaims.FirstOrDefault(c => c.Type == "surname")?.Value,
                emailAddress = userClaims.FirstOrDefault(c => c.Type == "emailAddress")?.Value,
                dateOfBirth = userClaims.FirstOrDefault(c => c.Type == "dateOfBirth")?.Value,
                isVerified = userClaims.FirstOrDefault(c => c.Type == "isVerified")?.Value,
                userRoles = userClaims.Where(c => c.Type == "userRoles").Select(c => c.Value).ToList()
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
    }
}
