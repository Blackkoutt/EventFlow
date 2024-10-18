using EventFlowAPI.Logic.Helpers.Enums;
using EventFlowAPI.Logic.Helpers.PdfOptions.PdfTextOptions;
using QuestPDF.Fluent;
using QuestPDF.Infrastructure;

namespace EventFlowAPI.Logic.Helpers.PdfOptions.PdfInfoOptions
{
    public abstract class InfoOptions
    {
        protected readonly string defaultFontType = FontType.Inter.ToString();
        public float PadLeft => 10f;

        public TextOptions OrderNumber => new TextOptions
        {
            Text = $"{OrderIdLabel} {OrderId}",
            PaddingBottom = 4f,
            Style = TextStyle.Default.FontFamily(defaultFontType).FontSize(12f).SemiBold()
        };

        public TextOptions OrderDate => new TextOptions
        {
            Text = $"{DateLabel} {DateOfOrder}",
            PaddingBottom = 2.5f,
            Style = TextStyle.Default.FontFamily(defaultFontType).FontSize(11f)
        };

        public TextOptions PaymentType => new TextOptions
        {
            Text = $"Typ płatności: {TypeOfPayment}",
            PaddingBottom = 2.5f,
            Style = TextStyle.Default.FontFamily(defaultFontType).FontSize(11f)
        };


        public TextOptions OrderingPerson => new TextOptions
        {
            Text = GetOrderingPerson(),
            PaddingBottom = 0f,
            Style = TextStyle.Default.FontFamily(defaultFontType).FontSize(11f)
        };

        private string GetOrderingPerson()
        {
            if (string.IsNullOrEmpty(UserName) || string.IsNullOrEmpty(UserSurname))
            {
                return $"Osoba zamawiająca: {UserEmail}";
            }
            return $"Osoba zamawiająca: {UserName} {UserSurname}";
        }

        protected abstract string OrderIdLabel { get; }
        protected abstract string OrderId { get; }
        protected abstract string DateLabel { get; }
        protected abstract string DateOfOrder { get; }
        protected abstract string TypeOfPayment { get; }
        protected abstract string UserName { get; }
        protected abstract string UserSurname { get; }
        protected abstract string UserEmail { get; }
    }
}
