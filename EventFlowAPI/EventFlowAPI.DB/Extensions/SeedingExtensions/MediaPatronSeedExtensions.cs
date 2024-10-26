using EventFlowAPI.DB.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EventFlowAPI.DB.Extensions.SeedingExtensions
{
    public static class MediaPatronSeedExtensions
    {
        public static void Seed(this EntityTypeBuilder<MediaPatron> entityBuilder)
        {
            entityBuilder.HasData(
               new MediaPatron
               {
                   Id = 1,
                   Name = "Gazeta Nowoczesna"
               },
               new MediaPatron
               {
                   Id = 2,
                   Name = "Nowy świat TV"
               },
               new MediaPatron
               {
                   Id = 3,
                   Name = "Tygodnik Nowiny"
               }
            );
        }
    }
}
