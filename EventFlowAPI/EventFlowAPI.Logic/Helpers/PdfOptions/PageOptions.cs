using QuestPDF.Helpers;
using QuestPDF.Infrastructure;

namespace EventFlowAPI.Logic.Helpers.PdfOptions
{
    public class PageOptions
    {
        public PageSize PageSize => PageSizes.A4;
        public float MarginVertical => 1f;
        public float MarginHorizontal => 1.2f;
        public Unit MarginUnit => Unit.Centimetre;
    }
}
