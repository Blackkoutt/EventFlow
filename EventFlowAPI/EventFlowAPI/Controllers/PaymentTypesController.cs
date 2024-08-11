using EventFlowAPI.Logic.DTO.RequestDto;
using EventFlowAPI.Logic.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace EventFlowAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentTypesController(IPaymentTypeService paymentTypeService) : ControllerBase
    {
        private readonly IPaymentTypeService _paymentTypeService = paymentTypeService;

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetAdditionalServices()
        {
            var result = await _paymentTypeService.GetAllAsync();
            return result.IsSuccessful ? Ok(result.Value) : BadRequest(result.Error.Details);
        }


        [HttpGet("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetAdditionalServiceById([FromRoute] int id)
        {
            var result = await _paymentTypeService.GetOneAsync(id);
            return result.IsSuccessful ? Ok(result.Value) : BadRequest(result.Error.Details);
        }


        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateAdditionalService([FromBody] AdditionalServicesRequestDto additionalServicesReqestDto)
        {
            var result = await _paymentTypeService.AddAsync(additionalServicesReqestDto);
            return result.IsSuccessful ? CreatedAtAction(nameof(GetAdditionalServiceById), new { id = result.Value.Id }, result.Value) : BadRequest(result.Error.Details);
        }


        [HttpPut("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateAdditionalService([FromRoute] int id, [FromBody] AdditionalServicesRequestDto additionalServicesReqestDto)
        {
            var result = await _paymentTypeService.UpdateAsync(id, additionalServicesReqestDto);
            return result.IsSuccessful ? NoContent() : BadRequest(result.Error.Details);
        }


        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> DeleteAdditionalService([FromRoute] int id)
        {
            var result = await _paymentTypeService.DeleteAsync(id);
            return result.IsSuccessful ? NoContent() : BadRequest(result.Error.Details);
        }
    }
}
