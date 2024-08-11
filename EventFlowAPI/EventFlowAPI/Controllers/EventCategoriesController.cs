using EventFlowAPI.Logic.DTO.RequestDto;
using EventFlowAPI.Logic.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace EventFlowAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventCategoriesController(IEventCategoryService eventCategoryService) : ControllerBase
    {
        private readonly IEventCategoryService _eventCategoryService = eventCategoryService;

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetAdditionalServices()
        {
            var result = await _eventCategoryService.GetAllAsync();
            return result.IsSuccessful ? Ok(result.Value) : BadRequest(result.Error.Details);
        }


        [HttpGet("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetAdditionalServiceById([FromRoute] int id)
        {
            var result = await _eventCategoryService.GetOneAsync(id);
            return result.IsSuccessful ? Ok(result.Value) : BadRequest(result.Error.Details);
        }


        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateAdditionalService([FromBody] AdditionalServicesRequestDto additionalServicesReqestDto)
        {
            var result = await _eventCategoryService.AddAsync(additionalServicesReqestDto);
            return result.IsSuccessful ? CreatedAtAction(nameof(GetAdditionalServiceById), new { id = result.Value.Id }, result.Value) : BadRequest(result.Error.Details);
        }


        [HttpPut("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateAdditionalService([FromRoute] int id, [FromBody] AdditionalServicesRequestDto additionalServicesReqestDto)
        {
            var result = await _eventCategoryService.UpdateAsync(id, additionalServicesReqestDto);
            return result.IsSuccessful ? NoContent() : BadRequest(result.Error.Details);
        }


        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> DeleteAdditionalService([FromRoute] int id)
        {
            var result = await _eventCategoryService.DeleteAsync(id);
            return result.IsSuccessful ? NoContent() : BadRequest(result.Error.Details);
        }
    }
}
