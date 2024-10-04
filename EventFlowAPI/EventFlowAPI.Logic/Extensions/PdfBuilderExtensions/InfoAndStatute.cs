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
        public static void AddInfoAndStatute(this IContainer container, InfoAndStatuteOptions options, Reservation? reservation = null)
        {
            container
            .Column(column =>
            {
                column.Item()
                .Row(row =>
                {
                    row.Spacing(options.InfoAndStatuteRowSpacing);

                    row.RelativeItem(options.InfoWidth)
                    .PaddingLeft(options.InfoPadLeft)
                    .AddInfoStatuteContent(options.Info, options);

                    row.RelativeItem(options.StatuteWidth)
                    .PaddingRight(options.StatutePadRight)
                    .AddInfoStatuteContent(options.Statute, options);
                });

                if(options is InfoAndStatuteTicketOptions ticketOptions)
                {
                    column.Item()
                    .PaddingLeft(ticketOptions.AdditionalContentPadLeft)
                    .PaddingTop(ticketOptions.AdditionalContentPadTop)
                    .Text(ticketOptions.OrganizerContent.Text)
                    .Style(ticketOptions.OrganizerContent.Style);

                    if (reservation is not null && reservation.EventPass is not null)
                    {
                        column.Item()
                        .PaddingLeft(ticketOptions.AdditionalContentPadLeft)
                        .PaddingTop(ticketOptions.AdditionalContentPadTop)
                        .Text(ticketOptions.PaymentAsEventPassContent.Text)
                        .Style(ticketOptions.PaymentAsEventPassContent.Style);
                    }
                }
                
            });
        }
    }
}
