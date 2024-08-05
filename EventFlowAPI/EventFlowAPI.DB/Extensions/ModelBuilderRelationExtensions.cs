using EventFlowAPI.DB.Entities;
using Microsoft.EntityFrameworkCore;

namespace EventFlowAPI.DB.Extensions
{
    public static class ModelBuilderRelationExtensions
    {
        public static void AddOneToOneRelations(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
               .HasOne(x => x.UserData)
               .WithOne(x => x.User)
               .HasForeignKey<User>(x => x.Id);

            modelBuilder.Entity<Event>()
                .HasOne(x => x.Details)
                .WithOne(x => x.Event)
                .HasForeignKey<Event>(x => x.Id);

            modelBuilder.Entity<Festival>()
                .HasOne(x => x.Details)
                .WithOne(x => x.Festival)
                .HasForeignKey<Festival>(x => x.Id);
        }
        public static void AddManyToManyRelations(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Festival_Organizer>()
                .HasOne(x => x.Festival)
                .WithMany(x => x.Organizers)
                .HasForeignKey(x => x.FestivalId);
            modelBuilder.Entity<Festival_Organizer>()
                .HasOne(x => x.Organizer)
                .WithMany(x => x.Festivals)
                .HasForeignKey(x => x.OrganizerId);

            modelBuilder.Entity<Festival_Sponsor>()
                .HasOne(x => x.Festival)
                .WithMany(x => x.Sponsors)
                .HasForeignKey(x => x.FestivalId);
            modelBuilder.Entity<Festival_Sponsor>()
                .HasOne(x => x.Sponsor)
                .WithMany(x => x.Festivals)
                .HasForeignKey(x => x.SponsorId);

            modelBuilder.Entity<Festival_MediaPatron>()
                .HasOne(x => x.Festival)
                .WithMany(x => x.MediaPatrons)
                .HasForeignKey(x => x.FestivalId);
            modelBuilder.Entity<Festival_MediaPatron>()
                .HasOne(x => x.MediaPatron)
                .WithMany(x => x.Festivals)
                .HasForeignKey(x => x.MediaPatronId);

            modelBuilder.Entity<Festival_Event>()
                .HasOne(x => x.Festival)
                .WithMany(x => x.Events)
                .HasForeignKey(x => x.FestivalId);
            modelBuilder.Entity<Festival_Event>()
                .HasOne(x => x.Event)
                .WithMany(x => x.Festivals)
                .HasForeignKey(x => x.EventId);

            modelBuilder.Entity<Reservation_Seat>()
                .HasOne(rs => rs.Reservation)
                .WithMany(s => s.Seats)
                .HasForeignKey(rs => rs.ReservationId)
                .OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<Reservation_Seat>()
                .HasOne(rs => rs.Seat)
                .WithMany(r => r.Reservations)
                .HasForeignKey(rs => rs.SeatId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<HallType_Equipment>()
                .HasOne(x => x.HallType)
                .WithMany(x => x.Equipments)
                .HasForeignKey(x => x.HallTypeId);
            modelBuilder.Entity<HallType_Equipment>()
                .HasOne(x => x.Equipment)
                .WithMany(x => x.HallTypes)
                .HasForeignKey(x => x.EquipmentId);

            modelBuilder.Entity<HallRent_AdditionalServices>()
                .HasOne(x => x.HallRent)
                .WithMany(x => x.AdditionalServices)
                .HasForeignKey(x => x.HallRentId);
            modelBuilder.Entity<HallRent_AdditionalServices>()
                .HasOne(x => x.AdditionalService)
                .WithMany(x => x.Rents)
                .HasForeignKey(x => x.AdditionalServiceId);
        }
    }
}
