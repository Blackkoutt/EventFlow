using EventFlowAPI.Logic.DTO.Abstract;

namespace EventFlowAPI.Logic.Services.OtherServices.Interfaces
{
    public interface ICollisionCheckerService
    {
        Task<bool> CheckTimeCollisionsWithHallRents(ICollisionalRequestDto newEntityRequest);
        Task<bool> CheckTimeCollisionsWithEvents(ICollisionalRequestDto newEntityRequest);
    }
}
