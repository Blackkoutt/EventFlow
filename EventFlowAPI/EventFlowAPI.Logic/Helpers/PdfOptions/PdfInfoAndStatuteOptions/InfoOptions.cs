namespace EventFlowAPI.Logic.Helpers.PdfOptions.PdfInfoAndStatuteOptions
{
    public class InfoOptions
    {
        public List<string> TicketInfoList => new List<string>()
        {
            "Ważne informacje",
            "1. Nie udostępniaj swojego biletu osobom trzecim!",
            "2. Bilet uprawnia do jednorazowego wejścia na wydarzenie. W przypadku festiwali umożliwia wymianę na opaskę lub inny identyfikator określony przez Organizatora.",
            "3. Kod biletu działa tylko raz. Jakiekolwiek powielanie lub udostępnianie biletu spowoduje jego unieważnienie.",
        };

        public List<string> EventPassInfoList => new List<string>()
        {
            "Ważne informacje",
            "1. Karnet uprawnia do udziału we wszystkich wydarzeniach o ile nadal posiada ważność.",
            "2. Karnet jest imienny przez co podczas wstępu na wydarzenie wymagane jest potwierdzenie tożsamości np za pomocą dowodu osobistego.",
            "3. Jakiekolwiek powielanie lub fałszowanie karnetu jest traktowane jako celowa próba oszustwa.",
        };

        public float Width => 1f;
        public float PadLeft => 10f;
    }
}
