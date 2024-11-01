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
                   Name = "EventFlow", 
                   OrganizerGuid = Guid.NewGuid(),
               },
               new Organizer
               {
                   Id = 2,
                   Name = "Snowflake",
                   OrganizerGuid = Guid.NewGuid(),
               },
               new Organizer
               {
                   Id = 3,
                   Name = "Aura",
                   OrganizerGuid = Guid.NewGuid(),
               }
            );
        }
    }
}
