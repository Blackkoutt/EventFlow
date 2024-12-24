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
                    Name = "PayU",
                    PaymentTypeGuid = Guid.NewGuid(),
                },
                new PaymentType
                {
                    Id = 2,
                    Name = "Karnet",
                    PaymentTypeGuid = Guid.NewGuid(),
                }
            );
        }
    }
}
