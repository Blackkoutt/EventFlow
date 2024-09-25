using EventFlowAPI.DB.Entities;
using EventFlowAPI.Logic.Helpers.PdfOptions.PdfInfoAndStatuteOptions;
using QuestPDF.Fluent;
using QuestPDF.Infrastructure;

namespace EventFlowAPI.Logic.Extensions.PdfBuilderExtensions
{
    public static class InfoAndStatute
    {
        public static void AddInfoStatuteContent(this IContainer row, List<string> textContentList, InfoAndStatuteOptions options)
        {
            row
            .Column(column =>
            {
                column.Item()
                .PaddingBottom(options.HeaderPadBottom)
                .Text(textContentList.FirstOrDefault())
                .Style(options.Header.Style);

                for (var i = 1; i<textContentList.Count; i++)
                {
                    column.Item()
                    .PaddingBottom(options.ItemPadBottom)
                    .Text(textContentList[i])
                    .Style(options.Item.Style);
                }   
            });
        }

        public static void AddEventPassInfoAndStatute(this IContainer container, InfoAndStatuteOptions options)
        {
            container
            .Column(column =>
            {
                column.Item()
                .Row(row =>
                {
                    row.Spacing(options.InfoAndStatuteRowSpacing);

                    row.RelativeItem(options.Info.Width)
                    .PaddingLeft(options.Info.PadLeft)
                    .AddInfoStatuteContent(options.Info.EventPassInfoList, options);

                    row.RelativeItem(options.Statute.Width)
                    .PaddingRight(options.Statute.PadRight)
                    .AddInfoStatuteContent(options.Statute.EventPassStatuteList, options);
                });
            });
        }

        public static void AddTicketInfoAndStatute(this IContainer container, Reservation reservation, InfoAndStatuteOptions options)
        {
            container
            .Column(column =>
            {
                column.Item()
                .Row(row =>
                 {
                     row.Spacing(options.InfoAndStatuteRowSpacing);

                     row.RelativeItem(options.Info.Width)
                     .PaddingLeft(options.Info.PadLeft)
                     .AddInfoStatuteContent(options.Info.TicketInfoList, options);

                     row.RelativeItem(options.Statute.Width)
                     .PaddingRight(options.Statute.PadRight)
                     .AddInfoStatuteContent(options.Statute.TicketStatuteList, options);
                 });

                column.Item()
                .PaddingLeft(options.Organizer.PadLeft)
                .PaddingTop(options.Organizer.PadTop)
                .Text(options.Organizer.Content.Text)
                .Style(options.Organizer.Content.Style);

                if(reservation.EventPass != null)
                {
                    column.Item()
                    .PaddingLeft(options.EventPassInfo.PadLeft)
                    .PaddingTop(options.EventPassInfo.PadTop)
                    .Text(options.EventPassInfo.Content.Text)
                    .Style(options.EventPassInfo.Content.Style);
                }             
            });
        }
    }
}
