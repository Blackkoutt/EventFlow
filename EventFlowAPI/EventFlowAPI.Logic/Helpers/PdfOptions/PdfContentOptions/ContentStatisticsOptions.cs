using EventFlowAPI.Logic.Enums;
using EventFlowAPI.Logic.Helpers.PdfOptions.PdfTextOptions;
using QuestPDF.Fluent;
using QuestPDF.Infrastructure;

namespace EventFlowAPI.Logic.Helpers.PdfOptions.PdfContentOptions
{
    public class ContentStatisticsOptions
    {
        private readonly string defaultFontType = FontType.Inter.ToString();
        public float PadLeft => 10f;

        public LabelValueTextOptions GetLabelValueString(string label, string value, float fontSize = 12f)
        {
            return new LabelValueTextOptions
            {
                Label = label,
                LabelStyle = TextStyle.Default.FontFamily(defaultFontType).SemiBold().FontSize(fontSize),
                Value = value,
                ValueStyle = TextStyle.Default.FontFamily(defaultFontType).FontSize(fontSize),
                Space = 5f,
                PaddingBottom = 2.5f
            };
        }
    }
}
