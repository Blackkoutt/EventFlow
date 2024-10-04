namespace EventFlowAPI.Logic.Helpers.PdfOptions.PdfInfoAndStatuteOptions
{
    public sealed class InfoAndStatuteHallRentOptions : InfoAndStatuteOptions
    {
        public sealed override List<string> Info => new List<string>()
        {
            "Ważne informacje",
            "1. Wynajem sali obejmuje udostępnienie przestrzeni na ustalony czas oraz wszelkie dodatkowe wybrane usługi (np. nagłośnienie, catering itp).",
            "2. Rezerwacja sali musi zostać potwierdzona minimum 7 dni przed datą wydarzenia, a pełna płatność musi zostać uiszczona przed rozpoczęciem wynajmu.",
            "3. Wynajmujący odpowiada za wszelkie szkody powstałe podczas użytkowania sali i zobowiązany jest do naprawy wszelkich usterek powstałych z jego winy.",
            "4. Wymagane jest przestrzeganie przepisów dotyczących ochrony przeciwpożarowej oraz zasad bezpieczeństwa obowiązujących w obiekcie.",
        };

        public sealed override List<string> Statute => new List<string>()
        {
            "Regulamin",
            "1. Wynajmujący zobowiązany jest do opuszczenia sali w stanie niepogorszonym, a wszelkie koszty związane z jej sprzątaniem po wydarzeniu mogą zostać nałożone na wynajmującego.",
            "2. Sala może być dostosowana w dowolny sposób od ułożenia miejsc po dodatkowe usługi dostępne dla danej sali. Zmiana wystroju sali musi być natomiast uprzednio uzgodniona z administracją obiektu.",
            "3. W przypadku odwołania rezerwacji później niż 48 godzin przed planowanym wynajmem, organizator zastrzega sobie prawo do pobrania opłaty w wysokości 50% wartości wynajmu.",
            "4. Organizator zastrzega sobie prawo do odwołania wynajmu sali z przyczyn niezależnych (np. awarie techniczne), zobowiązując się jednocześnie do pełnego zwrotu kosztów wynajmującemu.",
        };

    }
}
