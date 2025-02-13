﻿using EventFlowAPI.DB.Entities;
using EventFlowAPI.Logic.DTO.RequestDto;
using EventFlowAPI.Logic.DTO.ResponseDto;
using EventFlowAPI.Logic.DTO.UpdateRequestDto;
using EventFlowAPI.Logic.Query;
using EventFlowAPI.Logic.Services.CRUDServices.Interfaces.BaseInterfaces;

namespace EventFlowAPI.Logic.Services.CRUDServices.Interfaces
{
    public interface IMediaPatronService :
        IGenericService<
            MediaPatron,
            MediaPatronRequestDto,
            UpdateMediaPatronRequestDto,
            MediaPatronResponseDto,
            MediaPatronQuery
        >
    {
        IEnumerable<MediaPatronResponseDto> MapAsDto(IEnumerable<MediaPatron> records);
        MediaPatronResponseDto MapAsDto(MediaPatron entity);
    }
}
