using EventFlowAPI.DB.Entities;
using EventFlowAPI.DB.Entities.Abstract;
using EventFlowAPI.Logic.Helpers.Enums;
using EventFlowAPI.Logic.Helpers.PdfOptions.PdfTextOptions;
using QuestPDF.Infrastructure;

namespace EventFlowAPI.Logic.Helpers.PdfOptions.PdfSummaryOptions
{
    public abstract class DescriptionOptions
    {
        protected string defaultFontType = FontType.Inter.ToString();
        public float ColumnSpacing => 1f;
        public abstract List<IEntity> GetList { get; }
        public abstract TextOptions Header { get; }
        public abstract TextOptions GetListItemString(IEntity item);
    }
}
