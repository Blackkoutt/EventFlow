using EventFlowAPI.DB.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EventFlowAPI.DB.Extensions.SeedingExtensions
{
    public static class FestivalDetailsSeedExtentions
    {
        public static void Seed(this EntityTypeBuilder<FestivalDetails> entityBuilder)
        {
            entityBuilder.HasData(
                new FestivalDetails
                {
                    Id = 1,
                    LongDescription = "Opis festiwalu muzyki współczesnej",
                },
                new FestivalDetails
                {
                    Id = 2,
                    LongDescription = "Opis festiwalu filmowego",
                },
                new FestivalDetails
                {
                    Id = 3,
                    LongDescription = "Opis festiwalu sztuki abstrakcyjnej",
                }
            );
        }
    }
}
