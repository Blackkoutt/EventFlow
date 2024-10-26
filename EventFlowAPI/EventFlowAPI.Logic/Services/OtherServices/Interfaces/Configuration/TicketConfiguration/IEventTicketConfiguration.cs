using EventFlowAPI.DB.Entities;
using EventFlowAPI.Logic.Helpers.TicketOptions;
using EventFlowAPI.Logic.Services.OtherServices.Interfaces.Configuration.TicketConfiguration.Abstract;

namespace EventFlowAPI.Logic.Services.OtherServices.Interfaces.Configuration.TicketConfiguration
{
    public interface IEventTicketConfiguration : ITicketConfiguration<Event>
    {
        void SetDefaultPrintingParams();
        PricePrintingOptions GetPricePrintingOptions(Reservation reservation);
        PrintingOptions GetHallPrintingOptions();
        PrintingOptions GetDurationPrintingOpitons(Event eventEntity);
        PrintingOptions GetSeatsPrintingOptions();
    }
}
