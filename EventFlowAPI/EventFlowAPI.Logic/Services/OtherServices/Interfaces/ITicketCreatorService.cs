using EventFlowAPI.DB.Entities;

namespace EventFlowAPI.Logic.Services.OtherServices.Interfaces
{
    public interface ITicketCreatorService
    {
        Task<byte[]> CreateEventTicketJPEG(Reservation reservation);
        Task<List<byte[]>> CreateFestivalTicketPNG(Festival festival, List<Reservation> reservations);
    }
}
