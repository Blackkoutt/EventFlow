using EventFlowAPI.Logic.DTO.RequestDto;
using EventFlowAPI.Logic.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace EventFlowAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdditionalServicesController : ControllerBase
    {
        private readonly IAdditionalServicesService _additionalServicesService;

        public AdditionalServicesController(IAdditionalServicesService additionalServicesService)
        {
            _additionalServicesService = additionalServicesService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var records = await _additionalServicesService.GetAllAsync();
            return Ok(records);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetOne([FromRoute] int id)
        {
            var record = await _additionalServicesService.GetOneAsync(id);
            return Ok(record);
        }
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<IActionResult> Create([FromBody] AdditionalServicesRequestDto additionalServicesReqestDto)
        {
            await _additionalServicesService.AddAsync(additionalServicesReqestDto);
            return Created();
        }
        [HttpPut("{id:int}")]

        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] AdditionalServicesRequestDto additionalServicesReqestDto)
        {
            await _additionalServicesService.UpdateAsync(id, additionalServicesReqestDto);
            return NoContent();
        }

        [HttpDelete("{id:int}")]

        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            await _additionalServicesService.DeleteAsync(id);
            return NoContent();
        }
    }
}
