using EventFlowAPI.DB.Entities;
using Microsoft.EntityFrameworkCore;

namespace EventFlowAPI.DB.Extensions
{
    public static class ModelBuilderSeedExtensions
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {
            var today = new DateTime(2024, 9, 30);

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
                    RenewalDiscountPercentage = 5m,
                    Price = 89.99m
                },
                new EventPassType
                {
                    Id = 2,
                    Name = "Karnet kwartalny",
                    ValidityPeriodInMonths = 3,
                    RenewalDiscountPercentage = 10m,
                    Price = 235.99m
                },
                new EventPassType
                {
                    Id = 3,
                    Name = "Karnet półroczny",
                    ValidityPeriodInMonths = 6,
                    RenewalDiscountPercentage = 15m,
                    Price = 499.99m
                },
                new EventPassType
                {
                    Id = 4,
                    Name = "Karnet roczny",
                    ValidityPeriodInMonths = 12,
                    RenewalDiscountPercentage = 20m,
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
                },
                new PaymentType
                {
                    Id = 5,
                    Name = "Karnet",
                }
            );


            modelBuilder.Entity<SeatType>().HasData(
                new SeatType
                {
                    Id = 1,
                    Name = "Miejsce VIP",
                    Description = "Opis miejsca VIP",
                    SeatColor = "#9803fc",
                    AddtionalPaymentPercentage = 25.00m
                },
                new SeatType
                {
                    Id = 2,
                    Name = "Miejsce klasy premium",
                    SeatColor = "#ffa600",
                    Description = "Opis miejsca klasy premium",
                    AddtionalPaymentPercentage = 10.00m
                },
                new SeatType
                {
                    Id = 3,
                    Name = "Miejsce zwykłe",
                    SeatColor = "#039aff",
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

            modelBuilder.Entity<HallDetails>().HasData(
               new HallDetails
               {
                   Id = 1,
                   TotalLength = 12m,
                   TotalWidth = 10m,
                   TotalArea = 120m,
                   StageWidth = 5f,
                   StageLength = 6f,
                   NumberOfSeatsRows = 9,
                   MaxNumberOfSeatsRows = 9,
                   NumberOfSeatsColumns = 10,
                   MaxNumberOfSeatsColumns = 10,
                   NumberOfSeats = 90,
                   MaxNumberOfSeats = 90,
               },
               new HallDetails
               {
                   Id = 2,
                   TotalLength = 15m,
                   TotalWidth = 10m,
                   TotalArea = 150m,
                   StageWidth = null,
                   StageLength = null,
                   NumberOfSeatsRows = 15,
                   MaxNumberOfSeatsRows = 15,
                   NumberOfSeatsColumns = 10,
                   MaxNumberOfSeatsColumns = 10,
                   NumberOfSeats = 150,
                   MaxNumberOfSeats = 150,
               },
               new HallDetails
               {
                   Id = 3,
                   TotalLength = 10m,
                   TotalWidth = 8m,
                   TotalArea = 80m,
                   StageWidth = 5f,
                   StageLength = 4f,
                   NumberOfSeatsRows = 6,
                   MaxNumberOfSeatsRows = 6,
                   NumberOfSeatsColumns = 10,
                   MaxNumberOfSeatsColumns = 10,
                   NumberOfSeats = 60,
                   MaxNumberOfSeats = 60,
               },
               new HallDetails
               {
                   Id = 4,
                   TotalLength = 14m,
                   TotalWidth = 10m,
                   TotalArea = 140m,
                   StageWidth = 8f,
                   StageLength = 5f,
                   NumberOfSeatsRows = 10,
                   MaxNumberOfSeatsRows = 10,
                   NumberOfSeatsColumns = 10,
                   MaxNumberOfSeatsColumns = 10,
                   NumberOfSeats = 100,
                   MaxNumberOfSeats = 100,
               }
           );;

            modelBuilder.Entity<Hall>().HasData(
                new Hall
                {
                    Id = 1,
                    DefaultId = 1,
                    HallNr = 1,
                    RentalPricePerHour = 120.99m,
                    Floor = 2,
                    HallTypeId = 1
                },
                new Hall
                {
                    Id = 2,
                    DefaultId = 2,
                    HallNr = 2,
                    RentalPricePerHour = 89.99m,
                    Floor = 1,
                    HallTypeId = 2
                },
                new Hall
                {
                    Id = 3,
                    DefaultId = 3,
                    HallNr = 3,
                    RentalPricePerHour = 179.99m,
                    Floor = 2,
                    HallTypeId = 3
                },
                new Hall
                {
                    Id = 4,
                    DefaultId = 4,
                    HallNr = 4,
                    RentalPricePerHour = 199.99m,
                    Floor = 1,
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

            var epGuid1 = new Guid("b00ca94a-e6b2-4d2e-b270-244b3e76048d");
            var epGuid2 = new Guid("766245b4-8c08-49dd-9480-2606aaa590be");
            var epGuid3 = new Guid("33610a0d-a1b7-4700-bffe-9e334b977e6a");

            modelBuilder.Entity<EventPass>().HasData(
                new EventPass
                {
                    Id = 1,
                    EventPassGuid = epGuid1,
                    StartDate = today,
                    RenewalDate = null,
                    EndDate = today.AddMonths(6),
                    PaymentDate = today,
                    PaymentAmount = 499.99m,
                    PassTypeId = 3,
                    TotalDiscount = 0m,
                    TotalDiscountPercentage = 0m,
                    EventPassJPGName = $"eventflow_karnet_{epGuid1}.jpg",
                    EventPassPDFName = $"eventflow_karnet_{epGuid1}.pdf",
                    UserId = "2",
                    PaymentTypeId = 1
                },

                new EventPass
                {
                    Id = 2,
                    EventPassGuid = epGuid2,
                    StartDate = today,
                    RenewalDate = null,
                    EndDate = today.AddYears(1),
                    PaymentDate = today,
                    PaymentAmount = 999.99m,
                    PassTypeId = 4,
                    TotalDiscount = 0m,
                    TotalDiscountPercentage = 0m,
                    EventPassJPGName = $"eventflow_karnet_{epGuid2}.jpg",
                    EventPassPDFName = $"eventflow_karnet_{epGuid2}.pdf",
                    UserId = "3",
                    PaymentTypeId = 2
                },
                new EventPass
                {
                    Id = 3,
                    EventPassGuid = epGuid3,
                    StartDate = today,
                    RenewalDate = null,
                    EndDate = today.AddMonths(3),
                    PaymentDate = today,
                    PaymentAmount = 235.99m,
                    PassTypeId = 2,
                    TotalDiscount = 0m,
                    TotalDiscountPercentage = 0m,
                    EventPassJPGName = $"eventflow_karnet_{epGuid3}.jpg",
                    EventPassPDFName = $"eventflow_karnet_{epGuid3}.pdf",
                    UserId = "4",
                    PaymentTypeId = 1
                }
            );

            var hrGuid1 = Guid.NewGuid(); 
            var hrGuid2 = Guid.NewGuid(); 
            var hrGuid3 = Guid.NewGuid(); 
            var hrGuid4 = Guid.NewGuid(); 

            modelBuilder.Entity<HallRent>().HasData(
                new HallRent
                {
                    Id = 1,
                    HallRentGuid = hrGuid1,
                    StartDate = today.AddMonths(1).AddDays(1),
                    EndDate = today.AddMonths(1).AddDays(1).AddHours(8),
                    Duration = today.AddMonths(1).AddDays(1).AddHours(8) - today.AddMonths(1).AddDays(1),
                    RentDate = today,
                    PaymentDate = today,
                    PaymentAmount = 899.99m,
                    PaymentTypeId = 1,
                    HallId = 1,
                    UserId = "4"
                },
                new HallRent
                {
                    Id = 2,
                    HallRentGuid = hrGuid2,
                    StartDate = today.AddMonths(1).AddDays(2),
                    EndDate = today.AddMonths(1).AddDays(2).AddHours(4),
                    Duration = today.AddMonths(1).AddDays(2).AddHours(4) - today.AddMonths(1).AddDays(2),
                    RentDate = today,
                    PaymentDate = today,
                    PaymentAmount = 699.99m,
                    PaymentTypeId = 2,
                    HallId = 3,
                    UserId = "3"
                },
                new HallRent
                {
                    Id = 3,
                    HallRentGuid = hrGuid3,
                    StartDate = today.AddMonths(1).AddDays(3),
                    EndDate = today.AddMonths(1).AddDays(3).AddHours(2),
                    Duration = today.AddMonths(1).AddDays(3).AddHours(2) - today.AddMonths(1).AddDays(3),
                    RentDate = today,
                    PaymentDate = today,
                    PaymentAmount = 399.99m,
                    PaymentTypeId = 3,
                    HallId = 3,
                    UserId = "3"
                },
                new HallRent
                {
                    Id = 4,
                    HallRentGuid = hrGuid4,
                    StartDate = today.AddMonths(1).AddDays(4),
                    EndDate = today.AddMonths(1).AddDays(4).AddHours(1),
                    Duration = today.AddMonths(1).AddDays(4).AddHours(1) - today.AddMonths(1).AddDays(4),
                    RentDate = today,
                    PaymentDate = today,
                    PaymentAmount = 150.99m,
                    PaymentTypeId = 2,
                    HallId = 4,
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


            var res1 = new Guid("53538b58-f885-4f4a-b675-a4aa4063ccf3");
            var res2 = new Guid("ed8b9230-223b-4609-8d13-aa6017edad09");
            var res3 = new Guid("f9a076c4-3475-4a28-a60c-6e0e3c03731a");
            var res4 = new Guid("de1d6773-f027-4888-996a-0296e5c52708");
            var firstFestivalGuid = new Guid("0b74ec7b-933b-4163-afa5-e0997681dccd");
            var secondFestivalGuid = new Guid("806cade1-2685-43dc-8cfc-682fc4229db6");

            modelBuilder.Entity<TicketJPG>().HasData(
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


            modelBuilder.Entity<TicketPDF>().HasData(
                new TicketPDF
                {
                    Id = 1,
                    FileName = $"eventflow_bilet_test_{res1}.pdf",
                    ReservationGuid = res1,
                },
                new TicketPDF
                {
                    Id = 2,
                    FileName = $"eventflow_bilet_test_{res2}.pdf",
                    ReservationGuid = res2,
                },
                new TicketPDF
                {
                    Id = 3,
                    FileName = $"eventflow_bilet_test_{res3}.pdf",
                    ReservationGuid = res3,
                },
                new TicketPDF
                {
                    Id = 4,
                    FileName = $"eventflow_bilet_test_{res4}.pdf",
                    ReservationGuid = res4,
                },
                new TicketPDF
                {
                    Id = 5,
                    FileName = $"eventflow_bilet_test_{firstFestivalGuid}.pdf",
                    ReservationGuid = firstFestivalGuid,
                },
                new TicketPDF
                {
                    Id = 6,
                    FileName = $"eventflow_bilet_test_{secondFestivalGuid}.pdf",
                    ReservationGuid = secondFestivalGuid,
                }
            );


            modelBuilder.Entity<Reservation>().HasData(
                new Reservation
                {
                    Id = 1,
                    ReservationGuid = res1,
                    IsFestivalReservation = false,
                    ReservationDate = today,
                    StartOfReservationDate = today.AddMonths(1).AddDays(1),
                    EndOfReservationDate = today.AddMonths(1).AddDays(1).AddHours(1),
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
                    StartOfReservationDate = today.AddMonths(1).AddDays(2),
                    EndOfReservationDate = today.AddMonths(1).AddDays(2).AddHours(3),
                    PaymentDate = today,
                    TotalAddtionalPaymentPercentage = 0m,
                    TotalAdditionalPaymentAmount = 0m,
                    PaymentAmount = 34.99m,
                    UserId = "2",
                    PaymentTypeId = 2,
                    TicketId = 2,
                    TicketPDFId = 2
                },
                new Reservation
                {
                    Id = 3,
                    ReservationGuid = res3,
                    IsFestivalReservation = false,
                    ReservationDate = today,
                    StartOfReservationDate = today.AddMonths(1).AddDays(3),
                    EndOfReservationDate = today.AddMonths(1).AddDays(3).AddHours(2),
                    PaymentDate = today,
                    TotalAddtionalPaymentPercentage = 25m,
                    TotalAdditionalPaymentAmount = 7.5m,
                    PaymentAmount = 29.99m,
                    UserId = "3",
                    PaymentTypeId = 3,
                    TicketId = 3,
                    TicketPDFId = 3
                },
                new Reservation
                {
                    Id = 4,
                    ReservationGuid = res4,
                    IsFestivalReservation = false,
                    ReservationDate = today,
                    StartOfReservationDate = today.AddMonths(1).AddDays(4),
                    EndOfReservationDate = today.AddMonths(1).AddDays(4).AddHours(3),
                    PaymentDate = today,
                    TotalAddtionalPaymentPercentage = 0m,
                    TotalAdditionalPaymentAmount = 0m,
                    PaymentAmount = 19.99m,
                    UserId = "3",
                    PaymentTypeId = 2,
                    TicketId = 4,
                    TicketPDFId = 4
                },
                new Reservation
                {
                    Id = 5,
                    ReservationGuid = firstFestivalGuid,
                    IsFestivalReservation = true,
                    ReservationDate = today,
                    StartOfReservationDate = today.AddMonths(1).AddDays(1),
                    EndOfReservationDate = today.AddMonths(1).AddDays(1).AddHours(1),
                    PaymentDate = today,
                    TotalAddtionalPaymentPercentage = 10m,
                    TotalAdditionalPaymentAmount = 2m,
                    PaymentAmount = 19.99m,
                    UserId = "2",
                    PaymentTypeId = 2,
                    TicketId = 5,
                    TicketPDFId = 5
                },
                new Reservation
                {
                    Id = 6,
                    ReservationGuid = firstFestivalGuid,
                    IsFestivalReservation = true,
                    ReservationDate = today,
                    StartOfReservationDate = today.AddMonths(2).AddDays(1),
                    EndOfReservationDate = today.AddMonths(2).AddDays(1).AddHours(1),
                    PaymentDate = today,
                    TotalAddtionalPaymentPercentage = 10m,
                    TotalAdditionalPaymentAmount = 2m,
                    PaymentAmount = 19.99m,
                    UserId = "2",
                    PaymentTypeId = 2,
                    TicketId = 6,
                    TicketPDFId = 5
                },
                new Reservation
                {
                    Id = 7,
                    ReservationGuid = secondFestivalGuid,
                    IsFestivalReservation = true,
                    ReservationDate = today,
                    StartOfReservationDate = today.AddMonths(1).AddDays(2),
                    EndOfReservationDate = today.AddMonths(1).AddDays(2).AddHours(3),
                    PaymentDate = today,
                    TotalAddtionalPaymentPercentage = 0m,
                    TotalAdditionalPaymentAmount = 0m,
                    PaymentAmount = 29.99m,
                    UserId = "2",
                    PaymentTypeId = 2,
                    TicketId = 7,
                    TicketPDFId = 6
                },
                new Reservation
                {
                    Id = 8,
                    ReservationGuid = secondFestivalGuid,
                    IsFestivalReservation = true,
                    ReservationDate = today,
                    StartOfReservationDate = today.AddMonths(2).AddDays(3),
                    EndOfReservationDate = today.AddMonths(2).AddDays(3).AddHours(2),
                    PaymentDate = today,
                    TotalAddtionalPaymentPercentage = 0m,
                    TotalAdditionalPaymentAmount = 0m,
                    PaymentAmount = 29.99m,
                    UserId = "2",
                    PaymentTypeId = 2,
                    TicketId = 8,
                    TicketPDFId = 6
                }
            );

            modelBuilder.Entity<Reservation_TicketJPG>().HasData(
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
