﻿using EventFlowAPI.DB.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EventFlowAPI.DB.Extensions.SeedingExtensions
{
    public static class FAQSeedExtensions
    {
        public static void Seed(this EntityTypeBuilder<FAQ> entityBuilder)
        {
            entityBuilder.HasData(
                new FAQ
                {
                    Id = 1,
                    Question = "W jaki sposób mogę kupić bilet na wydarzenie ?",
                    Answer = "Bilety możesz kupić przechodząc do zakładki Wydarzenia. Następnie wybierz interesujące cię wydarzenie i kliknij przycisk Kup bilet. Wyświetli okno dialogowe w którym możliwy jest wybór miejsca i typu biletu. Wybierz miejsca i typ biletu, a następnie kliknij ponownie Kup bilet. Zostaniesz przekierowany do bramki płatniczej za pomocą której dokonasz opłaty za bilet. Po dokonaniu opłaty bilet zostanie przesłany na adres e-mail oraz będzie możliwy do pobrania w zakładce Profil > Moje rezerwacje."
                },
                new FAQ
                {
                    Id = 2,
                    Question = "Jak mogę wynająć salę ?",
                    Answer = "Aby wynająć salę przejdź do zakładki Wynajem. Następnie wybierz interesującą cię sale i kliknij przycisk Wynajmij. Pojawi się okno dialogowe z kalendarzem możliwych do wybrania dat rezerwacji. Wybierz datę rozpoczęcia i zakończenia rezerwacji oraz ewentualne dodatkowe usługi po czym kliknij przycisk Wynajmij. Zostaniesz przekierowany do bramki płątniczej za pomocą której dokonasz opłaty za wynajem. Po dokonaniu opłaty potwierdzenie wynajmu zostanie wysłane na adres e-mail oraz będzie możliwe do pobrania w zakładce Profil > Moje wynajmy."
                },
                new FAQ
                {
                    Id = 3,
                    Question = "Co ile czasu muszę przedłużać karnet ?",
                    Answer = "Karnet możesz przedłużyć w dowolnym momencie przed upływem jego daty ważności. Przy przedłużeniu karnetu zawsze otrzymasz zniżkę w zależności od aktualnie posiadanego karnetu. Przykładowo jeśli posiadasz karnet roczny otrzymasz 20% zniżki na każdy typ karnetu."
                },
                new FAQ
                {
                    Id = 4,
                    Question = "Jak zwrócić zakupiony bilet ?",
                    Answer = "Aby zwrócić zakupiony bilet konieczne jest anulowanie rezerwacji. Możesz tego dokonać przechodząc do zakładki Profil > Moje rezerwacje. Następnie z listy wybierz rezerwację którą chcesz anulowac i kliknij przycisk Anuluj. Pojawi się okno dialogowe z zapytaniem czy naewno chcesz wykonać daną operację. Po potwierdzeniu wykonania operacji otrzymasz na maila informację o anulowaniu biletu, a zwrot kosztów otrzymasz w ciągu 7 dni robocznych. Po anulowaniu rezerwacji posiadany bilet staje się nieważny.s"
                }
            );
        }
    }
}
