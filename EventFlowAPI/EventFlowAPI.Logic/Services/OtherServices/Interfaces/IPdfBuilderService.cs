using EventFlowAPI.DB.Entities;

namespace EventFlowAPI.Logic.Services.OtherServices.Interfaces
{
    public interface IPdfBuilderService
    {
        Task<byte[]> CreateTicketPdf(Reservation reservation, List<byte[]> tickets);
    }
}
