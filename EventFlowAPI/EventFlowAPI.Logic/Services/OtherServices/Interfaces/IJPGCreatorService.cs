using EventFlowAPI.DB.Entities;

namespace EventFlowAPI.Logic.Services.OtherServices.Interfaces
{
    public interface IJPGCreatorService
    {
        Task<byte[]> CreateEventPass(EventPass eventPass);
        Task<byte[]> CreateEventTicket(Reservation reservation);
        Task<List<byte[]>> CreateFestivalTicket(Festival festival, List<Reservation> reservations);
        Task<byte[]> CreateHallJPG(Hall hall);
    }
}
