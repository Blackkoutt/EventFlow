using EventFlowAPI.DB.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EventFlowAPI.DB.Extensions.SeedingExtensions
{
    public static class TicketJPGSeedExtentions
    {
        public static void Seed(this EntityTypeBuilder<TicketJPG> entityBuilder)
        {
            var res1 = new Guid("53538b58-f885-4f4a-b675-a4aa4063ccf3");
            var res2 = new Guid("ed8b9230-223b-4609-8d13-aa6017edad09");
            var res3 = new Guid("f9a076c4-3475-4a28-a60c-6e0e3c03731a");
            var res4 = new Guid("de1d6773-f027-4888-996a-0296e5c52708");
            var firstFestivalGuid = new Guid("0b74ec7b-933b-4163-afa5-e0997681dccd");
            var secondFestivalGuid = new Guid("806cade1-2685-43dc-8cfc-682fc4229db6");

            entityBuilder.HasData(
                new TicketJPG
                {
                    Id = 1,
                    FileName = $"eventflow_bilet_test_{res1}.jpg",
                    ReservationGuid = res1,
                },
                new TicketJPG
                {
                    Id = 2,
                    FileName = $"eventflow_bilet_test_{res2}.jpg",
                    ReservationGuid = res2,
                },
                new TicketJPG
                {
                    Id = 3,
                    FileName = $"eventflow_bilet_test_{res3}.jpg",
                    ReservationGuid = res3,
                },
                new TicketJPG
                {
                    Id = 4,
                    FileName = $"eventflow_bilet_test_{res4}.jpg",
                    ReservationGuid = res4,
                },
                new TicketJPG
                {
                    Id = 5,
                    FileName = $"eventflow_bilet_test_{firstFestivalGuid}_1.jpg",
                    ReservationGuid = firstFestivalGuid,
                },
                new TicketJPG
                {
                    Id = 6,
                    FileName = $"eventflow_bilet_test_{firstFestivalGuid}_2.jpg",
                    ReservationGuid = firstFestivalGuid,
                },
                new TicketJPG
                {
                    Id = 7,
                    FileName = $"eventflow_bilet_test_{secondFestivalGuid}_1.jpg",
                    ReservationGuid = secondFestivalGuid,
                },
                new TicketJPG
                {
                    Id = 8,
                    FileName = $"eventflow_bilet_test_{secondFestivalGuid}_2.jpg",
                    ReservationGuid = secondFestivalGuid,
                }
            );
        }
    }
}
