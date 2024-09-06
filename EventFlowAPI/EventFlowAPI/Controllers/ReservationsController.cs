using EventFlowAPI.Logic.DTO.RequestDto;
using EventFlowAPI.Logic.Services.CRUDServices.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace EventFlowAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReservationsController(IReservationService reservationService) : ControllerBase
    {
        private readonly IReservationService _reservationService = reservationService;

        [HttpGet("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetEquipmentById([FromRoute] int id)
        {
            var result = await _reservationService.GetOneAsync(id);
            return result.IsSuccessful ? Ok(result.Value) : BadRequest(result.Error.Details);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="reservationReqestDto"></param>
        /// <returns></returns>
        /// <remarks>
        /// Sample request:
        /// 
        ///     {
        ///         "paymentTypeId": 2,
        ///         "ticketId": 1,
        ///         "seatsIds": [
        ///             6,7
        ///          ]
        ///     }
        /// </remarks>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateReservation([FromBody] ReservationRequestDto reservationReqestDto)
        {
            await _reservationService.MakeReservation(reservationReqestDto);
            return Ok();  
            //return result.IsSuccessful ? CreatedAtAction(nameof(GetEquipmentById), new { id = result.Value.Id }, result.Value) : BadRequest(result.Error.Details);
        }
    }
}
