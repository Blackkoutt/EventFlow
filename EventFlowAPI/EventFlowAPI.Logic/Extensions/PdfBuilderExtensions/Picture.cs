using EventFlowAPI.Logic.Enums;
using EventFlowAPI.Logic.Helpers.PdfOptions.PdfCommonOptions;
using EventFlowAPI.Logic.Helpers.PdfOptions.PdfPictureOptions;
using QuestPDF.Fluent;
using QuestPDF.Infrastructure;

namespace EventFlowAPI.Logic.Extensions.PdfBuilderExtensions
{
    public static class Picture
    {
        public static void AddTicketPictures(this IContainer column, List<byte[]> tickets, PictureOptions ticketOptions, CommonOptions commonOptions)
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
                            .AddPicture(tickets[i], ticketOptions);

                            column.Item()
                            .PageBreak();
                        }
                        else
                        {
                            column.Item()
                            .AddBottomLine(commonOptions)
                            .AddPicture(tickets[i], ticketOptions);
                        }
                    }
                    else
                    {
                        column.Item()
                        .AddPicture(tickets[i], ticketOptions);
                    }
                }
            });
        }
        public static void AddPicture(this IContainer column, byte[] bitmap, PictureOptions options)
        {
            column
            .PaddingTop(options.PadTop)
            .PaddingBottom(options.PadBottom)
            .Image(bitmap);
        }

        public static void AddPlotPicture(this IContainer column, byte[] bitmap, PlotType plotType, PictureOptions options)
        {
            if(plotType == PlotType.Pie)
            {
                column
                    .AlignCenter()
                    .PaddingTop(options.PadTop)
                    .PaddingBottom(options.PadBottom)
                    .Width(310)
                    .Height(310)
                    .Image(bitmap).FitArea();
            }
            else
            {
                column
                    .AlignCenter()
                    .PaddingTop(options.PadTop)
                    .Width(390)
                    .Height(290)
                    .PaddingBottom(options.PadBottom)
                    .Image(bitmap).FitArea();
            }
        }
    }
}
