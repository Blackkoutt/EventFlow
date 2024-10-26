using EventFlowAPI.DB.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EventFlowAPI.DB.Extensions.SeedingExtensions
{
    public static class EventSeedExtensions
    {
        public static void Seed(this EntityTypeBuilder<Event> entityBuilder, DateTime today)
        {
            entityBuilder.HasData(
                new Event
                {
                    Id = 1,
                    Name = "Koncert Mystic Waves",
                    ShortDescription = "Któtki opis koncertu Mystic Waves.",
                    StartDate = today.AddMonths(1).AddDays(1),
                    EndDate = today.AddMonths(1).AddDays(1).AddHours(1),
                    DurationTimeSpan = today.AddMonths(1).AddDays(1) - today.AddMonths(1).AddDays(1).AddHours(1),
                    CategoryId = 1,
                    HallId = 6,
                },
                new Event
                {
                    Id = 2,
                    Name = "Cień Przeszłości",
                    ShortDescription = "Krótki opis spektaklu pt. Cień Przeszłości.",
                    StartDate = today.AddMonths(1).AddDays(2),
                    EndDate = today.AddMonths(1).AddDays(2).AddHours(3),
                    DurationTimeSpan = today.AddMonths(1).AddDays(2) - today.AddMonths(1).AddDays(2).AddHours(3),
                    CategoryId = 3,
                    HallId = 7,
                },
                new Event
                {
                    Id = 3,
                    Name = "Królestwo planety małp",
                    ShortDescription = "Nowy film Królestwo planety małp już w kinach!.",
                    StartDate = today.AddMonths(1).AddDays(3),
                    EndDate = today.AddMonths(1).AddDays(3).AddHours(2),
                    DurationTimeSpan = today.AddMonths(1).AddDays(3) - today.AddMonths(1).AddDays(3).AddHours(2),
                    CategoryId = 2,
                    HallId = 5,
                },
                new Event
                {
                    Id = 4,
                    Name = "Nowe inspiracje",
                    ShortDescription = "Nowe inspiracje to nowoczesna wystawa sztuki.",
                    StartDate = today.AddMonths(1).AddDays(4),
                    EndDate = today.AddMonths(1).AddDays(4).AddHours(3),
                    DurationTimeSpan = today.AddMonths(1).AddDays(4) - today.AddMonths(1).AddDays(4).AddHours(3),
                    CategoryId = 4,
                    HallId = 8,
                },
                new Event
                {
                    Id = 5,
                    Name = "Koncert: New Era",
                    ShortDescription = "Jedyna taka okazja na usłyszenie New Era na żywo.",
                    StartDate = today.AddMonths(2).AddDays(1),
                    EndDate = today.AddMonths(2).AddDays(1).AddHours(1),
                    DurationTimeSpan = today.AddMonths(2).AddDays(1) - today.AddMonths(2).AddDays(1).AddHours(1),
                    CategoryId = 1,
                    HallId = 10,
                },
                new Event
                {
                    Id = 6,
                    Name = "Gladiator",
                    ShortDescription = "Nowy film Gladiator już w kinach!.",
                    StartDate = today.AddMonths(2).AddDays(3),
                    EndDate = today.AddMonths(2).AddDays(3).AddHours(2),
                    DurationTimeSpan = today.AddMonths(2).AddDays(3) - today.AddMonths(2).AddDays(3).AddHours(2),
                    CategoryId = 2,
                    HallId = 9,
                },
                new Event
                {
                    Id = 7,
                    Name = "Nowa sztuka",
                    ShortDescription = "Nowe sztuka to nowoczesna wystawa sztuki.",
                    StartDate = today.AddMonths(2).AddDays(4),
                    EndDate = today.AddMonths(2).AddDays(4).AddHours(3),
                    DurationTimeSpan = today.AddMonths(2).AddDays(4) - today.AddMonths(2).AddDays(4).AddHours(3),
                    CategoryId = 4,
                    HallId = 11,
                }
            );
        }
    }
}
