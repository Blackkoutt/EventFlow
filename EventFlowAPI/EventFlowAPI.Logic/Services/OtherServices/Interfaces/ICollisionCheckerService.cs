using EventFlowAPI.Logic.DTO.Abstract;

namespace EventFlowAPI.Logic.Services.OtherServices.Interfaces
{
    public interface ICollisionCheckerService
    {
        Task<bool> CheckTimeCollisionsWithHallRents(ICollisionalRequestDto newEntityRequest, int? hallRentId = null);
        Task<bool> CheckTimeCollisionsWithEvents(ICollisionalRequestDto newEntityRequest, int? eventId = null);
    }
}
