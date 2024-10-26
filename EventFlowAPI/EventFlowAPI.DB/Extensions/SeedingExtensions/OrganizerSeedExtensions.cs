using EventFlowAPI.DB.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EventFlowAPI.DB.Extensions.SeedingExtensions
{
    public static class OrganizerSeedExtensions
    {
        public static void Seed(this EntityTypeBuilder<Organizer> entityBuilder)
        {
            entityBuilder.HasData(
               new Organizer
               {
                   Id = 1,
                   Name = "EventFlow"
               },
               new Organizer
               {
                   Id = 2,
                   Name = "Snowflake"
               },
               new Organizer
               {
                   Id = 3,
                   Name = "Aura"
               }
            );
        }
    }
}
