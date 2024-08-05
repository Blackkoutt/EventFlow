using EventFlowAPI.DB.Entities;
using Microsoft.EntityFrameworkCore;

namespace EventFlowAPI.DB.Extensions
{
    public static class ModelBuilderSeedExtensions
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {
            var today = new DateTime(2024, 6, 18);

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
                    Price = 340.00
                },
                new AdditionalServices
                {
                    Id = 3,
                    Name = "Obsługa nagłośnienia",
                    Price = 250.00
                },
                new AdditionalServices
                {
                    Id = 4,
                    Name = "Fotograf",
                    Price = 200.00
                },
                new AdditionalServices
                {
                    Id = 5,
                    Name = "Promocja wydarzenia",
                    Price = 140.00
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
                    Description = "Krótki opis wydarzenia Koncert: Mystic Waves"
                },
                new EventDetails
                {
                    Id = 2,
                    Description = "Krótki opis wydarzenia Spektakl: Cień Przeszłość"
                },
                new EventDetails
                {
                    Id = 3,
                    Description = "Krótki opis wydarzenia Film: Królestwo planety małp"
                },
                new EventDetails
                {
                    Id = 4,
                    Description = "Krótki opis wydarzenia Wystawa: Nowe inspiracje"
                },
                new EventDetails
                {
                    Id = 5,
                    Description = "Krótki opis wydarzenia Koncert: New Era"
                },
                new EventDetails
                {
                    Id = 6,
                    Description = "Krótki opis wydarzenia Film: Gladiator"
                },
                new EventDetails
                {
                    Id = 7,
                    Description = "Krótki opis wydarzenia Wystawa: Nowa sztuka"
                }
            );


            modelBuilder.Entity<EventPassType>().HasData(
                new EventPassType
                {
                    Id = 1,
                    Name = "Karnet miesięczny",
                    Price = 89.99
                },
                new EventPassType
                {
                    Id = 2,
                    Name = "Karnet kwartalny",
                    Price = 235.99
                },
                new EventPassType
                {
                    Id = 3,
                    Name = "Karnet półroczny",
                    Price = 499.99
                },
                new EventPassType
                {
                    Id = 4,
                    Name = "Karnet roczny",
                    Price = 999.99
                }
            );


            modelBuilder.Entity<FestivalDetails>().HasData(
                new FestivalDetails
                {
                    Id = 1,
                    Description = "Opis festiwalu muzyki współczesnej",
                },
                new FestivalDetails
                {
                    Id = 2,
                    Description = "Opis festiwalu filmowego",
                },
                new FestivalDetails
                {
                    Id = 3,
                    Description = "Opis festiwalu sztuki abstrakcyjnej",
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
                    AddtionalPaymentPercentage = 25.00
                },
                new SeatType
                {
                    Id = 2,
                    Name = "Miejsce klasy premium",
                    Description = "Opis miejsca klasy premium",
                    AddtionalPaymentPercentage = 10.00
                },
                new SeatType
                {
                    Id = 3,
                    Name = "Miejsce zwykłe",
                    Description = "Opis miejsca zwykłego",
                    AddtionalPaymentPercentage = 0.00
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
                    Name = "Bilet normalny"
                },
                new TicketType
                {
                    Id = 2,
                    Name = "Bilet ulgowy"
                },
                new TicketType
                {
                    Id = 3,
                    Name = "Bilet rodzinny"
                }
            );


            modelBuilder.Entity<UserData>().HasData(
                new UserData
                {
                    Id = 1,
                    Street = "Wesoła",
                    HouseNumber = 12,
                    FlatNumber = 14,
                    City = "Warszawa",
                    ZipCode = "15-264",
                    PhoneNumber = "789456123"
                },
                new UserData
                {
                    Id = 2,
                    Street = "Wiejska",
                    HouseNumber = 10,
                    FlatNumber = 31,
                    City = "Poznań",
                    ZipCode = "01-342",
                    PhoneNumber = "123456789"
                },
                new UserData
                {
                    Id = 3,
                    Street = "Pogodna",
                    HouseNumber = 7,
                    FlatNumber = 21,
                    City = "Białystok",
                    ZipCode = "14-453",
                    PhoneNumber = "147852369"
                }
            );


            modelBuilder.Entity<Festival>().HasData(
                new Festival
                {
                    Id = 1,
                    Name = "Festiwal muzyki współczesnej",
                    StartDate = today.AddMonths(1),
                    EndDate = today.AddMonths(1).AddDays(4)
                },
                new Festival
                {
                    Id = 2,
                    Name = "Festiwal filmowy",
                    StartDate = today.AddMonths(3),
                    EndDate = today.AddMonths(3).AddDays(2)
                },

                new Festival
                {
                    Id = 3,
                    Name = "Festiwal sztuki abstrakcyjnej",
                    StartDate = today.AddMonths(5),
                    EndDate = today.AddMonths(5).AddDays(1)
                }
            );


            modelBuilder.Entity<Hall>().HasData(
                new Hall
                {
                    HallNr = 1,
                    RentalPricePerHour = 120.99,
                    floor = 2,
                    Area = 50.45,
                    NumberOfSeatsRows = 10,
                    NumberOfSeatsColumns = 10,
                    MaxNumberOfSeats = 100,
                    HallTypeId = 1
                },
                new Hall
                {
                    HallNr = 2,
                    RentalPricePerHour = 89.99,
                    floor = 1,
                    Area = 68.85,
                    NumberOfSeatsRows = 8,
                    NumberOfSeatsColumns = 12,
                    MaxNumberOfSeats = 96,
                    HallTypeId = 2
                },
                new Hall
                {
                    HallNr = 3,
                    RentalPricePerHour = 179.99,
                    floor = 2,
                    Area = 75.30,
                    NumberOfSeatsRows = 7,
                    NumberOfSeatsColumns = 14,
                    MaxNumberOfSeats = 98,
                    HallTypeId = 3
                },
                new Hall
                {
                    HallNr = 4,
                    RentalPricePerHour = 199.99,
                    floor = 1,
                    Area = 55.20,
                    NumberOfSeatsRows = 8,
                    NumberOfSeatsColumns = 15,
                    MaxNumberOfSeats = 120,
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


            modelBuilder.Entity<User>().HasData(
                new User
                {
                    Id = 1,
                    Name = "Piotr",
                    Surname = "Nowicki",
                    Email = "p.nowicki@gmail.com",
                    DateOfBirth = new DateTime(2000, 4, 3)
                },
                new User
                {
                    Id = 2,
                    Name = "Adam",
                    Surname = "Nowak",
                    Email = "a.nowak@gmail.com",
                    DateOfBirth = new DateTime(1985, 2, 1)
                },
                new User
                {
                    Id = 3,
                    Name = "Anna",
                    Surname = "Kowalska",
                    Email = "a.kowalska@gmail.com",
                    DateOfBirth = new DateTime(1979, 12, 11)
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
                    StartDate = today.AddMonths(1).AddDays(1),
                    EndDate = today.AddMonths(1).AddDays(1).AddHours(1),
                    CategoryId = 1,
                    HallNr = 2,
                },
                new Event
                {
                    Id = 2,
                    Name = "Cień Przeszłość",
                    StartDate = today.AddMonths(1).AddDays(2),
                    EndDate = today.AddMonths(1).AddDays(2).AddHours(3),
                    CategoryId = 3,
                    HallNr = 3,
                },
                new Event
                {
                    Id = 3,
                    Name = "Królestwo planety małp",
                    StartDate = today.AddMonths(1).AddDays(3),
                    EndDate = today.AddMonths(1).AddDays(3).AddHours(2),
                    CategoryId = 2,
                    HallNr = 1,
                },
                new Event
                {
                    Id = 4,
                    Name = "Nowe inspiracje",
                    StartDate = today.AddMonths(1).AddDays(4),
                    EndDate = today.AddMonths(1).AddDays(4).AddHours(3),
                    CategoryId = 4,
                    HallNr = 4,
                },
                new Event
                {
                    Id = 5,
                    Name = "Koncert: New Era",
                    StartDate = today.AddMonths(2).AddDays(1),
                    EndDate = today.AddMonths(2).AddDays(1).AddHours(1),
                    CategoryId = 1,
                    HallNr = 2,
                },
                new Event
                {
                    Id = 6,
                    Name = "Gladiator",
                    StartDate = today.AddMonths(2).AddDays(3),
                    EndDate = today.AddMonths(2).AddDays(3).AddHours(2),
                    CategoryId = 2,
                    HallNr = 1,
                },
                new Event
                {
                    Id = 7,
                    Name = "Nowa sztuka",
                    StartDate = today.AddMonths(2).AddDays(4),
                    EndDate = today.AddMonths(2).AddDays(4).AddHours(3),
                    CategoryId = 4,
                    HallNr = 4,
                }
            );


            modelBuilder.Entity<Seat>().HasData(
                new Seat
                {
                    Id = 1,
                    SeatNr = 1,
                    Row = 1,
                    Column = 1,
                    SeatTypeId = 1,
                    HallNr = 1
                },
                new Seat
                {
                    Id = 2,
                    SeatNr = 2,
                    Row = 1,
                    Column = 2,
                    SeatTypeId = 1,
                    HallNr = 1
                },
                new Seat
                {
                    Id = 3,
                    SeatNr = 3,
                    Row = 1,
                    Column = 3,
                    SeatTypeId = 1,
                    HallNr = 1
                },
                new Seat
                {
                    Id = 4,
                    SeatNr = 4,
                    Row = 1,
                    Column = 4,
                    SeatTypeId = 1,
                    HallNr = 1
                },
                new Seat
                {
                    Id = 5,
                    SeatNr = 1,
                    Row = 1,
                    Column = 1,
                    SeatTypeId = 2,
                    HallNr = 2
                },
                new Seat
                {
                    Id = 6,
                    SeatNr = 2,
                    Row = 1,
                    Column = 2,
                    SeatTypeId = 2,
                    HallNr = 2
                },
                new Seat
                {
                    Id = 7,
                    SeatNr = 3,
                    Row = 1,
                    Column = 3,
                    SeatTypeId = 2,
                    HallNr = 2
                },
                new Seat
                {
                    Id = 8,
                    SeatNr = 4,
                    Row = 1,
                    Column = 4,
                    SeatTypeId = 2,
                    HallNr = 2
                },
                new Seat
                {
                    Id = 9,
                    SeatNr = 1,
                    Row = 1,
                    Column = 1,
                    SeatTypeId = 3,
                    HallNr = 3
                },
                new Seat
                {
                    Id = 10,
                    SeatNr = 2,
                    Row = 1,
                    Column = 2,
                    SeatTypeId = 3,
                    HallNr = 3
                },
                new Seat
                {
                    Id = 11,
                    SeatNr = 3,
                    Row = 1,
                    Column = 3,
                    SeatTypeId = 3,
                    HallNr = 3
                },
                new Seat
                {
                    Id = 12,
                    SeatNr = 4,
                    Row = 1,
                    Column = 4,
                    SeatTypeId = 3,
                    HallNr = 3
                },
                new Seat
                {
                    Id = 13,
                    SeatNr = 1,
                    Row = 1,
                    Column = 1,
                    SeatTypeId = 3,
                    HallNr = 4
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
                    PaymentAmount = 499.99,
                    PassTypeId = 3,
                    UserId = 1,
                    PaymentTypeId = 1
                },

                new EventPass
                {
                    Id = 2,
                    StartDate = today,
                    RenewalDate = null,
                    EndDate = today.AddYears(1),
                    PaymentDate = today,
                    PaymentAmount = 999.99,
                    PassTypeId = 4,
                    UserId = 2,
                    PaymentTypeId = 2
                },
                new EventPass
                {
                    Id = 3,
                    StartDate = today,
                    RenewalDate = null,
                    EndDate = today.AddMonths(3),
                    PaymentDate = today,
                    PaymentAmount = 235.99,
                    PassTypeId = 2,
                    UserId = 3,
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
                    PaymentAmount = 899.99,
                    PaymentTypeId = 1,
                    HallNr = 1,
                    UserId = 1
                },
                new HallRent
                {
                    Id = 2,
                    StartDate = today.AddMonths(1).AddDays(2),
                    EndDate = today.AddMonths(1).AddDays(2).AddHours(4),
                    PaymentDate = today.AddDays(-2),
                    PaymentAmount = 699.99,
                    PaymentTypeId = 2,
                    HallNr = 3,
                    UserId = 3
                },
                new HallRent
                {
                    Id = 3,
                    StartDate = today.AddMonths(1).AddDays(3),
                    EndDate = today.AddMonths(1).AddDays(3).AddHours(2),
                    PaymentDate = today.AddDays(-3),
                    PaymentAmount = 399.99,
                    PaymentTypeId = 3,
                    HallNr = 3,
                    UserId = 3
                },
                new HallRent
                {
                    Id = 4,
                    StartDate = today.AddMonths(1).AddDays(4),
                    EndDate = today.AddMonths(1).AddDays(4).AddHours(1),
                    PaymentDate = today.AddDays(-3),
                    PaymentAmount = 150.99,
                    PaymentTypeId = 2,
                    HallNr = 4,
                    UserId = 2
                }
            );


            modelBuilder.Entity<EventTicket>().HasData(
                new EventTicket
                {
                    Id = 1,
                    Price = 24.99,
                    EventId = 1,
                    TicketTypeId = 1,
                },
                new EventTicket
                {
                    Id = 2,
                    Price = 34.99,
                    EventId = 2,
                    TicketTypeId = 2,
                },
                new EventTicket
                {
                    Id = 3,
                    Price = 29.99,
                    EventId = 3,
                    TicketTypeId = 3,
                },
                new EventTicket
                {
                    Id = 4,
                    Price = 19.99,
                    EventId = 4,
                    TicketTypeId = 3,
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
                    AdditionalServiceId = 1
                },
                new HallRent_AdditionalServices
                {
                    HallRentId = 2,
                    AdditionalServiceId = 2
                }, new HallRent_AdditionalServices
                {
                    HallRentId = 2,
                    AdditionalServiceId = 3
                },
                new HallRent_AdditionalServices
                {
                    HallRentId = 3,
                    AdditionalServiceId = 2
                },
                new HallRent_AdditionalServices
                {
                    HallRentId = 4,
                    AdditionalServiceId = 4
                },
                new HallRent_AdditionalServices
                {
                    HallRentId = 4,
                    AdditionalServiceId = 5
                }
            );


            modelBuilder.Entity<Reservation>().HasData(
                new Reservation
                {
                    Id = 1,
                    ReservationDate = today.AddDays(10),
                    PaymentAmount = 24.99,
                    UserId = 1,
                    PaymentTypeId = 1,
                    EventTicketId = 1
                },
                new Reservation
                {
                    Id = 2,
                    ReservationDate = today.AddDays(16),
                    PaymentAmount = 34.99,
                    UserId = 2,
                    PaymentTypeId = 2,
                    EventTicketId = 2
                },
                new Reservation
                {
                    Id = 3,
                    ReservationDate = today.AddDays(17),
                    PaymentAmount = 29.99,
                    UserId = 3,
                    PaymentTypeId = 3,
                    EventTicketId = 3
                },
                new Reservation
                {
                    Id = 4,
                    ReservationDate = today.AddDays(18),
                    PaymentAmount = 19.99,
                    UserId = 3,
                    PaymentTypeId = 2,
                    EventTicketId = 4
                }
            );


            modelBuilder.Entity<Reservation_Seat>().HasData(
                new Reservation_Seat
                {
                    ReservationId = 1,
                    SeatId = 4
                },
                new Reservation_Seat
                {
                    ReservationId = 2,
                    SeatId = 8
                },
                new Reservation_Seat
                {
                    ReservationId = 3,
                    SeatId = 10
                },
                new Reservation_Seat
                {
                    ReservationId = 4,
                    SeatId = 13
                }
            );
        }
    }
}
