using EventFlowAPI.Logic.Helpers.PdfOptions;
using QuestPDF.Fluent;
using QuestPDF.Infrastructure;

namespace EventFlowAPI.Logic.Extensions.PdfBuilderExtensions
{
    public static class Footer
    {
        public static void AddFooterLogoItem(this IContainer container, byte[] logo, FooterOptions options)
        {
            container
            .Height(options.Height)
            .Image(logo)
            .FitHeight();
        }

        public static void AddPageNumber(this IContainer container)
        {
            container
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
                column.Item().AddFooterLogoItem(logo, options);
                column.Item().AddPageNumber();            
            });
        }
    }
}
