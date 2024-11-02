using EventFlowAPI.DB.Entities;
using EventFlowAPI.Logic.DTO.RequestDto;
using EventFlowAPI.Logic.DTO.UpdateRequestDto;
using EventFlowAPI.Logic.Identity.Helpers;
using EventFlowAPI.Logic.Query;
using EventFlowAPI.Logic.Query.Abstract;
using EventFlowAPI.Logic.Services.CRUDServices.Interfaces;
using EventFlowAPI.Logic.Services.OtherServices.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace EventFlowAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MediaPatronsController(
        IMediaPatronService mediaPatronService,
        IFileService fileService) : ControllerBase
    {
        private readonly IMediaPatronService _mediaPatronService = mediaPatronService;
        private readonly IFileService _fileService = fileService;

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetMediaPatrons([FromQuery] MediaPatronQuery query)
        {
            var result = await _mediaPatronService.GetAllAsync(query);
            return result.IsSuccessful ? Ok(result.Value) : BadRequest(result.Error.Details);
        }


        [HttpGet("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetMediaPatronById([FromRoute] int id)
        {
            var result = await _mediaPatronService.GetOneAsync(id);
            return result.IsSuccessful ? Ok(result.Value) : BadRequest(result.Error.Details);
        }


        [Authorize(Roles = nameof(Roles.Admin))]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<IActionResult> CreateMediaPatron([FromForm] MediaPatronRequestDto mediaPatronReqestDto)
        {
            var result = await _mediaPatronService.AddAsync(mediaPatronReqestDto);
            if (!result.IsSuccessful)
            {
                return result.Error.Details!.Code switch
                {
                    HttpStatusCode.BadRequest => BadRequest(result.Error.Details),
                    HttpStatusCode.Unauthorized => Unauthorized(result.Error.Details),
                    HttpStatusCode.Forbidden => StatusCode((int)HttpStatusCode.Forbidden, result.Error.Details),
                    _ => StatusCode((int)HttpStatusCode.InternalServerError, result.Error.Details)
                };
            }
            return CreatedAtAction(nameof(GetMediaPatronById), new { id = result.Value.Id }, result.Value);
        }


        [Authorize(Roles = nameof(Roles.Admin))]
        [HttpPut("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<IActionResult> UpdateMediaPatron([FromRoute] int id, [FromForm] UpdateMediaPatronRequestDto mediaPatronReqestDto)
        {
            var result = await _mediaPatronService.UpdateAsync(id, mediaPatronReqestDto);
            if (!result.IsSuccessful)
            {
                return result.Error.Details!.Code switch
                {
                    HttpStatusCode.BadRequest => BadRequest(result.Error.Details),
                    HttpStatusCode.Unauthorized => Unauthorized(result.Error.Details),
                    HttpStatusCode.Forbidden => StatusCode((int)HttpStatusCode.Forbidden, result.Error.Details),
                    _ => StatusCode((int)HttpStatusCode.InternalServerError, result.Error.Details)
                };
            }
            return NoContent();
        }

        [Authorize(Roles = nameof(Roles.Admin))]
        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<IActionResult> DeleteMediaPatron([FromRoute] int id)
        {
            var result = await _mediaPatronService.DeleteAsync(id);
            if (!result.IsSuccessful)
            {
                return result.Error.Details!.Code switch
                {
                    HttpStatusCode.BadRequest => BadRequest(result.Error.Details),
                    HttpStatusCode.Unauthorized => Unauthorized(result.Error.Details),
                    HttpStatusCode.Forbidden => StatusCode((int)HttpStatusCode.Forbidden, result.Error.Details),
                    _ => StatusCode((int)HttpStatusCode.InternalServerError, result.Error.Details)
                };
            }
            return NoContent();
        }

        [HttpGet("{id:int}/image")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetMediaPatronImage([FromRoute] int id)
        {
            var result = await _fileService.GetEntityPhoto<MediaPatron>(id);
            if (!result.IsSuccessful)
            {
                return result.Error.Details!.Code switch
                {
                    HttpStatusCode.BadRequest => BadRequest(result.Error.Details),
                    _ => StatusCode((int)HttpStatusCode.InternalServerError, result.Error.Details)
                };
            }
            return File(result.Value.Data, result.Value.ContentType, result.Value.FileName);
        }
    }
}
