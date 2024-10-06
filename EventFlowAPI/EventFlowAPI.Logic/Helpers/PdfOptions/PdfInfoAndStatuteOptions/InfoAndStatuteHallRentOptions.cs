namespace EventFlowAPI.Logic.Helpers.PdfOptions.PdfInfoAndStatuteOptions
{
    public sealed class InfoAndStatuteHallRentOptions : InfoAndStatuteOptions
    {
        public sealed override List<string> Info => new List<string>()
        {
            "Ważne informacje",
            "1. Rezerwacja sali musi zostać potwierdzona min. 7 dni przed datą wydarzenia, a płatność musi zostać uiszczona przed rozpoczęciem wynajmu.",
            "2. Sala może być dostosowana w dowolny sposób od ułożenia miejsc po dodatkowe usługi.",
            "3. Wynajmujący odpowiada za wszelkie szkody powstałe podczas użytkowania sali i zobowiązany jest do pokrycia kosztów ich napraw.",        
        };

        public sealed override List<string> Statute => new List<string>()
        {
            "Regulamin",
            "1. Wynajmujący zobowiązany jest do zachowania czystości w sali, w przypadku nie wywiązania się z zobowiązania wynajmujący może zostać obciążony dodatkowymi kosztami.",
            "2. Wynajmujący zobowiązany jest do przestrzegania ustalonych przepisów ppoż. oraz zasad BHP.",
            "3. Organizator ma prawo do odwołania wynajmu sali z przyczyn niezależnych, zobowiązując się do pełnego zwrotu kosztów wynajmu.",
        };

    }
}
