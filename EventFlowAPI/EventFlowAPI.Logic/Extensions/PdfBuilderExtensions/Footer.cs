using EventFlowAPI.Logic.Helpers.PdfOptions;
using QuestPDF.Fluent;
using QuestPDF.Infrastructure;

namespace EventFlowAPI.Logic.Extensions.PdfBuilderExtensions
{
    public static class Footer
    {
        public static void AddFooterLogoItem(this ColumnDescriptor column, byte[] logo, FooterOptions options)
        {
            column.Item()
            .Height(options.Height)
            .Image(logo)
            .FitHeight();
        }

        public static void AddPageNumberItem(this ColumnDescriptor column)
        {
            column.Item()
            .AlignCenter()
            .Text(x =>
            {
                x.CurrentPageNumber();
            });
        }
        public static void AddFooterLogoAndPageNumber(this IContainer container, byte[] logo, FooterOptions options)
        {
            container
            .Column(column =>
            {
                column.AddFooterLogoItem(logo, options);
                column.AddPageNumberItem();            
            });
        }
    }
}
