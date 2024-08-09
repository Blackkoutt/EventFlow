﻿using EventFlowAPI.Logic.DTO.RequestDto;
using EventFlowAPI.Logic.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace EventFlowAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdditionalServicesController(IAdditionalServicesService additionalServicesService) : ControllerBase
    {
        private readonly IAdditionalServicesService _additionalServicesService = additionalServicesService;

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetAll()
        {
            var result = await _additionalServicesService.GetAllAsync();
            return result.IsSuccessful ? Ok(result.Value) : BadRequest(result.Error.Details);
        }

        [HttpGet("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetOne([FromRoute] int id)
        {
            var result = await _additionalServicesService.GetOneAsync(id);
            return result.IsSuccessful ? Ok(result.Value) : BadRequest(result.Error.Details);
        }
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Create([FromBody] AdditionalServicesRequestDto additionalServicesReqestDto)
        {
            var result = await _additionalServicesService.AddAsync(additionalServicesReqestDto);
            return result.IsSuccessful ? CreatedAtAction(nameof(GetOne), new { id = result.Value.Id }, result.Value) : BadRequest(result.Error.Details);
        }

        [HttpPut("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] AdditionalServicesRequestDto additionalServicesReqestDto)
        {
            var result = await _additionalServicesService.UpdateAsync(id, additionalServicesReqestDto);
            return result.IsSuccessful ? NoContent() : BadRequest(result.Error.Details);
        }

        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            var result = await _additionalServicesService.DeleteAsync(id);
            return result.IsSuccessful ? NoContent() : BadRequest(result.Error.Details);
        }
    }
}