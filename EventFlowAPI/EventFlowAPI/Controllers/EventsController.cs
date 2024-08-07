using Azure.Core;
using EventFlowAPI.Logic.DTO.RequestDto;
using EventFlowAPI.Logic.DTO.ResponseDto;
using EventFlowAPI.Logic.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EventFlowAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventsController : ControllerBase
    {
        private readonly IEventService _eventService;

        public EventsController(IEventService eventService) 
        {
            _eventService = eventService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var records = await _eventService.GetAllAsync();
            return Ok(records);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetOne([FromRoute] int id)
        {
            var record = await _eventService.GetOneAsync(id);
            return Ok(record);
        }
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<IActionResult> Add([FromBody] EventRequestDto eventReqestDto)
        {
            await _eventService.AddAsync(eventReqestDto);
            return Created();
        }
        [HttpPut("{id:int}")]

        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] EventRequestDto eventReqestDto)
        {
            await _eventService.UpdateAsync(id, eventReqestDto);
            return NoContent();
        }

        [HttpDelete("{id:int}")]

        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            await _eventService.DeleteAsync(id);
            return NoContent();
        }
    }
}
