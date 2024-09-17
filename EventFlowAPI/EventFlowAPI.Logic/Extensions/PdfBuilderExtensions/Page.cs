using EventFlowAPI.Logic.Helpers.PdfOptions;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;

namespace EventFlowAPI.Logic.Extensions.PdfBuilderExtensions
{
    public static class Page
    {
        public static void ConfigurePage(this PageDescriptor page, PageOptions options)
        {
            page.Size(options.PageSize);
            page.MarginVertical(options.MarginVertical, options.MarginUnit);
            page.MarginHorizontal(options.MarginHorizontal, options.MarginUnit);
            page.ContentFromLeftToRight();
        }
    }
}
