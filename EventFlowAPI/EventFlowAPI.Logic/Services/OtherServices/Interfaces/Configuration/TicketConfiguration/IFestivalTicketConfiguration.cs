using EventFlowAPI.DB.Entities;
using EventFlowAPI.Logic.Helpers.TicketOptions;
using EventFlowAPI.Logic.Services.OtherServices.Interfaces.Configuration.TicketConfiguration.Abstract;

namespace EventFlowAPI.Logic.Services.OtherServices.Interfaces.Configuration.TicketConfiguration
{
    public interface IFestivalTicketConfiguration : ITicketConfiguration<Festival>
    {
        void SetDefaultPrintingParams();
        int GetReverseTicketCount(int reservationsCount);
        string GetEventInfo(Event eventEntity);
        string GetSeatsString(Reservation reservation);
        short TabsCount { get; }
        void MoveCursorToNextTab(int tabNr);
        bool IsNextPageOfReverseTicket();
        PrintingOptions GetTabNumberPrintingOptions();
        PrintingOptions GetEventInfoPrintingOpitons();
        PrintingOptions GetSeatsPrintingOpitons();
    }
}
