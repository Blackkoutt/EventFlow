using EventFlowAPI.Logic.Helpers.Enums;
using EventFlowAPI.Logic.Helpers.PdfOptions.PdfTextOptions;
using QuestPDF.Fluent;
using QuestPDF.Infrastructure;

namespace EventFlowAPI.Logic.Helpers.PdfOptions.PdfInfoAndStatuteOptions
{
    public abstract class InfoAndStatuteOptions
    {
        private readonly string defaultFontType = FontType.Inter.ToString();

        // Common 
        public float InfoAndStatuteRowSpacing => 20f;
        public float HeaderPadBottom => 4f;
        public TextOptions Header => new TextOptions()
        {
            Style = TextStyle.Default.FontFamily(defaultFontType).FontSize(9.5f)
        };
        public float ItemPadBottom => 2.3f;
        public TextOptions Item => new TextOptions()
        {
            Style = TextStyle.Default.FontFamily(defaultFontType).FontSize(8.5f)
        };

        // Info
        public abstract List<string> Info { get; }
        public float InfoWidth => 1f;
        public float InfoPadLeft => 10f;

        // Statute
        public abstract List<string> Statute { get; }
        public float StatuteWidth => 1f;
        public float StatutePadRight => 10f;


        // AdditonalContent
        public float AdditionalContentPadLeft => 10f;
        public float AdditionalContentPadTop => 10f;
    }
}
