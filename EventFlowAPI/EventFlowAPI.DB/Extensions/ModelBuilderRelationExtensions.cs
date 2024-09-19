﻿using EventFlowAPI.DB.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace EventFlowAPI.DB.Extensions
{
    public static class ModelBuilderRelationExtensions
    {
        public static void AddOneToOneRelations(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserData>()
               .HasOne(x => x.User)
               .WithOne(x => x.UserData)
               .HasForeignKey<UserData>(x => x.Id);

            modelBuilder.Entity<HallDetails>()
               .HasOne(x => x.Hall)
               .WithOne(x => x.HallDetails)
               .HasForeignKey<HallDetails>(x => x.Id);

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
            modelBuilder.Entity<User>()
             .HasMany(e => e.Roles)
             .WithMany(e => e.Users)
             .UsingEntity<IdentityUserRole<string>>();

            modelBuilder.Entity<Festival>()
               .HasMany(e => e.Organizers)
               .WithMany(e => e.Festivals)
               .UsingEntity<Festival_Organizer>();

            modelBuilder.Entity<Festival>()
               .HasMany(e => e.Sponsors)
               .WithMany(e => e.Festivals)
               .UsingEntity<Festival_Sponsor>();

            modelBuilder.Entity<Festival>()
                .HasMany(e => e.MediaPatrons)
                .WithMany(e => e.Festivals)
                .UsingEntity<Festival_MediaPatron>();

            modelBuilder.Entity<Festival>()
                .HasMany(e => e.Events)
                .WithMany(e => e.Festivals)
                .UsingEntity<Festival_Event>();

            modelBuilder.Entity<Reservation>()
                .HasMany(e => e.Seats)
                .WithMany(e => e.Reservations)
                .UsingEntity<Reservation_Seat>();

            /*            modelBuilder.Entity<Reservation_Seat>()
                            .HasOne(rs => rs.Reservation)
                            .WithMany(s => s.Seats)
                            .HasForeignKey(rs => rs.ReservationId)
                            .OnDelete(DeleteBehavior.NoAction);
                        modelBuilder.Entity<Reservation_Seat>()
                            .HasOne(rs => rs.Seat)
                            .WithMany(r => r.Reservations)
                            .HasForeignKey(rs => rs.SeatId)
                            .OnDelete(DeleteBehavior.NoAction);*/

            modelBuilder.Entity<HallType>()
                .HasMany(e => e.Equipments)
                .WithMany(e => e.HallTypes)
                .UsingEntity<HallType_Equipment>();

            modelBuilder.Entity<HallRent>()
               .HasMany(e => e.AdditionalServices)
               .WithMany(e => e.Rents)
               .UsingEntity<HallRent_AdditionalServices>();
        }
    }
}
