using EventFlowAPI.DB.Entities;
using EventFlowAPI.Logic.DTO.Abstract;
using EventFlowAPI.Logic.Services.OtherServices.Interfaces;
using EventFlowAPI.Logic.UnitOfWork;
using Microsoft.Extensions.Logging;

namespace EventFlowAPI.Logic.Services.OtherServices.Services
{
    public class CollisionCheckerService(IUnitOfWork unitOfWork) : ICollisionCheckerService
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        public async Task<bool> CheckTimeCollisionsWithHallRents(ICollisionalRequestDto newEntityRequest, int? hallRentId = null)
        {
            return (await _unitOfWork.GetRepository<HallRent>()
                        .GetAllAsync(q =>
                            q.Where(entity =>
                                entity.Id != hallRentId &&
                                !entity.IsCanceled &&
                                entity.EndDate >= DateTime.Now &&
                                entity.Hall.DefaultId == newEntityRequest.HallId &&
                               (newEntityRequest.StartDate <= entity.StartDate &&
                                newEntityRequest.EndDate > entity.StartDate ||
                                newEntityRequest.StartDate < entity.EndDate &&
                                newEntityRequest.EndDate >= entity.EndDate)))).Any();
        }
        public async Task<bool> CheckTimeCollisionsWithEvents(ICollisionalRequestDto newEntityRequest, int? eventId = null)
        {
            return (await _unitOfWork.GetRepository<Event>()
                        .GetAllAsync(q =>
                            q.Where(entity =>
                                entity.Id != eventId &&
                                !entity.IsCanceled &&
                                entity.EndDate >= DateTime.Now &&
                                entity.Hall.DefaultId == newEntityRequest.HallId &&
                               (newEntityRequest.StartDate <= entity.StartDate &&
                                newEntityRequest.EndDate > entity.StartDate ||
                                newEntityRequest.StartDate < entity.EndDate &&
                                newEntityRequest.EndDate >= entity.EndDate)))).Any();
        }
    }
}
