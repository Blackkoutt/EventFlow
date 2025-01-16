using EventFlowAPI.DB.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EventFlowAPI.DB.Extensions.SeedingExtensions
{
    public static class HallTypeSeedExtensions
    {
        public static void Seed(this EntityTypeBuilder<HallType> entityBuilder)
        {
            entityBuilder.HasData(
                new HallType
                {
                    Id = 1,
                    Name = "Sala filmowa",
                    Description = "Nowa sala kinowa wyposażona w nowoczesne nagłośnienie i ekran",
                    PhotoName = "filmowa.jpg"
                },

                new HallType
                {
                    Id = 2,
                    Name = "Sala koncertowa",
                    Description = "Nowa sala koncertowa wyposażona w najwyższej klasy nagłośnienie",
                    PhotoName = "koncertowa.jpg"
                },

                new HallType
                {
                    Id = 3,
                    Name = "Sala teatralna",
                    Description = "Opis sali teatralnej",
                    PhotoName = "teatralna.jpg"
                },
                new HallType
                {
                    Id = 4,
                    Name = "Sala wystawowa",
                    Description = "Opis sali wystawowa",
                    PhotoName = "wystawowa.jpg"
                },
                new HallType
                {
                    Id = 5,
                    Name = "Sala ogólna",
                    Description = "Sala ogólna",
                    PhotoName = "ogolna.jpg"
                }
            );
        }
    }
}
