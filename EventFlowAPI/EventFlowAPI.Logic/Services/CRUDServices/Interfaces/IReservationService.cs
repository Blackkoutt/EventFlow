﻿using EventFlowAPI.DB.Entities;
using EventFlowAPI.Logic.DTO.RequestDto;
using EventFlowAPI.Logic.DTO.ResponseDto;
using EventFlowAPI.Logic.ResultObject;
using EventFlowAPI.Logic.Services.CRUDServices.Interfaces.BaseInterfaces;

namespace EventFlowAPI.Logic.Services.CRUDServices.Interfaces
{
    public interface IReservationService :
        IGenericService<
            Reservation,
            ReservationRequestDto,
            ReservationResponseDto
        >
    {
        Task<Result<ReservationResponseDto>> MakeReservation(ReservationRequestDto? requestDto);
    }
}
