using EventFlowAPI.Logic.Helpers.Enums;
using EventFlowAPI.Logic.Helpers.PdfOptions.PdfTextOptions;
using QuestPDF.Fluent;
using QuestPDF.Infrastructure;

namespace EventFlowAPI.Logic.Helpers.PdfOptions.PdfInfoAndStatuteOptions
{
    public class EventPassOptions
    {
        public float PadLeft => 10f;
        public float PadTop => 10f;
        public TextOptions Content => new TextOptions
        {
            Text = "[UWAGA] Bilet został przypisany do posiadanego aktywnego karnetu. " +
                    "Wstęp na wydarzenie jest możliwy po okazaniu posiadanego karnetu lub powyższego biletu.",
            Style = TextStyle.Default.FontFamily(FontType.Inter.ToString()).FontSize(9f).SemiBold()
        };
    }
}
