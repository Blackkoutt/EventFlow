﻿using EventFlowAPI.Logic.Identity.DTO.RequestDto;
using EventFlowAPI.Logic.Identity.Services.Interfaces;
using EventFlowAPI.Logic.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;

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



        [HttpPost("register")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> RegisterUser([FromBody] UserRegisterRequestDto userRegisterRequestDto)
        {
            var result = await _authService.RegisterUser(userRegisterRequestDto);
            return result.IsSuccessful ? Ok() : BadRequest(result.Error.Details);
        }


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

        [HttpGet("google-login")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> LoginViaGoogle(string code)
        {
            var result = await _googleAuthService.Login(code);
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

        [HttpGet("facebook-login")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> LoginViaFacebook(string code)
        {
            var result = await _facebookAuthService.Login(code);
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
            var result = await _userService.GetCurrentUserInfo();
            return result.IsSuccessful ? Ok(result.Value) : BadRequest(result.Error.Details);
        }


    }
}
