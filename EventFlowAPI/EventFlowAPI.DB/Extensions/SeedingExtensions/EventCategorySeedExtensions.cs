using EventFlowAPI.DB.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EventFlowAPI.DB.Extensions.SeedingExtensions
{
    public static class EventCategorySeedExtensions
    {
        public static void Seed(this EntityTypeBuilder<EventCategory> entityBuilder)
        {
            entityBuilder.HasData(
                new EventCategory
                {
                    Id = 1,
                    Name = "Koncert"
                },
                new EventCategory
                {
                    Id = 2,
                    Name = "Film"
                },
                new EventCategory
                {
                    Id = 3,
                    Name = "Spektakl"
                },
                new EventCategory
                {
                    Id = 4,
                    Name = "Wystawa"
                }
            );
        }
    }
}
