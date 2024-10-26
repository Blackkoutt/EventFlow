﻿using EventFlowAPI.DB.Entities;
using EventFlowAPI.Logic.DTO.RequestDto;
using EventFlowAPI.Logic.DTO.ResponseDto;
using EventFlowAPI.Logic.DTO.UpdateRequestDto;
using EventFlowAPI.Logic.Errors;
using EventFlowAPI.Logic.ResultObject;
using EventFlowAPI.Logic.Services.CRUDServices.Interfaces.BaseInterfaces;

namespace EventFlowAPI.Logic.Services.CRUDServices.Interfaces
{
    public interface IHallRentService :
        IGenericService<
            HallRent,
            HallRentRequestDto,
            UpdateHallRentRequestDto,
            HallRentResponseDto
        >
    {
        Task<Result<HallRentResponseDto>> MakeRent(HallRentRequestDto? requestDto);
        Task<Error> SendMailsAboutUpdatedHallRents(IEnumerable<HallRent> activeHallRents, Hall hallEntity);
        Task<Error> SoftDeleteHallRent(HallRent hallRent);
    }
}
