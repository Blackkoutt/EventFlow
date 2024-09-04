using EventFlowAPI.DB.Entities;
using EventFlowAPI.Logic.DTO.RequestDto;
using EventFlowAPI.Logic.DTO.ResponseDto;
using EventFlowAPI.Logic.Services.CRUDServices.Interfaces;
using EventFlowAPI.Logic.Services.CRUDServices.Services.BaseServices;
using EventFlowAPI.Logic.UnitOfWork;

namespace EventFlowAPI.Logic.Services.CRUDServices.Services
{
    public sealed class SeatService(IUnitOfWork unitOfWork) :
        GenericService<
            Seat,
            SeatRequestDto,
            SeatResponseDto
        >(unitOfWork),
        ISeatService
    {
        public bool IsSeatHaveActiveReservationForEvent(Seat seatEntity, Event eventEntity) =>
            seatEntity.Reservations.Any(r => r.IsReservationActive && r.Ticket?.Event.Id == eventEntity.Id);

        protected sealed override Task<bool> IsSameEntityExistInDatabase(SeatRequestDto entityDto, int? id = null)
        {
            throw new NotImplementedException();
        }
    }
}
