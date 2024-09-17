using EventFlowAPI.Logic.Helpers.PdfOptions;
using QuestPDF.Fluent;
using QuestPDF.Infrastructure;

namespace EventFlowAPI.Logic.Extensions.PdfBuilderExtensions
{
    public static class ReservationInfo
    {
        public static void AddReservationInfo(this IContainer column, ReservationInfoOptions options)
        {
            column
            .AlignLeft()
            .ExtendHorizontal()
            .PaddingLeft(options.PadLeft)
            .Column(column =>
            {
                column.AddTextItem(options.OrderNumber);

                column.AddTextItem(options.TicketOrderDate);

                column.AddTextItem(options.TicketType);

                column.AddTextItem(options.PaymentType);

                column.AddTextItem(options.OrderingPerson);

            });
        }
    }
}
