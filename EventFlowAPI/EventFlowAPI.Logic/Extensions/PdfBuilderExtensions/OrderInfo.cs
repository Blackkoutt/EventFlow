using EventFlowAPI.Logic.Helpers.PdfOptions.PdfInfoOptions;
using QuestPDF.Fluent;
using QuestPDF.Infrastructure;

namespace EventFlowAPI.Logic.Extensions.PdfBuilderExtensions
{
    public static class OrderInfo
    {
        public static void AddOrderInfo(this IContainer column, InfoOptions options)
        {
            column
            .AlignLeft()
            .ExtendHorizontal()
            .PaddingLeft(options.PadLeft)
            .Column(column =>
            {
                column.AddTextItem(options.OrderNumber);

                column.AddTextItem(options.OrderDate);

                if(options is ReservationInfoOptions resOptions)
                {
                    column.AddTextItem(resOptions.TicketType);
                }
                else if(options is EventPassInfoOptions eventPassOptions)
                {
                    column.AddTextItem(eventPassOptions.EventPassRenew);
                    column.AddTextItem(eventPassOptions.OldEventPassType);
                    column.AddTextItem(eventPassOptions.EventPassType);
                }

                column.AddTextItem(options.PaymentType);

                column.AddTextItem(options.OrderingPerson);

            });
        }
    }
}
