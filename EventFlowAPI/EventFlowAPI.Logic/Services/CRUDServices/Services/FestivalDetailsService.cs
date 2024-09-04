﻿using EventFlowAPI.DB.Entities;
using EventFlowAPI.Logic.DTO.RequestDto;
using EventFlowAPI.Logic.DTO.ResponseDto;
using EventFlowAPI.Logic.Services.CRUDServices.Interfaces;
using EventFlowAPI.Logic.Services.CRUDServices.Services.BaseServices;
using EventFlowAPI.Logic.UnitOfWork;

namespace EventFlowAPI.Logic.Services.CRUDServices.Services
{
    public sealed class FestivalDetailsService(IUnitOfWork unitOfWork) :
        GenericService<
            FestivalDetails,
            FestivalDetailsRequestDto,
            FestivalDetailsResponseDto
        >(unitOfWork),
        IFestivalDetailsService
    {
        protected sealed override Task<bool> IsSameEntityExistInDatabase(FestivalDetailsRequestDto entityDto, int? id = null)
        {
            throw new NotImplementedException();
        }
    }
}