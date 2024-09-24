using EventFlowAPI.Logic.DTO.RequestDto;
using EventFlowAPI.Logic.Helpers;
using EventFlowAPI.Logic.Identity.Helpers;
using EventFlowAPI.Logic.Query;
using EventFlowAPI.Logic.Services.CRUDServices.Interfaces;
using EventFlowAPI.Logic.Services.OtherServices.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EventFlowAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventPassesController(
        IEventPassService eventPassService,
        IFileService fileService) : ControllerBase
    {
        private readonly IEventPassService _eventPassService = eventPassService;
        private readonly IFileService _fileService = fileService;

        [Authorize]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> GetEventPasses([FromQuery] EventPassQuery query)
        {
            var result = await _eventPassService.GetAllAsync(query);
            return result.IsSuccessful ? Ok(result.Value) : BadRequest(result.Error.Details);
        }

        [Authorize]
        [HttpGet("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> GetEventPassById([FromRoute] int id)
        {
            var result = await _eventPassService.GetOneAsync(id);
            return result.IsSuccessful ? Ok(result.Value) : BadRequest(result.Error.Details);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="eventPassReqestDto"></param>
        /// <returns></returns>
        /// <remarks>
        /// Sample request:
        /// 
        ///     {
        ///         "passTypeId": 1,
        ///         "paymentTypeId": 1
        ///     }
        /// </remarks>
        [Authorize]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> CreateEventPass([FromBody] EventPassRequestDto eventPassReqestDto)
        {
            var result = await _eventPassService.BuyEventPass(eventPassReqestDto);
            return result.IsSuccessful ? CreatedAtAction(nameof(GetEventPassById), new { id = result.Value.Id }, result.Value) : BadRequest(result.Error.Details);
        }

        [Authorize(Roles = Roles.Admin)]
        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> CancelEventPass([FromRoute] int id)
        {
            var result = await _eventPassService.DeleteAsync(id);
            return result.IsSuccessful ? Ok(result.Value) : BadRequest(result.Error.Details);
        }

        [Authorize]
        [HttpPut("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> RenewEventPass([FromRoute] int id, [FromBody] EventPassRequestDto eventPassReqestDto)
        {
            var result = await _eventPassService.UpdateAsync(id, eventPassReqestDto);
            return result.IsSuccessful ? NoContent() : BadRequest(result.Error.Details);
        }

        [Authorize]
        [HttpGet("{id:int}/pdf")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> GetEventPassPdfById([FromRoute] int id)
        {
            var result = await _fileService.GetEventPassFile(id, ContentType.PDF);
            return result.IsSuccessful ?
                File(result.Value.Data, result.Value.ContentType, result.Value.FileName) :
                BadRequest(result.Error.Details);
        }

        [Authorize]
        [HttpGet("{id:int}/jpg")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> GetEventPassJpgById([FromRoute] int id)
        {
            var result = await _fileService.GetEventPassFile(id, ContentType.JPEG);
            return result.IsSuccessful ?
                File(result.Value.Data, result.Value.ContentType, result.Value.FileName) :
                BadRequest(result.Error.Details);
        }
    }
}
