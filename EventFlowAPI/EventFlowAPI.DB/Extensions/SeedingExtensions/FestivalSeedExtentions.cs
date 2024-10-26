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
                    Name = "Festiwal muzyki współczesnej",
                    ShortDescription = "Festiwal muzyki współczesnej to nowy festiwal organizowany przez XYZ.",
                    StartDate = today.AddMonths(1).AddDays(1),
                    EndDate = today.AddMonths(2).AddDays(1).AddHours(1),
                    DurationTimeSpan = today.AddMonths(1) - today.AddMonths(2).AddDays(1).AddHours(1)
                },
                new Festival
                {
                    Id = 2,
                    Name = "Festiwal filmowy",
                    ShortDescription = "Festiwal filmowy to festiwal na którym można obejrzeć filmy.",
                    StartDate = today.AddMonths(1).AddDays(2),
                    EndDate = today.AddMonths(2).AddDays(3).AddHours(2),
                    DurationTimeSpan = today.AddMonths(1).AddDays(2) - today.AddMonths(2).AddDays(3).AddHours(2)
                },

                new Festival
                {
                    Id = 3,
                    Name = "Festiwal sztuki abstrakcyjnej",
                    ShortDescription = "Festiwal sztuki abstrakcyjnej to festiwal na którym można zobaczyć sztukę.",
                    StartDate = today.AddMonths(1).AddDays(4),
                    EndDate = today.AddMonths(2).AddDays(4).AddHours(3),
                    DurationTimeSpan = today.AddMonths(1).AddDays(4) - today.AddMonths(2).AddDays(4).AddHours(3)
                }
            );
        }
    }
}
