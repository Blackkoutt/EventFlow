using EventFlowAPI.DB.Entities;

namespace EventFlowAPI.Logic.Services.OtherServices.Interfaces
{
    public interface ITicketCreatorService
    {
        Task<int> CreateEventTicketPNG(Reservation reservation);
        Task<int> CreateFestivalTicketPNG(Festival festival, List<Reservation> reservations);
    }
}
