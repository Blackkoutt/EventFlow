using EventFlowAPI.Logic.Helpers.TicketOptions;

namespace EventFlowAPI.Logic.Services.OtherServices.Interfaces.Configuration.PassConfiguration
{
    public interface IEventPassConfiguration
    {
        PrintingOptions EventPassTypePrintingOptions { get; }
        PrintingOptions EventPassOwnerPrintingOptions { get; }
        DatePrintingOptions EventPassDatePrintingOptions { get; }
        QRCodePrintingOptions EventPassQrCodePrintingOptions { get; }
    }
}
