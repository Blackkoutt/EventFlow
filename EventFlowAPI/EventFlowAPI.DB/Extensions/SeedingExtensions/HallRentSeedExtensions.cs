using EventFlowAPI.DB.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EventFlowAPI.DB.Extensions.SeedingExtensions
{
    public static class HallRentSeedExtensions
    {
        public static void Seed(this EntityTypeBuilder<HallRent> entityBuilder, DateTime today)
        {
            var hrGuid1 = Guid.NewGuid();
            var hrGuid2 = Guid.NewGuid();
            var hrGuid3 = Guid.NewGuid();
            var hrGuid4 = Guid.NewGuid();

            entityBuilder.HasData(
                new HallRent
                {
                    Id = 1,
                    HallRentGuid = hrGuid1,
                    StartDate = today.AddMonths(1).AddDays(1),
                    EndDate = today.AddMonths(1).AddDays(1).AddHours(8),
                    DurationTimeSpan = today.AddMonths(1).AddDays(1).AddHours(8) - today.AddMonths(1).AddDays(1),
                    RentDate = today,
                    PaymentDate = today,
                    PaymentAmount = 899.99m,
                    PaymentTypeId = 1,
                    HallId = 12,
                    UserId = "4"
                },
                new HallRent
                {
                    Id = 2,
                    HallRentGuid = hrGuid2,
                    StartDate = today.AddMonths(1).AddDays(2),
                    EndDate = today.AddMonths(1).AddDays(2).AddHours(4),
                    DurationTimeSpan = today.AddMonths(1).AddDays(2).AddHours(4) - today.AddMonths(1).AddDays(2),
                    RentDate = today,
                    PaymentDate = today,
                    PaymentAmount = 699.99m,
                    PaymentTypeId = 2,
                    HallId = 13,
                    UserId = "3"
                },
                new HallRent
                {
                    Id = 3,
                    HallRentGuid = hrGuid3,
                    StartDate = today.AddMonths(1).AddDays(3),
                    EndDate = today.AddMonths(1).AddDays(3).AddHours(2),
                    DurationTimeSpan = today.AddMonths(1).AddDays(3).AddHours(2) - today.AddMonths(1).AddDays(3),
                    RentDate = today,
                    PaymentDate = today,
                    PaymentAmount = 399.99m,
                    PaymentTypeId = 3,
                    HallId = 14,
                    UserId = "3"
                },
                new HallRent
                {
                    Id = 4,
                    HallRentGuid = hrGuid4,
                    StartDate = today.AddMonths(1).AddDays(4),
                    EndDate = today.AddMonths(1).AddDays(4).AddHours(1),
                    DurationTimeSpan = today.AddMonths(1).AddDays(4).AddHours(1) - today.AddMonths(1).AddDays(4),
                    RentDate = today,
                    PaymentDate = today,
                    PaymentAmount = 150.99m,
                    PaymentTypeId = 2,
                    HallId = 15,
                    UserId = "2"
                }
            );
        }
    }
}
