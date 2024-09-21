using EventFlowAPI.Logic.DTO.RequestDto;
using EventFlowAPI.Logic.Query.Abstract;
using EventFlowAPI.Logic.Services.CRUDServices.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace EventFlowAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MediaPatronsController(IMediaPatronService mediaPatronService) : ControllerBase
    {
        private readonly IMediaPatronService _mediaPatronService = mediaPatronService;

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetMediaPatrons([FromQuery] QueryObject query)
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


        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateMediaPatron([FromBody] MediaPatronRequestDto mediaPatronReqestDto)
        {
            var result = await _mediaPatronService.AddAsync(mediaPatronReqestDto);
            return result.IsSuccessful ? CreatedAtAction(nameof(GetMediaPatronById), new { id = result.Value.Id }, result.Value) : BadRequest(result.Error.Details);
        }


        [HttpPut("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateMediaPatron([FromRoute] int id, [FromBody] MediaPatronRequestDto mediaPatronReqestDto)
        {
            var result = await _mediaPatronService.UpdateAsync(id, mediaPatronReqestDto);
            return result.IsSuccessful ? NoContent() : BadRequest(result.Error.Details);
        }


        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> DeleteMediaPatron([FromRoute] int id)
        {
            var result = await _mediaPatronService.DeleteAsync(id);
            return result.IsSuccessful ? NoContent() : BadRequest(result.Error.Details);
        }
    }
}
