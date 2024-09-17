using EventFlowAPI.Logic.Helpers.Enums;
using EventFlowAPI.Logic.Helpers.TicketOptions;
using SixLabors.ImageSharp;

namespace EventFlowAPI.Logic.Services.OtherServices.Interfaces.TicketConfiguration.Abstract
{
    public interface ITicketConfiguration<TEntity> where TEntity : class
    {
        TicketTitlePrintingOptions GetTitlePrintingOptions(TEntity entity);
        TicketDatePrintingOptions GetDatePrintingOptions();
        TicketQRCodePrintingOptions GetQRCodePrintingOptions();
    }
}
