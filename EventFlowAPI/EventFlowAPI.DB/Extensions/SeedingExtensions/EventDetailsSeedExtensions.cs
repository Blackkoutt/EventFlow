using EventFlowAPI.DB.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EventFlowAPI.DB.Extensions.SeedingExtensions
{
    public static class EventDetailsSeedExtensions
    {
        public static void Seed(this EntityTypeBuilder<EventDetails> entityBuilder)
        {
            entityBuilder.HasData(
                new EventDetails
                {
                    Id = 1,
                    LongDescription = "Krótki opis wydarzenia Koncert: Mystic Waves"
                },
                new EventDetails
                {
                    Id = 2,
                    LongDescription = "Krótki opis wydarzenia Spektakl: Cień Przeszłość"
                },
                new EventDetails
                {
                    Id = 3,
                    LongDescription = "Krótki opis wydarzenia Film: Królestwo planety małp"
                },
                new EventDetails
                {
                    Id = 4,
                    LongDescription = "Krótki opis wydarzenia Wystawa: Nowe inspiracje"
                },
                new EventDetails
                {
                    Id = 5,
                    LongDescription = "Krótki opis wydarzenia Koncert: New Era"
                },
                new EventDetails
                {
                    Id = 6,
                    LongDescription = "Krótki opis wydarzenia Film: Gladiator"
                },
                new EventDetails
                {
                    Id = 7,
                    LongDescription = "Krótki opis wydarzenia Wystawa: Nowa sztuka"
                }
            );
        }
    }
}
