using EventFlowAPI.DB.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EventFlowAPI.DB.Extensions.SeedingExtensions
{
    public static class Festival_SponsorSeedExtensions
    {
        public static void Seed(this EntityTypeBuilder<Festival_Sponsor> entityBuilder)
        {
            entityBuilder.HasData(
                new Festival_Sponsor
                {
                    FestivalId = 1,
                    SponsorId = 1
                },
                new Festival_Sponsor
                {
                    FestivalId = 1,
                    SponsorId = 2
                },
                new Festival_Sponsor
                {
                    FestivalId = 2,
                    SponsorId = 1
                },
                new Festival_Sponsor
                {
                    FestivalId = 2,
                    SponsorId = 3
                },
                new Festival_Sponsor
                {
                    FestivalId = 3,
                    SponsorId = 2
                },
                new Festival_Sponsor
                {
                    FestivalId = 3,
                    SponsorId = 3
                }
            );
        }
    }
}
