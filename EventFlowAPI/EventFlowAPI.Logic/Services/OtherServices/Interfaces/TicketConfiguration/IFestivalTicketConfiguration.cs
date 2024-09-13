using EventFlowAPI.DB.Entities;
using EventFlowAPI.Logic.Helpers.TicketOptions;
using EventFlowAPI.Logic.Services.OtherServices.Interfaces.TicketConfiguration.Abstract;

namespace EventFlowAPI.Logic.Services.OtherServices.Interfaces.TicketConfiguration
{
    public interface IFestivalTicketConfiguration : ITicketConfiguration<Festival>
    {
        int GetReverseTicketCount(int reservationsCount);
        string GetEventInfo(Event eventEntity);
        string GetSeatsString(Reservation reservation);
        short TabsCount { get; }
        void MoveCursorToNextTab(int tabNr);
        bool IsNextPageOfReverseTicket();
        TicketPrintingOptions GetTabNumberPrintingOptions();
        TicketPrintingOptions GetEventInfoPrintingOpitons();
        TicketPrintingOptions GetSeatsPrintingOpitons();
    }
}
