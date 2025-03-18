using EventFlowAPI.DB.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EventFlowAPI.DB.Extensions.SeedingExtensions
{
    public static class FestivalSeedExtentions
    {
        public static void Seed(this EntityTypeBuilder<Festival> entityBuilder, DateTime today)
        {
            entityBuilder.HasData(
                new Festival
                {
                    Id = 1,
                    Name = "Festiwal Muzyki Hip-Hop",
                    ShortDescription = "Największy festiwal hip-hopowy! Koncerty, freestyle battle, warsztaty DJ-skie i spotkania z artystami.",
                    StartDate = today.AddMonths(1).AddDays(1),
                    EndDate = today.AddMonths(2).AddDays(1).AddHours(1),
                    DurationTimeSpan = today.AddMonths(1) - today.AddMonths(2).AddDays(1).AddHours(1),
                    FestivalGuid = Guid.NewGuid(),
                    PhotoName = "festiwal_muzyki_hip_hop.png"
                },
                new Festival
                {
                    Id = 2,
                    Name = "Festiwal Filmowy",
                    ShortDescription = "Przegląd najlepszych filmów z całego świata! Premiera, spotkania z reżyserami i nocne maratony kinowe.",
                    StartDate = today.AddMonths(1).AddDays(2),
                    EndDate = today.AddMonths(2).AddDays(3).AddHours(2),
                    DurationTimeSpan = today.AddMonths(1).AddDays(2) - today.AddMonths(2).AddDays(3).AddHours(2),
                    FestivalGuid = Guid.NewGuid(),
                    PhotoName = "festiwal_filmowy.png"
                },

                new Festival
                {
                    Id = 3,
                    Name = "Festiwal Sztuki",
                    ShortDescription = "Wystawy, instalacje i performanse! Odkryj nowoczesne formy sztuki i weź udział w kreatywnych warsztatach.",
                    StartDate = today.AddMonths(1).AddDays(4),
                    EndDate = today.AddMonths(2).AddDays(4).AddHours(3),
                    DurationTimeSpan = today.AddMonths(1).AddDays(4) - today.AddMonths(2).AddDays(4).AddHours(3),
                    FestivalGuid = Guid.NewGuid(),
                    PhotoName = "festiwal_sztuki.png"
                }
            );
        }
    }
}
