using EventFlowAPI.DB.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EventFlowAPI.DB.Extensions.SeedingExtensions
{
    public static class PaymentTypeSeedExtensions
    {
        public static void Seed(this EntityTypeBuilder<PaymentType> entityBuilder)
        {
            entityBuilder.HasData(
                new PaymentType
                {
                    Id = 1,
                    Name = "Karta kredytowa",
                    PaymentTypeGuid = Guid.NewGuid(),
                },
                new PaymentType
                {
                    Id = 2,
                    Name = "Przelew",
                    PaymentTypeGuid = Guid.NewGuid(),
                },
                new PaymentType
                {
                    Id = 3,
                    Name = "BLIK",
                    PaymentTypeGuid = Guid.NewGuid(),
                },
                new PaymentType
                {
                    Id = 4,
                    Name = "Zapłać później",
                    PaymentTypeGuid = Guid.NewGuid(),
                },
                new PaymentType
                {
                    Id = 5,
                    Name = "Karnet",
                    PaymentTypeGuid = Guid.NewGuid(),
                }
            );
        }
    }
}
