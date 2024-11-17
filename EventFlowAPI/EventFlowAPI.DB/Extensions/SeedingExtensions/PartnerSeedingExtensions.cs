using EventFlowAPI.DB.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EventFlowAPI.DB.Extensions.SeedingExtensions
{
    public static class PartnerSeedingExtensions
    {
        public static void Seed(this EntityTypeBuilder<Partner> entityBuilder)
        {
            entityBuilder.HasData(
                new Partner
                {
                    Id = 1,
                    Name = "Basel",
                    PartnerGuid = Guid.NewGuid(),
                    PhotoName = "basel.png"
                },
                new Partner
                {
                    Id = 2,
                    Name = "Aura",
                    PartnerGuid = Guid.NewGuid(),
                    PhotoName = "aura.png"
                },
                 new Partner
                 {
                     Id = 3,
                     Name = "Vision",
                     PartnerGuid = Guid.NewGuid(),
                     PhotoName = "vision.png"
                 },
                 new Partner
                 {
                     Id = 4,
                     Name = "Snowflake",
                     PartnerGuid = Guid.NewGuid(),
                     PhotoName = "snowflake.png"
                 },
                 new Partner
                 {
                     Id = 5,
                     Name = "Waveless",
                     PartnerGuid = Guid.NewGuid(),
                     PhotoName = "waveless.png"
                 }
            );
        }
    }
}
