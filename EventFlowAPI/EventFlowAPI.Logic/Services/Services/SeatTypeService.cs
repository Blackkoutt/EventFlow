using EventFlowAPI.DB.Entities;
using EventFlowAPI.Logic.DTO.ResponseDto;
using EventFlowAPI.Logic.Services.Interfaces;
using EventFlowAPI.Logic.Services.Services.BaseServices;
using EventFlowAPI.Logic.UnitOfWork;

namespace EventFlowAPI.Logic.Services.Services
{
    public sealed class SeatTypeService(IUnitOfWork unitOfWork) : GenericService<SeatType, SeatTypeResponseDto>(unitOfWork), ISeatTypeService
    {
    }
}
