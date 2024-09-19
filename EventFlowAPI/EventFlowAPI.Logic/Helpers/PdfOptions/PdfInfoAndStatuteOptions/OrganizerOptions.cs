using EventFlowAPI.Logic.Helpers.Enums;
using EventFlowAPI.Logic.Helpers.PdfOptions.PdfTextOptions;
using QuestPDF.Fluent;
using QuestPDF.Infrastructure;

namespace EventFlowAPI.Logic.Helpers.PdfOptions.PdfInfoAndStatuteOptions
{
    public class OrganizerOptions
    {
        public float PadLeft => 10f;
        public float PadTop => 10f;
        public TextOptions Content => new TextOptions
        {
            Text = "Ogranizatorem wydarzenia jest EventFlow",
            Style = TextStyle.Default.FontFamily(FontType.Inter.ToString()).FontSize(9.5f)
        };
    }
}
