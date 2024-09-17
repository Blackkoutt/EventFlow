using EventFlowAPI.Logic.Helpers.PdfOptions.PdfCommonOptions;
using EventFlowAPI.Logic.Helpers.PdfOptions.PdfTextOptions;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using QuestPDF.Fluent;
using QuestPDF.Infrastructure;

namespace EventFlowAPI.Logic.Extensions.PdfBuilderExtensions
{
    public static class Common
    {
        public static IContainer AddBottomLine(this IContainer container, CommonOptions options)
        {
            return container
            .PaddingBottom(options.BottomLine.PadTop)
            .BorderBottom(options.BottomLine.Width, options.BottomLine.Unit)
            .PaddingBottom(options.BottomLine.PadBottom);
        }
        public static IContainer AddTopLine(this IContainer container, CommonOptions options)
        {
            return container
            .PaddingTop(options.TopLine.PadTop)
            .BorderTop(options.TopLine.Width, options.TopLine.Unit)
            .PaddingTop(options.TopLine.PadBottom);
        }
        public static void AddTextItem(this ColumnDescriptor column, TextOptions options) 
        {
            column.Item()
            .PaddingBottom(options.PaddingBottom)
            .Text(options.Text)
            .Style(options.Style);
        }
    }
}
