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
                   PhotoName = "eventflow.png",
               },
               new Organizer
               {
                   Id = 2,
                   Name = "Snowflake",
                   OrganizerGuid = Guid.NewGuid(),
                   PhotoName = "snowflake.png",
               },
               new Organizer
               {
                   Id = 3,
                   Name = "Aura",
                   OrganizerGuid = Guid.NewGuid(),
                   PhotoName = "aura.png",
               }
            );
        }
    }
}
