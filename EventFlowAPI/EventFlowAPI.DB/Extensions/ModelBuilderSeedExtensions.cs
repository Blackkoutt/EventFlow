using EventFlowAPI.DB.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Runtime.CompilerServices;

namespace EventFlowAPI.DB.Extensions
{
    public static class ModelBuilderSeedExtensions
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {
            var today = new DateTime(2024, 8, 5);

            modelBuilder.SeedUsersData();
            modelBuilder.SeedUsers();
            modelBuilder.SeedRoles();
            modelBuilder.SeedUsersInRoles();


            modelBuilder.Entity<AdditionalServices>().HasData(
                new AdditionalServices
                {
                    Id = 1,
                    Name = "DJ",
                    Price = 400.00m
                },
                new AdditionalServices
                {
                    Id = 2,
                    Name = "Obsługa oświetlenia",
                    Price = 340.00m
                },
                new AdditionalServices
                {
                    Id = 3,
                    Name = "Obsługa nagłośnienia",
                    Price = 250.00m
                },
                new AdditionalServices
                {
                    Id = 4,
                    Name = "Fotograf",
                    Price = 200.00m
                },
                new AdditionalServices
                {
                    Id = 5,
                    Name = "Promocja wydarzenia",
                    Price = 140.00m
                }
            );

            modelBuilder.Entity<Equipment>().HasData(
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


           modelBuilder.Entity<EventCategory>().HasData(
                new EventCategory
                {
                    Id = 1,
                    Name = "Koncert"
                },
                new EventCategory
                {
                    Id = 2,
                    Name = "Film"
                },
                new EventCategory
                {
                    Id = 3,
                    Name = "Spektakl"
                },
                new EventCategory
                {
                    Id = 4,
                    Name = "Wystawa"
                }
            );


            modelBuilder.Entity<EventDetails>().HasData(
                new EventDetails
                {
                    Id = 1,
                    LongDescription = "Krótki opis wydarzenia Koncert: Mystic Waves"
                },
                new EventDetails
                {
                    Id = 2,
                    LongDescription = "Krótki opis wydarzenia Spektakl: Cień Przeszłość"
                },
                new EventDetails
                {
                    Id = 3,
                    LongDescription = "Krótki opis wydarzenia Film: Królestwo planety małp"
                },
                new EventDetails
                {
                    Id = 4,
                    LongDescription = "Krótki opis wydarzenia Wystawa: Nowe inspiracje"
                },
                new EventDetails
                {
                    Id = 5,
                    LongDescription = "Krótki opis wydarzenia Koncert: New Era"
                },
                new EventDetails
                {
                    Id = 6,
                    LongDescription = "Krótki opis wydarzenia Film: Gladiator"
                },
                new EventDetails
                {
                    Id = 7,
                    LongDescription = "Krótki opis wydarzenia Wystawa: Nowa sztuka"
                }
            );


            modelBuilder.Entity<EventPassType>().HasData(
                new EventPassType
                {
                    Id = 1,
                    Name = "Karnet miesięczny",
                    ValidityPeriodInMonths = 1,
                    Price = 89.99m
                },
                new EventPassType
                {
                    Id = 2,
                    Name = "Karnet kwartalny",
                    ValidityPeriodInMonths = 3,
                    Price = 235.99m
                },
                new EventPassType
                {
                    Id = 3,
                    Name = "Karnet półroczny",
                    ValidityPeriodInMonths = 6,
                    Price = 499.99m
                },
                new EventPassType
                {
                    Id = 4,
                    Name = "Karnet roczny",
                    ValidityPeriodInMonths = 12,
                    Price = 999.99m
                }
            );


            modelBuilder.Entity<FestivalDetails>().HasData(
                new FestivalDetails
                {
                    Id = 1,
                    LongDescription = "Opis festiwalu muzyki współczesnej",
                },
                new FestivalDetails
                {
                    Id = 2,
                    LongDescription = "Opis festiwalu filmowego",
                },
                new FestivalDetails
                {
                    Id = 3,
                    LongDescription = "Opis festiwalu sztuki abstrakcyjnej",
                }
            );


            modelBuilder.Entity<HallType>().HasData(
                new HallType
                {
                    Id = 1,
                    Name = "Sala filmowa",
                    Description = "Nowa sala kinowa wyposażona w nowoczesne nagłośnienie i ekran",
                },

                new HallType
                {
                    Id = 2,
                    Name = "Sala koncertowa",
                    Description = "Nowa sala koncertowa wyposażona w najwyższej klasy nagłośnienie",
                },

                new HallType
                {
                    Id = 3,
                    Name = "Sala teatralna",
                    Description = "Opis sali teatralnej",
                },
                new HallType
                {
                    Id = 4,
                    Name = "Sala wystawowa",
                    Description = "Opis sali wystawowa",
                }
            );


            modelBuilder.Entity<MediaPatron>().HasData(
               new MediaPatron
               {
                   Id = 1,
                   Name = "Gazeta Nowoczesna"
               },
               new MediaPatron
               {
                   Id = 2,
                   Name = "Nowy świat TV"
               },
               new MediaPatron
               {
                   Id = 3,
                   Name = "Tygodnik Nowiny"
               }
            );


            modelBuilder.Entity<Organizer>().HasData(
               new Organizer
               {
                   Id = 1,
                   Name = "EventFlow"
               },
               new Organizer
               {
                   Id = 2,
                   Name = "Snowflake"
               },
               new Organizer
               {
                   Id = 3,
                   Name = "Aura"
               }
            );


            modelBuilder.Entity<PaymentType>().HasData(
                new PaymentType
                {
                    Id = 1,
                    Name = "Karta kredytowa",
                },
                new PaymentType
                {
                    Id = 2,
                    Name = "Przelew",
                },
                new PaymentType
                {
                    Id = 3,
                    Name = "BLIK",
                },
                new PaymentType
                {
                    Id = 4,
                    Name = "Zapłać później",
                }
            );


            modelBuilder.Entity<SeatType>().HasData(
                new SeatType
                {
                    Id = 1,
                    Name = "Miejsce VIP",
                    Description = "Opis miejsca VIP",
                    AddtionalPaymentPercentage = 25.00m
                },
                new SeatType
                {
                    Id = 2,
                    Name = "Miejsce klasy premium",
                    Description = "Opis miejsca klasy premium",
                    AddtionalPaymentPercentage = 10.00m
                },
                new SeatType
                {
                    Id = 3,
                    Name = "Miejsce zwykłe",
                    Description = "Opis miejsca zwykłego",
                    AddtionalPaymentPercentage = 0.00m
                }
            );


            modelBuilder.Entity<Sponsor>().HasData(
                new Sponsor
                {
                    Id = 1,
                    Name = "Basel"
                },
                new Sponsor
                {
                    Id = 2,
                    Name = "Vision"
                },
                new Sponsor
                {
                    Id = 3,
                    Name = "Waveless"
                }
            );


            modelBuilder.Entity<TicketType>().HasData(
                new TicketType
                {
                    Id = 1,
                    Name = "Bilet normalny",

                },
                new TicketType
                {
                    Id = 2,
                    Name = "Bilet ulgowy",
                },
                new TicketType
                {
                    Id = 3,
                    Name = "Bilet rodzinny",
                }
            ) ;


            modelBuilder.Entity<Festival>().HasData(
                new Festival
                {
                    Id = 1,
                    Name = "Festiwal muzyki współczesnej",
                    ShortDescription = "Festiwal muzyki współczesnej to nowy festiwal organizowany przez XYZ.",
                    StartDate = today.AddMonths(1).AddDays(1),
                    EndDate = today.AddMonths(2).AddDays(1).AddHours(1),
                    Duration = today.AddMonths(1) - today.AddMonths(2).AddDays(1).AddHours(1)
                },
                new Festival
                {
                    Id = 2,
                    Name = "Festiwal filmowy",
                    ShortDescription = "Festiwal filmowy to festiwal na którym można obejrzeć filmy.",
                    StartDate = today.AddMonths(1).AddDays(2),
                    EndDate = today.AddMonths(2).AddDays(3).AddHours(2),
                    Duration = today.AddMonths(1).AddDays(2) - today.AddMonths(2).AddDays(3).AddHours(2)
                },

                new Festival
                {
                    Id = 3,
                    Name = "Festiwal sztuki abstrakcyjnej",
                    ShortDescription = "Festiwal sztuki abstrakcyjnej to festiwal na którym można zobaczyć sztukę.",
                    StartDate = today.AddMonths(1).AddDays(4),
                    EndDate = today.AddMonths(2).AddDays(4).AddHours(3),
                    Duration = today.AddMonths(1).AddDays(4) - today.AddMonths(2).AddDays(4).AddHours(3)
                }
            );


            modelBuilder.Entity<Hall>().HasData(
                new Hall
                {
                    Id = 1,
                    HallNr = 1,
                    RentalPricePerHour = 120.99m,
                    Floor = 2,
                    TotalLength = 12m,
                    TotalWidth = 10m,
                    TotalArea = 120m,
                    StageArea = 30m,
                    NumberOfSeatsRows = 9,
                    MaxNumberOfSeatsRows = 9,
                    NumberOfSeatsColumns = 10,
                    MaxNumberOfSeatsColumns = 10,
                    NumberOfSeats = 90,
                    MaxNumberOfSeats = 90,
                    HallTypeId = 1
                },
                new Hall
                {
                    Id = 2,
                    HallNr = 2,
                    RentalPricePerHour = 89.99m,
                    Floor = 1,
                    TotalLength = 15m,
                    TotalWidth = 10m,
                    TotalArea = 150m,
                    StageArea = null,
                    NumberOfSeatsRows = 15,
                    MaxNumberOfSeatsRows = 15,
                    NumberOfSeatsColumns = 10,
                    MaxNumberOfSeatsColumns = 10,
                    NumberOfSeats = 150,
                    MaxNumberOfSeats = 150,
                    HallTypeId = 2
                },
                new Hall
                {
                    Id = 3,
                    HallNr = 3,
                    RentalPricePerHour = 179.99m,
                    Floor = 2,
                    TotalLength = 10m,
                    TotalWidth = 8m,
                    TotalArea = 80m,
                    StageArea = 20m,
                    NumberOfSeatsRows = 6,
                    MaxNumberOfSeatsRows = 6,
                    NumberOfSeatsColumns = 10,
                    MaxNumberOfSeatsColumns = 10,
                    NumberOfSeats = 60,
                    MaxNumberOfSeats = 60,
                    HallTypeId = 3
                },
                new Hall
                {
                    Id = 4,
                    HallNr = 4,
                    RentalPricePerHour = 199.99m,
                    Floor = 1,
                    TotalLength = 14m,
                    TotalWidth = 10m,
                    TotalArea = 140m,
                    StageArea = 40m,
                    NumberOfSeatsRows = 10,
                    MaxNumberOfSeatsRows = 10,
                    NumberOfSeatsColumns = 10,
                    MaxNumberOfSeatsColumns = 10,
                    NumberOfSeats = 100,
                    MaxNumberOfSeats = 100,
                    HallTypeId = 4
                }
            );


            modelBuilder.Entity<HallType_Equipment>().HasData(
                new HallType_Equipment
                {
                    HallTypeId = 1,
                    EquipmentId = 1
                },
                new HallType_Equipment
                {
                    HallTypeId = 1,
                    EquipmentId = 3
                },
                new HallType_Equipment
                {
                    HallTypeId = 2,
                    EquipmentId = 2
                },
                new HallType_Equipment
                {
                    HallTypeId = 2,
                    EquipmentId = 4
                },
                new HallType_Equipment
                {
                    HallTypeId = 3,
                    EquipmentId = 2
                }
            );


            modelBuilder.Entity<Festival_MediaPatron>().HasData(
                new Festival_MediaPatron
                {
                    FestivalId = 1,
                    MediaPatronId = 1
                },
                new Festival_MediaPatron
                {
                    FestivalId = 1,
                    MediaPatronId = 2
                },
                new Festival_MediaPatron
                {
                    FestivalId = 2,
                    MediaPatronId = 1
                },
                new Festival_MediaPatron
                {
                    FestivalId = 2,
                    MediaPatronId = 3
                },
                new Festival_MediaPatron
                {
                    FestivalId = 3,
                    MediaPatronId = 2
                },
                new Festival_MediaPatron
                {
                    FestivalId = 3,
                    MediaPatronId = 3
                }
            );
            modelBuilder.Entity<Festival_Organizer>().HasData(
                new Festival_Organizer
                {
                    FestivalId = 1,
                    OrganizerId = 1
                },
                new Festival_Organizer
                {
                    FestivalId = 1,
                    OrganizerId = 2
                },
                new Festival_Organizer
                {
                    FestivalId = 2,
                    OrganizerId = 1
                },
                new Festival_Organizer
                {
                    FestivalId = 2,
                    OrganizerId = 3
                },
                new Festival_Organizer
                {
                    FestivalId = 3,
                    OrganizerId = 2
                },
                new Festival_Organizer
                {
                    FestivalId = 3,
                    OrganizerId = 3
                }
            );


            modelBuilder.Entity<Festival_Sponsor>().HasData(
                new Festival_Sponsor
                {
                    FestivalId = 1,
                    SponsorId = 1
                },
                new Festival_Sponsor
                {
                    FestivalId = 1,
                    SponsorId = 2
                },
                new Festival_Sponsor
                {
                    FestivalId = 2,
                    SponsorId = 1
                },
                new Festival_Sponsor
                {
                    FestivalId = 2,
                    SponsorId = 3
                },
                new Festival_Sponsor
                {
                    FestivalId = 3,
                    SponsorId = 2
                },
                new Festival_Sponsor
                {
                    FestivalId = 3,
                    SponsorId = 3
                }
            );


            modelBuilder.Entity<Event>().HasData(
                new Event
                {
                    Id = 1,
                    Name = "Koncert: Mystic Waves",
                    ShortDescription = "Jedyna taka okazja na usłyszenie Mystic Waves na żywo.",
                    StartDate = today.AddMonths(1).AddDays(1),
                    EndDate = today.AddMonths(1).AddDays(1).AddHours(1),
                    Duration = today.AddMonths(1).AddDays(1) - today.AddMonths(1).AddDays(1).AddHours(1),
                    CategoryId = 1,
                    DefaultHallId = 2,
                    HallId = 2,
                },
                new Event
                {
                    Id = 2,
                    Name = "Cień Przeszłośći",
                    ShortDescription = "Cień Przeszłości to jedyny taki spektakl.",
                    StartDate = today.AddMonths(1).AddDays(2),
                    EndDate = today.AddMonths(1).AddDays(2).AddHours(3),
                    Duration = today.AddMonths(1).AddDays(2) - today.AddMonths(1).AddDays(2).AddHours(3),
                    CategoryId = 3,
                    DefaultHallId = 3,
                    HallId = 3,
                },
                new Event
                {
                    Id = 3,
                    Name = "Królestwo planety małp",
                    ShortDescription = "Nowy film Królestwo planety małp już w kinach!.",
                    StartDate = today.AddMonths(1).AddDays(3),
                    EndDate = today.AddMonths(1).AddDays(3).AddHours(2),
                    Duration = today.AddMonths(1).AddDays(3) - today.AddMonths(1).AddDays(3).AddHours(2),
                    CategoryId = 2,
                    DefaultHallId = 1,
                    HallId = 1,
                },
                new Event
                {
                    Id = 4,
                    Name = "Nowe inspiracje",
                    ShortDescription = "Nowe inspiracje to nowoczesna wystawa sztuki.",
                    StartDate = today.AddMonths(1).AddDays(4),
                    EndDate = today.AddMonths(1).AddDays(4).AddHours(3),
                    Duration = today.AddMonths(1).AddDays(4) - today.AddMonths(1).AddDays(4).AddHours(3),
                    CategoryId = 4,
                    DefaultHallId = 4,
                    HallId = 4,
                },
                new Event
                {
                    Id = 5,
                    Name = "Koncert: New Era",
                    ShortDescription = "Jedyna taka okazja na usłyszenie New Era na żywo.",
                    StartDate = today.AddMonths(2).AddDays(1),
                    EndDate = today.AddMonths(2).AddDays(1).AddHours(1),
                    Duration = today.AddMonths(2).AddDays(1) - today.AddMonths(2).AddDays(1).AddHours(1),
                    CategoryId = 1,
                    DefaultHallId = 2,
                    HallId = 2,
                },
                new Event
                {
                    Id = 6,
                    Name = "Gladiator",
                    ShortDescription = "Nowy film Gladiator już w kinach!.",
                    StartDate = today.AddMonths(2).AddDays(3),
                    EndDate = today.AddMonths(2).AddDays(3).AddHours(2),
                    Duration = today.AddMonths(2).AddDays(3) - today.AddMonths(2).AddDays(3).AddHours(2),
                    CategoryId = 2,
                    DefaultHallId = 1,
                    HallId = 1,
                },
                new Event
                {
                    Id = 7,
                    Name = "Nowa sztuka",
                    ShortDescription = "Nowe sztuka to nowoczesna wystawa sztuki.",
                    StartDate = today.AddMonths(2).AddDays(4),
                    EndDate = today.AddMonths(2).AddDays(4).AddHours(3),
                    Duration = today.AddMonths(2).AddDays(4) - today.AddMonths(2).AddDays(4).AddHours(3),
                    CategoryId = 4,
                    DefaultHallId = 4,
                    HallId = 4,
                }
            );


            modelBuilder.Entity<Seat>().HasData(
                new Seat
                {
                    Id = 1,
                    SeatNr = 1,
                    Row = 1,
                    GridRow = 1,
                    Column = 1,
                    GridColumn = 1,
                    SeatTypeId = 1,
                    HallId = 1
                },
                new Seat
                {
                    Id = 2,
                    SeatNr = 2,
                    Row = 1,
                    GridRow = 1,
                    Column = 2,
                    GridColumn = 2,
                    SeatTypeId = 1,
                    HallId = 1
                },
                new Seat
                {
                    Id = 3,
                    SeatNr = 3,
                    Row = 1,
                    GridRow = 1,
                    Column = 3,
                    GridColumn = 3,
                    SeatTypeId = 1,
                    HallId = 1
                },
                new Seat
                {
                    Id = 4,
                    SeatNr = 4,
                    Row = 1,
                    GridRow = 1,
                    Column = 4,
                    GridColumn = 4,
                    SeatTypeId = 1,
                    HallId = 1
                },
                new Seat
                {
                    Id = 5,
                    SeatNr = 1,
                    Row = 1,
                    GridRow = 1,
                    Column = 1,
                    GridColumn = 1,
                    SeatTypeId = 2,
                    HallId = 2
                },
                new Seat
                {
                    Id = 6,
                    SeatNr = 2,
                    Row = 1,
                    GridRow = 1,
                    Column = 2,
                    GridColumn = 2,
                    SeatTypeId = 2,
                    HallId = 2
                },
                new Seat
                {
                    Id = 7,
                    SeatNr = 3,
                    Row = 1,
                    GridRow = 1,
                    Column = 3,
                    GridColumn = 3,
                    SeatTypeId = 1,
                    HallId = 2
                },
                new Seat
                {
                    Id = 8,
                    SeatNr = 4,
                    Row = 1,
                    GridRow = 1,
                    Column = 4,
                    GridColumn = 4,
                    SeatTypeId = 2,
                    HallId = 2
                },
                new Seat
                {
                    Id = 9,
                    SeatNr = 1,
                    Row = 1,
                    GridRow = 1,
                    Column = 1,
                    GridColumn = 1,
                    SeatTypeId = 3,
                    HallId = 3
                },
                new Seat
                {
                    Id = 10,
                    SeatNr = 2,
                    Row = 1,
                    GridRow = 1,
                    Column = 2,
                    GridColumn = 2,
                    SeatTypeId = 3,
                    HallId = 3
                },
                new Seat
                {
                    Id = 11,
                    SeatNr = 3,
                    Row = 1,
                    GridRow = 1,
                    Column = 3,
                    GridColumn = 3,
                    SeatTypeId = 3,
                    HallId = 3
                },
                new Seat
                {
                    Id = 12,
                    SeatNr = 4,
                    Row = 1,
                    GridRow = 1,
                    Column = 4,
                    GridColumn = 4,
                    SeatTypeId = 3,
                    HallId = 3
                },
                new Seat
                {
                    Id = 13,
                    SeatNr = 1,
                    Row = 1,
                    GridRow = 1,
                    Column = 1,
                    GridColumn = 1,
                    SeatTypeId = 3,
                    HallId = 4
                }
            );


            modelBuilder.Entity<EventPass>().HasData(
                new EventPass
                {
                    Id = 1,
                    StartDate = today,
                    RenewalDate = null,
                    EndDate = today.AddMonths(6),
                    PaymentDate = today,
                    PaymentAmount = 499.99m,
                    PassTypeId = 3,
                    UserId = "1",
                    PaymentTypeId = 1
                },

                new EventPass
                {
                    Id = 2,
                    StartDate = today,
                    RenewalDate = null,
                    EndDate = today.AddYears(1),
                    PaymentDate = today,
                    PaymentAmount = 999.99m,
                    PassTypeId = 4,
                    UserId = "2",
                    PaymentTypeId = 2
                },
                new EventPass
                {
                    Id = 3,
                    StartDate = today,
                    RenewalDate = null,
                    EndDate = today.AddMonths(3),
                    PaymentDate = today,
                    PaymentAmount = 235.99m,
                    PassTypeId = 2,
                    UserId = "3",
                    PaymentTypeId = 1
                }
            );


            modelBuilder.Entity<HallRent>().HasData(
                new HallRent
                {
                    Id = 1,
                    StartDate = today.AddMonths(1).AddDays(1),
                    EndDate = today.AddMonths(1).AddDays(1).AddHours(8),
                    PaymentDate = today.AddDays(-1),
                    PaymentAmount = 899.99m,
                    PaymentTypeId = 1,
                    HallId = 1,
                    DefaultHallId = 1,
                    UserId = "1"
                },
                new HallRent
                {
                    Id = 2,
                    StartDate = today.AddMonths(1).AddDays(2),
                    EndDate = today.AddMonths(1).AddDays(2).AddHours(4),
                    PaymentDate = today.AddDays(-2),
                    PaymentAmount = 699.99m,
                    PaymentTypeId = 2,
                    HallId = 3,
                    DefaultHallId = 3,
                    UserId = "3"
                },
                new HallRent
                {
                    Id = 3,
                    StartDate = today.AddMonths(1).AddDays(3),
                    EndDate = today.AddMonths(1).AddDays(3).AddHours(2),
                    PaymentDate = today.AddDays(-3),
                    PaymentAmount = 399.99m,
                    PaymentTypeId = 3,
                    HallId = 3,
                    DefaultHallId = 3,
                    UserId = "3"
                },
                new HallRent
                {
                    Id = 4,
                    StartDate = today.AddMonths(1).AddDays(4),
                    EndDate = today.AddMonths(1).AddDays(4).AddHours(1),
                    PaymentDate = today.AddDays(-3),
                    PaymentAmount = 150.99m,
                    PaymentTypeId = 2,
                    HallId = 4,
                    DefaultHallId = 4,
                    UserId = "2"
                }
            );


            modelBuilder.Entity<Ticket>().HasData(
                new Ticket
                {
                    Id = 1,
                    Price = 24.99m,
                    EventId = 1,
                    TicketTypeId = 1
                },
                new Ticket
                {
                    Id = 2,
                    Price = 34.99m,
                    EventId = 2,
                    TicketTypeId = 2
                },
                new Ticket
                {
                    Id = 3,
                    Price = 29.99m,
                    EventId = 3,
                    TicketTypeId = 3
                },
                new Ticket
                {
                    Id = 4,
                    Price = 19.99m,
                    EventId = 4,
                    TicketTypeId = 3
                },
                new Ticket
                {
                    Id = 5,
                    Price = 19.99m,
                    EventId = 1,
                    FestivalId = 1,
                    TicketTypeId = 1
                },
                new Ticket
                {
                    Id = 6,
                    Price = 19.99m,
                    EventId = 5,
                    FestivalId = 1,
                    TicketTypeId = 1
                },
                new Ticket
                {
                    Id = 7,
                    Price = 29.99m,
                    EventId = 2,
                    FestivalId = 2,
                    TicketTypeId = 2
                },
                new Ticket
                {
                    Id = 8,
                    Price = 29.99m,
                    EventId = 6,
                    FestivalId = 2,
                    TicketTypeId = 2
                }
            );



            modelBuilder.Entity<Festival_Event>().HasData(
                new Festival_Event
                {
                    FestivalId = 1,
                    EventId = 1
                },
                new Festival_Event
                {
                    FestivalId = 1,
                    EventId = 5
                },
                new Festival_Event
                {
                    FestivalId = 2,
                    EventId = 2
                },
                new Festival_Event
                {
                    FestivalId = 2,
                    EventId = 6
                },
                new Festival_Event
                {
                    FestivalId = 3,
                    EventId = 4
                },
                new Festival_Event
                {
                    FestivalId = 3,
                    EventId = 7
                }
            );


            modelBuilder.Entity<HallRent_AdditionalServices>().HasData(
                new HallRent_AdditionalServices
                {
                    HallRentId = 2,
                    AdditionalServicesId = 1
                },
                new HallRent_AdditionalServices
                {
                    HallRentId = 2,
                    AdditionalServicesId = 2
                }, new HallRent_AdditionalServices
                {
                    HallRentId = 2,
                    AdditionalServicesId = 3
                },
                new HallRent_AdditionalServices
                {
                    HallRentId = 3,
                    AdditionalServicesId = 2
                },
                new HallRent_AdditionalServices
                {
                    HallRentId = 4,
                    AdditionalServicesId = 4
                },
                new HallRent_AdditionalServices
                {
                    HallRentId = 4,
                    AdditionalServicesId = 5
                }
            );


            modelBuilder.Entity<Reservation>().HasData(
                new Reservation
                {
                    Id = 1,
                    ReservationDate = today.AddDays(10),
                    StartOfReservationDate = today.AddMonths(1).AddDays(1),
                    EndOfReservationDate = today.AddMonths(1).AddDays(1).AddHours(1),
                    PaymentDate = today.AddDays(10),
                    PaymentAmount = 24.99m,
                    UserId = "1",
                    PaymentTypeId = 1,
                    TicketId = 1
                },
                new Reservation
                {
                    Id = 2,
                    ReservationDate = today.AddDays(16),
                    StartOfReservationDate = today.AddMonths(1).AddDays(2),
                    EndOfReservationDate = today.AddMonths(1).AddDays(2).AddHours(3),
                    PaymentDate = today.AddDays(16),
                    PaymentAmount = 34.99m,
                    UserId = "2",
                    PaymentTypeId = 2,
                    TicketId = 2
                },
                new Reservation
                {
                    Id = 3,
                    ReservationDate = today.AddDays(17),
                    StartOfReservationDate = today.AddMonths(1).AddDays(3),
                    EndOfReservationDate = today.AddMonths(1).AddDays(3).AddHours(2),
                    PaymentDate = today.AddDays(17),
                    PaymentAmount = 29.99m,
                    UserId = "3",
                    PaymentTypeId = 3,
                    TicketId = 3
                },
                new Reservation
                {
                    Id = 4,
                    ReservationDate = today.AddDays(18),
                    StartOfReservationDate = today.AddMonths(1).AddDays(4),
                    EndOfReservationDate = today.AddMonths(1).AddDays(4).AddHours(3),
                    PaymentDate = today.AddDays(18),
                    PaymentAmount = 19.99m,
                    UserId = "3",
                    PaymentTypeId = 2,
                    TicketId = 4
                },
                new Reservation
                {
                    Id = 5,
                    ReservationDate = today.AddDays(13),
                    StartOfReservationDate = today.AddMonths(1).AddDays(1),
                    EndOfReservationDate = today.AddMonths(1).AddDays(1).AddHours(1),
                    PaymentDate = today.AddDays(13),
                    PaymentAmount = 19.99m,
                    UserId = "2",
                    PaymentTypeId = 2,
                    TicketId = 5
                },
                new Reservation
                {
                    Id = 6,
                    ReservationDate = today.AddDays(14),
                    StartOfReservationDate = today.AddMonths(2).AddDays(1),
                    EndOfReservationDate = today.AddMonths(2).AddDays(1).AddHours(1),
                    PaymentDate = today.AddDays(14),
                    PaymentAmount = 19.99m,
                    UserId = "2",
                    PaymentTypeId = 2,
                    TicketId = 6
                },
                new Reservation
                {
                    Id = 7,
                    ReservationDate = today.AddDays(15),
                    StartOfReservationDate = today.AddMonths(1).AddDays(2),
                    EndOfReservationDate = today.AddMonths(1).AddDays(2).AddHours(3),
                    PaymentDate = today.AddDays(15),
                    PaymentAmount = 29.99m,
                    UserId = "2",
                    PaymentTypeId = 2,
                    TicketId = 7
                },
                new Reservation
                {
                    Id = 8,
                    ReservationDate = today.AddDays(15),
                    StartOfReservationDate = today.AddMonths(2).AddDays(3),
                    EndOfReservationDate = today.AddMonths(2).AddDays(3).AddHours(2),
                    PaymentDate = today.AddDays(15),
                    PaymentAmount = 29.99m,
                    UserId = "2",
                    PaymentTypeId = 2,
                    TicketId = 7
                }
            );


            modelBuilder.Entity<Reservation_Seat>().HasData(
                new Reservation_Seat
                {
                    ReservationId = 1,
                    SeatId = 5
                },
                new Reservation_Seat
                {
                    ReservationId = 2,
                    SeatId = 9
                },
                new Reservation_Seat
                {
                    ReservationId = 3,
                    SeatId = 1
                },
                new Reservation_Seat
                {
                    ReservationId = 4,
                    SeatId = 13
                },
                new Reservation_Seat
                {
                    ReservationId = 5,
                    SeatId = 8
                },
                new Reservation_Seat
                {
                    ReservationId = 6,
                    SeatId = 8
                },
                new Reservation_Seat
                {
                    ReservationId = 7,
                    SeatId = 12
                },
                new Reservation_Seat
                {
                    ReservationId = 8,
                    SeatId = 3
                }
            );
        }
    }
}
