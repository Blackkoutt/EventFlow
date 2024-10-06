using EventFlowAPI.DB.Entities;
using EventFlowAPI.DB.Entities.Abstract;
using EventFlowAPI.Logic.Helpers.Enums;
using EventFlowAPI.Logic.Helpers.PdfOptions.PdfTextOptions;
using QuestPDF.Fluent;
using QuestPDF.Infrastructure;

namespace EventFlowAPI.Logic.Helpers.PdfOptions.PdfSummaryOptions
{
    public sealed class AdditionalServicesOptions(List<AdditionalServices> additionalServices) : DescriptionOptions
    {
        public sealed override TextOptions Header => new TextOptions
        {
            Text = "Dodatkowe usługi:",
            Style = TextStyle.Default.FontFamily(defaultFontType).FontSize(10f)
        };

        public sealed override List<IEntity> GetList => additionalServices.Select(st => (IEntity)st).ToList();

        public sealed override TextOptions GetListItemString(IEntity item)
        {
            var additionalService = (AdditionalServices)item;
            return new TextOptions
            {
                Text = $"- {additionalService.Name}: {additionalService.Price} {Currency.PLN}",
                Style = TextStyle.Default.FontFamily(defaultFontType).FontSize(9f)
            };
        }
    }
}
