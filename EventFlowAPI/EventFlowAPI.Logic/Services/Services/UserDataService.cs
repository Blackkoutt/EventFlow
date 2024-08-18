﻿using EventFlowAPI.DB.Entities;
using EventFlowAPI.Logic.DTO.RequestDto;
using EventFlowAPI.Logic.DTO.ResponseDto;
using EventFlowAPI.Logic.Services.Interfaces;
using EventFlowAPI.Logic.Services.Services.BaseServices;
using EventFlowAPI.Logic.UnitOfWork;

namespace EventFlowAPI.Logic.Services.Services
{
    public sealed class UserDataService(IUnitOfWork unitOfWork) :
        GenericService<
            UserData,
            UserDataRequestDto,
            UserDataResponseDto
        >(unitOfWork),
        IUserDataService
    {
        protected sealed override Task<bool> IsSameEntityExistInDatabase(UserDataRequestDto entityDto, int? id = null)
        {
            throw new NotImplementedException();
        }
    }
}
