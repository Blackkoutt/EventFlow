using EventFlowAPI.DB.Entities;
using EventFlowAPI.Logic.Helpers.TicketOptions;
using EventFlowAPI.Logic.Services.OtherServices.Interfaces.TicketCreatorConfiguration.Abstract;

namespace EventFlowAPI.Logic.Services.OtherServices.Interfaces.TicketCreatorConfiguration
{
    public interface IEventTicketCreatorConfigurationService : ITicketCreatorConfigurationService
    {
        TicketTitlePrintingOptions GetTitlePrintingOptions(Event eventEntity);
        TicketDatePrintingOptions GetDatePrintingOptions();
        TicketPricePrintingOptions GetPricePrintingOptions(Reservation reservation);
        TicketPrintingOptions GetHallPrintingOptions();
        TicketPrintingOptions GetDurationPrintingOpitons(Event eventEntity);
        TicketPrintingOptions GetSeatsPrintingOptions();
        TicketQRCodePrintingOptions GetQRCodePrintingOptions();
    }
}
