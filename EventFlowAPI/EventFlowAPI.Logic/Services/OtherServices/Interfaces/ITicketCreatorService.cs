using EventFlowAPI.DB.Entities;

namespace EventFlowAPI.Logic.Services.OtherServices.Interfaces
{
    public interface ITicketCreatorService
    {
        Task<int> CreateEventTicketJPG(Reservation reservation);
    }
}
