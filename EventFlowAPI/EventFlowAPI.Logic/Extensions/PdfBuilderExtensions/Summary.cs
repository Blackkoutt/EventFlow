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
                column.AddSummaryItem(options.Price);
                if(options is ReservationSummaryOptions resOptions)
                {
                    if(!resOptions.Reservation.IsFestivalReservation)
                    {
                        column.AddSummaryItem(resOptions.SeatsCount);
                        column.AddSummaryItem(resOptions.AdditionalPayments);
                    }
                    if(resOptions.Reservation.EventPass != null)
                    {
                        column.AddSummaryItem(resOptions.EventPassDiscount);
                    }
                }
                if(options is EventPassSummaryOptions eventPassOptions)
                {
                    column.AddSummaryItem(eventPassOptions.Discount);
                }
                if(options is HallRentSummaryOptions hallRentOptions)
                {
                    column.AddSummaryItem(hallRentOptions.Hours);
                    column.AddSummaryItem(hallRentOptions.AdditionalServicesHeader);
                    foreach (var additionalService in hallRentOptions.AdditionalServicesList)
                    {
                        column.AddSummaryItem(hallRentOptions.GetAdditionalServiceCost(additionalService));
                    }
                }
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
                if(options is ReservationSummaryOptions resOptions)
                {
                    if(!resOptions.Reservation.IsFestivalReservation) 
                    {
                        row.RelativeItem(resOptions.DescriptionAdditionalPaymentRowWidth)
                            .AddDescriptionItem(resOptions.AdditionalPayment);
                    }

                    row.RelativeItem(resOptions.DescriptionAdditionalPaymentRowWidth)
                    .AddDescriptionItem(resOptions.TicketType);
                }
                else if (options is EventPassSummaryOptions eventPassOptions)
                {
                    row.RelativeItem()
                    .AddDescriptionItem(eventPassOptions.EventPassType);
                }
                else if (options is HallRentSummaryOptions hallRentOptions)
                {
                    row.RelativeItem()
                    .AddDescriptionItem(hallRentOptions.AdditionalServices);
                }
            });
        }

        public static void AddDescriptionItem(this IContainer row, DescriptionOptions options)
        {
            row
            .Column(column => {
                column.Spacing(options.ColumnSpacing);

                column.AddTextItem(options.Header);

                foreach (var descriptionItem in options.GetList)
                {
                    column.AddTextItem(options.GetListItemString(descriptionItem));
                }
            });
        }   
    }
}
