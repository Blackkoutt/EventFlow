using EventFlowAPI.Logic.Helpers.PdfOptions;
using EventFlowAPI.Logic.Helpers.PdfOptions.PdfCommonOptions;
using Microsoft.AspNetCore.Components.Web.Virtualization;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;

namespace EventFlowAPI.Logic.Extensions.PdfBuilderExtensions
{
    public static class TicketPicture
    {
        public static void AddTicketPictures(this IContainer column, List<byte[]> tickets, TicketPictureOptions ticketOptions, CommonOptions commonOptions)
        {
            column.Column(column =>
            {
                for (var i = 0; i < tickets.Count; i++)
                {
                    if (i == tickets.Count - 1)
                    {
                        if ((tickets.Count + 1) % 3 == 0)
                        {
                            column.Item()
                            .AddTicketPicture(tickets[i], ticketOptions);

                            column.Item()
                            .PageBreak();
                        }
                        else
                        {
                            column.Item()
                            .AddBottomLine(commonOptions)
                            .AddTicketPicture(tickets[i], ticketOptions);
                        }
                    }
                    else
                    {
                        column.Item()
                        .AddTicketPicture(tickets[i], ticketOptions);
                    }
                }
            });
        }
        public static void AddTicketPicture(this IContainer column, byte[] ticketJPEG, TicketPictureOptions options)
        {
            column
            .PaddingTop(options.PadTop)
            .PaddingBottom(options.PadBottom)
            .Image(ticketJPEG);
        }
    }
}
