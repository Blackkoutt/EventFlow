﻿using EventFlowAPI.DB.Entities;
using EventFlowAPI.Logic.DTO.RequestDto;
using EventFlowAPI.Logic.DTO.ResponseDto;
using EventFlowAPI.Logic.DTO.UpdateRequestDto;
using EventFlowAPI.Logic.Query;
using EventFlowAPI.Logic.Services.CRUDServices.Interfaces.BaseInterfaces;

namespace EventFlowAPI.Logic.Services.CRUDServices.Interfaces
{
    public interface ISponsorService :
        IGenericService<
            Sponsor,
            SponsorRequestDto,
            UpdateSponsorRequestDto,
            SponsorResponseDto,
            SponsorQuery
        >
    {
        IEnumerable<SponsorResponseDto> MapAsDto(IEnumerable<Sponsor> records);
        SponsorResponseDto MapAsDto(Sponsor entity);
    }
}
