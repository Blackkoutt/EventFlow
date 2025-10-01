using EventFlowAPI.Logic.Enums;
using EventFlowAPI.Logic.Helpers.PdfOptions.PdfTextOptions;
using QuestPDF.Fluent;
using QuestPDF.Infrastructure;

namespace EventFlowAPI.Logic.Helpers.PdfOptions.PdfInfoAndStatuteOptions
{
    public sealed class InfoAndStatuteTicketOptions : InfoAndStatuteOptions
    {
        public sealed override List<string> Info => new List<string>()
        {
            "Ważne informacje",
            "1. Nie udostępniaj swojego biletu osobom trzecim!",
            "2. Bilet uprawnia do jednorazowego wejścia na wydarzenie. W przypadku festiwali umożliwia wymianę na opaskę lub inny identyfikator określony przez Organizatora.",
            "3. Kod biletu działa tylko raz. Jakiekolwiek powielanie lub udostępnianie biletu spowoduje jego unieważnienie.",
        };
        public sealed override List<string> Statute => new List<string>()
        {
            "Regulamin",
            "1. Zakupienie biletu oznacza akceptację regulaminu organizatora wydarzenia, obiektu, w którym odbywa się wydarzenie, oraz ogólnych warunków sprzedaży.",
            "2. Organizator zastrzega sobie prawo do odmowy wstępu posiadaczom biletów o identycznym kodzie.",
            "3. Bilet elektroniczny nie podlega dalszej odsprzedaży.",
        };
        public TextOptions OrganizerContent => new TextOptions
        {
            Text = "Ogranizatorem wydarzenia jest EventFlow",
            Style = TextStyle.Default.FontFamily(FontType.Inter.ToString()).FontSize(9.5f)
        };
        public TextOptions PaymentAsEventPassContent => new TextOptions
        {
            Text = "[UWAGA] Bilet został przypisany do posiadanego aktywnego karnetu. " +
            "Wstęp na wydarzenie jest możliwy po okazaniu posiadanego karnetu lub powyższego biletu.",
            Style = TextStyle.Default.FontFamily(FontType.Inter.ToString()).FontSize(9f).SemiBold()
        };
    }
}
