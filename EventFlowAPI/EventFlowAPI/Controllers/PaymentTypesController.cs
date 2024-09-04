using EventFlowAPI.Logic.DTO.RequestDto;
using EventFlowAPI.Logic.Services.CRUDServices.Interfaces;
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
        public async Task<IActionResult> GetPaymentTypes()
        {
            var result = await _paymentTypeService.GetAllAsync();
            return result.IsSuccessful ? Ok(result.Value) : BadRequest(result.Error.Details);
        }


        [HttpGet("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetPaymentTypeById([FromRoute] int id)
        {
            var result = await _paymentTypeService.GetOneAsync(id);
            return result.IsSuccessful ? Ok(result.Value) : BadRequest(result.Error.Details);
        }


        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreatePaymentType([FromBody] PaymentTypeRequestDto paymentTypeReqestDto)
        {
            var result = await _paymentTypeService.AddAsync(paymentTypeReqestDto);
            return result.IsSuccessful ? CreatedAtAction(nameof(GetPaymentTypeById), new { id = result.Value.Id }, result.Value) : BadRequest(result.Error.Details);
        }


        [HttpPut("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdatePaymentType([FromRoute] int id, [FromBody] PaymentTypeRequestDto paymentTypeReqestDto)
        {
            var result = await _paymentTypeService.UpdateAsync(id, paymentTypeReqestDto);
            return result.IsSuccessful ? NoContent() : BadRequest(result.Error.Details);
        }


        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> DeletePaymentType([FromRoute] int id)
        {
            var result = await _paymentTypeService.DeleteAsync(id);
            return result.IsSuccessful ? NoContent() : BadRequest(result.Error.Details);
        }
    }
}
