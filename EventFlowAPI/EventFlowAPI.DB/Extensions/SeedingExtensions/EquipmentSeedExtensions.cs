using EventFlowAPI.DB.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EventFlowAPI.DB.Extensions.SeedingExtensions
{
    public static class EquipmentSeedExtensions
    {
        public static void Seed(this EntityTypeBuilder<Equipment> entityBuilder)
        {
            entityBuilder.HasData(
               new Equipment
               {
                   Id = 1,
                   Name = "Projektor multimedialny",
                   Description = "Nowoczesny projektor"
               },
               new Equipment
               {
                   Id = 2,
                   Name = "Oświetlenie",
                   Description = "Wysokiej klasy oświetlenie"
               },
               new Equipment
               {
                   Id = 3,
                   Name = "Nagłośnienie kinowe",
                   Description = "Głośniki przeznaczone do odtwrzania filmów"
               },
               new Equipment
               {
                   Id = 4,
                   Name = "Nagłośnienie koncertowe",
                   Description = "Głośniki przeznaczone do koncertów"
               }
           );
        }
    }
}
