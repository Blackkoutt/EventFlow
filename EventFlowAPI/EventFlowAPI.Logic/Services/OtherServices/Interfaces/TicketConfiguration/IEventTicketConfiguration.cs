using EventFlowAPI.DB.Entities;
using EventFlowAPI.Logic.Helpers.TicketOptions;
using EventFlowAPI.Logic.Services.OtherServices.Interfaces.TicketConfiguration.Abstract;

namespace EventFlowAPI.Logic.Services.OtherServices.Interfaces.TicketConfiguration
{
    public interface IEventTicketConfiguration : ITicketConfiguration<Event>
    {
        TicketPricePrintingOptions GetPricePrintingOptions(Reservation reservation);
        TicketPrintingOptions GetHallPrintingOptions();
        TicketPrintingOptions GetDurationPrintingOpitons(Event eventEntity);
        TicketPrintingOptions GetSeatsPrintingOptions();
    }
}
