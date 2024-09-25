﻿using EventFlowAPI.Logic.Helpers.Enums;
using EventFlowAPI.Logic.Helpers.PdfOptions.PdfTextOptions;
using QuestPDF.Fluent;
using QuestPDF.Infrastructure;

namespace EventFlowAPI.Logic.Helpers.PdfOptions.PdfInfoAndStatuteOptions
{
    public class InfoAndStatuteOptions
    {
        private readonly string defaultFontType = FontType.Inter.ToString();

        public InfoAndStatuteOptions()
        {
            Info = new();
            Statute = new();
            Organizer = new();
            EventPassInfo = new();
        }

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
        public InfoOptions Info { get; private set; }


        // Statute
        public StatuteOptions Statute { get; private set; }


        // Organizer
        public OrganizerOptions Organizer { get; private set; }


        // EventPassInfo
        public EventPassOptions EventPassInfo { get; private set; }
    }
}
