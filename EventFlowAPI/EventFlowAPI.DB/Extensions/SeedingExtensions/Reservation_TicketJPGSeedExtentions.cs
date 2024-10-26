using EventFlowAPI.DB.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EventFlowAPI.DB.Extensions.SeedingExtensions
{
    public static class Reservation_TicketJPGSeedExtentions
    {
        public static void Seed(this EntityTypeBuilder<Reservation_TicketJPG> entityBuilder)
        {
            entityBuilder.HasData(
                new Reservation_TicketJPG
                {
                    ReservationId = 1,
                    TicketJPGId = 1,
                },
                new Reservation_TicketJPG
                {
                    ReservationId = 2,
                    TicketJPGId = 2,
                },
                new Reservation_TicketJPG
                {
                    ReservationId = 3,
                    TicketJPGId = 3,
                },
                new Reservation_TicketJPG
                {
                    ReservationId = 4,
                    TicketJPGId = 4,
                },
                new Reservation_TicketJPG
                {
                    ReservationId = 5,
                    TicketJPGId = 5,
                },
                new Reservation_TicketJPG
                {
                    ReservationId = 5,
                    TicketJPGId = 6,
                },
                new Reservation_TicketJPG
                {
                    ReservationId = 6,
                    TicketJPGId = 5,
                },
                new Reservation_TicketJPG
                {
                    ReservationId = 6,
                    TicketJPGId = 6,
                },
                new Reservation_TicketJPG
                {
                    ReservationId = 7,
                    TicketJPGId = 7,
                },
                new Reservation_TicketJPG
                {
                    ReservationId = 7,
                    TicketJPGId = 8,
                },
                new Reservation_TicketJPG
                {
                    ReservationId = 8,
                    TicketJPGId = 7,
                },
                new Reservation_TicketJPG
                {
                    ReservationId = 8,
                    TicketJPGId = 8,
                }
            );
        }
    }
}
