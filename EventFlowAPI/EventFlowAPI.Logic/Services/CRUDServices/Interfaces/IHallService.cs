﻿using EventFlowAPI.DB.Entities;
using EventFlowAPI.Logic.DTO.RequestDto;
using EventFlowAPI.Logic.DTO.ResponseDto;
using EventFlowAPI.Logic.ResultObject;
using EventFlowAPI.Logic.Services.CRUDServices.Interfaces.BaseInterfaces;

namespace EventFlowAPI.Logic.Services.CRUDServices.Interfaces
{
    public interface IHallService :
        IGenericService<
            Hall,
            HallRequestDto,
            HallResponseDto
        >
    {
        Task<Result<HallResponseDto>> UpdateHallForEvent(int hallId, int eventId, HallRequestDto? requestDto);
    }
}