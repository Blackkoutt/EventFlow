using EventFlowAPI.DB.Extensions;
using EventFlowAPI.DB.Models;
using Microsoft.EntityFrameworkCore;

namespace EventFlowAPI.DB.Context
{
    public class APIContext : DbContext
    {
        public APIContext(DbContextOptions<APIContext> options) : base(options) { }
        public DbSet<AdditionalServices> AdditionalServices { get; set; }
        public DbSet<Equipment> Equipment{ get; set; }
        public DbSet<Event> Event { get; set; }
        public DbSet<EventCategory> EventCategory { get; set; }
        public DbSet<EventDetails> EventDetails { get; set; }
        public DbSet<EventPass> EventPass { get; set; }
        public DbSet<EventPassType> EventPassType { get; set; }
        public DbSet<EventTicket> EventTicket { get; set; }
        public DbSet<TicketType> TicketType { get; set; }
        public DbSet<Festival> Festival { get; set; }
        public DbSet<MediaPatron> MediaPatron { get; set; }
        public DbSet<Organizer> Organizer { get; set; }
        public DbSet<Sponsor> Sponsor { get; set; }
        public DbSet<Festival_Event> Festival_Event { get; set; }
        public DbSet<Festival_MediaPatron> Festival_MediaPatron { get; set; }
        public DbSet<Festival_Organizer> Festival_Organizer { get; set; }
        public DbSet<Festival_Sponsor> Festival_Sponsor { get; set; }
        public DbSet<FestivalDetails> FestivalDetails { get; set; }
        public DbSet<Hall> Hall { get; set; }
        public DbSet<HallRent> HallRent { get; set; }
        public DbSet<HallRent_AdditionalServices> HallRent_AdditionalServices { get; set; }
        public DbSet<HallType> HallType { get; set; }
        public DbSet<HallType_Equipment> HallType_Equipment { get; set; }
        public DbSet<PaymentType> PaymentType { get; set; }
        public DbSet<Reservation> Reservation { get; set; }
        public DbSet<Seat> Seat { get; set; }
        public DbSet<SeatType> SeatType { get; set; }
        public DbSet<Reservation_Seat> Reservation_Seat { get; set; }  
        public DbSet<User> User { get; set; }
        public DbSet<UserData> UserData { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Primary keys
            modelBuilder.Entity<AdditionalServices>().HasKey(x => x.Id);
            modelBuilder.Entity<Equipment>().HasKey(x => x.Id);
            modelBuilder.Entity<Event>().HasKey(x => x.Id);
            modelBuilder.Entity<EventCategory>().HasKey(x => x.Id);
            modelBuilder.Entity<EventDetails>().HasKey(x => x.Id);
            modelBuilder.Entity<EventPass>().HasKey(x => x.Id);
            modelBuilder.Entity<EventPassType>().HasKey(x => x.Id);
            modelBuilder.Entity<EventTicket>().HasKey(x => x.Id);
            modelBuilder.Entity<TicketType>().HasKey(x => x.Id);
            modelBuilder.Entity<Festival>().HasKey(x => x.Id);
            modelBuilder.Entity<MediaPatron>().HasKey(x => x.Id);
            modelBuilder.Entity<Organizer>().HasKey(x => x.Id);
            modelBuilder.Entity<Sponsor>().HasKey(x => x.Id);
            modelBuilder.Entity<Festival_Event>().HasKey(x => new { x.FestivalId, x.EventId});
            modelBuilder.Entity<Festival_MediaPatron>().HasKey(x => new { x.FestivalId, x.MediaPatronId});
            modelBuilder.Entity<Festival_Organizer>().HasKey(x => new { x.FestivalId, x.OrganizerId});
            modelBuilder.Entity<Festival_Sponsor>().HasKey(x => new { x.FestivalId, x.SponsorId});
            modelBuilder.Entity<FestivalDetails>().HasKey(x => x.Id);
            modelBuilder.Entity<Hall>().HasKey(x => x.HallNr);
            modelBuilder.Entity<HallRent>().HasKey(x => x.Id);
            modelBuilder.Entity<HallRent_AdditionalServices>().HasKey(x => new { x.HallRentId, x.AdditionalServiceId});
            modelBuilder.Entity<HallType>().HasKey(x => x.Id);
            modelBuilder.Entity<HallType_Equipment>().HasKey(x => new { x.EquipmentId, x.HallTypeId});
            modelBuilder.Entity<PaymentType>().HasKey(x => x.Id);
            modelBuilder.Entity<Reservation>().HasKey(x => x.Id);
            modelBuilder.Entity<Seat>().HasKey(x => x.Id);
            modelBuilder.Entity<SeatType>().HasKey(x => x.Id);
            modelBuilder.Entity<Reservation_Seat>().HasKey(x => new { x.ReservationId, x.SeatId});
            modelBuilder.Entity<User>().HasKey(x => x.Id);
            modelBuilder.Entity<UserData>().HasKey(x => x.Id);

            // Relations 1:1
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


            // Relations 1:N (only required)
            modelBuilder.Entity<Seat>()
                .HasOne(x => x.Hall)
                .WithMany(x => x.Seats)
                .HasForeignKey(x => x.HallNr);
            modelBuilder.Entity<Event>()
                .HasOne(x => x.Hall)
                .WithMany(x => x.Events)
                .HasForeignKey(x => x.HallNr);
            modelBuilder.Entity<HallRent>()
                .HasOne(x => x.Hall)
                .WithMany(x => x.Rents)
                .HasForeignKey(x => x.HallNr);
            modelBuilder.Entity<Hall>()
                .HasOne(x => x.Type)
                .WithMany(x => x.Halls)
                .HasForeignKey(x => x.HallTypeId);


            // Relations N:N
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

            modelBuilder.Seed();
        }
    }
}
