namespace EventFlowAPI.Logic.Helpers.PdfOptions.PdfInfoAndStatuteOptions
{
    public class InfoOptions
    {
        public List<string> List => new List<string>()
        {
            "Ważne informacje",
            "1. Nie udostępniaj swojego biletu osobom trzecim!",
            "2. Bilet uprawnia do jednorazowego wejścia na wydarzenie. W przypadku festiwali umożliwia wymianę na opaskę lub inny identyfikator określony przez Organizatora.",
            "3. Kod biletu działa tylko raz. Jakiekolwiek powielanie lub udostępnianie biletu spowoduje jego unieważnienie.",
        };
        public float Width => 1f;
        public float PadLeft => 10f;
    }
}
