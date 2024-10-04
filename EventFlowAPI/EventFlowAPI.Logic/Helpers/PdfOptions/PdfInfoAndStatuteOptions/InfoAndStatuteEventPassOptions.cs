namespace EventFlowAPI.Logic.Helpers.PdfOptions.PdfInfoAndStatuteOptions
{
    public sealed class InfoAndStatuteEventPassOptions : InfoAndStatuteOptions
    {
        public sealed override List<string> Info => new List<string>()
        {
            "Ważne informacje",
            "1. Karnet uprawnia do udziału we wszystkich wydarzeniach o ile nadal posiada ważność.",
            "2. Karnet jest imienny przez co podczas wstępu na wydarzenie wymagane jest potwierdzenie tożsamości np za pomocą dowodu osobistego.",
            "3. Jakiekolwiek powielanie lub fałszowanie karnetu jest traktowane jako celowa próba oszustwa.",
        };
        public sealed override List<string> Statute => new List<string>()
        {
            "Regulamin",
            "1. Zakupienie karnetu oznacza akceptację regulaminu organizatora wydarzenia, obiektu, w którym odbywa się wydarzenie, oraz ogólnych warunków sprzedaży.",
            "2. Organizator zastrzega sobie prawo do odmowy wstępu posiadaczom karnetów jeśli nie posiadają ważnego dokumentu tożsamości lub ich dane osobowe są niezgodne z danymi nabywcy.",
            "3. Karnet nie podlega dalszej odsprzedaży, ani przekazaniu osobie trzeciej.",
        };
    }
}
