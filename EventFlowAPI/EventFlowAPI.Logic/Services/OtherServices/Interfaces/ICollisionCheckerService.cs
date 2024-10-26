using EventFlowAPI.Logic.DTO.Abstract;

namespace EventFlowAPI.Logic.Services.OtherServices.Interfaces
{
    public interface ICollisionCheckerService
    {
        Task<bool> CheckTimeCollisionsWithHallRents(int hallId, DateTime startDate, DateTime endDate, int? hallRentId = null);
        Task<bool> CheckTimeCollisionsWithEvents(int hallId, DateTime startDate, DateTime endDate, int? eventId = null);
    }
}
