using EventFlowAPI.Logic.DTO.RequestDto;
using EventFlowAPI.Logic.Query.Abstract;
using EventFlowAPI.Logic.Services.CRUDServices.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace EventFlowAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrganizersController(IOrganizerService organizerService) : ControllerBase
    {
        private readonly IOrganizerService _organizerService = organizerService;

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetOrganizers([FromQuery] QueryObject query)
        {
            var result = await _organizerService.GetAllAsync(query);
            return result.IsSuccessful ? Ok(result.Value) : BadRequest(result.Error.Details);
        }


        [HttpGet("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetOrganizerById([FromRoute] int id)
        {
            var result = await _organizerService.GetOneAsync(id);
            return result.IsSuccessful ? Ok(result.Value) : BadRequest(result.Error.Details);
        }


        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateOrganizer([FromBody] OrganizerRequestDto organizerReqestDto)
        {
            var result = await _organizerService.AddAsync(organizerReqestDto);
            return result.IsSuccessful ? CreatedAtAction(nameof(GetOrganizerById), new { id = result.Value.Id }, result.Value) : BadRequest(result.Error.Details);
        }


        [HttpPut("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateOrganizer([FromRoute] int id, [FromBody] OrganizerRequestDto organizerReqestDto)
        {
            var result = await _organizerService.UpdateAsync(id, organizerReqestDto);
            return result.IsSuccessful ? NoContent() : BadRequest(result.Error.Details);
        }


        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> DeleteOrganizer([FromRoute] int id)
        {
            var result = await _organizerService.DeleteAsync(id);
            return result.IsSuccessful ? NoContent() : BadRequest(result.Error.Details);
        }
    }
}
