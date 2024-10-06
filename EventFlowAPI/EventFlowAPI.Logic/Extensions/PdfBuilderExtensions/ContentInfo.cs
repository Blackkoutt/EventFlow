using EventFlowAPI.Logic.Helpers.PdfOptions.PdfContentOptions;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;

namespace EventFlowAPI.Logic.Extensions.PdfBuilderExtensions
{
    public static class ContentInfo
    {
        public static void AddEventOrFestivalContent(this IContainer column, ContentFestivalEventOptions options)
        {
            column
            .PaddingLeft(options.PadLeft)
            .AlignLeft()
            .Column(column =>
            {
                if (options.IsFestival)
                {
                    column.AddTextItem(options.Festival.StartDate);
                    column.AddTextItem(options.Festival.Category);
                    column.AddTextItem(options.Festival.Name);
                }
                else
                {
                    column.AddTextItem(options.Event.StartDate);
                    column.AddTextItem(options.Event.Hall);
                    column.AddTextItem(options.Event.Category);
                    column.AddTextItem(options.Event.Name);
                }
            });
        }

        public static void AddHallRentContent(this IContainer column, ContentHallRentOptions options)
        {
            column
            .PaddingLeft(options.PadLeft)
            .AlignLeft()      
            .Column(column =>
            {
                column.AddTextItem(options.HallNr);
                column.AddTextItem(options.StartDate);
                column.AddTextItem(options.EndDate);
                column.Item()
                .Row(row =>
                {
                    row.RelativeItem()
                    .AlignCenter()
                    .Column(column =>
                    {
                        column.AddTextItem(options.RentDuration);
                        column.AddTextItem(options.HallSeatsCount);
                        column.AddTextItem(options.HallMaxSeatsCount);
                    });
                    row.RelativeItem()
                    .AlignCenter()
                    .Column(column =>
                    {
                        column.AddTextItem(options.HallDimensions);
                        column.AddTextItem(options.HallArea);
                        column.AddTextItem(options.StageArea());
                        
                    });
                });
                column.AddTextItem(options.HallType);
                column.AddTextItem(options.Equipment);
            });
        }
    }
}
