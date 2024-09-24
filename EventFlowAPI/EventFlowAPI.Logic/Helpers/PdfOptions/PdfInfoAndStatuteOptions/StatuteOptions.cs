namespace EventFlowAPI.Logic.Helpers.PdfOptions.PdfInfoAndStatuteOptions
{
    public class StatuteOptions
    {
        public List<string> TicketStatuteList => new List<string>()
        {
            "Regulamin",
            "1. Zakupienie biletu oznacza akceptację regulaminu organizatora wydarzenia, obiektu, w którym odbywa się wydarzenie, oraz ogólnych warunków sprzedaży.",
            "2. Organizator zastrzega sobie prawo do odmowy wstępu posiadaczom biletów o identycznym kodzie.",
            "3. Bilet elektroniczny nie podlega dalszej odsprzedaży.",
        };

        public List<string> EventPassStatuteList => new List<string>()
        {
            "Regulamin",
            "1. Zakupienie karnetu oznacza akceptację regulaminu organizatora wydarzenia, obiektu, w którym odbywa się wydarzenie, oraz ogólnych warunków sprzedaży.",
            "2. Organizator zastrzega sobie prawo do odmowy wstępu posiadaczom karnetów jeśli nie posiadają ważnego dokumentu tożsamości lub ich dane osobowe są niezgodne z danymi nabywcy.",
            "3. Karnet nie podlega dalszej odsprzedaży, ani przekazaniu osobie trzeciej.",
        };

        public float Width => 1f;
        public float PadRight => 10f;
    }
}
