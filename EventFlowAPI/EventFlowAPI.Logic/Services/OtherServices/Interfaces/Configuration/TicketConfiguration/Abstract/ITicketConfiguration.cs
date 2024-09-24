using EventFlowAPI.Logic.Helpers.Enums;
using EventFlowAPI.Logic.Helpers.TicketOptions;
using SixLabors.ImageSharp;

namespace EventFlowAPI.Logic.Services.OtherServices.Interfaces.Configuration.TicketConfiguration.Abstract
{
    public interface ITicketConfiguration<TEntity> where TEntity : class
    {
        TicketTitlePrintingOptions GetTitlePrintingOptions(TEntity entity);
        DatePrintingOptions GetDatePrintingOptions();
        QRCodePrintingOptions GetQRCodePrintingOptions();
    }
}
