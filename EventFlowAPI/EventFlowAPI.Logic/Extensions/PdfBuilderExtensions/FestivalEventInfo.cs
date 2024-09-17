using EventFlowAPI.Logic.Helpers.PdfOptions.PdfFestivalEventInfoOptions;
using QuestPDF.Fluent;
using QuestPDF.Infrastructure;

namespace EventFlowAPI.Logic.Extensions.PdfBuilderExtensions
{
    public static class FestivalEventInfo
    {
        public static void AddEventOrFestivalInfo(this IContainer column, FestivalEventInfoOptions options)
        {
            if (options.IsFestival)
            {
                column
                .PaddingLeft(options.PadLeft)
                .AlignLeft()
                .Column(column =>
                {
                    column.AddTextItem(options.Festival.StartDate);

                    column.AddTextItem(options.Festival.Category);

                    column.AddTextItem(options.Festival.Name);
                });
            }
            else
            {
                column
                .PaddingLeft(options.PadLeft)
                .AlignLeft()
                .Column(column =>
                {
                    column.AddTextItem(options.Event.StartDate);

                    column.AddTextItem(options.Event.Hall);

                    column.AddTextItem(options.Event.Category);

                    column.AddTextItem(options.Event.Name);
                });
            }
        }
    }
}
