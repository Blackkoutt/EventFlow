using EventFlowAPI.DB.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EventFlowAPI.DB.Extensions.SeedingExtensions
{
    public static class Festival_MediaPatronSeedExtentions
    {
        public static void Seed(this EntityTypeBuilder<Festival_MediaPatron> entityBuilder)
        {
            entityBuilder.HasData(
                new Festival_MediaPatron
                {
                    FestivalId = 1,
                    MediaPatronId = 1
                },
                new Festival_MediaPatron
                {
                    FestivalId = 1,
                    MediaPatronId = 2
                },
                new Festival_MediaPatron
                {
                    FestivalId = 2,
                    MediaPatronId = 1
                },
                new Festival_MediaPatron
                {
                    FestivalId = 2,
                    MediaPatronId = 3
                },
                new Festival_MediaPatron
                {
                    FestivalId = 3,
                    MediaPatronId = 2
                },
                new Festival_MediaPatron
                {
                    FestivalId = 3,
                    MediaPatronId = 3
                }
            );
        }
    }
}
