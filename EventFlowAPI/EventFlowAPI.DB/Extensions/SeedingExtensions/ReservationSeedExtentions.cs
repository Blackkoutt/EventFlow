using EventFlowAPI.DB.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EventFlowAPI.DB.Extensions.SeedingExtensions
{
    public static class ReservationSeedExtentions
    {
        public static void Seed(this EntityTypeBuilder<Reservation> entityBuilder, DateTime today)
        {
            var res1 = new Guid("53538b58-f885-4f4a-b675-a4aa4063ccf3");
            var res2 = new Guid("ed8b9230-223b-4609-8d13-aa6017edad09");
            var res3 = new Guid("f9a076c4-3475-4a28-a60c-6e0e3c03731a");
            var res4 = new Guid("de1d6773-f027-4888-996a-0296e5c52708");
            var firstFestivalGuid = new Guid("0b74ec7b-933b-4163-afa5-e0997681dccd");
            var secondFestivalGuid = new Guid("806cade1-2685-43dc-8cfc-682fc4229db6");

            entityBuilder.HasData(
                new Reservation
                {
                    Id = 1,
                    ReservationGuid = res1,
                    IsFestivalReservation = false,
                    ReservationDate = today,
                    StartDate = today.AddMonths(1).AddDays(1),
                    EndDate = today.AddMonths(1).AddDays(1).AddHours(1),
                    PaymentDate = today,
                    TotalAddtionalPaymentPercentage = 10m,
                    TotalAdditionalPaymentAmount = 2.5m,
                    PaymentAmount = 24.99m,
                    UserId = "4",
                    PaymentTypeId = 1,
                    TicketId = 1,
                    TicketPDFId = 1
                },
                new Reservation
                {
                    Id = 2,
                    ReservationGuid = res2,
                    IsFestivalReservation = false,
                    ReservationDate = today,
                    StartDate = today.AddMonths(1).AddDays(2),
                    EndDate = today.AddMonths(1).AddDays(2).AddHours(3),
                    PaymentDate = today,
                    TotalAddtionalPaymentPercentage = 0m,
                    TotalAdditionalPaymentAmount = 0m,
                    PaymentAmount = 34.99m,
                    UserId = "2",
                    PaymentTypeId = 1,
                    TicketId = 2,
                    TicketPDFId = 2
                },
                new Reservation
                {
                    Id = 3,
                    ReservationGuid = res3,
                    IsFestivalReservation = false,
                    ReservationDate = today,
                    StartDate = today.AddMonths(1).AddDays(3),
                    EndDate = today.AddMonths(1).AddDays(3).AddHours(2),
                    PaymentDate = today,
                    TotalAddtionalPaymentPercentage = 25m,
                    TotalAdditionalPaymentAmount = 7.5m,
                    PaymentAmount = 29.99m,
                    UserId = "3",
                    PaymentTypeId = 1,
                    TicketId = 3,
                    TicketPDFId = 3
                },
                new Reservation
                {
                    Id = 4,
                    ReservationGuid = res4,
                    IsFestivalReservation = false,
                    ReservationDate = today,
                    StartDate = today.AddMonths(1).AddDays(4),
                    EndDate = today.AddMonths(1).AddDays(4).AddHours(3),
                    PaymentDate = today,
                    TotalAddtionalPaymentPercentage = 0m,
                    TotalAdditionalPaymentAmount = 0m,
                    PaymentAmount = 19.99m,
                    UserId = "3",
                    PaymentTypeId = 1,
                    TicketId = 4,
                    TicketPDFId = 4
                },
                new Reservation
                {
                    Id = 5,
                    ReservationGuid = firstFestivalGuid,
                    IsFestivalReservation = true,
                    ReservationDate = today,
                    StartDate = today.AddMonths(1).AddDays(1),
                    EndDate = today.AddMonths(1).AddDays(1).AddHours(1),
                    PaymentDate = today,
                    TotalAddtionalPaymentPercentage = 10m,
                    TotalAdditionalPaymentAmount = 2m,
                    PaymentAmount = 19.99m,
                    UserId = "2",
                    PaymentTypeId = 1,
                    TicketId = 5,
                    TicketPDFId = 5
                },
                new Reservation
                {
                    Id = 6,
                    ReservationGuid = firstFestivalGuid,
                    IsFestivalReservation = true,
                    ReservationDate = today,
                    StartDate = today.AddMonths(2).AddDays(1),
                    EndDate = today.AddMonths(2).AddDays(1).AddHours(1),
                    PaymentDate = today,
                    TotalAddtionalPaymentPercentage = 10m,
                    TotalAdditionalPaymentAmount = 2m,
                    PaymentAmount = 19.99m,
                    UserId = "2",
                    PaymentTypeId = 1,
                    TicketId = 6,
                    TicketPDFId = 5
                },
                new Reservation
                {
                    Id = 7,
                    ReservationGuid = secondFestivalGuid,
                    IsFestivalReservation = true,
                    ReservationDate = today,
                    StartDate = today.AddMonths(1).AddDays(2),
                    EndDate = today.AddMonths(1).AddDays(2).AddHours(3),
                    PaymentDate = today,
                    TotalAddtionalPaymentPercentage = 0m,
                    TotalAdditionalPaymentAmount = 0m,
                    PaymentAmount = 29.99m,
                    UserId = "2",
                    PaymentTypeId = 1,
                    TicketId = 7,
                    TicketPDFId = 6
                },
                new Reservation
                {
                    Id = 8,
                    ReservationGuid = secondFestivalGuid,
                    IsFestivalReservation = true,
                    ReservationDate = today,
                    StartDate = today.AddMonths(2).AddDays(3),
                    EndDate = today.AddMonths(2).AddDays(3).AddHours(2),
                    PaymentDate = today,
                    TotalAddtionalPaymentPercentage = 0m,
                    TotalAdditionalPaymentAmount = 0m,
                    PaymentAmount = 29.99m,
                    UserId = "2",
                    PaymentTypeId = 1,
                    TicketId = 8,
                    TicketPDFId = 6
                }
            );
        }
    }
}
