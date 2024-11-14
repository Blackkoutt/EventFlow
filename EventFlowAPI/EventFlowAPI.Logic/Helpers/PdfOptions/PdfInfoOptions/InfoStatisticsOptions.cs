using EventFlowAPI.Logic.Helpers.Enums;
using EventFlowAPI.Logic.Helpers.PdfOptions.PdfTextOptions;
using QuestPDF.Fluent;
using QuestPDF.Infrastructure;

namespace EventFlowAPI.Logic.Helpers.PdfOptions.PdfInfoOptions
{
    public class InfoStatisticsOptions(DateTime startDate, DateTime endDate, Guid reportGuid)
    {
        private readonly string defaultFontType = FontType.Inter.ToString();
        public float PadLeft => 10f;

        public LabelValueTextOptions ReportNr => new LabelValueTextOptions
        {
            Label = "Numer raportu: ",
            LabelStyle = TextStyle.Default.FontFamily(defaultFontType).SemiBold().FontSize(12f),
            Value = $"{reportGuid}",
            ValueStyle = TextStyle.Default.FontFamily(defaultFontType).FontSize(12f),
            Space = 5f,
            PaddingBottom = 2.5f
        };

        public LabelValueTextOptions ReportFromToDate => new LabelValueTextOptions
        {
            Label = "Raport dotyczy dat: ",
            LabelStyle = TextStyle.Default.FontFamily(defaultFontType).SemiBold().FontSize(12f),
            Value = $"{startDate.ToString(DateFormat.Date)} - {endDate.ToString(DateFormat.Date)}",
            ValueStyle = TextStyle.Default.FontFamily(defaultFontType).FontSize(12f),
            Space = 5f,
            PaddingBottom = 2.5f
        };

        public LabelValueTextOptions ReportDate => new LabelValueTextOptions
        {
            Label = "Data wygenerowania raportu: ",
            LabelStyle = TextStyle.Default.FontFamily(defaultFontType).SemiBold().FontSize(12f),
            Value = $"{DateTime.Now.ToString(DateFormat.DateTime)}",
            ValueStyle = TextStyle.Default.FontFamily(defaultFontType).FontSize(12f),
            Space = 5f,
            PaddingBottom = 2.5f
        };

        public LabelValueTextOptions User => new LabelValueTextOptions
        {
            Label = "Użytkownik generujący raport: ",
            LabelStyle = TextStyle.Default.FontFamily(defaultFontType).SemiBold().FontSize(12f),
            Value = $"Admin",
            ValueStyle = TextStyle.Default.FontFamily(defaultFontType).FontSize(12f),
            Space = 5f,
            PaddingBottom = 0f
        };
    }
}
