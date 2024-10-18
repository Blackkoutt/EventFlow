using EventFlowAPI.DB.Entities;

namespace EventFlowAPI.Logic.Services.OtherServices.Interfaces
{
    public interface IPdfBuilderService
    {
        Task<byte[]> CreateEventPassPdf(EventPass eventPass, byte[] eventPassJPGBitmap, EventPassType? oldEventPassType);
        Task<byte[]> CreateTicketPdf(Reservation reservation, List<byte[]> tickets);
        Task<byte[]> CreateHallViewPdf(byte[] hallBitmap, Hall hall, HallRent? hallRent = null, Event? eventEntity = null);
        Task<byte[]> CreateHallRentPdf(HallRent hallRent);
    }
}
