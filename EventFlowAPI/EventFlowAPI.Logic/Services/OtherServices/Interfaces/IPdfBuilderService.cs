using EventFlowAPI.DB.Entities;

namespace EventFlowAPI.Logic.Services.OtherServices.Interfaces
{
    public interface IPdfBuilderService
    {
        Task<byte[]> CreateEventPassPdf(EventPass eventPass, byte[] eventPassJPGBitmap, EventPassType? oldEventPassType);
        Task<byte[]> CreateTicketPdf(Reservation reservation, List<byte[]> tickets);
        Task<int> CreateHallRentPdf(HallRent hallRent);
    }
}
