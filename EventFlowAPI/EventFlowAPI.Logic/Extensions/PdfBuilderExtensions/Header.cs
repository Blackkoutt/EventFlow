using EventFlowAPI.Logic.Helpers.PdfOptions;
using Microsoft.AspNetCore.Builder;
using QuestPDF.Fluent;
using QuestPDF.Infrastructure;

namespace EventFlowAPI.Logic.Extensions.PdfBuilderExtensions
{
    public static class Header
    {
        public static void AddHeaderLogo(this IContainer header, byte[] logo, HeaderOptions options)
        {
            header
            .Height(options.Height)
            .AlignLeft()
            .ExtendHorizontal()
            .Image(logo)
            .FitHeight();
        }
    }
}
