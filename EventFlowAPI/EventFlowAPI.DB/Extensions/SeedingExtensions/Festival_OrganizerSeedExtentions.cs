using EventFlowAPI.DB.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EventFlowAPI.DB.Extensions.SeedingExtensions
{
    public static class Festival_OrganizerSeedExtentions
    {
        public static void Seed(this EntityTypeBuilder<Festival_Organizer> entityBuilder)
        {
            entityBuilder.HasData(
                new Festival_Organizer
                {
                    FestivalId = 1,
                    OrganizerId = 1
                },
                new Festival_Organizer
                {
                    FestivalId = 1,
                    OrganizerId = 2
                },
                new Festival_Organizer
                {
                    FestivalId = 2,
                    OrganizerId = 1
                },
                new Festival_Organizer
                {
                    FestivalId = 2,
                    OrganizerId = 3
                },
                new Festival_Organizer
                {
                    FestivalId = 3,
                    OrganizerId = 2
                },
                new Festival_Organizer
                {
                    FestivalId = 3,
                    OrganizerId = 3
                }
            );
        }
    }
}
