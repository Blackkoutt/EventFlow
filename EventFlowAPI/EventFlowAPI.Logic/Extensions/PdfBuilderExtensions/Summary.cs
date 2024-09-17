using EventFlowAPI.Logic.Helpers.PdfOptions.PdfSummaryOptions;
using EventFlowAPI.Logic.Helpers.PdfOptions.PdfTextOptions;
using QuestPDF.Fluent;
using QuestPDF.Infrastructure;

namespace EventFlowAPI.Logic.Extensions.PdfBuilderExtensions
{
    public static class Summary
    {
        public static void AddSummaryContainer(this IContainer column, SummaryOptions options)
        {
            column
            .PaddingLeft(options.ContainerPadLeft)
            .Row(row =>
            {
                row.Spacing(options.ContainerRowSpacing);

                row.RelativeItem(options.ContainerSummaryDescriptionRowWidth)
                .AddSummaryDescription(options);

                row.RelativeItem(options.ContainerSummaryRowWidth)
                .AddSummary(options);
            });
        }

        public static void AddSummary(this IContainer row, SummaryOptions options)
        {
            row
            .AlignRight()
            .PaddingRight(options.PadRight)
            .Column(column =>
            {
                column.Spacing(options.ColumnSpacing);
                column.AddSummaryItem(options.TicketPrice);
                column.AddSummaryItem(options.SeatsCount);
                column.AddSummaryItem(options.AdditionalPayments);
                column.AddSummaryItem(options.TotalCost);
            });
        }

        public static void AddSummaryItem(this ColumnDescriptor column, SummaryTextOptions options)
        {     
            column
            .Item()
            .Background(options.TextBackgound)
            .PaddingVertical(options.PadVertical)
            .PaddingHorizontal(options.PadHorizontal)
            .Row(row =>
            {
                row.RelativeItem(options.LabelWidth)
                .AlignLeft()
                .Text(options.Label)
                .Style(options.Style);

                row.RelativeItem(options.ValueWidth)
                .AlignRight()
                .AlignMiddle()
                .Text(options.Value)
                .Style(options.Style);
            });
        }

        public static void AddSummaryDescription(this IContainer row, SummaryOptions options)
        {
            row
            .Row(row =>
            {
                row.RelativeItem(options.DescriptionAdditionalPaymentRowWidth).AddAdditionalPayment(options.AdditionalPayment);
                row.RelativeItem(options.DescriptionAdditionalPaymentRowWidth).AddTicketTypes(options.TicketType);
            });
        }

        public static void AddAdditionalPayment(this IContainer row, AdditionalPaymentOptions options)
        {
            row
            .Column(column => {
                column.Spacing(options.ColumnSpacing);

                column.AddTextItem(options.Header);

                foreach(var seatType in options.SeatTypes)
                {
                    column.AddTextItem(options.GetSeatTypeString(seatType));
                }
            });
        }

        public static void AddTicketTypes(this IContainer row, TicketTypeOptions options)
        {
            row
            .Column(column => {
                column.Spacing(options.ColumnSpacing);

                column.AddTextItem(options.Header);
                foreach (var ticketType in options.TicketTypes)
                {
                    column.AddTextItem(options.GetTicketTypeString(ticketType));
                }
            });
        }
    }
}
