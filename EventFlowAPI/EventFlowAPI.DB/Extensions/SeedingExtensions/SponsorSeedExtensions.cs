using EventFlowAPI.DB.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EventFlowAPI.DB.Extensions.SeedingExtensions
{
    public static class SponsorSeedExtensions
    {
        public static void Seed(this EntityTypeBuilder<Sponsor> entityBuilder)
        {
            entityBuilder.HasData(
                new Sponsor
                {
                    Id = 1,
                    Name = "Basel",
                    SponsorGuid = Guid.NewGuid(),     
                    PhotoName = "basel.png"
                },
                new Sponsor
                {
                    Id = 2,
                    Name = "Vision",
                    SponsorGuid = Guid.NewGuid(),
                    PhotoName = "vision.png"
                },
                new Sponsor
                {
                    Id = 3,
                    Name = "Waveless",
                    SponsorGuid = Guid.NewGuid(),
                    PhotoName = "waveless.png"
                }
            );
        }
    }
}
