using EventFlowAPI.DB.Entities;

namespace EventFlowAPI.Logic.Services.OtherServices.Interfaces
{
    public interface ITicketCreatorService
    {
        Task<byte[]> CreateEventTicket(Reservation reservation);
        Task<List<byte[]>> CreateFestivalTicket(Festival festival, List<Reservation> reservations);
    }
}
